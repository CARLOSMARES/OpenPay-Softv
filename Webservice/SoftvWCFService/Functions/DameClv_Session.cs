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
    public class DameClv_Session: DataAccess
    {
        public  int AddDameClv_Session(DameClv_SessionEntity entity_DameClv_Session)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_DameClv_SessionAdd", connection);

                AssingParameter(comandoSql, "@IdSession", entity_DameClv_Session.Clv_Session);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error adding DameClv_Session " + ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }
                result = (int)comandoSql.Parameters["@IdDameClv_Session"].Value;
            }
            return result;
        }

        /// <summary>
        /// Deletes a DameClv_Session
        ///</summary>
        /// <param name="">   to delete </param>
        public  int DeleteDameClv_Session()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_DameClv_SessionDelete", connection);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error deleting DameClv_Session " + ex.Message, ex);
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
        /// Edits a DameClv_Session
        ///</summary>
        /// <param name="DameClv_Session"> Objeto DameClv_Session a editar </param>
        public  int EditDameClv_Session(DameClv_SessionEntity entity_DameClv_Session)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_DameClv_SessionEdit", connection);

                AssingParameter(comandoSql, "@IdSession", entity_DameClv_Session.Clv_Session);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    result = int.Parse(ExecuteNonQuery(comandoSql).ToString());

                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error updating DameClv_Session " + ex.Message, ex);
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
        /// Gets all DameClv_Session
        ///</summary>
        public  List<DameClv_SessionEntity> GetDameClv_Session(long? Contrato)
        {
            List<DameClv_SessionEntity> DameClv_SessionList = new List<DameClv_SessionEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("DameClv_Session", connection);

                AssingParameter(comandoSql, "@Contrato", Contrato);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        DameClv_SessionList.Add(GetDameClv_SessionFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data DameClv_Session " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return DameClv_SessionList;
        }

        /// <summary>
        /// Gets all DameClv_Session by List<int>
        ///</summary>
        public  List<DameClv_SessionEntity> GetDameClv_Session(List<int> lid)
        {
            List<DameClv_SessionEntity> DameClv_SessionList = new List<DameClv_SessionEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                DataTable IdDT = BuildTableID(lid);

                SqlCommand comandoSql = CreateCommand("Softv_DameClv_SessionGetByIds", connection);
                AssingParameter(comandoSql, "@IdTable", IdDT);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        DameClv_SessionList.Add(GetDameClv_SessionFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data DameClv_Session " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return DameClv_SessionList;
        }

        /// <summary>
        /// Gets DameClv_Session by
        ///</summary>
        public  DameClv_SessionEntity GetDameClv_SessionById(long? Contrato)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("DameClv_SessionPagoLinea", connection);

                AssingParameter(comandoSql, "@Contrato", Contrato);

                DameClv_SessionEntity entity_DameClv_Session = null;


                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
                    if (rd.Read())
                        entity_DameClv_Session = GetDameClv_SessionFromReader(rd);
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data DameClv_Session " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_DameClv_Session;
            }

        }



        public  DameClv_SessionEntity GetOneDeepDos(String ContratoCom)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("DameClv_SessionPagoLinea", connection);

                AssingParameter(comandoSql, "@Contrato", ContratoCom);

                DameClv_SessionEntity entity_DameClv_Session = null;

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
                    if (rd.Read())
                        entity_DameClv_Session = GetDameClv_SessionDosFromReader(rd);
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data DameClv_Session " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_DameClv_Session;
            }

        }







        /// <summary>
        ///Get DameClv_Session
        ///</summary>
        public  SoftvList<DameClv_SessionEntity> GetPagedList(int pageIndex, int pageSize)
        {
            SoftvList<DameClv_SessionEntity> entities = new SoftvList<DameClv_SessionEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_DameClv_SessionGetPaged", connection);

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
                        entities.Add(GetDameClv_SessionFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data DameClv_Session " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetDameClv_SessionCount();
                return entities ?? new SoftvList<DameClv_SessionEntity>();
            }
        }

        /// <summary>
        ///Get DameClv_Session
        ///</summary>
        public  SoftvList<DameClv_SessionEntity> GetPagedList(int pageIndex, int pageSize, String xml)
        {
            SoftvList<DameClv_SessionEntity> entities = new SoftvList<DameClv_SessionEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_DameClv_SessionGetPagedXml", connection);

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
                        entities.Add(GetDameClv_SessionFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data DameClv_Session " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetDameClv_SessionCount(xml);
                return entities ?? new SoftvList<DameClv_SessionEntity>();
            }
        }

        /// <summary>
        ///Get Count DameClv_Session
        ///</summary>
        public int GetDameClv_SessionCount()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_DameClv_SessionGetCount", connection);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data DameClv_Session " + ex.Message, ex);
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
        ///Get Count DameClv_Session
        ///</summary>
        public int GetDameClv_SessionCount(String xml)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_DameClv_SessionGetCountXml", connection);

                AssingParameter(comandoSql, "@xml", xml);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data DameClv_Session " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }

        protected virtual DameClv_SessionEntity GetDameClv_SessionFromReader(IDataReader reader)
        {
            DameClv_SessionEntity entity_DameClv_Session = null;
            try
            {
                entity_DameClv_Session = new DameClv_SessionEntity();
                entity_DameClv_Session.Clv_Session = (long?)(GetFromReader(reader, "IdSession"));
                entity_DameClv_Session.Error = (int?)(GetFromReader(reader, "Error"));
                entity_DameClv_Session.Msg = (String)(GetFromReader(reader, "Msg", IsString: true));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting DameClv_Session data to entity", ex);
            }
            return entity_DameClv_Session;
        }



        protected virtual DameClv_SessionEntity GetDameClv_SessionDosFromReader(IDataReader reader)
        {
            DameClv_SessionEntity entity_DameClv_Session = null;
            try
            {
                entity_DameClv_Session = new DameClv_SessionEntity();
                entity_DameClv_Session.Clv_Session = (long?)(GetFromReader(reader, "IdSession"));
                entity_DameClv_Session.Error = (int?)(GetFromReader(reader, "Error"));
                entity_DameClv_Session.Msg = (String)(GetFromReader(reader, "Msg", IsString: true));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting DameClv_Session data to entity", ex);
            }
            return entity_DameClv_Session;
        }
    }
}