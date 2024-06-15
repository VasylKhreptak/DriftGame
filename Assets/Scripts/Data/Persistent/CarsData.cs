using System.Collections.Generic;
using Cars.Customization;

namespace Data.Persistent
{
    public class CarsData
    {
        public readonly Dictionary<CarModel, Dictionary<PartGroup, CarPart>> Parts = new Dictionary<CarModel, Dictionary<PartGroup, CarPart>>();

        public CarsData()
        {
            Parts.Add(CarModel.Base, new Dictionary<PartGroup, CarPart>
            {
                { PartGroup.Spoiler, CarPart.NoSpoiler },
                { PartGroup.SideSkirt, CarPart.NoSideSkirts }
            });
        }
    }
}