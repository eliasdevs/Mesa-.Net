using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa_SV.Filter
{
    public class ApiExceptionResult
    {
        public int? ErrorTypeCode { get; set; }

        public string ErrorTypeName { get; set; }

        public string Message { get; set; }

        public string? RelatedObject { get; set; }

        public string RelatedField { get; set; }
    }
}
