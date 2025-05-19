using System.Collections.Generic;

namespace Domain.Data
{
    [System.Serializable]
    public class GameStateData
    {
        public List<CardData> cards;
        public int movesCount;
        public int score;
        public double elapsedSeconds;
    }
}