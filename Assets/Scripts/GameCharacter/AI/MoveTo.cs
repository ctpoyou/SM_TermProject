using System;
using UnityEngine;

namespace SoftwareModeling.GameCharacter.AI
{
    public class MoveTo : AbstractAINode
    {
        private float _stoppingDistance;

        public MoveTo(float stopDistance_ )
        {
            _stoppingDistance = stopDistance_;
        }

        public override bool executeNode(AICharacter self_)
        {
            return self_.moveTo(parent.target.position, _stoppingDistance);
        }
    }
}
