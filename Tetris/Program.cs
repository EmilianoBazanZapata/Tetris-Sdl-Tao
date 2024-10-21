using MyGame.Configuration;
using MyGame.Inputs;
using MyGame.Interfaces;
using MyGame.Services;
using Tao.Sdl;

namespace MyGame
{
    public class Program
    {
        private static GlobalGameConfiguration config;
        private static IInputStrategy inputStrategy;
        private static IGameVisualService gameVisualService = new GameVisualService();

        private static void Main(string[] args)
        {
            Engine.Initialize();
            Sdl.SDL_SetVideoMode(1280, 720, 15, Sdl.SDL_SWSURFACE);

            config = GlobalGameConfiguration.Instance;
            inputStrategy = new KeyboardInputStrategy();

            config.GameGrid.InitializeBoard();

            (config.CurrentPiece, config.NextPiece) = GameLogicService.GenerateRandomPieces();

            Sdl.SDL_Event evento;
            var running = true;

            while (running)
            {
                while (Sdl.SDL_PollEvent(out evento) != 0)
                {
                    if (evento.type == Sdl.SDL_QUIT)
                        running = false;
                }

                inputStrategy.CheckInputs(config);
                CheckInputs();
                Update();
                Render();
                Sdl.SDL_Delay(20); // Delay para reducir el consumo de CPU
            }

            Sdl.SDL_Quit();
        }

        private static void CheckInputs()
        {
            inputStrategy.CheckInputs(config);
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
            gameVisualService.DrawBoard(config.GameGrid);
            gameVisualService.DrawCurrentPiece(config.CurrentPiece, config.CellSize);
            gameVisualService.DrawNextPiece(config.NextPiece, 15, 2, config.CellSize);
            gameVisualService.DrawText("Score", 550, 140, config.Font);
            gameVisualService.DrawText(config.Score.ToString(), 557, 180, config.Font);
            Engine.Show();
        }
    }
}