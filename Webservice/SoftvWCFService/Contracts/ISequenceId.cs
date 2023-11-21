
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
    public interface ISequenceId
    {
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetSequenceId", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SequenceIdEntity GetSequenceId();
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeepSequenceId", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SequenceIdEntity GetDeepSequenceId();
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetSequenceIdList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SequenceIdEntity> GetSequenceIdList();
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetSequenceIdPagedList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SoftvList<SequenceIdEntity> GetSequenceIdPagedList(int page, int pageSize);
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetSequenceIdPagedListXml", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SoftvList<SequenceIdEntity> GetSequenceIdPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "AddSequenceId", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? AddSequenceId(SequenceIdEntity objSequenceId);
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "UpdateSequenceId", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? UpdateSequenceId(SequenceIdEntity objSequenceId);

        //[OperationContract]
        //[WebInvoke(Method = "*", UriTemplate = "DeleteSequenceId", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        //int? DeleteSequenceId(String BaseRemoteIp, int BaseIdUser,);

    }
}

