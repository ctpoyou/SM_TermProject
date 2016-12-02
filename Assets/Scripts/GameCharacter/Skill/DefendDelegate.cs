using SoftwareModeling.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwareModeling.GameCharacter.Skill
{
    class DefendDelegate : SkillDelegate
    {
        ITargetable _defenderTarget;
        ISkillUsable _defenderUser;
        ITargetable _target;

        public DefendDelegate(ITargetable defender_, double coeff_, double cooldown_, double range_) : base( coeff_, cooldown_, range_ )
        {
            _defenderTarget = defender_;
        }

        override public bool useSkillTo(ISkillUsable from_, ITargetable to_)
        {
            _defenderUser = from_;
            _target = to_;
            to_.onHit += onTargetHit;

            return true;
        }

        void onTargetHit(ISkillUsable from_, double dmg_)
        {
            double time_ = SMTimeManager.getInstance().currentTime;
            if (isSkillReady(time_, _defenderUser, _target))
            {
                _target.hitPoint += dmg_;
                _defenderTarget.attacked(from_, dmg_);
            }
            else
            {
                _target = null;
            }
        }
    }
}
