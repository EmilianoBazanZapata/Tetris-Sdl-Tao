using System.Collections.Generic;

namespace MyGame.Menues
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