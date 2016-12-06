using SoftwareModeling.GameCharacter.Skill;
using SoftwareModeling.GameCharacter.AI;

using UnityEngine;

namespace SoftwareModeling.GameCharacter
{
    public class AIKnight : AICharacter
    {
        public int _attackDmg = 20;

        protected override void Awake()
        {
            base.Awake();

            hitPoint = _maxHitPoint;
            addSkill(new MeleeAttackDelegate(_attackDmg, 3, 3, 1));
            addSkill(new DefendSelfDelegate(this, 0.7, 10, 10, 1));
            addSkill(new DefendOtherDelegate(this, 0.7, 10, 3, 1));

            WWW xmlFile = new WWW("file:///" + Application.dataPath + "/../Assets/Resources/PreworkedAI/tanker.xmi");
            while (!xmlFile.isDone)
            {
                //Debug.Log(www.progress);
            }

            BehaviorTree bt = new BehaviorTree(xmlFile.text);

            AIRoot = bt.getRoot();
        }
    }
}
