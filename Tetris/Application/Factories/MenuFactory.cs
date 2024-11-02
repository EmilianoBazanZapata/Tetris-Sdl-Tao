using System;
using Application.Managers;
using Domain.Entities;
using Domain.Enums;

namespace Application.Factories
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
            menu.AddItem(new MenuItem("Play", StartGame,315,225));
            menu.AddItem(new MenuItem("Credits", CreditsGame,315,275));
            menu.AddItem(new MenuItem("Controls", ControlsGame, 315, 325)); 
            menu.AddItem(new MenuItem("Exit", ExitGame,315,375));
            return menu;
        }

        public Menu CreateCreditsMenu()
        {
            var menu = new Menu();
            menu.AddItem(new MenuItem("Game developed by", null, 180, 250));
            menu.AddItem(new MenuItem("Emiliano Bazán-Zapata", null, 135, 300));
            menu.AddItem(new MenuItem("Back", GoBackToMainMenu,315,550 ));
            return menu;
        }

        public Menu CreateGameOverMenu()
        {
            var menu = new Menu();
            menu.AddItem(new MenuItem("The game is over, you lost ...", null, 25, 250));
            menu.AddItem(new MenuItem("Retry", RetryGame,315,550 ));
            return menu;
        }

        public Menu CreateControlsMenu()
        {
            var menu = new Menu();
            
            menu.AddItem(new MenuItem("  > Press the A key to", null, 0, 50));
            menu.AddItem(new MenuItem("move the piece to the left", null, 95, 100));
            
            menu.AddItem(new MenuItem("  > Press the D key to", null, 0, 150));
            menu.AddItem(new MenuItem("move the piece to the right", null, 95, 200));
            
            menu.AddItem(new MenuItem("  > Press the R key to", null, 0, 250));
            menu.AddItem(new MenuItem("rotate the piece", null, 95, 300));
            
            menu.AddItem(new MenuItem("  > Press the S key to", null, 0, 350));
            menu.AddItem(new MenuItem("make the piece fall faster", null, 95, 400));
            
            menu.AddItem(new MenuItem("  > Press the Q key to", null, 0, 450));
            menu.AddItem(new MenuItem("hold the current piece", null, 95, 500));

            menu.AddItem(new MenuItem("Back", GoBackToMainMenu,315,550 ));
            return menu;
        }

        public Menu CreateWinGameMenu()
        {
            var menu = new Menu();
            menu.AddItem(new MenuItem("Back to Menu", GoBackToMainMenu,315,550 ));
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