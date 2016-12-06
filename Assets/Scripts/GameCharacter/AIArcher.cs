using SoftwareModeling.GameCharacter.Skill;
using SoftwareModeling.GameCharacter.AI;

using UnityEngine;

namespace SoftwareModeling.GameCharacter
{
    public class AIArcher : AICharacter
    {
        public int _attackDmg = 20;

        protected override void Awake()
        {
            base.Awake();

            hitPoint = _maxHitPoint;
            addSkill(new RangeAttackDelegate(_attackDmg, 3, 12, 1));
            addSkill(new DefendSelfDelegate(this, 0.9, 10, 10, 1));
            addSkill(new RangeAttackDelegate(_attackDmg * 2, 10, 12, 1));

            WWW xmlFile = new WWW("file:///" + Application.dataPath + "/../Assets/Resources/PreworkedAI/dealer.xmi");
            while (!xmlFile.isDone)
            {
                //Debug.Log(www.progress);
            }

            BehaviorTree bt = new BehaviorTree(xmlFile.text);

            AIRoot = bt.getRoot();
        }
    }
}
