
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
    public interface IValidaSaldoContrato
    {
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetValidaSaldoContrato", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ValidaSaldoContratoEntity GetValidaSaldoContrato(long? Contrato);




    }
}

