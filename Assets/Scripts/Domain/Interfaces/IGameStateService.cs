using System;
using System.Collections.Generic;
using Domain.Data;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IGameStateService
    {
        public event Action<Card> CardFlipped;
        public event Action<List<Card>> PairMatched;
        public event Action<List<Card>> PairMismatched;
        
        public void FlipCard(int cardId);
        public bool IsCardFlippable(int cardId);
        public List<Card> GetFaceUpCards();
        public void MatchCards(List<int> ids);
        public void ResetUnmatchedCards();

        public void LoadState(GameStateData data);
        public void InitializeNewGame(List<Card> cards);
        public List<Card> GetAllCards();

        public GameStateData SaveState();
    }
}