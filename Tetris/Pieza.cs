using System;
using MyGame.Enums;

namespace MyGame
{
    public class Pieza
    {
        public int[,] Forma { get; set; }
        public (int x, int y) Posicion { get; set; }
        private Image Imagen { get; set; }

        private Pieza(int[,] forma, Image imagen)
        {
            Forma = forma;
            Posicion = (0, 0);
            Imagen = imagen;
        }

        public Pieza()
        {
        }

        public static Pieza CrearPieza(TipoPieza tipo)
        {
            switch (tipo)
            {
                case TipoPieza.I:
                    return CrearPiezaI();
                case TipoPieza.O:
                    return CrearPiezaO();
                case TipoPieza.T:
                    return CrearPiezaT();
                case TipoPieza.L:
                    return CrearPiezaL();
                case TipoPieza.J:
                    return CrearPiezaJ();
                case TipoPieza.S:
                    return CrearPiezaS();
                case TipoPieza.Z:
                    return CrearPiezaZ();
                default:
                    throw new ArgumentOutOfRangeException(nameof(tipo), "Pieza desconocida.");
            }
        }

        public void DibujarPieza(int celda)
        {
            var filas = Forma.GetLength(0);
            var columnas = Forma.GetLength(1);

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    if (Forma[i, j] == 0) continue;

                    // Dibujar la imagen de la pieza en la posición de la celda ocupada
                    Engine.Draw(Imagen.Pointer, (Posicion.x + j) * celda,
                        (Posicion.y + i) * celda); // Dibujar la imagen
                }
            }
        }

        #region Fijar Pieza

        public static void FijarPiezaEnTablero(Pieza pieza, int columnasTotalesTablero, int filasTotalesTablero,
            int[,] tablero)
        {
            var filas = pieza.Forma.GetLength(0);
            var columnas = pieza.Forma.GetLength(1);

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    if (pieza.Forma[i, j] == 0) continue;
                    var x = pieza.Posicion.x + j;
                    var y = pieza.Posicion.y + i;

                    // Asegurar que la pieza no se fije fuera de los límites del tablero
                    if (x >= 0 && x < columnasTotalesTablero && y >= 0 && y < filasTotalesTablero)
                    {
                        tablero[y, x] = pieza.Forma[i, j]; // Marcar la celda como ocupada
                    }
                }
            }
        }

        #endregion

        #region Rotar Pieza

        public void Rotar(int columnasTotalesTablero, int filasTotalesTablero)
        {
            if (!PuedeRotar(columnasTotalesTablero, filasTotalesTablero))
                return;

            var filas = Forma.GetLength(0);
            var columnas = Forma.GetLength(1);

            // Realizar la rotación de la pieza (90 grados)
            var nuevaForma = new int[columnas, filas];
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    nuevaForma[j, filas - i - 1] = Forma[i, j];
                }
            }

            Forma = nuevaForma; // Actualizar la forma de la pieza con la nueva forma rotada
        }


        private bool PuedeRotar(int columnasTotalesTablero, int filasTotalesTablero)
        {
            var filas = Forma.GetLength(0);
            var columnas = Forma.GetLength(1);

            // Verificar las nuevas posiciones directamente sin crear una nueva matriz
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    if (Forma[i, j] != 1) continue;

                    // Calcular las nuevas posiciones como si la pieza estuviera rotada
                    var nuevoX = Posicion.x + (filas - i - 1);
                    var nuevoY = Posicion.y + j;

                    // Verificar si las nuevas posiciones se salen del tablero
                    if (nuevoX < 0 || nuevoX >= columnasTotalesTablero || nuevoY < 0 || nuevoY >= filasTotalesTablero)
                        return false;
                }
            }

            return true;
        }

        #endregion

        #region Mover Pieza

        // Método para mover la pieza hacia abajo
        public void MoverAbajo()
        {
            Posicion = (Posicion.x, Posicion.y + 1);
        }

        // Método para mover la pieza a la izquierda
        public void MoverIzquierda()
        {
            Posicion = (Posicion.x - 1, Posicion.y);
        }

        // Método para mover la pieza a la derecha
        public void MoverDerecha()
        {
            Posicion = (Posicion.x + 1, Posicion.y);
        }

        #endregion

        #region Piezas del Juego

        private static Pieza CrearPiezaI()
        {
            return new Pieza(new int[,] { { 1, 1, 1, 1 } },
                Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileCyan.png"));
        }

        private static Pieza CrearPiezaO()
        {
            return new Pieza(new int[,] { { 2, 2 }, { 2, 2 } },
                Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileYellow.png"));
        }

        private static Pieza CrearPiezaT()
        {
            return new Pieza(new int[,] { { 0, 3, 0 }, { 3, 3, 3 } },
                Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TilePurple.png"));
        }

        private static Pieza CrearPiezaL()
        {
            return new Pieza(new int[,] { { 4, 0, 0 }, { 4, 4, 4 } },
                Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileOrange.png"));
        }

        private static Pieza CrearPiezaJ()
        {
            return new Pieza(new int[,]
            {
                { 0, 0, 5 },
                { 5, 5, 5 }
            }, Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileBlue.png"));
        }

        private static Pieza CrearPiezaS()
        {
            return new Pieza(new int[,]
            {
                { 0, 6, 6 },
                { 6, 6, 0 }
            }, Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TilePurple.png"));
        }

        private static Pieza CrearPiezaZ()
        {
            return new Pieza(new int[,]
            {
                { 7, 7, 0 },
                { 0, 7, 7 }
            }, Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileRed.png"));
        }

        #endregion
    }
}