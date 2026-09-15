using System.Collections.Generic;

namespace Analytics
{
    public class MultiAnalyticProvider : IAnalyticProvider
    {
        private readonly List<IAnalyticProvider> _analyticProviders;

        public MultiAnalyticProvider(List<IAnalyticProvider> analyticProviders)
        {
            _analyticProviders = analyticProviders;
        }
        
        public void GameStarted()
        {
            foreach (var provider in _analyticProviders)
                provider.GameStarted();
        }

        public void MachineUnlocked(int machineId)
        {
            foreach (var provider in _analyticProviders)
                provider.MachineUnlocked(machineId);
        }

        public void MachineUpgrade(int machineId, int level)
        {
            foreach (var provider in _analyticProviders)
                provider.MachineUpgrade(machineId, level);
        }

        public void BoostStarted(int machineId, float durationInSeconds)
        {
            foreach (var provider in _analyticProviders)
                provider.BoostStarted(machineId, durationInSeconds);
        }

        public void BoostFinished(int machineId)
        {
            foreach (var provider in _analyticProviders)
                provider.BoostFinished(machineId);
        }

        public void OfflineIncomeApplied(int offlineTimeInSeconds, int income)
        {
            foreach (var provider in _analyticProviders)
                provider.OfflineIncomeApplied(offlineTimeInSeconds, income);
        }

        public void PurchaseSucceed(string productId)
        {
            foreach (var provider in _analyticProviders)
                provider.PurchaseSucceed(productId);
        }

        public void PurchaseFailed(string productId, string reason)
        {
            foreach (var provider in _analyticProviders)
                provider.PurchaseFailed(productId, reason);
        }
    }
}