using System.Collections.Generic;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Managers
{
    public class GameManager
    {
        public EGameState _currentState;
        private List<IGameStateObserver> _observers = new List<IGameStateObserver>();

        public GameManager()
        {
            _currentState = EGameState.InMenu;  // Estado inicial
        }

        // Método para suscribir observadores
        public void Subscribe(IGameStateObserver observer)
        {
            _observers.Add(observer);
        }

        // Método para desuscribir observadores
        public void Unsubscribe(IGameStateObserver observer)
        {
            _observers.Remove(observer);
        }

        // Cambiar el estado del juego y notificar a los observadores
        public void ChangeState(EGameState newState)
        {
            if (_currentState == newState) return;
            _currentState = newState;
            NotifyObservers();
        }

        // Notificar a los observadores del cambio de estado
        private void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.OnGameStateChanged(_currentState);
            }
        }
    }
}