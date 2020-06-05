
using UnityEngine;

namespace ARQ.Helpers
{

    public static class CameraExtensions
    {
        public static void FitToBounds(this Camera camera, Transform transform, float distance)
        {
            var bounds = transform.EncapsulateBounds();
            var boundRadius = bounds.extents.magnitude;
            var finalDistance = boundRadius / (2.0f * Mathf.Tan(0.5f * camera.fieldOfView * Mathf.Deg2Rad)) * distance;
            if (float.IsNaN(finalDistance))
            {
                return;
            }
            camera.farClipPlane = finalDistance * 2f;
            camera.transform.position = new Vector3(bounds.center.x, bounds.center.y, bounds.center.z + finalDistance);
            camera.transform.LookAt(bounds.center);
        }

        public static void FitToBounds(this Camera camera, Transform transform, float distance, float angle)
        {
            var bounds = transform.EncapsulateBounds();
            var boundRadius = bounds.extents.magnitude;
            var finalDistance = boundRadius / (2.0f * Mathf.Tan(0.5f * camera.fieldOfView * Mathf.Deg2Rad)) * distance;
            if (float.IsNaN(finalDistance))
            {
                return;
            }
            camera.farClipPlane = finalDistance * 2f;
            camera.transform.position = new Vector3(bounds.center.x, bounds.center.y + bounds.extents.y, bounds.center.z + finalDistance);
            camera.transform.LookAt(bounds.center);
        }

    }


}

