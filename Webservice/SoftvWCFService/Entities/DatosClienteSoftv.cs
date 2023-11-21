using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftvWCFService.Entities
{
   public class DatosClienteSoftv
    {

        public long? ContratoReal { get; set; }
        public string ContratoCom { get; set; }
        public string NombreCli { get; set; }
        public string Direccion { get; set; }

        public int? Nivel { get; set; }

        public bool? Proporcional { get; set; }

        public List<DatosClienteSoftv> LstCliente { get; set; }

    }
}
