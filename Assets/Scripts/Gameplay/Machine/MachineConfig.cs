namespace Gameplay.Machine
{
    public class MachineConfig
    {
        public int? ID;
        public LockState? LockState;
        public int? Level;
        public int? ProductionRateInSeconds;
        public int? OpenCost;
        public float? UpgradeCostMultiplier;
        public float? BoostDuration;
        public float? BoostMultiplier;
        public bool? CanBoost;
        public int? MaxTimeOfflineProductionInSeconds;
    }
}
