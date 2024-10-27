using MyGame.Enums;

namespace MyGame.Interfaces
{
    public interface IGameStateObserver
    {
        void OnGameStateChanged(EGameState state);
    }
}