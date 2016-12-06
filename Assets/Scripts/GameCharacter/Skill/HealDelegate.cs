using SoftwareModeling.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwareModeling.GameCharacter.Skill
{
    public class HealDelegate : SkillDelegate
    {
        public HealDelegate(double coeff_, double cooldown_, double range_, double delay_) : base(coeff_, cooldown_, range_, delay_)
        {
        }

        public override bool useSkillTo(ISkillUsable from_, ITargetable to_)
        {
            double time_ = SMTimeManager.getInstance().currentTime;
            if (isSkillReady(time_, from_, to_))
            {
                to_.attacked(from_, -coeff);

                updateLastSkillUse(time_);
                applyDelay(from_);

                return true;
            }

            return false;
        }
    }
}
