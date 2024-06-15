using Cars.Customization;
using Data.Persistent;
using UniRx;

namespace Infrastructure.Data.Persistent
{
    public class PlayerData
    {
        public readonly Resources Resources = new Resources();

        public readonly IntReactiveProperty SelectedLevelIndex = new IntReactiveProperty(1);

        public CarModel SelectedCarModel = CarModel.Base;

        public readonly CarsData Cars = new CarsData();
    }
}