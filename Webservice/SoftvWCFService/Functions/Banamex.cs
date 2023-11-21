using System.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using SoftvWCFService.Entities;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.IO;

namespace SoftvWCFService.Functions
{


    //---------- Get Create Checkout Session
    public class apiMerchant
    {
        public String apiOperation { get; set; }
        public order order { get; set; }
        public interaction interaction { get; set; }

    }

    public class order
    {
        public String id { get; set; }
        public String currency { get; set; }
        public String amount { get; set; }
        public String reference { get; set; }

    }


    public class interaction //no cambiar 
    {
        public String returnUrl { get; set; }
        public String cancelUrl { get; set; }
        public int? timeout { get; set; }
        public String timeoutUrl { get; set; }
        public String operation { get; set; }

    }

    public class merchant
    {
        public String name { get; set; }
        public String logo { get; set; }
    }


    //------------Get response Create Checkout Session

    public class Session2
    {
        public string id { get; set; }
        public string updateStatus { get; set; }
        public string version { get; set; }
    }
    public class Banamex : DataAccess
    {
        public int AddBanamex(BanamexEntity entity_Banamex)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_BanamexAdd", connection);

                AssingParameter(comandoSql, "@idInt", entity_Banamex.idInt);

                AssingParameter(comandoSql, "@merchant", entity_Banamex.merchant);

                AssingParameter(comandoSql, "@result", entity_Banamex.result);

