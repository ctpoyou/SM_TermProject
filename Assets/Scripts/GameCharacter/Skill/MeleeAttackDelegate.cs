using SoftwareModeling.Managers;

namespace SoftwareModeling.GameCharacter.Skill
{
    class MeleeAttackDelegate : SkillDelegate
    {
        public MeleeAttackDelegate(double coeff_, double cooldown_, double range_) : base(coeff_, cooldown_, range_)
        {
        }

        override public bool useSkillTo( AICharacter from_, AICharacter to_ )
        {
            double time_ = SMTimeManager.getInstance().currentTime;
            if ( isSkillReady( time_, from_, to_ ) )
            {
                updateLastSkillUse(time_);
                to_.attacked(from_, coeff );

                return true;
            }

            return false;
        }
    }
}
