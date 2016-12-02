using SoftwareModeling.GameCharacter.Skill;
using SoftwareModeling.GameCharacter.AI;

using UnityEngine;

namespace SoftwareModeling.GameCharacter
{
    public class AIKnight : AICharacter
    {
        public int _attackDmg = 40;

        private ParticleSystem _particleSystem;
        protected override void Awake()
        {
            base.Awake();


            _particleSystem = GetComponent<ParticleSystem>();

            hitPoint = _maxHitPoint;
            addSkill(new MeleeAttackDelegate(_attackDmg, 3, 3));
            addSkill(new DefendDelegate(this, 0, 10, 10));

            WWW xmlFile = new WWW("file:///E:/UnityWorkspace/SM_TermP/Assets/Resources/PreworkedAI/tanker.xmi");
            while (!xmlFile.isDone)
            {
                //Debug.Log(www.progress);
            }

            BehaviorTree bt = new BehaviorTree(xmlFile.text);

            AbstractAINode root = bt.getRoot();
            /*new Sequencer();
            root.addChild(new FindNearestEnemy());
            root.addChild(new MoveTo(3));
            root.addChild(new UseSkillTo(0));*/

            AIRoot = root;

            onHit += hitEffect;
        }

        private void hitEffect(ISkillUsable from_, double dmg_)
        {
            _particleSystem.Simulate(0);
            _particleSystem.Play();
        }
    }
}
