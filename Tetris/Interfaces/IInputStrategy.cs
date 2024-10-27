using MyGame.Configuration;
using Tao.Sdl;

namespace MyGame.Interfaces
{
    public interface IInputStrategy
    {
        void CheckInputs(GlobalGameConfiguration config, Sdl.SDL_Event sdlEvent);
    }
}