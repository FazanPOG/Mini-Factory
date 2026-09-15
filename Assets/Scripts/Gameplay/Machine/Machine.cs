using Analytics;
using Gameplay.Machine.UI;
using UnityEngine;

namespace Gameplay.Machine
{
    public class Machine : MonoBehaviour
    {
        [SerializeField] private MachineView _machineView;
        
        private const float ProductionIntervalInSeconds = 1f;

        private MachineConfig _config;
        private MachineState _state;
        private PlayerStateProxy _playerStateProxy;
        private IAnalyticProvider _analyticProvider;

        private float _productionTimer;

        public void Init(MachineConfig config, MachineState state, PlayerStateProxy playerStateProxy, IAnalyticProvider analyticProvider)
        {
            _config = config;
            _state = state;
            _playerStateProxy = playerStateProxy;
            _analyticProvider = analyticProvider;
            
            _productionTimer = 0f;

            if (_state.LockState == LockState.Unlocked)
                _playerStateProxy.AddProductionSpeed(_state.ProductionRateInSeconds);
            
            _machineView.OnOpenClicked += Open;
            _machineView.OnUpgradeClicked += Upgrade;

            _machineView.Init(state);
        }

        private void Update()
        {
            if (_state == null)
                return;

            if(_state.LockState == LockState.Locked)
                return;
            
            _productionTimer += Time.deltaTime;

            while (_productionTimer >= ProductionIntervalInSeconds)
            {
                _productionTimer -= ProductionIntervalInSeconds;
                _playerStateProxy.AddCurrency(_state.ProductionRateInSeconds);
            }
        }

        private void Open()
        {
            if (_state == null || _state.LockState == LockState.Unlocked)
                return;

            if (_playerStateProxy.Currency < _state.OpenCost)
                return;

            _playerStateProxy.SpendCurrency(_state.OpenCost);

            _state.LockState = LockState.Unlocked;
            _state.UpgradeCost = CalculateUpgradeCost(_state.OpenCost);
            _playerStateProxy.AddProductionSpeed(_state.ProductionRateInSeconds);

            UpdateView();
            
            _analyticProvider.MachineUnlocked(_state.ID);
        }

        private void Upgrade()
        {
            if (_state == null || _state.LockState == LockState.Locked)
                return;

            if (_playerStateProxy.Currency < _state.UpgradeCost)
                return;

            int productionSpeedUpgrade = _config.ProductionRateInSeconds.Value;
            
            _playerStateProxy.SpendCurrency(_state.UpgradeCost);
            _playerStateProxy.AddProductionSpeed(productionSpeedUpgrade);
            
            _state.UpgradeCost = CalculateUpgradeCost(_state.UpgradeCost);
            _state.ProductionRateInSeconds += productionSpeedUpgrade;
            _state.Level++;

            UpdateView();
            
            _analyticProvider.MachineUpgrade(_state.ID, _state.Level);
        }

        private int CalculateUpgradeCost(int previousCost) => (int)(previousCost * _config.UpgradeCostMultiplier.Value);

        private void UpdateView()
        {
            _machineView.UpdateStateView(_state);
            _machineView.UpdateOpenView(_state);
            _machineView.UpdateUpgradeView(_state);
        }

        private void OnDestroy()
        {
            if (_machineView == null)
                return;

            _machineView.OnOpenClicked -= Open;
            _machineView.OnUpgradeClicked -= Upgrade;
        }
    }
}
