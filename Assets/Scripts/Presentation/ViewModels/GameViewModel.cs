using Domain.Interfaces;
using Domain.UseCases;

namespace Presentation.ViewModels
{
    public class GameViewModel
    {
        private readonly FlipCardUseCase _flipCardUseCase;
        public IGameStateService GameStateService { get; }

        public GameViewModel(FlipCardUseCase flipCardUseCase, IGameStateService gameStateService)
        {
            _flipCardUseCase = flipCardUseCase;
            GameStateService = gameStateService;
        }

        public void OnCardClicked(int cardId)
        {
            _flipCardUseCase.Execute(cardId);
        }
    }
}