using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using SoftvWCFService.Entities;
using System.Configuration;

namespace SoftvWCFService.Functions
{
    public class ContratoMaestroFac: DataAccess
    {

        public  int AddContratoMaestroFac(ContratoMaestroFacEntity entity_ContratoMaestroFac)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("ContratoMaestroFacAdd", connection);

                AssingParameter(comandoSql, "@IdContratoMaestro", null, pd: ParameterDirection.Output, IsKey: true);

                AssingParameter(comandoSql, "@RazonSocial", entity_ContratoMaestroFac.RazonSocial);

                AssingParameter(comandoSql, "@NombreComercial", entity_ContratoMaestroFac.NombreComercial);

                AssingParameter(comandoSql, "@Distribuidor", entity_ContratoMaestroFac.Distribuidor);

                AssingParameter(comandoSql, "@Calle", entity_ContratoMaestroFac.Calle);

                AssingParameter(comandoSql, "@NumExt", entity_ContratoMaestroFac.NumExt);

                AssingParameter(comandoSql, "@NumInt", entity_ContratoMaestroFac.NumInt);

                AssingParameter(comandoSql, "@Ciudad", entity_ContratoMaestroFac.Ciudad);

                AssingParameter(comandoSql, "@Estado", entity_ContratoMaestroFac.Estado);

                AssingParameter(comandoSql, "@CodigoPostal", entity_ContratoMaestroFac.CodigoPostal);

                AssingParameter(comandoSql, "@RFC", entity_ContratoMaestroFac.RFC);

                AssingParameter(comandoSql, "@Prepago", entity_ContratoMaestroFac.Prepago);

                AssingParameter(comandoSql, "@PostPago", entity_ContratoMaestroFac.PostPago);

                AssingParameter(comandoSql, "@DiasCredito", entity_ContratoMaestroFac.DiasCredito);

                AssingParameter(comandoSql, "@DiasGracia", entity_ContratoMaestroFac.DiasGracia);

                AssingParameter(comandoSql, "@LimiteCredito", entity_ContratoMaestroFac.LimiteCredito);

                AssingParameter(comandoSql, "@FechaFac", entity_ContratoMaestroFac.FechaFac);

                AssingParameter(comandoSql, "@PagoEdoCuetna", entity_ContratoMaestroFac.PagoEdoCuetna);

                AssingParameter(comandoSql, "@PagoFac", entity_ContratoMaestroFac.PagoFac);

                AssingParameter(comandoSql, "@TipoCorteCli", entity_ContratoMaestroFac.TipoCorteCli);

                AssingParameter(comandoSql, "@ReactivarMan", entity_ContratoMaestroFac.ReactivarMan);

                AssingParameter(comandoSql, "@ReactivarPagoFac", entity_ContratoMaestroFac.ReactivarPagoFac);

                AssingParameter(comandoSql, "@TipoPago", entity_ContratoMaestroFac.TipoPago);

                AssingParameter(comandoSql, "@Localidad", entity_ContratoMaestroFac.Localidad);

                AssingParameter(comandoSql, "@Colonia", entity_ContratoMaestroFac.Colonia);

                AssingParameter(comandoSql, "@Referencia", entity_ContratoMaestroFac.Referencia);

                AssingParameter(comandoSql, "@Referencia2", entity_ContratoMaestroFac.Referencia2);

