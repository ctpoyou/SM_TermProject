using SoftwareModeling.GameCharacter.Skill;
using SoftwareModeling.GameCharacter.AI;

using UnityEngine;

namespace SoftwareModeling.GameCharacter
{
    public class AIPriest : AICharacter
    {
        public int _attackDmg = 20;

        protected override void Awake()
        {
            base.Awake();

            hitPoint = _maxHitPoint;
            addSkill(new MeleeAttackDelegate(_attackDmg, 3, 3, 1));
            addSkill(new DefendSelfDelegate(this, 0.9, 10, 10, 1));
            addSkill(new HealDelegate(_attackDmg * 2, 10, 8, 1));

            string path;
            if (Debug.isDebugBuild)
            {
                path = "file:///" + Application.dataPath + "/../Assets/Resources/PreworkedAI/healer.xmi";
            }
            else
            {
                path = "file:///" + Application.dataPath + "/healer.xmi";
            }
            WWW xmlFile = new WWW(path);
            while (!xmlFile.isDone)
            {
                //Debug.Log(www.progress);
            }

            BehaviorTree bt = new BehaviorTree(xmlFile.text);

            AIRoot = bt.getRoot();
        }
    }
}
