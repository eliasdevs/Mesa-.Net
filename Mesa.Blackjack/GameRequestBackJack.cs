using Mesa_SV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Blackjack
{
    public class GameRequestBackJack:GameRequestBase
    {
        /// <summary>
        /// este sera el backjack que va pertenecer a la solicitud, permite null, hasta que se acepte la solicitud se setea backjack
        /// </summary>
        public Blackjack? backjack { get; set; }
    }
}
