using UnityEngine;
using UnityEngine.UI;

namespace ARQ.NeuroSym.UIKit
{
    public class PopupBackground : MonoBehaviour, IPopupStateSwitchable
    {
        [SerializeField]
        private Image _background;
        [SerializeField]
        private Color _highlighted;
        [SerializeField]
        private Color _notHighlighted;

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
            _background.color = _highlighted;
        }
        private void SetStateNotHighlighted()
        {
            _background.color = _notHighlighted;
        }

    }
}