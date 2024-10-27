using System.Collections.Generic;
using MyGame.Enums;
using MyGame.Interfaces;

namespace MyGame.Managers
{
    public class GameManager
    {
        private EGameState currentState;
        private List<IGameStateObserver> observers = new List<IGameStateObserver>();

        public GameManager()
        {
            currentState = EGameState.InMenu;  // Estado inicial
        }

        // Método para suscribir observadores
        public void Subscribe(IGameStateObserver observer)
        {
            observers.Add(observer);
        }

        // Método para desuscribir observadores
        public void Unsubscribe(IGameStateObserver observer)
        {
            observers.Remove(observer);
        }

        // Cambiar el estado del juego y notificar a los observadores
        public void ChangeState(EGameState newState)
        {
            if (currentState == newState) return;
            currentState = newState;
            NotifyObservers();
        }

        // Notificar a los observadores del cambio de estado
        private void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.OnGameStateChanged(currentState);
            }
        }
    }
}