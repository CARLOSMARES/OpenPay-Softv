
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
    public interface IDameClv_Session
    {
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDameClv_Session", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        DameClv_SessionEntity GetDameClv_Session(long? Contrato);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeepDameClv_Session", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        DameClv_SessionEntity GetDeepDameClv_Session(long? Contrato);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeepDameClv_SessionDos", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        DameClv_SessionEntity GetDeepDameClv_SessionDos(String ContratoCom);

    }
}

