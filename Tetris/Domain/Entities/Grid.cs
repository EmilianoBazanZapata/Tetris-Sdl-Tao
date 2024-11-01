using System.Collections.Generic;
using Domain.Core;

namespace Domain.Entities
{
    public class Grid
    {
        private int[,] Board { get; set; }
        private int Columns { get; set; }
        private int Rows { get; set; }
        private int Cells { get; set; }
        private Image EmptyCellImage { get; set; }
        private Dictionary<int, Image> PieceImages { get; set; }

        public Grid(int[,] board, 
                    int columns, 
                    int rows, 
                    int cells, 
                    Image emptyCellImage,
                    Dictionary<int, Image> pieceImages)
        {
            Board = board;
            Columns = columns;
            Rows = rows;
            Cells = cells;
            EmptyCellImage = emptyCellImage;
            PieceImages = pieceImages;
        }

        /// <summary>
        /// Inicializa el tablero del juego estableciendo todas las celdas en 0, 
        /// lo que indica que están vacías.
        /// </summary>
        public void InitializeBoard()
        {
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    Board[i, j] = 0;
                }
            }
        }
    }
}