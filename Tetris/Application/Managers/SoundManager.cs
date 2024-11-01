using System;
using System.Collections.Generic;
using System.IO;
using Domain.Core;
using Domain.Enums;
using Domain.Interfaces;
using Tao.Sdl;

namespace Application.Managers
{
    public class SoundManager : IGameStateObserver
    {
        private static SoundManager _instance;
        private static readonly object _lock = new object();
        private Dictionary<string, Sound> _musicTracks;

        private SoundManager()
        {
            _musicTracks = new Dictionary<string, Sound>();
            InitializeAudio();
        }

        public static SoundManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SoundManager();
                    }
                }

                return _instance;
            }
        }

        public void OnGameStateChanged(EGameState state)
        {
            switch (state)
            {
                case EGameState.InMenu:
                    StopMusic();
                    PlayMusic("MenuTheme", loop: true);
                    break;
                case EGameState.InGame:
                    StopMusic();
                    PlayMusic("GameTheme", loop: true);
                    break;
                case EGameState.InGameOver:
                    StopMusic();
                    PlayMusic("GameOvertheme", loop: true);
                    break;
                case EGameState.InCredits:
                    PlayMusic("MenuTheme", loop: true);
                    break;
                case EGameState.InControlgames:
                    PlayMusic("MenuTheme", loop: true);
                    break;
                default:
                    StopMusic();
                    break;
            }
        }
        
        private void InitializeAudio()
        {
            // Inicializa el sistema de audio de SDL_mixer
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
            
            LoadMusic("GameOvertheme", Path.Combine(projectDirectory, "Infrastructure", "Assets", "Sounds", "GameOvertheme.mp3"));
            LoadMusic("GameTheme", Path.Combine(projectDirectory, "Infrastructure", "Assets", "Sounds", "GameTheme.mp3"));
            LoadMusic("MenuTheme", Path.Combine(projectDirectory, "Infrastructure", "Assets", "Sounds", "MenuTheme.mp3"));
            LoadMusic("WinGame", Path.Combine(projectDirectory, "Infrastructure", "Assets", "Sounds", "WinGame.mp3"));
        }

        // Cargar música
        private void LoadMusic(string name, string path)
        {
            var sound = new Sound(path, isMusic: false);
            _musicTracks[name] = sound;
        }
        
        // Reproducir música de fondo
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

        // Detener la música de fondo
        private void StopMusic()
        {
            SdlMixer.Mix_HaltMusic();
        }
    }
}