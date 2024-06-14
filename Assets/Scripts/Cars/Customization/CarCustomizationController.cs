using System;
using System.Collections.Generic;
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
        [SerializeField]
        private KeyValuePairs<PartGroup, List<PartReference>> _parts;

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
        }

        #endregion

        [Button]
        public void SetPart(CartPart part)
        {
            PartGroup? partGroup = GetPartGroup(part);

            if (partGroup.HasValue == false)
            {
                _logService.LogError($"Part {part} not found in the parts dictionary.");
                return;
            }

            List<PartReference> groupParts = _partsDictionary[partGroup.Value];

            foreach (PartReference partReference in groupParts)
            {
                if (partReference.GameObject != null)
                    partReference.GameObject.SetActive(partReference.Part == part);
            }
        }

        private PartGroup? GetPartGroup(CartPart part)
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
            public CartPart Part;
            public GameObject GameObject;
        }
    }
}