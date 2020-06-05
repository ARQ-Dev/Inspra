using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;
using UnityEngine.UI;

namespace ARQ.UIKit
{
    public class BottomView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private RectTransform _rootRectTransform;
        [SerializeField]
        private Vector2 _shownPosition;
        [SerializeField]
        private Vector2 _expandedPosition;
        [SerializeField]
        private Vector2 _hidenPosition;
        [SerializeField]
        private Button[] _relatedButtons;

        private RectTransform _rectTransform;
        private Vector2 _localPoint = new Vector2();

        public event Action OnHalfShowSmooth = delegate { };
        public event Action OnExpandSmooth = delegate { };
        public event Action OnHideSmooth = delegate { };
        public event Action OnHalfShow = delegate { };
        public event Action OnExpand = delegate { };
        public event Action OnExpanded = delegate { };
        public event Action OnHide = delegate { };

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();

        }

        public void HalfShowSmooth()
        {
            _rectTransform.DOAnchorPos(_shownPosition, 0.3f).SetEase(Ease.InOutCubic);
            OnHalfShowSmooth();
        }

        public void ExpandSmooth()
        {
            _rectTransform.DOAnchorPos(_expandedPosition, 0.3f).SetEase(Ease.InOutCubic).OnComplete(()=>
            {
                OnExpanded();
            });
            OnExpandSmooth();
        }

        public void HideSmooth()
        {
            _rectTransform.DOAnchorPos(_hidenPosition, 0.3f).SetEase(Ease.InOutCubic);
            OnHideSmooth();
        }

        public void HalfShow()
        {
            _rectTransform.anchoredPosition = _shownPosition;
            OnHalfShow();
        }

        public void Hide()
        {
            _rectTransform.anchoredPosition = _hidenPosition;
            OnHide();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {

        }

        public void OnDrag(PointerEventData eventData)
        {

            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rootRectTransform,
                                                                    eventData.delta,
                                                                    null,
                                                                    out _localPoint);
            _rectTransform.anchoredPosition += new Vector2(0, _localPoint.y);
            
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var y = _rectTransform.anchoredPosition.y;
            if (y < (_shownPosition.y - 300))
            {
                //HideSmooth();
                foreach (var but in _relatedButtons)
                {
                    but.onClick.Invoke();
                }
            }
            else if (y > (_shownPosition.y + 100))
            {
                ExpandSmooth();
            }
            else
            {
                HalfShowSmooth();
            }
        }
    }
}