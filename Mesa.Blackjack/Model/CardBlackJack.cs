using Mesa_SV;
using Mesa_SV.BlackJack.Model.Barajas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.BlackJack.Model
{
    /// <summary>
    /// Representa del BlackJack
    /// </summary>
    public class CardBlackJack : Card
    {
        public CardBlackJack(int originalValue, int subValue, string representation, TypeCard typeOfCardId, string blackJackId) : base(originalValue, subValue, representation, typeOfCardId)
        {
            BlackJackId = blackJackId;
        }

        public string BlackJackId { get; set; } 
    }
}
