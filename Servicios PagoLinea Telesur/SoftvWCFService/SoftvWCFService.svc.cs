using SoftvWCFService.Entities;
using SoftvWCFService.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Xml.Linq;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

using SoftvWCFService.Functions;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml;
using System.Data;
using Newtonsoft.Json;
using System.Web.UI.WebControls;

namespace SoftvWCFService
{
    [ScriptService]
    public partial class SoftvWCFService : IUsuario, IModule, IRole, IPermiso,
        ICobraSaldo, IDameClv_Session,
        IEcom_PagoEnLinea, ISumaTotalDetalle
        
    {

        CobraSaldo CobraSaldo = new CobraSaldo();
        DameClv_Session DameClv_Session = new DameClv_Session();
        Session Session = new Session();
        Usuario Usuario = new Usuario();
        Module Module = new Module();
        Role Role = new Role();
        Permiso Permiso = new Permiso();
        SumaTotalDetalle SumaTotalDetalle = new SumaTotalDetalle();
        Ecom_PagoEnLinea Ecom_PagoEnLinea = new Ecom_PagoEnLinea();

        

        #region Usuario
        public UsuarioLoginEntity GetusuarioByUserAndPass(string Usuariox, string Pass)
        {
            return Usuario.GetusuarioByUserAndPass(Usuariox, Pass);
        }

        //public override UsuarioEntity GetDatosCliente(long? Contrato)
        //{
        //    return Usuario.GetDatosCliente(Contrato);
        //}


        private const string BasicAuth = "Basic";

        public UsuarioLoginEntity LogOn()
        {
            var asd = WebOperationContext.Current.IncomingRequest.Method;

            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            if (WebOperationContext.Current.IncomingRequest.Headers["Authorization"] == null)
            {
                WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"myrealm\"");
                throw new WebFaultException<string>("Acceso no autorizado, favor de validar autenticación", HttpStatusCode.Unauthorized);
            }
            else // Decode the header, check password
            {
                string encodedUnamePwd = GetEncodedCredentialsFromHeader();
                if (!string.IsNullOrEmpty(encodedUnamePwd))
                {
                    // Decode the credentials
                    byte[] decodedBytes = null;
                    try
                    {
                        decodedBytes = Convert.FromBase64String(encodedUnamePwd);
                    }
                    catch (FormatException)
                    {

                    }

                    string credentials = ASCIIEncoding.ASCII.GetString(decodedBytes);

                    // Validate User and Password
                    string[] authParts = credentials.Split(':');
                    Usuario objUsuario = new Usuario();
                    UsuarioLoginEntity objUsr = Usuario.GetusuarioByUserAndPass(authParts[0], authParts[1]);

                    if (objUsr != null)
                    {
                       
                        objUsr.Token = GenerateToken(authParts[0]); 

                        return objUsr;
                    }
                    else
                    {
                        WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"myrealm\"");
                        throw new WebFaultException<string>("Acceso no autorizado, favor de validar autenticación", HttpStatusCode.Unauthorized);
                    }
                }
            }

