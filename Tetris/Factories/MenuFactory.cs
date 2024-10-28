using System;
using MyGame.Enums;
using MyGame.Managers;
using MyGame.Menues;

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
            menu.AddItem(new MenuItem("Credits", CreditsGame));
            menu.AddItem(new MenuItem("Controls", ControlsGame));
            menu.AddItem(new MenuItem("Exit", ExitGame));
            return menu;
        }

        public Menu CreateCreditsMenu()
        {
            var image = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\Credits.png");
            var menu = new Menu();
            menu.AddItem(new MenuItem("Back", GoBackToMainMenu, image));
            return menu;
        }
        
        public Menu CreateGameOverMenu()
        {
            var menu = new Menu();
            menu.AddItem(new MenuItem("Retry", RetryGame));
            menu.AddItem(new MenuItem("Exit", ExitGame));
            return menu;
        }

        public Menu CreateControlsMenu()
        {
            var image = Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\Controls.png");
            var menu = new Menu();
            menu.AddItem(new MenuItem("Back", GoBackToMainMenu, image));
            return menu;
        }

        private void StartGame()
        {
            _gameManager.ChangeState(EGameState.InGame);
        }
        
        private void GoBackToMainMenu()
        {
            _gameManager.ChangeState(EGameState.InMenu);
        }
        
        private void RetryGame()
        {
            _gameManager.ChangeState(EGameState.InGame);
        }
        
        private void CreditsGame()
        {
            _gameManager.ChangeState(EGameState.InCredits);
        }

        private void ControlsGame()
        {
            _gameManager.ChangeState(EGameState.InControlgames);
        }
        
        private static void ExitGame()
        {
            Environment.Exit(0);
        }
    }
}