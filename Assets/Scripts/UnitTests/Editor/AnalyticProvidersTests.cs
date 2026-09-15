using System.Collections.Generic;
using Analytics;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class AnalyticProvidersTests
    {
        private const int MACHINE_ID = 1;
        private const int MACHINE_LEVEL = 2;
        private const float BOOST_DURATION = 10f;
        private const int OFFLINE_TIME_IN_SECONDS = 3600;
        private const int OFFLINE_INCOME = 120;
        private const string PRODUCT_ID = "no_ads";
        private const string PURCHASE_FAIL_REASON = "cancelled";
        private const int EVENTS_COUNT = 8;

        private TestAnalyticProvider _testProvider;

        [SetUp]
        public void SetUp() => _testProvider = new TestAnalyticProvider();

        [Test]
        public void TestProvider_TracksAllGameplayEvents_InOrder()
        {
            TrackAllEvents(_testProvider);

            CollectionAssert.AreEqual(
                new List<string>
                {
                    "GameStarted",
                    $"MachineUnlocked machineId:{MACHINE_ID}",
                    $"MachineUpgrade machineId:{MACHINE_ID} level:{MACHINE_LEVEL}",
                    $"BoostStarted machineId:{MACHINE_ID} durationInSeconds:{BOOST_DURATION}",
                    $"BoostFinished machineId:{MACHINE_ID}",
                    $"OfflineIncomeApplied offlineTimeInSeconds:{OFFLINE_TIME_IN_SECONDS} income:{OFFLINE_INCOME}",
                    $"PurchaseSucceed productId:{PRODUCT_ID}",
                    $"PurchaseFailed productId:{PRODUCT_ID} reason:{PURCHASE_FAIL_REASON}"
                },
                _testProvider.Events);
        }

        [Test]
        public void TestProvider_CountsEventsByName()
        {
            _testProvider.MachineUnlocked(1);
            _testProvider.MachineUnlocked(2);
            _testProvider.MachineUpgrade(1, MACHINE_LEVEL);

            Assert.AreEqual(3, _testProvider.EventsCount);
            Assert.AreEqual(2, _testProvider.GetCount("MachineUnlocked"));
            Assert.AreEqual(1, _testProvider.GetCount("MachineUpgrade"));
            Assert.AreEqual(0, _testProvider.GetCount("GameStarted"));
            Assert.IsTrue(_testProvider.Contains("MachineUnlocked"));
            Assert.IsFalse(_testProvider.Contains("GameStarted"));
        }

        [Test]
        public void TestProvider_GetEvents_ReturnsOnlyMatchingEvents()
        {
            _testProvider.MachineUnlocked(1);
            _testProvider.MachineUnlocked(2);
            _testProvider.MachineUpgrade(1, MACHINE_LEVEL);

            CollectionAssert.AreEqual(
                new List<string> { "MachineUnlocked machineId:1", "MachineUnlocked machineId:2" },
                _testProvider.GetEvents("MachineUnlocked"));
            CollectionAssert.AreEqual(
                new List<string> { $"MachineUpgrade machineId:1 level:{MACHINE_LEVEL}" },
                _testProvider.GetEvents("MachineUpgrade"));
        }

        [Test]
        public void TestProvider_DoesNotMatchEventsByPartialName()
        {
            _testProvider.MachineUnlocked(MACHINE_ID);

            Assert.IsFalse(_testProvider.Contains("Machine"));
            Assert.AreEqual(0, _testProvider.GetCount("Machine"));
            Assert.IsEmpty(_testProvider.GetEvents("Machine"));
            Assert.IsEmpty(_testProvider.GetEvents("MachineUnlock"));
        }

        [Test]
        public void TestProvider_GetLastEvent_ReturnsLastTrackedEvent()
        {
            _testProvider.GameStarted();
            _testProvider.MachineUnlocked(1);
            _testProvider.MachineUnlocked(2);

            Assert.AreEqual("MachineUnlocked machineId:2", _testProvider.GetLastEvent());
            Assert.AreEqual("MachineUnlocked machineId:2", _testProvider.GetLastEvent("MachineUnlocked"));
            Assert.AreEqual("GameStarted", _testProvider.GetLastEvent("GameStarted"));
            Assert.IsNull(_testProvider.GetLastEvent("MachineUpgrade"));
        }

        [Test]
        public void TestProvider_WithoutEvents_ReturnsEmptyData()
        {
            Assert.AreEqual(0, _testProvider.EventsCount);
            Assert.IsEmpty(_testProvider.Events);
            Assert.IsNull(_testProvider.GetLastEvent());
            Assert.IsFalse(_testProvider.Contains("GameStarted"));
        }

        [Test]
        public void TestProvider_Clear_RemovesAllEvents()
        {
            TrackAllEvents(_testProvider);

            _testProvider.Clear();

            Assert.AreEqual(0, _testProvider.EventsCount);
            Assert.IsEmpty(_testProvider.Events);
            Assert.IsFalse(_testProvider.Contains("GameStarted"));
            Assert.IsNull(_testProvider.GetLastEvent());
        }

        [Test]
        public void MultiProvider_ForwardsEveryEventToAllProviders()
        {
            var firstProvider = new TestAnalyticProvider();
            var secondProvider = new TestAnalyticProvider();
            var multiProvider = new MultiAnalyticProvider(new List<IAnalyticProvider> { firstProvider, secondProvider });

            TrackAllEvents(multiProvider);

            Assert.AreEqual(EVENTS_COUNT, firstProvider.EventsCount);
            Assert.AreEqual(EVENTS_COUNT, secondProvider.EventsCount);
            CollectionAssert.AreEqual(firstProvider.Events, secondProvider.Events);
            CollectionAssert.AreEqual(new List<string> { "GameStarted" }, firstProvider.GetEvents("GameStarted"));
        }

        [Test]
        public void MultiProvider_WithoutProviders_DoesNotThrow()
        {
            var multiProvider = new MultiAnalyticProvider(new List<IAnalyticProvider>());

            Assert.DoesNotThrow(() => TrackAllEvents(multiProvider));
        }

        [Test]
        public void ConsoleProvider_HandlesAllEvents()
        {
            var consoleProvider = new ConsoleAnalyticProvider();

            Assert.DoesNotThrow(() => TrackAllEvents(consoleProvider));
        }

        private static void TrackAllEvents(IAnalyticProvider analyticProvider)
        {
            analyticProvider.GameStarted();
            analyticProvider.MachineUnlocked(MACHINE_ID);
            analyticProvider.MachineUpgrade(MACHINE_ID, MACHINE_LEVEL);
            analyticProvider.BoostStarted(MACHINE_ID, BOOST_DURATION);
            analyticProvider.BoostFinished(MACHINE_ID);
            analyticProvider.OfflineIncomeApplied(OFFLINE_TIME_IN_SECONDS, OFFLINE_INCOME);
            analyticProvider.PurchaseSucceed(PRODUCT_ID);
            analyticProvider.PurchaseFailed(PRODUCT_ID, PURCHASE_FAIL_REASON);
        }
    }
}