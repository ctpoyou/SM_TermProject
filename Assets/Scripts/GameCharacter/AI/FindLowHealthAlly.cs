using SoftwareModeling.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SoftwareModeling.GameCharacter.AI
{
    class FindLowHealthAlly : AbstractAINode
    {
        public override bool executeNode(AICharacter self_)
        {
            ITargetable target = SMCharacterManager.getInstance().getLowestHealthAlly(self_);

            if( target == null)
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
