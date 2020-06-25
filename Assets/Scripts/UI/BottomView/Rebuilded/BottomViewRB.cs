using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;
using ARQ.UI.Utils;
using UnityEngine.UI;

namespace ARQ.UI
{
    public enum BottomViewState
    {
        Hiden,
        Shown,
        Expanded
    }

    public class BottomViewRB : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private int _viewID;
        [SerializeField]
        private RectTransform _rootTransform;
        [SerializeField]
        private float _expandedRatio;
        [SerializeField]
        private float _shownRatio;
        [SerializeField]
        private float _hidenRatio;
        [SerializeField]
        private Button[] _relatedButtons;
        [SerializeField]
        private bool _useHalfPosition = true;

        private Vector2 _localPoint = new Vector2();
        private Vector2 _shownPosition;
        private Vector2 _expandedPosition;
        private Vector2 _hidenPosition;

        public BottomViewState _viewState = BottomViewState.Hiden;

        private RectTransform _rectTransform;

        public int ViewID { get => _viewID; }

        #region Events
        public event Action OnHalfShowSmooth = delegate { };
        public event Action OnExpandSmooth = delegate { };
        public event Action OnHideSmooth = delegate { };
        public event Action OnHalfShow = delegate { };
        public event Action OnExpand = delegate { };
        public event Action OnHide = delegate { };
        #endregion

        #region MonoBeahaviour
        private void Awake()
        {
            FindObjectOfType<OrientationController>().OnDeviceOrientationHasChanged += OrientationChangedHandler;
            _rectTransform = GetComponent<RectTransform>();
            OnExpand += ExpandedHandler;
            OnHalfShow += ShownHandler;
            OnHide += HidedHandled;
        }
        private void Start()
        {
            Debug.Log(_rootTransform.rect);
            SetCurrentPosition();
        }
        #endregion

        #region Moving and positioning
        private void HidedHandled()
        {
            _viewState = BottomViewState.Hiden;
        }
        private void ShownHandler()
        {
            _viewState = BottomViewState.Shown;
        }
        private void ExpandedHandler()
        {
            _viewState = BottomViewState.Expanded;
        }
        private void OrientationChangedHandler(DeviceOrientation obj)
        {
            SetCurrentPosition();
        }
        private void SetCurrentPosition()
        {
            switch (_viewState)
            {
                case BottomViewState.Expanded:
                    Expand();
                    break;
                case BottomViewState.Hiden:
                    Hide();
                    break;
                case BottomViewState.Shown:
                    HalfShow();
                    break;
            }
        }
        public void HalfShowSmooth()
        {
            _shownPosition = new Vector2(0, _rootTransform.rect.height * _shownRatio);

            if (_useHalfPosition)
            {
                _rectTransform.DOAnchorPos(_shownPosition, 0.3f).SetEase(Ease.InOutCubic).OnComplete(() =>
                {
                    OnHalfShow();
                });
                OnHalfShowSmooth();
            }
        }
        public void ExpandSmooth()
        {
            _expandedPosition = new Vector2(0, _rootTransform.rect.height * _expandedRatio);
            _rectTransform.DOAnchorPos(_expandedPosition, 0.3f).SetEase(Ease.InOutCubic).OnComplete(() =>
            {
                OnExpand();
            });
            OnExpandSmooth();
        }
        public void HideSmooth()
        {
            _hidenPosition = new Vector2(0, _rootTransform.rect.height * _hidenRatio);
            _rectTransform.DOAnchorPos(_hidenPosition, 0.3f).SetEase(Ease.InOutCubic).OnComplete(() =>
            {
                OnHide();
            });
            OnHideSmooth();
        }
        public void HalfShow()
        {
            _shownPosition = new Vector2(0, _rootTransform.rect.height * _shownRatio);
            if (_useHalfPosition)
            {
                _rectTransform.anchoredPosition = _shownPosition;
                OnHalfShow();
            }
        }
        public void Hide()
        {
            _hidenPosition = new Vector2(0, _rootTransform.rect.height * _hidenRatio);
            _rectTransform.anchoredPosition = _hidenPosition;
            OnHide();
        }
        public void Expand()
        {
            _expandedPosition = new Vector2(0, _rootTransform.rect.height * _expandedRatio);
            _rectTransform.anchoredPosition = _expandedPosition;
            OnExpand();
        }
        #endregion

        public void OnBeginDrag(PointerEventData eventData)
        {
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_useHalfPosition && eventData.delta.y > 0)
            {
                return;
            }
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rootTransform,
                                                                    eventData.delta,
                                                                    null,
                                                                    out _localPoint);
            _rectTransform.anchoredPosition += new Vector2(0, _localPoint.y);
            if(_rectTransform.anchoredPosition.y > 0)
            {
                _rectTransform.anchoredPosition = new Vector2(0, 0);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _shownPosition = new Vector2(0, _rootTransform.rect.height * _shownRatio);
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
                if (_useHalfPosition)
                {
                    HalfShowSmooth();
                }
            }
        }

    }
}