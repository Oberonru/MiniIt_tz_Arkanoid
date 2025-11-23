using Core.Ball;
using Core.Configs.Audio;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Audio
{
    public class AudioPlayer :  MonoBehaviour
    {
        [Inject] private IAudioHandler _handler;
        [Inject] private AudioClipsConfig _config;
        [SerializeField] private BallInstance _ball; 

        private void OnEnable()
        {
            _ball.Controller.OnPlatformConcern.Subscribe(_ =>
            {
                _handler.PlaySfx(_config.PlatformConcern);
            }).AddTo(this);
        }

        private void Start()
        {
            _handler.PlayMusic(_config.LevelMusic);
        }
    }
}