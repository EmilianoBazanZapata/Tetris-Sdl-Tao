using System;

namespace MyGame.Menues
{
    public class MenuItem
    {
        public string Text { get; }
        public Action Action { get; }
        public Image Image { get; set; }
        
        public MenuItem(string text, Action action, Image image = null)
        {
            Text = text;
            Action = action;
            Image = image;
        }
    }
}