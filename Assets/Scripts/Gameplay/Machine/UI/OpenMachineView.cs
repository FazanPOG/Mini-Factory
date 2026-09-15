using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Machine.UI
{
    public class OpenMachineView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _openCostText;
        [SerializeField] private Button _openButton;

        public Button OpenButton => _openButton;

        public void UpdateOpenCostText(MachineState state)
        {
            if (state.LockState == LockState.Locked)
                _openCostText.text = state.OpenCost.ToString();
            else
                _openCostText.text = "-";
        }
    }
}