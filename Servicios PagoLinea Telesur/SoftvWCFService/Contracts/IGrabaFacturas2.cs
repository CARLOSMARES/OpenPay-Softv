
using SoftvWCFService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace SoftvWCFService.Contracts
{
    [AuthenticatingHeader]
    [ServiceContract]
    public interface IGrabaFacturas2
    {

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeepGrabaFacturas2", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        GrabaFacturas2Entity GetDeepGrabaFacturas2(long? Contrato, long? Clv_Session, String Cajera, int? Sucursal, String IpMaquina, String Tipo, String Serie_V, long? Folio_V, int? Clv_Vendedor, int? Tipo1, Decimal? Monto1, Decimal? GLOEFECTIVO2, Decimal? GLOCHEQUE2, int? GLOCLV_BANCOCHEQUE2, String NUMEROCHEQUE2, Decimal? GLOTARJETA2, int? GLOCLV_BANCOTARJETA2, String NUMEROTARJETA2, String TARJETAAUTORIZACION2, long? CLV_Nota3, Decimal? GLONOTA3, String Token);

    }
}

