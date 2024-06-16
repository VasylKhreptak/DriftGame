using UI.Garage.Buttons.Customization;
using UnityEngine;

namespace Cars.Customization
{
    public class OpenCustomizationMenuButtons : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private OpenCustomizationMenuButton[] _openCustomizationMenuButtons;

        #region MonoBehaviour

        private void OnValidate() => _openCustomizationMenuButtons = GetComponentsInChildren<OpenCustomizationMenuButton>();

        #endregion

        public void SetActive(bool enabled)
        {
            foreach (OpenCustomizationMenuButton button in _openCustomizationMenuButtons)
            {
                button.Enabled = enabled;
            }
        }
    }
}