using System;

namespace MyGame.Factories
{
    public class MenuFactory
    {
        public Menu CreateMainMenu()
        {
            var menu = new Menu();
            menu.AddItem(new MenuItem("Play", StartGame));
            menu.AddItem(new MenuItem("Exit", ExitGame));
            return menu;
        }
        
        private static void StartGame()
        {
            Console.WriteLine("Starting game...");
        }

        private static void ExitGame()
        {
            Console.WriteLine("Exiting game...");
            Environment.Exit(0);
        }
    }
}