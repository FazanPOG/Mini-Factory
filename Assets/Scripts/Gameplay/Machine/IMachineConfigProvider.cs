using System.Collections.Generic;

namespace Gameplay.Machine
{
    public interface IMachineConfigProvider
    {
        List<MachineConfig> Load();
    }
}