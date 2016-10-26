using System;
using UnityEngine;

namespace SoftwareModeling.GameCharacter
{

    public enum FactionEnum { Ally, Enemy, Neutral };

    abstract public class AbstractCharacter : AbstractGameObject, ITargetable
    {
        [Range(1, 10000)]
        public double _maxHitPoint;
        private double _hitPoint;

        private Transform _transform;
        private FactionEnum _faction;

        private event onDestroyDelegate _onDestroy;
        private event onHitDelegate _onHit;

        protected override void Awake()
        {
            _transform = transform;
        }

        #region peripherals

        public Vector3 position
        {
            get
            {
                return _transform.position;
            }

            set
            {
                _transform.position = value;
            }
        }

        public FactionEnum faction
        {
            set
            {
                _faction = value;
            }

            get
            {
                return _faction;
            }
        }

        public double hitPointRatio
        {
            get
            {
                return _hitPoint / _maxHitPoint;
            }
        }

        public double hitPoint
        {
            get
            {
                return _hitPoint;
            }

            set
            {
                _hitPoint = value;
            }
        }

        public void attacked(ISkillUsable from_, double dmg_)
        {
            if (_onHit != null)
            {
                _onHit(from_, dmg_);
            }

            hitPoint -= dmg_;

            if (!isAlive)
            {
                if (_onDestroy != null)
                {
                    _onDestroy(this);
                }
            }
        }

        public event onDestroyDelegate onDestroy
        {
            add
            {
                _onDestroy += value;
            }

            remove
            {
                _onDestroy -= value;
            }
        }

        public event onHitDelegate onHit
        {
            add
            {
                _onHit += value;
            }

            remove
            {
                _onHit -= value;
            }
        }

        public bool isAlive
        {
            get
            {
                return hitPoint >= 0;
            }
        }
        #endregion
    }
}
