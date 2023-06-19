using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa_SV
{
    /// <summary>
    /// detalles del usuario jugador
    /// </summary>
    public  record JugadorVo
    {
        public JugadorVo(Guid idUser, string name)
        {
            IdUser = idUser;
            Name = name;
        }

        public Guid IdUser { get; set; }
        public string Name { get; set; }
    }    
}
