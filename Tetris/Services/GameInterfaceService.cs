using MyGame.Interfaces;
using System;
using System.Collections.Generic;
using MyGame.Configuration;
using MyGame.Menues;
using Tao.Sdl;

namespace MyGame.Services
{
    public class GameInterfaceService : IInterfaceService
    {
        public void DrawBoard()
        {
            DrawBoard(GlobalGameConfiguration.Instance);
        }

        public void DrawCurrentPiece(IPiece currentPiece, int cellSize)
        {
            currentPiece.DrawPiece(cellSize, GlobalGameConfiguration.Instance);
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

        public void DrawMenu(IntPtr screen,
                             int configSelectedButtonInterface,
                             List<MenuItem> optionsMenu,
                             Sdl.SDL_Color selectedColor,
                             Sdl.SDL_Color normalColor,
                             int menuStartX,
                             int menuStartY,
                             int menuOffsetY,
                             int menuImageOffset)
        {
            for (int i = 0; i < optionsMenu.Count; i++)
            {
                if (optionsMenu[i].Image != null)
                {
                    Engine.Draw(optionsMenu[i].Image, 0, 0);
                    menuStartY += menuImageOffset;
                }

                var color = (i == configSelectedButtonInterface) ? selectedColor : normalColor;

                var optionText = optionsMenu[i].Text;

                Engine.DrawText(optionText, menuStartX, (short)(menuStartY + i * menuOffsetY), color.r, color.b,
                    color.g, GlobalGameConfiguration.Instance.Font);
            }
        }
        private static void DrawBoard(GlobalGameConfiguration config)
        {
            for (var i = 0; i < config.Rows; i++)
            {
                for (var j = 0; j < config.Columns; j++)
                {
                    var pieceType = config.Board[i, j];

                    Engine.Draw(pieceType == 0 ? config.EmptyCellImage.Pointer : config.PieceImages[pieceType].Pointer,
                        j * config.CellSize + config.OffsetX, i * config.CellSize);
                }
            }
        }
    }
}