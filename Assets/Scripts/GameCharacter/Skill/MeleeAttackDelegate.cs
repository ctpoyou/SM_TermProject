using SoftwareModeling.Managers;
using UnityEngine;

namespace SoftwareModeling.GameCharacter.Skill
{
    class MeleeAttackDelegate : SkillDelegate
    {
        public MeleeAttackDelegate(double coeff_, double cooldown_, double range_, double delay_) : base(coeff_, cooldown_, range_, delay_)
        {
        }

        override public bool useSkillTo(ISkillUsable from_, ITargetable to_)
        {
            double time_ = SMTimeManager.getInstance().currentTime;
            if ( isSkillReady( time_, from_, to_ ) )
            {
                from_.setDelay(time_);
                to_.attacked(from_, coeff );

                updateLastSkillUse(time_);
                applyDelay(from_);

                return true;
            }

            return false;
        }
    }
}
