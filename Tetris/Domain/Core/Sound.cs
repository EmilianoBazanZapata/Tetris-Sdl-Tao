using System;
using Tao.Sdl;

namespace Domain.Core
{
    public class Sound
    {
        // Atributos
        private IntPtr _pointer;

        // Operaciones

        // Constructor a partir de un nombre de fichero
        public Sound(string nombreFichero)
        {
            _pointer = SdlMixer.Mix_LoadMUS(nombreFichero);
        }

        // Reproducir una vez
        public void PlayOnce()
        {
            SdlMixer.Mix_PlayMusic(_pointer, 1);
        }

        // Reproducir continuo (musica de fondo)
        public void Play()
        {
            SdlMixer.Mix_PlayMusic(_pointer, -1);
        }
    }
}
