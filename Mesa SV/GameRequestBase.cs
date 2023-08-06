namespace Mesa_SV
{
    /// <summary>
    /// object Value representa una solicitud de Juego
    /// </summary>
    public abstract class GameRequestBase
    {
        public GameRequestBase()
        {
            Id = Guid.NewGuid().ToString();
        }

        public GameRequestBase(string playerId, TypeGame tipoJuego, string? acceptedPlayerId, GameRequestStatus status)
        {
            PlayerId = playerId;
            AcceptedPlayerId = acceptedPlayerId;
            Status = status;
            TipoJuego = tipoJuego;
        }

        /// <summary>
        /// el id de la solicitud
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// el id del usuario que hace la solicitud 
        /// se pone solamente el userid xq el OV se pondra en backjack
        /// </summary>
        public string PlayerId { get; set; }

        /// <summary>
        /// el id del user que acepta la solicitud
        /// se pone solamente el userid xq el OV se pondra en backjack        
        /// </summary>
        public string? AcceptedPlayerId { get; set; }

        /// <summary>
        /// el estado de la solicitud 
        /// </summary>
        public GameRequestStatus Status { get; set; }

        /// <summary>
        /// El tipo de juego a jugar
        /// </summary>
        public TypeGame TipoJuego { get; set; }
    }
}
