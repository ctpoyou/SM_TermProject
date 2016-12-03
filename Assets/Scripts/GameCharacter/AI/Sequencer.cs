
namespace SoftwareModeling.GameCharacter.AI
{
    class Sequencer : Composite
    {
        public override bool executeNode( AICharacter self_)
        {
            foreach( AbstractAINode child in children)
            {
                if(!child.executeNode( self_ ))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
