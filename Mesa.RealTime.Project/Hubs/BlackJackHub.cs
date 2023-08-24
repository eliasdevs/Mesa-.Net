using Mesa_SV;
using Mesa_SV.BlackJack;
using Mesa_SV.BlackJack.Dtos.Output;
using Mesa_SV.VoDeJuegos;
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
        #region Request
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
            List<string> targetClientIds = new List<string>();

            //representa el contextId de Hub de los jugadores
            string? userIdCreateRequest = request.PlayerInfo?.FirstOrDefault(x => x.IdUser == request.PlayerId)?.IdContextWS; //este jugador creo la solicitud
            string? userIdAcceptRequest = request.PlayerInfo?.FirstOrDefault(x => x.IdUser == request.AcceptedPlayerId)?.IdContextWS; //este jugador creo la solicitud

            if (!string.IsNullOrEmpty(userIdCreateRequest) && !string.IsNullOrEmpty(userIdAcceptRequest))
            {
                targetClientIds.Add(userIdCreateRequest);
                targetClientIds.Add(userIdAcceptRequest);
            }

            if (targetClientIds.Any() && targetClientIds.Count() == 2)
            {
                //Mnado mensaje a los dos jugadores, debe actualizar el request de ambos
                await Clients.Clients(targetClientIds).SendAsync("AcceptRequestResult", request);
            }
        }

        /// <summary>
        /// recibe el id de la solicitud ya esta configurada completamente la request
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public async Task StartGameBlackJack(string requestId)
        {

            //crea el blackJack
            BlackjackStartOutput blackJackOutput = await _blackJackSdk.StartBlackJack(requestId);

            List<string> targetClientIds = await  GetUserContextId(requestId);

            if (targetClientIds.Any() && targetClientIds.Count() == 2)
            {
                //Mnado mensaje a los dos jugadores
                await Clients.Clients(targetClientIds).SendAsync("StartBlackJackResult", blackJackOutput);
            }
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
        #endregion

        /// <summary>
        /// Permite pedir una carta 
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="blackJackId"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public async Task DrawCard(string playerId, string blackJackId, string requestId)
        {
            List<string> targetClientIds = await GetUserContextId(requestId);

            //Pido una carta
            ManoJugadorVo mano = await _blackJackSdk.GetCardById(playerId, blackJackId);

            await Clients.Client(Context.ConnectionId).SendAsync("DrawCardResult", mano);

            if (mano.Estado == StatusHand.STAND_HAND)
            {  
                //Aviso a los involucrados que se planto alguien
                await Clients.Clients(targetClientIds).SendAsync("InDrawCardStandHand", "JugadorPlantado");
            }
        }

        /// <summary>
        /// permite asignar o quitar el turno a un jugador
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="isplayerTurn"></param>
        /// <returns></returns>
        public async Task PlayerTurn(string requestId, bool isplayerTurn)
        {
            string? connectionId = GetRivalUserContextId(await GetUserContextId(requestId));

            if (connectionId == null) return;

            await Clients.Client(connectionId).SendAsync("IsMyTurn", isplayerTurn);
        }

        /// <summary>
        /// Representa cuando el jugador se planta con su mano
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="blackJackId"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public async Task StandHand(string playerId, string blackJackId, string requestId)
        {
            List<string> targetClientIds = await GetUserContextId(requestId);

            ManoJugadorVo mano = await _blackJackSdk.PlantarBlackJack(blackJackId, playerId);
            
            await Clients.Client(Context.ConnectionId).SendAsync("StandHandResult", mano);

            if (mano.Estado == StatusHand.STAND_HAND)
            {
                //Aviso a los involucrados que se planto alguien
                await Clients.Clients(targetClientIds).SendAsync("InDrawCardStandHand", "JugadorPlantado");
            }
        }

        private string? GetRivalUserContextId(List<string> targetClientIds)
        {   
            return targetClientIds.FirstOrDefault(x => x != Context.ConnectionId);
        }
        /// <summary>
        /// Normalmente se usara para extraer la mano del jugador rival
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="blackJackId"></param>
        /// <returns></returns>
        public async Task GetActiveHand(string playerId, string blackJackId)
        {   
            ManoJugadorVo mano = await _blackJackSdk.GetActiveHand(blackJackId, playerId);

            //se responde solo al users que pidio consultar la mano
            await Clients.Client(Context.ConnectionId).SendAsync("GetActiveHandResult", mano);
        }

        /// <summary>
        /// Permite reiniciar la mano de un jugadorss
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="blackJackId"></param>
        /// <returns></returns>
        public async Task ResetHand(string playerId, string blackJackId)
        {
            ManoJugadorVo mano = await _blackJackSdk.ResetHand(blackJackId, playerId);

            //se responde solo al users Reinicio su mano
            await Clients.Client(Context.ConnectionId).SendAsync("ResetHandResult", mano);
        }

        #region blackJack

        /// <summary>
        /// Este metodo devuelve la lista de los dos jugadores
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        private async Task<List<string>> GetUserContextId(string requestId)
        {
            //preparo para mandar mensaje a los dos jugadores
            List<string> targetClientIds = new List<string>();

            //consulto la solicitud
            GameRequestBackJackOutput request = await _blackJackSdk.GetRequest(requestId);

            //verifico que no sean nulos para preparar el clients de signal
            if (string.IsNullOrEmpty(request.PlayerId) || string.IsNullOrEmpty(request.AcceptedPlayerId))
                return new List<string>();            

            //representa el contextId de Hub de los jugadores
            string? userIdCreateRequest = request.PlayerInfo?.FirstOrDefault(x => x.IdUser == request.PlayerId)?.IdContextWS; //este jugador creo la solicitud
            string? userIdAcceptRequest = request.PlayerInfo?.FirstOrDefault(x => x.IdUser == request.AcceptedPlayerId)?.IdContextWS; //este jugador creo la solicitud

            if (!string.IsNullOrEmpty(userIdCreateRequest) && !string.IsNullOrEmpty(userIdAcceptRequest))
            {
                targetClientIds.Add(userIdCreateRequest);
                targetClientIds.Add(userIdAcceptRequest);
            }

            return targetClientIds;
        }

        #endregion
    }
}
