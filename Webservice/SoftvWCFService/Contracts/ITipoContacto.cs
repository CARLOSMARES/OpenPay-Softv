
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
    public interface ITipoContacto
    {
    [OperationContract]
    [WebInvoke(Method = "*", UriTemplate = "GetTipoContacto", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
    TipoContactoEntity GetTipoContacto(long? IdTipoContacto);
    [OperationContract]
    [WebInvoke(Method = "*", UriTemplate = "GetDeepTipoContacto", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
    TipoContactoEntity GetDeepTipoContacto(long? IdTipoContacto);
    [OperationContract]
    [WebInvoke(Method = "*", UriTemplate = "GetTipoContactoList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
    IEnumerable<TipoContactoEntity> GetTipoContactoList();
    [OperationContract]
    [WebInvoke(Method = "*", UriTemplate = "GetTipoContactoPagedList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
    SoftvList<TipoContactoEntity> GetTipoContactoPagedList(int page, int pageSize);
    [OperationContract]
    [WebInvoke(Method = "*", UriTemplate = "GetTipoContactoPagedListXml", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
    SoftvList<TipoContactoEntity> GetTipoContactoPagedListXml(int page, int pageSize, String xml);
    [OperationContract]
    [WebInvoke(Method = "*", UriTemplate = "AddTipoContacto", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
    int? AddTipoContacto(TipoContactoEntity objTipoContacto);
    [OperationContract]
    [WebInvoke(Method = "*", UriTemplate = "UpdateTipoContacto", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
    int? UpdateTipoContacto(TipoContactoEntity objTipoContacto);
    [OperationContract]
    [WebInvoke(Method = "*", UriTemplate = "DeleteTipoContacto", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
    int? DeleteTipoContacto(String BaseRemoteIp, int BaseIdUser,long? IdTipoContacto);
    
    }
    }

  