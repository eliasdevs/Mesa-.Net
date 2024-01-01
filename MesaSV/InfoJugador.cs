namespace Mesa_SV
{
    /// <summary>
    /// detalles del usuario jugador
    /// </summary>
    public class InfoJugador
    {
        /// <summary>
        /// este es el id del user en el identity 
        /// </summary>
        public string IdUser { get; set; }

        /// <summary>
        /// representa el id del websocket del user
        /// </summary>
        public string IdContextWS { get; set; }

    }
}
