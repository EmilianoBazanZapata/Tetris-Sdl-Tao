using MyGame.Configuration;
using MyGame.Interfaces;
using MyGame.Services;

namespace MyGame.Inputs
{
    public class KeyboardInputStrategy : IInputStrategy
    {
        public void CheckInputs(GlobalGameConfiguration config)
        {
            // Detectar si la tecla de rotación está siendo presionada
            if (Engine.KeyPress(Engine.KEY_R))
            {
                if (!config.RotationPerformed &&
                    config.MovementController.CanMoveDown(config.CurrentPiece, config.Rows))
                {
                    config.CurrentPiece.Rotate(config.Columns, config.Rows);
                    config.RotationPerformed = true;
                }

                config.RotationKeyPressed = true;
            }
            else
            {
                config.RotationKeyPressed = false;
                config.RotationPerformed = false;
            }

            // Movimiento a la izquierda con "A"
            if (Engine.KeyPress(Engine.KEY_A))
            {
                // Mover inmediatamente si se presiona una vez
                if (!config.LeftMovementPerformed && config.MovementController.CanMoveLeft(config.CurrentPiece))
                {
                    config.CurrentPiece.MoveLeft();
                    config.LeftMovementPerformed = true;
                    config.LateralLeftMovementCounter = 0; // Reiniciar el contador al mover inmediatamente
                }

                config.LateralLeftMovementCounter++;

                if ((config.LateralLeftMovementCounter >= config.LateralMovementInterval) &&
                    config.MovementController.CanMoveLeft(config.CurrentPiece))
                {
                    config.CurrentPiece.MoveLeft();
                    config.LateralLeftMovementCounter = 0;
                }
            }
            else
            {
                config.LeftMovementPerformed = false;
            }

            // Movimiento a la derecha con "D"
            if (Engine.KeyPress(Engine.KEY_D))
            {
                // Mover inmediatamente si se presiona una vez
                if (!config.RightMovementPerformed &&
                    config.MovementController.CanMoveRight(config.CurrentPiece, config))
                {
                    config.CurrentPiece.MoveRight();
                    config.RightMovementPerformed = true;
                    config.LateralRightMovementCounter = 0; // Reiniciar el contador al mover inmediatamente
                }

                config.LateralRightMovementCounter++;

                if ((config.LateralRightMovementCounter >= config.LateralMovementInterval) &&
                    config.MovementController.CanMoveRight(config.CurrentPiece, config))
                {
                    config.CurrentPiece.MoveRight();
                    config.LateralRightMovementCounter = 0;
                }
            }
            else
            {
                config.RightMovementPerformed = false;
            }

            // Movimiento hacia abajo con "S"
            if (Engine.KeyPress(Engine.KEY_S))
            {
                // Mover inmediatamente si se presiona una vez
                if (!config.DownMovementPerformed &&
                    config.MovementController.CanMoveDown(config.CurrentPiece, config.Rows))
                {
                    config.CurrentPiece.MoveDown();
                    config.DownMovementPerformed = true;
                    config.LateralRightMovementCounter = 0; // Reiniciar el contador al mover inmediatamente
                }

                config.DownMovementCounter++;

                if (config.DownMovementCounter < config.DownMovementInterval) return;

                if (config.MovementController.CanMoveDown(config.CurrentPiece, config.Rows))
                    config.CurrentPiece.MoveDown();

                config.DownMovementCounter = 0;
            }
            else
            {
                config.DownMovementPerformed = false;
            }

            // Almacenar Pieza
            if (Engine.KeyPress(Engine.KEY_Q))
            {
                if (config.IsHoldKeyPressed) return;
                GameLogicService.HoldPiece(config);
                config.IsHoldKeyPressed = true;
            }
            else
            {
                config.IsHoldKeyPressed = false; 
            }
        }
    }
}