using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwareModeling.GameCharacter
{
    public interface ISkillUsable : ITargetable
    {
        bool useSkillTo(int sklIdx_, ITargetable target_);
    }
}
