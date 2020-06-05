using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ARQ.NeuroSym.UIKit {
    public class ActivateButton : ViewSwitchableImageButton
    {
        [SerializeField]
        private bool _activatedAtStart = false;

        private bool _activated; // 0 - activated, 1 - deactivated
        public bool Activated { get => _activated;}

        public event Action<bool> OnActivateButtonClick = delegate { };

        protected override void Awake()
        {
            base.Awake();
            if (_activatedAtStart)
            {
                ChangeViewTo(0);
                _activated = true;
            }
            else
            {
                ChangeViewTo(1);
                _activated = false;
            }
        }
        public override void CleanUp()
        {
            base.CleanUp();
            SetStateActivated();
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            if (_activated)
            {
                SetStateDeactivated();
            }
            else
            {
                SetStateActivated();
            }
            OnActivateButtonClick(_activated);
        }
        public void SetState(bool state)
        {
            if (state)
            {
                SetStateActivated();
            }
            else
            {
                SetStateDeactivated();
            }
        }
        private void SetStateActivated()
        {
            ChangeViewTo(0);
            _activated = true;
        }
        private void SetStateDeactivated()
        {
            ChangeViewTo(1);
            _activated = false;
        }
    }
}