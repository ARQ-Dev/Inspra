using UnityEngine;
using UnityEngine.UI;

namespace ARQ.UIKit
{
    public class ScrollViewDisabler : MonoBehaviour
    {
        private BottomView _bottomView;
        [SerializeField]
        private ScrollRect _scrollRect;

        private void Awake()
        {
            _bottomView = GetComponentInParent<BottomView>();
            _bottomView.OnExpanded += HandleExpand;
        }

        private void HandleExpand()
        {
            _scrollRect.enabled = true;
        }

        public void HandleValueChanged(Vector2 value)
        {
            if (value.y > 0.999)
            {
                _scrollRect.enabled = false;
            }
        }
    }
}