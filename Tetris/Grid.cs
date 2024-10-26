using System.Collections.Generic;
using MyGame.Configuration;

namespace MyGame
{
    public class Grid
    {
        private int[,] Board { get; }
        private int Columns { get; }
        private int Rows { get; }
        private int Cells { get; }
        private Image EmptyCellImage { get; }
        private Dictionary<int, Image> PieceImages { get; }

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

        public void DrawBoard(GlobalGameConfiguration config)
        {
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    var pieceType = Board[i, j];

                    Engine.Draw(pieceType == 0 ? EmptyCellImage.Pointer : PieceImages[pieceType].Pointer,
                        j * Cells + config.OffsetX, i * Cells);
                }
            }
        }
    }
}