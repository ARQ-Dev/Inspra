using UnityEngine.EventSystems;
using UnityEngine;
using System;

namespace ARQ.UIKit.TD
{
    public class PointerClickable : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnClick = delegate { };

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("here");
            OnClick();
        }
    }
}