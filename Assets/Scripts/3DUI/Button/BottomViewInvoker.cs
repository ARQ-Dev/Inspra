using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARQ.UIKit {
    public class BottomViewInvoker : MonoBehaviour
    {
        [SerializeField]
        private int _assotiatedViewID;

        private BottomView _assotiatedBottomView;

        private void Awake()
        {
            var views = FindObjectsOfType<BottomView>();
            foreach (var view in views)
            {
                if (view.ViewID == _assotiatedViewID)
                {
                    _assotiatedBottomView = view;
                }
            }
        }

        private bool ChechBottomView()
        {
            if (_assotiatedBottomView == null)
            {
                Debug.Log("BottomView with such ID not found");
                return false;
            }
            else
            {
                return true;
            }

        }

        public void InvokeExpandSmooth()
        {
            if (ChechBottomView())
            {
                _assotiatedBottomView.ExpandSmooth();
            }
        }

        public void InvokeHideSmooth()
        {
            if (ChechBottomView())
            {
                _assotiatedBottomView.HideSmooth();
            }
        }
        public void InvokeHalfShowSmooth()
        {
            if (ChechBottomView())
            {
                _assotiatedBottomView.HalfShowSmooth();
            }
        }
    }
}