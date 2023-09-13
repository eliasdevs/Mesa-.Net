using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa_SV.ExtensionsHelpers
{
    public static class GetEnum
    {
        public static int GetValueInt(this TypeCard valueEnum)
        {
            return Convert.ToInt32(valueEnum);
        }
    }
}
