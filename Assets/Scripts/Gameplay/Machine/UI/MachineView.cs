using UnityEngine;

namespace Gameplay.Machine.UI
{
    public class MachineView : MonoBehaviour
    {
        [SerializeField] private MachineStateView _stateView;
        [SerializeField] private OpenMachineView _openMachineView;
        [SerializeField] private UpgradeMachineView _upgradeMachineView;

        public MachineStateView StateView => _stateView;
        public OpenMachineView OpenMachineView => _openMachineView;
        public UpgradeMachineView UpgradeMachineView => _upgradeMachineView;

        public void Refresh(MachineState state)
        {
            _stateView.UpdateOpenText(state.LockState);
            _stateView.UpdateLevelText(state.Level);
            _openMachineView.UpdateOpenCostText(state.OpenCost);
            _upgradeMachineView.UpdateCostText(state.UpgradeCost);
        }
    }
}