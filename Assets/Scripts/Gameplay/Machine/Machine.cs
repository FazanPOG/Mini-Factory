using Gameplay.Machine.UI;
using UnityEngine;

namespace Gameplay.Machine
{
    public class Machine : MonoBehaviour
    {
        [SerializeField] private MachineView _machineView;
        
        private const float ProductionIntervalInSeconds = 1f;

        private MachineState _state;

        private float _productionTimer;
        private int _currency = 0;

        public int Currency => _currency;

        public void Init(MachineConfig config, MachineState state)
        {
            _state = state;

            _productionTimer = 0f;
            _currency = 0;
            
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

                // ProductionRateInSeconds is the currency amount produced per second
                _currency += _state.ProductionRateInSeconds;
            }
        }
    }
}
