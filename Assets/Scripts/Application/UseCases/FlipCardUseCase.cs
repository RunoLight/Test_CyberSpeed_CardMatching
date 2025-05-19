using Application.Services;
using Domain.Interfaces;

namespace Application.UseCases
{
    public class FlipCardUseCase
    {
        private readonly IGameStateService _gameState;
        private readonly ISoundPlayer _soundPlayer;
        private readonly SaveGameUseCase _saveGameUseCase;
        private readonly MatchCheckUseCase _matchCheckUseCase;
        private readonly MovesService _movesService;

        public FlipCardUseCase(
            IGameStateService gameState, ISoundPlayer soundPlayer,
            SaveGameUseCase saveGameUseCase, MatchCheckUseCase matchCheckUseCase,
            MovesService movesService
        )
        {
            _gameState = gameState;
            _soundPlayer = soundPlayer;
            _saveGameUseCase = saveGameUseCase;
            _matchCheckUseCase = matchCheckUseCase;
            _movesService = movesService;
        }

        public void Execute(int cardId)
        {
            if (!_gameState.IsCardFlippable(cardId))
                return;

            _movesService.Increase();
            _gameState.FlipCard(cardId);
            _soundPlayer.Play(SoundType.Flip);

            if (_gameState.GetFaceUpCards().Count == 2)
            {
                _matchCheckUseCase.Execute();
            }

            _saveGameUseCase.Execute();
        }
    }
}