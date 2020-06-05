using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ARQ.NeuroSym.UIKit
{
    public class ViewSwitchableImageButton : MonoBehaviour, IPointerClickHandler
    {
        #region SerializedFields
        [SerializeField]
        private GameObject[] _buttonViewPrefabs;

        #region Unity events
        [System.Serializable]
        public class myVoidEvent : UnityEvent { }
        [SerializeField]
        public myVoidEvent OnClick;
        #endregion 

        #endregion

        private GameObject[] _instatiatedView;
        private GameObject _currentView;

        public event Action OnButtonClick = delegate { };

        #region MonoBehaviour
        protected virtual void Awake()
        {
            InstantiateViews();
            SetCurrentView(0);
        }
        public virtual void CleanUp()
        {
            if (_instatiatedView == null) return;
            foreach (GameObject view in _instatiatedView)
            {
                if (view == null) continue;
                view.SetActive(false);
            }
            //_viewsInstantiated = false;
        }
        protected virtual void OnEnable()
        {

        }
        #endregion

        #region private
        protected void SetCurrentView(int i)
        {
            _currentView = _instatiatedView[i];
            _currentView.SetActive(true);
        }
        private void InstantiateViews()
        {
                _instatiatedView = new GameObject[_buttonViewPrefabs.Length];
                for (int i = 0; i < _buttonViewPrefabs.Length; i++)
                {
                    _instatiatedView[i] = Instantiate(_buttonViewPrefabs[i], transform);
                    _instatiatedView[i].SetActive(false);
                }
        }
        #endregion

        #region public
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            OnClick.Invoke();
            OnButtonClick();
        }
        public virtual void ChangeViewTo(int index)
        {
            if (_instatiatedView == null) return;
            foreach (var view in _instatiatedView)
            {
                view.SetActive(false);
            }
            SetCurrentView(index);
        } 
        #endregion
    }
}