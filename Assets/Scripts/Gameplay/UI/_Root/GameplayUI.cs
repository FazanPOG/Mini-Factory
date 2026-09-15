using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private BalanceView _balanceView;
    [SerializeField] private ProductionView _productionView;

    private PlayerStateProxy _playerStateProxy;
    
    public void Init(PlayerStateProxy playerStateProxy)
    {
        _playerStateProxy = playerStateProxy;
        
        _playerStateProxy.OnCurrencyChanged += UpdateBalanceView;
        _playerStateProxy.OnProductionSpeedChanged += UpdateProductionView;
        UpdateBalanceView();
        UpdateProductionView();
    }

    private void UpdateBalanceView() => _balanceView.UpdateBalanceText(_playerStateProxy.Currency);
    private void UpdateProductionView() => _productionView.UpdateProductionText(_playerStateProxy.ProductionSpeed);
    
    private void OnDestroy()
    {
        _playerStateProxy.OnCurrencyChanged -= UpdateBalanceView;
        _playerStateProxy.OnProductionSpeedChanged -= UpdateProductionView;
    }
}