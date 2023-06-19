using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa_SV.Exceptions
{
    public enum HttpErrorType
    {
        Client = 400,
        NotFound = 404,
        Forbidden = 403
    }
}
