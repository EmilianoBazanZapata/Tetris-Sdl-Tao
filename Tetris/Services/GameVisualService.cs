using MyGame.Interfaces;
using System;

namespace MyGame.Services
{
    public class GameVisualService : IGameVisualService
    {
        public void DrawBoard(Grid gameGrid)
        {
            gameGrid.DrawBoard();
        }

        public void DrawCurrentPiece(IPiece currentPiece, int cellSize)
        {
            currentPiece.DrawPiece(cellSize);
        }

        public void DrawNextPiece(IPiece nextPiece, int offsetX, int offsetY, int cellSize)
        {
            Engine.Draw(nextPiece.Icon.Pointer, offsetX, offsetY);
        }

        public void DrawText(string text, int positionX, int positionY, IntPtr font)
        {
            Engine.DrawText(text, positionX, positionY, 255, 255, 255, font);
        }
        
        public void DrawHeldPiece(IPiece nextPiece, int offsetX, int offsetY, int cellSize)
        {
            if (nextPiece is null)
                return;
            
            Engine.Draw(nextPiece.Icon.Pointer, offsetX, offsetY);
        }
    }
}