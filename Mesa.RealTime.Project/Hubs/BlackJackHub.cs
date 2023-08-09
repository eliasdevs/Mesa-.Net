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
       
        /// <summary>
        /// ejemplo de mensaje global dejar alli por el momento
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="TipoJuego"></param>
        /// <returns></returns>
        public async Task CreateRequest(string playerId, TypeGame tipoJuego)
        {
            //crea la solicitud
            GameRequestBackJackOutput request =  await _blackJackSdk.CreateRequest(playerId, Context.ConnectionId, tipoJuego);

            //se manda a todos porque necesita que se actualize la lista de request in realtime
            await Clients.All.SendAsync("CreateRequestResult", request);
        }

        /// <summary>
        /// el id del jugador que acepta
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="requestId"></param>
        /// <param name="tipoJuego"></param>
        /// <returns></returns>
        public async Task AcceptRequest(string playerId, string requestId, TypeGame tipoJuego)
        {
            //crea la solicitud
            GameRequestBackJackOutput request = await _blackJackSdk.AcceptRequest(playerId, requestId,Context.ConnectionId);

            //TODO: este metodo debe mandar AcceptRequestResult solo al que acepto la solicitud
            await Clients.All.SendAsync("AcceptRequestResult", request);
        }

        /// <summary>
        /// recibe el id de la solicitud ya esta configurada completamente la request
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public async Task StartGameBlackJack(string requestId)
        {
            //crea la solicitud
            BlackjackStartOutput request = await _blackJackSdk.StartBlackJack(requestId);

            //TODO: mandar mensaje a los dos por el contextId de cada jugador lo va escuchar "StartBlackJack"            
            await Clients.All.SendAsync("StartBlackJack", request);
        }

        /// <summary>
        /// permite consultar las solicitudes que esperan ser aceptadas
        /// </summary>
        /// <param name="init"></param>
        /// <returns></returns>
        public async Task GetAllRequestGame(bool init)
        {
            List<GameRequestBackJackOutput> requests = await _blackJackSdk.GetAllRequest();

            await Clients.All.SendAsync("GetAllRequests", requests);
        }
    }
}
