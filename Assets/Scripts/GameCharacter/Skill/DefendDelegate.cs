using SoftwareModeling.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwareModeling.GameCharacter.Skill
{
    class DefendDelegate : SkillDelegate
    {
        ISkillUsable _defender;
        ITargetable _target;

        public DefendDelegate(double coeff_, double cooldown_, double range_) : base( coeff_, cooldown_, range_ )
        {
        }

        override public bool useSkillTo(ISkillUsable from_, ITargetable to_)
        {
            _defender = from_;
            _target = to_;
            to_.onHit += onTargetHit;

            return true;
        }

        void onTargetHit(ISkillUsable from_, double dmg_)
        {
            double time_ = SMTimeManager.getInstance().currentTime;
            if (isSkillReady(time_, _defender, _target))
            {
                _target.hitPoint += dmg_;
                _defender.attacked(from_, dmg_);
            }
            else
            {
                _target = null;
            }
        }
    }
}
