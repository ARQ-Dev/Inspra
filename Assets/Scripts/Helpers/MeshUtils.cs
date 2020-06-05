using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARQ.Helpers
{

    public class MeshUtils
    {
        /// <summary>
        /// I don't know how it works!!!!!!!!
        /// </summary>
        public static Bounds GetBounds(GameObject go)
        {
            MeshFilter[] meshFilters = go.transform.GetComponentsInChildren<MeshFilter>();

            Bounds bounds;

            if (meshFilters != null && meshFilters.Length > 0)
            {
                bounds = meshFilters[0].mesh.bounds;
                for (var i = 1; i < meshFilters.Length; i++)
                {
                    bounds.Encapsulate(meshFilters[0].mesh.bounds);
                }
            }
            else
            {
                bounds = new Bounds();
            };
            return bounds;
        }
        /// <summary>
        /// I don't know how it works!!!!!!!!
        /// </summary>
        public static Bounds GetMaxVolumeBounds(GameObject go)
        {
            MeshFilter[] meshFilters = go.transform.GetComponentsInChildren<MeshFilter>();

            Bounds bounds;

            if (meshFilters != null && meshFilters.Length > 0)
            {
                bounds = meshFilters[0].mesh.bounds;
                var volume = bounds.size.x * bounds.size.y * bounds.size.z;
                for (var i = 1; i < meshFilters.Length; i++)
                {
                    var mesh = meshFilters[i].mesh;
                    var currentVolume = mesh.bounds.size.x * mesh.bounds.size.y * mesh.bounds.size.z;
                    if (currentVolume > volume)
                    {
                        volume = currentVolume;
                        bounds = mesh.bounds;
                    }
                }
            }
            else
            {
                bounds = new Bounds();
            };
            return bounds;
        }

    }
}

