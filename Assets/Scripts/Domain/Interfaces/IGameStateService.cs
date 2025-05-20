using System;
using System.Collections.Generic;
using Domain.Data;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IGameStateService
    {
        public event Action GameWon;
        public event Action<List<Card>, int, double, int> GameStateChanged;
        public event Action<Card> CardFlipped;
        public event Action<List<Card>> PairMatched;
        public event Action<List<Card>> PairMismatched;

        public void FlipCard(int cardId);
        public bool IsCardFlippable(int cardId);
        public List<Card> GetFaceUpCards();
        public (Card first, Card second) GetPairCards(int firstId, int secondId);
        public Card GetCard(int id);
        public void MatchCards(List<int> ids);
        public void ResetUnmatchedCards();

        /// <summary>
        /// Loads game state. New game and loading existing one are starts here
        /// </summary>
        /// <param name="data"></param>
        public void LoadState(GameStateData data);

        public GameStateData SaveState();
    }
}