                AssingParameter(comandoSql, "@successIndicator", entity_Banamex.successIndicator);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding Banamex " + ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }
                result = (int)comandoSql.Parameters["@IdBanamex"].Value;
            }
            return result;
        }

        /// <summary>
        /// Deletes a Banamex
        ///</summary>
        /// <param name="">   to delete </param>
        public int DeleteBanamex()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_BanamexDelete", connection);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting Banamex " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Edits a Banamex
        ///</summary>
        /// <param name="Banamex"> Objeto Banamex a editar </param>
        public int EditBanamex(BanamexEntity entity_Banamex)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_BanamexEdit", connection);

                AssingParameter(comandoSql, "@idInt", entity_Banamex.idInt);
                AssingParameter(comandoSql, "@merchant", entity_Banamex.merchant);
                AssingParameter(comandoSql, "@result", entity_Banamex.result);
                AssingParameter(comandoSql, "@successIndicator", entity_Banamex.successIndicator);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    result = int.Parse(ExecuteNonQuery(comandoSql).ToString());

                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error updating Banamex " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }




        public string llamada = "https://evopaymentsmexico.gateway.mastercard.com/api/rest/version/55/merchant/";

        public async Task<String> sendData(String merchantId, String token, apiMerchant obj)
        {


            string resourceAddress = llamada + merchantId + "/session";

            var httpClient = new HttpClient();
            var postBody = new JavaScriptSerializer().Serialize(obj);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("BASIC", token);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpResponseMessage response = await httpClient.PostAsync(resourceAddress, new StringContent(postBody, Encoding.UTF8, "application/json"));

            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody.ToString();
        }


        public String GetCreateCheckoutSession(String merchantId, long? idInt, String token, String apiOperation,
            String id, String amount, String currency, String referenciaDePedido, String returnUrl, String cancelUrl, String logoUrl, int? timeout, String timeoutUrl)
        {
            var x = "";
            try
            {
                var obj = new apiMerchant
                {
                    apiOperation = apiOperation,
                    order = new order
                    {
                        id = id,
                        amount = amount, // amount.Replace("\n", ""),
                        currency = currency,
                        reference = referenciaDePedido
                    },
                    interaction = new interaction
                    {
                        operation = "PURCHASE",//"VERIFY",//"NONE", -- No cambiar PURCHASE
                        returnUrl = returnUrl,
                        cancelUrl = cancelUrl,
                        timeout = timeout,
                        timeoutUrl = timeoutUrl
                    }
                };

                Task<String> task = sendData(merchantId, token, obj);
                task.Wait();
                x = task.Result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.Message, ex);
            }
            return x;
        }



        /*
         public override List<BanamDatosMerchantEntity> GetDatosMerchant_freeAuth()
         {
             List<BanamDatosMerchantEntity> BanamexList = new List<BanamDatosMerchantEntity>();

             using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.Banamex.ConnectionString))
             {
                 SqlCommand comandoSql = CreateCommand("DameComercioData_Ecom", connection);
                 comandoSql.CommandTimeout = 20;
                 IDataReader rd = null;

                 try
                 {

                     if (connection.State == ConnectionState.Closed)
                         connection.Open();
                     rd = ExecuteReader(comandoSql);

                     while (rd.Read())
                     {
                         BanamexList.Add(GetBanamDatosMerchantFromReader(rd));
                     }
                 }
                 catch (Exception ex)
                 {
                     throw new Exception("Error getting Merchant Datos " + ex.Message, ex);
                 }
                 finally
                 {

                     if (connection != null)
                         connection.Close();
                     if (rd != null)
                         rd.Close();
                 }
             }
             return BanamexList;
         }
         */










        public String GetRetrieve(long? idInt, String merchantId, String sessionId, String token, long? clv_session)
        {
            Task<String> task = GetRetrieveOrder(merchantId, clv_session, token);
            task.Wait();
            var x = task.Result;

            GuardaRetrieve(x, sessionId, clv_session);

            return x;
        }


        public String GetRetrieveNuevo(long? idInt, long? clv_session)
        {
            String sessionId = "";
            String merchantId = "";
            String userid = "";
            String password = "";

            List<BanamDatosMerchantEntity> BanamexList = new List<BanamDatosMerchantEntity>();

            BanamexList = GetDatosMerchant(0);

            foreach (var ban in BanamexList)
            {
                merchantId = ban.merchantId;
                userid = ban.userId;
                password = ban.password;
            }

            String toEncode = (userid + ":" + password);
            String token = EncodeTo64(toEncode);

            var x = "";

            Task<String> task = GetRetrieveOrder(merchantId, clv_session, token);
            task.Wait();
            x = task.Result;


            GuardaRetrieve(x, sessionId, clv_session);

            return x;
        }



        static public string EncodeTo64(string toEncode)
        {

            byte[] toEncodeAsBytes

                  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);

            string returnValue

                  = System.Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;

        }





        public void GuardaRetrieve(String dataRetrieve, String sessionId, long? clv_session)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    SqlCommand comandoSql = CreateCommand("GuardaRetrieve_EcommerceNew", connection); //GuardaRetrieve_ecommerce
                    // AssingParameter(comandoSql, "@sessionId", sessionId);
                    AssingParameter(comandoSql, "@dataRetrieve", dataRetrieve);
                    AssingParameter(comandoSql, "@clv_session", clv_session);
                    comandoSql.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error Guarda retrieve " + ex.Message, ex);
                }
                finally
                {

                    if (connection != null)
                        connection.Close();
                }

            }
        }




        public async Task<String> GetRetrieveOrder(String merchantId, long? clv_session, String token)
        {

            string resourceAddress = llamada + merchantId + "/order/" + clv_session;

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("BASIC", token);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpResponseMessage response = await httpClient.GetAsync(resourceAddress);
            //response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody.ToString();
        }


        public async Task<String> GetRetrieveAnswer(String merchantId, String sessionId, String token)
        {

            string resourceAddress = llamada + merchantId + "/session/" + sessionId;

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("BASIC", token);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpResponseMessage response = await httpClient.GetAsync(resourceAddress);
            //response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody.ToString();
        }




        public List<BanamexGuardaPagoEntity> GetGuardaPagoEnLinea(long? clv_Session, long? contrato)
        {
            List<BanamexGuardaPagoEntity> BanamexList = new List<BanamexGuardaPagoEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                SqlCommand comandoSql = CreateCommand("GuardaPagoEnLineaEcommerce", connection);
                AssingParameter(comandoSql, "@Clv_Session", clv_Session);
                AssingParameter(comandoSql, "@Contrato", contrato);
                comandoSql.CommandTimeout = 100;

                IDataReader rd = null;

                try
                {

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        BanamexList.Add(GetBanamGuardaPagoFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    
                    //GuardaPagoEnLineaEcommerce_ERROR(clv_Session, contrato); //Para revisar en bitácora
                    throw new Exception("Error getting data Guarda pago en linea " + ex.Message, ex);

                }
                finally
                {

                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return BanamexList;
        }









        public List<BanamexEntity> GetGuarda_ReturnData(long? clv_Session, String retrieveData, String resultIndicator, string amount, string description, string id, string brand, string transactionId, int? conOsinError)
        {
            List<BanamexEntity> BanamexList = new List<BanamexEntity>();

            XmlDocument xmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode(retrieveData, "root");

            // Now create StringWriter object to get data from xml document.
            StringWriter sw = new StringWriter();
            XmlTextWriter xw = new XmlTextWriter(sw);
            xmlDoc.WriteTo(xw);
            String XmlString = sw.ToString();

            Console.WriteLine(xmlDoc);
            Console.WriteLine(XmlString);

            //obtener variables desde json
            // string amount, string description, string id, string brand


            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                SqlCommand comandoSql = CreateCommand("GuardaReturnData_EcomNew", connection); // GuardaReturnData_Ecom
                AssingParameter(comandoSql, "@Clv_Session", clv_Session);
                AssingParameter(comandoSql, "@xml", XmlString);
                AssingParameter(comandoSql, "@resultIndicator", resultIndicator);
                AssingParameter(comandoSql, "@conOsinError", conOsinError);
                comandoSql.CommandTimeout = 100;

                IDataReader rd = null;

                try
                {

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    //var seGuardoConExito = Int32.Parse(rd[0].ToString());


                    /* // descomentar para enviar correo
                    while (rd.Read())
                    {
                       BanamexList.Add(GetBanamexFromReader(rd));
                    }

                    if (conOsinError == 0)
                    {
                        EnviaReciboEcomm(amount, description, id, brand, transactionId);
                    }
                    */



                    //if (seGuardoConExito == 1)
                    //{
                    //    EnviaReciboEcomm(amount, description, id, brand, transactionId);
                    //}
                    //else {
                    //    EnviaReciboEcomm_ERROR(amount, description, id, brand, transactionId); //Para revisar en bitácora
                    //}                  

                }
                catch (Exception ex)
                {
                    //GuardaDatosDelError(clv_Session, retrieveData, resultIndicator, amount, description, id, brand, transactionId, ex.ToString() );
                    throw new Exception("Error getting Return Data " + ex.Message, ex);
                }
                finally
                {

                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }

            //try
            //{
            //    EnviaReciboEcomm(amount, description, id, brand, transactionId);
            //}
            //catch (Exception ex)
            //{

            //}
            return BanamexList;
        }



        public void GuardaDatosDelError(long? clv_Session, String retrieveData, String resultIndicator, string amount, string description, string id, string brand, string transactionId, string error)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    SqlCommand comandoSql = CreateCommand("GuardaDatosDelError_ecommerce", connection);
                    AssingParameter(comandoSql, "@Clv_Session", clv_Session);
                    AssingParameter(comandoSql, "@retrieveData", retrieveData);
                    AssingParameter(comandoSql, "@resultIndicator", resultIndicator);
                    AssingParameter(comandoSql, "@error", error);
                    AssingParameter(comandoSql, "@ID", id);
                    AssingParameter(comandoSql, "@MONTO", amount);
                    AssingParameter(comandoSql, "@METODO", brand);
                    AssingParameter(comandoSql, "@DESCRIPCION", description);
                    AssingParameter(comandoSql, "@TransactionId", transactionId);
                    comandoSql.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error GuardaDatosDelError_ecommerce " + ex.Message, ex);
                }
                finally
                {

                    if (connection != null)
                        connection.Close();
                }

            }
        }


        public void EnviaReciboEcomm(string amount, string description, string id, string brand, string transactionId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    SqlCommand comandoSql = CreateCommand("MandaCorreoRecibo_ecommerce", connection);
                    AssingParameter(comandoSql, "@ID", id);
                    AssingParameter(comandoSql, "@MONTO", amount);
                    AssingParameter(comandoSql, "@METODO", brand);
                    AssingParameter(comandoSql, "@DESCRIPCION", description);
                    AssingParameter(comandoSql, "@TransactionId", transactionId);
                    comandoSql.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error MandaCorreoRecibo_ecommerce " + ex.Message, ex);
                }
                finally
                {

                    if (connection != null)
                        connection.Close();
                }

            }
        }



        public List<BanamexSessionBancoEntity> GetIdSessionTransaccion(long? contrato, String resultIndicator)
        {
            List<BanamexSessionBancoEntity> BanamexList = new List<BanamexSessionBancoEntity>();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                SqlCommand comandoSql = CreateCommand("GetIdSession_delBanco_Ecommerce", connection);
                AssingParameter(comandoSql, "@contrato", contrato);
                AssingParameter(comandoSql, "@resultIndicator", resultIndicator);
                comandoSql.CommandTimeout = 100;

                IDataReader rd = null;

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        BanamexList.Add(GetIdSessionTransaccionFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting Return Data " + ex.Message, ex);
                }
                finally
                {

                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return BanamexList;
        }







        public List<BanamDatosMerchantEntity> GetDatosMerchant(long Contrato)
        {
            List<BanamDatosMerchantEntity> BanamexList = new List<BanamDatosMerchantEntity>();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                SqlCommand comandoSql = CreateCommand("DameComercioData_EcomNew", connection);
                AssingParameter(comandoSql, "@Contrato", Contrato);
                comandoSql.CommandTimeout = 50;

                IDataReader rd = null;

                try
                {

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        BanamexList.Add(GetBanamDatosMerchantFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting Merchant Datos New " + ex.Message, ex);
                }
                finally
                {

                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return BanamexList;
        }





        /// <summary>
        /// Gets all Usuario
        ///</summary>
        public List<LinkEntity> GetLinkRegistro(int? id)
        {
            List<LinkEntity> linkList = new List<LinkEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("DameLinkDeRegistro_Ecom", connection);
                // AssingParameter(comandoSql, "@successIndicator", @successIndicator);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        linkList.Add(getLinkFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Usuario " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return linkList;
        }




        public int GetGuardaMovimiento(long? clv_Session, long? contrato, string navegador, string ip)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {

                    SqlCommand comandoSql = CreateCommand("GuardaMovimientoEcommerce", connection);
                    AssingParameter(comandoSql, "@Clv_Session", clv_Session);
                    AssingParameter(comandoSql, "@contrato", contrato);
                    AssingParameter(comandoSql, "@navegador", navegador);
                    AssingParameter(comandoSql, "@ip", ip);


                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }




        public int GetGuarda_Redireccion(long? idInt, long? nivel, long? contrato, long? clv_session, String sessionId, String laUrl, String resultIndicator)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    SqlCommand strSQL = new SqlCommand("GuardaRedireccion_EcommerceNew"); // GuardaRedireccion_Ecommerce
                    strSQL.Connection = connection;
                    strSQL.CommandType = CommandType.StoredProcedure;
                    strSQL.CommandTimeout = 0;

                    SqlParameter par1 = new SqlParameter("@redireccion", SqlDbType.Int);
                    par1.Value = nivel;
                    par1.Direction = ParameterDirection.Input;
                    strSQL.Parameters.Add(par1);

                    SqlParameter par2 = new SqlParameter("@contrato", SqlDbType.BigInt);
                    par2.Value = contrato;
                    par2.Direction = ParameterDirection.Input;
                    strSQL.Parameters.Add(par2);

                    SqlParameter par3 = new SqlParameter("@clv_session", SqlDbType.BigInt);
                    par3.Value = clv_session;
                    par3.Direction = ParameterDirection.Input;
                    strSQL.Parameters.Add(par3);


                    SqlParameter par4 = new SqlParameter("@sessionId", SqlDbType.VarChar);
                    par4.Value = sessionId;
                    par4.Direction = ParameterDirection.Input;
                    strSQL.Parameters.Add(par4);

                    SqlParameter par5 = new SqlParameter("@laUrl", SqlDbType.VarChar);
                    par5.Value = laUrl;
                    par5.Direction = ParameterDirection.Input;
                    strSQL.Parameters.Add(par5);

                    SqlParameter par6 = new SqlParameter("@resultIndicator", SqlDbType.VarChar);
                    par6.Value = resultIndicator;
                    par6.Direction = ParameterDirection.Input;
                    strSQL.Parameters.Add(par6);

                    SqlParameter par7 = new SqlParameter("@hazRetrieveData", SqlDbType.Int);
                    par7.Direction = ParameterDirection.Output;
                    strSQL.Parameters.Add(par7);

                    strSQL.ExecuteNonQuery();
                    // result = int.Parse(par2.Value.ToString());
                    result = int.Parse(par7.Value.ToString());

                }
                catch (Exception ex)
                {
                    throw new Exception("Error guardando redireccion " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }

            }
            return result;
        }




        public int GetGuarda_Redireccion_respaldo(long? idInt, long? nivel, long? contrato, long? clv_session, String sessionId, String laUrl)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("GuardaRedireccion_Ecommerce", connection);
                AssingParameter(comandoSql, "@redireccion", nivel);
                AssingParameter(comandoSql, "@contrato", contrato);
                AssingParameter(comandoSql, "@clv_session", clv_session);
                AssingParameter(comandoSql, "@sessionId", sessionId);
                AssingParameter(comandoSql, "@laUrl", laUrl);


                //IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    //result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    throw new Exception("Error guardando redireccion " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }

            }
            return result;
        }






        public int GetGuarda_Redireccion_respaldoNuevo(long? idInt, long? nivel, long? contrato, long? clv_session, String sessionId, String laUrl)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("GuardaRedireccion_Ecommerce", connection);
                AssingParameter(comandoSql, "@redireccion", nivel);
                AssingParameter(comandoSql, "@contrato", contrato);
                AssingParameter(comandoSql, "@clv_session", clv_session);
                AssingParameter(comandoSql, "@sessionId", sessionId);
                AssingParameter(comandoSql, "@laUrl", laUrl);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error guardando redireccion " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }





        public int GetGuarda_IdSessionByMovimiento(long? clv_Session, String idSessionBanco, String successIndicator)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    SqlCommand comandoSql = CreateCommand("Guarda_IdSession_Ecommerce", connection);
                    AssingParameter(comandoSql, "@Clv_Session", clv_Session);
                    AssingParameter(comandoSql, "@idSession", idSessionBanco);
                    AssingParameter(comandoSql, "@successIndicator", successIndicator);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }





        public List<ValidaContratoNoMaestroEntity> GetValidaNoContratoMaestro(long? contrato)
        {
            List<ValidaContratoNoMaestroEntity> BanamexList = new List<ValidaContratoNoMaestroEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                SqlCommand comandoSql = CreateCommand("ValidaNoContratoMaestro_Ecom", connection);
                AssingParameter(comandoSql, "@Contrato", contrato);
                IDataReader rd = null;

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        BanamexList.Add(GetValidaContratoNoMaestroFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data Valida Contrato " + ex.Message, ex);
                }
                finally
                {

                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return BanamexList;
        }

        public List<BanamexBuscaEntity> GetBuscaMovimiento(long? clv_Session)
        {
            List<BanamexBuscaEntity> BanamexList = new List<BanamexBuscaEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                SqlCommand comandoSql = CreateCommand("BuscaMovimientoEcommerce", connection);
                AssingParameter(comandoSql, "@Clv_Session", clv_Session);
                IDataReader rd = null;

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        BanamexList.Add(GetBanamexBusca(rd));
                    }
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting busca movimiento " + ex.Message, ex);
                }
                finally
                {

                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return BanamexList;
        }





        /// <summary>
        /// Gets all Banamex by List<int>
        ///</summary>
        public List<BanamexEntity> GetBanamex(List<int> lid)
        {
            List<BanamexEntity> BanamexList = new List<BanamexEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                DataTable IdDT = BuildTableID(lid);

                SqlCommand comandoSql = CreateCommand("Softv_BanamexGetByIds", connection);
                AssingParameter(comandoSql, "@IdTable", IdDT);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        BanamexList.Add(GetBanamexFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data Banamex " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return BanamexList;
        }

        /// <summary>
        /// Gets Banamex by
        ///</summary>
        public BanamexEntity GetBanamexById()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_BanamexGetById", connection);
                BanamexEntity entity_Banamex = null;


                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
                    if (rd.Read())
                        entity_Banamex = GetBanamexFromReader(rd);
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data Banamex " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_Banamex;
            }

        }



        /// <summary>
        ///Get Banamex
        ///</summary>
        public SoftvList<BanamexEntity> GetPagedList(int pageIndex, int pageSize)
        {
            SoftvList<BanamexEntity> entities = new SoftvList<BanamexEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_BanamexGetPaged", connection);

                AssingParameter(comandoSql, "@pageIndex", pageIndex);
                AssingParameter(comandoSql, "@pageSize", pageSize);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);
                    while (rd.Read())
                    {
                        entities.Add(GetBanamexFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data Banamex " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetBanamexCount();
                return entities ?? new SoftvList<BanamexEntity>();
            }
        }

        /// <summary>
        ///Get Banamex
        ///</summary>
        public SoftvList<BanamexEntity> GetPagedList(int pageIndex, int pageSize, String xml)
        {
            SoftvList<BanamexEntity> entities = new SoftvList<BanamexEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_BanamexGetPagedXml", connection);

                AssingParameter(comandoSql, "@pageSize", pageSize);
                AssingParameter(comandoSql, "@pageIndex", pageIndex);
                AssingParameter(comandoSql, "@xml", xml);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);
                    while (rd.Read())
                    {
                        entities.Add(GetBanamexFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data Banamex " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetBanamexCount(xml);
                return entities ?? new SoftvList<BanamexEntity>();
            }
        }

        /// <summary>
        ///Get Count Banamex
        ///</summary>
        public int GetBanamexCount()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_BanamexGetCount", connection);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data Banamex " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }


        /// <summary>
        ///Get Count Banamex
        ///</summary>
        public int GetBanamexCount(String xml)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_BanamexGetCountXml", connection);

                AssingParameter(comandoSql, "@xml", xml);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data Banamex " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }



        public List<BanamexBuscaEntity> GetNotificacion(String response)
        {

            List<BanamexBuscaEntity> BanamexList = new List<BanamexBuscaEntity>();

            /*
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.Banamex.ConnectionString))
            {
                SqlCommand comandoSql = CreateCommand("BuscaMovimientoEcommerce", connection);
                AssingParameter(comandoSql, "@Clv_Session", clv_Session);
                IDataReader rd = null;

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        BanamexList.Add(GetBanamexBusca(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting busca movimiento " + ex.Message, ex);
                }
                finally
                {

                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }*/
            return BanamexList;
        }


        public DatosMovimientoEntity GetObtieneDatosMovimiento(long? clv_Session, long? contrato, string navegador, string ip, decimal importe, string contratocompuesto)
        {
            DatosMovimientoEntity result = new DatosMovimientoEntity();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {

                    SqlCommand comandoSql = CreateCommand("GuardaMovimientoEcommerce", connection);
                    AssingParameter(comandoSql, "@Clv_Session", clv_Session);
                    AssingParameter(comandoSql, "@contrato", contrato);
                    AssingParameter(comandoSql, "@navegador", navegador);
                    AssingParameter(comandoSql, "@ip", ip);


                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    ExecuteNonQuery(comandoSql);

                    BanamDatosMerchantEntity datosBanamex = new BanamDatosMerchantEntity();


                    SqlCommand comandoSqlObtieneDatosBanamex = CreateCommand("DameComercioData_EcomNew", connection);
                    AssingParameter(comandoSqlObtieneDatosBanamex, "@Contrato", contrato);
                    comandoSqlObtieneDatosBanamex.CommandTimeout = 50;

                    IDataReader rd = null;

                    rd = ExecuteReader(comandoSqlObtieneDatosBanamex);

                    if (rd.Read())
                    {
                        datosBanamex = GetBanamDatosMerchantFromReader(rd);
                    }

                    rd.Close();

                    byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(datosBanamex.userId + ":" + datosBanamex.password);
                    string token = Convert.ToBase64String(toEncodeAsBytes.ToArray());

                    var x = "";
                    var obj = new apiMerchant
                    {
                        apiOperation = "CREATE_CHECKOUT_SESSION",
                        order = new order
                        {
                            id = clv_Session.ToString(),
                            amount = importe.ToString(),
                            currency = "MXN",
                            reference = contratocompuesto
                        },
                        interaction = new interaction
                        {
                            operation = "PURCHASE",
                            returnUrl = datosBanamex.returnUrl,
                            cancelUrl = datosBanamex.cancelUrl,
                            timeout = datosBanamex.timeoutSeg,
                            timeoutUrl = datosBanamex.timeoutUrl
                        }
                    };

                    Task<String> task = sendData(datosBanamex.merchantId, token, obj);
                    task.Wait();
                    x = task.Result;

                    Dictionary<string, string> datosMovimiento = new Dictionary<string, string>();

                    JObject objDatosMovimiento = JObject.Parse(x);

                    foreach (JProperty p in objDatosMovimiento.Properties())
                    {
                        if (p.Name.ToString() == "session")
                        {
                            JObject objSession = JObject.Parse(p.Value.ToString());
                            foreach (JProperty pSession in objSession.Properties())
                            {
                                datosMovimiento[pSession.Name.ToString()] = pSession.Value.ToString();
                            }
                        }
                        else
                        {
                            datosMovimiento[p.Name.ToString()] = p.Value.ToString();
                        }
                    }


                    SqlCommand comandoSqlGuardaMovimiento = CreateCommand("Guarda_IdSession_Ecommerce", connection);
                    AssingParameter(comandoSqlGuardaMovimiento, "@Clv_Session", clv_Session);
                    AssingParameter(comandoSqlGuardaMovimiento, "@idSession", datosMovimiento["id"]);
                    AssingParameter(comandoSqlGuardaMovimiento, "@successIndicator", datosMovimiento["successIndicator"]);

                    comandoSqlGuardaMovimiento.ExecuteNonQuery();

                    result.addressLine1 = datosBanamex.addressLine1;
                    result.addressLine2 = datosBanamex.addressLine2;
                    result.descripcionImporte = datosBanamex.descripcionImporte;
                    result.email = datosBanamex.email;
                    result.merchantName = datosBanamex.merchantName;
                    result.sessionId = datosMovimiento["id"].ToString();
                    result.userID = datosBanamex.userId;
                    result.merchantId = datosBanamex.merchantId;

                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }

        protected virtual BanamDatosMerchantEntity GetBanamDatosMerchantFromReader(IDataReader reader)
        {
            BanamDatosMerchantEntity entity_merchantData = null;
            try
            {
                entity_merchantData = new BanamDatosMerchantEntity();
                entity_merchantData.merchantId = (String)(GetFromReader(reader, "merchantId", IsString: true));
                entity_merchantData.userId = (String)(GetFromReader(reader, "userId", IsString: true));
                entity_merchantData.password = (String)(GetFromReader(reader, "password", IsString: true));
                entity_merchantData.merchantName = (String)(GetFromReader(reader, "merchantName", IsString: true));
                entity_merchantData.addressLine1 = (String)(GetFromReader(reader, "addressLine1", IsString: true));
                entity_merchantData.addressLine2 = (String)(GetFromReader(reader, "addressLine2", IsString: true));
                entity_merchantData.email = (String)(GetFromReader(reader, "email", IsString: true));
                entity_merchantData.descripcionImporte = (String)(GetFromReader(reader, "descripcionImporte", IsString: true));
                //nuevos
                entity_merchantData.cancelUrl = (String)(GetFromReader(reader, "cancelUrl", IsString: true));
                entity_merchantData.returnUrl = (String)(GetFromReader(reader, "returnUrl", IsString: true));
                entity_merchantData.timeoutSeg = (int?)(GetFromReader(reader, "timeoutSeg"));
                entity_merchantData.timeoutUrl = (String)(GetFromReader(reader, "timeoutUrl", IsString: true));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Banamex data to entity", ex);
            }
            return entity_merchantData;
        }



        protected virtual LinkEntity getLinkFromReader(IDataReader reader)
        {
            LinkEntity entityLink = null;
            try
            {
                entityLink = new LinkEntity();
                entityLink.linkRegistro = (String)(GetFromReader(reader, "linkRegistro", IsString: true));
                entityLink.linkRecuperaPassword = (String)(GetFromReader(reader, "linkRecuperaPassword", IsString: true));
            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Usuario data to entity", ex);
            }
            return entityLink;
        }

        protected virtual BanamexEntity GetBanamexFromReader(IDataReader reader)
        {
            BanamexEntity entity_Banamex = null;
            try
            {
                entity_Banamex = new BanamexEntity();
                entity_Banamex.idInt = (long?)(GetFromReader(reader, "idInt"));
                entity_Banamex.merchant = (String)(GetFromReader(reader, "merchant", IsString: true));
                entity_Banamex.result = (String)(GetFromReader(reader, "result", IsString: true));
                entity_Banamex.successIndicator = (String)(GetFromReader(reader, "successIndicator", IsString: true));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Banamex data to entity", ex);
            }
            return entity_Banamex;
        }

        protected virtual BanamexSessionBancoEntity GetIdSessionTransaccionFromReader(IDataReader reader)
        {
            BanamexSessionBancoEntity entity_Banamex = null;
            try
            {
                entity_Banamex = new BanamexSessionBancoEntity();
                entity_Banamex.clv_session = (long?)(GetFromReader(reader, "clv_session"));
                entity_Banamex.idSession = (String)(GetFromReader(reader, "idSession", IsString: true));
            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Banamex data to entity", ex);
            }
            return entity_Banamex;
        }

        protected virtual BanamexGuardaPagoEntity GetBanamGuardaPagoFromReader(IDataReader reader)
        {
            BanamexGuardaPagoEntity entity_BanamexGuarda = null;
            try
            {
                entity_BanamexGuarda = new BanamexGuardaPagoEntity();
                entity_BanamexGuarda.BndError = (int?)(GetFromReader(reader, "BndError"));
                entity_BanamexGuarda.MsgError = (String)(GetFromReader(reader, "MsgError", IsString: true));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Banamex data to entity", ex);
            }
            return entity_BanamexGuarda;
        }

        protected virtual BanamexBuscaEntity GetBanamexBusca(IDataReader reader)
        {
            BanamexBuscaEntity entity_Banamex = null;
            try
            {
                entity_Banamex = new BanamexBuscaEntity();
                entity_Banamex.existeOno = (int?)(GetFromReader(reader, "existeOno"));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Banamex data to entity", ex);
            }
            return entity_Banamex;
        }

        protected virtual ValidaContratoNoMaestroEntity GetValidaContratoNoMaestroFromReader(IDataReader reader)
        {
            ValidaContratoNoMaestroEntity entity_Valida = null;
            try
            {
                entity_Valida = new ValidaContratoNoMaestroEntity();
                entity_Valida.PerteneceMaestro = (int?)(GetFromReader(reader, "PerteneceMaestro"));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Banamex data to entity", ex);
            }
            return entity_Valida;
        }
    }
}