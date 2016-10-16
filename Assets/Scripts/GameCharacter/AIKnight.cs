using SoftwareModeling.GameCharacter.Skill;
using SoftwareModeling.GameCharacter.AI;

namespace SoftwareModeling.GameCharacter
{
    public class AIKnight : AICharacter
    {
        protected override void Awake()
        {
            base.Awake();
            hitPoint = _maxHitPoint;
            addSkill(new MeleeAttackDelegate(49, 3, 3));

            Composite root = new Sequencer();
            root.addChild(new FindNearestEnemy());
            root.addChild(new MoveTo(3));
            root.addChild(new UseSkillTo(0));

            AIRoot = root;
        }
    }
}
