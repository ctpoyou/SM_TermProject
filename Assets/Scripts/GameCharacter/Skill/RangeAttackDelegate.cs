using SoftwareModeling.Managers;


namespace SoftwareModeling.GameCharacter.Skill
{
    class RangeAttackDelegate : SkillDelegate
    {
        public RangeAttackDelegate(double coeff_, double cooldown_, double range_) : base( coeff_, cooldown_, range_ )
        {
        }

        override public bool useSkillTo(ISkillUsable from_, ITargetable to_)
        {
            double time_ = SMTimeManager.getInstance().currentTime;
            if (isSkillReady(time_, from_, to_))
            {
                updateLastSkillUse(time_);
                to_.attacked(from_, coeff);

                return true;
            }

            return false;
        }
    }
}
