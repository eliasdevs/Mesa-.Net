using Mesa_SV;
using Mesa_SV.BlackJack;
using Mesa_SV.BlackJack.Dtos.Output;
using Microsoft.AspNetCore.SignalR;

namespace Mesa.RealTime.Project.Hubs
{
    /// <summary>
    /// este proyecto va controlar el realtime de los juegos
    /// aparentemente es un netcore pero no se usara como tal si no como servidor signal
    /// lo consumira el proyecto de blazor
    /// </summary>
    public class BlackJackHub : Hub
    {
        private readonly IBlackJackJackSdk _blackJackSdk;

        public BlackJackHub(IBlackJackJackSdk blackJackSdk)
        {
            _blackJackSdk = blackJackSdk;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }
                
        /// <summary>
        /// No devuelve nada porque solo crea la request
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="TipoJuego"></param>
        /// <returns></returns>
        public async Task CreateRequest(string playerId, TypeGame tipoJuego)
        {
            //crea la solicitud
            await _blackJackSdk.CreateRequest(playerId, Context.ConnectionId, tipoJuego);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerId">el id del jugador que acepta</param>
        /// <param name="requestId"></param>
        /// <param name="tipoJuego"></param>
        /// <returns></returns>
        public async Task AcceptRequest(string playerId, string requestId)
        {
            //acepta la solicitud
            GameRequestBackJackOutput request = await _blackJackSdk.AcceptRequest(playerId, requestId, Context.ConnectionId);

            //preparo para mandar mensaje a los dos jugadores
            List<string> targetClientIds = new List<string>
                {
                    Context.ConnectionId, ///acepta la partida
                    request.PlayerId //este jugador creo la solicitud
                };

            //Mnado mensaje a los dos jugadores, debe actualizar el request de ambos
            await Clients.Clients(targetClientIds).SendAsync("AcceptRequestResult", request);
        }

        /// <summary>
        /// recibe el id de la solicitud ya esta configurada completamente la request
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public async Task StartGameBlackJack(string requestId)
        {

            //consulto la solicitud
            GameRequestBackJackOutput request = await _blackJackSdk.GetRequest(requestId);

            //verifico que no sean nulos para preparar el clients de signal
            if (string.IsNullOrEmpty(request.PlayerId) || string.IsNullOrEmpty(request.AcceptedPlayerId))
                return;

            //crea el blackJack
            BlackjackStartOutput blackJackOutput = await _blackJackSdk.StartBlackJack(requestId);
            
            //preparo para mandar mensaje a los dos jugadores
            List<string> targetClientIds = new List<string>
                {
                    request.AcceptedPlayerId, ///acepta la partida
                    request.PlayerId //este jugador creo la solicitud
                };

            //Mnado mensaje a los dos jugadores
            await Clients.Clients(targetClientIds).SendAsync("StartBlackJackResult", blackJackOutput);
        }

        /// <summary>
        /// permite consultar las solicitudes que esperan ser aceptadas
        /// </summary>
        /// <param name="init"></param>
        /// <returns></returns>
        public async Task GetAllRequestGame()
        {
            List<GameRequestBackJackOutput> requests = await _blackJackSdk.GetAllRequest();

            await Clients.All.SendAsync("GetAllRequests", requests);
        }
    }
}
