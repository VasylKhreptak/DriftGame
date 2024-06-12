using UI.Garage.Windows;
using UI.Garage.Windows.SelectLevel;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Garage
{
    public class GarageUIInstaller : MonoInstaller
    {
        [Header("References")]
        [SerializeField] private SettingsWindow _settingsWindow;
        [SerializeField] private IAPShopWindow _iapShopWindow;
        [FormerlySerializedAs("_selectedLevelWindow")] [SerializeField]
        private SelectLevelWindow _selectLevelWindow;

        #region MonoBehaviour

        private void OnValidate()
        {
            _settingsWindow ??= FindObjectOfType<SettingsWindow>(true);
            _iapShopWindow ??= FindObjectOfType<IAPShopWindow>(true);
            _selectLevelWindow ??= FindObjectOfType<SelectLevelWindow>(true);
        }

        #endregion

        public override void InstallBindings()
        {
            Container.BindInstance(_settingsWindow).AsSingle();
            Container.BindInstance(_iapShopWindow).AsSingle();
            Container.BindInstance(_selectLevelWindow).AsSingle();
        }
    }
}