            return new UsuarioLoginEntity();
        }

        private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";

        public static string GenerateToken(string username, int expireMinutes = 3)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
                        new Claim(ClaimTypes.Name, username)
                    }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }

        public UsuarioEntity GetDatosCliente(long? Contrato)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Usuario.GetDatosCliente(Contrato);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        private static string GetEncodedCredentialsFromHeader()
        {
            WebOperationContext ctx = WebOperationContext.Current;

            // credentials are in the Authorization Header
            string credsHeader = ctx.IncomingRequest.Headers[HttpRequestHeader.Authorization];
            if (credsHeader != null)
            {
                // make sure that we have 'Basic' auth header. Anything else can't be handled
                string creds = null;
                int credsPosition = credsHeader.IndexOf(BasicAuth, StringComparison.OrdinalIgnoreCase);
                if (credsPosition != -1)
                {
                    // 'Basic' creds were found
                    credsPosition += BasicAuth.Length + 1;
                    if (credsPosition < credsHeader.Length - 1)
                    {
                        creds = credsHeader.Substring(credsPosition, credsHeader.Length - credsPosition);
                        return creds;
                    }
                    return null;
                }
                else
                {
                    // we did not find Basic auth header but some other type of auth. We can't handle it. Return null.
                    return null;
                }
            }

            // no auth header was found
            return null;
        }




        #endregion

        #region Module


        
        public List<ModuleEntity> GetModulos_Permisos(int? idrol)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Module.GetModulos_Permisos(idrol);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }


        }

        public ModuleEntity GetModule(int? IdModule)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Module.GetModuleById(IdModule);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public ModuleEntity GetDeepModule(int? IdModule)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Module.GetModuleById(IdModule);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }

            }
        }

        public IEnumerable<ModuleEntity> GetModuleList()
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Module.GetModule();
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public SoftvList<ModuleEntity> GetModulePagedList(int page, int pageSize)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Module.GetPagedList(page, pageSize);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public SoftvList<ModuleEntity> GetModulePagedListXml(int page, int pageSize, String xml)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Module.GetPagedList(page, pageSize, xml);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public int? AddModule(ModuleEntity objModule)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Module.AddModule(objModule);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public int? UpdateModule(ModuleEntity objModule)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Module.EditModule(objModule);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public int? DeleteModule(String BaseRemoteIp, int BaseIdUser, int? IdModule)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Module.DeleteModule(IdModule);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        #endregion

        #region Role




        public RoleEntity GetRoleById(int? IdRol)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Role.GetRoleById(IdRol);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }


        }



        public IEnumerable<RoleEntity> GetRoleList()
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Role.GetRole();
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public int? AddRole(RoleEntity objRole)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Role.AddRole(objRole);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public int? UpdateRole(RoleEntity objRole)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Role.EditRole(objRole);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public IEnumerable<RoleEntity> GetUpListPermisos(RoleEntity objRole, List<PermisoEntity> LstPer)
        {

            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                objRole.LstPer = LstPer;

                XElement xe = XElement.Parse(SerializeTool.Serialize<RoleEntity>(objRole));

                XElement xml2 = XElement.Parse(SerializeTool.SerializeList<PermisoEntity>(objRole.LstPer, "objRole"));

                xe.Add(xml2);
                try
                {
                    return Role.GetUpListPermisos(xe.ToString());
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message + " " + xe.ToString(), HttpStatusCode.ExpectationFailed);
                }
            }
        } 

        public IEnumerable<PermisoEntity> GetPermiRolList(int? IdRol)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Permiso.GetPermisoRolList(IdRol);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }




        #endregion

        #region Permiso
        public IEnumerable<PermisoEntity> GetPermisoList()
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    List<PermisoEntity> entities = new List<PermisoEntity>();
                    entities = Permiso.GetPermiso();

                    List<RoleEntity> lRole = Role.GetRole(entities.Where(x => x.IdRol.HasValue).Select(x => x.IdRol.Value).ToList());
                    lRole.ForEach(xRole => entities.Where(x => x.IdRol.HasValue).Where(x => x.IdRol == xRole.IdRol).ToList().ForEach(y => y.Role = xRole));

                    return entities ?? new List<PermisoEntity>();
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        #endregion



        #region CobraSaldo
        public CobraSaldoEntity GetDeepCobraSaldo(long? Contrato)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return CobraSaldo.GetCobraSaldoById(Contrato);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }

            }
        }

        #endregion




        #region DameClv_Session
        public DameClv_SessionEntity GetDameClv_Session(long? Contrato)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return DameClv_Session.GetDameClv_SessionById(Contrato);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public DameClv_SessionEntity GetDeepDameClv_Session(long? Contrato)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return DameClv_Session.GetDameClv_SessionById(Contrato);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }

            }
        }

        public DameClv_SessionEntity GetDeepDameClv_SessionDos(String ContratoCom)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return DameClv_Session.GetOneDeepDos(ContratoCom);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }

            }
        }

        #endregion



        #region SumaTotalDetalle

        public SumaTotalDetalleEntity GetDeepSumaTotalDetalle(long? IdSession)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return SumaTotalDetalle.GetSumaTotalDetalleById(IdSession);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }

            }
        }


        #endregion

        #region Ecom_PagoEnLinea




        public Ecom_CorreoEntity GetCorreo(long? Contrato)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Ecom_PagoEnLinea.GetCorreo(Contrato);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }


        public Ecom_CorreoEntity GetValidaCorreo(int? aux, String Correo)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Ecom_PagoEnLinea.GetValidaCorreo(aux, Correo);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }



        public bool? GetDimeSiHayOtroPagoEnProceso_Softv(long? contrato)
        {

            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Ecom_PagoEnLinea.GetDimeSiHayOtroPagoEnProceso_Softv(contrato);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }

        }



        public Ecom_ContCompEntity GetContratoCompuesto(long? Contrato)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Ecom_PagoEnLinea.GetContratoCompuesto(Contrato);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public ParametrosPagoRedireccionEntity GetGeneraDatosPago(long? Clv_Session, long? Contrato, decimal Total)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Ecom_PagoEnLinea.GetGeneraDatosPago(Clv_Session, Contrato, Total);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public void GetNotificacionesWebhook()
        {
            if (WebOperationContext.Current.IncomingRequest.Method != "OPTIONS")
            {
                try
                {
                    var request = OperationContext.Current.RequestContext.RequestMessage.ToString();

                    DBFuncion db = new DBFuncion();
                    db.agregarParametro("@Notificacion", SqlDbType.VarChar, request);
                    db.consultaSinRetorno("GuardaNotificiacion");

                    request = request.Replace(" type=\"string\"", "").Replace(" type=\"object\"", "").Replace(" type=\"null\"", "").Replace(" type=\"number\"", "");
                    
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(request);

                    string Json = JsonConvert.SerializeXmlNode(doc);

                    Ecom_PagoEnLinea.GetNotificacionesWebhook(Json);
                }
                catch (Exception ex)
                {
                    //throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public IEnumerable<BusCliPorContrato_FacEntity> GetBusCliPorContrato_FacList(int? Id, String ContratoC)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Ecom_PagoEnLinea.GetBusCliPorContrato_Fac(Id, ContratoC);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }


        public IEnumerable<DameDetalleEntity> GetDameDetalleList(long? Clv_Session)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Ecom_PagoEnLinea.GetDameDetalleList(Clv_Session);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public IEnumerable<DameServicioCliente> GetDameServicioCliente(long? Contrato)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Ecom_PagoEnLinea.GetDameServicioCliente(Contrato);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public IEnumerable<SumaDetalleEntity> GetSumaDetalleList(long? Clv_Session)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Ecom_PagoEnLinea.GetSumaDetalleList(Clv_Session);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        //  importeTotalEntity GetImporteTotal(long? Clv_Session);

        public importeTotalEntity GetImporteTotal(long? Clv_Session)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Ecom_PagoEnLinea.GetImporteTotal(Clv_Session);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }


        public IEnumerable<BuscaFacturasHistorialEntity> GetBuscaFacturasHistorialList(int? Id, String Serie, long? Folio, String Fecha, String Tipo, long? ContratoO)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Ecom_PagoEnLinea.GetBuscaFacturasHistorialList(Id, Serie, Folio, Fecha, Tipo, ContratoO);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public tieneEdoCuentaEntity GetDeeptieneEdoCuenta(long? Contrato)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Ecom_PagoEnLinea.GettieneEdoCuentaById(Contrato);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }

            }
        }



       

        public System.IO.Stream RetornoPagoLineaExitoso()
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    var request = HttpContext.Current.Request;
                    var Form = request.Form;

                    string mp_order = Form["mp_order"].FirstOrDefault().ToString();
                    string mp_reference = Form["mp_reference"].FirstOrDefault().ToString();
                    string mp_amount = Form["mp_amount"].FirstOrDefault().ToString();
                    string mp_response = Form["mp_response"].FirstOrDefault().ToString();
                    string mp_responsemsg = Form["mp_responsemsg"].FirstOrDefault().ToString();
                    string mp_authorization = Form["mp_authorization"].FirstOrDefault().ToString();
                    string mp_signature = Form["mp_signature"].FirstOrDefault().ToString();

                    DBFuncion db = new DBFuncion();
                    db.agregarParametro("@Clv_Session", System.Data.SqlDbType.BigInt, mp_order);
                    db.agregarParametro("@Autorizacion", System.Data.SqlDbType.VarChar, mp_authorization);
                    db.agregarParametro("@mp_response", System.Data.SqlDbType.VarChar, mp_response);
                    db.agregarParametro("@mp_responsemsg", System.Data.SqlDbType.VarChar, mp_responsemsg);
                    db.agregarParametro("@Clv_Factura", System.Data.SqlDbType.BigInt, System.Data.ParameterDirection.Output);
                    db.consultaOutput("GuardaRespuestaPagoLinea");
                    long Clv_Factura = long.Parse(db.diccionarioOutput["@Clv_Factura"].ToString());

                    string result = "<html><head>";
                    result += "<script>";
                    result += "function load() {";
                    result += "location.href = 'http://localhost:9000/#!/auth/reciboPublico/" + Clv_Factura.ToString() + "'; ";
                    result += "}";
                    result += "window.onload = load;";
                    result += "</script></head>";
                    result += "<body><h3>VALIDANDO INFORMACION...</h3></body>";
                    result += "<body></body></html>";
                    byte[] resultBytes = Encoding.UTF8.GetBytes(result);
                    WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
                    return new MemoryStream(resultBytes);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public System.IO.Stream RetornoPagoLinea(List<ResultadoPagoLineaEntity> resultado)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    //var request = HttpContext.Current.Request;
                    //var Form = request.Form;

                    //string s_transm = Form["s_transm"].ToString();
                    //string c_referencia = Form["c_referencia"].ToString();
                    //string t_importe = Form["t_importe"].ToString();
                    //string n_autoriz = Form["n_autoriz"].ToString();
                    //string contrato = Form["contrato"].ToString();
                    //string t_pago = Form["t_pago"].ToString();

                    //string val_9 = "";
                    //if (t_pago == "01" || t_pago == "1")
                    //{
                    //    val_9 = Form["val_9"].ToString();
                    //}

                    string s_transm = resultado.Where(x => x.Nombre == "s_transm").FirstOrDefault().Valor;
                    string c_referencia = resultado.Where(x => x.Nombre == "c_referencia").FirstOrDefault().Valor;
                    string t_importe = resultado.Where(x => x.Nombre == "t_importe").FirstOrDefault().Valor;
                    string n_autoriz = resultado.Where(x => x.Nombre == "n_autoriz").FirstOrDefault().Valor;
                    string contrato = resultado.Where(x => x.Nombre == "contrato").FirstOrDefault().Valor;
                    string t_pago = resultado.Where(x => x.Nombre == "t_pago").FirstOrDefault().Valor;

                    string val_9 = "";
                    if (t_pago == "01" || t_pago == "1")
                    {
                        val_9 = resultado.Where(x => x.Nombre == "val_9").FirstOrDefault().Valor;
                    }

                    DBFuncion db = new DBFuncion();
                    db.agregarParametro("@Contrato", System.Data.SqlDbType.BigInt, contrato);
                    db.agregarParametro("@Clv_Session", System.Data.SqlDbType.BigInt, s_transm);
                    db.agregarParametro("@Referencia_Bancaria", System.Data.SqlDbType.VarChar, n_autoriz);
                    db.agregarParametro("@GLOIMPORTETARJETA", System.Data.SqlDbType.Money, t_importe);
                    db.agregarParametro("@T_Pago", System.Data.SqlDbType.Int, t_pago);
                    db.agregarParametro("@N_Tarjeta", System.Data.SqlDbType.VarChar, val_9);
                    db.agregarParametro("@ClvFactura", System.Data.SqlDbType.BigInt, System.Data.ParameterDirection.Output);
                    db.consultaOutput("GuardaPagoenLinea_Bancomer");
                    long Clv_Factura = long.Parse(db.diccionarioOutput["@ClvFactura"].ToString());

                    string result = "<html><head>";
                    result += "<script>";
                    result += "function load() {";
                    //result += "location.href = 'http://localhost:9000/#!/auth/reciboPublico/" + Clv_Factura.ToString() + "'; ";
                    result += "location.href = 'http://www.gigacable.com.mx/pagoenlinea2/app/#!/auth/reciboPublico/" + Clv_Factura.ToString() + "'; ";
                    result += "}";
                    result += "window.onload = load;";
                    result += "</script></head>";
                    result += "<body><h3>VALIDANDO INFORMACION...</h3></body>";
                    result += "<body></body></html>";
                    byte[] resultBytes = Encoding.UTF8.GetBytes(result);
                    WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
                    return new MemoryStream(resultBytes);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public System.IO.Stream RetornoPagoLineaError()
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    var request = HttpContext.Current.Request;
                    var Form = request.Form;

                    string mp_order = Form["mp_order"].FirstOrDefault().ToString();
                    string mp_reference = Form["mp_reference"].FirstOrDefault().ToString();
                    string mp_amount = Form["mp_amount"].FirstOrDefault().ToString();
                    string mp_response = Form["mp_response"].FirstOrDefault().ToString();
                    string mp_responsemsg = Form["mp_responsemsg"].FirstOrDefault().ToString();
                    string mp_authorization = Form["mp_authorization"].FirstOrDefault().ToString();
                    string mp_signature = Form["mp_signature"].FirstOrDefault().ToString();

                    DBFuncion db = new DBFuncion();
                    db.agregarParametro("@Clv_Session", System.Data.SqlDbType.BigInt, mp_order);
                    db.agregarParametro("@Autorizacion", System.Data.SqlDbType.VarChar, mp_authorization);
                    db.agregarParametro("@mp_response", System.Data.SqlDbType.VarChar, mp_response);
                    db.agregarParametro("@mp_responsemsg", System.Data.SqlDbType.VarChar, mp_responsemsg);
                    db.agregarParametro("@Clv_Factura", System.Data.SqlDbType.BigInt, System.Data.ParameterDirection.Output);
                    db.consultaOutput("GuardaRespuestaPagoLinea");
                    //long Clv_Factura = long.Parse(db.diccionarioOutput["@Clv_Factura"].ToString());

                    string result = "<html><head>";
                    result += "<script>";
                    result += "function load() {";
                    result += "location.href = 'http://localhost:9000/#!/home/cancelado';";
                    result += "}";
                    result += "window.onload = load;";
                    result += "</script></head>";
                    result += "<body><h3>Validando información...</h3></body>";
                    result += "<body></body></html>";
                    byte[] resultBytes = Encoding.UTF8.GetBytes(result);
                    WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
                    return new MemoryStream(resultBytes);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public int GetNuevoPreFacturas_PagoLinea(long Contrato, long Clv_Session, decimal Importe)
        {
            
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return 0;
            }
            else
            {
                try
                {
                    return Ecom_PagoEnLinea.GetNuevoPreFacturas_PagoLinea(Contrato, Clv_Session, Importe);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
            
        }

        public YaHuboPagoEntity GetDime_Yahubo_pagoLinea(long Contrato)
        {

            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Ecom_PagoEnLinea.GetDime_Yahubo_pagoLinea(Contrato);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }

        }

        public string GetReporteTicket(long? Clv_Factura)
        {

            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Ecom_PagoEnLinea.GetReporteTicket(Clv_Factura);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }

        }

        public int fnChangePassword(long? Contrato, string Nueva)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return 0;
            }
            else
            {
                try
                {
                    return Ecom_PagoEnLinea.fnChangePassword(Contrato, Nueva);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }

            }
        }

        #endregion

    }
}
