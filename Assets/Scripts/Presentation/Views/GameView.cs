using System.Collections.Generic;
using Domain.Entities;
using Presentation.ViewModels;
using UnityEngine;

namespace Presentation.Views
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private Transform _cardContainer;  
        [SerializeField] private CardView _cardPrefab;      

        private GameViewModel _viewModel;
        
        private readonly Dictionary<int, CardView> _cardViews = new();

        public void Init(GameViewModel viewModel, List<Card> cards)
        {
            _viewModel = viewModel;

            foreach (Transform child in _cardContainer)
                Destroy(child.gameObject);

            _cardViews.Clear();

            foreach (var card in cards)
            {
                var cardView = InstantiateCardView(card);
                _cardViews[card.Id] = cardView;
                
                UpdateCardView(card.Id, card);
            }

            viewModel.GameStateService.CardFlipped += card =>
            {
                if (card.IsFaceUp)
                {
                    _cardViews[card.Id]. SetFaceUp();
                }
                else
                {
                    _cardViews[card.Id]. SetFaceDown();
                }
            };
            
            viewModel.GameStateService.PairMatched += matched =>
            {
                foreach (var card in matched)
                    _cardViews[card.Id].SetMatched();
            };
            
            viewModel.GameStateService.PairMismatched += mismatched =>
            {
                foreach (var card in mismatched)
                    _cardViews[card.Id].SetFaceDown();
            };


            CardView InstantiateCardView(Card card)
            {
                var cardGO = Instantiate(_cardPrefab, _cardContainer);
                var cardView = cardGO.GetComponent<CardView>();
                cardView.Init(card.Id, card.PairId);
                cardView.OnClicked = HandleCardClicked;


                return cardGO;
            }
        }

        private void HandleCardClicked(int cardId)
        {
            _viewModel.OnCardClicked(cardId);
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