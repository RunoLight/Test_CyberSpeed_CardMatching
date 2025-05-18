using Domain.Interfaces;

namespace Domain.UseCase
{
    public class LoadGameUseCase
    {
        private readonly IGameSaver _saver;
        private readonly IGameStateService _gameStateService;
        private readonly ILoadFallbackCardsProvider _fallbackCardsProvider;

        public LoadGameUseCase(
            IGameSaver saver,
            IGameStateService gameStateService,
            ILoadFallbackCardsProvider fallbackCardsProvider)
        {
            _saver = saver;
            _gameStateService = gameStateService;
            _fallbackCardsProvider = fallbackCardsProvider;
        }

        public void Execute()
        {
            var saved = _saver.Load();

            if (saved != null)
            {
                _gameStateService.LoadState(saved);
            }
            else
            {
                var cards = _fallbackCardsProvider.CreateInitialCards();
                _gameStateService.InitializeNewGame(cards);
            }
        }
    }
}