using UnityEngine;

namespace Gameplay.SpawnPoints.Core
{
    public class SpawnPoints : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform[] _transforms;

        public Transform this[int index] => _transforms[index];
    }
}