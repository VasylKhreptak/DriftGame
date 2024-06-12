using Sirenix.OdinInspector;
using UnityEngine;

namespace Utilities
{
    public class ColliderRemover : MonoBehaviour
    {
        [Button]
        private void Remove()
        {
            foreach (Collider collider in GetComponentsInChildren<Collider>(true))
            {
                DestroyImmediate(collider);
            }

            DestroyImmediate(this);
        }
    }
}