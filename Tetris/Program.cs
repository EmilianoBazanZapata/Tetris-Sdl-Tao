using System;
using MyGame.Enums;
using MyGame.Managers;
using Tao.Sdl;

namespace MyGame
{
    internal class Program
    {
        private static GlobalGameConfiguration config;
        
        private static void Main(string[] args)
        {
            Engine.Initialize();
            Sdl.SDL_SetVideoMode(1280, 720, 15, Sdl.SDL_SWSURFACE);
            
            config = GlobalGameConfiguration.Instance;
            
            config.GameGrid.InicializarTablero();

            (config.CurrentPiece, config.NextPiece) =
                GenerarPiezasAleatorias();

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
            var config = Managers.GlobalGameConfiguration.Instance; // Accede a la configuración global

            // Detectar si la tecla de rotación está siendo presionada
            if (Engine.KeyPress(Engine.KEY_R) &&
                config.MovementController.PuedeMoverAbajo(config.CurrentPiece, config.Rows))
            {
                // Si la tecla se presiona y no hemos rotado aún
                if (!config.RotationPerformed)
                {
                    config.CurrentPiece.Rotar(config.Columns, config.Rows);
                    Console.WriteLine("Pieza rotada correctamente.");

                    config.RotationPerformed = true; // Marcar que ya se realizó la rotación
                }

                config.RotationKeyPressed = true; // Marcar que la tecla está presionada
            }
            else
            {
                // Si la tecla ya no está presionada, reiniciar la posibilidad de rotar
                config.RotationKeyPressed = false;
                config.RotationPerformed = false; // Permitir una nueva rotación en la siguiente pulsación
            }

            config.LateralMovementCounter++;

            // Movimiento a la izquierda con "A"
            if (Engine.KeyPress(Engine.KEY_A))
            {
                // Mover inmediatamente si se presiona una vez
                if (!config.LeftMovementPerformed)
                {
                    if (config.MovementController.PuedeMoverIzquierda(config.CurrentPiece))
                    {
                        config.CurrentPiece.MoverIzquierda();
                        Console.WriteLine("Pieza movida a la izquierda.");
                    }

                    config.LeftMovementPerformed = true;
                    config.LateralMovementCounter = 0; // Reiniciar el contador al mover inmediatamente
                }

                // Mover continuamente mientras se mantenga presionada la tecla
                if (config.LateralMovementCounter >= config.LateralMovementInterval)
                {
                    if (config.MovementController.PuedeMoverIzquierda(config.CurrentPiece))
                    {
                        config.CurrentPiece.Posicion =
                            (config.CurrentPiece.Posicion.x - 1, config.CurrentPiece.Posicion.y);
                        Console.WriteLine("Pieza movida a la izquierda.");
                    }

                    config.LateralMovementCounter = 0; // Reiniciar el contador
                }
            }
            else
            {
                // Cuando se suelta la tecla "A", permitir un nuevo movimiento
                config.LeftMovementPerformed = false;
            }

            // Movimiento a la derecha con "D"
            if (Engine.KeyPress(Engine.KEY_D))
            {
                // Mover inmediatamente si se presiona una vez
                if (!config.RightMovementPerformed)
                {
                    if (config.MovementController.PuedeMoverDerecha(config.CurrentPiece))
                    {
                        config.CurrentPiece.MoverDerecha();
                        Console.WriteLine("Pieza movida a la derecha.");
                    }

                    config.RightMovementPerformed = true;
                    config.LateralMovementCounter = 0; // Reiniciar el contador al mover inmediatamente
                }

                // Mover continuamente mientras se mantenga presionada la tecla
                if (config.LateralMovementCounter >= config.LateralMovementInterval)
                {
                    if (config.MovementController.PuedeMoverDerecha(config.CurrentPiece))
                    {
                        config.CurrentPiece.Posicion =
                            (config.CurrentPiece.Posicion.x + 1, config.CurrentPiece.Posicion.y);
                        Console.WriteLine("Pieza movida a la derecha.");
                    }

                    config.LateralMovementCounter = 0; // Reiniciar el contador
                }
            }
            else
            {
                // Cuando se suelta la tecla "D", permitir un nuevo movimiento
                config.RightMovementPerformed = false;
            }

            config.DownMovementCounter++;

            // Movimiento hacia abajo con "S"
            if (Engine.KeyPress(Engine.KEY_S))
            {
                // Movimiento inmediato hacia abajo
                if (!config.DownMovementPerformed)
                {
                    if (config.MovementController.PuedeMoverAbajo(config.CurrentPiece, config.Rows))
                    {
                        config.CurrentPiece.Posicion =
                            (config.CurrentPiece.Posicion.x, config.CurrentPiece.Posicion.y + 1);
                    }

                    config.DownMovementPerformed = true;
                    config.DownMovementCounter = 0; // Reiniciar el contador al mover inmediatamente
                }

                // Movimiento continuo mientras se mantenga presionada la tecla "S"
                if (config.DownMovementCounter < config.DownMovementInterval) return;
                if (config.MovementController.PuedeMoverAbajo(config.CurrentPiece, config.Rows))
                {
                    config.CurrentPiece.Posicion = (config.CurrentPiece.Posicion.x, config.CurrentPiece.Posicion.y + 1);
                }

                config.DownMovementCounter = 0; // Reiniciar el contador
            }
            else
            {
                config.DownMovementPerformed = false;
            }
        }


        private static void Update()
        {
            config.TimeCounter++;

            MoverPiezaAutomnaticamente();

            config.TimeCounter++;
        }

        private static void MoverPiezaAutomnaticamente()
        {
            // Mover la pieza hacia abajo después de cierto tiempo
            if (config.TimeCounter < config.DropInterval) return;

            // Verificar si la pieza puede moverse hacia abajo
            if (config.MovementController.PuedeMoverAbajo(
                    config.CurrentPiece, config.Rows))
            {
                config.CurrentPiece.MoverAbajo();
            }
            else
            {
                // Fijar la pieza en el tablero
                Pieza.FijarPiezaEnTablero(config.CurrentPiece,
                    config.Columns,
                    config.Rows,
                    config.Board);

                // Limpiar filas completas
                config.GameGrid.LimpiarFilasCompletas();

                // Generar una nueva pieza (actualizar piezaActual y piezaSiguiente)
                config.CurrentPiece =
                    config.NextPiece; // La actual se convierte en la siguiente
                (config.NextPiece, _) =
                    GenerarPiezasAleatorias(); // Generar una nueva siguiente pieza
            }

            config.TimeCounter = 0; // Reiniciar el contador
        }


        private static void Render()
        {
            config.GameGrid.DibujarTablero();

            config.CurrentPiece.DibujarPieza(config.CellSize);

            Engine.Show();
        }

        private static (Pieza piezaActual, Pieza piezaSiguiente) GenerarPiezasAleatorias()
        {
            var random = new Random();

            // Generar la pieza actual
            var tipoPiezaActual = (TipoPieza)random.Next(1, 7); // Genera un número entre 1 y 7 para la pieza actual
            var piezaActual = Pieza.CrearPieza(tipoPiezaActual);
            piezaActual.Posicion = (10, 0); // Posicionar la nueva pieza en la parte superior del tablero

            // Generar la siguiente pieza
            var tipoPiezaSiguiente =
                (TipoPieza)random.Next(1, 7); // Genera un número entre 1 y 7 para la siguiente pieza
            var piezaSiguiente = Pieza.CrearPieza(tipoPiezaSiguiente);
            piezaSiguiente.Posicion = (10, 0);

            return (piezaActual, piezaSiguiente); // Retornar ambas piezas
        }
    }
}