
using SoftvWCFService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using System.IO;

namespace SoftvWCFService.Contracts
{
    [AuthenticatingHeader]
    [ServiceContract]
    public interface IContratoMaestroFac
    {
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetContratoMaestroFac", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ContratoMaestroFacEntity GetContratoMaestroFac(long? IdContratoMaestro);


        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeepContratoMaestroFac", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ContratoMaestroFacEntity GetDeepContratoMaestroFac(long? IdContratoMaestro);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetContratoMaestroFacList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<ContratoMaestroFacEntity> GetContratoMaestroFacList();

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "AddContratoMaestroFac", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? AddContratoMaestroFac(ContratoMaestroFacEntity objContratoMaestroFac);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "UpdateContratoMaestroFac", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? UpdateContratoMaestroFac(ContratoMaestroFacEntity objContratoMaestroFac);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "DeleteContratoMaestroFac", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? DeleteContratoMaestroFac(String BaseRemoteIp, int BaseIdUser, long? IdContratoMaestro);



        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetBusquedaContratoMaestroFac", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<ContratoMaestroFacEntity> GetBusquedaContratoMaestroFac(String RazonSocial, String NombreComercial, int? ClvCiudad, int? Op);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetAddRelContratoMaestroContrato", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<ContratoMaestroFacEntity> GetAddRelContratoMaestroContrato(ContratoMaestroFacEntity objRep, List<RelContratoMaestro_ContratoSoftvEntity> lstRel);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetContratos_CS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<ContratoMaestroFacEntity> GetContratos_CS();

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetRelContratos", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<ContratoMaestroFacEntity> GetRelContratos(long? IdContratoMaestro);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetRomperRelContratoMaestroContrato", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<ContratoMaestroFacEntity> GetRomperRelContratoMaestroContrato(ContratoMaestroFacEntity objRep, List<RelContratoMaestro_ContratoSoftvEntity> lstRel);

    

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetAddUpdate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<ContratoMaestroFacEntity> GetAddUpdate(ContratoMaestroFacEntity objCM, List<ContratoMaestroFacEntity> Contratos);
              

        

        

    }
}

