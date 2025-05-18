using Application.UseCases;
using Domain.Interfaces;

namespace Presentation.ViewModels
{
    public class GameViewModel
    {
        private readonly FlipCardUseCase _flipCardUseCase;
        private readonly SaveGameUseCase _saveUseCase;
        public IGameStateService GameStateService { get; }

        public GameViewModel(FlipCardUseCase flipCardUseCase, IGameStateService gameStateService,
            SaveGameUseCase saveUseCase)
        {
            _flipCardUseCase = flipCardUseCase;
            _saveUseCase = saveUseCase;
            GameStateService = gameStateService;
        }

        public void OnCardClicked(int cardId)
        {
            _flipCardUseCase.Execute(cardId);
        }
        
        public void OnSaveRequested()
        {
            _saveUseCase.Execute();
        }
    }
}