

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

    public interface IAdelantar
    {

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeepAdelantar", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        AdelantarEntity GetDeepAdelantar(long? IdSession);


        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeepChecaAdelantarPagosDif", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        AdelantarEntity GetDeepChecaAdelantarPagosDif(long? Contrato);

    }
}

