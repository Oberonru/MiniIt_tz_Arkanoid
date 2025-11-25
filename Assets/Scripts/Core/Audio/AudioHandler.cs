using UnityEngine;

namespace Core.Audio
{
    public class AudioHandler : MonoBehaviour, IAudioHandler
    {
        [SerializeField] private AudioSource _sfxSource;
        [SerializeField] private AudioSource _musicSource;
        [SerializeField, Range(0f, 1f)] private float _minPitch = 0.9f;
        [SerializeField, Range(1f, 2f)] private float _maxPitch = 1.1f;

        public void PlaySfx(AudioClip clip)
        {
            ChangePitch();
            _sfxSource.PlayOneShot(clip);
        }


        public void PlaySfx(AudioClip[] clips)
        {
            if (clips == null || clips.Length == 0)
            {
                Debug.LogWarning("No clip given to PlaySfx");

                return;
            }

            var rnd = Random.Range(0, clips.Length);
            ChangePitch();
            _musicSource.PlayOneShot(clips[rnd]);
        }

        public void PlayMusic(AudioClip clip, bool loop = false)
        {
            _musicSource.clip = clip;
            _musicSource.loop = loop;
            _musicSource.Play();
        }

        public void StopMusic()
        {
            _musicSource.Stop();
        }

        private void ChangePitch()
        {
            _sfxSource.pitch = Random.Range(_minPitch, _minPitch);
        }
    }
}