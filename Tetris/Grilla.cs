using Tao.Sdl;

namespace MyGame
{
    public class Grilla
    {
        private int[,] Tablero { get; set; }
        private int Columnas { get; set; }
        private int Filas { get; set; }
        private int Celdas { get; set; }

        public Grilla(int[,] tablero, int columnas, int filas, int celdas)
        {
            Tablero = tablero;
            Columnas = columnas;
            Filas = filas;
            Celdas = celdas;
        }

        public void InicializarTablero()
        {
            for (var i = 0; i < Filas; i++)
            {
                for (var j = 0; j < Columnas; j++)
                {
                    Tablero[i, j] = 0; // Celdas vacías
                }
            }
        }

        public void DibujarTablero()
        {
            // Fondo negro
            DrawRect(0, 0, Columnas * Celdas, Filas * Celdas, 0, 0, 0); // Fondo negro
            
            // Dibujamos el tablero
            for (var i = 0; i < Filas; i++)
            {
                for (var j = 0; j < Columnas; j++)
                {
                    // Dibujar el borde de la celda (separador entre celdas)
                    DrawRect(j * Celdas, i * Celdas, Celdas, Celdas, 0, 0, 0); // Borde negro

                    switch (Tablero[i, j])
                    {
                        case 0:
                            // Dibujar la celda vacía (color azul)
                            DrawRect(j * Celdas + 1, i * Celdas + 1, Celdas - 2, Celdas - 2, 0, 0,
                                255);
                            break;
                        case 1:
                            // Dibujar la celda ocupada (color rojo)
                            DrawRect(j * Celdas + 1, i * Celdas + 1, Celdas - 2, Celdas - 2, 255, 0,
                                0);
                            break;
                    }
                }
            }
        }
        
        public void LimpiarFilasCompletas()
        {
            for (int i = 40 - 1; i >= 0; i--) // Empezar desde abajo
            {
                bool filaCompleta = true;

                // Verificar si la fila está completamente ocupada
                for (int j = 0; j < 20; j++)
                {
                    if (Tablero[i, j] == 0) // Si encontramos una celda vacía
                    {
                        filaCompleta = false;
                        break;
                    }
                }

                // Si la fila está completa, la limpiamos
                if (filaCompleta)
                {
                    EliminarFila(i);
                }
            }
        }
        
        private void EliminarFila(int fila)
        {
            // Mover todas las filas superiores una fila hacia abajo
            for (int i = fila; i > 0; i--)
            {
                for (int j = 0; j < 20; j++)
                {
                    Tablero[i, j] = Tablero[i - 1, j]; // La fila actual toma el valor de la fila superior
                }
            }

            // Limpiar la fila superior (ahora vacía)
            for (int j = 0; j < 20; j++)
            {
                Tablero[0, j] = 0; // La fila superior ahora queda vacía
            }
        }
        
        // Método para dibujar un rectángulo usando SDL
        private static void DrawRect(int x, int y, int width, int height, byte r, byte g, byte b)
        {
            var rect = new Sdl.SDL_Rect
            {
                x = (short)x,
                y = (short)y,
                w = (short)width,
                h = (short)height
            };

            var surface = Sdl.SDL_GetVideoSurface();
            var color = MapRgb(r, g, b);
            var intColor = unchecked((int)color);
            Sdl.SDL_FillRect(surface, ref rect, intColor);
        }
        
        // Método para calcular el color en formato RGB888
        private static uint MapRgb(byte r, byte g, byte b)
        {
            return (uint)((r << 16) | (g << 8) | b); // Combina los valores RGB en un solo uint
        }

    }
}