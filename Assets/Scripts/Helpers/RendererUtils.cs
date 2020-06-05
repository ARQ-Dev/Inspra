using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace ARQ.Helpers
{
    public class RendererUtils
    {
        public static Bounds GetBounds(GameObject obj)
        {
            Bounds bounds;
            var transform = obj.transform;
            var renderers = transform.GetComponentsInChildren<Renderer>();
            if (renderers != null && renderers.Length > 0)
            {
                bounds = renderers[0].bounds;
                for (var i = 1; i < renderers.Length; i++)
                {
                    var renderer = renderers[i];
                    bounds.Encapsulate(renderer.bounds);
                }
            }
            else
            {
                bounds = new Bounds();
            };
            return bounds;
        }

        public static Bounds GetMaxVolumeBounds(GameObject obj)
        {
            Bounds bounds;
            var transform = obj.transform;
            var renderers = transform.GetComponentsInChildren<Renderer>();
            if (renderers != null && renderers.Length > 0)
            {
                bounds = renderers[0].bounds;
                var volume = bounds.size.x * bounds.size.y * bounds.size.z;
                for (var i = 1; i < renderers.Length; i++)
                {
                    var renderer = renderers[i];
                    var currentVolume = renderer.bounds.size.x * renderer.bounds.size.y * renderer.bounds.size.z;
                    if (currentVolume > volume)
                    {
                        volume = currentVolume;
                        bounds = renderer.bounds;
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


