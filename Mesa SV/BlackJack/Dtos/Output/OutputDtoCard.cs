using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa_SV.BlackJack.Dtos.Output
{
    public class OutputDtoCard
    {
        public OutputDtoCard(int originalValue, int subValue, string representation, TypeCard typeOfCardId)
        {
            OriginalValue = originalValue;
            SubValue = subValue;
            Representation = representation;
            TypeOfCardId = typeOfCardId;
        }

        /// <summary>
        /// valor original example A:1
        /// </summary>
        public int OriginalValue { get; set; }

        /// <summary>
        /// valor 2 example; A=11
        /// </summary>
        public int SubValue { get; set; }

        /// <summary>
        /// Simbolo de la carta de A,Q,K,J o 2-10
        /// </summary>
        public string Representation { get; set; }

        /// <summary>
        /// guarda el tipo de carta
        /// </summary>
        public TypeCard TypeOfCardId { get; set; }
    }
}
