using Application;
using Application.Services;
using Application.UseCases;
using Domain;
using Domain.Interfaces;
using Infrastructure.Generators;
using Infrastructure.SaveSystem;
using Infrastructure.SoundSystem;
using Presentation.MonoBehaviour;
using Presentation.ViewModels;
using Presentation.Views;
using UnityEngine;

namespace EntryPoint
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private TimerBehaviour timerBehaviour;
        [SerializeField] private GameView gameView;
        [SerializeField] private AudioManager audioManager;

        private void Start()
        {
            // Infrastructure
            var saver = new PlayerPrefsGameSaver();
            var soundService = new SoundService(audioManager);

            // Domain services
            var newGameFactory = new NewGameFactory(new CardGenerator(), 4, 4);
            var scoreService = new ScoreService();
            var timerService = new TimerService();
            var movesService = new MovesService();

            IGameStateService gameStateService = new GameStateService(scoreService, timerService, movesService);
            var matchCheckService = new MatchCheckService(gameStateService, soundService);

            // UseCases
            var loadGameUseCase = new LoadGameUseCase(saver, gameStateService);
            var saveUseCase = new SaveGameUseCase(gameStateService, saver);
            var matchCheckUseCase = new MatchCheckUseCase(
                matchCheckService, gameStateService, soundService, saveUseCase, scoreService
            );

            var flipUseCase = new FlipCardUseCase(
                gameStateService, soundService, saveUseCase, matchCheckUseCase, movesService
            );

            // Behaviours
            timerBehaviour.Init(timerService);

            // ViewModel
            var viewModel = new GameViewModel(flipUseCase, gameStateService, saveUseCase, soundService, newGameFactory,
                timerService, scoreService, movesService);

            // Link View to ViewModel
            gameView.Init(viewModel);

            // Link UI components to ViewModel
            foreach (var newGameButton in FindObjectsOfType<StartNewGameButton>())
            {
                newGameButton.Init(viewModel);
            }

            // Load and start game
            loadGameUseCase.ExecuteWithFallback(newGameFactory);
        }
    }
}