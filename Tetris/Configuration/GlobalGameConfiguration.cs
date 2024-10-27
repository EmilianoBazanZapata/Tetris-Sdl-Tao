using System;
using System.Collections.Generic;
using System.IO;
using MyGame.Controllers;
using MyGame.Interfaces;
using Tao.Sdl;
using static Tao.Sdl.SdlTtf;

namespace MyGame.Configuration
{
    public class GlobalGameConfiguration
    {
        // Singleton
        private static GlobalGameConfiguration _instance;
        private static readonly object _lock = new object();

        // Recursos gráficos y fuentes
        public Image EmptyCellImage,
            PieceJImage,
            PieceIImage,
            PieceTImage,
            PieceOImage,
            PieceLImage,
            PieceSImage,
            PieceZImage;

        public Dictionary<int, Image> PieceImages { get; set; }
        public IntPtr Font { get; private set; }

        // Tablero y controladores
        public Grid GameGrid { get; private set; }
        public MovementController MovementController { get; private set; }
        public int[,] Board { get; private set; }

        // Estado del juego
        public IPiece CurrentPiece, NextPiece, HeldPiece;

        public bool IsHoldKeyPressed,
            RotationPerformed,
            LeftMovementPerformed,
            RightMovementPerformed,
            DownMovementPerformed,
            RotationKeyPressed;

        public int LateralLeftMovementCounter, LateralRightMovementCounter, DownMovementCounter, TimeCounter, Score;

        // Configuración de movimiento
        public int LateralMovementInterval = 7, DownMovementInterval = 7, DropInterval = 20;

        // Configuración de la interfaz
        public int Columns = 15, Rows = 24, CellSize = 30;
        public int PositionInterfaceX = 630, OffsetX = 150, MenuStartX = 0, MenuStartY = 0, MenuOffsetY = 50;

        public Sdl.SDL_Color SelectedColor = new Sdl.SDL_Color(255, 0, 0),
            NormalColor = new Sdl.SDL_Color(255, 255, 255);

        public Menu Menu { get; set; }
        public int SelectedButtonInterface;

        // Otros
        public bool Running = true;
        public (int x, int y) StartPosition = (6, 0);

        // Pantalla
        public IntPtr Screen { get; set; }

        // Constructor privado
        private GlobalGameConfiguration()
        {
            LoadResources();
            InitializeGameGrid();
        }

        // Singleton pattern
        public static GlobalGameConfiguration Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new GlobalGameConfiguration();
                        }
                    }
                }

                return _instance;
            }
        }

        // Método para cargar imágenes y fuentes
        private void LoadResources()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.FullName;
            var assetsPath = Path.Combine(projectDirectory, "assets");
            var fontPath = Path.Combine(projectDirectory, "Fonts");

            EmptyCellImage = Engine.LoadImage(Path.Combine(assetsPath, "block original (1).png"));
            PieceJImage = Engine.LoadImage(Path.Combine(assetsPath, "TileBlue.png"));
            PieceIImage = Engine.LoadImage(Path.Combine(assetsPath, "TileCyan.png"));
            PieceTImage = Engine.LoadImage(Path.Combine(assetsPath, "TilePurple.png"));
            PieceOImage = Engine.LoadImage(Path.Combine(assetsPath, "TileYellow.png"));
            PieceLImage = Engine.LoadImage(Path.Combine(assetsPath, "TileOrange.png"));
            PieceSImage = Engine.LoadImage(Path.Combine(assetsPath, "TileGreen.png"));
            PieceZImage = Engine.LoadImage(Path.Combine(assetsPath, "TileRed.png"));

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

            Font = TTF_OpenFont(Path.Combine(fontPath, "PressStart2P-Regular.ttf"), 24);
        }

        // Método para inicializar el tablero y controlador
        private void InitializeGameGrid()
        {
            Board = new int[Rows, Columns];
            GameGrid = new Grid(Board, Columns, Rows, CellSize, EmptyCellImage, PieceImages);
            MovementController = new MovementController(Board);
        }
    }
}