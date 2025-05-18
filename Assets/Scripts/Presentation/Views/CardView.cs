using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.Views
{
    public enum CardState
    {
        FaceDown, FaceUp, Matched  
    }
    
    public class CardView : MonoBehaviour
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
        
        public void Init(int id, int pairId)
        {
            cardId = id;
            this.pairId = pairId;
            _text.text = TextStatus;
            _image.color = GetColorFromNumber(pairId);
        }

        public void Flip()
        {
            Debug.Log("Flip");
            // Animator.SetTrigger("Flip");
        }

        public void SetMatched()
        {
            Debug.Log("Matched");
            state = CardState.Matched;
            _text.text = TextStatus;

            // Animator.SetTrigger("Matched");
        }
        
        public void SetFaceUp()
        {
            Debug.Log("FaceUp");
            state = CardState.FaceUp;
            _text.text = TextStatus;

            // Animator.SetBool("FaceUp", true);
        }

        public void SetFaceDown()
        {
            Debug.Log("FaceDown");
            state = CardState.FaceDown;
            _text.text = TextStatus;

            // Animator.SetBool("FaceUp", false);
        }
        
        public static Color GetColorFromNumber(int number)
        {
            var rng = new System.Random(number); // Number-determined so always same for same number

            float r = (float)rng.NextDouble();
            float g = (float)rng.NextDouble();
            float b = (float)rng.NextDouble();

            return new Color(r, g, b);
        }
    }
}