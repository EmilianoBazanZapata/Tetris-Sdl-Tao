using System.Collections.Generic;

namespace MyGame
{
    public class Grid
    {
        private int[,] Board { get; set; }
        private int Columns { get; set; }
        private int Rows { get; set; }
        private int Cells { get; set; }
        private Image EmptyCellImage { get; set; }
        private Dictionary<int, Image> PieceImages { get; set; }

        public Grid(int[,] board, int columns, int rows, int cells, Image emptyCellImage,
            Dictionary<int, Image> pieceImages)
        {
            Board = board;
            Columns = columns;
            Rows = rows;
            Cells = cells;
            EmptyCellImage = emptyCellImage;
            PieceImages = pieceImages;
        }

        public void InitializeBoard()
        {
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    Board[i, j] = 0; // Celdas vacías
                }
            }
        }

        public void DrawBoard()
        {
            int offsetX = 90; // Ajusta este valor según lo que necesites

            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    var pieceType = Board[i, j];

                    if (pieceType == 0)
                    {
                        Engine.Draw(EmptyCellImage.Pointer, j * Cells + offsetX, i * Cells);
                    }
                    else
                    {
                        Engine.Draw(PieceImages[pieceType].Pointer, j * Cells + offsetX, i * Cells);
                    }
                }
            }
        }

        public void ClearCompleteRows()
        {
            for (int i = Rows - 1; i >= 0; i--) // Empezar desde abajo
            {
                bool completeRow = true;

                // Verificar si la fila está completamente ocupada
                for (int j = 0; j < Columns; j++)
                {
                    if (Board[i, j] == 0) // Si encontramos una celda vacía
                    {
                        completeRow = false;
                        break;
                    }
                }

                // Si la fila está completa, la limpiamos
                if (completeRow)
                {
                    RemoveRow(i);
                }
            }
        }

        private void RemoveRow(int row)
        {
            // Mover todas las filas superiores una fila hacia abajo
            for (int i = row; i > 0; i--)
            {
                for (int j = 0; j < 20; j++)
                {
                    Board[i, j] = Board[i - 1, j]; // La fila actual toma el valor de la fila superior
                }
            }

            // Limpiar la fila superior (ahora vacía)
            for (int j = 0; j < 20; j++)
            {
                Board[0, j] = 0; // La fila superior ahora queda vacía
            }
        }

        // Método para calcular el color en formato RGB888
        private static uint MapRgb(byte r, byte g, byte b)
        {
            return (uint)((r << 16) | (g << 8) | b); // Combina los valores RGB en un solo uint
        }
    }
}
