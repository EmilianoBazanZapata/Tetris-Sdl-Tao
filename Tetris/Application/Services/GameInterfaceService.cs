using System;
using System.Collections.Generic;
using Application.Configurations;
using Application.Strategies;
using Domain.Core;
using Domain.Entities;
using Domain.Interfaces;
using Tao.Sdl;

namespace Application.Services
{
    public class GameInterfaceService : IInterfaceService
    {
        public void DrawBoard()
        {
            DrawBoard(GlobalGameConfiguration.Instance);
        }

        public void DrawCurrentPiece(IPiece currentPiece, int cellSize, int offsetX)
        {
            DrawPiece(currentPiece, cellSize, offsetX);
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
                    color.g, GlobalGameConfiguration.Instance.FontGame);
            }
        }

        private void DrawPiece(IPiece currentPiece, int cell, int offsetX)
        {
            var rows = currentPiece.Shape.GetLength(0);
            var columns = currentPiece.Shape.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (currentPiece.Shape[i, j] == 0) continue;

                    // Dibujar la imagen de la pieza en la posición de la celda ocupada, aplicando el desplazamiento
                    Engine.Draw(currentPiece.Image.Pointer, (currentPiece.Position.x + j) * cell + offsetX,
                        (currentPiece.Position.y + i) * cell); // Dibujar la imagen
                }
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