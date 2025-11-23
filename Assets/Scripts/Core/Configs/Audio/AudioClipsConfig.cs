using Infrastructure.Configs;
using UnityEngine;

namespace Core.Configs.Audio
{
    [CreateAssetMenu(menuName = "Config/Audio/AudioClipsConfig", fileName = "AudioClipsConfig")]

    public class AudioClipsConfig : ScriptableConfig
    {
        [SerializeField] private AudioClip _levelMusic;
        [SerializeField] private AudioClip[] _brickHits;
        [SerializeField] private AudioClip _destroyClip;
        
        public AudioClip LevelMusic => _levelMusic;
        public AudioClip[] BrickHits => _brickHits;
        public AudioClip DestroyClip => _destroyClip;
    }
}