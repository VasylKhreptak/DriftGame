using Garage.SpawnPoints;
using UnityEngine;
using Zenject;

namespace Garage
{
    public class GarageInstaller : MonoInstaller
    {
        [Header("References")]
        [SerializeField] private CarSpawnPoint _carSpawnPoint;

        #region MonoBehaviour

        private void OnValidate()
        {
            _carSpawnPoint ??= FindObjectOfType<CarSpawnPoint>(true);
        }

        #endregion

        public override void InstallBindings()
        {
            Container.BindInstance(_carSpawnPoint).AsSingle();

            Container.Bind<CarSelector>().AsSingle();
        }
    }
}