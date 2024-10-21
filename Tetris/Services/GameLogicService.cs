using System;
using MyGame.Configuration;
using MyGame.Enums;
using MyGame.Factories;
using MyGame.Interfaces;

namespace MyGame.Services
{
    public static class GameLogicService
    {
        public static (IPiece currentPiece, IPiece nextPiece) GenerateRandomPieces()
        {
            var random = new Random();

            // Generar la pieza actual
            var currentPieceType = (TipoPieza)random.Next(1, 7); // Genera un número entre 1 y 7 para la pieza actual
            var currentPiece = PieceFactory.CreatePiece(currentPieceType);
            currentPiece.Position = (10, 0); // Posicionar la nueva pieza en la parte superior del tablero

            // Generar la siguiente pieza
            var nextPieceType = (TipoPieza)random.Next(1, 7); // Genera un número entre 1 y 7 para la siguiente pieza
            var nextPiece = PieceFactory.CreatePiece(nextPieceType);
            nextPiece.Position = (10, 0);

            return (currentPiece, nextPiece); // Retornar ambas piezas
        }
        
        public static void HoldPiece(GlobalGameConfiguration config)
        {
            if (config.HeldPiece == null)
            {
                config.HeldPiece = config.CurrentPiece;
                config.HeldPiece.Position = (10, 0);
                config.CurrentPiece = GenerateRandomPieces().currentPiece;
            }
            else
            {
                // Si ya hay una pieza guardada, intercambiarla con la pieza actual
                var temp = config.CurrentPiece;
                config.CurrentPiece = config.HeldPiece;
                config.CurrentPiece.Position = (10, 0);
                config.HeldPiece = temp; // Intercambiar las piezas
            }
        }

        public static void MovePieceAutomatically(GlobalGameConfiguration config)
        {
            // Mover la pieza hacia abajo después de cierto tiempo
            if (config.TimeCounter < config.DropInterval) return;

            // Verificar si la pieza puede moverse hacia abajo
            if (config.MovementController.CanMoveDown(config.CurrentPiece, config.Rows))
            {
                config.CurrentPiece.MoveDown();
            }
            else
            {
                // Fijar la pieza en el tablero
                config.CurrentPiece.FixPieceOnBoard(config.CurrentPiece,
                    config.Columns,
                    config.Rows,
                    config.Board);

                // Generar una nueva pieza (actualizar piezaActual y piezaSiguiente)
                config.CurrentPiece = config.NextPiece; // La actual se convierte en la siguiente
                (config.NextPiece, _) = GenerateRandomPieces(); // Generar una nueva siguiente pieza
            }
            
            // Limpiar filas completas
            config.GameGrid.ClearCompleteRows(config);

            config.TimeCounter = 0; // Reiniciar el contador
        }
    }
}