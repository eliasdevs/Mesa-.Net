using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa_SV.Exceptions.MesaBase
{
    public class MesaForbiddenException : Exception
    {
        public MesaForbiddenException(string message)
            : base(message)
        {
        }

        public MesaForbiddenException()
        {
        }
    }
}
