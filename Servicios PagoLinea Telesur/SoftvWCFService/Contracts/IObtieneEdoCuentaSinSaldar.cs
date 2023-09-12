
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
    public interface IObtieneEdoCuentaSinSaldar
    {


        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeepObtieneEdoCuentaSinSaldar", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ObtieneEdoCuentaSinSaldarEntity GetDeepObtieneEdoCuentaSinSaldar();

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetObtieneEdoCuentaSinSaldarList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<ObtieneEdoCuentaSinSaldarEntity> GetObtieneEdoCuentaSinSaldarList(long? Contrato, long? ClvSession);


    }
}

