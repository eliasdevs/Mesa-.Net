using Mesa_SV.Exceptions;

namespace Mesa_SV.Exceptions.MesaBase
{
    public class MesaClientException<T> : MesaBaseException where T : Enum
    {
        public T ExceptionType { get; set; }

        protected MesaClientException(T exceptionType, string relatedField, Type relatedObject, string message)
            : base(relatedField, relatedObject, message)
        {
            ExceptionType = exceptionType;
        }

        protected static string GetMessage(T exceptionType, string relatedField, Type relatedObject)
        {
            return "Error de cliente de tipo " + exceptionType.ToString() + " en el campo " + relatedField + " del objeto " + relatedObject.FullName;
        }

        public override HttpErrorType GetErrorType()
        {
            return HttpErrorType.Client;
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
