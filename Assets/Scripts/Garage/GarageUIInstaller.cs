using UI.Garage.Windows;
using UI.Windows;
using UnityEngine;
using Zenject;

namespace Garage
{
    public class GarageUIInstaller : MonoInstaller
    {
        [Header("References")]
        [SerializeField] private SettingsWindow _settingsWindow;
        [SerializeField] private IAPShopWindow _iapShopWindow;

        #region MonoBehaviour

        private void OnValidate()
        {
            _settingsWindow ??= FindObjectOfType<SettingsWindow>(true);
            _iapShopWindow ??= FindObjectOfType<IAPShopWindow>(true);
        }

        #endregion

        public override void InstallBindings()
        {
            Container.BindInstance(_settingsWindow).AsSingle();
            Container.BindInstance(_iapShopWindow).AsSingle();
        }
    }
}