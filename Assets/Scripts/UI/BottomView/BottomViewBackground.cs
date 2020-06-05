using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ARQ.UIKit
{
    [RequireComponent(typeof(Image))]
    public class BottomViewBackground : MonoBehaviour
    {
        [SerializeField]
        private Color _shownColor;
        [SerializeField]
        private Color _hidenColor;

        private BottomView _bottomView;

        private void Awake()
        {
            _bottomView = GetComponentInChildren<BottomView>();
            _bottomView.OnHalfShowSmooth += HandleHalfShowingShooth;
            _bottomView.OnHideSmooth += HandleHidingSmooth;
            _bottomView.OnHalfShow += HandleShowing;
            _bottomView.OnHide += HandleHiding;
        }

        private void HandleHalfShowingShooth()
        {
            GetComponent<Image>().DOColor(_shownColor, 0.3f);
        }
        private void HandleShowing()
        {
            GetComponent<Image>().color = _shownColor;
        }
        private void HandleHidingSmooth()
        {
            GetComponent<Image>().DOColor(_hidenColor, 0.3f);
        }
        private void HandleHiding()
        {
            GetComponent<Image>().color = _hidenColor;
        }

    }
}