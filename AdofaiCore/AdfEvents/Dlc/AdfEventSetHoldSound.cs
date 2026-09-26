using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagicShaper.AdofaiCore.AdfClass;

namespace MagicShaper.AdofaiCore.AdfEvents.Dlc
{
    internal class AdfEventSetHoldSound : AdfEventBase
    {
        public override string EventType => "SetHoldSound";

        public AdfHoldSoundType HoldStartSound { get; set; } = AdfHoldSoundType.Fuse;

        public AdfHoldSoundType HoldLoopSound { get; set; } = AdfHoldSoundType.Fuse;

        public AdfHoldSoundType HoldEndSound { get; set; } = AdfHoldSoundType.Fuse;

        public AdfHoldSoundType HoldMidSound { get; set; } = AdfHoldSoundType.Fuse;

        public AdfHoldMidSoundType HoldMidSoundType { get; set; } = AdfHoldMidSoundType.Once;

        public double HoldMidSoundDelay { get; set; } = 0.5d;

        public AdfHoldMidSoundTimingRelativeToType HoldMidSoundTimingRelativeTo { get; set; } = AdfHoldMidSoundTimingRelativeToType.End;

        public double HoldSoundVolume { get; set; } = 100d;
    }
}
