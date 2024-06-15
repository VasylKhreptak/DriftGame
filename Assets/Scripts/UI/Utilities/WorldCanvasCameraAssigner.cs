using UnityEngine;
using Zenject;

namespace UI.Utilities
{
    public class WorldCanvasCameraAssigner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Canvas _canvas;

        private Camera _camera;

        [Inject]
        private void Constructor([InjectOptional] Camera camera)
        {
            _camera = camera;
        }

        #region MonoBehaviour

        private void OnValidate() => _canvas ??= GetComponent<Canvas>();

        private void Awake() => _canvas.worldCamera = _camera;

        #endregion
    }
}