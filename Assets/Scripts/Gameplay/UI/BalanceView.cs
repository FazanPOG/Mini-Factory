using TMPro;
using UnityEngine;

public class BalanceView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _balanceText;
    
    public void UpdateBalanceText(int balance) => _balanceText.text = $"Balance: {balance}";
}