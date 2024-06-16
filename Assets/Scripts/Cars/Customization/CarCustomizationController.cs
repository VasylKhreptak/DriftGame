using System;
using System.Collections.Generic;
using Gameplay.Cars;
using Infrastructure.Serialization;
using Infrastructure.Services.Log.Core;
using Infrastructure.Services.PersistentData.Core;
using Infrastructure.Services.Scene.Core;
using Photon.Pun;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Cars.Customization
{
    public class CarCustomizationController : MonoBehaviourPunCallbacks
    {
        [Header("References")]
        [SerializeField] private Car _car;
        [SerializeField] private KeyValuePairs<PartGroup, List<PartReference>> _parts;

        private Dictionary<PartGroup, List<PartReference>> _partsDictionary;

        private ILogService _logService;
        private IPersistentDataService _persistentDataService;
        private ISceneService _sceneService;

        [Inject]
        private void Constructor(ILogService logService, IPersistentDataService persistentDataService, ISceneService sceneService)
        {
            _logService = logService;
            _persistentDataService = persistentDataService;
            _sceneService = sceneService;
        }

        #region MonoBehaviour

        private void Awake()
        {
            _partsDictionary = _parts.ToDictionary();
        }

        private void Start()
        {
            if (photonView.IsMine || _sceneService.IsInGarage())
                RestoreSavedData();
        }

        #endregion

        public IReadOnlyList<CarPart> GetGroupParts(PartGroup group) => _partsDictionary[group].ConvertAll(x => x.Part);

        [Button, HideInEditorMode]
        public void SetPart(CarPart part)
        {
            SetPartInternal(part);

            if (_sceneService.IsInGarage())
                return;

            photonView.RPC(nameof(SetPartInternal), RpcTarget.OthersBuffered, part, false);
        }

        [PunRPC]
        private void SetPartInternal(CarPart part, bool updatePersistentData = true)
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
            {
                if (_sceneService.IsInGarage())
                    SetPartInternal(part, false);
                else
                    photonView.RPC(nameof(SetPartInternal), RpcTarget.AllBuffered, part, false);
            }
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