
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
    public interface IDameRelSucursalCompa
    {


        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeepDameRelSucursalCompa", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        DameRelSucursalCompaEntity GetDeepDameRelSucursalCompa(int? IdSucursal, long? Contrato);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeepDimeSiYaGrabeFac", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        DameRelSucursalCompaEntity GetDeepDimeSiYaGrabeFac(long? Contrato);

    }
}

