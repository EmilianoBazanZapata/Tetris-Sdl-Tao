using MyGame.Configuration;

namespace MyGame.Interfaces
{
    public interface IInputStrategy
    {
        void CheckInputs(GlobalGameConfiguration config);
    }
}