using Mesa.Blackjack.Data;
using Mesa_SV;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Text.RegularExpressions;

namespace Mesa.Blackjack.Api
{
    public class MiClaseSignalR : Hub
    {
        private readonly BlackJackContext _context;
        
        private int _idMensajeEscuchado;
        public MiClaseSignalR(BlackJackContext dbContext)
        {
            _context= dbContext;
        }
        private Dictionary<int, List<string>> _gruposMensajes = new Dictionary<int, List<string>>();
        string user1 = "admin1", user2 = "admin2";
        /// <summary>
        /// establece la coneccion
        /// </summary>
        /// <param name="user"></param>
        /// <param name="IdMensaje"></param>
        /// <returns></returns>
        public async Task IniciarConversacion(string user, int IdMensaje)
        {
            var mensaje = await _context.Mensajes.FirstOrDefaultAsync(x => x.Id == IdMensaje);

            if (!string.IsNullOrEmpty(user) && user == "remitente")
            {
                mensaje.Remitente = Context.ConnectionId;
            }
            else if(user=="receptor")
            {
                mensaje.idReceptor= Context.ConnectionId;
            }

            _context.Mensajes.Entry(mensaje).State = EntityState.Modified; // Marcar la entidad como modificada
            await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

            // Notificar a los usuarios que la conversación ha iniciado
            //await Clients.Users(user1, user2).SendAsync("ConversacionIniciada");
        }

        /// <summary>
        /// se usa para mandar el mensaje a alguien en especifico
        /// </summary>
        /// <param name="user"></param>
        /// <param name="IdMensaje"></param>
        /// <returns></returns>
        public async Task EnviarMensaje2(string user, int IdMensaje)
        {
            var mensaje = await _context.Mensajes.FirstOrDefaultAsync(x => x.Id == IdMensaje);

            string mensaggeTo = (user == "remitente") ? mensaje.idReceptor : mensaje.Remitente;
            
            await Clients.Client(mensaggeTo).SendAsync("MensajeRecibido", mensaje);

        }


        public async Task EnviarMensaje(string user, string message, int IdMensaje)
        {
            if (!_gruposMensajes.ContainsKey(IdMensaje))
            {
                _gruposMensajes[IdMensaje] = new List<string>();
            }
            if (_gruposMensajes.ContainsKey(IdMensaje))
            {
                _gruposMensajes[IdMensaje].Add(message);
                await Clients.Group(IdMensaje.ToString()).SendAsync("RespuestaDeSignalR", "Respuesta personalizada");
            }
            await Clients.Caller.SendAsync("RespuestaDeSignalR", "Respuesta personalizada2");
        }


        public async Task IniciarEscuchaMensajes(int IdMensaje)
        {
            _idMensajeEscuchado = IdMensaje;
            var mensaje = await _context.Mensajes.FirstOrDefaultAsync(x => x.Id == IdMensaje);
            if (mensaje != null)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, IdMensaje.ToString());

                if (!_gruposMensajes.ContainsKey(IdMensaje))
                {
                    _gruposMensajes[IdMensaje] = new List<string>();
                }

                await Clients.Caller.SendAsync("EscuchaMensajesIniciada");
            }
        }


        public async Task ModificarMensaje(int idMensaje, string nuevoContenido)
        {
            var mensaje = await _context.Mensajes.FirstOrDefaultAsync(x => x.Id == idMensaje);
            if (mensaje != null)
            {
                mensaje.Contenido = nuevoContenido;
                _context.Mensajes.Entry(mensaje).State = EntityState.Modified; // Marcar la entidad como modificada
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                if (_gruposMensajes.ContainsKey(idMensaje))
                {
                    _gruposMensajes[idMensaje].Add(nuevoContenido);
                }

                await Clients.Caller.SendAsync("MensajeEditado", mensaje);
            }
        }

    }

}
