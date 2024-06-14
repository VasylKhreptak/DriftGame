using EntryPoints.Core;
using Infrastructure.Services.PersistentData.Core;
using Zenject;

namespace Garage
{
    public class GarageEntryPoint : EntryPoint
    {
        private CarSelector _carSelector;
        private IPersistentDataService _persistentDataService;

        [Inject]
        private void Constructor(CarSelector carSelector, IPersistentDataService persistentDataService)
        {
            _carSelector = carSelector;
            _persistentDataService = persistentDataService;
        }

        protected override void Enter()
        {
            _carSelector.Select(_persistentDataService.Data.PlayerData.SelectedCarModel);
        }
    }
}