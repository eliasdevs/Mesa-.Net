namespace Mesa_SV.VoDeJuegos
{
    /// <summary>
    /// clase de cartas
    /// </summary>
    public record Card
    {
        public Card(int originalValue, int subValue, string representation, TypeCard typeOfCardId)
        {
            OriginalValue = originalValue;
            SubValue = subValue;
            Representation = representation;
            TypeOfCardId = typeOfCardId;
        }

        /// <summary>
        /// toma el id de la carta
        /// </summary>
        public int Id { get; set; }
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
