using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IGameStateService
    {
        public void FlipCard(int cardId);
        public bool IsCardFlippable(int cardId);
        public List<Card> GetFaceUpCards();
        public void MatchCards(List<int> ids);
        public void ResetUnmatchedCards();
    }
}