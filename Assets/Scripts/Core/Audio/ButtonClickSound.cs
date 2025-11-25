using Core.Configs.Audio;
using UnityEngine;
using Zenject;

namespace Core.Audio
{
    public class ButtonClickSound : MonoBehaviour
    {
        [Inject] private IAudioHandler _audioHandler;
        [Inject] private AudioClipsConfig _config;
        
        public void PlayClickSound()
        {
            _audioHandler.PlaySfx(_config.ClickButtonClip);
        }
    }
}