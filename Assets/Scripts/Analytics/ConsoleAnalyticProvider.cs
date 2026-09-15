using UnityEngine;

namespace Analytics
{
    public class ConsoleAnalyticProvider : IAnalyticProvider
    {
        private const string DEFAULT_PREFIX = "[Analytics]";

        private readonly string _prefix;

        public ConsoleAnalyticProvider(string prefix = DEFAULT_PREFIX)
        {
            _prefix = prefix;
        }

        public void GameStarted() => Log("GameStarted");

        public void MachineUnlocked(int machineId) => Log($"MachineUnlocked machineId:{machineId}");

        public void MachineUpgrade(int machineId, int level) => Log($"MachineUpgrade machineId:{machineId} level:{level}");

        public void BoostStarted(int machineId, float durationInSeconds) => Log($"BoostStarted machineId:{machineId} durationInSeconds:{durationInSeconds}");

        public void BoostFinished(int machineId) => Log($"BoostFinished machineId:{machineId}");

        public void OfflineIncomeApplied(int offlineTimeInSeconds, int income) => Log($"OfflineIncomeApplied offlineTimeInSeconds:{offlineTimeInSeconds} income:{income}");

        public void PurchaseSucceed(string productId) => Log($"PurchaseSucceed productId:{productId}");

        public void PurchaseFailed(string productId, string reason) => Log($"PurchaseFailed productId:{productId} reason:{reason}");

        private void Log(string message) => Debug.Log($"{_prefix} {message}");
    }
}