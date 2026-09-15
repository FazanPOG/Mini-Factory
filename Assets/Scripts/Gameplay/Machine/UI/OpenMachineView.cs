using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Machine.UI
{
    public class OpenMachineView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _openCostText;
        [SerializeField] private Button _openButton;

        public Action OnOpenButtonClicked;
        
        private void OnEnable()
        {
            _openButton.onClick.AddListener(() => OnOpenButtonClicked?.Invoke());
        }

        public void UpdateOpenCostText(int openCost) => _openCostText.text = openCost.ToString();
        
        private void OnDisable()
        {
            _openButton.onClick.RemoveAllListeners();
        }
    }
}