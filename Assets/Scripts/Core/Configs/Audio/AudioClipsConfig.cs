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
        [SerializeField] private AudioClip _platformConcern;
        [SerializeField] private AudioClip _restartClip;
        [SerializeField] private AudioClip _gameOverClip;
        [SerializeField] private AudioClip _winClip;
        [SerializeField] private AudioClip _wallHit;
        
        public AudioClip LevelMusic => _levelMusic;
        public AudioClip[] BrickHits => _brickHits;
        public AudioClip DestroyClip => _destroyClip;
        public AudioClip PlatformConcern => _platformConcern;
        public AudioClip RestartClip => _restartClip;
        public AudioClip GameOverClip => _gameOverClip;
        public AudioClip WinClip => _winClip;
        public AudioClip WallHit => _wallHit;
    }
}