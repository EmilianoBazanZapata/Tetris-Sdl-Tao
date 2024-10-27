using System;
using MyGame.Enums;
using MyGame.Managers;

namespace MyGame.Factories
{
    public class MenuFactory
    {
        private GameManager _gameManager;
        
        public MenuFactory(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
        
        public Menu CreateMainMenu()
        {
            var menu = new Menu();
            menu.AddItem(new MenuItem("Play", StartGame));
            menu.AddItem(new MenuItem("Exit", ExitGame));
            return menu;
        }
        
        private void StartGame()
        {
            _gameManager.ChangeState(EGameState.InGame);
        }

        private static void ExitGame()
        {
            Environment.Exit(0);
        }
    }
}