using System;
using System.Collections.Generic;
using MyGame.Controllers;
using MyGame.Interfaces;
using static Tao.Sdl.SdlTtf;

namespace MyGame.Configuration
{
    public class GlobalGameConfiguration
    {
        private static GlobalGameConfiguration _instance;

        private static readonly object _lock = new object();

        public Image EmptyCellImage { get; set; }
        public Image PieceJImage { get; set; }
        public Image PieceIImage { get; set; }
        public Image PieceTImage { get; set; }
        public Image PieceOImage { get; set; }
        public Image PieceLImage { get; set; }
        public Image PieceSImage { get; set; }
        public Image PieceZImage { get; set; }
        public Image PieceJIcon { get; set; }
        public Image PieceIIcon { get; set; }
        public Image PieceTIcon { get; set; }
        public Image PieceOIcon { get; set; }
        public Image PieceLIcon { get; set; }
        public Image PieceSIcon { get; set; }
        public Image PieceZIcon { get; set; }

        public Dictionary<int, Image> PieceImages { get; set; }
        public Dictionary<int, Image> PieceICons { get; set; }
        public IPiece CurrentPiece { get; set; }
        public IPiece NextPiece { get; set; }
        public IPiece HeldPiece { get; set; }

        public int Columns { get; set; }
        public int Rows { get; set; }
        public int CellSize { get; set; }
        public int[,] Board { get; set; }
        public Grid GameGrid { get; set; }
        public MovementController MovementController { get; set; }

        public int TimeCounter { get; set; }
        public int DropInterval { get; set; }
        public bool IsHoldKeyPressed { get; set; }
        public bool RotationPerformed { get; set; }
        public bool RotationKeyPressed { get; set; }
        public bool LeftMovementPerformed { get; set; }
        public bool RightMovementPerformed { get; set; }
        public int LateralLeftMovementCounter { get; set; }
        public int LateralMovementInterval { get; set; }
        public int LateralRightMovementCounter { get; set; }
        public bool DownMovementPerformed { get; set; }
        public int DownMovementCounter { get; set; }
        public int DownMovementInterval { get; set; }
        public int Score { get; set; }
        public IntPtr Font { get; set; }

        private GlobalGameConfiguration()
        {
            EmptyCellImage =
                Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\block original (1).png");
            PieceJImage = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileBlue.png");
            PieceIImage = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileCyan.png");
            PieceTImage = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TilePurple.png");
            PieceOImage = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileYellow.png");
            PieceLImage = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileOrange.png");
            PieceSImage = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileGreen.png");
            PieceZImage = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileRed.png");

            PieceImages = new Dictionary<int, Image>
            {
                { 1, PieceIImage },
                { 2, PieceOImage },
                { 3, PieceTImage },
                { 4, PieceLImage },
                { 5, PieceJImage },
                { 6, PieceSImage },
                { 7, PieceZImage }
            };

            PieceJIcon = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\Block-J.png");
            PieceIIcon = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\Block-I.png");
            PieceTIcon = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\Block-T.png");
            PieceOIcon = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\Block-O.png");
            PieceLIcon = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\Block-L.png");
            PieceSIcon = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\Block-S.png");
            PieceZIcon = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\Block-Z.png");

            PieceICons = new Dictionary<int, Image>
            {
                { 1, PieceIIcon },
                { 2, PieceOIcon },
                { 3, PieceTIcon },
                { 4, PieceLIcon },
                { 5, PieceJIcon },
                { 6, PieceSIcon },
                { 7, PieceZIcon }
            };

            Columns = 15;
            Rows = 24;
            CellSize = 30; // Tamaño de cada celda en píxeles

            Board = new int[Rows, Columns]; // Matriz del tablero
            GameGrid = new Grid(Board, Columns, Rows, CellSize, EmptyCellImage, PieceImages);
            MovementController = new MovementController(Board);

            TimeCounter = 0;
            DropInterval = 20; // Controla la velocidad de caída
            RotationPerformed = false;
            RotationKeyPressed = false;
            LeftMovementPerformed = false;
            RightMovementPerformed = false;
            LateralMovementInterval = 7; // Ajusta según la velocidad deseada
            DownMovementPerformed = false;
            DownMovementCounter = 0;
            DownMovementInterval = 7; // Ajusta según la velocidad deseada
            LateralRightMovementCounter = 7; // Ajusta según la velocidad deseada
            Font = TTF_OpenFont("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\Fonts\\PressStart2P-Regular.ttf", 24);
            Score = 0;
            IsHoldKeyPressed = false;
        }

        public static GlobalGameConfiguration Instance
        {
            get
            {
                // Double-check locking for thread safety
                if (_instance != null) return _instance;
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new GlobalGameConfiguration();
                    }
                }

                return _instance;
            }
        }
    }
}