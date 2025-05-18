using Domain.Interfaces;

namespace Domain.UseCases
{
    public class FlipCardUseCase
    {
        private readonly IGameStateService _gameState;
        private readonly IMatchCheckService _matchChecker;
        private readonly ISoundPlayer _soundPlayer;

        public FlipCardUseCase(IGameStateService gameState, IMatchCheckService matchChecker, ISoundPlayer soundPlayer)
        {
            _gameState = gameState;
            _matchChecker = matchChecker;
            _soundPlayer = soundPlayer;
        }

        public void Execute(int cardId)
        {
            if (!_gameState.IsCardFlippable(cardId))
                return;

            _gameState.FlipCard(cardId);
            _soundPlayer.Play(SoundType.Flip);

            _matchChecker.TryMatch();
        }
    }
}