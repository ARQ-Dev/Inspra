using UnityEngine;

namespace ARQ.UIKit.TD
{
    public class SpatialTextMesh : MonoBehaviour
    {
        public Vector3 GetGetMeshSize()
        {
            return GetComponent<MeshRenderer>().bounds.size;
        }
    }
}