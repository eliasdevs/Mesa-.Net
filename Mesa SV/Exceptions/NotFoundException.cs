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

        public static Exception CreateException(NotFoundExceptionType supportEntity, string v, Type type)
        {
            throw new NotImplementedException();
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
        Register = 0,
        SupportEntity = 1,
        Country = 2,
        State = 3,
        Region = 4,
        MaritalStatus = 5,
        Profession = 6,
        EarningSource = 7,
        EconomicActivity = 8,
        BankAccount = 9,
        Transfer = 10,
        VirtualCard = 11,
        TopUp = 12,
        Remittance = 13,
        Favorites = 14,
        QuickPay = 15,
        BillPay =16,
		Withdrawal = 17,
 		Transaction = 18,
        QrStatic = 19,
        DepositExpress = 20,
        Contract = 21,
    }
}
