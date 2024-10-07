using System;
using Tao.Sdl;

namespace MyGame
{
    internal static class Program
    {
        static Image image = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\Block-O.png");

        static int columnas = 5;
        static int filas = 10;
        static int tamañoCelda = 60; // Tamaño de cada celda en píxeles

        static int[,] tablero = new int[filas, columnas]; // Matriz del tablero
        
        static IntPtr pantalla; // Representación de la ventana principal

        private static void Main(string[] args)
        {
            Engine.Initialize();
            pantalla = Sdl.SDL_SetVideoMode(300, 600, 32, Sdl.SDL_SWSURFACE); // Definir la ventana

            InicializarTablero();

            Sdl.SDL_Event evento;
            var running = true;

            while (running)
            {
                while (Sdl.SDL_PollEvent(out evento) != 0)
                {
                    if (evento.type == Sdl.SDL_QUIT)
                    {
                        running = false;
                    }
                }

                CheckInputs();
                Update();
                Render();
                Sdl.SDL_Delay(20); // Delay para reducir el consumo de CPU
            }

            Sdl.SDL_Quit();
        }

        private static void InicializarTablero()
        {
            for (var i = 0; i < filas; i++)
            {
                for (var j = 0; j < columnas; j++)
                {
                    tablero[i, j] = 0; // Celdas vacías
                }
            }
        }

        private static void CheckInputs()
        {
            // Aquí se manejara la entrada del usuario (Teclado)
        }

        private static void Update()
        {
            // Lógica para actualizar el estado del juego
        }

        private static void Render()
        {
            // Fondo negro
            DrawRect(0, 0, columnas * tamañoCelda, filas * tamañoCelda, 0, 0, 0);  // Fondo negro

            // Dibujamos el tablero
            for (var i = 0; i < filas; i++)
            {
                for (var j = 0; j < columnas; j++)
                {
                    // Dibujar el borde de la celda (separador entre celdas)
                    DrawRect(j * tamañoCelda, i * tamañoCelda, tamañoCelda, tamañoCelda, 0, 0, 0);  // Borde negro

                    switch (tablero[i, j])
                    {
                        case 0:
                            // Dibujar la celda vacía (color azul)
                            DrawRect(j * tamañoCelda + 1, i * tamañoCelda + 1, tamañoCelda - 2, tamañoCelda - 2, 0, 0, 255);
                            break;
                        case 1:
                            // Dibujar la celda ocupada (color rojo)
                            DrawRect(j * tamañoCelda + 1, i * tamañoCelda + 1, tamañoCelda - 2, tamañoCelda - 2, 255, 0, 0);
                            break;
                    }
                }
            }

            // Mostrar los cambios en pantalla
            Engine.Show();
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
            return (uint)((r << 16) | (g << 8) | b);  // Combina los valores RGB en un solo uint
        }
    }
}