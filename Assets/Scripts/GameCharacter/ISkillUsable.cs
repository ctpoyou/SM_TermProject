using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwareModeling.GameCharacter
{
    public interface ISkillUsable : IPlacable
    {
        bool useSkillTo(int sklIdx_, ITargetable target_);
        void setDelay(double delay_);
        bool isReady();
    }
}
