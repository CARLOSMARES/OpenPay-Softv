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
    public interface ITipoUsuario
    {
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetTipoUsuario", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        TipoUsuarioEntity GetTipoUsuario(int? IdTipoUsuario);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeepTipoUsuario", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        TipoUsuarioEntity GetDeepTipoUsuario(int? IdTipoUsuario);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetTipoUsuarioList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<TipoUsuarioEntity> GetTipoUsuarioList();

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetTipoUsuarioPagedList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SoftvList<TipoUsuarioEntity> GetTipoUsuarioPagedList(int page, int pageSize);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetTipoUsuarioPagedListXml", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SoftvList<TipoUsuarioEntity> GetTipoUsuarioPagedListXml(int page, int pageSize, String xml);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "AddTipoUsuario", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? AddTipoUsuario(TipoUsuarioEntity objTipoUsuario);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "UpdateTipoUsuario", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? UpdateTipoUsuario(TipoUsuarioEntity objTipoUsuario);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "DeleteTipoUsuario", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? DeleteTipoUsuario(String BaseRemoteIp, int BaseIdUser, int? IdTipoUsuario);

    }
}

