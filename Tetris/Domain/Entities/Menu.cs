using System.Collections.Generic;

namespace Domain.Entities
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