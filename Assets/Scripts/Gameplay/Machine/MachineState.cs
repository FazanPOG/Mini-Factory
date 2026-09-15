using System;

namespace Gameplay.Machine
{
    [Serializable]
    public class MachineState
    {
        public int ID;
        public LockState LockState;
        public int Level;
        public int ProductionRateInSeconds;
        public int OpenCost;
        public int UpgradeCost;
    }
}