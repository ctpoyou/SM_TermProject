using SoftwareModeling.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SoftwareModeling.GameCharacter.Skill
{
    class DefendSelfDelegate : SkillDelegate
    {
        ITargetable _defenderTarget;
        ISkillUsable _defenderUser;
        ITargetable _target;

        public DefendSelfDelegate(ITargetable defender_, double coeff_, double cooldown_, double range_, double delay_) : base( coeff_, cooldown_, range_, delay_ )
        {
            _defenderTarget = defender_;
        }

        override public bool useSkillTo(ISkillUsable from_, ITargetable to_)
        {
            _defenderUser = from_;
            _target = _defenderTarget;
            
            _target.onHit += onTargetHit;

            return true;
        }

        void onTargetHit(ISkillUsable from_, double dmg_)
        {
            _target.onHit -= onTargetHit;

            double time_ = SMTimeManager.getInstance().currentTime;
            if (isSkillReady(time_, _defenderUser, _target))
            {
                if( dmg_ > 0 )
                {
                    _target.onHit -= onTargetHit;
                    _target.hitPoint += dmg_;
                    _defenderTarget.attacked(from_, dmg_ * coeff);
                    updateLastSkillUse(time_);
                    applyDelay(from_);
                }

            }
        }
    }
}
