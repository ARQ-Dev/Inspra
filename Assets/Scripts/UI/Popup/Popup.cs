using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace ARQ.NeuroSym.UIKit
{
    [RequireComponent(typeof(RectTransform))]
    public class Popup : MonoBehaviour
    {
        [SerializeField]
        private float _toastDuration;

        [SerializeField]
        private float _moveDuration;

        [SerializeField]
        private GameObject[] _popups;

        [SerializeField]
        private Vector2 _shownPosition;

        [SerializeField]
        private Vector2 _hidenPosition;

        private RectTransform _rectTransfrom;

        private GameObject _currentPopup;

        private void Awake()
        {
            _rectTransfrom = GetComponent<RectTransform>();
        }

        public void ShowPopupByIndex(int index)
        {
            if(_currentPopup != null)
            {
                Destroy(_currentPopup);
                _currentPopup = null;
            }
            _currentPopup = Instantiate(_popups[index], transform);
            ShowPopup();
        }

        private void ShowPopup()
        {
            StopAllCoroutines();
            _rectTransfrom.DOAnchorPos(_shownPosition, _moveDuration).SetEase(Ease.OutExpo);

        }

        public void HideActivePopup()
        {

            StartCoroutine(StartCountdownCoroutine());
        }

        private IEnumerator StartCountdownCoroutine()
        {
            yield return new WaitForSecondsRealtime(_toastDuration);
            _rectTransfrom.DOAnchorPos(_hidenPosition, _moveDuration).SetEase(Ease.OutExpo);
        }

    }
}