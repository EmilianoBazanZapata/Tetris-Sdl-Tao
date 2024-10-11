using System;

namespace MyGame
{
    public class Pieza
    {
        public int[,] Forma { get; private set; }
        public (int x, int y) Posicion { get; set; }

        public Pieza(int[,] forma)
        {
            Forma = forma;
            Posicion = (0, 0);
        }

        public Pieza()
        {
                
        }
        
        public bool PuedeRotar(int columnasTotalesTablero, int filasTotalesTablero)
        {
            var filas = Forma.GetLength(0);
            var columnas = Forma.GetLength(1);

            // Crear una matriz para la nueva forma rotada
            var nuevaForma = new int[columnas, filas];

            // Realizar la rotación de la pieza (90 grados)
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    nuevaForma[j, filas - i - 1] = Forma[i, j];
                }
            }

            // Verificar si la nueva forma cabe dentro del tablero
            for (int i = 0; i < nuevaForma.GetLength(0); i++)
            {
                for (int j = 0; j < nuevaForma.GetLength(1); j++)
                {
                    if (nuevaForma[i, j] != 1) continue; // Solo verificamos las celdas ocupadas por la pieza
                    var x = Posicion.x + j;
                    var y = Posicion.y + i;

                    // Verificar si se sale del tablero
                    if (x < 0 || x >= columnasTotalesTablero || y < 0 || y >= filasTotalesTablero)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        
        public void Rotar(int columnasTotalesTablero, int filasTotalesTablero)
        {
            if (PuedeRotar(columnasTotalesTablero, filasTotalesTablero)) // Solo rotar si la rotación es válida
            {
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
                Console.WriteLine("Pieza rotada correctamente.");
            }
            else
            {
                Console.WriteLine("No se puede rotar: La pieza se saldría del tablero.");
            }
        }

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

        // Definimos todas las piezas clásicas del Tetris
        public static class PiezasClasicas
        {
            public static Pieza CrearPiezaI()
            {
                return new Pieza(new int[,]
                {
                    { 1, 1, 1, 1 }
                });
            }

            public static Pieza CrearPiezaO()
            {
                return new Pieza(new int[,]
                {
                    { 1, 1 },
                    { 1, 1 }
                });
            }

            public static Pieza CrearPiezaT()
            {
                return new Pieza(new int[,]
                {
                    { 0, 1, 0 },
                    { 1, 1, 1 }
                });
            }

            public static Pieza CrearPiezaL()
            {
                return new Pieza(new int[,]
                {
                    { 1, 0, 0 },
                    { 1, 1, 1 }
                });
            }

            public static Pieza CrearPiezaJ()
            {
                return new Pieza(new int[,]
                {
                    { 0, 0, 1 },
                    { 1, 1, 1 }
                });
            }

            public static Pieza CrearPiezaS()
            {
                return new Pieza(new int[,]
                {
                    { 0, 1, 1 },
                    { 1, 1, 0 }
                });
            }

            public static Pieza CrearPiezaZ()
            {
                return new Pieza(new int[,]
                {
                    { 1, 1, 0 },
                    { 0, 1, 1 }
                });
            }
        }
    }
}
