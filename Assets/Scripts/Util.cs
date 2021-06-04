using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    static class Util
    {
        public static bool CheckValue(float value, float howValueShouldBe, float margin)
        {
            if (value > howValueShouldBe - margin && value < howValueShouldBe + margin)
                return true;
            return false;
        }
    }
}
