using System;
using System.IO;
using Application.Configurations;
using Application.Factories;
using Application.Managers;
using Application.Services;
using Application.Strategies;
using Domain.Core;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
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
        public MovementManager MovementManager { get; private set; }
        public IInterfaceService InterfaceService { get; private set; }
        public RenderManager RenderManager { get; private set; }

        public void InitializeGame()
        {
            // Inicializar SDL y pantalla
            Engine.Initialize();

            LoadIcon();

            var screen = Sdl.SDL_SetVideoMode(770, 720, 15, Sdl.SDL_SWSURFACE);

            // Inicialización de configuraciones y dependencias
            Config = GlobalGameConfiguration.GetInstance;
            Config.Screen = screen;
            Config.GameGrid.InitializeBoard();

            // Inicializar administradores y servicios
            GameManager = GameManager.GetInstance;
            SoundManager = SoundManager.GetInstance;
            
            // Suscribir SoundManager a los cambios de estado
            GameManager.Subscribe(SoundManager);
            
            // Establecer el estado inicial del juego
            GameManager.ChangeState(EGameState.InMenu);
            
            MenuFactory = new MenuFactory(GameManager);
            GameLogicService = new GameLogicService(Config, GameManager);
            InputKeyboard = new KeyboardInputStrategy(GameLogicService, Config);
            InputMouse = new MouseInputStrategy(GameManager, Config);
            InterfaceService = new GameInterfaceService();

            // Generar las piezas iniciales
            (Config.CurrentPiece, Config.NextPiece) = GameLogicService.GenerateRandomPieces();
            
            MovementManager = MovementManager.GetInstance(Config.Board, GameLogicService);
            
            // Configurar el RenderManager como singleton
            RenderManager = RenderManager.GetInstance(GameManager, Config, MenuFactory, InterfaceService);
            
            // Suscribir RenderManager a los cambios de estado
            GameManager.Subscribe(RenderManager);

            // Inicializar el menú si es nulo
            if (Config.Menu is null)
                Config.Menu = new Menu();
        }

        private void LoadIcon()
        {
            // Ruta relativa al ícono BMP en la carpeta assets
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            
            var projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.FullName;

            var iconPath = Path.Combine(projectDirectory, "Application", "Assets", "Icons", "Icon.bmp");
            
            // Cargar el archivo BMP
            var icon = Sdl.SDL_LoadBMP(iconPath);

            Sdl.SDL_WM_SetIcon(icon, null);

            Sdl.SDL_WM_SetCaption("Tetris", null);
        }
    }
}