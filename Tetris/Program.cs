using System;
using MyGame.Enums;
using Tao.Sdl;

namespace MyGame
{
    internal class Program
    {
        static Image imagenPiezaI =
            Engine.LoadImage("D:\\Utn\\Programacion\\Tetris-Tsl-Tao\\Tetris\\assets\\TileYellow2.png");

        static int columnas = 20;
        static int filas = 40;
        static int celda = 15; // Tamaño de cada celda en píxeles

        static int[,] tablero = new int[filas, columnas]; // Matriz del tablero

        static IntPtr pantalla; // Representación de la ventana principal

        static Pieza piezaActual = new Pieza();
        static Grilla grilla = new Grilla(tablero, columnas, filas, celda);

        private static MovementController controller = new MovementController(tablero);

        // Temporizador para bajar la pieza automáticamente
        static int contadorTiempo = 0;
        static int intervaloTiempo = 30; // Controla la velocidad de caída (ajústalo según prefieras)

        // Variable para controlar la rotación
        // Variable para controlar si ya se ha realizado la rotación
        static bool rotacionRealizada = false;

        // Variable para verificar si la tecla está siendo presionada
        static bool teclaRotacionPresionada = false;

        // Variables para controlar el movimiento lateral
        static bool movimientoIzquierdaRealizado = false;
        static bool movimientoDerechaRealizado = false;

        // Contadores para el movimiento lateral
        static int contadorMovimientoLateral = 0;
        static int intervaloMovimientoLateral = 10; // Ajusta este valor según la velocidad que quieras

        // Variables para controlar el movimiento hacia abajo con "S"
        static bool movimientoAbajoRealizado = false;
        static int contadorMovimientoAbajo = 0;
        static int intervaloMovimientoAbajo = 10; // Ajusta este valor según la velocidad deseada

