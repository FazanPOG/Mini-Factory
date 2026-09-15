using TMPro;
using UnityEngine;

namespace Gameplay.Machine.UI
{
    public class MachineStateView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _isOpenText;
        [SerializeField] private TextMeshProUGUI _levelText;

        public void UpdateOpenText(LockState lockState)
        {
            if(lockState == LockState.Unlocked)
                _isOpenText.text = $"Is open: open";
            else
                _isOpenText.text = $"Is open: closed";
        }

        public void UpdateLevelText(int level) => _levelText.text = $"Level: {level}";
    }
}