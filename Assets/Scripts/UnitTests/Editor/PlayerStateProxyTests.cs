using Gameplay.State;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class PlayerStateProxyTests
    {
        private const int INITIAL_CURRENCY = 100;
        private const int INITIAL_PRODUCTION_SPEED = 5;
        private const int ADDED_CURRENCY = 50;
        private const int ADDED_PRODUCTION_SPEED = 15;

        private PlayerState _playerState;
        private PlayerStateProxy _playerStateProxy;

        private int _currencyChangedCount;
        private int _productionSpeedChangedCount;

        [SetUp]
        public void SetUp()
        {
            _playerState = new PlayerState
            {
                Currency = INITIAL_CURRENCY,
                ProductionSpeed = INITIAL_PRODUCTION_SPEED
            };

            _playerStateProxy = new PlayerStateProxy(_playerState);
            _playerStateProxy.OnCurrencyChanged += OnCurrencyChanged;
            _playerStateProxy.OnProductionSpeedChanged += OnProductionSpeedChanged;
        }

        [TearDown]
        public void TearDown()
        {
            _playerStateProxy.OnCurrencyChanged -= OnCurrencyChanged;
            _playerStateProxy.OnProductionSpeedChanged -= OnProductionSpeedChanged;
        }

        [Test]
        public void CurrencyAndProductionSpeed_ReadValuesFromPlayerState()
        {
            Assert.AreEqual(INITIAL_CURRENCY, _playerStateProxy.Currency);
            Assert.AreEqual(INITIAL_PRODUCTION_SPEED, _playerStateProxy.ProductionSpeed);
        }

        [Test]
        public void AddCurrency_ChangesSharedStateAndRaisesEvent()
        {
            _playerStateProxy.AddCurrency(ADDED_CURRENCY);

            Assert.AreEqual(INITIAL_CURRENCY + ADDED_CURRENCY, _playerStateProxy.Currency);
            Assert.AreEqual(INITIAL_CURRENCY + ADDED_CURRENCY, _playerState.Currency);
            Assert.AreEqual(1, _currencyChangedCount);
            Assert.AreEqual(0, _productionSpeedChangedCount);
        }

        [TestCase(0)]
        [TestCase(-ADDED_CURRENCY)]
        public void AddCurrency_WithNonPositiveAmount_IsIgnored(int amount)
        {
            _playerStateProxy.AddCurrency(amount);

            Assert.AreEqual(INITIAL_CURRENCY, _playerStateProxy.Currency);
            Assert.AreEqual(0, _currencyChangedCount);
        }

        [Test]
        public void SpendCurrency_ChangesSharedStateAndRaisesEvent()
        {
            _playerStateProxy.SpendCurrency(ADDED_CURRENCY);

            Assert.AreEqual(INITIAL_CURRENCY - ADDED_CURRENCY, _playerStateProxy.Currency);
            Assert.AreEqual(INITIAL_CURRENCY - ADDED_CURRENCY, _playerState.Currency);
            Assert.AreEqual(1, _currencyChangedCount);
            Assert.AreEqual(0, _productionSpeedChangedCount);
        }

        [Test]
        public void SpendCurrency_WithAllCurrency_LeavesZero()
        {
            _playerStateProxy.SpendCurrency(INITIAL_CURRENCY);

            Assert.AreEqual(0, _playerStateProxy.Currency);
            Assert.AreEqual(0, _playerState.Currency);
            Assert.AreEqual(1, _currencyChangedCount);
        }

        [TestCase(INITIAL_CURRENCY + 1)]
        public void SpendCurrency_WithoutEnoughCurrency_IsIgnored(int amount)
        {
            _playerStateProxy.SpendCurrency(amount);

            Assert.AreEqual(INITIAL_CURRENCY, _playerStateProxy.Currency);
            Assert.AreEqual(INITIAL_CURRENCY, _playerState.Currency);
            Assert.AreEqual(0, _currencyChangedCount);
        }

        [TestCase(0)]
        [TestCase(-ADDED_CURRENCY)]
        public void SpendCurrency_WithNonPositiveAmount_IsIgnored(int amount)
        {
            _playerStateProxy.SpendCurrency(amount);

            Assert.AreEqual(INITIAL_CURRENCY, _playerStateProxy.Currency);
            Assert.AreEqual(INITIAL_CURRENCY, _playerState.Currency);
            Assert.AreEqual(0, _currencyChangedCount);
        }

        [Test]
        public void AddProductionSpeed_ChangesSharedStateAndRaisesEvent()
        {
            _playerStateProxy.AddProductionSpeed(ADDED_PRODUCTION_SPEED);

            Assert.AreEqual(INITIAL_PRODUCTION_SPEED + ADDED_PRODUCTION_SPEED, _playerStateProxy.ProductionSpeed);
            Assert.AreEqual(INITIAL_PRODUCTION_SPEED + ADDED_PRODUCTION_SPEED, _playerState.ProductionSpeed);
            Assert.AreEqual(1, _productionSpeedChangedCount);
            Assert.AreEqual(0, _currencyChangedCount);
        }

        [TestCase(-1)]
        public void AddProductionSpeed_WithNegativeAmount_IsIgnored(int amount)
        {
            _playerStateProxy.AddProductionSpeed(amount);

            Assert.AreEqual(INITIAL_PRODUCTION_SPEED, _playerStateProxy.ProductionSpeed);
            Assert.AreEqual(0, _productionSpeedChangedCount);
        }

        private void OnCurrencyChanged() => _currencyChangedCount++;

        private void OnProductionSpeedChanged() => _productionSpeedChangedCount++;
    }
}
