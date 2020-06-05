using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARQ.AR.Positioning
{
    public class PlanePositioningOptions : IPositioningOptions
    {
        public GameObject[] ObjectsToActivateFirst { get; set; }

        public IndicatorType IndicatorType { get; set; }

        public PlacingType PlacingType { get; set; }

        public GameObject PlacementIndicator { get; set; }

        public Material HightlightMaterial { get; set; }

        public PlanePositioningOptions(PlanePositioningSettings settings, GameObject[] gameObjects = null)
        {
            ObjectsToActivateFirst = gameObjects;
            IndicatorType = settings.IndicatorType;
            PlacingType = settings.PlacingType;
            PlacementIndicator = settings.PlacementIndicator;
            HightlightMaterial = settings.HightLightMaterial;
        }

        public PlanePositioningOptions(
            GameObject[] gameObjects = null,
            IndicatorType indicatorType = IndicatorType.onlyIndicator,
            PlacingType placingType = PlacingType.activate,
            GameObject placementIndicator = null,
            Material highlightMaterial = null
            )
        {
            ObjectsToActivateFirst = gameObjects;
            IndicatorType = indicatorType;
            PlacingType = placingType;
            PlacementIndicator = placementIndicator;
            HightlightMaterial = highlightMaterial; 
        }

    }
}

