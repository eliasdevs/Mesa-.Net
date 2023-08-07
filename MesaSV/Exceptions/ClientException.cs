using Mesa_SV.Exceptions.MesaBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mesa_SV.Exceptions
{
    public class ClientException : MesaClientException<ClientExceptionType>
    {
        protected ClientException(ClientExceptionType exceptionType, string relatedField, Type relatedObject, string message) : base(exceptionType, relatedField, relatedObject, message)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="relatedField"></param>
        /// <param name="relatedObject"></param>
        /// <param name="message">Opcional, si no se esopecifica se pone un mensaje generico indicando el campo y clase base </param>
        /// <returns></returns>
        public static ClientException CreateException(ClientExceptionType type, string relatedField,
            Type relatedObject, string message = null)
        {
            return new ClientException(type, relatedField, relatedObject, message ?? GetMessage(type, relatedField, relatedObject));
        }
    }

    /// <summary>
    /// Note: errores abajo de 100 pertenecen a codigos de la asep
    /// </summary>
    public enum ClientExceptionType
    {
        /// <summary>
        /// 
        /// </summary>
        RequiredField = 100,
        /// <summary>
        /// 
        /// </summary>
        InvalidOperation = 101,

        /// <summary>
        /// Se especifico unvalor no valido para un campo
        /// </summary>
        InvalidFieldValue = 102,
        /// <summary>
        /// Cuando el tamaño de un campo es muy corto o muy largo
        /// </summary>
        InvalidFieldSize = 103,

        /// <summary>
        /// Cuando se trata de crear una entidad que ya existe.
        /// </summary>
        EntityAlreadyExist = 104,

        /// <summary>
        /// Cuando se quiere acceder a algun proceso que no esta implementado
        /// </summary>
        NotImplemented = 105,

        /// <summary>
        /// Cuando al momento de vincular el cliente devuelve si el cliente esta en lista negra.
        /// </summary>
        ClientIsBlackList = 106
    }
}
