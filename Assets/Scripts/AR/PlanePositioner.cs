using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using ARQ.Helpers.Heirarchy;

namespace ARQ.AR.Positioning
{
    [RequireComponent(typeof(ARRaycastManager))]
    [RequireComponent(typeof(ARPlaneManager))]
    public class PlanePositioner : MonoBehaviour, IPositioner
    {
        [SerializeField]
        private GameObject _defaultPrefab = null;
        private GameObject _prefabToInstantiate;

        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private PlanePositioningSettings _defaultPlacingSettings;

        private PlanePositioningOptions _positioningOptions;

        private GameObject _placementIndicator;

        private ARRaycastManager _reycastManager;

        private ARPlaneManager _planeManager;

        //private List<Pose> _previousPositions = new List<Pose>();

        private Pose _placementPose;

        private bool _isObjectPositioned = false;

        private bool _positionIsValid = false;

        private bool _placingStarted = false;

        private Vector3 _screenCenter { get { return _camera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f)); } }

        public Action<GameObject> Instantiated;

        #region MonoBehaviour

        private void Awake()
        {
            _reycastManager = GetComponent<ARRaycastManager>();
            _planeManager = GetComponent<ARPlaneManager>();

        }

        private void OnEnable()
        {
            if (_defaultPlacingSettings.StartAtEnabling) StartPositioning(_defaultPrefab);
        }

        private void Update()
        {
            if (_isObjectPositioned) return;
            if (_placementIndicator == null) return;

            if (_planeManager.trackables.count < 1)
            {
                _placementIndicator.SetActive(false);
                return;
            }

            UpdatePlacementPose();
            UpdateIndicatorPose();

            if (_positionIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (!EventSystemHelper.IsPointerOverUIObject())
                    PositionObject();
            }
        }

        #endregion

        #region IPositioner

        public event Action<EventArgs> ObjectPositioned;

        public void CancelPositioning()
        {
            _placementIndicator.SetActive(false);

            Destroy(_placementIndicator);

            _isObjectPositioned = true;
            _placingStarted = false;

        }

        public void StartPositioning(GameObject prefabToInstantiate, IPositioningOptions options = null)
        {
            if (_placingStarted) return;

            _positioningOptions = options as PlanePositioningOptions;

            if (_positioningOptions == null)
            {
                _positioningOptions = new PlanePositioningOptions(_defaultPlacingSettings);
            }
            else
            {
                if (_positioningOptions.PlacementIndicator == null)
                {
                    _positioningOptions.PlacementIndicator = _defaultPlacingSettings.PlacementIndicator;
                }
                if (_positioningOptions.HightlightMaterial == null)
                {
                    _positioningOptions.HightlightMaterial = _defaultPlacingSettings.HightLightMaterial;
                }
            }

            _prefabToInstantiate = prefabToInstantiate != null ? prefabToInstantiate : _defaultPrefab;

            ConfigurateIndicator();

            _isObjectPositioned = false;
            _placingStarted = true;

            gameObject.SetActive(true);
        }

        #endregion

        #region Methods

        public void PositionObject()
        {
            var cameraForward = _camera.transform.forward;
            var cameraBering = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            _placementPose.rotation = Quaternion.LookRotation(cameraBering);

            if (_prefabToInstantiate != null)
            {
                switch (_positioningOptions.PlacingType)
                {
                    case PlacingType.activate:
                        _prefabToInstantiate.transform.position = _placementPose.position;
                        _prefabToInstantiate.transform.rotation = _placementPose.rotation;
                        _prefabToInstantiate.SetActive(true);
                        Instantiated?.Invoke(_prefabToInstantiate);
                        break;
                    case PlacingType.instantiate:
                        var prefab = Instantiate(_prefabToInstantiate, _placementPose.position, _placementPose.rotation);
                        //prefab.transform.rotation = _placementPose.rotation;
                        prefab.transform.eulerAngles = _placementPose.rotation.eulerAngles;
                        prefab.SetActive(true);
                        Instantiated?.Invoke(prefab);
                        break;
                }
            }

            _placementIndicator.SetActive(false);

            Destroy(_placementIndicator);

            _isObjectPositioned = true;
            _placingStarted = false;

            EventArgs args = new EventArgs();
            ObjectPositioned?.Invoke(args);
        }

        #endregion


        private void ConfigurateIndicator()
        {
            switch (_positioningOptions.IndicatorType)
            {
                case IndicatorType.onlyIndicator:
                    _placementIndicator = Instantiate(_positioningOptions.PlacementIndicator);
                    _placementIndicator.transform.SetParent(transform);
                    break;
                case IndicatorType.prefabAndIndicator:
                    var indicator = Instantiate(_prefabToInstantiate);
                    Heirarchy.PassToHeirarchyEnd(indicator, PreparePrefabCopy);
                    indicator.transform.localPosition = new Vector3(0f, 0f, 0f);
                    _placementIndicator = Instantiate(_positioningOptions.PlacementIndicator);
                    _placementIndicator.transform.SetParent(transform);
                    indicator.transform.SetParent(_placementIndicator.transform);
                    break;
                case IndicatorType.prefabAsIndicator:
                    _placementIndicator = new GameObject();
                    _placementIndicator.transform.localPosition = new Vector3(0f, 0f, 0f);
                    var copy = Instantiate(_prefabToInstantiate, _placementIndicator.transform);
                    copy.transform.localPosition = new Vector3(0f, 0f, 0f);
                    Heirarchy.PassToHeirarchyEnd(_placementIndicator, PreparePrefabCopy);
                    break;
            }
        }

        private void UpdatePlacementPose()
        {
            var hits = new List<ARRaycastHit>();
            _reycastManager.Raycast(_screenCenter, hits, TrackableType.PlaneWithinPolygon | TrackableType.FeaturePoint);
            _positionIsValid = hits.Count > 0;
            if (!_positionIsValid) return;
            //_previousPositions.Add(hits[0].pose);
            //if (_previousPositions.Count > 10) _previousPositions.RemoveRange(0, _previousPositions.Count - 10);
            //foreach (Pose pose in _previousPositions)
            //{
            //    _placementPose.position += pose.position;
            //}
            //_placementPose.position *= 0.1f;
            _placementPose.position = hits[0].pose.position;
        }

        private void UpdateIndicatorPose()
        {
            if (!_positionIsValid)
            {
                _placementIndicator.SetActive(false);
                return;
            }
            _placementIndicator.SetActive(true);
            _placementIndicator.transform.SetPositionAndRotation(_placementPose.position, _placementPose.rotation);
            var cameraForward = _camera.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            _placementIndicator.transform.rotation = Quaternion.LookRotation(cameraBearing);
        }

        private void PreparePrefabCopy(GameObject go)
        {
            Component[] comps = go.GetComponents(typeof(Component));
            go.SetActive(true);
            foreach (var comp in comps)
            {
                if (!(comp is MeshFilter) && !(comp is MeshRenderer) && !(comp is Transform))
                {
                    Destroy(comp);
                }
                if (_defaultPlacingSettings.HightLightMaterial == null) continue;
                var rend = comp as MeshRenderer;
                if (rend != null)
                {
                    rend.material = _defaultPlacingSettings.HightLightMaterial;
                }
            }
        }
    }
}
