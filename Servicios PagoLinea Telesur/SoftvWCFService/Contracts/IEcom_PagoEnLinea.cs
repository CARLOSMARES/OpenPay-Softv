using SoftvWCFService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using SoftvWCFService.Functions;

namespace SoftvWCFService.Contracts
{
    [AuthenticatingHeader]
    [ServiceContract]
    public interface IEcom_PagoEnLinea
    {

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "fnChangePassword", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int fnChangePassword(long? Contrato, String Nueva);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetContratoCompuesto", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Ecom_ContCompEntity GetContratoCompuesto(long? Contrato);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetGeneraDatosPago", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ParametrosPagoRedireccionEntity GetGeneraDatosPago(long? Clv_Session, long? Contrato, decimal Total);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetGeneraDatosPagoStore", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ParametrosPagoRedireccionEntity GetGeneraDatosPagoStore(long? Clv_Session, long? Contrato, decimal Total);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetNotificacionesWebhook", RequestFormat = WebMessageFormat.Json)]
        void GetNotificacionesWebhook();


        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetBusCliPorContrato_FacList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<BusCliPorContrato_FacEntity> GetBusCliPorContrato_FacList(int? Id, String ContratoC);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDameDetalleList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<DameDetalleEntity> GetDameDetalleList(long? Clv_Session);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDameServicioCliente", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<DameServicioCliente> GetDameServicioCliente(long? Contrato);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetSumaDetalleList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SumaDetalleEntity> GetSumaDetalleList(long? Clv_Session);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetImporteTotal", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        importeTotalEntity GetImporteTotal(long? Clv_Session);
  

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetBuscaFacturasHistorialList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<BuscaFacturasHistorialEntity> GetBuscaFacturasHistorialList(int? Id, String Serie, long? Folio, String Fecha, String Tipo, long? ContratoO);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeeptieneEdoCuenta", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        tieneEdoCuentaEntity GetDeeptieneEdoCuenta(long? Contrato);
        
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetCorreo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Ecom_CorreoEntity GetCorreo(long? Contrato);


        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetValidaCorreo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Ecom_CorreoEntity GetValidaCorreo(int? aux, String Correo);




        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDimeSiHayOtroPagoEnProceso_Softv", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool? GetDimeSiHayOtroPagoEnProceso_Softv(long? contrato);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "RetornoPagoLineaExitoso", BodyStyle = WebMessageBodyStyle.Bare)]
        System.IO.Stream RetornoPagoLineaExitoso();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "RetornoPagoLinea")]
        System.IO.Stream RetornoPagoLinea(List<ResultadoPagoLineaEntity> resultado);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetNuevoPreFacturas_PagoLinea", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int GetNuevoPreFacturas_PagoLinea(long Contrato, long Clv_Session, decimal Importe);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDime_Yahubo_pagoLinea", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        YaHuboPagoEntity GetDime_Yahubo_pagoLinea(long Contrato);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetReporteTicket", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string GetReporteTicket(long? Clv_Factura);

    }
}

