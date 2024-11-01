using System;
using System.IO;
using Domain.Core;
using Domain.Entities;
using Domain.Interfaces;
using MyGame.Enums;

namespace Application.Factories
{
    public static class PieceFactory
    {
        private static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string ProjectDirectory = Directory.GetParent(BaseDirectory).Parent.Parent.FullName;
        private static readonly string AssetsPath = Path.Combine(ProjectDirectory, "Infrastructure", "Assets", "Images");

        public static IPiece CreatePiece(EPieceType tipo)
        {
            switch (tipo)
            {
                case EPieceType.I:
                    return CreatePieceI();
                case EPieceType.O:
                    return CreatePieceO();
                case EPieceType.T:
                    return CreatePieceT();
                case EPieceType.L:
                    return CreatePieceL();
                case EPieceType.J:
                    return CreatePieceJ();
                case EPieceType.S:
                    return CreatePieceS();
                case EPieceType.Z:
                    return CreatePieceZ();
                default:
                    throw new ArgumentOutOfRangeException(nameof(tipo), "Unknown piece.");
            }
        }

        private static IPiece CreatePieceI()
        {
            return new Piece(
                new int[,] { { 1, 1, 1, 1 } },
                Engine.LoadImage(Path.Combine(AssetsPath, "TileCyan.png")), 
                Engine.LoadImage(Path.Combine(AssetsPath, "Block-I.png")));
        }

        private static IPiece CreatePieceO()
        {
            return new Piece(
                new int[,] { { 2, 2 }, { 2, 2 } },
                Engine.LoadImage(Path.Combine(AssetsPath, "TileYellow.png")),
                Engine.LoadImage(Path.Combine(AssetsPath, "Block-O.png")));
        }

        private static IPiece CreatePieceT()
        {
            return new Piece(
                new int[,] { { 0, 3, 0 }, { 3, 3, 3 } },
                Engine.LoadImage(Path.Combine(AssetsPath, "TilePurple.png")),
                Engine.LoadImage(Path.Combine(AssetsPath, "Block-T.png")));
        }

        private static IPiece CreatePieceL()
        {
            return new Piece(
                new int[,] { { 4, 0, 0 }, { 4, 4, 4 } },
                Engine.LoadImage(Path.Combine(AssetsPath, "TileOrange.png")),
                Engine.LoadImage(Path.Combine(AssetsPath, "Block-L.png")));
        }

        private static IPiece CreatePieceJ()
        {
            return new Piece(
                new int[,] { { 0, 0, 5 }, { 5, 5, 5 } },
                Engine.LoadImage(Path.Combine(AssetsPath, "TileBlue.png")),
                Engine.LoadImage(Path.Combine(AssetsPath, "Block-J.png")));
        }

        private static IPiece CreatePieceS()
        {
            return new Piece(
                new int[,] { { 0, 6, 6 }, { 6, 6, 0 } },
                Engine.LoadImage(Path.Combine(AssetsPath, "TileGreen.png")),
                Engine.LoadImage(Path.Combine(AssetsPath, "Block-S.png")));
        }

        private static IPiece CreatePieceZ()
        {
            return new Piece(
                new int[,] { { 7, 7, 0 }, { 0, 7, 7 } },
                Engine.LoadImage(Path.Combine(AssetsPath, "TileRed.png")),
                Engine.LoadImage(Path.Combine(AssetsPath, "Block-Z.png")));
        }
    }
}
