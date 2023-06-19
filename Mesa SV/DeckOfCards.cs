namespace Mesa_SV
{
    /// <summary>
    ///clase representa Baraja
    /// </summary>
    public class DeckOfCards
    { 

        /// <summary>
        /// id  de baraja
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// nombre de la baraja example Inglesa
        /// </summary>
        public string Name{ get; set; }

        /// <summary>
        /// lista de  cartas las 52 o segun la baraja
        /// </summary>
        public List<Card> Cards { get; set;}

    }
}