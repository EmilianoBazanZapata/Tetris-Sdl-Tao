using System;

namespace MyGame.Interfaces
{
    public interface IGameVisualService
    {
        void DrawBoard(Grid gameGrid);
        void DrawCurrentPiece(IPiece currentPiece, int cellSize);
        void DrawNextPiece(IPiece nextPiece, int offsetX, int offsetY, int cellSize);
        void DrawText(string text, int positionX, int positionY, IntPtr font);
    }
}