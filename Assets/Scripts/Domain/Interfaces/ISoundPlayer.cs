namespace Domain.Interfaces
{
    public enum SoundType
    {
        Flip,
        Match,
        Mismatch,
        GameOver,
        NewGame
    }

    public interface ISoundPlayer
    {
        public void Play(SoundType type);
    }
}