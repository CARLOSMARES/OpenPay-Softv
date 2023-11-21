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
    public class ConRelClienteObs: DataAccess
    {
        public  int AddConRelClienteObs(ConRelClienteObsEntity entity_ConRelClienteObs)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ConRelClienteObsAdd", connection);

                AssingParameter(comandoSql, "@Contrato", entity_ConRelClienteObs.Contrato);

                AssingParameter(comandoSql, "@Obs", entity_ConRelClienteObs.Obs);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding ConRelClienteObs " + ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }
                result = (int)comandoSql.Parameters["@IdConRelClienteObs"].Value;
            }
            return result;
        }

        /// <summary>
        /// Deletes a ConRelClienteObs
        ///</summary>
        /// <param name="">   to delete </param>
        public  int DeleteConRelClienteObs()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ConRelClienteObsDelete", connection);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting ConRelClienteObs " + ex.Message, ex);
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
        /// Edits a ConRelClienteObs
        ///</summary>
        /// <param name="ConRelClienteObs"> Objeto ConRelClienteObs a editar </param>
        public  int EditConRelClienteObs(ConRelClienteObsEntity entity_ConRelClienteObs)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ConRelClienteObsEdit", connection);

                AssingParameter(comandoSql, "@Contrato", entity_ConRelClienteObs.Contrato);

                AssingParameter(comandoSql, "@Obs", entity_ConRelClienteObs.Obs);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    result = int.Parse(ExecuteNonQuery(comandoSql).ToString());

                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating ConRelClienteObs " + ex.Message, ex);
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
        /// Gets all ConRelClienteObs
        ///</summary>
        public  List<ConRelClienteObsEntity> GetConRelClienteObs()
        {
            List<ConRelClienteObsEntity> ConRelClienteObsList = new List<ConRelClienteObsEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ConRelClienteObsGet", connection);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        ConRelClienteObsList.Add(GetConRelClienteObsFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ConRelClienteObs " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return ConRelClienteObsList;
        }

        /// <summary>
        /// Gets all ConRelClienteObs by List<int>
        ///</summary>
        public  List<ConRelClienteObsEntity> GetConRelClienteObs(List<int> lid)
        {
            List<ConRelClienteObsEntity> ConRelClienteObsList = new List<ConRelClienteObsEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                DataTable IdDT = BuildTableID(lid);

                SqlCommand comandoSql = CreateCommand("Softv_ConRelClienteObsGetByIds", connection);
                AssingParameter(comandoSql, "@IdTable", IdDT);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        ConRelClienteObsList.Add(GetConRelClienteObsFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ConRelClienteObs " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return ConRelClienteObsList;
        }

        /// <summary>
        /// Gets ConRelClienteObs by
        ///</summary>
        public  ConRelClienteObsEntity GetConRelClienteObsById(long? Contrato)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("ConRelClienteObs", connection);
                ConRelClienteObsEntity entity_ConRelClienteObs = null;

                AssingParameter(comandoSql, "@Contrato", Contrato);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
                    if (rd.Read())
                        entity_ConRelClienteObs = GetConRelClienteObsFromReader(rd);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ConRelClienteObs " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_ConRelClienteObs;
            }

        }



        /// <summary>
        ///Get ConRelClienteObs
        ///</summary>
        public  SoftvList<ConRelClienteObsEntity> GetPagedList(int pageIndex, int pageSize)
        {
            SoftvList<ConRelClienteObsEntity> entities = new SoftvList<ConRelClienteObsEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ConRelClienteObsGetPaged", connection);

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
                        entities.Add(GetConRelClienteObsFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ConRelClienteObs " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetConRelClienteObsCount();
                return entities ?? new SoftvList<ConRelClienteObsEntity>();
            }
        }

        /// <summary>
        ///Get ConRelClienteObs
        ///</summary>
        public  SoftvList<ConRelClienteObsEntity> GetPagedList(int pageIndex, int pageSize, String xml)
        {
            SoftvList<ConRelClienteObsEntity> entities = new SoftvList<ConRelClienteObsEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ConRelClienteObsGetPagedXml", connection);

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
                        entities.Add(GetConRelClienteObsFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ConRelClienteObs " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetConRelClienteObsCount(xml);
                return entities ?? new SoftvList<ConRelClienteObsEntity>();
            }
        }

        /// <summary>
        ///Get Count ConRelClienteObs
        ///</summary>
        public int GetConRelClienteObsCount()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ConRelClienteObsGetCount", connection);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ConRelClienteObs " + ex.Message, ex);
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
        ///Get Count ConRelClienteObs
        ///</summary>
        public int GetConRelClienteObsCount(String xml)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ConRelClienteObsGetCountXml", connection);

                AssingParameter(comandoSql, "@xml", xml);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ConRelClienteObs " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }

        protected virtual ConRelClienteObsEntity GetConRelClienteObsFromReader(IDataReader reader)
        {
            ConRelClienteObsEntity entity_ConRelClienteObs = null;
            try
            {
                entity_ConRelClienteObs = new ConRelClienteObsEntity();
                entity_ConRelClienteObs.Obs = (String)(GetFromReader(reader, "Obs", IsString: true));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting ConRelClienteObs data to entity", ex);
            }
            return entity_ConRelClienteObs;
        }

    }
}