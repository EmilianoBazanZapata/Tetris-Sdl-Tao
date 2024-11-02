using System;
using Application.Configurations;
using Application.Factories;
using Application.Strategies;
using Domain.Core;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Managers
{
    public class RenderManager : IGameStateObserver
    {
        // Singleton instance
        private static RenderManager _instance;
        private static readonly object _lock = new object();

        private readonly GlobalGameConfiguration _config;
        private readonly MenuFactory _menuFactory;
        private readonly IInterfaceService _interfaceService;
        private EGameState _currentState;
        private EGameState _lastRenderedState = EGameState.None;

        // Constructor privado para evitar la creación de múltiples instancias
        private RenderManager(GameManager gameManager,
            GlobalGameConfiguration config,
            MenuFactory menuFactory,
            IInterfaceService interfaceService)
        {
            _config = config;
            _menuFactory = menuFactory;
            _interfaceService = interfaceService;

            // Suscribir RenderManager al GameManager
            gameManager.Subscribe(this);
        }

        // Propiedad para acceder a la instancia única del singleton
        public static RenderManager GetInstance(GameManager gameManager,
            GlobalGameConfiguration config,
            MenuFactory menuFactory,
            IInterfaceService interfaceService)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new RenderManager(gameManager, config, menuFactory, interfaceService);
                }
            }

            return _instance;
        }

        // Método que se llama cuando cambia el estado
        public void OnGameStateChanged(EGameState state)
        {
            _currentState = state;
        }

        // Método público Render para ser llamado en el bucle principal
        public void Render()
        {
            // Limpia la pantalla antes de dibujar cualquier elemento
            Engine.Clear();
            
            switch (_currentState)
            {
                case EGameState.InMenu:
                    _config.Menu = _menuFactory.CreateMainMenu();
                    _interfaceService.DrawMenu(_config.Screen, _config.SelectedButtonInterface,
                        _config.Menu.OptionsMenu, _config.SelectedColor, _config.NormalColor, _config.MenuStartX,
                        _config.MenuStartY, _config.MenuOffsetY, _config.MenuImageOffset);
                    break;

                case EGameState.InGame:
                    _interfaceService.DrawBoard();
                    _interfaceService.DrawCurrentPiece(_config.CurrentPiece, _config.CellSize, _config.OffsetX);
                    _interfaceService.DrawText("Next", _config.PositionInterfaceX, 5, _config.FontGame);
                    _interfaceService.DrawNextPiece(_config.NextPiece, _config.PositionInterfaceX, 30,
                        _config.CellSize);
                    _interfaceService.DrawText("Hold", _config.PositionInterfaceX, 155, _config.FontGame);
                    _interfaceService.DrawHeldPiece(_config.HeldPiece, _config.PositionInterfaceX, 180,
                        _config.CellSize);
                    _interfaceService.DrawText("Score", _config.PositionInterfaceX, 305, _config.FontGame);
                    _interfaceService.DrawText(_config.Score.ToString(), 615, 340, _config.FontGame);
                    break;

                case EGameState.InGameOver:
                    _config.Menu = _menuFactory.CreateGameOverMenu();
                    _interfaceService.DrawMenu(_config.Screen, _config.SelectedButtonInterface,
                        _config.Menu.OptionsMenu, _config.SelectedColor, _config.NormalColor, _config.MenuStartX,
                        _config.MenuStartY, _config.MenuOffsetY, _config.MenuImageOffset);
                    break;

                case EGameState.InCredits:
                    _config.Menu = _menuFactory.CreateCreditsMenu();
                    _interfaceService.DrawMenu(_config.Screen, _config.SelectedButtonInterface,
                        _config.Menu.OptionsMenu, _config.SelectedColor, _config.NormalColor, _config.MenuStartX,
                        _config.MenuStartY, _config.MenuOffsetY, _config.MenuImageOffset);
                    break;

                case EGameState.InControlgames:
                    _lastRenderedState = EGameState.InControlgames;
                    _config.Menu = _menuFactory.CreateControlsMenu();
                    _interfaceService.DrawMenu(_config.Screen, _config.SelectedButtonInterface,
                        _config.Menu.OptionsMenu, _config.SelectedColor, _config.NormalColor, _config.MenuStartX,
                        _config.MenuStartY, _config.MenuOffsetY, _config.MenuImageOffset);
                    break;

                case EGameState.WinGame:
                    _config.Menu = _menuFactory.CreateWinGameMenu();
                    _interfaceService.DrawMenu(_config.Screen, _config.SelectedButtonInterface,
                        _config.Menu.OptionsMenu, _config.SelectedColor, _config.NormalColor, _config.MenuStartX - 100,
                        _config.MenuStartY, _config.MenuOffsetY, _config.MenuImageOffset);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Mostrar los cambios en la pantalla
            Engine.Show();
        }
    }
}