        private static void Main(string[] args)
        {
            Engine.Initialize();
            pantalla = Sdl.SDL_SetVideoMode(300, 600, 32, Sdl.SDL_SWSURFACE); // Definir la ventana

            piezaActual = GenerarPiezaAleatoria();

            grilla.InicializarTablero();

            Sdl.SDL_Event evento;
            var running = true;

            while (running)
            {
                while (Sdl.SDL_PollEvent(out evento) != 0)
                {
                    if (evento.type == Sdl.SDL_QUIT)
                    {
                        running = false;
                    }
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
            // Detectar si la tecla de rotación está siendo presionada
            if (Engine.KeyPress(Engine.KEY_R) && controller.PuedeMoverAbajo(piezaActual, filas))
            {
                // Si la tecla se presiona y no hemos rotado aún
                if (!rotacionRealizada)
                {
                    piezaActual.Rotar(columnas, filas);
                    Console.WriteLine("Pieza rotada correctamente.");

                    rotacionRealizada = true; // Marcar que ya se realizó la rotación
                }

                teclaRotacionPresionada = true; // Marcar que la tecla está presionada
            }
            else
            {
                // Si la tecla ya no está presionada, reiniciar la posibilidad de rotar
                teclaRotacionPresionada = false;
                rotacionRealizada = false; // Permitir una nueva rotación en la siguiente pulsación
            }

            contadorMovimientoLateral++;

            // Movimiento a la izquierda con "A"
            if (Engine.KeyPress(Engine.KEY_A))
            {
                // Mover inmediatamente si se presiona una vez
                if (!movimientoIzquierdaRealizado)
                {
                    if (controller.PuedeMoverIzquierda(piezaActual))
                    {
                        piezaActual.Posicion = (piezaActual.Posicion.x - 1, piezaActual.Posicion.y);
                        Console.WriteLine("Pieza movida a la izquierda.");
                    }

                    movimientoIzquierdaRealizado = true;
                    contadorMovimientoLateral = 0; // Reiniciar el contador al mover inmediatamente
                }

                // Mover continuamente mientras se mantenga presionada la tecla
                if (contadorMovimientoLateral >= intervaloMovimientoLateral)
                {
                    if (controller.PuedeMoverIzquierda(piezaActual))
                    {
                        piezaActual.Posicion = (piezaActual.Posicion.x - 1, piezaActual.Posicion.y);
                        Console.WriteLine("Pieza movida a la izquierda.");
                    }

                    contadorMovimientoLateral = 0; // Reiniciar el contador
                }
            }
            else
            {
                // Cuando se suelta la tecla "A", permitir un nuevo movimiento
                movimientoIzquierdaRealizado = false;
            }

            // Movimiento a la derecha con "D"
            if (Engine.KeyPress(Engine.KEY_D))
            {
                // Mover inmediatamente si se presiona una vez
                if (!movimientoDerechaRealizado)
                {
                    if (controller.PuedeMoverDerecha(piezaActual))
                    {
                        piezaActual.Posicion = (piezaActual.Posicion.x + 1, piezaActual.Posicion.y);
                        Console.WriteLine("Pieza movida a la derecha.");
                    }

                    movimientoDerechaRealizado = true;
                    contadorMovimientoLateral = 0; // Reiniciar el contador al mover inmediatamente
                }

                // Mover continuamente mientras se mantenga presionada la tecla
                if (contadorMovimientoLateral >= intervaloMovimientoLateral)
                {
                    if (controller.PuedeMoverDerecha(piezaActual))
                    {
                        piezaActual.Posicion = (piezaActual.Posicion.x + 1, piezaActual.Posicion.y);
                        Console.WriteLine("Pieza movida a la derecha.");
                    }

                    contadorMovimientoLateral = 0; // Reiniciar el contador
                }
            }
            else
            {
                // Cuando se suelta la tecla "D", permitir un nuevo movimiento
                movimientoDerechaRealizado = false;
            }

            contadorMovimientoAbajo++;

            // Movimiento hacia abajo con "S"
            if (Engine.KeyPress(Engine.KEY_S))
            {
                // Movimiento inmediato hacia abajo
                if (!movimientoAbajoRealizado)
                {
                    if (controller.PuedeMoverAbajo(piezaActual, filas))
                    {
                        piezaActual.Posicion = (piezaActual.Posicion.x, piezaActual.Posicion.y + 1);
                    }

                    movimientoAbajoRealizado = true;
                    contadorMovimientoAbajo = 0; // Reiniciar el contador al mover inmediatamente
                }

                // Movimiento continuo mientras se mantenga presionada la tecla "S"
                if (contadorMovimientoAbajo < intervaloMovimientoAbajo) return;
                if (controller.PuedeMoverAbajo(piezaActual, filas))
                {
                    piezaActual.Posicion = (piezaActual.Posicion.x, piezaActual.Posicion.y + 1);
                }

                contadorMovimientoAbajo = 0; // Reiniciar el contador
            }
            else
            {
                movimientoAbajoRealizado = false;
            }
        }

        private static void Update()
        {
            contadorTiempo++;

            MoverPiezaAutomnaticamente();
            
            contadorTiempo++;
        }

        private static void MoverPiezaAutomnaticamente()
        {
            // Mover la pieza hacia abajo después de cierto tiempo
            if (contadorTiempo < intervaloTiempo) return;
            // Verificar si la pieza puede moverse hacia abajo
            if (controller.PuedeMoverAbajo(piezaActual, filas))
            {
                piezaActual.MoverAbajo();
            }
            else
            {
                // Fijar la pieza en el tablero
                Pieza.FijarPiezaEnTablero(piezaActual, columnas, filas, tablero);

                // Limpiar filas completas
                grilla.LimpiarFilasCompletas();

                // Generar una nueva pieza (por ahora, seguimos con la pieza "I")
                piezaActual = GenerarPiezaAleatoria();

                // En el futuro, podrías agregar lógica aquí para verificar si el jugador ha perdido
            }

            contadorTiempo = 0; // Reiniciar el contador
        }

        private static void Render()
        {
            grilla.DibujarTablero();

            piezaActual.DibujarPieza(imagenPiezaI, celda);
            // Mostrar los cambios en pantalla
            Engine.Show();
        }
        
        private static Pieza GenerarPiezaAleatoria()
        {
            var random = new Random();
            var tipoPieza = (TipoPieza)random.Next(1, 7); // Genera un número entre 1 y 7 y lo convierte al enum

            //Posicionar la nueva pieza en la parte superior del tablero
            var piezaActual = Pieza.CrearPieza(tipoPieza);
            piezaActual.Posicion = (10, 0);

            return piezaActual;
        }
    }
}