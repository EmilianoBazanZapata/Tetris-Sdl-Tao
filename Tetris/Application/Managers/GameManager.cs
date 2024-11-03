using System.Collections.Generic;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Managers
{
    public class GameManager
    {
        // Instancia Singleton
        private static GameManager _instance;
        private static readonly object _lock = new object();

        // Estado actual del juego y lista de observadores
        public EGameState _currentState;
        private List<IGameStateObserver> _observers = new List<IGameStateObserver>();

        // Constructor privado para evitar instancias múltiples
        private GameManager()
        {
            _currentState = EGameState.None;
        }

        // Propiedad para acceder a la instancia única de GameManager
        public static GameManager GetInstance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new GameManager();
                    }
                }

                return _instance;
            }
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