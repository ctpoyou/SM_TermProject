using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwareModeling.GameCharacter.AI
{
    public class isHealthLow : AbstractAINode
    {
        public override bool executeNode(AICharacter self_)
        {
            return self_.hitPointRatio < 0.3;
        }
    }
}
