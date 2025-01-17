using System;
using Application.Configurations;
using Application.Factories;
using Application.Managers;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using MyGame.Enums;

namespace Application.Services
{
    public class GameLogicService : IGameLogicService
    {
        private readonly GlobalGameConfiguration _config;
        private readonly GameManager _gameManager;

        public GameLogicService(GlobalGameConfiguration config, 
                                GameManager gameManager)
        {
            _config = config;
            _gameManager = gameManager;
        }

        public (Piece currentPiece, Piece nextPiece) GenerateRandomPieces()
        {
            var random = new Random();

            // Generar la pieza actual
            var currentPieceType = (EPieceType)random.Next(1, 7); // Genera un número entre 1 y 7 para la pieza actual
            var currentPiece = PieceFactory.CreatePiece(currentPieceType);

            // Verificar si la posición inicial está libre
            if (!IsPositionValid(_config.StartPosition))
            {
                _gameManager.ChangeState(EGameState.InGameOver);
                ResetGame();
            }

            currentPiece.Position = _config.StartPosition; // Posicionar la nueva pieza en la parte superior del tablero

            // Generar la siguiente pieza
            var nextPieceType = (EPieceType)random.Next(1, 7); // Genera un número entre 1 y 7 para la siguiente pieza
            var nextPiece = PieceFactory.CreatePiece(nextPieceType);
            nextPiece.Position = _config.StartPosition; // La siguiente pieza también está en su posición inicial

            return (currentPiece, nextPiece); // Retornar ambas piezas
        }

        public void HoldPiece()
        {
            if (_config.HeldPiece == null)
            {
                _config.HeldPiece = _config.CurrentPiece;
                _config.HeldPiece.Position = _config.StartPosition;
                _config.CurrentPiece = GenerateRandomPieces().currentPiece;
            }
            else
            {
                // Si ya hay una pieza guardada, intercambiarla con la pieza actual
                var temp = _config.CurrentPiece;
                _config.CurrentPiece = _config.HeldPiece;
                _config.CurrentPiece.Position = _config.StartPosition;
                _config.HeldPiece = temp; // Intercambiar las piezas
            }
        }

        public void ClearCompleteRows()
        {
            var completedRowCount = 0;

            // Iterar desde la última fila hasta la primera
            for (int i = _config.Rows - 1; i >= 0; i--)
            {
                var completeRow = true;

                // Verificar si la fila está completamente ocupada
                for (int j = 0; j < _config.Columns; j++)
                {
                    if (_config.Board[i, j] != 0) continue;
                    completeRow = false;
                    break;
                }

                // Si la fila está completa, desplazar las filas superiores hacia abajo
                if (!completeRow) continue;
                {
                    completedRowCount++;
                    // Mover todas las filas superiores hacia abajo
                    for (int k = i; k > 0; k--)
                    {
                        for (int j = 0; j < _config.Columns; j++)
                        {
                            _config.Board[k, j] = _config.Board[k - 1, j];
                        }
                    }

                    // Vaciar la primera fila
                    for (int j = 0; j < _config.Columns; j++)
                    {
                        _config.Board[0, j] = 0;
                    }

                    // Ajustar el índice `i` para volver a verificar la fila actual,
                    // ya que ahora contiene la fila desplazada hacia abajo.
                    i++;
                }
            }

            // Calcular el puntaje basado en la cantidad de filas completadas
            _config.Score += CalculateScore(completedRowCount);
            
            if(_config.Score >= _config.ConditionCoreGame)
                _gameManager.ChangeState(EGameState.WinGame);
        }

        private void ResetGame()
        {
            _config.Score = 0;
            _config.Score = 0;
            
            for (int i = 0; i < _config.Rows; i++)
            {
                for (int j = 0; j < _config.Columns; j++)
                {
                    _config.Board[i, j] = 0;
                }
            }
        }
        
        public void FixPieceOnBoard(Piece piece, int totalBoardColumns, int totalBoardRows, int[,] board)
        {
            var rows = piece.Shape.GetLength(0);
            var columns = piece.Shape.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (piece.Shape[i, j] == 0) continue;
                    var x = piece.Position.x + j;
                    var y = piece.Position.y + i;

                    // Asegurar que la pieza no se fije fuera de los límites del tablero
                    if (x >= 0 && x < totalBoardColumns && y >= 0 && y < totalBoardRows)
                    {
                        board[y, x] = piece.Shape[i, j]; // Marcar la celda como ocupada
                    }
                }
            }
        }

        private int CalculateScore(int completedRows)
        {
            switch (completedRows)
            {
                case 1: return 100; // 1 row
                case 2: return 300; // 2 rows
                case 3: return 500; // 3 rows
                case 4: return 800; // 4 rows
                default: return 0;
            }
        }
        
        /// <summary>
        /// Metodo para saber si al momento de crear una pieza el jugador a perdido o no
        /// </summary>
        /// <param name="startPosition"></param>
        /// <returns></returns>
        private bool IsPositionValid((int x, int y) startPosition)
        {
            // Verificar si las coordenadas están dentro del tablero y si la celda está vacía
            return startPosition.x >= 0 && startPosition.x < _config.Columns && startPosition.y >= 0 && startPosition.y < _config.Rows &&
                   _config.Board[startPosition.y, startPosition.x] == 0;
            // Si alguna celda está ocupada o fuera de los límites, la posición no es válida
            // Si todas las celdas están libres, la posición es válida
        }
    }
}