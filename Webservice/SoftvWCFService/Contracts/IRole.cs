
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
    public interface IRole
    {
        //[OperationContract]
        //[WebInvoke(Method = "*", UriTemplate = "GetRole", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        //RoleEntity GetRole(int? IdRol);

        //[OperationContract]
        //[WebInvoke(Method = "*", UriTemplate = "GetDeepRole", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        //RoleEntity GetDeepRole(int? IdRol);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetRoleList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<RoleEntity> GetRoleList();

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetUpListPermisos", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<RoleEntity> GetUpListPermisos(RoleEntity objRole, List<PermisoEntity> LstPer);


        //[OperationContract]
        //[WebInvoke(Method = "*", UriTemplate = "GetRolePagedList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        //SoftvList<RoleEntity> GetRolePagedList(int page, int pageSize);

        //[OperationContract]
        //[WebInvoke(Method = "*", UriTemplate = "GetRolePagedListXml", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        //SoftvList<RoleEntity> GetRolePagedListXml(int page, int pageSize, String xml);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "AddRole", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? AddRole(RoleEntity objRole);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "UpdateRole", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? UpdateRole(RoleEntity objRole);

        //[OperationContract]
        //[WebInvoke(Method = "*", UriTemplate = "DeleteRole", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        //int? DeleteRole(String BaseRemoteIp, int BaseIdUser, int? IdRol);
        
        //[OperationContract]
        //[WebInvoke(Method = "*", UriTemplate = "ChangeStateRole", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        //int? ChangeStateRole(RoleEntity objRole, bool State);


        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetPermiRolList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<PermisoEntity> GetPermiRolList(int? IdRol);

       

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetRoleById", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        RoleEntity GetRoleById(int? IdRol);


    }
}

