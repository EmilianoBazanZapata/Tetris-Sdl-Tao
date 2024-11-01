using Application.Configurations;

namespace Domain.Interfaces
{
    public interface IInputStrategy
    {
        void CheckInputs(GlobalGameConfiguration config);
    }
}