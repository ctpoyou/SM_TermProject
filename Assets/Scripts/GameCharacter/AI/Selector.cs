﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwareModeling.GameCharacter.AI
{
    public class Selector : Composite
    {
        public override bool executeNode( AICharacter self_ )
        {
            foreach (AbstractAINode child in children)
            {
                if (child.executeNode(self_))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
