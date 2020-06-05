using UnityEngine;
using UnityEngine.UI;

namespace ARQ.NeuroSym.UIKit
{
    public class PopupIcon : MonoBehaviour, IPopupStateSwitchable
    {
        [SerializeField]
        private Image _sunIcon;
        [SerializeField]
        private Image _line;
        [SerializeField]
        private Color _highlightedColor;
        [SerializeField]
        private Color _notHighlightedColor;

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
            _sunIcon.color = _highlightedColor;
            _line.gameObject.SetActive(false);
        }
        private void SetStateNotHighlighted()
        {
            _sunIcon.color = _notHighlightedColor;
            _line.gameObject.SetActive(true);
        }
    }
}