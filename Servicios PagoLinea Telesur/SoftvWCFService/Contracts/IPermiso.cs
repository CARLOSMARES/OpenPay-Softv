
using SoftvWCFService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SoftvWCFService.Contracts
{
    [ServiceContract]
    public interface IPermiso
    {
        //[OperationContract]
        //SoftvList<PermisoEntity> GetXmlPermiso(String xml);
        //[OperationContract]
        //int? MargePermiso(int BaseIdUser, String BaseRemoteIp, String xml);

        //[OperationContract]
        //[WebInvoke(Method = "*", UriTemplate = "GetXmlPermiso", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        //SoftvList<PermisoEntity> GetXmlPermiso(String xml);

        //[OperationContract]
        //[WebInvoke(Method = "*", UriTemplate = "MargePermiso", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        //int? MargePermiso(int BaseIdUser, String BaseRemoteIp, String xml);



        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetPermisoList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<PermisoEntity> GetPermisoList();

    }
}

