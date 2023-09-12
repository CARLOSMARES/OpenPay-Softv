
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
    public interface Isp_dameContratoCompaniaAdic
    {

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "Getsp_dameContratoCompaniaAdicList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<sp_dameContratoCompaniaAdicEntity> Getsp_dameContratoCompaniaAdicList(String ContratoCom, int? ClvUsuario, String Modulo);


    }
}

