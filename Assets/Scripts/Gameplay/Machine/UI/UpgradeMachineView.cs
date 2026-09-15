using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Machine.UI
{
    public class UpgradeMachineView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private Button _upgradeButton;

        public Button UpgradeButton => _upgradeButton;
        
        public void UpdateCostText(int cost) => _costText.text = cost.ToString();
    }
}