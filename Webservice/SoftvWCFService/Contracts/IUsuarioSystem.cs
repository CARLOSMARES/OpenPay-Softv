
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
    public interface IUsuarioSystem
    {
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetUsuarioSystem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        UsuarioSystemEntity GetUsuarioSystem(int? IdUsuario);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeepUsuarioSystem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        UsuarioSystemEntity GetDeepUsuarioSystem(int? IdUsuario);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetUsuarioSystemList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<UsuarioSystemEntity> GetUsuarioSystemList();

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetUsuarioSystemPagedList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SoftvList<UsuarioSystemEntity> GetUsuarioSystemPagedList(int page, int pageSize);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetUsuarioSystemPagedListXml", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SoftvList<UsuarioSystemEntity> GetUsuarioSystemPagedListXml(int page, int pageSize, String xml);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "AddUsuarioSystem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? AddUsuarioSystem(UsuarioSystemEntity objUsuarioSystem);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "UpdateUsuarioSystem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? UpdateUsuarioSystem(UsuarioSystemEntity objUsuarioSystem);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "DeleteUsuarioSystem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? DeleteUsuarioSystem(String BaseRemoteIp, int BaseIdUser, int? IdUsuario);


      




    }
}

