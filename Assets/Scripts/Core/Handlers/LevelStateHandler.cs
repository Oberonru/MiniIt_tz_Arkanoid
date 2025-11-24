using System;
using Core.Audio;
using Core.Ball;
using Core.Configs.Audio;
using Core.Game;
using Core.Gameplay;
using Core.Platform;
using Core.UI.Handlers;
using Core.UI.Model;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Handlers
{
    public class LevelStateHandler : MonoBehaviour
    {
        [Inject] private IScreenHandler _screenHandler;
        [Inject] private IAudioHandler _audioHandler;
        [Inject] private AudioClipsConfig _clipsConfig;
        
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private LoseZone _loseZone;
        [SerializeField] private PlatformInstance _platform;
        [SerializeField] private BallInstance _ball;

        private void OnEnable()
        {
            _loseZone.OnLose.Subscribe(_ =>
                {
                    _platform.Health.TakeDamage(1);
                    _audioHandler.PlaySfx(_clipsConfig.RestartClip);
                    ResetPosition();
                })
                .AddTo(this);

            _platform.Health.OnDestroyed.Take(1).
                Subscribe(_ => 
                {
                    _audioHandler.PlaySfx(_clipsConfig.GameOverClip);        
                    _screenHandler.SetScreen(ScreenType.LoseScreen);
                })
                .AddTo(this);

            _gameManager.OnWin.Take(1).Subscribe(_ =>
            {
                _audioHandler.PlaySfx(_clipsConfig.WinClip);
                _screenHandler.SetScreen(ScreenType.WinScreen);
                ResetPosition();

                _platform.StateHandler.DisableAllComponents();
            }).AddTo(this);
        }

        private void OnValidate()
        {
            if (_gameManager == null) _gameManager = FindObjectOfType<GameManager>();
        }

        private void ResetPosition()
        {
            _platform.Controller.Reset();
            _ball.Controller.Reset();
            _ball.Disable();
            _ball.Enable();
        }
    }
}