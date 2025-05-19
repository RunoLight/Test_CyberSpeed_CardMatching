using System;

namespace Application.Services
{
    public class ScoreService
    {
        private int _score;

        public event Action<int> ScoreChanged;

        public void SetPoints(int points)
        {
            _score = points;
            ScoreChanged?.Invoke(_score);
        }

        public void AddPoints(int points)
        {
            _score += points;
            ScoreChanged?.Invoke(_score);
        }

        public int GetScore() => _score;
    }
}