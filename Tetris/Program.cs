using System;
using System.Linq;
using MyGame.Configuration;
using MyGame.Enums;
using MyGame.Factories;
using MyGame.Inputs;
using MyGame.Interfaces;
using MyGame.Managers;
using MyGame.Services;
using Tao.Sdl;

namespace MyGame
{
    public static class Program
    {
        private static GlobalGameConfiguration _config;
        private static MenuFactory _menuFactory;
        private static IInputStrategy _inputMouse = new MouseInputStrategy();
        private static IInterfaceService _interfaceService = new GameInterfaceService();
        private static GameManager _gameManager = new GameManager();
        private static GameLogicService _gameLogicService;
        private static IInputStrategy _inputKeiboard;

        private static void Main(string[] args)
        {
            Engine.Initialize();
            var screen = Sdl.SDL_SetVideoMode(770, 720, 15, Sdl.SDL_SWSURFACE);

            _config = GlobalGameConfiguration.Instance;
            _config.Screen = screen;
            _config.GameGrid.InitializeBoard();
            
            _gameManager.ChangeState(EGameState.InMenu);
            
            _menuFactory = new MenuFactory(_gameManager);
            
            _gameLogicService = new GameLogicService(_config, _gameManager);
            
            _inputKeiboard = new KeyboardInputStrategy(_gameLogicService);

            (_config.CurrentPiece, _config.NextPiece) = _gameLogicService.GenerateRandomPieces();
            
            if (_config.Menu is null)
                _config.Menu = new Menu();

            while (_config.Running)
            {
                CheckInputs();
                Update();
                Render();
                Sdl.SDL_Delay(20); // Delay para reducir el consumo de CPU
            }

            Sdl.SDL_Quit();
        }

        private static void CheckInputs()
        {
            _inputMouse.CheckInputs(_config);
            _inputKeiboard.CheckInputs(_config);
        }

        private static void Update()
        {
            if (_gameManager.currentState != EGameState.InGame) return;
            _config.TimeCounter++;
            _gameLogicService.MovePieceAutomatically();
            _config.TimeCounter++;
        }

        private static void Render()
        {
            // Limpia la pantalla antes de dibujar cualquier elemento
            Engine.Clear();
            
            switch (_gameManager.currentState)
            {
                case EGameState.InMenu:
                    // Dibujar el menú principal
                    _config.Menu = _menuFactory.CreateMainMenu();
                    _interfaceService.DrawMenu(_config.Screen, _config.SelectedButtonInterface,
                        _config.Menu.OptionsMenu, _config.SelectedColor, _config.NormalColor, _config.MenuStartX,
                        _config.MenuStartY, _config.MenuOffsetY);
                    break;
                case EGameState.InGame:
                    _interfaceService.DrawBoard();
                    _interfaceService.DrawCurrentPiece(_config.CurrentPiece, _config.CellSize);
                    _interfaceService.DrawText("Next", _config.PositionInterfaceX, 5, _config.Font);
                    _interfaceService.DrawNextPiece(_config.NextPiece, _config.PositionInterfaceX, 30,
                        _config.CellSize);
                    _interfaceService.DrawText("Hold", _config.PositionInterfaceX, 155, _config.Font);
                    _interfaceService.DrawHeldPiece(_config.HeldPiece, _config.PositionInterfaceX, 180,
                        _config.CellSize);
                    _interfaceService.DrawText("Score", _config.PositionInterfaceX, 305, _config.Font);
                    _interfaceService.DrawText(_config.Score.ToString(), _config.PositionInterfaceX, 340, _config.Font);
                    break;
                case EGameState.InGameOver:
                    _config.Menu = _menuFactory.CreateGameOverMenu();
                    _interfaceService.DrawMenu(_config.Screen, _config.SelectedButtonInterface,
                        _config.Menu.OptionsMenu, _config.SelectedColor, _config.NormalColor, _config.MenuStartX,
                        _config.MenuStartY, _config.MenuOffsetY);
                    break;
                case EGameState.InCredits:
                    _config.Menu = _menuFactory.CreateCreditsMenu();
                    _interfaceService.DrawMenu(_config.Screen, _config.SelectedButtonInterface,
                        _config.Menu.OptionsMenu, _config.SelectedColor, _config.NormalColor, _config.MenuStartX,
                        _config.MenuStartY, _config.MenuOffsetY);
                    break;
                case EGameState.InControlgames:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Mostrar los cambios en la pantalla
            Engine.Show();
        }
    }
}