using UnityEngine;
using UnityEngine.UI;

namespace ARQ.UI
{
    public class ScrollViewDisabler : MonoBehaviour
    {
        private BottomViewRB _bottomView;
        [SerializeField]
        private ScrollRect _scrollRect;

        private void Awake()
        {
            _bottomView = GetComponentInParent<BottomViewRB>();
            _bottomView.OnExpand += HandleExpand;
            _bottomView.OnHide += HandleHiding;
        }

        private void HandleHiding()
        {
            _scrollRect.verticalScrollbar.value = 1f;
            _scrollRect.verticalScrollbar.onValueChanged.Invoke(1f);
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