using Application.Configurations;
using Application.Factories;
using Application.Managers;
using Application.Services;
using Application.Strategies;
using Domain.Core;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Managers;
using Infrastructure.Inputs;
using Tao.Sdl;

namespace Infrastructure.Initializers
{
public class GameInitializer
    {
        public GlobalGameConfiguration Config { get; private set; }
        public MenuFactory MenuFactory { get; private set; }
        public IInputStrategy InputMouse { get; private set; }
        public IGameLogicService GameLogicService { get; private set; }
        public IInputStrategy InputKeyboard { get; private set; }
        public SoundManager SoundManager { get; private set; }
        public GameManager GameManager { get; private set; }
        public IInterfaceService InterfaceService { get; private set; }
        public RenderManager RenderManager { get; private set; }

        public void InitializeGame()
        {
            // Inicializar SDL y pantalla
            Engine.Initialize();
            Sdl.SDL_WM_SetCaption("Tetris", null);
            var screen = Sdl.SDL_SetVideoMode(770, 720, 15, Sdl.SDL_SWSURFACE);

            // Inicialización de configuraciones y dependencias
            Config = GlobalGameConfiguration.GetInstance;
            Config.Screen = screen;
            Config.GameGrid.InitializeBoard();

            // Inicializar administradores y servicios
            GameManager = new GameManager();
            SoundManager = SoundManager.GetInstance;
            MenuFactory = new MenuFactory(GameManager);
            GameLogicService = new GameLogicService(Config, GameManager);
            InputKeyboard = new KeyboardInputStrategy(GameLogicService, Config);
            InputMouse = new MouseInputStrategy(GameManager, Config);
            InterfaceService = new GameInterfaceService();

            // Suscribir SoundManager a los cambios de estado
            GameManager.Subscribe(SoundManager);

            // Generar las piezas iniciales
            (Config.CurrentPiece, Config.NextPiece) = GameLogicService.GenerateRandomPieces();

            // Configurar el RenderManager como singleton
            RenderManager = RenderManager.GetInstance(GameManager, Config, MenuFactory, InterfaceService);

            // Inicializar el menú si es nulo
            if (Config.Menu is null)
                Config.Menu = new Menu();

            // Establecer el estado inicial del juego
            GameManager.ChangeState(EGameState.InMenu);
        }
    }
}