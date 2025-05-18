using Domain.Interfaces;

namespace Infrastructure.SoundSystem
{
    public class SoundService : ISoundPlayer
    {
        private readonly AudioManager _audioManager;

        public SoundService(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        public void Play(SoundType type)
        {
            _audioManager.PlaySound(type);
        }
    }
}