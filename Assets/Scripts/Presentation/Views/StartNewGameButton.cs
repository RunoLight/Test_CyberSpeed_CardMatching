using Presentation.ViewModels;
using UnityEngine.UI;

namespace Presentation.Views
{
    public class StartNewGameButton : UnityEngine.MonoBehaviour
    {
        private GameViewModel _viewModel;

        public void Init(GameViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void OnClick()
        {
            _viewModel.OnNewGameRequested();
        }

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            GetComponent<Button>().onClick.RemoveListener(OnClick);
        }
    }
}