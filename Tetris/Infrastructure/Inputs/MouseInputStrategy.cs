using Application.Configurations;
using Application.Managers;
using Application.Strategies;
using Domain.Enums;
using Domain.Interfaces;
using Tao.Sdl;

namespace Infrastructure.Inputs
{
    public class MouseInputStrategy : IInputStrategy
    {
        private static Sdl.SDL_Event _sdlEvent;

        private readonly GameManager _gameManager;
        private readonly GlobalGameConfiguration _config;

        public MouseInputStrategy(GameManager gameManager,
            GlobalGameConfiguration config)
        {
            _gameManager = gameManager;
            _config = config;
        }

        public void CheckInputs()
        {
            HandleMouseInputs();
        }

        private void HandleMouseInputs()
        {
            // Procesa todos los eventos disponibles
            while (Sdl.SDL_PollEvent(out _sdlEvent) != 0)
            {
                // Manejar eventos de cierre de ventana
                if (_sdlEvent.type == Sdl.SDL_QUIT)
                {
                    _config.Running = false; // Esto hará que el loop principal del juego termine
                    return;
                }

                //si el juego tiene el estado ingame desactivo los controles del mouse
                if (_gameManager._currentState == EGameState.InGame)
                    continue;

                switch (_sdlEvent.type)
                {
                    case Sdl.SDL_QUIT:
                        _config.Running = false;
                        break;
                    case Sdl.SDL_MOUSEBUTTONDOWN:
                        if (_sdlEvent.button.button == Sdl.SDL_BUTTON_LEFT)
                        {
                            ExecuteOption(_config);
                        }

                        break;
                    case Sdl.SDL_MOUSEMOTION:
                        UpdateSelection(_sdlEvent.motion.x, _sdlEvent.motion.y);
                        break;
                }
            }
        }

        private void UpdateSelection(int mouseX, int mouseY)
        {
            var found = false;

            for (int i = 0; i < _config.Menu.OptionsMenu.Count; i++)
            {
                // Verifica si el mouse está sobre la opción (con o sin imagen)
                if (!IsMouseOverOption(mouseX, mouseY, i)) continue;

                if (_config.SelectedButtonInterface != i)
                    _config.SelectedButtonInterface = i; // Actualiza la opción seleccionada si es diferente

                found = true;
                break;
            }

            // Si no se encontró ninguna opción bajo el cursor, deselecciona
            if (!found && _config.SelectedButtonInterface != -1)
            {
                _config.SelectedButtonInterface = -1;
            }
        }

// Método auxiliar para verificar si el mouse está sobre una opción
        private bool IsMouseOverOption(int mouseX, int mouseY, int optionIndex)
        {
            // Obtén el MenuItem específico
            var menuItem = _config.Menu.OptionsMenu[optionIndex];

            // Define los límites de la opción utilizando PosX y PosY del MenuItem
            var optionTop = menuItem.PosY;
            var optionBottom = optionTop + _config.OptionHeight;
            var optionLeft = menuItem.PosX;
            var optionRight = optionLeft + _config.OptionWidth;

            // Verifica si el mouse está dentro de los límites de la opción
            return mouseY >= optionTop && mouseY <= optionBottom && mouseX >= optionLeft && mouseX <= optionRight;
        }


        private void ExecuteOption(GlobalGameConfiguration config)
        {
            // Solo ejecuta si hay una opción seleccionada
            if (config.SelectedButtonInterface < 0 ||
                config.SelectedButtonInterface >= config.Menu.OptionsMenu.Count) return;
            var selectedOption = config.Menu.OptionsMenu[config.SelectedButtonInterface];
            selectedOption.Action.Invoke();
        }
    }
}