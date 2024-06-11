using UI.Windows;
using UnityEngine;
using Zenject;

namespace Garage
{
    public class GarageUIInstaller : MonoInstaller
    {
        [Header("References")]
        [SerializeField] private SettingsWindow _settingsWindow;

        #region MonoBehaviour

        private void OnValidate()
        {
            _settingsWindow ??= FindObjectOfType<SettingsWindow>(true);
        }

        #endregion

        public override void InstallBindings()
        {
            Container.BindInstance(_settingsWindow).AsSingle();
        }
    }
}