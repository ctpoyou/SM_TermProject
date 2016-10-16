using SoftwareModeling.GameCharacter;
using System.Collections.Generic;

namespace SoftwareModeling.GameCharacter.AI
{
    abstract public class Composite : AbstractAINode
    {
        private List<AbstractAINode> _children;
        private AICharacter _target;

        public Composite()
        {
            _children = new List<AbstractAINode>();
        }

        public AbstractAINode addChild(AbstractAINode child_)
        {
            _children.Add(child_);
            child_.parent = this;

            return this;
        }

        protected List<AbstractAINode> children
        {
            get
            {
                return _children;
            }
        }

        public AICharacter target
        {
            get
            {
                return _target;
            }

            set
            {
                _target = value;
            }
        }
    }
}
