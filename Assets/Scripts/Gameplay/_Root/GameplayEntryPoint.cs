using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Root
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private List<Machine.Machine> _machines;

        private void Start()
        {
            //TODO:
            //wait api load
            //load data
            //update data by offline time
        
        }
    
        private void InitMachines()
        {
            if(_machines.Count == 0)
                Debug.LogError($"No machines found! ({nameof(GameplayEntryPoint)} ->  {nameof(InitMachines)})");
        
            foreach (var machine in _machines)
            {
            
            }
        }
    }
}