using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using SoftvWCFService.Entities;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using Openpay;
using Openpay.Entities;
using Openpay.Utils;
using Openpay.Entities.Request;
using Newtonsoft.Json.Linq;
using System.Diagnostics.Contracts;

namespace SoftvWCFService.Functions
{
    public class Ecom_PagoEnLinea: DataAccess
    {
        public  Ecom_ContCompEntity GetContratoCompuesto(long? Contrato)
        {

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("ObtieneDatosClientePagoLinea", connection);

                Ecom_ContCompEntity entity_ContComp = null;
                AssingParameter(comandoSql, "@Contrato", Contrato);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    if (rd.Read())
                        entity_ContComp = GetContratoCompuestoFromReader(rd);

                    DBFuncion db = new DBFuncion();
                    db.agregarParametro("@Contrato", SqlDbType.BigInt, Contrato);
                    SqlDataReader reader = db.consultaReader("MuestraArbolServicios_Clientes");
                    entity_ContComp.Servicios = db.MapDataToEntityCollection<ServiciosClienteEntity>(reader).ToList();
                    db.conexion.Close();
                    db.conexion.Dispose();

                    if (entity_ContComp.Servicios.Any(x => x.status == "I" || x.status == "D" || x.status == "S" || x.status == "C"))
                    {
                        entity_ContComp.Servicios = entity_ContComp.Servicios.Where(x => x.status != "B").ToList();
                    }

                    foreach(ServiciosClienteEntity servicioAux in entity_ContComp.Servicios)
                    {
                        if(servicioAux.status == "I")
                        {
                            servicioAux.status = "Instalado";
                        }
                        else if (servicioAux.status == "D")
                        {
                            servicioAux.status = "Desconectado";
                        }
                        else if (servicioAux.status == "S")
                        {
                            servicioAux.status = "Suspendido";
                        }
                        else if (servicioAux.status == "B")
                        {
                            servicioAux.status = "Baja";
                        }
                        else if (servicioAux.status == "F")
                        {
                            servicioAux.status = "Fuera de Área";
                        }
                        else if (servicioAux.status == "C")
                        {
                            servicioAux.status = "Contratado";
                        }
                        else if (servicioAux.status == "T")
                        {
                            servicioAux.status = "Suspensión Temporal";
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data Get Contrato " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_ContComp;
            }

        }

        public ParametrosPagoRedireccionEntity GetGeneraDatosPago(long? Clv_Session, long? Contrato, decimal Total)
        {


            ParametrosPagoRedireccionEntity result = new ParametrosPagoRedireccionEntity();
            ParametrosOpenPayEntity parametrosOpenPay = new ParametrosOpenPayEntity();
            DatosClienteEntity datosCliente = new DatosClienteEntity();

            DBFuncion dbDatosCliente = new DBFuncion();
            dbDatosCliente.agregarParametro("@Contrato", SqlDbType.BigInt, Contrato);
            //Obtiene los datos del cliente
            SqlDataReader readerDatosCliente = dbDatosCliente.consultaReader("ObtieneDatosClientePagoLinea");
            datosCliente = dbDatosCliente.MapDataToEntityCollection<DatosClienteEntity>(readerDatosCliente).FirstOrDefault();
            dbDatosCliente.conexion.Close();
            dbDatosCliente.conexion.Dispose();
            try
            {
                DBFuncion db = new DBFuncion();
                //Obtiene la url para la pasarela de pagos
                SqlDataReader reader = db.consultaReader("ObtieneParametrosOpenPay");
                parametrosOpenPay = db.MapDataToEntityCollection<ParametrosOpenPayEntity>(reader).FirstOrDefault();
                db.conexion.Close();
                db.conexion.Dispose();

                OpenpayAPI api = new OpenpayAPI(parametrosOpenPay.LlavePrivada, parametrosOpenPay.ID);
                api.Production = false;
                ChargeRequest request = new ChargeRequest();
                Customer customer = new Customer();
                customer.Name = datosCliente.SoloNombre;
                customer.LastName = datosCliente.SoloApellidos;
                customer.PhoneNumber = datosCliente.Telefono;
                customer.Email = datosCliente.Email;

                request.Method = "card";
                request.Amount = decimal.Parse(Total.ToString("0.##"));
                request.Description = "Cargo desde terminal virtual de Telesur";
                request.OrderId = Clv_Session.ToString();
                request.Confirm = "false";
                request.SendEmail = true;
                request.RedirectUrl = "https://pagos.micabletelesur.com/#/home";//"http://915009d10274.sn.mynetname.net:10443/SoftvWCFService.svc/Ecom_PagoEnLinea/GetNotificacionesWebhook";
                request.Customer = customer;
                request.Currency = "MXN";

               

                Charge charge = api.ChargeService.Create(request);


                DBFuncion dbGuarda = new DBFuncion();
                dbGuarda.agregarParametro("@Clv_Session", SqlDbType.BigInt, Clv_Session);
                dbGuarda.agregarParametro("@ID", SqlDbType.VarChar, charge.Id);
                dbGuarda.agregarParametro("@Contrato", SqlDbType.BigInt, Contrato);
                dbGuarda.consultaSinRetorno("GuardaSessionOpenPayID");

                result.URLRedireccion = charge.PaymentMethod.Url;
            }
            catch (Exception ex)
            {

                throw new Exception("Error getting data Get Contrato " + ex.Message, ex);
            }
                 
            return result;
        }

        public int fnChangePassword(long? Contrato, String Nueva)
        {

            int result = 0;
            
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            
            SqlCommand comandoSql = CreateCommand("CambiarContraseña", connection);
            
            AssingParameter(comandoSql, "@Contrato", Contrato);
            AssingParameter(comandoSql, "@Nueva", Nueva);
            
            IDataReader rd = null;

            try
            {
                
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                rd = ExecuteReader(comandoSql);

                while (rd.Read())
                {
                    result = (int)rd.GetValue(0);
                }
                

            }
            catch (Exception ex)
            {
                throw new Exception("Error Gettin data Change Password " + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
                if (rd != null)
                    rd.Close();
            }

            return result;

        }
        public void GetNotificacionesWebhook(string JsonRequest)
        {

            try
            {
                string IdOpenPay = "";
                string Clv_Session = "";
                string type = "";
                
                JObject objRoot = JObject.Parse(JsonRequest);
                foreach (JProperty pRoot in objRoot.Properties())
                {
                    if (pRoot.Name == "root")
                    {
                        JObject objPrincipal = JObject.Parse(pRoot.Value.ToString());
                        foreach (JProperty pPrincipal in objPrincipal.Properties())
                        {
                            if (pPrincipal.Name == "type")
                            {
                                type = pPrincipal.Value.ToString();
                            }
                            else if (pPrincipal.Name == "transaction")
                            {
                                JObject objTransaction = JObject.Parse(pPrincipal.Value.ToString());
                                foreach (JProperty pTransaction in objTransaction.Properties())
                                {
                                    if (pTransaction.Name == "id")
                                    {
                                        IdOpenPay = pTransaction.Value.ToString();
                                    }
                                    else if (pTransaction.Name == "order_id")
                                    {
                                        Clv_Session = pTransaction.Value.ToString();
                                    }
                                }
                            }
                        }
                    }
                }

                DBFuncion db = new DBFuncion();
                db.agregarParametro("@Clv_Session", SqlDbType.BigInt, Clv_Session == "" ? 0 : int.Parse(Clv_Session));
                db.agregarParametro("@ID", SqlDbType.VarChar, IdOpenPay);
                db.agregarParametro("@type", SqlDbType.VarChar, type);
                db.agregarParametro("@JsonResponse", SqlDbType.VarChar, JsonRequest);
                db.consultaSinRetorno("AfectaPagoNotificacionOpenPay");
            }
            catch (Exception ex)
            {

                //throw new Exception("Error getting data Get Contrato " + ex.Message, ex);
            }

        }

        public  string GetReporteTicket(long? Clv_Factura)
        {
            string apppath = System.AppDomain.CurrentDomain.BaseDirectory;
            string ruta = apppath + "/ReportesSistema/";
            string name = Guid.NewGuid().ToString() + ".pdf";
            string fileName = apppath + "/Reportes/" + name;
            ReportDocument reportDocument = new ReportDocument();
            string result = "";
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            try
            {

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand comandoSql = CreateCommand("REPORTETicket", connection);
                AssingParameter(comandoSql, "@Clv_Factura", Clv_Factura);
                AssingParameter(comandoSql, "@Clv_Factura_Ini", 0);
                AssingParameter(comandoSql, "@Clv_Factura_Fin", 0);
                AssingParameter(comandoSql, "@Fecha_Ini", DateTime.Today.ToShortDateString());
                AssingParameter(comandoSql, "@Fecha_Fin", DateTime.Today.ToShortDateString());
                AssingParameter(comandoSql, "@op", 0);


                SqlDataAdapter dataAdapter = new SqlDataAdapter(comandoSql);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                dataSet.Tables[0].TableName = "CALLES";
                dataSet.Tables[1].TableName = "CATALOGOCAJAS";
                dataSet.Tables[2].TableName = "CIUDADES";
                dataSet.Tables[3].TableName = "CLIENTES";
                dataSet.Tables[4].TableName = "COLONIAS";
                dataSet.Tables[5].TableName = "DATOSFISCALES";
                dataSet.Tables[6].TableName = "DETFACTURAS";
                dataSet.Tables[7].TableName = "DETFACTURASIMPUESTOS";
                dataSet.Tables[8].TableName = "FACTURAS";
                dataSet.Tables[9].TableName = "GENERALDESCONEXION";
                dataSet.Tables[10].TableName = "ReportesFacturas;1";
                dataSet.Tables[11].TableName = "SUCURSALES";
                dataSet.Tables[12].TableName = "USUARIOS";
                dataSet.Tables[13].TableName = "PROMO";

                reportDocument.Load(ruta + "ReporteTicket.rpt");


                reportDocument.SetDataSource(dataSet);

                SqlCommand comandoSqlDatosGenerales = CreateCommand("DameDatosGenerales_2", connection);
                SqlDataReader reader = comandoSqlDatosGenerales.ExecuteReader();
                string GloEmpresa = "";
                string GloDireccionEmpresa = "";
                string GloColonia_CpEmpresa = "";
                string GloCiudadEmpresa = "";
                string GloRfcEmpresa = "";
                string GloTelefonoEmpresa = "";
                if (reader.Read())
                {
                    GloEmpresa = reader.GetString(1);
                    GloDireccionEmpresa = reader.GetString(2);
                    GloColonia_CpEmpresa = reader.GetString(3);
                    GloCiudadEmpresa = reader.GetString(4);
                    GloRfcEmpresa = reader.GetString(5);
                    GloTelefonoEmpresa = reader.GetString(4);
                }

                reader.Close();

                reportDocument.DataDefinition.FormulaFields["Empresa"].Text = "'" + GloEmpresa + "'";
                reportDocument.DataDefinition.FormulaFields["DireccionEmpresa"].Text = "'" + GloDireccionEmpresa + "'";
                reportDocument.DataDefinition.FormulaFields["Colonia_CpEmpresa"].Text = "'" + GloColonia_CpEmpresa + "'";
                reportDocument.DataDefinition.FormulaFields["CiudadEmpresa"].Text = "'" + GloCiudadEmpresa + "'";
                reportDocument.DataDefinition.FormulaFields["RfcEmpresa"].Text = "'" + GloRfcEmpresa + "'";
                reportDocument.DataDefinition.FormulaFields["TelefonoEmpresa"].Text = "'" + GloTelefonoEmpresa + "'";
                reportDocument.DataDefinition.FormulaFields["Mensaje"].Text = "''";


                reportDocument.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, fileName);
                reportDocument.Dispose();
                reportDocument.Close();
                result = name;
            }
            catch (Exception ex)
            {
                throw new Exception("Error ReportesFacturas " + ex.Message, ex);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            return result;

        }


        /// <summary>
        /// Gets all BusCliPorContrato_Fac
        ///</summary>
        public  List<BusCliPorContrato_FacEntity> GetBusCliPorContrato_Fac(int? Id, String ContratoC)
        {
            List<BusCliPorContrato_FacEntity> BusCliPorContrato_FacList = new List<BusCliPorContrato_FacEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("BusCliPorContrato_Fac", connection);

                AssingParameter(comandoSql, "@Id", Id);
                AssingParameter(comandoSql, "@ContratoC", ContratoC);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        BusCliPorContrato_FacList.Add(GetBusCliPorContrato_FacFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data BusCliPorContrato_Fac " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return BusCliPorContrato_FacList;
        }


        //dame detalle
        /// <summary>
        /// Gets all DameDetalle
        ///</summary>
        public  List<DameDetalleEntity> GetDameDetalleList(long? Clv_Session)
        {
            List<DameDetalleEntity> DameDetalleList = new List<DameDetalleEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("DameDetalleAPP", connection);

                AssingParameter(comandoSql, "@Clv_Session", Clv_Session);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        DameDetalleList.Add(GetDameDetalleListFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data DameDetalle " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return DameDetalleList;
        }

        public List<DameServicioCliente> GetDameServicioCliente(long? Contrato)
        {
            List<DameServicioCliente> DameDetalleList = new List<DameServicioCliente>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("ServiciosdelClienteApp", connection);

                AssingParameter(comandoSql, "@Contrato", Contrato);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        DameDetalleList.Add(GetDameServicioClienteFromReader(rd));
                    }
                }
                catch (Exception ex)
                {

                    throw new Exception("Error getting data DameDetalle " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return DameDetalleList;
        }

        //dame suma
        /// <summary>
        /// Gets all SumaDetalle
        ///</summary>
        public  List<SumaDetalleEntity> GetSumaDetalleList(long? Clv_Session)
        {
            List<SumaDetalleEntity> SumaDetalleList = new List<SumaDetalleEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("SumaDetalle", connection);

                AssingParameter(comandoSql, "@Clv_Session", Clv_Session);
                AssingParameter(comandoSql, "@Desglosa_Iva", 0);
                AssingParameter(comandoSql, "@op", 0);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        SumaDetalleList.Add(GetSumaDetalleFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data SumaDetalle " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return SumaDetalleList;
        }


        // public abstract importeTotalEntity GetImporteTotal(long? Clv_Session);
        public  importeTotalEntity GetImporteTotal(long? Clv_Session)
        {
            importeTotalEntity data = new importeTotalEntity();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("TotalPagoLinea", connection);
                AssingParameter(comandoSql, "@Clv_Session", Clv_Session);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        data.Descripcion = rd[3].ToString();
                        data.Total = Decimal.Parse(rd[4].ToString());
                    }
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data importeTotal_ecom " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return data;
        }





        /// <summary>
        /// Gets all BuscaFacturasHistorial
        ///</summary>
        public  List<BuscaFacturasHistorialEntity> GetBuscaFacturasHistorialList(int? Id, String Serie, long? Folio, String Fecha, String Tipo, long? ContratoO)
        {
            List<BuscaFacturasHistorialEntity> BuscaFacturasHistorialList = new List<BuscaFacturasHistorialEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("BuscaFacturasHistorialPagos", connection);

                AssingParameter(comandoSql, "@Id", Id);
                AssingParameter(comandoSql, "@Serie", Serie);
                AssingParameter(comandoSql, "@Folio", Folio);
                AssingParameter(comandoSql, "@Fecha", Fecha);
                AssingParameter(comandoSql, "@Tipo", Tipo);
                AssingParameter(comandoSql, "@ContratoO", ContratoO);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        BuscaFacturasHistorialList.Add(GetBuscaFacturasHistorialFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data BuscaFacturasHistorial " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return BuscaFacturasHistorialList;
        }



        /// <summary>
        /// Gets tieneEdoCuenta by
        ///</summary>
        public  tieneEdoCuentaEntity GettieneEdoCuentaById(long? Contrato)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("tieneEdoCuenta", connection);
                tieneEdoCuentaEntity entity_tieneEdoCuenta = null;

                AssingParameter(comandoSql, "@Contrato", Contrato);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
                    if (rd.Read())
                        entity_tieneEdoCuenta = GettieneEdoCuentaFromReader(rd);
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data tieneEdoCuenta " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_tieneEdoCuenta;
            }

        }


        public  Ecom_CorreoEntity GetCorreo(long? Contrato)
        {

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_GetCorreo", connection);

                Ecom_CorreoEntity entity_Correo = null;
                AssingParameter(comandoSql, "@Contrato", Contrato);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    if (rd.Read())
                        entity_Correo = GetCorreoFromReader(rd);
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data GetCorreo " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_Correo;
            }

        }


        public  Ecom_CorreoEntity GetValidaCorreo(int? aux, String Correo)
        {

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_GetValidaCorreo", connection);

                Ecom_CorreoEntity entity_Correo = null;
                AssingParameter(comandoSql, "@Correo", Correo);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    if (rd.Read())
                        entity_Correo = GetValidaCorreoFromReader(rd);
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data GetCorreo " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_Correo;
            }

        }



        public  bool GetDimeSiHayOtroPagoEnProceso_Softv(long? contrato)
        {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    SqlCommand strSQL = new SqlCommand("DimeSiHayOtroPagoEnProceso_Ecommerce");
                    strSQL.Connection = connection;
                    strSQL.CommandType = CommandType.StoredProcedure;
                    strSQL.CommandTimeout = 0;

                    SqlParameter par1 = new SqlParameter("@CONTRATO", SqlDbType.BigInt);
                    par1.Value = contrato;
                    par1.Direction = ParameterDirection.Input;
                    strSQL.Parameters.Add(par1);

                    SqlParameter par2 = new SqlParameter("@bndEnProceso", SqlDbType.Bit);
                    par2.Direction = ParameterDirection.Output;
                    strSQL.Parameters.Add(par2);

                    strSQL.ExecuteNonQuery();
                    result = bool.Parse(par2.Value.ToString());


                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error updating DimeSiHayOtroPagoEnProceso_Ecommerce " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }


            return result;
        }

        public  int GetNuevoPreFacturas_PagoLinea(long Contrato, long Clv_Session, decimal Importe)
        {
            int result = 1;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    SqlCommand strSQL = new SqlCommand("NuevoPreFacturas_PagoLinea");
                    strSQL.Connection = connection;
                    strSQL.CommandType = CommandType.StoredProcedure;
                    strSQL.CommandTimeout = 0;

                    SqlParameter par1 = new SqlParameter("@Contrato", SqlDbType.BigInt);
                    par1.Value = Contrato;
                    par1.Direction = ParameterDirection.Input;
                    strSQL.Parameters.Add(par1);

                    SqlParameter par2 = new SqlParameter("@Clv_Session", SqlDbType.BigInt);
                    par2.Value = Clv_Session;
                    par2.Direction = ParameterDirection.Input;
                    strSQL.Parameters.Add(par2);

                    SqlParameter par3 = new SqlParameter("@Importe", SqlDbType.Money);
                    par3.Value = Importe;
                    par3.Direction = ParameterDirection.Input;
                    strSQL.Parameters.Add(par3);

                    strSQL.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error updating DimeSiHayOtroPagoEnProceso_Ecommerce " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }


            return result;
        }

        public YaHuboPagoEntity GetDime_Yahubo_pagoLinea(long Contrato)
        {
            YaHuboPagoEntity result = new YaHuboPagoEntity();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    SqlCommand strSQL = new SqlCommand("Dime_Yahubo_pagoLinea");
                    strSQL.Connection = connection;
                    strSQL.CommandType = CommandType.StoredProcedure;
                    strSQL.CommandTimeout = 0;

                    SqlParameter par1 = new SqlParameter("@Contrato", SqlDbType.BigInt);
                    par1.Value = Contrato;
                    par1.Direction = ParameterDirection.Input;
                    strSQL.Parameters.Add(par1);

                    SqlParameter par2 = new SqlParameter("@Clv_Session", SqlDbType.BigInt);
                    par2.Value = 0;
                    par2.Direction = ParameterDirection.Output;
                    strSQL.Parameters.Add(par2);

                    SqlParameter par3 = new SqlParameter("@Pasa", SqlDbType.SmallInt);
                    par3.Value = 0;
                    par3.Direction = ParameterDirection.Output;
                    strSQL.Parameters.Add(par3);

                    strSQL.ExecuteNonQuery();

                    result.Pasa = int.Parse(par3.Value.ToString());
                    result.Clv_Session = long.Parse(par2.Value.ToString());

                }
                catch (Exception ex)
                {

                    throw new Exception("Error GetDime_Yahubo_pagoLinea " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }

        protected virtual Ecom_CorreoEntity GetCorreoFromReader(IDataReader reader)
        {
            Ecom_CorreoEntity entity_Correo = null;
            try
            {
                entity_Correo = new Ecom_CorreoEntity();
                entity_Correo.Correo = (String)(GetFromReader(reader, "Correo", IsString: true));
            }
            catch (Exception ex)
            {
                throw new Exception("Error converting GetCorreoFromReader data to entity", ex);
            }
            return entity_Correo;
        }


        protected virtual Ecom_CorreoEntity GetValidaCorreoFromReader(IDataReader reader)
        {
            Ecom_CorreoEntity entity_Correo = null;
            try
            {
                entity_Correo = new Ecom_CorreoEntity();
                entity_Correo.EsValido = (int)(GetFromReader(reader, "EsValido"));
            }
            catch (Exception ex)
            {
                throw new Exception("Error converting GetValidaCorreoFromReader data to entity", ex);
            }
            return entity_Correo;
        }



        /// <summary>
        /// Converts data from reader to entity
        /// </summary>
        protected virtual Ecom_ContCompEntity GetContratoCompuestoFromReader(IDataReader reader)
        {
            Ecom_ContCompEntity entity_ContComp = null;
            try
            {
                entity_ContComp = new Ecom_ContCompEntity();
                entity_ContComp.Nombre = (String)(GetFromReader(reader, "Nombre", IsString: true));
                entity_ContComp.Contrato = (long?)(GetFromReader(reader, "Contrato"));
                entity_ContComp.Email = (String)(GetFromReader(reader, "Email", IsString: true));
                entity_ContComp.Telefono = (String)(GetFromReader(reader, "Telefono", IsString: true));
                entity_ContComp.Direccion = (String)(GetFromReader(reader, "Direccion", IsString: true));
                entity_ContComp.Colonia = (String)(GetFromReader(reader, "Colonia", IsString: true));
                entity_ContComp.Ciudad = (String)(GetFromReader(reader, "Ciudad", IsString: true));
                entity_ContComp.ContratoCompuesto = (String)(GetFromReader(reader, "ContratoCompuesto", IsString: true));
            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Reporte_Tokens data to entity", ex);
            }
            return entity_ContComp;
        }

        protected virtual BusCliPorContrato_FacEntity GetBusCliPorContrato_FacFromReader(IDataReader reader)
        {
            BusCliPorContrato_FacEntity entity_BusCliPorContrato_Fac = null;
            try
            {
                entity_BusCliPorContrato_Fac = new BusCliPorContrato_FacEntity();
                //entity_BusCliPorContrato_Fac.Id = (int?)(GetFromReader(reader, "Id"));
                entity_BusCliPorContrato_Fac.Contrato = (long?)(GetFromReader(reader, "Contrato"));
                entity_BusCliPorContrato_Fac.Nombre = (String)(GetFromReader(reader, "Nombre", IsString: true));
                entity_BusCliPorContrato_Fac.Calle = (String)(GetFromReader(reader, "Calle", IsString: true));
                entity_BusCliPorContrato_Fac.Colonia = (String)(GetFromReader(reader, "Colonia", IsString: true));
                entity_BusCliPorContrato_Fac.Numero = (String)(GetFromReader(reader, "Numero", IsString: true));
                entity_BusCliPorContrato_Fac.Ciudad = (String)(GetFromReader(reader, "Ciudad", IsString: true));
                entity_BusCliPorContrato_Fac.SoloInternet = (bool?)(GetFromReader(reader, "SoloInternet"));
                entity_BusCliPorContrato_Fac.EsHotel = (bool?)(GetFromReader(reader, "EsHotel"));
                entity_BusCliPorContrato_Fac.Telefono = (String)(GetFromReader(reader, "Telefono", IsString: true));
                entity_BusCliPorContrato_Fac.Celular = (String)(GetFromReader(reader, "Celular", IsString: true));
                entity_BusCliPorContrato_Fac.Desglosa_IVA = (bool?)(GetFromReader(reader, "Desglosa_IVA"));
                entity_BusCliPorContrato_Fac.ContratoC = (String)(GetFromReader(reader, "ContratoC", IsString: true));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting BusCliPorContrato_Fac data to entity", ex);
            }
            return entity_BusCliPorContrato_Fac;
        }

        protected virtual DameServicioCliente GetDameServicioClienteFromReader(IDataReader reader)
        {
            DameServicioCliente entity_DameServicioCliente = null;
            try
            {
                entity_DameServicioCliente = new DameServicioCliente();
                entity_DameServicioCliente.CLV_TipSer = (int?)(GetFromReader(reader, "Clv_TipSer"));
                entity_DameServicioCliente.Descripcion = (String)(GetFromReader(reader, "Descripcion"));
            }
            catch (Exception ex)
            {
                throw new Exception("Error converting DameDetalle data to entity", ex);
            }
            return entity_DameServicioCliente;
        }

        protected virtual DameDetalleEntity GetDameDetalleListFromReader(IDataReader reader)
        {
            DameDetalleEntity entity_DameDetalle = null;
            try
            {
                entity_DameDetalle = new DameDetalleEntity();
                entity_DameDetalle.Clv_Session = (long?)(GetFromReader(reader, "Clv_Session"));
                entity_DameDetalle.CLV_SERVICIO = (int?)(GetFromReader(reader, "CLV_SERVICIO"));
                entity_DameDetalle.Clv_llavedelservicio = (int?)(GetFromReader(reader, "Clv_llavedelservicio"));
                entity_DameDetalle.Clv_UnicaNet = (long?)(GetFromReader(reader, "Clv_UnicaNet"));
                entity_DameDetalle.CLAVE = (int?)(GetFromReader(reader, "CLAVE"));
                entity_DameDetalle.DESCORTA = (String)(GetFromReader(reader, "DESCORTA", IsString: true));
                entity_DameDetalle.Pagos_Adelantados = (String)(GetFromReader(reader, "Pagos_Adelantados", IsString: true));
                entity_DameDetalle.tvAdic = (int?)(GetFromReader(reader, "tvAdic"));
                entity_DameDetalle.Meses_Cortesia = (int?)(GetFromReader(reader, "Meses_Cortesia"));
                entity_DameDetalle.mesesApagar = (int?)(GetFromReader(reader, "mesesApagar"));
                entity_DameDetalle.importe = (Decimal?)(GetFromReader(reader, "importe"));
                entity_DameDetalle.periodoPagadoIni = (String)(GetFromReader(reader, "periodoPagadoIni", IsString: true));
                entity_DameDetalle.periodoPagadoFin = (String)(GetFromReader(reader, "periodoPagadoFin", IsString: true));
                entity_DameDetalle.PuntosAplicadosOtros = (int?)(GetFromReader(reader, "PuntosAplicadosOtros"));
                entity_DameDetalle.puntosAplicadosAnt = (int?)(GetFromReader(reader, "puntosAplicadosAnt"));
                entity_DameDetalle.PuntosAplicadosPagoAdelantado = (int?)(GetFromReader(reader, "PuntosAplicadosPagoAdelantado"));
                entity_DameDetalle.DescuentoNet = (Decimal?)(GetFromReader(reader, "DescuentoNet"));
                entity_DameDetalle.Des_Otr_Ser_Misma_Categoria = (Decimal?)(GetFromReader(reader, "Des_Otr_Ser_Misma_Categoria"));
                entity_DameDetalle.bonificacion = (Decimal?)(GetFromReader(reader, "bonificacion"));
                entity_DameDetalle.importeAdicional = (Decimal?)(GetFromReader(reader, "importeAdicional"));
                entity_DameDetalle.columnaDetalle = (String)(GetFromReader(reader, "columnaDetalle", IsString: true));
                entity_DameDetalle.DiasBonifica = (int?)(GetFromReader(reader, "DiasBonifica"));
                entity_DameDetalle.mesesBonificar = (int?)(GetFromReader(reader, "mesesBonificar"));
                entity_DameDetalle.importeBonifica = (Decimal?)(GetFromReader(reader, "importeBonifica"));
                entity_DameDetalle.Ultimo_Mes = (int?)(GetFromReader(reader, "Ultimo_Mes"));
                entity_DameDetalle.Ultimo_anio = (int?)(GetFromReader(reader, "Ultimo_anio"));
                entity_DameDetalle.Adelantado = (bool?)(GetFromReader(reader, "Adelantado"));
                entity_DameDetalle.DESCRIPCION = (String)(GetFromReader(reader, "DESCRIPCION", IsString: true));
                entity_DameDetalle.MacCableModem = (String)(GetFromReader(reader, "MacCableModem", IsString: true));
                entity_DameDetalle.CLV_DETALLE = (long?)(GetFromReader(reader, "CLV_DETALLE"));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting DameDetalle data to entity", ex);
            }
            return entity_DameDetalle;
        }

        protected virtual SumaDetalleEntity GetSumaDetalleFromReader(IDataReader reader)
        {
            SumaDetalleEntity entity_SumaDetalle = null;
            try
            {
                entity_SumaDetalle = new SumaDetalleEntity();
                entity_SumaDetalle.Clv_Session = (int?)(GetFromReader(reader, "Clv_Session"));
                entity_SumaDetalle.Posicion = (int?)(GetFromReader(reader, "Posicion"));
                entity_SumaDetalle.Nivel = (int?)(GetFromReader(reader, "Nivel"));
                entity_SumaDetalle.Descripcion = (String)(GetFromReader(reader, "Descripcion", IsString: true));
                entity_SumaDetalle.Total = (Decimal?)(GetFromReader(reader, "Total"));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting SumaDetalle data to entity", ex);
            }
            return entity_SumaDetalle;
        }

        protected virtual BuscaFacturasHistorialEntity GetBuscaFacturasHistorialFromReader(IDataReader reader)
        {
            BuscaFacturasHistorialEntity entity_BuscaFacturasHistorial = null;
            try
            {
                entity_BuscaFacturasHistorial = new BuscaFacturasHistorialEntity();
                entity_BuscaFacturasHistorial.Clv_Factura = (long?)(GetFromReader(reader, "Clv_Factura"));
                entity_BuscaFacturasHistorial.Status = (String)(GetFromReader(reader, "Status", IsString: true));
                entity_BuscaFacturasHistorial.Serie = (String)(GetFromReader(reader, "Serie", IsString: true));
                entity_BuscaFacturasHistorial.Folio = (long?)(GetFromReader(reader, "Folio"));
                entity_BuscaFacturasHistorial.Fecha = (String)(GetFromReader(reader, "Fecha", IsString: true));
                entity_BuscaFacturasHistorial.Contrato = (String)(GetFromReader(reader, "Contrato", IsString: true));
                entity_BuscaFacturasHistorial.Importe = (Decimal?)(GetFromReader(reader, "Importe"));
                entity_BuscaFacturasHistorial.Cliente = (String)(GetFromReader(reader, "Cliente", IsString: true));
                entity_BuscaFacturasHistorial.Tipo = (String)(GetFromReader(reader, "Tipo", IsString: true));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting BuscaFacturasHistorial data to entity", ex);
            }
            return entity_BuscaFacturasHistorial;
        }

        protected virtual tieneEdoCuentaEntity GettieneEdoCuentaFromReader(IDataReader reader)
        {
            tieneEdoCuentaEntity entity_tieneEdoCuenta = null;
            try
            {
                entity_tieneEdoCuenta = new tieneEdoCuentaEntity();
                //entity_tieneEdoCuenta.Contrato = (long?)(GetFromReader(reader, "Contrato")); , IsString: true));
                entity_tieneEdoCuenta.tieneEdoCuenta = (bool)(GetFromReader(reader, "tieneEdoCuenta"));
                entity_tieneEdoCuenta.idEstadoCuenta = (long?)(GetFromReader(reader, "idEstadoCuenta"));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting tieneEdoCuenta data to entity", ex);
            }
            return entity_tieneEdoCuenta;
        }
    }
}