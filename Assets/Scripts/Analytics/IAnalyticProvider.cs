namespace Analytics
{
    public interface IAnalyticProvider
    {
        void GameStarted();
        void MachineUnlocked(int machineId);
        void MachineUpgrade(int machineId, int level);
        void BoostStarted(int machineId, float durationInSeconds);
        void BoostFinished(int machineId);
        void OfflineIncomeApplied(int offlineTimeInSeconds, int income);
        void PurchaseSucceed(string productId);
        void PurchaseFailed(string productId, string reason);
    }
}