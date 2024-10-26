using System;
using MyGame.Configuration;
using MyGame.Enums;
using MyGame.Factories;
using MyGame.Interfaces;

namespace MyGame.Services
{
    public static class GameLogicService
    {
        public static (IPiece currentPiece, IPiece nextPiece) GenerateRandomPieces(GlobalGameConfiguration config)
        {
            var random = new Random();

            // Generar la pieza actual
            var currentPieceType = (TipoPieza)random.Next(1, 7); // Genera un número entre 1 y 7 para la pieza actual
            var currentPiece = PieceFactory.CreatePiece(currentPieceType);
            currentPiece.Position = config.StartPosition; // Posicionar la nueva pieza en la parte superior del tablero

            // Generar la siguiente pieza
            var nextPieceType = (TipoPieza)random.Next(1, 7); // Genera un número entre 1 y 7 para la siguiente pieza
            var nextPiece = PieceFactory.CreatePiece(nextPieceType);
            nextPiece.Position = config.StartPosition;

            return (currentPiece, nextPiece); // Retornar ambas piezas
        }

        public static void HoldPiece(GlobalGameConfiguration config)
        {
            if (config.HeldPiece == null)
            {
                config.HeldPiece = config.CurrentPiece;
                config.HeldPiece.Position = config.StartPosition;
                config.CurrentPiece = GenerateRandomPieces(config).currentPiece;
            }
            else
            {
                // Si ya hay una pieza guardada, intercambiarla con la pieza actual
                var temp = config.CurrentPiece;
                config.CurrentPiece = config.HeldPiece;
                config.CurrentPiece.Position = config.StartPosition;
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
                (config.NextPiece, _) = GenerateRandomPieces(config); // Generar una nueva siguiente pieza
            }

            // Limpiar filas completas
            ClearCompleteRows(config);

            config.TimeCounter = 0; // Reiniciar el contador
        }

        public static void ClearCompleteRows(GlobalGameConfiguration config)
        {
            var completedRowCount = 0;

            // Iterar desde la última fila hasta la primera
            for (int i = config.Rows - 1; i >= 0; i--)
            {
                var completeRow = true;

                // Verificar si la fila está completamente ocupada
                for (int j = 0; j < config.Columns; j++)
                {
                    if (config.Board[i, j] != 0) continue;
                    completeRow = false;
                    break;
                }

                // Si la fila está completa, desplazar las filas superiores hacia abajo
                if (!completeRow) continue;
                {
                    completedRowCount++;
                    // Mover todas las filas superiores hacia abajo
                    for (int k = i; k > 0; k--)
                    {
                        for (int j = 0; j < config.Columns; j++)
                        {
                            config.Board[k, j] = config.Board[k - 1, j];
                        }
                    }

                    // Vaciar la primera fila
                    for (int j = 0; j < config.Columns; j++)
                    {
                        config.Board[0, j] = 0;
                    }

                    // Ajustar el índice `i` para volver a verificar la fila actual,
                    // ya que ahora contiene la fila desplazada hacia abajo.
                    i++;
                }
            }

            // Calcular el puntaje basado en la cantidad de filas completadas
            config.Score += CalculateScore(completedRowCount);
        }
        
        private static int CalculateScore(int completedRows)
        {
            switch (completedRows)
            {
                case 1: return 100; // 1 row
                case 2: return 300; // 2 rows
                case 3: return 500; // 3 rows
                case 4: return 800; // 4 rows
                default: return 0;
            }
        }
    }
}