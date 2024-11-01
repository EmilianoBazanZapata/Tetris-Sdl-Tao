using Domain.Enums;

namespace Domain.Interfaces
{
    public interface IGameStateObserver
    {
        void OnGameStateChanged(EGameState state);
    }
}