using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARQ.AR.Positioning
{
    public enum IndicatorType
    {
        onlyIndicator,
        prefabAsIndicator,
        prefabAndIndicator
    }

    public enum PlacingType
    {
        instantiate,
        activate
    }

    [CreateAssetMenu(fileName = "Plane Placing Settings", menuName = "Plane Placing Settings", order = 51)]
    public class PlanePositioningSettings : ScriptableObject
    {
        [SerializeField]
        private IndicatorType _defaultIndicatorType;

        [SerializeField]
        private PlacingType _defaultPlacingType;

        [SerializeField]
        private GameObject _defaultPlacementIndicator;

        [SerializeField]
        private Material _defaultHightlightMaterial;

        [SerializeField]
        private bool _startAtEnabling = false;

        public IndicatorType IndicatorType
        {
            get { return _defaultIndicatorType; }
        }

        public PlacingType PlacingType
        {
            get { return _defaultPlacingType; }
        }

        public GameObject PlacementIndicator
        {
            get { return _defaultPlacementIndicator; }
        }

        public Material HightLightMaterial
        {
            get { return _defaultHightlightMaterial; }
        }

        public bool StartAtEnabling
        {
            get { return _startAtEnabling; }
        }

    }
}