                AssingParameter(comandoSql, "@ClvBanco", entity_ContratoMaestroFac.ClvBanco);


                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding ContratoMaestroFac " + ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }
                result = (int)comandoSql.Parameters["@IdContratoMaestro"].Value;
            }
            return result;
        }

        /// <summary>
        /// Deletes a ContratoMaestroFac
        ///</summary>
        /// <param name="">  IdContratoMaestro to delete </param>
        public  int DeleteContratoMaestroFac(long? IdContratoMaestro)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ContratoMaestroFacDelete", connection);

                AssingParameter(comandoSql, "@IdContratoMaestro", IdContratoMaestro);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting ContratoMaestroFac " + ex.Message, ex);
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
        /// Edits a ContratoMaestroFac
        ///</summary>
        /// <param name="ContratoMaestroFac"> Objeto ContratoMaestroFac a editar </param>
        public  int EditContratoMaestroFac(ContratoMaestroFacEntity entity_ContratoMaestroFac)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ContratoMaestroFacEdit", connection);

                AssingParameter(comandoSql, "@IdContratoMaestro", entity_ContratoMaestroFac.IdContratoMaestro);

                AssingParameter(comandoSql, "@RazonSocial", entity_ContratoMaestroFac.RazonSocial);

                AssingParameter(comandoSql, "@NombreComercial", entity_ContratoMaestroFac.NombreComercial);

                AssingParameter(comandoSql, "@Distribuidor", entity_ContratoMaestroFac.Distribuidor);

                AssingParameter(comandoSql, "@Calle", entity_ContratoMaestroFac.Calle);

                AssingParameter(comandoSql, "@NumExt", entity_ContratoMaestroFac.NumExt);

                AssingParameter(comandoSql, "@NumInt", entity_ContratoMaestroFac.NumInt);

                AssingParameter(comandoSql, "@Ciudad", entity_ContratoMaestroFac.Ciudad);

                AssingParameter(comandoSql, "@Estado", entity_ContratoMaestroFac.Estado);

                AssingParameter(comandoSql, "@CodigoPostal", entity_ContratoMaestroFac.CodigoPostal);

                AssingParameter(comandoSql, "@RFC", entity_ContratoMaestroFac.RFC);

                AssingParameter(comandoSql, "@Prepago", entity_ContratoMaestroFac.Prepago);

                AssingParameter(comandoSql, "@PostPago", entity_ContratoMaestroFac.PostPago);

                AssingParameter(comandoSql, "@DiasCredito", entity_ContratoMaestroFac.DiasCredito);

                AssingParameter(comandoSql, "@DiasGracia", entity_ContratoMaestroFac.DiasGracia);

                AssingParameter(comandoSql, "@LimiteCredito", entity_ContratoMaestroFac.LimiteCredito);

                AssingParameter(comandoSql, "@FechaFac", entity_ContratoMaestroFac.FechaFac);

                AssingParameter(comandoSql, "@PagoEdoCuetna", entity_ContratoMaestroFac.PagoEdoCuetna);

                AssingParameter(comandoSql, "@PagoFac", entity_ContratoMaestroFac.PagoFac);

                AssingParameter(comandoSql, "@TipoCorteCli", entity_ContratoMaestroFac.TipoCorteCli);

                AssingParameter(comandoSql, "@ReactivarMan", entity_ContratoMaestroFac.ReactivarMan);

                AssingParameter(comandoSql, "@ReactivarPagoFac", entity_ContratoMaestroFac.ReactivarPagoFac);

                AssingParameter(comandoSql, "@TipoPago", entity_ContratoMaestroFac.TipoPago);

                AssingParameter(comandoSql, "@Localidad", entity_ContratoMaestroFac.Localidad);

                AssingParameter(comandoSql, "@Colonia", entity_ContratoMaestroFac.Colonia);

                AssingParameter(comandoSql, "@Referencia", entity_ContratoMaestroFac.Referencia);

                AssingParameter(comandoSql, "@Referencia2", entity_ContratoMaestroFac.Referencia2);

                AssingParameter(comandoSql, "@ClvBanco", entity_ContratoMaestroFac.ClvBanco);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    result = int.Parse(ExecuteNonQuery(comandoSql).ToString());

                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating ContratoMaestroFac " + ex.Message, ex);
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
        /// Gets all ContratoMaestroFac
        ///</summary>
        public  List<ContratoMaestroFacEntity> GetContratoMaestroFac()
        {
            List<ContratoMaestroFacEntity> ContratoMaestroFacList = new List<ContratoMaestroFacEntity>();


            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ContratoMaestroFacGet", connection);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        ContratoMaestroFacList.Add(GetContratoMaestroFacFromReader(rd));
                    }



                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ContratoMaestroFac " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();

                }
            }
            return ContratoMaestroFacList;
        }





        public  List<ContratoMaestroFacEntity> GetBusquedaContratoMaestroFac(String RazonSocial, String NombreComercial, int? ClvCiudad, int? Op)
        {
            List<ContratoMaestroFacEntity> ContratoMaestroFacList = new List<ContratoMaestroFacEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("BusquedaFiltrosFacEmpresarial", connection);

                AssingParameter(comandoSql, "@RazonSocial", RazonSocial);
                AssingParameter(comandoSql, "@NombreComercial", NombreComercial);
                AssingParameter(comandoSql, "@ClvCiudad", ClvCiudad);
                AssingParameter(comandoSql, "@Op", Op);

                IDataReader rd = null;
                IDataReader rd2 = null;

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        ContratoMaestroFacList.Add(GetContratoMaestroFacFromReader(rd));
                    }
                    rd.Close();

                    foreach (ContratoMaestroFacEntity a in ContratoMaestroFacList)
                    {
                        List<DatosClienteSoftv> lsC = new List<DatosClienteSoftv>();

                        SqlCommand comandoSql2 = CreateCommand("DatosCliente_CM", connection);
                        AssingParameter(comandoSql2, "@IdContratoMaestro", a.IdContratoMaestro);
                        rd2 = ExecuteReader(comandoSql2);

                        while (rd2.Read())
                        {
                            lsC.Add(DatosClienteSoftvGet(rd2));
                        }

                        a.lstCliS = lsC;
                        rd2.Close();
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ContratoMaestroFac " + ex.Message, ex);
                }
            }
            return ContratoMaestroFacList;
        }


        public  List<ContratoMaestroFacEntity> GetAddRel(String xml)
        {
            List<ContratoMaestroFacEntity> ContratoMaestroFacList = new List<ContratoMaestroFacEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("RelContratoMaestro_ContratoSoftvAdd", connection);

                AssingParameter(comandoSql, "@xml", xml);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    //while (rd.Read())
                    //{
                    //    ContratoMaestroFacList.Add(GetAddRelFromReader(rd));
                    //}
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ContratoMaestroFac " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return ContratoMaestroFacList;
        }



        public  List<ContratoMaestroFacEntity> GetRompeRel(String xml)
        {
            List<ContratoMaestroFacEntity> ContratoMaestroFacList = new List<ContratoMaestroFacEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("RomperRelContratoMaestro_ContratoSoftv", connection);

                AssingParameter(comandoSql, "@xml", xml);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    //while (rd.Read())
                    //{
                    //    ContratoMaestroFacList.Add(GetContratoMaestroFacFromReader(rd));
                    //}
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ContratoMaestroFac " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return ContratoMaestroFacList;
        }


        public  List<ContratoMaestroFacEntity> GetAddUpdate(String xml)
        {
            List<ContratoMaestroFacEntity> ContratoMaestroFacList = new List<ContratoMaestroFacEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("ContratoMaestro_Excel_UpdateAdd", connection);

                AssingParameter(comandoSql, "@xml", xml);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    //while (rd.Read())
                    //{
                    //    ContratoMaestroFacList.Add(GetContratoMaestroFacFromReader(rd));
                    //}
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ContratoMaestroFac " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return ContratoMaestroFacList;
        }











































        /// <summary>
        /// Gets all ContratoMaestroFac by List<int>
        ///</summary>
        public  List<ContratoMaestroFacEntity> GetContratoMaestroFac(List<int> lid)
        {
            List<ContratoMaestroFacEntity> ContratoMaestroFacList = new List<ContratoMaestroFacEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                DataTable IdDT = BuildTableID(lid);

                SqlCommand comandoSql = CreateCommand("Softv_ContratoMaestroFacGetByIds", connection);
                AssingParameter(comandoSql, "@IdTable", IdDT);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        ContratoMaestroFacList.Add(GetContratoMaestroFacFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ContratoMaestroFac " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return ContratoMaestroFacList;
        }

        /// <summary>
        /// Gets ContratoMaestroFac by
        ///</summary>
        public  ContratoMaestroFacEntity GetContratoMaestroFacById(long? IdContratoMaestro)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ContratoMaestroFacGetById", connection);
                ContratoMaestroFacEntity entity_ContratoMaestroFac = null;


                AssingParameter(comandoSql, "@IdContratoMaestro", IdContratoMaestro);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
                    if (rd.Read())
                        entity_ContratoMaestroFac = GetContratoMaestroFacFromReader(rd);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ContratoMaestroFac " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_ContratoMaestroFac;
            }

        }


        /// <summary>
        /// Change State a ContratoMaestroFac
        ///</summary>
        /// <param name="">  IdContratoMaestro to delete </param>
        public  int ChangeStateContratoMaestroFac(long? IdContratoMaestro, bool State)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ContratoMaestroFacChangeState", connection);

                AssingParameter(comandoSql, "@IdContratoMaestro", IdContratoMaestro);

                AssingParameter(comandoSql, "@Estado", State);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting ContratoMaestroFac " + ex.Message, ex);
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
        ///Get ContratoMaestroFac
        ///</summary>
        public  SoftvList<ContratoMaestroFacEntity> GetPagedList(int pageIndex, int pageSize)
        {
            SoftvList<ContratoMaestroFacEntity> entities = new SoftvList<ContratoMaestroFacEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ContratoMaestroFacGetPaged", connection);

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
                        entities.Add(GetContratoMaestroFacFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ContratoMaestroFac " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetContratoMaestroFacCount();
                return entities ?? new SoftvList<ContratoMaestroFacEntity>();
            }
        }

        /// <summary>
        ///Get ContratoMaestroFac
        ///</summary>
        public  SoftvList<ContratoMaestroFacEntity> GetPagedList(int pageIndex, int pageSize, String xml)
        {
            SoftvList<ContratoMaestroFacEntity> entities = new SoftvList<ContratoMaestroFacEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ContratoMaestroFacGetPagedXml", connection);

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
                        entities.Add(GetContratoMaestroFacFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ContratoMaestroFac " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetContratoMaestroFacCount(xml);
                return entities ?? new SoftvList<ContratoMaestroFacEntity>();
            }
        }

        /// <summary>
        ///Get Count ContratoMaestroFac
        ///</summary>
        public int GetContratoMaestroFacCount()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ContratoMaestroFacGetCount", connection);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ContratoMaestroFac " + ex.Message, ex);
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
        ///Get Count ContratoMaestroFac
        ///</summary>
        public int GetContratoMaestroFacCount(String xml)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ContratoMaestroFacGetCountXml", connection);

                AssingParameter(comandoSql, "@xml", xml);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ContratoMaestroFac " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }




        public  List<ContratoMaestroFacEntity> GetContratos_CS()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                List<ContratoMaestroFacEntity> ContratoMaestroFacList = new List<ContratoMaestroFacEntity>();

                SqlCommand comandoSql = CreateCommand("Softv_ContratoMaestroFacGet", connection);

                IDataReader rd = null;
                IDataReader rd2 = null;

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        ContratoMaestroFacList.Add(GetContratoMaestroFacFromReader(rd));
                    }
                    rd.Close();

                    foreach (ContratoMaestroFacEntity a in ContratoMaestroFacList)
                    {
                        List<DatosClienteSoftv> lsC = new List<DatosClienteSoftv>();

                        SqlCommand comandoSql2 = CreateCommand("DatosCliente_CM", connection);
                        AssingParameter(comandoSql2, "@IdContratoMaestro", a.IdContratoMaestro);
                        rd2 = ExecuteReader(comandoSql2);

                        while (rd2.Read())
                        {
                            lsC.Add(DatosClienteSoftvGet(rd2));
                        }

                        a.lstCliS = lsC;
                        rd2.Close();
                    }

                }

                catch (Exception ex)
                {
                    throw new Exception("Error Get usuario By User And Pass " + ex.Message, ex);
                }

                return ContratoMaestroFacList;
            }

        }



        public  List<ContratoMaestroFacEntity> GetRelContratos(long? IdContratoMaestro)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                List<ContratoMaestroFacEntity> ContratoMaestroFacList = new List<ContratoMaestroFacEntity>();

                SqlCommand comandoSql = CreateCommand("Softv_ContratoMaestroFacGet_ByIdCM", connection);

                AssingParameter(comandoSql, "@IdContratoMaestro", IdContratoMaestro);

                IDataReader rd = null;
                IDataReader rd2 = null;

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        ContratoMaestroFacList.Add(GetContratoMaestroFacFromReader(rd));
                    }
                    rd.Close();

                    foreach (ContratoMaestroFacEntity a in ContratoMaestroFacList)
                    {
                        List<DatosClienteSoftv> lsC = new List<DatosClienteSoftv>();

                        SqlCommand comandoSql2 = CreateCommand("DatosCliente_CM", connection);
                        AssingParameter(comandoSql2, "@IdContratoMaestro", a.IdContratoMaestro);
                        rd2 = ExecuteReader(comandoSql2);

                        while (rd2.Read())
                        {
                            lsC.Add(DatosClienteSoftvGet(rd2));
                        }

                        a.lstCliS = lsC;
                        rd2.Close();
                    }

                }

                catch (Exception ex)
                {
                    throw new Exception("Error Get usuario By User And Pass " + ex.Message, ex);
                }

                return ContratoMaestroFacList;
            }

        }

        protected virtual ContratoMaestroFacEntity GetContratoMaestroFacFromReader(IDataReader reader)
        {
            ContratoMaestroFacEntity entity_ContratoMaestroFac = null;
            try
            {
                entity_ContratoMaestroFac = new ContratoMaestroFacEntity();
                entity_ContratoMaestroFac.IdContratoMaestro = (long?)(GetFromReader(reader, "IdContratoMaestro"));
                entity_ContratoMaestroFac.RazonSocial = (String)(GetFromReader(reader, "RazonSocial", IsString: true));
                entity_ContratoMaestroFac.NombreComercial = (String)(GetFromReader(reader, "NombreComercial", IsString: true));
                entity_ContratoMaestroFac.Distribuidor = (int?)(GetFromReader(reader, "Distribuidor"));
                entity_ContratoMaestroFac.Calle = (int?)(GetFromReader(reader, "Calle", IsString: true));
                entity_ContratoMaestroFac.NumExt = (String)(GetFromReader(reader, "NumExt", IsString: true));
                entity_ContratoMaestroFac.NumInt = (String)(GetFromReader(reader, "NumInt", IsString: true));
                entity_ContratoMaestroFac.Ciudad = (int?)(GetFromReader(reader, "Ciudad", IsString: true));
                entity_ContratoMaestroFac.Estado = (int?)(GetFromReader(reader, "Estado", IsString: true));
                entity_ContratoMaestroFac.Localidad = (int?)(GetFromReader(reader, "Localidad", IsString: true));
                entity_ContratoMaestroFac.Colonia = (int?)(GetFromReader(reader, "Colonia", IsString: true));
                entity_ContratoMaestroFac.CodigoPostal = (String)(GetFromReader(reader, "CodigoPostal", IsString: true));
                entity_ContratoMaestroFac.RFC = (String)(GetFromReader(reader, "RFC", IsString: true));
                entity_ContratoMaestroFac.Prepago = (bool?)(GetFromReader(reader, "Prepago"));
                entity_ContratoMaestroFac.PostPago = (bool?)(GetFromReader(reader, "PostPago"));
                entity_ContratoMaestroFac.DiasCredito = (int?)(GetFromReader(reader, "DiasCredito"));
                entity_ContratoMaestroFac.DiasGracia = (int?)(GetFromReader(reader, "DiasGracia"));
                entity_ContratoMaestroFac.LimiteCredito = (int?)(GetFromReader(reader, "LimiteCredito"));
                entity_ContratoMaestroFac.FechaFac = (String)(GetFromReader(reader, "FechaFac", IsString: true));
                entity_ContratoMaestroFac.PagoEdoCuetna = (bool?)(GetFromReader(reader, "PagoEdoCuetna"));
                entity_ContratoMaestroFac.PagoFac = (bool?)(GetFromReader(reader, "PagoFac"));
                entity_ContratoMaestroFac.TipoCorteCli = (int?)(GetFromReader(reader, "TipoCorteCli"));
                entity_ContratoMaestroFac.ReactivarMan = (bool?)(GetFromReader(reader, "ReactivarMan"));
                entity_ContratoMaestroFac.ReactivarPagoFac = (bool?)(GetFromReader(reader, "ReactivarPagoFac"));
                entity_ContratoMaestroFac.TipoPago = (int?)(GetFromReader(reader, "TipoPago"));
                entity_ContratoMaestroFac.DistribuidorDes = (String)(GetFromReader(reader, "DistribuidorDes", IsString: true));
                entity_ContratoMaestroFac.EstadoDes = (String)(GetFromReader(reader, "EstadoDes", IsString: true));
                entity_ContratoMaestroFac.CiudadDes = (String)(GetFromReader(reader, "CiudadDes", IsString: true));
                entity_ContratoMaestroFac.LocalidadDes = (String)(GetFromReader(reader, "LocalidadDes", IsString: true));
                entity_ContratoMaestroFac.ColoniaDes = (String)(GetFromReader(reader, "ColoniaDes", IsString: true));
                entity_ContratoMaestroFac.CalleDes = (String)(GetFromReader(reader, "CalleDes", IsString: true));
                entity_ContratoMaestroFac.Referencia = (String)(GetFromReader(reader, "Referencia", IsString: true));
                entity_ContratoMaestroFac.Referencia2 = (String)(GetFromReader(reader, "Referencia2", IsString: true));
                entity_ContratoMaestroFac.ClvBanco = (int?)(GetFromReader(reader, "ClvBanco"));
                //entity_ContratoMaestroFac.ContratoReal = (long?)(GetFromReader(reader, "ContratoReal"));
                //entity_ContratoMaestroFac.ContratoCom = (String)(GetFromReader(reader, "ContratoCom", IsString: true));
                //entity_ContratoMaestroFac.NombreCli = (String)(GetFromReader(reader, "NombreCli", IsString: true));
                //entity_ContratoMaestroFac.Direccion = (String)(GetFromReader(reader, "Direccion", IsString: true));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting ContratoMaestroFac data to entity", ex);
            }
            return entity_ContratoMaestroFac;
        }


        protected virtual ContratoMaestroFacEntity BusquedaContratoMaestroFacGet(IDataReader reader)
        {
            ContratoMaestroFacEntity entity_ContratoMaestroFac = null;
            try
            {
                entity_ContratoMaestroFac = new ContratoMaestroFacEntity();
                entity_ContratoMaestroFac.IdContratoMaestro = (long?)(GetFromReader(reader, "IdContratoMaestro"));
                entity_ContratoMaestroFac.RazonSocial = (String)(GetFromReader(reader, "RazonSocial", IsString: true));
                entity_ContratoMaestroFac.NombreComercial = (String)(GetFromReader(reader, "NombreComercial", IsString: true));
                entity_ContratoMaestroFac.Distribuidor = (int?)(GetFromReader(reader, "Distribuidor"));
                entity_ContratoMaestroFac.Calle = (int?)(GetFromReader(reader, "Calle", IsString: true));
                entity_ContratoMaestroFac.NumExt = (String)(GetFromReader(reader, "NumExt", IsString: true));
                entity_ContratoMaestroFac.NumInt = (String)(GetFromReader(reader, "NumInt", IsString: true));
                entity_ContratoMaestroFac.Ciudad = (int?)(GetFromReader(reader, "Ciudad", IsString: true));
                entity_ContratoMaestroFac.Estado = (int?)(GetFromReader(reader, "Estado", IsString: true));
                entity_ContratoMaestroFac.Localidad = (int?)(GetFromReader(reader, "Localidad", IsString: true));
                entity_ContratoMaestroFac.Colonia = (int?)(GetFromReader(reader, "Colonia", IsString: true));
                entity_ContratoMaestroFac.CodigoPostal = (String)(GetFromReader(reader, "CodigoPostal", IsString: true));
                entity_ContratoMaestroFac.RFC = (String)(GetFromReader(reader, "RFC", IsString: true));
                entity_ContratoMaestroFac.Prepago = (bool?)(GetFromReader(reader, "Prepago"));
                entity_ContratoMaestroFac.PostPago = (bool?)(GetFromReader(reader, "PostPago"));
                entity_ContratoMaestroFac.DiasCredito = (int?)(GetFromReader(reader, "DiasCredito"));
                entity_ContratoMaestroFac.DiasGracia = (int?)(GetFromReader(reader, "DiasGracia"));
                entity_ContratoMaestroFac.LimiteCredito = (int?)(GetFromReader(reader, "LimiteCredito"));
                entity_ContratoMaestroFac.FechaFac = (String)(GetFromReader(reader, "FechaFac", IsString: true));
                entity_ContratoMaestroFac.PagoEdoCuetna = (bool?)(GetFromReader(reader, "PagoEdoCuetna"));
                entity_ContratoMaestroFac.PagoFac = (bool?)(GetFromReader(reader, "PagoFac"));
                entity_ContratoMaestroFac.TipoCorteCli = (int?)(GetFromReader(reader, "TipoCorteCli"));
                entity_ContratoMaestroFac.ReactivarMan = (bool?)(GetFromReader(reader, "ReactivarMan"));
                entity_ContratoMaestroFac.ReactivarPagoFac = (bool?)(GetFromReader(reader, "ReactivarPagoFac"));
                entity_ContratoMaestroFac.TipoPago = (int?)(GetFromReader(reader, "TipoPago"));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting ContratoMaestroFac data to entity", ex);
            }
            return entity_ContratoMaestroFac;
        }




        protected virtual DatosClienteSoftv DatosClienteSoftvGet(IDataReader reader)
        {
            DatosClienteSoftv entity_DatosClienteSoftv = null;
            try
            {
                entity_DatosClienteSoftv = new DatosClienteSoftv();
                entity_DatosClienteSoftv.ContratoReal = (long?)(GetFromReader(reader, "ContratoReal"));
                entity_DatosClienteSoftv.ContratoCom = (String)(GetFromReader(reader, "ContratoCom", IsString: true));
                entity_DatosClienteSoftv.NombreCli = (String)(GetFromReader(reader, "NombreCli", IsString: true));
                entity_DatosClienteSoftv.Direccion = (String)(GetFromReader(reader, "Direccion", IsString: true));
                entity_DatosClienteSoftv.Nivel = (int?)(GetFromReader(reader, "Nivel"));
                entity_DatosClienteSoftv.Proporcional = (bool?)(GetFromReader(reader, "Proporcional"));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting DatosClienteSoftv data to entity", ex);
            }
            return entity_DatosClienteSoftv;
        }




        protected virtual ContratoMaestroFacEntity GetAddRelFromReader(IDataReader reader)
        {
            ContratoMaestroFacEntity entity_ContratoMaestroFac = null;
            try
            {
                entity_ContratoMaestroFac = new ContratoMaestroFacEntity();
                entity_ContratoMaestroFac.Bandera = (int?)(GetFromReader(reader, "Bandera"));
                entity_ContratoMaestroFac.Msg = (String)(GetFromReader(reader, "Msg", IsString: true));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting ContratoMaestroFac data to entity", ex);
            }
            return entity_ContratoMaestroFac;
        }



        public class DetContrato
        {
            public string Contrato { get; set; }

            public int Prioridad { get; set; }

            public int Proporcional { get; set; }
        }
    }
}