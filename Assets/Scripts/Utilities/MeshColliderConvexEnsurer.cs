using Sirenix.OdinInspector;
using UnityEngine;

namespace Utilities
{
    public class MeshColliderConvexEnsurer : MonoBehaviour
    {
        [Button]
        private void EnsureConvexEnabled()
        {
            foreach (MeshCollider collider in GetComponentsInChildren<MeshCollider>())
                collider.convex = true;

            DestroyImmediate(this);
        }
    }
}