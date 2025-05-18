using Domain.Interfaces;

namespace Application.UseCases
{
    public class FlipCardUseCase
    {
        private readonly IGameStateService _gameState;
        private readonly IMatchCheckService _matchChecker;
        private readonly ISoundPlayer _soundPlayer;
        private readonly SaveGameUseCase _saveGameUseCase;

        public FlipCardUseCase(
            IGameStateService gameState,
            IMatchCheckService matchChecker,
            ISoundPlayer soundPlayer,
            SaveGameUseCase saveGameUseCase
        )
        {
            _gameState = gameState;
            _matchChecker = matchChecker;
            _soundPlayer = soundPlayer;
            _saveGameUseCase = saveGameUseCase;
        }

        public void Execute(int cardId)
        {
            if (!_gameState.IsCardFlippable(cardId))
                return;

            _gameState.FlipCard(cardId);
            _soundPlayer.Play(SoundType.Flip);

            _matchChecker.TryMatch();
            _saveGameUseCase.Execute();
        }
    }
}