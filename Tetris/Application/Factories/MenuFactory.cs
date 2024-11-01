using System;
using System.IO;
using Domain.Core;
using Domain.Entities;
using Domain.Enums;
using Domain.Managers;

namespace Application.Factories
{
    public class MenuFactory
    {
        private GameManager _gameManager;
        private readonly string _assetsPath;

        public MenuFactory(GameManager gameManager)
        {
            _gameManager = gameManager;
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.FullName;
            _assetsPath = Path.Combine(projectDirectory, "Infrastructure", "Assets", "Images");
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
            var imagePath = Path.Combine(_assetsPath, "Credits.png");
            var image = Engine.LoadImage(imagePath);
            var menu = new Menu();
            menu.AddItem(new MenuItem("Back", GoBackToMainMenu, image));
            return menu;
        }

        public Menu CreateGameOverMenu()
        {
            var imagePath = Path.Combine(_assetsPath, "LoseGame.png");
            var image = Engine.LoadImage(imagePath);

            var menu = new Menu();
            menu.AddItem(new MenuItem("Retry", RetryGame, image));
            return menu;
        }

        public Menu CreateControlsMenu()
        {
            var imagePath = Path.Combine(_assetsPath, "Controls.png");
            var image = Engine.LoadImage(imagePath);
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
