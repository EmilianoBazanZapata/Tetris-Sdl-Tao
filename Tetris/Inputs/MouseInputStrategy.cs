using MyGame.Configuration;
using MyGame.Enums;
using MyGame.Interfaces;
using MyGame.Managers;
using Tao.Sdl;

namespace MyGame.Inputs
{
    public class MouseInputStrategy : IInputStrategy
    {
        private static Sdl.SDL_Event _sdlEvent;
        
        private readonly GameManager _gameManager;

        public MouseInputStrategy(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        
        public void CheckInputs(GlobalGameConfiguration config)
        {
            HandleMouseInputs(config);
        }

        private void HandleMouseInputs(GlobalGameConfiguration config)
        {
            // Procesa todos los eventos disponibles
            while (Sdl.SDL_PollEvent(out _sdlEvent) != 0)
            {
                // Manejar eventos de cierre de ventana
                if (_sdlEvent.type == Sdl.SDL_QUIT)
                {
                    config.Running = false; // Esto hará que el loop principal del juego termine
                    return;
                }
                
                //si el juego tiene el estado ingame desactivo los controles del mouse
                if (_gameManager.currentState == EGameState.InGame)
                    continue;
                
                switch (_sdlEvent.type)
                {
                    case Sdl.SDL_QUIT:
                        config.Running = false;
                        break;
                    case Sdl.SDL_MOUSEBUTTONDOWN:
                        if (_sdlEvent.button.button == Sdl.SDL_BUTTON_LEFT)
                        {
                            ExecuteOption(config);
                        }
                        break;
                    case Sdl.SDL_MOUSEMOTION:
                        UpdateSelection(_sdlEvent.motion.x, _sdlEvent.motion.y, config);
                        break;
                }
            }
        }

        private void UpdateSelection(int mouseX, int mouseY, GlobalGameConfiguration config)
        {
            var found = false;

            for (int i = 0; i < config.Menu.OptionsMenu.Count; i++)
            {
                // Verifica si el mouse está sobre la opción (con o sin imagen)
                if (IsMouseOverOption(mouseX, mouseY, config, i))
                {
                    if (config.SelectedButtonInterface != i)
                    {
                        config.SelectedButtonInterface = i; // Actualiza la opción seleccionada si es diferente
                    }
                    found = true;
                    break;
                }
            }

            // Si no se encontró ninguna opción bajo el cursor, deselecciona
            if (!found && config.SelectedButtonInterface != -1)
            {
                config.SelectedButtonInterface = -1;
            }
        }

// Método auxiliar para verificar si el mouse está sobre una opción
        private bool IsMouseOverOption(int mouseX, int mouseY, GlobalGameConfiguration config, int optionIndex)
        {
            int optionTop = config.MenuStartY + (config.Menu.OptionsMenu[optionIndex].Image != null ? config.MenuImageOffset : 0) + optionIndex * config.MenuOffsetY;
            int optionBottom = optionTop + config.OptionHeight;
            int optionLeft = config.MenuStartX;
            int optionRight = optionLeft + config.OptionWidth;

            // Verifica si el mouse está dentro de los límites de la opción
            return mouseY >= optionTop && mouseY <= optionBottom && mouseX >= optionLeft && mouseX <= optionRight;
        }


        private void ExecuteOption(GlobalGameConfiguration config)
        {
            // Solo ejecuta si hay una opción seleccionada
            if (config.SelectedButtonInterface < 0 || config.SelectedButtonInterface >= config.Menu.OptionsMenu.Count) return;
            var selectedOption = config.Menu.OptionsMenu[config.SelectedButtonInterface];
            selectedOption.Action.Invoke();
        }
    }
}
