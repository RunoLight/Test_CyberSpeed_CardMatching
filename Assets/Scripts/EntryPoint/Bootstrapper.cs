using Application;
using Domain.Data;
using Domain.Entities;
using Domain.Interfaces;
using Domain.UseCase;
using Domain.UseCases;
using Infrastructure;
using Infrastructure.SaveSystem;
using Infrastructure.SoundSystem;
using Presentation.ViewModels;
using Presentation.Views;
using UnityEngine;

namespace EntryPoint
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private GameView gameView;
        [SerializeField] private AudioManager audioManager;

        private void Start()
        {
            // Infrastructure
            var saver = new PlayerPrefsGameSaver();
            var soundService = new SoundService(audioManager);
            var fallbackCardProvider = new SimpleFallbackCardProvider(4, 4);

            // Domain services
            IGameStateService gameStateService = new GameStateService();
            var matchCheckService = new MatchCheckService(gameStateService, soundService);

            var saved = saver.Load();
            if (saved != null)
            {
                gameStateService.LoadState(saved);
            }
            else
            {
                var cards = CardGenerator.CreateCards(4, 4);  
                gameStateService.InitializeNewGame(cards);
            }

            // UseCases
            var flipUseCase = new FlipCardUseCase(gameStateService, matchCheckService, soundService);
            var loadGameUseCase = new LoadGameUseCase(saver, gameStateService, fallbackCardProvider);
            
            loadGameUseCase.Execute();

            // ViewModel
            var viewModel = new GameViewModel(flipUseCase, gameStateService);

            // Link View to ViewModel
            gameView.Init(viewModel, gameStateService.GetAllCards());
        }
    }
}