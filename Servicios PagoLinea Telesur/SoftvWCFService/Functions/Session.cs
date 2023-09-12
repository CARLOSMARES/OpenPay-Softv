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
    public class Session: DataAccess
    {
        /// <summary>
        ///</summary>
        /// <param name="Session"> Object Session added to List</param>
        public  int AddSession(SessionEntity entity_Session)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_SessionAdd", connection);

                AssingParameter(comandoSql, "@IdSession", null, pd: ParameterDirection.Output, IsKey: true);

                AssingParameter(comandoSql, "@IdUsuario", entity_Session.IdUsuario);

                AssingParameter(comandoSql, "@Token", entity_Session.Token);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding Session " + ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }
                result = (int)comandoSql.Parameters["@IdSession"].Value;
            }
            return result;
        }

        /// <summary>
        /// Deletes a Session
        ///</summary>
        /// <param name="">  IdSession to delete </param>
        public  int DeleteSession(long? IdSession)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_SessionDelete", connection);

                AssingParameter(comandoSql, "@IdSession", IdSession);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting Session " + ex.Message, ex);
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
        /// Edits a Session
        ///</summary>
        /// <param name="Session"> Objeto Session a editar </param>
        public  int EditSession(SessionEntity entity_Session)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_SessionEdit", connection);

                AssingParameter(comandoSql, "@IdSession", entity_Session.IdSession);

                AssingParameter(comandoSql, "@IdUsuario", entity_Session.IdUsuario);

                AssingParameter(comandoSql, "@Token", entity_Session.Token);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    result = int.Parse(ExecuteNonQuery(comandoSql).ToString());

                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating Session " + ex.Message, ex);
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
        /// Gets all Session
        ///</summary>
        public  List<SessionEntity> GetSession()
        {
            List<SessionEntity> SessionList = new List<SessionEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_SessionGet", connection);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        SessionList.Add(GetSessionFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Session " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return SessionList;
        }

        /// <summary>
        /// Gets all Session by List<int>
        ///</summary>
        public  List<SessionEntity> GetSession(List<int> lid)
        {
            List<SessionEntity> SessionList = new List<SessionEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                DataTable IdDT = BuildTableID(lid);

                SqlCommand comandoSql = CreateCommand("Ecom_SessionGetByIds", connection);
                AssingParameter(comandoSql, "@IdTable", IdDT);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        SessionList.Add(GetSessionFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Session " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return SessionList;
        }

        /// <summary>
        /// Gets Session by
        ///</summary>
        public  SessionEntity GetSessionById(long? IdSession)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_SessionGetById", connection);
                SessionEntity entity_Session = null;


                AssingParameter(comandoSql, "@IdSession", IdSession);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
                    if (rd.Read())
                        entity_Session = GetSessionFromReader(rd);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Session " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_Session;
            }

        }


        public  List<SessionEntity> GetSessionByIdUsuario(int? IdUsuario)
        {
            List<SessionEntity> SessionList = new List<SessionEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_SessionGetByIdUsuario", connection);

                AssingParameter(comandoSql, "@IdUsuario", IdUsuario);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        SessionList.Add(GetSessionFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Session " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return SessionList;
        }



        public  List<SessionEntity> GetIdUserByToken(string token)
        {
            List<SessionEntity> SessionList = new List<SessionEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_SessionGetIdUserByToken", connection);

                AssingParameter(comandoSql, "@Token", token);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        SessionList.Add(GetSessionFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Session " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return SessionList;
        }

        /// <summary>
        ///Get Session
        ///</summary>
        public  SoftvList<SessionEntity> GetPagedList(int pageIndex, int pageSize)
        {
            SoftvList<SessionEntity> entities = new SoftvList<SessionEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_SessionGetPaged", connection);

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
                        entities.Add(GetSessionFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Session " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetSessionCount();
                return entities ?? new SoftvList<SessionEntity>();
            }
        }

        /// <summary>
        ///Get Session
        ///</summary>
        public  SoftvList<SessionEntity> GetPagedList(int pageIndex, int pageSize, String xml)
        {
            SoftvList<SessionEntity> entities = new SoftvList<SessionEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_SessionGetPagedXml", connection);

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
                        entities.Add(GetSessionFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Session " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetSessionCount(xml);
                return entities ?? new SoftvList<SessionEntity>();
            }
        }

        /// <summary>
        ///Get Count Session
        ///</summary>
        public int GetSessionCount()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_SessionGetCount", connection);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Session " + ex.Message, ex);
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
        ///Get Count Session
        ///</summary>
        public int GetSessionCount(String xml)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_SessionGetCountXml", connection);

                AssingParameter(comandoSql, "@xml", xml);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Session " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }

        protected virtual SessionEntity GetSessionFromReader(IDataReader reader)
        {
            SessionEntity entity_Session = null;
            try
            {
                entity_Session = new SessionEntity();
                entity_Session.IdSession = (long?)(GetFromReader(reader, "IdSession"));
                entity_Session.IdUsuario = (int?)(GetFromReader(reader, "IdUsuario"));
                entity_Session.Token = (String)(GetFromReader(reader, "Token", IsString: true));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Session data to entity", ex);
            }
            return entity_Session;
        }
    }
}