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

        #region initialize
        protected override void Awake()
        {
            base.Awake();
            _skills = new List<SkillDelegate>();
            _navAgent = GetComponent<NavMeshAgent>();
            _hudRoot = GetComponentInChildren<CharacterHUDRoot>();
            _trailRenderer = GetComponent<TrailRenderer>();

            switch( this. tag)
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
            _hudRoot.showNumber((int)dmg_);
        }

        private void onDestroyed( ITargetable self_ )
        {
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
                return true;
            }

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
        #endregion
    }
}
