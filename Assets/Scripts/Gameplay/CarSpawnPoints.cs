using UnityEngine;

namespace Gameplay
{
    public class CarSpawnPoints : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform[] _transforms;

        public Transform this[int index] => _transforms[index];
    }
}