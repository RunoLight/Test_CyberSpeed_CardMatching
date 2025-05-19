using System.Collections.Generic;
using Domain.Entities;
using Presentation.ViewModels;
using TMPro;
using UnityEngine;

namespace Presentation.Views
{
    public class GameView : UnityEngine.MonoBehaviour
    {
        [SerializeField] private Transform cardContainer;
        [SerializeField] private CardView cardPrefab;
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private TMP_Text movesText;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private Transform winContainer;

        private GameViewModel _viewModel;

        private readonly Dictionary<int, CardView> _cardViews = new();

        public void Init(GameViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.GameStateService.GameStateChanged += RefreshView;
            _viewModel.GameStateService.GameWon += OnGameWon;
            _viewModel.GameStateService.CardFlipped += FlipCard;
            _viewModel.GameStateService.PairMatched += MatchCardsPair;
            _viewModel.GameStateService.PairMismatched += MismatchCardsPair;

            _viewModel.TimeUpdated += SetTime;
            _viewModel.ScoreChanged += SetScore;
            _viewModel.MovesAmountUpdated += SetMoves;
        }

        private void HandleCardClicked(int cardId)
        {
            _viewModel.OnCardClicked(cardId);
        }

        private void MismatchCardsPair(List<Card> mismatched)
        {
            foreach (var card in mismatched) _cardViews[card.Id].SetFaceDown();
        }

        private void MatchCardsPair(List<Card> matched)
        {
            foreach (var card in matched) _cardViews[card.Id].SetMatched();
        }

        private void FlipCard(Card card)
        {
            if (card.IsFaceUp)
            {
                _cardViews[card.Id].SetFaceUp();
            }
            else
            {
                _cardViews[card.Id].SetFaceDown();
            }
        }

        private void OnGameWon()
        {
            winContainer.gameObject.SetActive(true);
        }

        private void SetMoves(int amount)
        {
            movesText.text = $"Moves: {amount}";
        }

        private void SetTime(double seconds)
        {
            // TODO Time saves only on some action, its better to save also on exit so pre-exit AFK is counted
            timerText.text = $"Time: {seconds:f1}";
        }

        private void SetScore(int score)
        {
            scoreText.text = $"Score: {score}";
        }

        private void RefreshView(List<Card> cards, int score, double elapsedSeconds, int movesCount)
        {
            SetScore(score);
            SetTime(elapsedSeconds);

            winContainer.gameObject.SetActive(false);

            foreach (Transform child in cardContainer)
                Destroy(child.gameObject);

            _cardViews.Clear();

            foreach (var card in cards)
            {
                var cardView = Instantiate(cardPrefab, cardContainer)
                    .Init(card.Id, card.PairId, HandleCardClicked);

                _cardViews[card.Id] = cardView;

                UpdateCardView(card.Id, card);
            }
        }

        private void UpdateCardView(int cardId, Card card)
        {
            var view = _cardViews[cardId];
            if (card.IsMatched)
                view.SetMatched();
            else if (card.IsFaceUp)
                view.SetFaceUp();
            else
                view.SetFaceDown();
        }
    }
}