using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa_SV.Exceptions.MesaBase
{
    public class MesaNotFoundException<T> : MesaBaseException where T : Enum
    {
        public T ExceptionType { get; set; }

        protected MesaNotFoundException(T exceptionType, string relatedField, Type relatedObject, string message)
            : base(relatedField, relatedObject, message)
        {
            ExceptionType = exceptionType;
        }

        protected static string GetMessage(T exceptionType, string relatedField, Type relatedObject)
        {
            return "El objeto de tipo " + exceptionType.ToString() + " referenciado por el campo " + relatedField + " del objeto " + relatedObject.FullName + " no existe";
        }

        public override HttpErrorType GetErrorType()
        {
            return HttpErrorType.NotFound;
        }

        public override int GetExceptionTypeCode()
        {
            return Convert.ToInt32(ExceptionType);
        }

        public override string GetExceptionTypeDescription()
        {
            return ExceptionType.ToString();
        }
    }
}
