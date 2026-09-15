using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Machine
{
    public class MachineSOConfigProvider : IMachineConfigProvider
    {
        private const string DEFAULT_RESOURCES_PATH = "";

        private readonly string _resourcesPath;

        public MachineSOConfigProvider(string resourcesPath = DEFAULT_RESOURCES_PATH)
        {
            _resourcesPath = resourcesPath;
        }

        public List<MachineConfig> Load()
        {
            var configsSO = Resources.LoadAll<MachineConfigSO>(_resourcesPath);
            var configs = new List<MachineConfig>(configsSO.Length);

            foreach (var configSO in configsSO)
            {
                configs.Add(new MachineConfig()
                {
                    ID = configSO.ID,
                    LockState = configSO.LockState,
                    Level = configSO.Level,
                    ProductionRateInSeconds = configSO.ProductionRateInSeconds,
                    OpenCost = configSO.OpenCost,
                    UpgradeCostMultiplier = configSO.UpgradeCostMultiplier,
                    BoostDuration = configSO.BoostDuration,
                    BoostMultiplier =  configSO.BoostMultiplier,
                    CanBoost = configSO.CanBoost,
                    MaxTimeOfflineProductionInSeconds = configSO.MaxTimeOfflineProductionInSeconds
                });
            }
                

            return configs;
        }
    }
}