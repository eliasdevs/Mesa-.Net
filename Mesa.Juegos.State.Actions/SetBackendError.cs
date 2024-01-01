namespace Mesa.Juegos.State.Shared
{
    /// <summary>
    /// Accion que se dispara cuando ocurre un error llamando a un API
    /// </summary>
    /// <param name="apiException"></param>
    public record SetBackendError(string HttpStatusCode,
        int? ErrorTypeCode,
        string? ErrorTypeName,
        string Message,
        string RelatedObject,
        string RelatedField
        );

    /// <summary>
    /// Quita el errro de back end para que ya no se muestra la pantalla de error
    /// </summary>
    public record ClearBackendError();
}