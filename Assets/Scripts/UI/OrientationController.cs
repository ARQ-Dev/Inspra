using System;
using System.Collections;
using UnityEngine;

namespace ARQ.UI.Utils
{
    public class OrientationController : MonoBehaviour
    {
        #region Private fields
        #region Serialized
        [SerializeField]
        private bool _allowLandscape = false;
        #endregion

        private bool _cheсker = true;
        private DeviceOrientation _orientation;
        #endregion

        #region Public properties
        public DeviceOrientation Orientation { get => _orientation; }
        #endregion

        #region Public events
        public event Action<DeviceOrientation> OnDeviceOrientationHasChanged = delegate { };
        #endregion

        #region MonoBehavior
        private void Awake()
        {
            if (_allowLandscape)
            {
                Screen.orientation = ScreenOrientation.AutoRotation;
                StartCoroutine(StartObservingOrientation());
            }
            else
            {
                Screen.orientation = ScreenOrientation.Portrait;
            }

        }
        #endregion

        #region Private mthods
        private void CheckOrientation()
        {
            if (_allowLandscape)
            {
                if (Screen.width > Screen.height)
                {
                    if (_cheсker)
                    {
                        _orientation = DeviceOrientation.LandscapeLeft;
                        OnDeviceOrientationHasChanged(_orientation);
                        Debug.Log(this + "  ::: Device orientation has changed to " + DeviceOrientation.LandscapeLeft);
                    }
                    _cheсker = false;
                }
                else
                {
                    if (!_cheсker)
                    {
                        _orientation = DeviceOrientation.Portrait;
                        OnDeviceOrientationHasChanged(_orientation);
                        Debug.Log(this + "  ::: Device orientation has changed to " + DeviceOrientation.Portrait);
                    }
                    _cheсker = true;
                }
            }
        }
        private void OnDestroy()
        {
            StopAllCoroutines();
        }
        #endregion

        #region Coroutines
        private IEnumerator StartObservingOrientation()
        {
            while (true)
            {
                CheckOrientation();
                yield return new WaitForSecondsRealtime(0.1f);
            }
        }
        #endregion
    }
}