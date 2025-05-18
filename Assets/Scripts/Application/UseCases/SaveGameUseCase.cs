using Domain.Interfaces;

namespace Application.UseCases
{
    public class SaveGameUseCase
    {
        private readonly IGameStateService _gameStateService;
        private readonly IGameSaver _gameSaver;

        public SaveGameUseCase(IGameStateService gameStateService, IGameSaver gameSaver)
        {
            _gameStateService = gameStateService;
            _gameSaver = gameSaver;
        }

        public void Execute()
        {
            var state = _gameStateService.SaveState();
            _gameSaver.Save(state);
        }
    }
}