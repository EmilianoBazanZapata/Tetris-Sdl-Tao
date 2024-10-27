using System;
using System.Collections.Generic;
using MyGame.Configuration;
using Tao.Sdl;

namespace MyGame
{
    public class Menu
    {
        public List<MenuItem> OptionsMenu = new List<MenuItem>();
        
        public void AddItem(MenuItem item)
        {
            OptionsMenu.Add(item);
        }
    }
}