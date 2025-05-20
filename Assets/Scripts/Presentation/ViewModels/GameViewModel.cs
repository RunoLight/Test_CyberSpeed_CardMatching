using System;
using System.Collections.Generic;
using Application.Services;
using Application.UseCases;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.SoundSystem;
using UnityEngine;

namespace Presentation.ViewModels
{
    public class GameViewModel
    {
        public event Action<double> TimeUpdated;
        public event Action<int> ScoreChanged;
        public event Action<int> MovesAmountUpdated;
        public event Action GameWon;
        public event Action<List<Card>, int, double, int> GameStateChanged;
        public event Action<Card> CardFlipped;
        public event Action<List<Card>> PairMatched;
        public event Action<List<Card>> PairMismatched;

        private IGameStateService GameStateService { get; }

        private readonly Queue<PendingMatchPair> _pendingPairs = new();
        private PendingMatchPair _currentMatchPair;

        private readonly FlipCardUseCase _flipCardUseCase;
        private readonly SaveGameUseCase _saveUseCase;
        private readonly SoundService _soundService;
        private readonly NewGameFactory _newGameFactory;
        private readonly MatchCheckUseCase _matchCheckUseCase;

        public GameViewModel(
            FlipCardUseCase flipCardUseCase, IGameStateService gameStateService, SaveGameUseCase saveUseCase,
            SoundService soundService, NewGameFactory newGameFactory, TimerService timerService,
            ScoreService scoreService, MovesService movesService, MatchCheckUseCase matchCheckUseCase
        )
        {
            _flipCardUseCase = flipCardUseCase;
            _saveUseCase = saveUseCase;
            _soundService = soundService;
            _newGameFactory = newGameFactory;
            _matchCheckUseCase = matchCheckUseCase;
            GameStateService = gameStateService;

            GameStateService.GameStateChanged +=
                (cards, arg2, arg3, arg4) =>
                {
                    _pendingPairs.Clear();

                    var faceUpCards = GameStateService.GetFaceUpCards();
                    if (faceUpCards.Count != 0)
                    {
                        _currentMatchPair = new PendingMatchPair()
                        {
                            FirstCardId = faceUpCards[0].Id,
                            FirstReady = true
                        };
                        _pendingPairs.Enqueue(_currentMatchPair);
                    }

                    GameStateChanged?.Invoke(cards, arg2, arg3, arg4);
                };
            GameStateService.GameWon += () => GameWon?.Invoke();
            GameStateService.CardFlipped += card => CardFlipped?.Invoke(card);
            GameStateService.PairMatched += list => PairMatched?.Invoke(list);
            GameStateService.PairMismatched += list => PairMismatched?.Invoke(list);

            timerService.TimeUpdated += time => { TimeUpdated?.Invoke(time); };

            scoreService.ScoreChanged += score => { ScoreChanged?.Invoke(score); };

            movesService.MovesAmountChanged += moves => { MovesAmountUpdated?.Invoke(moves); };
        }

        public void FaceUpAnimationFinished(int cardId)
        {
            foreach (var pair in _pendingPairs)
            {
                if (pair.FirstCardId == cardId)
                    pair.FirstReady = true;
                if (pair.SecondCardId == cardId)
                    pair.SecondReady = true;

                if (pair.IsReady)
                {
                    _matchCheckUseCase.Execute(pair.FirstCardId, pair.SecondCardId);
                }
            }

            while (_pendingPairs.Count > 0 && _pendingPairs.Peek().IsReady)
            {
                _pendingPairs.Dequeue();
            }
        }

        public void OnCardClicked(int cardId)
        {
            _flipCardUseCase.Execute(cardId);
            if (GameStateService.GetCard(cardId).IsFaceUp)
            {
                if (_currentMatchPair == null)
                {
                    _currentMatchPair = new PendingMatchPair { FirstCardId = cardId, FirstReady = false };
                    _pendingPairs.Enqueue(_currentMatchPair);
                }
                else if (_currentMatchPair.SecondCardId == 0)
                {
                    _currentMatchPair.SecondCardId = cardId;
                    _currentMatchPair = null;
                }
            }
            else
            {
                var peekPair = _pendingPairs.Peek();
                if (peekPair.FirstCardId != cardId)
                {
                    Debug.LogError("Undefined behaviour");
                    return;
                }

                _pendingPairs.Dequeue();
            }
        }

        public void OnSaveRequested()
        {
            _saveUseCase.Execute();
        }

        public void OnNewGameRequested()
        {
            GameStateService.LoadState(_newGameFactory.Create());
            _saveUseCase.Execute();
            _soundService.Play(SoundType.NewGame);
        }

        private class PendingMatchPair
        {
            public int FirstCardId;
            public int SecondCardId;
            public bool FirstReady;
            public bool SecondReady;

            public bool IsReady => FirstReady && SecondReady;
        }
    }
}