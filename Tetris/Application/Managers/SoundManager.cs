using System;
using System.Collections.Generic;
using System.IO;
using Domain.Core;
using Domain.Enums;
using Domain.Interfaces;
using Tao.Sdl;

namespace Application.Managers
{
    public class SoundManager : IGameStateObserver, IDisposable
    {
        private static SoundManager _instance;
        private static readonly object _lock = new object();
        private Dictionary<string, Sound> _musicTracks;
        private bool _isDisposed = false;
        private string _currentTheme;

        private SoundManager()
        {
            _musicTracks = new Dictionary<string, Sound>();
            InitializeAudio();
        }

        public static SoundManager GetInstance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SoundManager();
                    }
                    return _instance;
                }
            }
        }

        public void OnGameStateChanged(EGameState state)
        {
            // switch (state)
            // {
            //     case EGameState.InMenu:
            //     case EGameState.InCredits:
            //     case EGameState.InControlgames:
            //         // Solo inicia "MenuTheme" si no está ya reproduciéndose
            //         if (_currentTheme != "MenuTheme")
            //         {
            //             PlayMusic("MenuTheme", loop: true);
            //             _currentTheme = "MenuTheme"; // Actualiza el tema actual
            //         }
            //         break;
            //     case EGameState.InGame:
            //         StopMusic();
            //         PlayMusic("GameTheme", loop: true);
            //         _currentTheme = "GameTheme";
            //         break;
            //     case EGameState.InGameOver:
            //         StopMusic();
            //         PlayMusic("GameOverTheme", loop: true);
            //         _currentTheme = "GameOverTheme";
            //         break;
            //     default:
            //         StopMusic();
            //         _currentTheme = null;
            //         break;
            // }
        }
        
        private void InitializeAudio()
        {
            if (SdlMixer.Mix_OpenAudio(22050, (short)SdlMixer.MIX_DEFAULT_FORMAT, 2, 4096) == -1)
            {
                Console.WriteLine("Error initializing SDL_mixer: " + SdlMixer.Mix_GetError());
            }
            LoadThemes();
        }

        private void LoadThemes()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.FullName;
            string[] themes = {"GameOverTheme", "GameTheme", "MenuTheme", "WinGame"};
            foreach (var theme in themes)
            {
                LoadMusic(theme, Path.Combine(projectDirectory, "Infrastructure", "Assets", "Sounds", $"{theme}.wav"));
            }
        }

        private void LoadMusic(string name, string path)
        {
            if (!_musicTracks.ContainsKey(name))
            {
                var sound = new Sound(path);
                _musicTracks[name] = sound;
            }
        }

        private void PlayMusic(string name, bool loop = true)
        {
            if (_musicTracks.TryGetValue(name, out var music))
            {
                if (loop)
                    music.Play();
                else
                    music.PlayOnce();
            }
            else
            {
                Console.WriteLine($"Music track {name} not found.");
            }
        }

        private void StopMusic()
        {
            SdlMixer.Mix_HaltMusic();
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
                    foreach (var music in _musicTracks.Values)
                    {
                        music.Dispose();
                    }
                    _musicTracks.Clear();
                }
                SdlMixer.Mix_CloseAudio();
                _isDisposed = true;
            }
        }

        ~SoundManager()
        {
            Dispose(false);
        }
    }
}
