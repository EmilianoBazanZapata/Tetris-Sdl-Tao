using System;
using System.Collections.Generic;
using Domain.Entities;
using Tao.Sdl;

namespace Application.Strategies
{
    public interface IInterfaceService
    {
        void DrawBoard();
        void DrawCurrentPiece(Piece currentPiece, int cellSize, int offsetX);
        void DrawNextPiece(Piece nextPiece, int offsetX, int offsetY, int cellSize);
        void DrawText(string text, int positionX, int positionY, IntPtr font);
        void DrawHeldPiece(Piece nextPiece, int offsetX, int offsetY, int cellSize);

        void DrawMenu(IntPtr screen, int configSelectedButtonInterface, List<MenuItem> optionsMenu,
            Sdl.SDL_Color selectedColor, Sdl.SDL_Color normalColor, int menuStartX, int menuStartY, int menuOffsetY,
            int menuImageOffset);
    }
}