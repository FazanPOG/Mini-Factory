using System.Collections.Generic;
using Gameplay.Machine;
using UnityEngine;

namespace Gameplay.Root
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        //Use serializable dictionary
        [SerializeField] private List<Machine.Machine> _machines;
        [SerializeField] private List<MachineConfig> _configs;

        private void Start()
        {
            //TODO:
            //wait api load
            //load data
            //update data by offline time
            InitMachines();
        }
    
        private void InitMachines()
        {
            if(_machines.Count == 0)
                Debug.LogError($"No machines found! ({nameof(GameplayEntryPoint)} ->  {nameof(InitMachines)})");

            if(_machines.Count != _configs.Count)
                Debug.LogError($"number of machines and configs does not match ({nameof(GameplayEntryPoint)} ->  {nameof(InitMachines)})");
            
            for (int i = 0; i < _machines.Count; i++)
            {
                MachineConfig config = _configs[i];
                Machine.Machine machine = _machines[i];
                
                MachineState state = new MachineState()
                {
                    ID = config.ID,
                    Level = config.Level,
                    LockState = config.LockState,
                    OpenCost = config.OpenCost,
                    ProductionRateInSeconds = config.ProductionRateInSeconds,
                    UpgradeCost = (int)(config.OpenCost * config.UpgradeCostMultiplier)
                };
                
                machine.Init(config, state);
            }
        }
    }
}