using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwareModeling.GameCharacter
{
    // events
    public delegate void onHitDelegate(ISkillUsable from_, double dmg_);
    public delegate void onDestroyDelegate(ITargetable self_);

    public interface ITargetable : IPlacable
    {
        void attacked(ISkillUsable from_, double dmg_);
        bool isAlive { get; }
        event onDestroyDelegate onDestroy;
        event onHitDelegate onHit;
    }
}
