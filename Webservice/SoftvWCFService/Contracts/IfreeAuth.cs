
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
    public interface IfreeAuth
    {
        

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetNotificacion", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<BanamexBuscaEntity> GetNotificacion(String response);

        /*
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDatosMerchant_freeAuth", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<BanamDatosMerchantEntity> GetDatosMerchant_freeAuth();
        */

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDatosLogueo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<UsuarioEntity> GetDatosLogueo(int? id, string successIndicator, long? nivel, String laUrl);


        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetLinkRegistro", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<LinkEntity> GetLinkRegistro(int? id);



        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetGuarda_RedireccionPublico", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? GetGuarda_RedireccionPublico(long? idInt, long? nivel, long? contrato, long? clv_session, String sessionId, String laUrl);
        
        

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDatosLogueo2", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? GetDatosLogueo2(int? id, string successIndicator);


    }
}

