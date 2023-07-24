using Mesa_SV.Exceptions.MesaBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pisto.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class NotFoundException : MesaNotFoundException<NotFoundExceptionType>
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="relatedField"></param>
        /// <param name="relatedObject"></param>
        /// <returns></returns>
        public static NotFoundException CreateException(NotFoundExceptionType type, string relatedField,
            Type relatedObject, string v)
        {
            return new NotFoundException(type, relatedField, relatedObject, GetMessage(type, relatedField, relatedObject));
        }

        protected NotFoundException(NotFoundExceptionType exceptionType, string relatedField, Type relatedObject, string message) : base(exceptionType, relatedField, relatedObject, message)
        {
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public enum NotFoundExceptionType
    {
        BlackJack = 1,
        Card = 2
    }
}
