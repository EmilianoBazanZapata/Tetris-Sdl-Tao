using System.Collections.Generic;
using Tao.Sdl;

namespace MyGame
{
    public class Grilla
    {
        private int[,] Tablero { get; set; }
        private int Columnas { get; set; }
        private int Filas { get; set; }
        private int Celdas { get; set; }
        private Image ImagenCeldaEnBlanco { get; set; }
        private Dictionary<int, Image> ImagenesPiezas { get; set; }

        public Grilla(int[,] tablero, int columnas, int filas, int celdas, Image imagenCeldaEnBlanco,
            Dictionary<int, Image> imagenesPiezas)
        {
            Tablero = tablero;
            Columnas = columnas;
            Filas = filas;
            Celdas = celdas;
            ImagenCeldaEnBlanco = imagenCeldaEnBlanco;
            ImagenesPiezas = imagenesPiezas;
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
            int desplazamientoX = 90; // Ajusta este valor según lo que necesites

            for (var i = 0; i < Filas; i++)
            {
                for (var j = 0; j < Columnas; j++)
                {
                    var tipoPieza = Tablero[i, j];

                    if (tipoPieza == 0)
                    {
                        Engine.Draw(ImagenCeldaEnBlanco.Pointer, j * Celdas + desplazamientoX, i * Celdas);
                    }
                    else
                    {
                        Engine.Draw(ImagenesPiezas[tipoPieza].Pointer, j * Celdas + desplazamientoX, i * Celdas);
                    }
                }
            }
        }


        public void LimpiarFilasCompletas()
        {
            for (int i = Filas - 1; i >= 0; i--) // Empezar desde abajo
            {
                bool filaCompleta = true;

                // Verificar si la fila está completamente ocupada
                for (int j = 0; j < Columnas; j++)
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
        
        // Método para calcular el color en formato RGB888
        private static uint MapRgb(byte r, byte g, byte b)
        {
            return (uint)((r << 16) | (g << 8) | b); // Combina los valores RGB en un solo uint
        }
    }
}