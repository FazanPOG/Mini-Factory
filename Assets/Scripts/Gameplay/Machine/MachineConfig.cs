using UnityEngine;

namespace Gameplay.Machine
{
    [CreateAssetMenu(fileName = "MachineConfig", menuName = "Configs/Machine")]
    public class MachineConfig : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private LockState _lockState;
        [SerializeField] private int _level;
        [SerializeField] private float _productionRate;
        [SerializeField] private int _openCost;
        [SerializeField] private float _upgradeCostMultiplier;
        [SerializeField] private float _boostDuration;
        [SerializeField] private float _boostMultiplier;
        [SerializeField] private bool _canBoost;
        [SerializeField] private int _maxTimeOfflineProductionInSeconds;
        
        public int ID => _id;
        public LockState LockState => _lockState;
        public int Level => _level;
        public float ProductionRate => _productionRate;
        public int OpenCost => _openCost;
        public float UpgradeCostMultiplier => _upgradeCostMultiplier;
        public float BoostDuration => _boostDuration;
        public float BoostMultiplier => _boostMultiplier;
        public bool CanBoost => _canBoost;
        public int MaxTimeOfflineProductionInSeconds => _maxTimeOfflineProductionInSeconds;
    }
}