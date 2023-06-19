using Mesa_SV.Exceptions.MesaBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pisto.Exceptions
{
    public class ForbiddenException : MesaForbiddenException
    {
        protected ForbiddenException()
        {
        }

        /// <summary>
        /// Crear la excepcion
        /// </summary>
        /// <returns></returns>
        public static ForbiddenException CreateException()
        {
            return new ForbiddenException();
        }

    }
}
