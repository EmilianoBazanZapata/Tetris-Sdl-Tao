﻿using System;
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
using MyGame.Interfaces;
using Tao.Sdl;

namespace Tetris
{
    public static class Program
    {
        private static GlobalGameConfiguration _config;
        private static MenuFactory _menuFactory;
        private static IInputStrategy _inputMouse;
        private static IGameLogicService _gameLogicService;
        private static IInputStrategy _inputKeiboard;
        private static SoundManager _soundManager;
        private static GameManager _gameManager = new GameManager();
        private static IInterfaceService _interfaceService = new GameInterfaceService();

        private static void Main(string[] args)
        {
            Engine.Initialize();

            Sdl.SDL_WM_SetCaption("Tetris", null);

            var screen = Sdl.SDL_SetVideoMode(770, 720, 15, Sdl.SDL_SWSURFACE);

            _config = GlobalGameConfiguration.Instance;
            
            _gameManager.ChangeState(EGameState.InMenu);
            
            _config.Screen = screen;
            
            _config.GameGrid.InitializeBoard();
            
            _soundManager = SoundManager.Instance;
            
            _gameManager.Subscribe(_soundManager);

            _menuFactory = new MenuFactory(_gameManager);

            _gameLogicService = new GameLogicService(_config, _gameManager);

            _inputKeiboard = new KeyboardInputStrategy(_gameLogicService);

            _inputMouse = new MouseInputStrategy(_gameManager);

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
                    _config.Menu = _menuFactory.CreateControlsMenu();
                    _interfaceService.DrawMenu(_config.Screen, _config.SelectedButtonInterface,
                        _config.Menu.OptionsMenu, _config.SelectedColor, _config.NormalColor, _config.MenuStartX,
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