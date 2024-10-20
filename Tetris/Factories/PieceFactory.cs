using System;
using MyGame.Entities;
using MyGame.Enums;
using MyGame.Interfaces;

namespace MyGame.Factories
{
    public static class PieceFactory
    {
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

        private static Interfaces.IPiece CreatePieceI()
        {
            return new Piece(new int[,] { { 1, 1, 1, 1 } },
                Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileCyan.png"));
        }

        private static Interfaces.IPiece CreatePieceO()
        {
            return new Piece(new int[,] { { 2, 2 }, { 2, 2 } },
                Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileYellow.png"));
        }

        private static Interfaces.IPiece CreatePieceT()
        {
            return new Piece(new int[,] { { 0, 3, 0 }, { 3, 3, 3 } },
                Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TilePurple.png"));
        }

        private static Interfaces.IPiece CreatePieceL()
        {
            return new Piece(new int[,] { { 4, 0, 0 }, { 4, 4, 4 } },
                Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileOrange.png"));
        }

        private static Interfaces.IPiece CreatePieceJ()
        {
            return new Piece(new int[,]
            {
                { 0, 0, 5 },
                { 5, 5, 5 }
            }, Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileBlue.png"));
        }

        private static Interfaces.IPiece CreatePieceS()
        {
            return new Piece(new int[,]
            {
                { 0, 6, 6 },
                { 6, 6, 0 }
            }, Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TilePurple.png"));
        }

        private static Interfaces.IPiece CreatePieceZ()
        {
            return new Piece(new int[,]
            {
                { 7, 7, 0 },
                { 0, 7, 7 }
            }, Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileRed.png"));
        }
    }
}