using MyGame.Configuration;
using MyGame.Interfaces;
using Tao.Sdl;

namespace MyGame.Inputs
{
    public class MouseInputStrategy : IInputStrategy
    {
        private static Sdl.SDL_Event _sdlEvent;
        
        
        public void CheckInputs(GlobalGameConfiguration config)
        {
            HandleMouseInputs(config);
        }

        private void HandleMouseInputs(GlobalGameConfiguration config)
        {
            // Procesa todos los eventos disponibles
            while (Sdl.SDL_PollEvent(out _sdlEvent) != 0)
            {
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
            int startY = 0;  // Y inicial para las opciones del menú, ajusta si es necesario
            int offsetX = 0; // X inicial para las opciones del menú, ajusta si es necesario
            int offsetY = 50;  // Espacio vertical entre las opciones del menú
            int optionHeight = 30;  // Altura de cada opción del menú
            int optionWidth = 200;  // Ancho de las opciones del menú

            bool found = false;

            for (int i = 0; i < config.Menu.items.Count; i++)
            {
                // Calcula los límites de cada opción del menú
                int optionTop = startY + i * offsetY;
                int optionBottom = optionTop + optionHeight;
                int optionLeft = offsetX;
                int optionRight = optionLeft + optionWidth;

                // Verifica si el mouse está sobre una opción
                if (mouseY < optionTop || mouseY > optionBottom || mouseX < optionLeft ||
                    mouseX > optionRight) continue;
                if (config.SelectedButtonInterface != i)
                {
                    config.SelectedButtonInterface = i;
                    config.Menu.Display(config.Screen, config.SelectedButtonInterface);  // Redibuja si hay cambio
                }
                found = true;
                break;
            }

            // Si no se encuentra ninguna opción bajo el cursor, deselecciona
            if (!found && config.SelectedButtonInterface != -1)
            {
                config.SelectedButtonInterface = -1;
                config.Menu.Display(config.Screen, config.SelectedButtonInterface);  // Redibuja si no hay selección
            }
        }

        private void ExecuteOption(GlobalGameConfiguration config)
        {
            // Solo ejecuta si hay una opción seleccionada
            if (config.SelectedButtonInterface < 0 || config.SelectedButtonInterface >= config.Menu.items.Count) return;
            var selectedOption = config.Menu.items[config.SelectedButtonInterface];
            selectedOption.Action.Invoke();
        }
    }
}
