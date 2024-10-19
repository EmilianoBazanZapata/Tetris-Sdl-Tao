using System.Collections.Generic;

namespace MyGame.Managers
{
    public class GlobalGameConfiguration
    {
        private static GlobalGameConfiguration _instance;

        // Lock object for thread safety
        private static readonly object _lock = new object();
        
        public Image EmptyCellImage { get; set; }
        public Image PieceJImage { get; set; }
        public Image PieceIImage { get; set; }
        public Image PieceTImage { get; set; }
        public Image PieceOImage { get; set; }
        public Image PieceLImage { get; set; }
        public Image PieceSImage { get; set; }
        public Image PieceZImage { get; set; }

        public Dictionary<int, Image> PieceImages { get; set; }
        public Pieza CurrentPiece { get; set; }
        public Pieza NextPiece { get; set; }

        public int Columns { get; set; }
        public int Rows { get; set; }
        public int CellSize { get; set; }
        public int[,] Board { get; set; }
        public Grilla GameGrid { get; set; }
        public MovementController MovementController { get; set; }

        public int TimeCounter { get; set; }
        public int DropInterval { get; set; }
        public bool RotationPerformed { get; set; }
        public bool RotationKeyPressed { get; set; }
        public bool LeftMovementPerformed { get; set; }
        public bool RightMovementPerformed { get; set; }
        public int LateralMovementCounter { get; set; }
        public int LateralMovementInterval { get; set; }
        public bool DownMovementPerformed { get; set; }
        public int DownMovementCounter { get; set; }
        public int DownMovementInterval { get; set; }

        private  GlobalGameConfiguration()
        {
            EmptyCellImage = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\block original (1).png");
            PieceJImage = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileBlue.png");
            PieceIImage = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileCyan.png");
            PieceTImage = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TilePurple.png");
            PieceOImage = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileYellow.png");
            PieceLImage = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileOrange.png");
            PieceSImage = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TilePurple.png");
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

            Columns = 20;
            Rows = 24;
            CellSize = 30; // Tamaño de cada celda en píxeles

            Board = new int[Rows, Columns]; // Matriz del tablero
            GameGrid = new Grilla(Board, Columns, Rows, CellSize, EmptyCellImage, PieceImages);
            MovementController = new MovementController(Board);
            
            TimeCounter = 0;
            DropInterval = 20; // Controla la velocidad de caída
            RotationPerformed = false;
            RotationKeyPressed = false;
            LeftMovementPerformed = false;
            RightMovementPerformed = false;
            LateralMovementCounter = 0;
            LateralMovementInterval = 10; // Ajusta según la velocidad deseada
            DownMovementPerformed = false;
            DownMovementCounter = 0;
            DownMovementInterval = 10; // Ajusta según la velocidad deseada
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