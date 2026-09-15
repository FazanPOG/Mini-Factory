using System;
using Gameplay.State;

public class PlayerStateProxy
{
    private PlayerState _playerState;

    public Action OnCurrencyChanged;
    public Action OnProductionSpeedChanged;
    
    public int Currency => _playerState.Currency;
    public int ProductionSpeed => _playerState.ProductionSpeed;

    public PlayerStateProxy(PlayerState playerState)
    {
        _playerState = playerState;
    }

    public void AddCurrency(int amount)
    {
        if (amount > 0)
        {
            _playerState.Currency += amount;
            OnCurrencyChanged?.Invoke();
        }
    }

    public void SpendCurrency(int amount)
    {
        if(amount < 0 && amount > _playerState.Currency)
            return;
        
        _playerState.Currency -= amount;
        OnCurrencyChanged?.Invoke();
    }

    public void AddProductionSpeed(int amount)
    {
        if (amount < 0)
            return;

        _playerState.ProductionSpeed += amount;
        OnProductionSpeedChanged?.Invoke();
    }
}