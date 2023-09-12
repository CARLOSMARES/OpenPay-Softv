
using SoftvWCFService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using SoftvWCFService.Functions;

namespace SoftvWCFService.Contracts
{
    [AuthenticatingHeader]
    [ServiceContract]
    public interface ISeguridadToken
    {
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetSeguridadToken", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SeguridadTokenEntity GetSeguridadToken();

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeepSeguridadToken", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SeguridadTokenEntity GetDeepSeguridadToken();

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetSeguridadTokenList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SeguridadTokenEntity> GetSeguridadTokenList();

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetSeguridadTokenPagedList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SoftvList<SeguridadTokenEntity> GetSeguridadTokenPagedList(int page, int pageSize);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetSeguridadTokenPagedListXml", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SoftvList<SeguridadTokenEntity> GetSeguridadTokenPagedListXml(int page, int pageSize, String xml);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "AddSeguridadToken", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? AddSeguridadToken(SeguridadTokenEntity objSeguridadToken);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "UpdateSeguridadToken", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? UpdateSeguridadToken(SeguridadTokenEntity objSeguridadToken);


    }
}

