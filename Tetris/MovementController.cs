namespace MyGame
{
    public class MovementController
    {
        private int[,] Tablero { get; set; }

        public MovementController(int[,] tablero)
        {
            Tablero = tablero;
        }

        #region Verificar Movimiento

        public bool PuedeMoverAbajo(Pieza pieza, int filasDelTablero)
        {
            var filas = pieza.Forma.GetLength(0);
            var columnas = pieza.Forma.GetLength(1);

            // Verificar si la pieza ha alcanzado el fondo del tablero
            if (pieza.Posicion.y + filas >= filasDelTablero)
                return false; // La pieza ha alcanzado el límite inferior del tablero

            // Verificar si hay una pieza abajo
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    if (pieza.Forma[i, j] == 0) continue; // Solo chequeamos las celdas ocupadas por la pieza
                    // Verificar si la celda debajo está ocupada
                    if (Tablero[(pieza.Posicion.y + i) + 1, (pieza.Posicion.x + j)] !=
                        0) // +1 en y para mirar la celda de abajo
                        return false; // Hay una colisión con otra pieza
                }
            }

            return true; // Si no hay colisión y no alcanzó el límite, la pieza puede moverse
        }

        // Verificar si la pieza puede moverse a la izquierda
        public bool PuedeMoverIzquierda(Pieza pieza)
        {
            var filas = pieza.Forma.GetLength(0);
            var columnas = pieza.Forma.GetLength(1);

            // Verificar los límites del tablero
            if (pieza.Posicion.x <= 0)
                return false; // La pieza está en el borde izquierdo

            // Verificar si hay una pieza a la izquierda
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    if (pieza.Forma[i, j] == 0) continue; // Solo chequeamos las celdas ocupadas por la pieza

                    // Verificar si la celda a la izquierda está ocupada
                    if (Tablero[(pieza.Posicion.y + i), (pieza.Posicion.x + j) - 1] != 0)
                        return false; // Hay una pieza a la izquierda
                }
            }

            return true; // Puede moverse a la izquierda
        }

        // Verificar si la pieza puede moverse a la derecha
        public bool PuedeMoverDerecha(Pieza pieza)
        {
            var filas = pieza.Forma.GetLength(0);
            var columnas = pieza.Forma.GetLength(1);

            // Verificar los límites del tablero
            if (pieza.Posicion.x + columnas >= 20)
                return false; // La pieza está en el borde derecho

            // Verificar si hay una pieza a la derecha
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    if (pieza.Forma[i, j] == 0) continue; // Solo chequeamos las celdas ocupadas por la pieza
                    // Verificar si la celda a la derecha está ocupada
                    if (Tablero[(pieza.Posicion.y + i), (pieza.Posicion.x + j) + 1] != 0)
                        return false; // Hay una pieza a la derecha
                }
            }

            return true; // Puede moverse a la derecha
        }

        #endregion
    }
}