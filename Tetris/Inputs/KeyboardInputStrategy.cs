using System;
using MyGame.Configuration;
using MyGame.Interfaces;

namespace MyGame.Inputs
{
    public class KeyboardInputStrategy : IInputStrategy
    {
        public void CheckInputs(GlobalGameConfiguration config)
        {
            // Detectar si la tecla de rotation está siendo presionada
            if (Engine.KeyPress(Engine.KEY_R) && 
                config.MovementController.CanMoveDown(config.CurrentPiece, config.Rows))
            {
                if (!config.RotationPerformed)
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
                if (config.MovementController.CanMoveLeft(config.CurrentPiece))
                {
                    config.CurrentPiece.MoveLeft();
                }
            }

            // Movimiento a la derecha con "D"
            if (Engine.KeyPress(Engine.KEY_D))
            {
                if (config.MovementController.CanMoveRight(config.CurrentPiece))
                {
                    config.CurrentPiece.MoveRight();
                }
            }

            // Movimiento hacia abajo con "S"
            if (Engine.KeyPress(Engine.KEY_S))
            {
                if (config.MovementController.CanMoveDown(config.CurrentPiece, config.Rows))
                {
                    config.CurrentPiece.MoveDown();
                }
            }
        }
    }

}