using Core.Configs.Audio;
using Core.Platform;
using UnityEngine;
using Zenject;

namespace Core.Audio
{
    public class AudioPlayer :  MonoBehaviour
    {
        [Inject] private IAudioHandler _handler;
        [Inject] private AudioClipsConfig _config;
        [SerializeField] private PlatformInstance _platform;

        private void OnEnable()
        {
            
        }

        private void Start()
        {
            _handler.PlayMusic(_config.LevelMusic);
        }
    }
}