using UnityEngine;
using UnityEngine.UI;

namespace ARQ.NeuroSym.UIKit
{
    public class PopupText : MonoBehaviour, IPopupStateSwitchable
    {
        [SerializeField]
        private Text _changingText;
        [SerializeField]
        private Text _staticText;
        [SerializeField]
        private string _highlightedText;
        [SerializeField]
        private string _notHighlightedText;
        [SerializeField]
        private Color _highlightedColor;
        [SerializeField]
        private Color _notHighlightedColor;
        [SerializeField]
        private Color _highlightedTextColor;
        [SerializeField]
        private Color _notHighlightedTextColor;


        public void SwitchState(PopupState state)
        {
            if (state == PopupState.Highlighted)
            {
                SetStateHighlighted();
            }
            else if (state == PopupState.NotHighlighted)
            {
                SetStateNotHighlighted();
            }
        }

        private void SetStateHighlighted()
        {
            _changingText.text = _highlightedText;
            _changingText.color = _highlightedTextColor;
            _staticText.color = _highlightedColor;
        }
        private void SetStateNotHighlighted()
        {
            _changingText.text = _notHighlightedText;
            _changingText.color = _notHighlightedTextColor;
            _staticText.color = _notHighlightedColor;
        }
    }
}