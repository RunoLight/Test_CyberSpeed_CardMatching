using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class GameStateService : IGameStateService
    {
        public event Action GameWon;
        public event Action<List<Card>, int, double, int> GameStateChanged;
        public event Action<Card> CardFlipped;
        public event Action<List<Card>> PairMatched;
        public event Action<List<Card>> PairMismatched;

        private readonly ScoreService _scoreService;
        private readonly TimerService _timerService;
        private readonly MovesService _movesService;

        private List<Card> _cards = new();

        public GameStateService(ScoreService scoreService, TimerService timerService, MovesService movesService)
        {
            _scoreService = scoreService;
            _timerService = timerService;
            _movesService = movesService;
        }

        public void FlipCard(int cardId)
        {
            var card = _cards.FirstOrDefault(c => c.Id == cardId);
            if (card == null || card.IsMatched)
                return;

            card.Flip();
            CardFlipped?.Invoke(card);
        }

        public bool IsCardFlippable(int cardId) => !_cards.First(c => c.Id == cardId).IsMatched;

        public List<Card> GetFaceUpCards() => _cards.Where(c => c.IsFaceUp && !c.IsMatched).ToList();

        public void MatchCards(List<int> ids)
        {
            var matched = new List<Card>();
            foreach (var card in ids.Select(id => _cards.First(c => c.Id == id)))
            {
                card.Match();
                matched.Add(card);
            }

            PairMatched?.Invoke(matched);

            TryPerformVictory();
        }

        public void ResetUnmatchedCards()
        {
            var mismatched = _cards.Where(c => c.IsFaceUp && !c.IsMatched).ToList();
            foreach (var card in mismatched)
                card.Flip();

            if (mismatched.Any())
                PairMismatched?.Invoke(mismatched);
        }

        public List<Card> GetAllCards() => _cards;

        public void LoadState(GameStateData data)
        {
            _cards = data.cards.Select(cardData => new Card(cardData)).ToList();

            _scoreService.SetPoints(data.score);
            _timerService.SetTime(data.elapsedSeconds);
            _movesService.SetAmount(data.movesCount);

            _timerService.Start();

            GameStateChanged?.Invoke(_cards,
                _scoreService.GetScore(),
                _timerService.GetTime(),
                _movesService.GetAmount()
            );

            TryPerformVictory();
        }

        public GameStateData SaveState()
        {
            return new GameStateData
            {
                cards = _cards.Select(c => new CardData(c)).ToList(),
                score = _scoreService.GetScore(),
                elapsedSeconds = _timerService.GetTime(),
                movesCount = _movesService.GetAmount()
            };
        }

        private void TryPerformVictory()
        {
            if (_cards.Any(card => !card.IsMatched))
                return;

            _timerService.Stop();
            GameWon?.Invoke();
        }
    }
}