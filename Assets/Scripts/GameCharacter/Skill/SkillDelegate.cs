using UnityEngine;

namespace SoftwareModeling.GameCharacter.Skill
{
    abstract public class SkillDelegate
    {
        private double _coeff;
        private double _cooldown;
        private double _lastUsedTime;
        private double _range;

        public SkillDelegate(double coeff_, double cooldown_, double range_)
        {
            _coeff = coeff_;
            _cooldown = cooldown_;
            _lastUsedTime = -cooldown_;
            _range = range_;
        }

        public abstract bool useSkillTo(AICharacter from_, AICharacter to_);

        protected bool isSkillReady( double time_, AICharacter from_, AICharacter to_ )
        {
            return _cooldown <= time_ - _lastUsedTime && Vector2.Distance(from_.position, to_.position) <= _range;
        }

        protected void updateLastSkillUse( double time_ )
        {
            _lastUsedTime = time_;
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
