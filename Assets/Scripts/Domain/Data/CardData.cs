using Domain.Entities;

namespace Domain.Data
{
    [System.Serializable]
    public class CardData
    {
        public int Id;
        public int PairId;
        public bool IsFaceUp;
        public bool IsMatched;

        public CardData(Card card)
        {
            Id = card.Id;
            PairId = card.PairId;
            IsFaceUp = card.IsFaceUp;
            IsMatched = card.IsMatched;
        }
    }
}