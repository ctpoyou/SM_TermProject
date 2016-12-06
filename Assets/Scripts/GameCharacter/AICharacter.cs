using SoftwareModeling.GameCharacter.Skill;
using SoftwareModeling.GameCharacter.AI;
using SoftwareModeling.Managers;
using SoftwareModeling.HUD;

using System.Collections.Generic;


using UnityEngine;
using UnityEngine.AI;

namespace SoftwareModeling.GameCharacter
{
    abstract public class AICharacter : AbstractCharacter, ISkillUsable
    {
        private NavMeshAgent _navAgent;
        private TrailRenderer _trailRenderer;
        private List<SkillDelegate> _skills;
        private AbstractAINode _AIRoot;

        private CharacterHUDRoot _hudRoot;
        private ParticleSystem _particleSystem;
        private Animator _animator;
        private double _delay = 0;

        #region initialize
        protected override void Awake()
        {
            base.Awake();
            _skills = new List<SkillDelegate>();
            _navAgent = GetComponent<NavMeshAgent>();
            _hudRoot = GetComponentInChildren<CharacterHUDRoot>();
            _trailRenderer = GetComponent<TrailRenderer>();
            _animator = GetComponentInChildren<Animator>();

            _particleSystem = GetComponent<ParticleSystem>();

            switch ( this. tag)
            {
                case "PlayerParty":
                    faction = FactionEnum.Ally;
                    break;
                case "EnemyParty":
                    faction = FactionEnum.Enemy;
                    break;
            }

            onHit += onAttacked;
            onDestroy += onDestroyed;
        }

        protected override void Start()
        {
        }
        #endregion

        protected override void Update()
        {
            if( !isReady() )
            {
                _delay -= SMTimeManager.getInstance().deltaTime;
            }

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
        private void onAttacked( ISkillUsable from_, double dmg_ )
        {
            hitPoint -= dmg_;

            if( dmg_ > 0 )
            {
                _particleSystem.Simulate(0);
                _particleSystem.Play();
                _hudRoot.showNumber((int)dmg_, Color.red);
            }
            else
            {
                _hudRoot.showNumber((int)-dmg_, Color.green);
            }
    }

        private void onDestroyed( ITargetable self_ )
        {
            _animator.SetBool("Dead", true);
            Destroy(_trailRenderer);
            _navAgent.Stop();
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

        public bool moveTo( Vector3 dest, float stopDistance)
        {
            if( Vector2.Distance( dest, position) < stopDistance )
            {
                _animator.SetBool("Run", false);
                return true;
            }

            _animator.SetBool("Run", true);
            _navAgent.stoppingDistance = stopDistance;
            _navAgent.SetDestination(dest);

            return false;
        }

        public bool useSkillTo(int sklIdx_, ITargetable target_)
        {
            return _skills[sklIdx_].useSkillTo( this, target_ );
        }
        

        protected void addSkill( SkillDelegate skill_ )
        {
            _skills.Add(skill_);
        }

        public void setDelay(double delay_)
        {
            _delay = delay_;
        }

        public bool isReady()
        {
            return _delay <= 0;
        }
        #endregion
    }
}
