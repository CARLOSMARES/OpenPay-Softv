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
    [ServiceContract]
    public interface ISession
    {
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetSession", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SessionEntity GetSession(long? IdSession);
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeepSession", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SessionEntity GetDeepSession(long? IdSession);
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetSessionList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SessionEntity> GetSessionList();
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetSessionPagedList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SoftvList<SessionEntity> GetSessionPagedList(int page, int pageSize);
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetSessionPagedListXml", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SoftvList<SessionEntity> GetSessionPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "AddSession", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? AddSession(SessionEntity objSession);
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "UpdateSession", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? UpdateSession(SessionEntity objSession);
    }
}

