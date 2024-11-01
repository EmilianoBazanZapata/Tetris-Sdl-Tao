using System;
using Tao.Sdl;

namespace Domain.Core
{
    public class Image
    {
        public IntPtr Pointer { get; private set; }

        public Image(string imagePath)
        {
            LoadImage(imagePath);
        }

        public void LoadImage(string imagePath)
        {
            Pointer = SdlImage.IMG_Load(imagePath);
            if (Pointer != IntPtr.Zero) return;
            var error = SdlImage.IMG_GetError();
            Console.WriteLine("Error al cargar la imagen {0}: {1}", imagePath, error);
            throw new InvalidOperationException($"No se pudo cargar la imagen {imagePath}: {error}");
        }

        ~Image()
        {
            if (Pointer == IntPtr.Zero) return;
            Sdl.SDL_FreeSurface(Pointer);
            Pointer = IntPtr.Zero;
        }
    }
}