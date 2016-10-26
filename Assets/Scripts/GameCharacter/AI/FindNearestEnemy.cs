using SoftwareModeling.Managers;

namespace SoftwareModeling.GameCharacter.AI
{
    class FindNearestEnemy : AbstractAINode
    {
        public override bool executeNode(AICharacter self_)
        {
            ITargetable target = SMCharacterManager.getInstance().getNearestEnemyFrom(self_);
            if( target == null )
            {
                return false;
            }
            else
            {
                parent.target = target;
                return true; 
            }
        }
    }
}
