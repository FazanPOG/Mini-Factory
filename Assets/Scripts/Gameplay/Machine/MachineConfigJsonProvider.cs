using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Machine
{
    public class MachineConfigJsonProvider : IMachineConfigProvider
    {
        private const string DEFAULT_RESOUCES_PATH = "";

        private readonly string _resourcesPath;

        public MachineConfigJsonProvider(string resourcesPath = DEFAULT_RESOUCES_PATH)
        {
            _resourcesPath = resourcesPath;
        }

        public List<MachineConfig> Load()
        {
            var jsonFiles = Resources.LoadAll<TextAsset>(_resourcesPath);
            var configs = new List<MachineConfig>(jsonFiles.Length);

            for (int i = 0; i < jsonFiles.Length; i++)
            {
                var fileName = jsonFiles[i].name;
                var json = jsonFiles[i].text;
                
                MachineConfig config = JsonUtility.FromJson<MachineConfig>(json);
                Validate(config, fileName);
                configs.Add(config);   
            }

            return configs;
        }

        private void Validate(MachineConfig config, string fileName)
        {
            if (config.ID == null) throw new Exception($"Failed to load '{fileName}': Field 'ID' is missing");
            if (config.LockState == null) throw new Exception($"Failed to load '{fileName}': Field 'LockState' is missing");
            if (config.Level == null) throw new Exception($"Failed to load '{fileName}': Field 'Level' is missing");
            if (config.ProductionRateInSeconds == null) throw new Exception($"Failed to load '{fileName}': Field 'ProductionRateInSeconds' is missing");
            if (config.OpenCost == null) throw new Exception($"Failed to load '{fileName}': Field 'OpenCost' is missing");
            if (config.UpgradeCostMultiplier == null) throw new Exception($"Failed to load '{fileName}': Field 'UpgradeCostMultiplier' is missing");
            if (config.BoostDuration == null) throw new Exception($"Failed to load '{fileName}': Field 'BoostDuration' is missing");
            if (config.BoostMultiplier == null) throw new Exception($"Failed to load '{fileName}': Field 'BoostMultiplier' is missing");
            if (config.CanBoost == null) throw new Exception($"Failed to load '{fileName}': Field 'CanBoost' is missing");
            if (config.MaxTimeOfflineProductionInSeconds == null) throw new Exception($"Failed to load '{fileName}': Field 'MaxTimeOfflineProductionInSeconds ' is missing");
        }
    }
}