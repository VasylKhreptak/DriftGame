using UI.Garage.Windows;
using UI.Garage.Windows.SelectLevel;
using UI.Utilities;
using UnityEngine;
using Zenject;

namespace Garage.Installers
{
    public class GarageUIInstaller : MonoInstaller
    {
        [Header("References")]
        [SerializeField] private SettingsWindow _settingsWindow;
        [SerializeField] private IAPShopWindow _iapShopWindow;
        [SerializeField] private SelectLevelWindow _selectLevelWindow;
        [SerializeField] private Touchpad _touchpad;

        #region MonoBehaviour

        private void OnValidate()
        {
            _settingsWindow ??= FindObjectOfType<SettingsWindow>(true);
            _iapShopWindow ??= FindObjectOfType<IAPShopWindow>(true);
            _selectLevelWindow ??= FindObjectOfType<SelectLevelWindow>(true);
            _touchpad ??= FindObjectOfType<Touchpad>(true);
        }

        #endregion

        public override void InstallBindings()
        {
            Container.BindInstance(_settingsWindow).AsSingle();
            Container.BindInstance(_iapShopWindow).AsSingle();
            Container.BindInstance(_selectLevelWindow).AsSingle();
            Container.BindInstance(_touchpad).AsSingle();
        }
    }
}