using UnityEngine;

namespace SoftwareModeling.GameCharacter.Skill
{
    abstract public class SkillDelegate
    {
        private double _coeff;
        private double _cooldown;
        private double _lastUsedTime;
        private double _range;
        private double _delay;

        public SkillDelegate(double coeff_, double cooldown_, double range_, double delay_)
        {
            _coeff = coeff_;
            _cooldown = cooldown_;
            _lastUsedTime = -cooldown_;
            _range = range_;
            _delay = delay_;
        }

        public abstract bool useSkillTo(ISkillUsable from_, ITargetable to_);

        protected bool isSkillReady( double time_, ISkillUsable from_, ITargetable to_ )
        {
            Debug.Log(from_);
            Debug.Log(to_);
            return from_.isReady() && _cooldown <= time_ - _lastUsedTime && Vector2.Distance(from_.position, to_.position) <= _range;
        }

        protected void updateLastSkillUse( double time_ )
        {
            _lastUsedTime = time_;
        }

        protected void applyDelay(ISkillUsable from_)
        {
            from_.setDelay(_delay);
        }

        public double coeff
        {
            get
            {
                return _coeff;
            }
        }
    }
}
