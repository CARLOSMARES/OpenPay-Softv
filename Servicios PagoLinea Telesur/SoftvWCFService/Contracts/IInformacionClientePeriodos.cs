
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
    public interface IInformacionClientePeriodos
    {
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetInformacionClientePeriodos", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        InformacionClientePeriodosEntity GetInformacionClientePeriodos(long? Contrato);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeepInformacionClientePeriodos", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        InformacionClientePeriodosEntity GetDeepInformacionClientePeriodos(long? Contrato);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetInformacionClientePeriodosList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<InformacionClientePeriodosEntity> GetInformacionClientePeriodosList(long? Contrato);


        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetPeriodoCliente", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<InformacionClientePeriodosEntity> GetPeriodoCliente(long? Contrato);

    }
}

