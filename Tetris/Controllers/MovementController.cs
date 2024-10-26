using MyGame.Configuration;
using MyGame.Interfaces;

namespace MyGame.Controllers
{
    public class MovementController
    {
        private int[,] Board { get; set; }

        public MovementController(int[,] board)
        {
            Board = board;
        }

        #region Verificar Movimiento

        public bool CanMoveDown(IPiece piece, int boardRows)
        {
            var rows = piece.Shape.GetLength(0);
            var columns = piece.Shape.GetLength(1);

            // Verificar si la pieza ha alcanzado el fondo del tablero
            if (piece.Position.y + rows >= boardRows)
                return false; // La pieza ha alcanzado el límite inferior del tablero

            // Verificar si hay una pieza abajo
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (piece.Shape[i, j] == 0) continue; // Solo chequeamos las celdas ocupadas por la pieza
                    // Verificar si la celda debajo está ocupada
                    if (Board[(piece.Position.y + i) + 1, (piece.Position.x + j)] != 0) // +1 en y para mirar la celda de abajo
                        return false; // Hay una colisión con otra pieza
                }
            }

            return true; // Si no hay colisión y no alcanzó el límite, la pieza puede moverse
        }

        // Verificar si la pieza puede moverse a la izquierda
        public bool CanMoveLeft(IPiece piece)
        {
            var rows = piece.Shape.GetLength(0);
            var columns = piece.Shape.GetLength(1);

            // Verificar los límites del tablero
            if (piece.Position.x <= 0)
                return false; // La pieza está en el borde izquierdo

            // Verificar si hay una pieza a la izquierda
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (piece.Shape[i, j] == 0) continue; // Solo chequeamos las celdas ocupadas por la pieza

                    // Verificar si la celda a la izquierda está ocupada
                    if (Board[(piece.Position.y + i), (piece.Position.x + j) - 1] != 0)
                        return false; // Hay una pieza a la izquierda
                }
            }

            return true; // Puede moverse a la izquierda
        }

        // Verificar si la pieza puede moverse a la derecha
        public bool CanMoveRight(IPiece piece, GlobalGameConfiguration config)
        {
            var rows = piece.Shape.GetLength(0);
            var columns = piece.Shape.GetLength(1);

            // Verificar los límites del tablero
            if (piece.Position.x + columns >= config.Columns)
                return false; // La pieza está en el borde derecho

            // Verificar si hay una pieza a la derecha
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (piece.Shape[i, j] == 0) continue; // Solo chequeamos las celdas ocupadas por la pieza
                    // Verificar si la celda a la derecha está ocupada
                    if (Board[(piece.Position.y + i), (piece.Position.x + j) + 1] != 0)
                        return false; // Hay una pieza a la derecha
                }
            }

            return true; // Puede moverse a la derecha
        }

        #endregion
    }
}