using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Diagnostics.Eventing;
using System.ServiceModel;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml;
using System.Configuration;
using System.ServiceModel.Web;
using System.Net;
using System.Text;
using System.IO;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

using SoftvWCFService.Entities;
using SoftvWCFService.Functions;

using Newtonsoft.Json;

namespace SoftvWCFService
{
    public class MessageInspector : IDispatchMessageInspector, IServiceBehavior
    {
        #region IDispatchMessageInspector
        List<String> lstInvaliModules;
        List<String> lstInvaliAction;
        public MessageInspector()
        {
            lstInvaliModules = ConfigurationManager.AppSettings["NoRegisterInBitacoraModules"].Split(',').ToList();
            lstInvaliAction = ConfigurationManager.AppSettings["NoRegisterInBitacoraStartWith"].Split(',').ToList();
        }

        public static XmlDocument RemoveXmlns(String xml)
        {
            XDocument d = XDocument.Parse(xml);
            d.Root.Descendants().Attributes().Where(x => x.IsNamespaceDeclaration).Remove();

            d.Root.Descendants().Attributes().Where(x => x.Name.Namespace != "").Remove();

            foreach (var elem in d.Descendants())
                elem.Name = elem.Name.LocalName;

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(d.CreateReader());

            return xmlDocument;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            WebOperationContext.Current.OutgoingResponse.Headers.Remove("Access-Control-Allow-Methods");
            WebOperationContext.Current.OutgoingResponse.Headers.Remove("Access-Control-Allow-Origin");
            WebOperationContext.Current.OutgoingResponse.Headers.Remove("Access-Control-Allow-Headers");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods", "*");   
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Headers", "Authorization,accept,content-type,Unique");      
            var headers = WebOperationContext.Current.OutgoingResponse.Headers;

        }


        private const string BasicAuth = "Basic";
        private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";


        public static string GenerateToken(string username, int expireMinutes = 200)
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

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                var validto = jwtToken.ValidTo;
                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(Secret);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;


                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }

