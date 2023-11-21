

using SoftvWCFService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SoftvWCFService.Contracts
{
    //[ServiceContract]
    //public interface IModule
    //{
    //[OperationContract]
    //ModuleEntity GetModule(int? IdModule);
    //[OperationContract]
    //ModuleEntity GetDeepModule(int? IdModule);
    //[OperationContract]
    //IEnumerable<ModuleEntity> GetModuleList();
    //[OperationContract]
    //SoftvList<ModuleEntity> GetModulePagedList(int page, int pageSize);
    //[OperationContract]
    //SoftvList<ModuleEntity> GetModulePagedListXml(int page, int pageSize, String xml);
    //[OperationContract]
    //int? AddModule(ModuleEntity objModule);
    //[OperationContract]
    //int? UpdateModule(ModuleEntity objModule);
    //[OperationContract]
    //int? DeleteModule(String BaseRemoteIp, int BaseIdUser, int? IdModule);

    [AuthenticatingHeader]
    [ServiceContract]
    public interface IModule
    {
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetModule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ModuleEntity GetModule(int? IdModule);
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeepModule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ModuleEntity GetDeepModule(int? IdModule);
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetModuleList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<ModuleEntity> GetModuleList();
 
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "AddModule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? AddModule(ModuleEntity objModule);
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "UpdateModule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? UpdateModule(ModuleEntity objModule);
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "DeleteModule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? DeleteModule(String BaseRemoteIp, int BaseIdUser, int? IdModule);

        [WebInvoke(Method = "*", UriTemplate = "GetModulos_Permisos", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<ModuleEntity> GetModulos_Permisos(int? idrol);

    }
}

