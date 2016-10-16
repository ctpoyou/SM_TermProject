using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwareModeling.GameCharacter.AI
{
    abstract public class AbstractAINode
    {
        private Composite _parent = null;

        abstract public bool executeNode( AICharacter self_ );

        public Composite parent
        {
            get
            {
                return _parent;
            }

            set
            {
                _parent = value;
            }
        }
        
    }
}
