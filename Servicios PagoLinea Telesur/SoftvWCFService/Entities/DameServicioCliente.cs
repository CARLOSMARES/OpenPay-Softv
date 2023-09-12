using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SoftvWCFService.Entities
{
    public class DameServicioCliente
    {
        /// <summary>
        /// Property DESCORTA
        /// </summary>
        [DataMember]
        public String Descripcion { get; set; }

        [DataMember]
        public int? CLV_TipSer { get; set; }
    }
}