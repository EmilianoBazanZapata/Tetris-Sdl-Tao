using System;
using MyGame.Configuration;
using MyGame.Enums;
using MyGame.Factories;
using MyGame.Interfaces;
using MyGame.Services;

namespace MyGame.Entities
{
    public class Piece : IPiece
    {
        public int[,] Shape { get; set; }
        public (int x, int y) Position { get; set; }
        public Image Image { get; set; }
        public Image Icon { get; set; }

        public Piece(int[,] shape, Image image, Image icon)
        {
            Shape = shape;
            Position = (0, 0);
            Image = image;
            Icon = icon;
        }
        
        public Piece CreatePiece(TipoPieza tipo)
        {
            throw new System.NotImplementedException();
        }

        public void DrawPiece(int cell, GlobalGameConfiguration config)
        {
            var rows = Shape.GetLength(0);
            var columns = Shape.GetLength(1);
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (Shape[i, j] == 0) continue;

                    // Dibujar la imagen de la pieza en la posición de la celda ocupada, aplicando el desplazamiento
                    Engine.Draw(Image.Pointer, (Position.x + j) * cell + config.OffsetX, (Position.y + i) * cell); // Dibujar la imagen
                }
            }
        }

        public void FixPieceOnBoard(IPiece piece, int totalBoardColumns, int totalBoardRows, int[,] board)
        {
            var rows = piece.Shape.GetLength(0);
            var columns = piece.Shape.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (piece.Shape[i, j] == 0) continue;
                    var x = piece.Position.x + j;
                    var y = piece.Position.y + i;

                    // Asegurar que la pieza no se fije fuera de los límites del tablero
                    if (x >= 0 && x < totalBoardColumns && y >= 0 && y < totalBoardRows)
                    {
                        board[y, x] = piece.Shape[i, j]; // Marcar la celda como ocupada
                    }
                }
            }
        }
        

        public void Rotate(int totalBoardColumns, int totalBoardRows)
        {
            if (!CanRotate(totalBoardColumns, totalBoardRows))
                return;

            var rows = Shape.GetLength(0);
            var columns = Shape.GetLength(1);

            // Realizar la rotación de la pieza (90 grados)
            var newShape = new int[columns, rows];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    newShape[j, rows - i - 1] = Shape[i, j];
                }
            }

            Shape = newShape; // Actualizar la forma de la pieza con la nueva forma rotada
        }

        public bool CanRotate(int totalBoardColumns, int totalBoardRows)
        {
            var rows = Shape.GetLength(0);
            var columns = Shape.GetLength(1);

            // Verificar las nuevas posiciones directamente sin crear una nueva matriz
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (Shape[i, j] != 1) continue;

                    // Calcular las nuevas posiciones como si la pieza estuviera rotada
                    var newX = Position.x + (rows - i - 1);
                    var newY = Position.y + j;

                    // Verificar si las nuevas posiciones se salen del tablero
                    if (newX < 0 || newX >= totalBoardColumns || newY < 0 || newY >= totalBoardRows)
                        return false;
                }
            }

            return true;
        }


        // Método para mover la pieza hacia abajo
        public void MoveDown()
        {
            Position = (Position.x, Position.y + 1);
        }

        // Método para mover la pieza a la izquierda
        public void MoveLeft()
        {
            Position = (Position.x - 1, Position.y);
        }

        // Método para mover la pieza a la derecha
        public void MoveRight()
        {
            Position = (Position.x + 1, Position.y);
        }


        public void MovePieceAutomatically(GlobalGameConfiguration config)
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
                FixPieceOnBoard(config.CurrentPiece,
                    config.Columns,
                    config.Rows,
                    config.Board);

                // Limpiar filas completas
                GameLogicService.ClearCompleteRows(config);

                // Generar una nueva pieza (actualizar piezaActual y piezaSiguiente)
                config.CurrentPiece = config.NextPiece; // La actual se convierte en la siguiente
                (config.NextPiece, _) = GenerateRandomPieces(); // Generar una nueva siguiente pieza
            }

            config.TimeCounter = 0; // Reiniciar el contador
        }

        public static (IPiece currentPiece, IPiece nextPiece) GenerateRandomPieces()
        {
            var random = new Random();

            // Generar la pieza actual
            var currentPieceType = (TipoPieza)random.Next(1, 7); // Genera un número entre 1 y 7 para la pieza actual
            var currentPiece = PieceFactory.CreatePiece(currentPieceType);
            currentPiece.Position = (10, 0); // Posicionar la nueva pieza en la parte superior del tablero

            // Generar la siguiente pieza
            var nextPieceType =
                (TipoPieza)random.Next(1, 7); // Genera un número entre 1 y 7 para la siguiente pieza
            var nextPiece = PieceFactory.CreatePiece(nextPieceType);
            nextPiece.Position = (10, 0);

            return (currentPiece, nextPiece); // Retornar ambas piezas
        }
    }
}