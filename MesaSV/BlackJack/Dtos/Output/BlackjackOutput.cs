namespace Mesa_SV.BlackJack.Dtos.Output
{
    public record BlackjackOutput(string Id, string IdRequest, int ContadorMazo, GameStatus Status);

    public record CardOutput(int OriginalValue, int SubValue, string Representation, TypeCard TypeOfCardId);

    public record GameRequestBackJackOutput(string Id, string PlayerId, string? AcceptedPlayerId, GameRequestStatus Status, GameMode GameMode, List<InfoJugador> PlayerInfo, DateTimeOffset CreacionDate);
    
}
