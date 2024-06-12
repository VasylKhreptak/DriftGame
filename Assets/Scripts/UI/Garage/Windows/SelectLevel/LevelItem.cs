using System;
using Infrastructure.Services.PersistentData.Core;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Garage.Windows.SelectLevel
{
    public class LevelItem : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Button _button;
        [SerializeField] private Image _outline;
        [SerializeField] private TMP_Text _levelText;

        [Header("Preferences")]
        [SerializeField] private int _levelIndex = 1;
        [SerializeField] private Color _defaultOutlineColor = Color.gray;
        [SerializeField] private Color _selectedOutlineColor = Color.green;

        private IPersistentDataService _persistentDataService;

        [Inject]
        private void Constructor(IPersistentDataService persistentDataService)
        {
            _persistentDataService = persistentDataService;
        }

        private IDisposable _selectedLevelIndexSubscription;

        #region MonoBehaviour

        private void OnValidate()
        {
            _button ??= GetComponent<Button>();
            UpdateLevelIndexFromHierarchy();
        }

        private void Awake() => _levelText.text = (_levelIndex + 1).ToString();

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClicked);
            _selectedLevelIndexSubscription = _persistentDataService.Data.PlayerData.SelectedLevelIndex.Subscribe(OnSelectedLevelIndexChanged);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
            _selectedLevelIndexSubscription?.Dispose();
        }

        #endregion

        private void UpdateLevelIndexFromHierarchy() => _levelIndex = transform.GetSiblingIndex();

        private void OnClicked() => SetLevel(_levelIndex);

        private void SetLevel(int index) => _persistentDataService.Data.PlayerData.SelectedLevelIndex.Value = index;

        private void OnSelectedLevelIndexChanged(int index) => _outline.color = index == _levelIndex ? _selectedOutlineColor : _defaultOutlineColor;
    }
}