using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Utilities
{
    public class Touchpad : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private UIBehaviour _uiBehaviour;

        public UIBehaviour UIBehaviour => _uiBehaviour;

        #region MonoBehaviour

        private void OnValidate() => _uiBehaviour ??= GetComponent<UIBehaviour>();

        #endregion
    }
}