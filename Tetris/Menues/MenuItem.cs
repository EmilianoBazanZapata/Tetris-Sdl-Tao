using System;

namespace MyGame.Menues
{
    public class MenuItem
    {
        public string Text { get; }
        public Action Action { get; }
        public Image Image { get; set; }
        
        public bool IsInteractive => Action != null;
        
        public MenuItem(string text, Action action, Image image = null)
        {
            Text = text;
            Action = action;
            Image = image;
        }
    }
}