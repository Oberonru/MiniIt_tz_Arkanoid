using UnityEngine;

namespace Core.Audio
{
    public interface IAudioHandler
    {
        void PlaySfx(AudioClip clip);
        void PlaySfx(AudioClip[] clips);
        void PlayMusic(AudioClip clip, bool loop = false);
        void StopMusic();
    }
}