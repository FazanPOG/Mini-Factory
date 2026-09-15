using System.Collections.Generic;
using Analytics;
using Gameplay.Machine;
using Gameplay.State;
using UnityEngine;

namespace Gameplay.Root
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private List<Machine.Machine> _machines;
        [SerializeField] private GameplayUI _gameplayUI;
        
        private List<MachineConfig> _configs;

        private void Start()
        {
            //TODO:
            //wait api load
            //load data
            //update data by offline time

            IAnalyticProvider analyticProvider = new MultiAnalyticProvider(new List<IAnalyticProvider>()
            {
                new ConsoleAnalyticProvider(),
                new ConsoleAnalyticProvider()
            });
            
            PlayerStateProxy playerStateProxy = new PlayerStateProxy(new PlayerState()
            {
                Currency = 5000,
                ProductionSpeed = 0
            });
            
            LoadMachineConfigs(new MachineSOConfigProvider("Configs/SO"));
            InitMachines(playerStateProxy, analyticProvider);
            _gameplayUI.Init(playerStateProxy);
            
            analyticProvider.GameStarted();
        }

        private void LoadMachineConfigs(IMachineConfigProvider provider) => _configs = provider.Load();
        
        private void InitMachines(PlayerStateProxy playerStateProxy, IAnalyticProvider analyticProvider)
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
                    ID = config.ID.Value,
                    Level = config.Level.Value,
                    LockState = config.LockState.Value,
                    OpenCost = config.OpenCost.Value,
                    ProductionRateInSeconds = config.ProductionRateInSeconds.Value,
                    UpgradeCost = (int)(config.OpenCost.Value * config.UpgradeCostMultiplier.Value)
                };
                
                machine.Init(config, state, playerStateProxy, analyticProvider);
            }
        }
    }
}