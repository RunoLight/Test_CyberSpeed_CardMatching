using Application.Services;
using Domain.Data;
using Domain.Interfaces;

namespace Application.UseCases
{
    public class LoadGameUseCase
    {
        private readonly IGameSaver _saver;
        private readonly IGameStateService _gameStateService;

        public LoadGameUseCase(IGameSaver saver, IGameStateService gameStateService)
        {
            _saver = saver;
            _gameStateService = gameStateService;
        }

        public void ExecuteWithFallback(NewGameFactory fallbackDataFactory)
        {
            GameStateData savedGameStateData = _saver.Load();
            _gameStateService.LoadState(savedGameStateData ?? fallbackDataFactory.Create());
        }
    }
}