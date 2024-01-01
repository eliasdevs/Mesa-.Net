namespace Mesa_SV
{
    /// <summary>
    /// representa el tipo de carta
    /// </summary>
    public enum TypeCard
    {
        Corazon = 0,
        Diamantes = 1,
        Trebol = 2,
        Picas = 3
    }

    /// <summary>
    /// representa el typo de backjack amistoso o recompensada
    /// </summary>
    public enum GameMode
    {
        /// <summary>
        /// partida amistosa, solo dejara 5 amistosas entre jugadores es decir 5 request por dia
        /// </summary>
        FRIENDLY = 1,

        /// <summary>
        /// CONTRA EL SISTEMA Llleva dinero en juego
        /// </summary>
        CRUPIER_REWARDED = 2,

        /// <summary>
        /// AMISTOSA CONTRA EL CRUPIER
        /// </summary>
        CRUPIER_FRIENDLY = 3,

        /// <summary>
        /// partida recompensada, 1 vs 1
        /// </summary>
        REWARDED = 4
    }

    /// <summary>
    /// muestra el los valores de las fichas o menedas dentro del jeugo
    /// </summary>
    public enum CoinValue
    {
        One = 1,
        Five = 5,
        TwentyFive = 25,
        Fifty = 50,
        OneHundred = 100,
        TwoHundred = 200,
        FiveHundred = 500
    }

    /// <summary>
    /// representa el estado de la solicitud de start Game
    /// </summary>
    public enum GameRequestStatus
    {
        /// <summary>
        /// cuando se crea la solicitud
        /// </summary>
        Pending = 1,

        /// <summary>
        /// cuando se acepta la solicitud
        /// </summary>
        Accepted = 2,

        /// <summary>
        /// cuando se cancela
        /// </summary>
        Rejected = 3
    }

    /// <summary>
    /// los estados del blackjack
    /// </summary>
    public enum GameStatus
    {
        /// <summary>
        /// iniciado
        /// </summary>
        Started = 1,

        /// <summary>
        /// finalizado
        /// </summary>
        finalized = 2,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum StatusHand
    {
        /// <summary>
        /// Este es cuando se ha iniciado el juego crea la lista de cartas sin jugador id
        /// </summary>
        INIT = 0,

        /// <summary>
        /// CUANDO presiona plantarse - Todas las cartas del jugador se deben poner en estado plantado
        /// </summary>
        STAND_HAND = 1,

        /// <summary>
        /// cuando esta pidiendo cartas
        /// </summary>
        ACTIVE = 2
    }
}
