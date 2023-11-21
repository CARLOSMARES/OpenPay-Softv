
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
    public interface IDameTiposClientes
    {

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDameTiposClientesList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<DameTiposClientesEntity> GetDameTiposClientesList(long? Contrato);


    }
}

