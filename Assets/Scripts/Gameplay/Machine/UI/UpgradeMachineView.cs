using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Machine.UI
{
    public class UpgradeMachineView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private Button _upgradeButton;

        public Action OnUpgradeButtonClicked;
        
        private void OnEnable()
        {
            _upgradeButton.onClick.AddListener(() => OnUpgradeButtonClicked?.Invoke());
        }

        public void UpdateCostText(int cost) => _costText.text = cost.ToString();
        
        private void OnDisable()
        {
            _upgradeButton.onClick.RemoveAllListeners();
        }
    }
}