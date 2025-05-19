using Presentation.ViewModels;
using UnityEngine.UI;

namespace Presentation.Views
{
    /// <summary>
    /// Currently not used as there is auto-saving
    /// </summary>
    public class SaveButton : UnityEngine.MonoBehaviour
    {
        private GameViewModel _viewModel;

        public void Init(GameViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _viewModel.OnSaveRequested();
        }
    }
}