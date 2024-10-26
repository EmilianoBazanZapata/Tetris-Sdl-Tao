using MyGame.Configuration;
using MyGame.Inputs;
using MyGame.Interfaces;
using MyGame.Services;
using Tao.Sdl;

namespace MyGame
{
    public static class Program
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

            (config.CurrentPiece, config.NextPiece) = GameLogicService.GenerateRandomPieces(config);

            Sdl.SDL_Event evento;
            var running = true;

            while (running)
            {
                while (Sdl.SDL_PollEvent(out evento) != 0)
                {
                    if (evento.type == Sdl.SDL_QUIT)
                        running = false;
                }

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
            gameVisualService.DrawText("Next", config.PositionInterfaceX, 5, config.Font);
            gameVisualService.DrawNextPiece(config.NextPiece, config.PositionInterfaceX, 30, config.CellSize);
            gameVisualService.DrawText("Hold", config.PositionInterfaceX, 155, config.Font);
            gameVisualService.DrawHeldPiece(config.HeldPiece, config.PositionInterfaceX, 180, config.CellSize);
            gameVisualService.DrawText("Score", config.PositionInterfaceX, 305, config.Font);
            gameVisualService.DrawText(config.Score.ToString(), config.PositionInterfaceX, 340, config.Font);
            Engine.Show();
        }
    }
}