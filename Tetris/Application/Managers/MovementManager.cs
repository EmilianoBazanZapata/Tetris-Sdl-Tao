using Application.Configurations;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Managers
{
    public class MovementManager : IGameStateObserver
    {
        private static MovementManager _instance;
        private readonly GlobalGameConfiguration _config = GlobalGameConfiguration.GetInstance;
        private static readonly object _lock = new object();
        private EGameState _currentState;
        private IGameLogicService _gameLogicService;
        
        private int[,] Board { get; set; }

        private MovementManager(int[,] board)
        {
            Board = board;
        }

        public static MovementManager GetInstance(int[,] board, IGameLogicService gameLogicService)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new MovementManager(board);
                    _instance._gameLogicService = gameLogicService;
                }
            }

            return _instance;
        }

        #region Verificar Movimiento

        public bool CanMoveDown(Piece piece, int boardRows)
        {
            if (_currentState != EGameState.InMenu) return false;
            
            var rows = piece.Shape.GetLength(0);
            var columns = piece.Shape.GetLength(1);

            if (piece.Position.y + rows >= boardRows)
                return false;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (piece.Shape[i, j] == 0) continue;
                    if (Board[(piece.Position.y + i) + 1, (piece.Position.x + j)] != 0)
                        return false;
                }
            }

            return true;
        }

        public bool CanMoveLeft(Piece piece)
        {
            if (_currentState != EGameState.InMenu) return false;
            
            var rows = piece.Shape.GetLength(0);
            var columns = piece.Shape.GetLength(1);

            if (piece.Position.x <= 0)
                return false;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (piece.Shape[i, j] == 0) continue;
                    if (Board[(piece.Position.y + i), (piece.Position.x + j) - 1] != 0)
                        return false;
                }
            }

            return true;
        }

        public bool CanMoveRight(Piece piece, GlobalGameConfiguration config)
        { 
            var rows = piece.Shape.GetLength(0);
            var columns = piece.Shape.GetLength(1);

            if (piece.Position.x + columns >= config.Columns)
                return false;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (piece.Shape[i, j] == 0) continue;
                    if (Board[(piece.Position.y + i), (piece.Position.x + j) + 1] != 0)
                        return false;
                }
            }

            return true;
        }

        public bool CanRotate(Piece piece, int totalBoardColumns, int totalBoardRows)
        { 
            var rows = piece.Shape.GetLength(0);
            var columns = piece.Shape.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    var newX = piece.Position.x + (rows - i - 1);
                    var newY = piece.Position.y + j;

                    if (newX < 0 || newX >= totalBoardColumns || newY < 0 || newY >= totalBoardRows)
                        return false;
                }
            }

            return true;
        }

        #endregion

        #region Realizar Movimiento

        public void MovePieceAutomatically()
        {
            // Mover la pieza hacia abajo después de cierto tiempo
            if (_config.TimeCounter < _config.DropInterval) return;

            // Verificar si la pieza puede moverse hacia abajo
            if (CanMoveDown(_config.CurrentPiece, _config.Rows))
            {
                MoveDown(_config.CurrentPiece);
            }
            else
            {
                // Fijar la pieza en el tablero
                _gameLogicService.FixPieceOnBoard(_config.CurrentPiece,
                    _config.Columns,
                    _config.Rows,
                    _config.Board);

                // Limpiar filas completas
                _gameLogicService.ClearCompleteRows();

                // Generar una nueva pieza (actualizar piezaActual y piezaSiguiente)
                _config.CurrentPiece = _config.NextPiece; // La actual se convierte en la siguiente
                (_config.NextPiece, _) = _gameLogicService.GenerateRandomPieces(); // Generar una nueva siguiente pieza
            }

            _config.TimeCounter = 0; // Reiniciar el contador
        }
        
        public void Rotate(Piece piece, int totalBoardColumns, int totalBoardRows)
        { 
            if (!CanRotate(piece, totalBoardColumns, totalBoardRows))
                return;

            var rows = piece.Shape.GetLength(0);
            var columns = piece.Shape.GetLength(1);

            var newShape = new int[columns, rows];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    newShape[j, rows - i - 1] = piece.Shape[i, j];
                }
            }

            piece.Shape = newShape;
        }

        // Método para mover la pieza hacia abajo
        public void MoveDown(Piece piece)
        {
            piece.Position = (piece.Position.x, piece.Position.y + 1);
        }

        // Método para mover la pieza a la izquierda
        public void MoveLeft(Piece piece)
        {
            piece.Position = (piece.Position.x - 1, piece.Position.y);
        }

        // Método para mover la pieza a la derecha
        public void MoveRight(Piece piece)
        {
            piece.Position = (piece.Position.x + 1, piece.Position.y);
        }

        #endregion

        public void OnGameStateChanged(EGameState state)
        {
            _currentState = state;
        }
    }
}