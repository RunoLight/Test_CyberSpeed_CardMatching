namespace Domain.Interfaces
{
    public enum SoundType
    {
        Flip,
        Match,
        Mismatch,
        GameOver
    }

    public interface ISoundPlayer
    {
        public void Play(SoundType type);
    }
}