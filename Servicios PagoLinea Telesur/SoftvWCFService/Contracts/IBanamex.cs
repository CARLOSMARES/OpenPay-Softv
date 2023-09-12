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
    public interface IBanamex
    {

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDatosMerchant", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<BanamDatosMerchantEntity> GetDatosMerchant(long Contrato);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetGuarda_ReturnData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<BanamexEntity> GetGuarda_ReturnData(long? clv_Session, String retrieveData, String resultIndicator, string amount, string description, string id, string brand, string transactionId, int? conOsinError);
        

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetIdSessionTransaccion", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<BanamexSessionBancoEntity> GetIdSessionTransaccion(long? contrato, String resultIndicator);
      

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetBanamex", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        BanamexEntity GetBanamex();

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeepBanamex", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        BanamexEntity GetDeepBanamex();

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetCreateCheckoutSession", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        String GetCreateCheckoutSession(String merchantId, long? idInt, String token, String apiOperation, String id, String amount, String currency, String referenciaDePedido, String returnUrl, String cancelUrl, String logoUrl, int? timeout, String timeoutUrl);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetRetrieve", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        String GetRetrieve(long? idInt, String merchantId, String sessionId, String token, long? clv_session);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetRetrieveNuevo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        String GetRetrieveNuevo(long? idInt, long? clv_session);


        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetGuardaPagoEnLinea", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<BanamexGuardaPagoEntity> GetGuardaPagoEnLinea(long? clv_Session, long? contrato);
        /*
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetGuardaMovimiento", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<BanamexGuardaPagoEntity> GetGuardaMovimiento(long? clv_Session);
        */
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetBuscaMovimiento", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<BanamexBuscaEntity> GetBuscaMovimiento(long? clv_Session);

           
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetGuardaMovimiento", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? GetGuardaMovimiento(long? clv_Session, long? contrato, string navegador);

        //[OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetGuarda_Redireccion", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? GetGuarda_Redireccion(long? idInt, long? nivel, long? contrato, long? clv_session, String sessionId, String laUrl);


        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetGuarda_IdSessionByMovimiento", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? GetGuarda_IdSessionByMovimiento(long? clv_Session, String idSessionBanco, String successIndicator);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetValidaNoContratoMaestro", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<ValidaContratoNoMaestroEntity> GetValidaNoContratoMaestro(long? contrato);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetBanamexPagedList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<BanamexEntity> GetBanamexPagedList(int page, int pageSize);
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetBanamexPagedListXml", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<BanamexEntity> GetBanamexPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "AddBanamex", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? AddBanamex(BanamexEntity objBanamex);
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "UpdateBanamex", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? UpdateBanamex(BanamexEntity objBanamex);
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "DeleteBanamex", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? DeleteBanamex(String BaseRemoteIp, int BaseIdUser);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetObtieneDatosMovimiento", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        DatosMovimientoEntity GetObtieneDatosMovimiento(long? clv_Session, long? contrato, string navegador, string ip, decimal importe, string contratocompuesto);



        //[OperationContract]
        //[WebInvoke(Method = "*", UriTemplate = "GetDatosLogueo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        //IEnumerable<UsuarioEntity> GetDatosLogueo(String successIndicator);


    }
}

