using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.Views
{
    public enum CardState
    {
        FaceDown,
        FaceUp,
        Matched
    }

    public class CardView : UnityEngine.MonoBehaviour
    {
        public int cardId;
        public int pairId;

        public CardState state;

        // public Animator Animator;
        public System.Action<int> OnClicked;

        private Button _button;
        private TMP_Text _text;
        private Image _image;

        private string TextStatus => $"id:{cardId}\npairId:{pairId}\nstate:\n{state}";

        private void Awake()
        {
            _button = GetComponent<Button>();
            _text = GetComponentInChildren<TMP_Text>();
            _image = GetComponentInChildren<Image>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            OnClicked?.Invoke(cardId);
        }

        public CardView Init(int id, int pId,  Action<int> clickAction)
        {
            cardId = id;
            pairId = pId;
            OnClicked = clickAction;

            _text.text = TextStatus;
            _image.color = GetColorFromNumber(pId);

            return this;
        }

        public void Flip()
        {
            Debug.Log("Flip");
            // Animator.SetTrigger("Flip");
        }

        public void SetMatched()
        {
            state = CardState.Matched;
            _text.text = TextStatus;

            // Animator.SetTrigger("Matched");
        }

        public void SetFaceUp()
        {
            // Debug.Log("FaceUp");
            state = CardState.FaceUp;
            _text.text = TextStatus;

            // Animator.SetBool("FaceUp", true);
        }

        public void SetFaceDown()
        {
            // Debug.Log("FaceDown");
            state = CardState.FaceDown;
            _text.text = TextStatus;

            // Animator.SetBool("FaceUp", false);
        }

        private static Color GetColorFromNumber(int number)
        {
            var rng = new System.Random(number); // Number-determined so always same for same number

            float r = (float)rng.NextDouble();
            float g = (float)rng.NextDouble();
            float b = (float)rng.NextDouble();

            return new Color(r, g, b);
        }
    }
}