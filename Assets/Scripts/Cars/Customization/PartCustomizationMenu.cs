using System.Collections.Generic;
using Gameplay.Cars;
using Infrastructure.Services.PersistentData.Core;
using Plugins.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Cars.Customization
{
    public class PartCustomizationMenu : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private Car _car;
        [SerializeField] private Button _leftArrow;
        [SerializeField] private Button _rightArrow;
        [SerializeField] private TMP_Text _partNameTMP;

        [Header("Preferences")]
        [SerializeField] private PartGroup _group;

        private IPersistentDataService _persistentDataService;

        [Inject]
        private void Constructor(IPersistentDataService persistentDataService)
        {
            _persistentDataService = persistentDataService;
        }

        private IReadOnlyList<CarPart> _parts;

        private int _currentPartIndex;

        public bool Enabled
        {
            get => _gameObject.activeSelf;
            set => _gameObject.SetActive(value);
        }

        #region MonoBehaviour

        private void Awake() => _parts = _car.CustomizationController.GetGroupParts(_group);

        private void OnEnable()
        {
            UpdateCurrentPartIndex();
            UpdatePartName();

            _leftArrow.onClick.AddListener(OnClickedLeft);
            _rightArrow.onClick.AddListener(OnClickedRight);
        }

        private void OnDisable()
        {
            _leftArrow.onClick.RemoveListener(OnClickedLeft);
            _rightArrow.onClick.RemoveListener(OnClickedRight);
        }

        #endregion

        private void UpdateCurrentPartIndex() => _currentPartIndex = GetCurrentPartIndex();

        private int GetCurrentPartIndex()
        {
            Dictionary<PartGroup, CarPart> partsMap = _persistentDataService.Data.PlayerData.Cars.Parts[_car.Model];

            CarPart part = partsMap[_group];

            return _parts.IndexOf(part);
        }

        private void OnClickedLeft()
        {
            _currentPartIndex--;

            if (_currentPartIndex < 0)
                _currentPartIndex = _parts.Count - 1;

            SetPart(_parts[_currentPartIndex]);
        }

        private void OnClickedRight()
        {
            _currentPartIndex++;

            if (_currentPartIndex >= _parts.Count)
                _currentPartIndex = 0;

            SetPart(_parts[_currentPartIndex]);
        }

        private void SetPart(CarPart part)
        {
            _car.CustomizationController.SetPart(part);
            UpdatePartName();
        }

        private void UpdatePartName() => _partNameTMP.text = _parts[_currentPartIndex].ToString();
    }
}