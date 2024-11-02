using System;
using System.Collections.Generic;
using System.IO;
using Domain.Core;
using Domain.Entities;
using Tao.Sdl;

namespace Application.Configurations
{
    public class GlobalGameConfiguration
    {
        private static GlobalGameConfiguration _instance;
        private static readonly object _lock = new object();

        #region Recursos gráficos y fuentes
        public Image EmptyCellImage, PieceJImage, PieceIImage, PieceTImage, PieceOImage, PieceLImage, PieceSImage, PieceZImage;
        public Dictionary<int, Image> PieceImages { get; set; }
        public IntPtr FontGame { get; private set; }
        #endregion

        #region Tablero y estado del juego
        public Grid GameGrid { get; private set; }
        public int[,] Board { get; set; }
        public Piece CurrentPiece, NextPiece, HeldPiece;
        #endregion

        #region Controles de movimiento y estado del juego
        public bool IsHoldKeyPressed, RotationPerformed, LeftMovementPerformed, RightMovementPerformed, DownMovementPerformed, RotationKeyPressed;
        public int LateralLeftMovementCounter, LateralRightMovementCounter, DownMovementCounter, TimeCounter, Score;
        public int LateralMovementInterval = 7, DownMovementInterval = 7, DropInterval = 20;
        public int ConditionCoreGame = 999999;
        #endregion

        #region Configuración de la interfaz
        public int Columns = 15, Rows = 24, CellSize = 30;
        public int PositionInterfaceX = 630, OffsetX = 150, MenuStartX = 325, MenuStartY = 225, MenuOffsetY = 50, OptionHeight = 30, OptionWidth = 200;
        public int MenuImageOffset { get; set; } = 300;
        public Sdl.SDL_Color SelectedColor = new Sdl.SDL_Color(255, 0, 0), NormalColor = new Sdl.SDL_Color(255, 255, 255);
        public Menu Menu { get; set; }
        public int SelectedButtonInterface;
        #endregion

        #region Control de ejecución y pantalla
        public bool Running = true;
        public (int x, int y) StartPosition = (6, 0);
        public IntPtr Screen { get; set; }
        #endregion

        #region Constructor y Singleton
        private GlobalGameConfiguration()
        {
            LoadResources();
            InitializeGameGrid();
        }
        
        public static GlobalGameConfiguration GetInstance
        {
            get
            {
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
        #endregion

        #region Métodos de inicialización de recursos
        private void LoadResources()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.FullName;
            var assetsPath = Path.Combine(projectDirectory, "Infrastructure", "Assets", "Images");
            var fontPath = Path.Combine(projectDirectory, "Infrastructure", "Assets", "Fonts");

            EmptyCellImage = Engine.LoadImage(Path.Combine(assetsPath, "EmptyBlock.png"));
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

            FontGame = Engine.LoadFont(Path.Combine(fontPath, "PressStart2P-Regular.ttf"), 24);
        }

        private void InitializeGameGrid()
        {
            Board = new int[Rows, Columns];
            GameGrid = new Grid(Board, Columns, Rows, CellSize, EmptyCellImage, PieceImages);
        }
        #endregion
    }
}
