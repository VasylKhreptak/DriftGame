using System;
using System.Collections.Generic;
using Gameplay.Cars;
using Infrastructure.Serialization;
using Infrastructure.Services.Log.Core;
using Infrastructure.Services.PersistentData.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Cars.Customization
{
    public class CarCustomizationController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Car _car;
        [SerializeField] private KeyValuePairs<PartGroup, List<PartReference>> _parts;

        private Dictionary<PartGroup, List<PartReference>> _partsDictionary;

        private ILogService _logService;
        private IPersistentDataService _persistentDataService;

        [Inject]
        private void Constructor(ILogService logService, IPersistentDataService persistentDataService)
        {
            _logService = logService;
            _persistentDataService = persistentDataService;
        }

        #region MonoBehaviour

        private void Awake()
        {
            _partsDictionary = _parts.ToDictionary();

            RestoreSavedData();
        }

        #endregion

        public IReadOnlyList<CarPart> GetGroupParts(PartGroup group) => _partsDictionary[group].ConvertAll(x => x.Part);

        [Button, HideInEditorMode]
        public void SetPart(CarPart part, bool updatePersistentData = true)
        {
            PartGroup? partGroup = GetPartGroup(part);

            if (partGroup.HasValue == false)
            {
                _logService.LogError($"Part {part} not found in the parts dictionary.");
                return;
            }

            List<PartReference> groupParts = _partsDictionary[partGroup.Value];

            foreach (PartReference partReference in groupParts)
            foreach (var partObject in partReference.GameObjects)
                partObject.SetActive(partReference.Part == part);

            if (updatePersistentData)
                UpdatePersistentData(partGroup.Value, part);
        }

        private void RestoreSavedData()
        {
            if (_persistentDataService.Data.PlayerData.Cars.Parts.TryGetValue(_car.Model, out Dictionary<PartGroup, CarPart> parts) == false)
                return;

            foreach (CarPart part in parts.Values)
                SetPart(part, false);
        }

        private void UpdatePersistentData(PartGroup partGroup, CarPart carPart)
        {
            if (_persistentDataService.Data.PlayerData.Cars.Parts.ContainsKey(_car.Model) == false)
                _persistentDataService.Data.PlayerData.Cars.Parts[_car.Model] = new Dictionary<PartGroup, CarPart>();

            _persistentDataService.Data.PlayerData.Cars.Parts[_car.Model][partGroup] = carPart;
        }

        private PartGroup? GetPartGroup(CarPart part)
        {
            foreach (PartGroup partGroup in _partsDictionary.Keys)
            foreach (PartReference partReference in _partsDictionary[partGroup])
            {
                if (partReference.Part.Equals(part))
                    return partGroup;
            }

            return null;
        }

        [Serializable]
        public class PartReference
        {
            public CarPart Part;
            public GameObject[] GameObjects;
        }
    }
}