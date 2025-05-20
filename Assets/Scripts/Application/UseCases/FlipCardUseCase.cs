using Application.Services;
using Domain.Interfaces;

namespace Application.UseCases
{
    public class FlipCardUseCase
    {
        private readonly IGameStateService _gameState;
        private readonly ISoundPlayer _soundPlayer;
        private readonly SaveGameUseCase _saveGameUseCase;
        private readonly MovesService _movesService;

        public FlipCardUseCase(
            IGameStateService gameState, ISoundPlayer soundPlayer,
            SaveGameUseCase saveGameUseCase, MovesService movesService
        )
        {
            _gameState = gameState;
            _soundPlayer = soundPlayer;
            _saveGameUseCase = saveGameUseCase;
            _movesService = movesService;
        }

        public void Execute(int cardId)
        {
            if (!_gameState.IsCardFlippable(cardId))
                return;

            _movesService.Increase();
            _gameState.FlipCard(cardId);
            _soundPlayer.Play(SoundType.Flip);

            _saveGameUseCase.Execute();
        }
    }
}