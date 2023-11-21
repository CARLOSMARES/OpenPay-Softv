
using SoftvWCFService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SoftvWCFService.Contracts
{
    [AuthenticatingHeader]
    [ServiceContract]
    public interface IUsuario
    {

       

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "LogOn", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        UsuarioLoginEntity LogOn();

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDatosCliente", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        UsuarioEntity GetDatosCliente(long? Contrato);




    }
}

