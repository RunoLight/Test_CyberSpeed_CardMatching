using Domain.Data;

namespace Domain.Entities
{
    public class Card
    {
        public int Id { get; }
        public int PairId { get; }
        public bool IsFaceUp { get; private set; }
        public bool IsMatched { get; private set; }
        
        public Card(int id, int pairId)
        {
            Id = id;
            PairId = pairId;
        }

        public Card(CardData cardData)
        {
            Id = cardData.Id;
            PairId = cardData.PairId;
            IsMatched =cardData.IsMatched;
            IsFaceUp = cardData.IsFaceUp;
        }

        public void Flip() => IsFaceUp = !IsFaceUp;
        public void Match() => IsMatched = true;
    }
}