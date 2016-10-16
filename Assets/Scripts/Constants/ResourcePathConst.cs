using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwareModeling.Constants
{
    class ResourcePathConst
    {
        static public string getNumberPath(int number_)
        {
            return "Prefabs/Number/Number" + number_;
        }

        public const string NumberSpritePath = "Prefabs/Number/NumberSprite";
    }
}
