using Mesa_SV;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Juegos.Modules.BlackJack.Models
{
    public class CreateRequestVm
    {
        /// <summary>
        /// tipo de juego
        /// </summary>
        [Required(ErrorMessage = "Por Favor Seleccione el tipo de Juego")]        
        public GameMode? GameMode { get; set; }

        /// <summary>
        /// este es el Id del jugador
        /// </summary>
        [Required(ErrorMessage= "Por Favor agregue el PlayerId")]
        public string PlayerId { get; set; }
    }
}
