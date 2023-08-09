using Mesa_SV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Blackjack.Data
{
    /// <summary>
    /// se encarga de manipular los datos de request
    /// </summary>
    public interface IGameRequestBackJackRepository
    {
        /// <summary>
        /// metodo para extraer una solicitud por su Id
        /// </summary>
        Task<GameRequestBackJack?> GetGameRequestBackJackAsync(string id);

        /// <summary>
        /// metodo para extraer todas las solicitudes en estado pending
        /// </summary>
        Task<List<GameRequestBackJack>> GetGameRequestsBackJackAsync();

        /// <summary>
        /// se encarga de crear una solicitud 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task CreateRequestAsync(GameRequestBackJack request);

        /// <summary>
        /// actualiza los cambios en la BD
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}
