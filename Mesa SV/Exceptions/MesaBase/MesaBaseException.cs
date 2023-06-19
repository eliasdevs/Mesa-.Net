using Mesa_SV.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa_SV.Exceptions.MesaBase
{
    public abstract class MesaBaseException : Exception
    {
        public string RelatedField { get; set; }

        public Type RelatedObject { get; set; }

        protected MesaBaseException(string relatedField, Type relatedObject, string message)
            : base(message)
        {
            RelatedField = relatedField;
            RelatedObject = relatedObject;
        }

        public abstract HttpErrorType GetErrorType();

        public abstract int GetExceptionTypeCode();

        public abstract string GetExceptionTypeDescription();
    }
}
