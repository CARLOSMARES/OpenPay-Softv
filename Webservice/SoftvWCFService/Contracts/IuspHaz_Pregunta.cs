
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
    public interface IuspHaz_Pregunta
    {

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeepuspHaz_Pregunta", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        uspHaz_PreguntaEntity GetDeepuspHaz_Pregunta(long? Contrato, int? Op);


        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeeAfirmacionPregunta", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        uspHaz_PreguntaEntity GetDeeAfirmacionPregunta(long? Contrato, int? MesesAdelantados, int? Op, long? ClvSession, int? Op2);


    }
}

