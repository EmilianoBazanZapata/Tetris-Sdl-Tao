using System;
using Tao.Sdl;

namespace Domain.Core
{
    public class Sound
    {
        // Atributos
        private IntPtr _pointer;
        private bool _isMusic;

        // Operaciones

        // Constructor a partir de un nombre de fichero
        public Sound(string nombreFichero, bool isMusic )
        {
            _pointer = SdlMixer.Mix_LoadMUS(nombreFichero);
            _isMusic = isMusic;
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

        // Interrumpir toda la reproducción de sonido
        public void Stop()
        {
            SdlMixer.Mix_HaltMusic();
        }

        public void Dispose()
        {
            if (_isMusic)
            {
                SdlMixer.Mix_FreeMusic(_pointer);
            }
            else
            {
                SdlMixer.Mix_FreeChunk(_pointer);
            }
        }
    }
}
