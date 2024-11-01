using Domain.Core;
using Domain.Entities;
using MyGame.Enums;

namespace Domain.Interfaces
{
    public interface IPiece
    {
        (int x, int y) Position { get; set; }
        int[,] Shape { get; set; }
        Image Image { get; set; }
        Image Icon { get; set; }
        Piece CreatePiece(EPieceType type);
        void Rotate(int totalBoardColumns, int totalBoardRows);
        bool CanRotate(int totalBoardColumns, int totalBoardRows);
        void MoveDown();
        void MoveLeft();
        void MoveRight();
    }
}