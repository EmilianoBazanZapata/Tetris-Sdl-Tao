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
            menu.AddItem(new MenuItem("Exit", ExitGame));
            return menu;
        }
        
        public Menu CreateCreditsMenu()
        {
            var menu = new Menu();
            menu.AddItem(new MenuItem("Back", GoBackToMainMenu));
            return menu;
        }
        
        public Menu CreateGameOverMenu()
        {
            var menu = new Menu();
            menu.AddItem(new MenuItem("Retry", RetryGame));
            menu.AddItem(new MenuItem("Exit", ExitGame));
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

        private static void ExitGame()
        {
            Environment.Exit(0);
        }
    }
}