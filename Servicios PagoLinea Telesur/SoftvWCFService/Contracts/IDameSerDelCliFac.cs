
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
    public interface IDameSerDelCliFac
    {
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDameSerDelCliFac", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        DameSerDelCliFacEntity GetDameSerDelCliFac();

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeepDameSerDelCliFac", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        DameSerDelCliFacEntity GetDeepDameSerDelCliFac();

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDameSerDelCliFacList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<DameSerDelCliFacEntity> GetDameSerDelCliFacList(long? Contrato);


    }
}

