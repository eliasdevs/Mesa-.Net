using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa_SV
{
    public class Mensaje
    {
        public int Id { get; set; }
        public string idReceptor { get; set; }
        public string Remitente{ get; set; }
        public string Contenido { get; set; }
        public DateTime FechaEnvio { get; set; }= DateTime.Now;
    }
}
