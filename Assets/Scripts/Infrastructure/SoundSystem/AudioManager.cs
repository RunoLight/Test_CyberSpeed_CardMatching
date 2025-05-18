using Domain.Interfaces;
using UnityEngine;

namespace Infrastructure.SoundSystem
{
    public class AudioManager : MonoBehaviour
    {
        public AudioClip flipClip;
        public AudioClip matchClip;
        public AudioClip mismatchClip;
        public AudioClip gameOverClip;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(SoundType type)
        {
            AudioClip clip = type switch
            {
                SoundType.Flip => flipClip,
                SoundType.Match => matchClip,
                SoundType.Mismatch => mismatchClip,
                SoundType.GameOver => gameOverClip,
                _ => null
            };

            if (clip != null)
                _audioSource.PlayOneShot(clip);
        }
    }
}