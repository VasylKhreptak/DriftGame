using UnityEngine;

namespace Map.SpawnPoints.Core
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private Transform _point;

        public Transform Point => _point;

        #region MonoBehaviour

        private void OnValidate() => _point ??= GetComponent<Transform>();

        #endregion
    }
}