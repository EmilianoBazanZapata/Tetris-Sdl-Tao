using System.Collections.Generic;
using MyGame.Configuration;

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
            var offsetX = 90; // Ajusta este valor según lo que necesites

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

        public void ClearCompleteRows(GlobalGameConfiguration configScore)
        {
            int completedRowCount = 0; // Contador de filas completadas

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
                if (!completeRow) continue;
                completedRowCount++; // Incrementa el contador de filas completadas
                RemoveRow(i, Columns);
                configScore.Score += 100;
            }
        }


        private void RemoveRow(int row, int columns)
        {
            // Mover todas las filas superiores una fila hacia abajo
            for (int i = row; i > 0; i--)
            {
                for (int j = 0; j < columns; j++)
                {
                    Board[i, j] = Board[i - 1, j]; // La fila actual toma el valor de la fila superior
                }
            }

            // Limpiar la fila superior (ahora vacía)
            for (int j = 0; j < columns; j++)
            {
                Board[0, j] = 0; // La fila superior ahora queda vacía
            }
        }
        
        public static  int CalculateScore(int completedRows)
        {
            switch (completedRows)
            {
                case 1: return 100; // 1 row
                case 2: return 300; // 2 rows
                case 3: return 500; // 3 rows
                case 4: return 800; // 4 rows (tetris)
                default: return 0;
            }
        }
    }
}