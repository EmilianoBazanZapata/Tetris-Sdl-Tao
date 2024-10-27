using System;
using MyGame.Enums;
using MyGame.Interfaces;

namespace MyGame.Observers
{
    public class GameObserver : IGameStateObserver
    {
        public void OnGameStateChanged(EGameState state)
        {
            switch (state)
            {
                case EGameState.InMenu:
                    Console.WriteLine("Game is now in Menu.");
                    break;
                case EGameState.InGame:
                    Console.WriteLine("Game is now In-Game.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}