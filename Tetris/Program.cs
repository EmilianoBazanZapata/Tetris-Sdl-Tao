using System;
using MyGame.Configuration;
using MyGame.Enums;
using MyGame.Factories;
using MyGame.Inputs;
using MyGame.Interfaces;
using MyGame.Managers;
using MyGame.Observers;
using MyGame.Services;
using Tao.Sdl;

namespace MyGame
{
    public static class Program
    {
        private static GlobalGameConfiguration config;
        private static IInputStrategy inputKeiboard = new KeyboardInputStrategy();
        private static IInputStrategy inputMouse = new MouseInputStrategy();
        private static IInterfaceService _interfaceService = new GameInterfaceService();
        
        private static GameManager gameManager = new GameManager();
        private static GameObserver consoleObserver = new GameObserver();
        
        private static MenuFactory MenuFactory = new MenuFactory(gameManager);

        static Sdl.SDL_Event sdlEvent;

        private static void Main(string[] args)
        {
            gameManager.Subscribe(consoleObserver);
            
            gameManager.ChangeState(EGameState.InMenu);
            
            Engine.Initialize();
            var screen = Sdl.SDL_SetVideoMode(1280, 720, 15, Sdl.SDL_SWSURFACE);

            config = GlobalGameConfiguration.Instance;

            config.Screen = screen;

            config.GameGrid.InitializeBoard();

            (config.CurrentPiece, config.NextPiece) = GameLogicService.GenerateRandomPieces(config);

            config.Menu = MenuFactory.CreateMainMenu();
            config.Menu.Display(config.Screen, config.SelectedButtonInterface);

            config.GameGrid.InitializeBoard();
            (config.CurrentPiece, config.NextPiece) = GameLogicService.GenerateRandomPieces(config);

            while (config.Running)
            {
                CheckInputs();
                Update();
                //Render();
                Sdl.SDL_Delay(20); // Delay para reducir el consumo de CPU
            }

            Sdl.SDL_Quit();
        }


        private static void CheckInputs()
        {
            inputMouse.CheckInputs(config, sdlEvent);
            inputKeiboard.CheckInputs(config, sdlEvent);
        }

        private static void Update()
        {
            config.TimeCounter++;

            GameLogicService.MovePieceAutomatically(config);

            config.TimeCounter++;
        }


        private static void Render()
        {
            Engine.Clear();
            _interfaceService.DrawBoard(config.GameGrid);
            _interfaceService.DrawCurrentPiece(config.CurrentPiece, config.CellSize);
            _interfaceService.DrawText("Next", config.PositionInterfaceX, 5, config.Font);
            _interfaceService.DrawNextPiece(config.NextPiece, config.PositionInterfaceX, 30, config.CellSize);
            _interfaceService.DrawText("Hold", config.PositionInterfaceX, 155, config.Font);
            _interfaceService.DrawHeldPiece(config.HeldPiece, config.PositionInterfaceX, 180, config.CellSize);
            _interfaceService.DrawText("Score", config.PositionInterfaceX, 305, config.Font);
            _interfaceService.DrawText(config.Score.ToString(), config.PositionInterfaceX, 340, config.Font);
            Engine.Show();
        }
    }
}