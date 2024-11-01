using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Interfaces;
using Tao.Sdl;

namespace Application.Strategies
{
    public interface IInterfaceService
    {
        void DrawBoard();
        void DrawCurrentPiece(IPiece currentPiece, int cellSize, int offsetX);
        void DrawNextPiece(IPiece nextPiece, int offsetX, int offsetY, int cellSize);
        void DrawText(string text, int positionX, int positionY, IntPtr font);
        void DrawHeldPiece(IPiece nextPiece, int offsetX, int offsetY, int cellSize);

        void DrawMenu(IntPtr screen, int configSelectedButtonInterface, List<MenuItem> optionsMenu,
            Sdl.SDL_Color selectedColor, Sdl.SDL_Color normalColor, int menuStartX, int menuStartY, int menuOffsetY,
            int menuImageOffset);
    }
}