using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa_SV.BlackJack.Dtos.Output
{
    public record BlackjackStartOutput(string Id, string IdRequest, int ContadorMazo, GameStatus Status);
}
