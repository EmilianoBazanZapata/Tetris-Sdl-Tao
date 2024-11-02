using System;
using Tao.Sdl;

namespace Domain.Core
{
    public class Sound : IDisposable
    {
        private IntPtr _pointer;
        private bool _isDisposed = false;

        public Sound(string filename)
        {
            _pointer = SdlMixer.Mix_LoadMUS(filename);
            if (_pointer == IntPtr.Zero)
            {
                throw new Exception("Failed to load music: " + SdlMixer.Mix_GetError());
            }
        }

        public void PlayOnce()
        {
            SdlMixer.Mix_PlayMusic(_pointer, 1);
        }

        public void Play()
        {
            SdlMixer.Mix_PlayMusic(_pointer, -1);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects) if any.
                }

                if (_pointer != IntPtr.Zero)
                {
                    SdlMixer.Mix_FreeMusic(_pointer);
                    _pointer = IntPtr.Zero;
                }
                _isDisposed = true;
            }
        }

        ~Sound()
        {
            Dispose(false);
        }
    }
}