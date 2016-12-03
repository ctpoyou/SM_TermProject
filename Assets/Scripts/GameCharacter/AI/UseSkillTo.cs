﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwareModeling.GameCharacter.AI
{
    public class UseSkillTo : AbstractAINode
    {
        private int _sklIdx;

        public UseSkillTo( int sklIdx_ )
        {
            _sklIdx = sklIdx_;
        }

        public override bool executeNode(AICharacter self_)
        {
            if( parent.target != null && self_.isReady())
            {
                return self_.useSkillTo(_sklIdx, parent.target);
            }
            else
            {
                return false;
            }
        }
    }
}
