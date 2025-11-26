using Core.Audio.Model;
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
        
        [SerializeField] private MusicType _type;

        private void OnEnable()
        {
            if (_ball == null) return;
            
            _ball.Controller.OnPlatformConcern.Subscribe(_ =>
            {
                _handler.PlaySfx(_config.PlatformConcern);
            }).AddTo(this);
        }

        private void Start()
        {

            switch (_type)
            {
                case MusicType.MainMenu:
                    _handler.PlayMusic(_config.MenuMusic, true);
                    break;
                case MusicType.Level:
                default:
                    _handler.PlayMusic(_config.LevelMusic, true);
                    break;
            }
        }
    }
}