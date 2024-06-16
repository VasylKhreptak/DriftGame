using TMPro;
using UnityEngine;

namespace UI.InputFields.Core
{
    public class BaseInputField : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_InputField _inputField;

        public bool IsEmpty => string.IsNullOrEmpty(_inputField.text);

        public string Text
        {
            get => _inputField.text;
            set => _inputField.text = value;
        }

        #region MonoBehaviour

        private void OnValidate()
        {
            _inputField ??= GetComponent<TMP_InputField>();
        }

        #endregion
    }
}