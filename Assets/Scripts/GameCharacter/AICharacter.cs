using SoftwareModeling.GameCharacter.Skill;
using SoftwareModeling.GameCharacter.AI;
using SoftwareModeling.Managers;
using SoftwareModeling.HUD;

using System.Collections.Generic;


using UnityEngine;
using UnityEngine.AI;

namespace SoftwareModeling.GameCharacter
{
    // events
    public delegate void onHitDelegate( AICharacter from_, AICharacter to_);
    public delegate void onDestroyDelegate( AICharacter self_);
    public enum FactionEnum { Ally, Enemy };

    abstract public class AICharacter : AbstractCharacter
    {
        [Range(1, 10000)]
        public double _maxHitPoint;

        private double _hitPoint;
        private NavMeshAgent _navAgent;
        private List<SkillDelegate> _skills;
        private AbstractAINode _AIRoot;
        private FactionEnum _faction;
        private CharacterHUDRoot _hudRoot;

        private event onDestroyDelegate _onDestroy;
        private event onHitDelegate _onHit;

        #region initialize
        protected override void Awake()
        {
            base.Awake();
            _skills = new List<SkillDelegate>();
            _navAgent = GetComponent<NavMeshAgent>();
            _hudRoot = GetComponentInChildren<CharacterHUDRoot>();

            switch( this. tag)
            {
                case "PlayerParty":
                    _faction = FactionEnum.Ally;
                    break;
                case "EnemyParty":
                    _faction = FactionEnum.Enemy;
                    break;
            }
        }

        protected override void Start()
        {
        }
        #endregion

        protected override void Update()
        {
            switch( SMGameManager.getInstance().gameState )
            {
                case SMGameState.IN_GAME:
                    if( isAlive )
                    {
                        AIRoot.executeNode(this);
                    }
                    break;
                case SMGameState.PAUSED:
                    break;
            }
        }

        #region peripherals

        public bool isAlive
        {
            get
            {
                return hitPoint >= 0;
            }
        }

        public FactionEnum faction
        {
            get
            {
                return _faction;
            }
        }

        protected AbstractAINode AIRoot
        {
            get
            {
                return _AIRoot;
            }

            set
            {
                _AIRoot = value;
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

        public void attacked( AICharacter from_, double dmg_ )
        {
            if( _onHit != null )
            {
                _onHit(from_, this);
            }

            hitPoint -= dmg_;
            _hudRoot.showNumber((int)dmg_);

            if( !isAlive )
            {
                _navAgent.Stop();

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

        public bool moveTo( Vector3 dest, float stopDistance)
        {
            if( Vector2.Distance( dest, position) < stopDistance )
            {
                return true;
            }

            _navAgent.stoppingDistance = stopDistance;
            _navAgent.SetDestination(dest);

            return false;
        }

        public bool useSkillTo(int sklIdx_, AICharacter target_)
        {
            return _skills[sklIdx_].useSkillTo( this, target_ );
        }
        

        protected void addSkill( SkillDelegate skill_ )
        {
            _skills.Add(skill_);
        }
        #endregion
    }
}
