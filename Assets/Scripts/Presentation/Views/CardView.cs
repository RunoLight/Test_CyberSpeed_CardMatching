using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.Views
{
    public class CardView : UnityEngine.MonoBehaviour
    {
        public event Action<int> OnFaceUpAnimationFinished;

        [SerializeField] private Button button;
        [SerializeField] private TMP_Text text;
        [SerializeField] private Image image;
        [SerializeField] private Animator animator;

        private string TextStatus => $"id:{_cardId}\npairId:{_pairId}\nstate:\n{_state}";

        private int _cardId;
        private int _pairId;
        private CardState _state;
        private Action<int> _onClicked;

        private static readonly int FlipFaceUp = Animator.StringToHash("FlipFaceUp");
        private static readonly int FlipFaceUpInstant = Animator.StringToHash("FlipFaceUpInstant");
        private static readonly int FlipFaceDown = Animator.StringToHash("FlipFaceDown");
        private static readonly int FlipFaceDownInstant = Animator.StringToHash("FlipFaceDownInstant");
        private static readonly int Match = Animator.StringToHash("Match");
        private static readonly int MatchInstant = Animator.StringToHash("MatchInstant");

        public CardView Init(int id, int pId, Action<int> clickAction)
        {
            _cardId = id;
            _pairId = pId;
            _onClicked = clickAction;

            text.text = TextStatus;
            image.color = GetColorFromNumber(pId);

            return this;
        }

        public void SetMatched(bool noAnimation = false)
        {
            _state = CardState.Matched;
            text.text = TextStatus;

            animator.SetTrigger(noAnimation ? MatchInstant : Match);
        }

        public void SetFaceUp(bool noAnimation = false)
        {
            _state = CardState.FaceUp;
            text.text = TextStatus;

            animator.SetTrigger(noAnimation ? FlipFaceUpInstant : FlipFaceUp);
        }

        public void SetFaceDown(bool noAnimation = false)
        {
            _state = CardState.FaceDown;
            text.text = TextStatus;

            animator.SetTrigger(noAnimation ? FlipFaceDownInstant : FlipFaceDown);
        }

        [UsedImplicitly]
        private void FaceUpAnimationFinished()
        {
            OnFaceUpAnimationFinished?.Invoke(_cardId);
        }

        private void Awake()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _onClicked?.Invoke(_cardId);
        }

        private static Color GetColorFromNumber(int number)
        {
            var rng = new System.Random(number); // Number-determined so always same for same number

            float r = (float)rng.NextDouble();
            float g = (float)rng.NextDouble();
            float b = (float)rng.NextDouble();

            return new Color(r, g, b);
        }

        private enum CardState
        {
            FaceDown,
            FaceUp,
            Matched
        }
    }
}