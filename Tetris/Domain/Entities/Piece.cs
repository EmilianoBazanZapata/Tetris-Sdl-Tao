using Domain.Core;
using Domain.Interfaces;
using MyGame.Enums;

namespace Domain.Entities
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
        
        public Piece CreatePiece(EPieceType type)
        {
            throw new System.NotImplementedException();
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
    }
}