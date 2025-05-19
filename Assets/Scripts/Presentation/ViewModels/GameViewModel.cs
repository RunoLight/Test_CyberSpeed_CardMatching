using System;
using Application.Services;
using Application.UseCases;
using Domain.Interfaces;
using Infrastructure.SoundSystem;

namespace Presentation.ViewModels
{
    public class GameViewModel
    {
        public event Action<double> TimeUpdated;
        public event Action<int> ScoreChanged;
        public event Action<int> MovesAmountUpdated;

        public IGameStateService GameStateService { get; }

        private readonly FlipCardUseCase _flipCardUseCase;
        private readonly SaveGameUseCase _saveUseCase;
        private readonly SoundService _soundService;
        private readonly NewGameFactory _newGameFactory;

        public GameViewModel(
            FlipCardUseCase flipCardUseCase, IGameStateService gameStateService,
            SaveGameUseCase saveUseCase, SoundService soundService, NewGameFactory newGameFactory,
            TimerService timerService, ScoreService scoreService, MovesService movesService
        )
        {
            _flipCardUseCase = flipCardUseCase;
            _saveUseCase = saveUseCase;
            _soundService = soundService;
            _newGameFactory = newGameFactory;
            GameStateService = gameStateService;

            timerService.TimeUpdated += time => { TimeUpdated?.Invoke(time); };

            scoreService.ScoreChanged += score => { ScoreChanged?.Invoke(score); };

            movesService.MovesAmountChanged += moves => { MovesAmountUpdated?.Invoke(moves); };
        }

        public void OnCardClicked(int cardId)
        {
            _flipCardUseCase.Execute(cardId);
        }

        public void OnSaveRequested()
        {
            _saveUseCase.Execute();
        }

        public void OnNewGameRequested()
        {
            GameStateService.LoadState(_newGameFactory.Create());
            _saveUseCase.Execute();
            _soundService.Play(SoundType.NewGame);
        }
    }
}