using System;
using MyGame.Entities;
using MyGame.Enums;
using MyGame.Interfaces;
using System.IO;

namespace MyGame.Factories
{
    public static class PieceFactory
    {
        private static readonly string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.FullName;
        private static readonly string assetsPath = Path.Combine(projectDirectory, "assets");

        public static IPiece CreatePiece(TipoPieza tipo)
        {
            switch (tipo)
            {
                case TipoPieza.I:
                    return CreatePieceI();
                case TipoPieza.O:
                    return CreatePieceO();
                case TipoPieza.T:
                    return CreatePieceT();
                case TipoPieza.L:
                    return CreatePieceL();
                case TipoPieza.J:
                    return CreatePieceJ();
                case TipoPieza.S:
                    return CreatePieceS();
                case TipoPieza.Z:
                    return CreatePieceZ();
                default:
                    throw new ArgumentOutOfRangeException(nameof(tipo), "Unknown piece.");
            }
        }

        private static IPiece CreatePieceI()
        {
            return new Piece(
                new int[,] { { 1, 1, 1, 1 } },
                Engine.LoadImage(Path.Combine(assetsPath, "TileCyan.png")), 
                Engine.LoadImage(Path.Combine(assetsPath, "Block-I.png")));
        }

        private static IPiece CreatePieceO()
        {
            return new Piece(
                new int[,] { { 2, 2 }, { 2, 2 } },
                Engine.LoadImage(Path.Combine(assetsPath, "TileYellow.png")),
                Engine.LoadImage(Path.Combine(assetsPath, "Block-O.png")));
        }

        private static IPiece CreatePieceT()
        {
            return new Piece(
                new int[,] { { 0, 3, 0 }, { 3, 3, 3 } },
                Engine.LoadImage(Path.Combine(assetsPath, "TilePurple.png")),
                Engine.LoadImage(Path.Combine(assetsPath, "Block-T.png")));
        }

        private static IPiece CreatePieceL()
        {
            return new Piece(
                new int[,] { { 4, 0, 0 }, { 4, 4, 4 } },
                Engine.LoadImage(Path.Combine(assetsPath, "TileOrange.png")),
                Engine.LoadImage(Path.Combine(assetsPath, "Block-L.png")));
        }

        private static IPiece CreatePieceJ()
        {
            return new Piece(
                new int[,] { { 0, 0, 5 }, { 5, 5, 5 } },
                Engine.LoadImage(Path.Combine(assetsPath, "TileBlue.png")),
                Engine.LoadImage(Path.Combine(assetsPath, "Block-J.png")));
        }

        private static IPiece CreatePieceS()
        {
            return new Piece(
                new int[,] { { 0, 6, 6 }, { 6, 6, 0 } },
                Engine.LoadImage(Path.Combine(assetsPath, "TileGreen.png")),
                Engine.LoadImage(Path.Combine(assetsPath, "Block-S.png")));
        }

        private static IPiece CreatePieceZ()
        {
            return new Piece(
                new int[,] { { 7, 7, 0 }, { 0, 7, 7 } },
                Engine.LoadImage(Path.Combine(assetsPath, "TileRed.png")),
                Engine.LoadImage(Path.Combine(assetsPath, "Block-Z.png")));
        }
    }
}
