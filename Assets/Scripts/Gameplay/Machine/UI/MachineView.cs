using System;
using UnityEngine;

namespace Gameplay.Machine.UI
{
    public class MachineView : MonoBehaviour
    {
        [SerializeField] private MachineStateView _stateView;
        [SerializeField] private OpenMachineView _openMachineView;
        [SerializeField] private UpgradeMachineView _upgradeMachineView;

        public Action OnOpenClicked;
        public Action OnUpgradeClicked;

        private void OnEnable()
        {
            _openMachineView.OpenButton.onClick.AddListener(() => OnOpenClicked?.Invoke());
            _upgradeMachineView.UpgradeButton.onClick.AddListener(() => OnUpgradeClicked?.Invoke());
        }

        public void Init(MachineState state)
        {
            UpdateStateView(state);
            UpdateOpenView(state);
            UpdateUpgradeView(state);
        }

        public void UpdateOpenView(MachineState state)
        {
            _openMachineView.UpdateOpenCostText(state);
        }
        
        public void UpdateStateView(MachineState state)
        {
            _stateView.UpdateOpenText(state.LockState);
            _stateView.UpdateLevelText(state.Level);
        }

        public void UpdateUpgradeView(MachineState state)
        {
            _upgradeMachineView.UpdateCostText(state.UpgradeCost);
        }
        
        private void OnDisable()
        {
            _openMachineView.OpenButton.onClick.RemoveAllListeners();
            _upgradeMachineView.UpgradeButton.onClick.RemoveAllListeners();
        }
    }
}