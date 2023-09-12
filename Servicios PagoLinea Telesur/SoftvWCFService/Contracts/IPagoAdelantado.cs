
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
    public interface IPagoAdelantado
    {

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetPagoAdelantadoList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<PagoAdelantadoEntity> GetPagoAdelantadoList(long? Clv_Session, long? Clv_Servicio, long? Clv_llave, long? Clv_UnicaNet, int? Clave, int? MesesAdelantados, int? IdTipoCli, long? Contrato);


        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetAdelantaParcialidades", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<PagoAdelantadoEntity> GetAdelantaParcialidades(long? Contrato, long? Clv_Session, int? numeroAdelantar);


    }
}

