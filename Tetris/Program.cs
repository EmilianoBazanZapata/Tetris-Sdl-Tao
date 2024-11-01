using System;
using System.IO;
using Domain.Enums;
using Infrastructure.Initializers;
using Tao.Sdl;

namespace Tetris
{
    public static class Program
    {
        private static GameInitializer _initializer;

        public static void Main(string[] args)
        {
            _initializer = new GameInitializer();
            _initializer.InitializeGame();

            while (_initializer.Config.Running)
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
            _initializer.InputMouse.CheckInputs();
            _initializer.InputKeyboard.CheckInputs();
        }

        private static void Update()
        {
            if (_initializer.GameManager.currentState != EGameState.InGame) return;
            _initializer.Config.TimeCounter++;
            _initializer.GameLogicService.MovePieceAutomatically();
            _initializer.Config.TimeCounter++;
        }

        private static void Render()
        {
            _initializer.RenderManager.Render();
        }
    }
}