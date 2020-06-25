using UnityEngine.EventSystems;
using UnityEngine;
using System;

namespace ARQ.UI.TD
{
    public class PointerClickable : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnClick = delegate { };

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick();
        }
    }
}