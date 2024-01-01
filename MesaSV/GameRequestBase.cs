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

        public GameRequestBase(string playerId, GameMode gameMode, string? acceptedPlayerId, GameRequestStatus status)
        {
            PlayerId = playerId;
            AcceptedPlayerId = acceptedPlayerId;
            Status = status;
            GameMode = gameMode;
            CreacionDate = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// el id de la solicitud
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// el id del usuario que hace la solicitud         
        /// </summary>
        public string PlayerId { get; set; }

        /// <summary>
        /// el id del user que acepta la solicitud         
        /// </summary>
        public string? AcceptedPlayerId { get; set; }

        /// <summary>
        /// el estado de la solicitud 
        /// </summary>
        public GameRequestStatus Status { get; set; }

        /// <summary>
        /// El modo de juego a jugar
        /// </summary>
        public GameMode GameMode { get; set; }

        /// <summary>
        /// es la fecha de creacion de la solicitud
        /// </summary>
        public DateTimeOffset CreacionDate { get; set; }
    }
}
