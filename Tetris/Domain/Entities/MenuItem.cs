using System;
using Domain.Core;

namespace Domain.Entities
{
    public class MenuItem
    {
        public string Text { get; }
        public Action Action { get; }
        public int PosX { get; }
        public int PosY { get; }
        
        public bool HasAction => Action != null;
        
        public MenuItem(string text, Action action, int posX, int posY)
        {
            Text = text;
            Action = action;
            PosX = posX;
            PosY = posY;
        }
    }
}