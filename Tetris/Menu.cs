using System;
using System.Collections.Generic;
using MyGame.Configuration;
using Tao.Sdl;

namespace MyGame
{
    public class Menu
    {
        public List<MenuItem> items = new List<MenuItem>();
        private static Sdl.SDL_Color selectedColor = new Sdl.SDL_Color(255, 0, 0); // Color rojo
        private static Sdl.SDL_Color normalColor = new Sdl.SDL_Color(255, 255, 255); // Color blanco

        private int startX = 0;
        private int startY = 0;
        private int offsetY = 50; // Espacio entre las opciones

        public void AddItem(MenuItem item)
        {
            items.Add(item);
        }

        public void Display(IntPtr screen, int configSelectedButtonInterface)
        {
            for (int i = 0; i < items.Count; i++)
            {
                var color = (i == configSelectedButtonInterface) ? selectedColor : normalColor;
                var optionText = items[i].Text;

                var surface = SdlTtf.TTF_RenderText_Solid(GlobalGameConfiguration.Instance.Font, optionText, color);
                if (surface == IntPtr.Zero)
                {
                    Console.WriteLine("Error al renderizar texto: " + SdlTtf.TTF_GetError());
                    continue;
                }

                // Inicializa srcRect a un valor conocido antes de usar
                var srcRect = new Sdl.SDL_Rect { x = 0, y = 0, w = 0, h = 0 };
                Sdl.SDL_GetClipRect(surface, ref srcRect);  // Usando 'ref'

                var destRect = new Sdl.SDL_Rect
                {
                    x = (short)startX,
                    y = (short)(startY + i * offsetY),
                    w = srcRect.w,
                    h = srcRect.h
                };

                Sdl.SDL_BlitSurface(surface, ref srcRect, screen, ref destRect);
                Sdl.SDL_FreeSurface(surface);
            }

            Sdl.SDL_UpdateRect(screen, 0, 0, 0, 0); // Actualiza la pantalla completa
        }
    }
}