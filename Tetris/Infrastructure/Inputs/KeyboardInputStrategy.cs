using Application.Configurations;
using Application.Managers;
using Application.Strategies;
using Domain.Core;
using Domain.Interfaces;

namespace Infrastructure.Inputs
{
    public class KeyboardInputStrategy : IInputStrategy
    {
        private readonly IGameLogicService _gameLogicService;
        private readonly GlobalGameConfiguration _config;
        private readonly MovementManager _movementManager;
        

        public KeyboardInputStrategy(IGameLogicService gameLogicService,
                                     GlobalGameConfiguration config)
        {
            _gameLogicService = gameLogicService;
            _config = config;
            _movementManager = MovementManager.GetInstance(_config.Board);
        }

        public void CheckInputs()
        {
            HandleKeyboardInputs();
        }

        private void HandleKeyboardInputs()
        {
            // Detectar si la tecla de rotación está siendo presionada
            if (Engine.KeyPress(Engine.KEY_R))
            {
                if (!_config.RotationPerformed &&
                    _movementManager.CanMoveDown(_config.CurrentPiece, _config.Rows))
                {
                    _movementManager.Rotate(_config.CurrentPiece,_config.Columns, _config.Rows);
                    _config.RotationPerformed = true;
                }

                _config.RotationKeyPressed = true;
            }
            else
            {
                _config.RotationKeyPressed = false;
                _config.RotationPerformed = false;
            }

            // Movimiento a la izquierda con "A"
            if (Engine.KeyPress(Engine.KEY_A))
            {
                // Mover inmediatamente si se presiona una vez
                if (!_config.LeftMovementPerformed && _movementManager.CanMoveLeft(_config.CurrentPiece))
                {

                    _movementManager.MoveLeft(_config.CurrentPiece);

                    _config.LeftMovementPerformed = true;
                    _config.LateralLeftMovementCounter = 0; // Reiniciar el contador al mover inmediatamente
                }

                _config.LateralLeftMovementCounter++;

                if ((_config.LateralLeftMovementCounter >= _config.LateralMovementInterval) &&
                    _movementManager.CanMoveLeft(_config.CurrentPiece))
                {
                    _movementManager.MoveLeft(_config.CurrentPiece);
                    _config.LateralLeftMovementCounter = 0;
                }
            }
            else
            {
                _config.LeftMovementPerformed = false;
            }

            // Movimiento a la derecha con "D"
            if (Engine.KeyPress(Engine.KEY_D))
            {
                // Mover inmediatamente si se presiona una vez
                if (!_config.RightMovementPerformed &&
                    _movementManager.CanMoveRight(_config.CurrentPiece, _config))
                {
                    _movementManager.MoveRight(_config.CurrentPiece);
                    _config.RightMovementPerformed = true;
                    _config.LateralRightMovementCounter = 0; // Reiniciar el contador al mover inmediatamente
                }

                _config.LateralRightMovementCounter++;

                if ((_config.LateralRightMovementCounter >= _config.LateralMovementInterval) &&
                    _movementManager.CanMoveRight(_config.CurrentPiece, _config))
                {
                    _movementManager.MoveRight(_config.CurrentPiece);
                    _config.LateralRightMovementCounter = 0;
                }
            }
            else
            {
                _config.RightMovementPerformed = false;
            }

            // Movimiento hacia abajo con "S"
            if (Engine.KeyPress(Engine.KEY_S))
            {
                // Mover inmediatamente si se presiona una vez
                if (!_config.DownMovementPerformed &&
                    _movementManager.CanMoveDown(_config.CurrentPiece, _config.Rows))
                {
                    _movementManager.MoveDown(_config.CurrentPiece);
                    _config.DownMovementPerformed = true;
                    _config.LateralRightMovementCounter = 0; // Reiniciar el contador al mover inmediatamente
                }

                _config.DownMovementCounter++;

                if (_config.DownMovementCounter < _config.DownMovementInterval) return;

                if (_movementManager.CanMoveDown(_config.CurrentPiece, _config.Rows))
                    _movementManager.MoveDown(_config.CurrentPiece);
                
                _config.DownMovementCounter = 0;
            }
            else
            {
                _config.DownMovementPerformed = false;
            }

            // Almacenar Pieza
            if (Engine.KeyPress(Engine.KEY_Q))
            {
                if (_config.IsHoldKeyPressed) return;
                _gameLogicService.HoldPiece();
                _config.IsHoldKeyPressed = true;
            }
            else
            {
                _config.IsHoldKeyPressed = false;
            }
        }
    }
}