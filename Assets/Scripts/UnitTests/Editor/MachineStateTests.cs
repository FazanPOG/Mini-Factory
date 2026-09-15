using Gameplay.Machine;
using NUnit.Framework;
using UnityEngine;

namespace UnitTests
{
    [TestFixture]
    public class MachineStateTests
    {
        private const int MACHINE_ID = 2;
        private const int MACHINE_LEVEL = 3;
        private const int PRODUCTION_RATE_IN_SECONDS = 15;
        private const int OPEN_COST = 100;
        private const int UPGRADE_COST = 250;
        private const float UPGRADE_COST_MULTIPLIER = 2.5f;

        [Test]
        public void NewState_IsLockedAndWithoutProgress()
        {
            var state = new MachineState();

            Assert.AreEqual(0, state.ID);
            Assert.AreEqual(LockState.Locked, state.LockState);
            Assert.AreEqual(0, state.Level);
            Assert.AreEqual(0, state.ProductionRateInSeconds);
            Assert.AreEqual(0, state.OpenCost);
            Assert.AreEqual(0, state.UpgradeCost);
        }

        [Test]
        public void State_SurvivesJsonRoundTrip_KeepingAllValues()
        {
            MachineState state = CreateState();

            MachineState restoredState = JsonUtility.FromJson<MachineState>(JsonUtility.ToJson(state));

            Assert.AreEqual(state.ID, restoredState.ID);
            Assert.AreEqual(state.LockState, restoredState.LockState);
            Assert.AreEqual(state.Level, restoredState.Level);
            Assert.AreEqual(state.ProductionRateInSeconds, restoredState.ProductionRateInSeconds);
            Assert.AreEqual(state.OpenCost, restoredState.OpenCost);
            Assert.AreEqual(state.UpgradeCost, restoredState.UpgradeCost);
        }

        [Test]
        public void State_ParsedFromJson_KeepsLockStateAndCosts()
        {
            const string JSON = "{\"ID\":2,\"LockState\":1,\"Level\":3,\"ProductionRateInSeconds\":15,\"OpenCost\":100,\"UpgradeCost\":250}";

            MachineState state = JsonUtility.FromJson<MachineState>(JSON);

            Assert.AreEqual(MACHINE_ID, state.ID);
            Assert.AreEqual(LockState.Unlocked, state.LockState);
            Assert.AreEqual(MACHINE_LEVEL, state.Level);
            Assert.AreEqual(PRODUCTION_RATE_IN_SECONDS, state.ProductionRateInSeconds);
            Assert.AreEqual(OPEN_COST, state.OpenCost);
            Assert.AreEqual(UPGRADE_COST, state.UpgradeCost);
        }

        //Mirrors the MachineConfig -> MachineState mapping of GameplayEntryPoint.InitMachines
        [Test]
        public void State_CreatedFromConfig_KeepsConfigValues()
        {
            MachineConfig config = CreateConfig();

            MachineState state = CreateStateFromConfig(config);

            Assert.AreEqual(config.ID.Value, state.ID);
            Assert.AreEqual(config.LockState.Value, state.LockState);
            Assert.AreEqual(config.Level.Value, state.Level);
            Assert.AreEqual(config.ProductionRateInSeconds.Value, state.ProductionRateInSeconds);
            Assert.AreEqual(config.OpenCost.Value, state.OpenCost);
            Assert.AreEqual(UPGRADE_COST, state.UpgradeCost);
        }

        [Test]
        public void UpgradeCost_GrowsByMultiplier_OnEveryUpgrade()
        {
            MachineState state = CreateStateFromConfig(CreateConfig());

            state.UpgradeCost = CalculateUpgradeCost(state.UpgradeCost);
            Assert.AreEqual(625, state.UpgradeCost);

            state.UpgradeCost = CalculateUpgradeCost(state.UpgradeCost);
            Assert.AreEqual(1562, state.UpgradeCost);
        }

        private static MachineState CreateState() => CreateStateFromConfig(CreateConfig());

        private static MachineConfig CreateConfig() => new MachineConfig
        {
            ID = MACHINE_ID,
            LockState = LockState.Unlocked,
            Level = MACHINE_LEVEL,
            ProductionRateInSeconds = PRODUCTION_RATE_IN_SECONDS,
            OpenCost = OPEN_COST,
            UpgradeCostMultiplier = UPGRADE_COST_MULTIPLIER,
            BoostDuration = 10f,
            BoostMultiplier = 1.5f,
            CanBoost = true,
            MaxTimeOfflineProductionInSeconds = 60
        };

        private static MachineState CreateStateFromConfig(MachineConfig config) => new MachineState
        {
            ID = config.ID.Value,
            LockState = config.LockState.Value,
            Level = config.Level.Value,
            ProductionRateInSeconds = config.ProductionRateInSeconds.Value,
            OpenCost = config.OpenCost.Value,
            UpgradeCost = CalculateUpgradeCost(config.OpenCost.Value)
        };

        private static int CalculateUpgradeCost(int previousCost) => (int)(previousCost * UPGRADE_COST_MULTIPLIER);
    }
}