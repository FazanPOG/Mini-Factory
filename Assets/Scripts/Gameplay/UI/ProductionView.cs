using TMPro;
using UnityEngine;

public class ProductionView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _productionText;

    public void UpdateProductionText(int production) => _productionText.text = $"Production: {production}/s";
}