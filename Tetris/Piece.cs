using System;
using MyGame.Enums;

namespace MyGame
{
    public class Piece
    {
        public int[,] Shape { get; set; }
        public (int x, int y) Position { get; set; }
        public Image Image { get; set; }

        private Piece(int[,] shape, Image image)
        {
            Shape = shape;
            Position = (0, 0);
            Image = image;
        }

        public Piece()
        {
        }

        public static Piece CreatePiece(TipoPieza tipo)
        {
            switch (tipo)
            {
                case TipoPieza.I:
                    return CreatePieceI();
                case TipoPieza.O:
                    return CreatePieceO();
                case TipoPieza.T:
                    return CreatePieceT();
                case TipoPieza.L:
                    return CreatePieceL();
                case TipoPieza.J:
                    return CreatePieceJ();
                case TipoPieza.S:
                    return CreatePieceS();
                case TipoPieza.Z:
                    return CreatePieceZ();
                default:
                    throw new ArgumentOutOfRangeException(nameof(tipo), "Unknown piece.");
            }
        }

        public void DrawPiece(int cell)
        {
            var rows = Shape.GetLength(0);
            var columns = Shape.GetLength(1);
            int offsetX = 90; // Asegúrate de que este valor coincida con el usado en el tablero

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (Shape[i, j] == 0) continue;

                    // Dibujar la imagen de la pieza en la posición de la celda ocupada, aplicando el desplazamiento
                    Engine.Draw(Image.Pointer, (Position.x + j) * cell + offsetX,
                        (Position.y + i) * cell); // Dibujar la imagen
                }
            }
        }


        #region Fix Piece

        public static void FixPieceOnBoard(Piece piece, int totalBoardColumns, int totalBoardRows,
            int[,] board)
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

        #endregion

        #region Rotate Piece

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


        private bool CanRotate(int totalBoardColumns, int totalBoardRows)
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

        #endregion

        #region Move Piece

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

        #endregion

        #region Game Pieces

        private static Piece CreatePieceI()
        {
            return new Piece(new int[,] { { 1, 1, 1, 1 } },
                Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileCyan.png"));
        }

        private static Piece CreatePieceO()
        {
            return new Piece(new int[,] { { 2, 2 }, { 2, 2 } },
                Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileYellow.png"));
        }

        private static Piece CreatePieceT()
        {
            return new Piece(new int[,] { { 0, 3, 0 }, { 3, 3, 3 } },
                Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TilePurple.png"));
        }

        private static Piece CreatePieceL()
        {
            return new Piece(new int[,] { { 4, 0, 0 }, { 4, 4, 4 } },
                Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileOrange.png"));
        }

        private static Piece CreatePieceJ()
        {
            return new Piece(new int[,]
            {
                { 0, 0, 5 },
                { 5, 5, 5 }
            }, Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileBlue.png"));
        }

        private static Piece CreatePieceS()
        {
            return new Piece(new int[,]
            {
                { 0, 6, 6 },
                { 6, 6, 0 }
            }, Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TilePurple.png"));
        }

        private static Piece CreatePieceZ()
        {
            return new Piece(new int[,]
            {
                { 7, 7, 0 },
                { 0, 7, 7 }
            }, Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileRed.png"));
        }

        #endregion
    }
}
