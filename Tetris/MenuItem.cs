using System;

namespace MyGame
{
    public class MenuItem
    {
        public string Text { get; }
        public Action Action { get; }
        
        public MenuItem(string text, Action action)
        {
            Text = text;
            Action = action;
        }
    }
}