            catch (Exception e)
            {

                // WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"myrealm\"");
                throw new WebFaultException<string>("Acceso no autorizado, favor de validar autenticación", HttpStatusCode.Unauthorized);
                //should write log

            }
        }

        private static bool ValidateToken(string token, out string username)
        {
            username = null;

            var simplePrinciple = GetPrincipal(token);
            var identity = simplePrinciple.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            var usernameClaim = identity.FindFirst(ClaimTypes.Name);
            username = usernameClaim.Value;

            if (string.IsNullOrEmpty(username))
                return false;

            // More validate to check whether username exists in system

            return true;
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.OK;
                return null;
            }
            else
            {
                List<String> lstUriAction = request.Headers.To.ToString().Split('/').ToList();
                String Action = lstUriAction.Last().ToUpper();
                String Module = lstUriAction[lstUriAction.Count() - 2].ToUpper();

                if ((Action == "RETORNOPAGOLINEAEXITOSO" || Action == "RETORNOPAGOLINEAERROR" || Action == "GETREPORTETICKET" || Action == "GETNOTIFICACIONESWEBHOOK") && Module == "ECOM_PAGOENLINEA")
                {
                    //var requestAux = HttpContext.Current.Request;
                    return null;
                }
                else if ((Action == "RETORNOPAGOLINEA") && Module == "ECOM_PAGOENLINEA")
                {
                    ///////////////////////////////////////
                    // Build the body from the form values
                    string body;

                    // Retrieve the base64 encrypted message body
                    using (var ms = new MemoryStream())
                    {
                        using (var xw = XmlWriter.Create(ms))
                        {
                            request.WriteBody(xw);
                            xw.Flush();
                            body = Encoding.UTF8.GetString(ms.ToArray());
                        }
                    }

                    // Trim any characters at the beginning of the string, if they're not a <
                    body = body.Replace("<?xml version=\"1.0\" encoding=\"utf - 8\"?>", "");
                    if (body.Contains("?xml"))
                    {
                        body = body.Substring(body.IndexOf(">") + 1);
                    }
                    body = TrimExtended(body);

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(body);

                    string Json = JsonConvert.SerializeXmlNode(doc);

                    //// Grab base64 binary data from <Binary> XML node
                    //var doc = XDocument.Parse(body);
                    //if (doc.Root == null)
                    //{
                    //    // Unable to parse body
                    //    return null;
                    //}

                    //var node = doc.Root.Elements("Binary").FirstOrDefault();
                    //if (node == null)
                    //{
                    //    // No "Binary" element
                    //    return null;
                    //}

                    ////// Decrypt the XML element value into a string
                    //var bodyBytes = Convert.FromBase64String(node.Value);
                    //var bodyDecoded = Encoding.UTF8.GetString(bodyBytes);

                    ////// Deserialize the form request into the correct data contract
                    //var qss = QueryStringSerializer(bodyDecoded);
                    //var newContract = qss.Deserialize<MyServiceContract>(bodyDecoded);

                    //// Form the new message and set it
                    var newMessage = Message.CreateMessage(OperationContext.Current.IncomingMessageVersion, Action, Json);
                    request = newMessage;
                    return null;
                }
                else
                {
                    if (WebOperationContext.Current.IncomingRequest.Headers["Authorization"] == null)
                    {
                        //WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"myrealm\"");
                        throw new WebFaultException<string>("Acceso no autorizado, favor de validar autenticación", HttpStatusCode.Unauthorized);
                    }
                    else // Decode the header, check password
                    {
                        string encodedUnamePwd = "";
                        string token = "";
                        encodedUnamePwd = GetEncodedCredentialsFromHeader();
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
                                return false;
                            }

                            string credentials = ASCIIEncoding.ASCII.GetString(decodedBytes);
                            string[] authParts = credentials.Split(':');
                            //Usuario objUsuario = new Usuario();
                            Usuario Usuario = new Usuario();
                            UsuarioLoginEntity objUsr = Usuario.GetusuarioByUserAndPass(authParts[0], authParts[1]);
                            if (objUsr == null)
                            {
                                //WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"myrealm\"");
                                throw new WebFaultException<string>("Acceso no autorizado, favor de validar autenticación", HttpStatusCode.Unauthorized);
                            }
                        }
                        else
                        {
                            token = GetTokenFromHeader();
                            if (!string.IsNullOrEmpty(token))
                            {
                                string username;
                                if (ValidateToken(token, out username) == true)
                                {
                                    return null;

                                }

                            }
                            else
                            {
                                // WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"myrealm\"");
                                throw new WebFaultException<string>("Acceso no autorizado, favor de validar autenticación", HttpStatusCode.Unauthorized);
                            }


                        }
                    }
                }
            }

            return null;
        }

        private List<ResultadoPagoLineaEntity> QueryStringSerializer(string body)
        {
            List<ResultadoPagoLineaEntity> result = new List<ResultadoPagoLineaEntity>();
            try
            {
                foreach(string parametro in body.Split('&'))
                {
                    ResultadoPagoLineaEntity aux = new ResultadoPagoLineaEntity();
                    aux.Nombre = parametro.Split('=')[0];
                    aux.Valor = parametro.Split('=')[1];
                    result.Add(aux);
                }
            }
            catch(Exception ex)
            {

            }
            return result;
        }


        private string TrimExtended(string s)
        {
            while (true)
            {
                if (s.StartsWith("<"))
                {
                    // Nothing to do, return the string
                    return s;
                }

                // Replace the first character of the string
                s = s.Substring(1);
                if (!s.StartsWith("<"))
                {
                    continue;
                }
                return s;
            }
        }

        /// <summary>
        /// Basic auth encodes uname and pwd pair. We take the credential string from the HTTP header.
        /// </summary>
        /// <returns></returns>
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

        private static string GetTokenFromHeader()
        {
            WebOperationContext ctx = WebOperationContext.Current;

            // credentials are in the Authorization Header
            string credsHeader = ctx.IncomingRequest.Headers[HttpRequestHeader.Authorization];
            if (credsHeader != null)
            {
                return credsHeader;
            }
            // no auth header was found
            return null;
        }


        #endregion

        #region IServiceBehavior

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (var endpoint in dispatcher.Endpoints)
                {
                    endpoint.DispatchRuntime.MessageInspectors.Add(new MessageInspector());
                }
            }
        }

        public void AddBindingParameters(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
        }

        public void Validate(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase)
        {
        }

        #endregion
    }
}