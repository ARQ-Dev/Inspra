using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace ARQ.Helpers
{
    public static class TransformExtensions
    {

        public static Bounds EncapsulateBounds(this Transform transform)
        {
            Bounds bounds;
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
            }
            return bounds;
        }

        public static Transform FindDeepChild(this Transform transform, string name, bool endsWith = false)
        {
            if (endsWith ? transform.name == name : transform.name.EndsWith(name))
            {
                return transform;
            }
            foreach (Transform child in transform)
            {
                var result = child.FindDeepChild(name);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }
        public static void DestroyChildren(this Transform transform, bool destroyImmediate = false)
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                var child = transform.GetChild(i);
                if (destroyImmediate)
                {
                    Object.DestroyImmediate(child.gameObject);
                }
                else
                {
                    Object.Destroy(child.gameObject);
                }
            }
        }

    }
}



