using Mesa_SV;
using Mesa_SV.BlackJack.Dtos.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Juegos.State.Actions.Blackjacks
{
    /// <summary>
    /// permite crear una solicitud de juego
    /// </summary>
    /// <param name="PlayerId"></param>
    /// <param name="TipoJuego"></param>
    public record StartCreateRequest(string PlayerId, TypeGame TipoJuego);

    /// <summary>
    /// es cuando se ha creado la Request
    /// </summary>
    /// <param name="Request"></param>
    public record EndCreateRequest(GameRequestBackJackOutput Request);

}
