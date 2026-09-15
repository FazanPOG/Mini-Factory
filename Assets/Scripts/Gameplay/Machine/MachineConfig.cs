using UnityEngine;

namespace Gameplay.Machine
{
    [CreateAssetMenu(fileName = "MachineConfig", menuName = "Configs/Machine")]
    public class MachineConfig : ScriptableObject
    {
        [Header("State settings")]
        [Min(1)]
        [SerializeField] private int _id;
        [SerializeField] private LockState _lockState;
        [Min(1)]
        [SerializeField] private int _level;
        [SerializeField] private float _productionRateInSeconds;
        [Min(0)]
        [SerializeField] private int _openCost;
        [SerializeField] private float _upgradeCostMultiplier;
        
        [Header("Boost Settings")]
        [Space(10)]
        [Min(0)]
        [SerializeField] private float _boostDuration;
        [Min(0)]
        [SerializeField] private float _boostMultiplier;
        [SerializeField] private bool _canBoost;
        
        [Header("Offline production settings")]
        [Space(10)]
        [Min(0)]
        [SerializeField] private int _maxTimeOfflineProductionInSeconds;
        
        public int ID => _id;
        public LockState LockState => _lockState;
        public int Level => _level;
        public float ProductionRate => _productionRateInSeconds;
        public int OpenCost => _openCost;
        public float UpgradeCostMultiplier => _upgradeCostMultiplier;
        public float BoostDuration => _boostDuration;
        public float BoostMultiplier => _boostMultiplier;
        public bool CanBoost => _canBoost;
        public int MaxTimeOfflineProductionInSeconds => _maxTimeOfflineProductionInSeconds;
    }
}