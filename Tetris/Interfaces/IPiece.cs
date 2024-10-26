using MyGame.Configuration;
using MyGame.Entities;
using MyGame.Enums;

namespace MyGame.Interfaces
{
    public interface IPiece
    {
        (int x, int y) Position { get; set; }
        int[,] Shape { get; set; }
        Image Image { get; set; }
        Image Icon { get; set; }
        Piece CreatePiece(TipoPieza tipo);
        void DrawPiece(int cell, GlobalGameConfiguration config);
        void FixPieceOnBoard(IPiece piece, int totalBoardColumns, int totalBoardRows, int[,] board);
        void Rotate(int totalBoardColumns, int totalBoardRows);
        bool CanRotate(int totalBoardColumns, int totalBoardRows);
        void MoveDown();
        void MoveLeft();
        void MoveRight();
    }
}