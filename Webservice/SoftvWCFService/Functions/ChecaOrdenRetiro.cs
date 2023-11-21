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
    public class ChecaOrdenRetiro: DataAccess
    {
        public  int AddChecaOrdenRetiro(ChecaOrdenRetiroEntity entity_ChecaOrdenRetiro)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ChecaOrdenRetiroAdd", connection);

                AssingParameter(comandoSql, "@Resultado", entity_ChecaOrdenRetiro.Resultado);

                AssingParameter(comandoSql, "@Msg", entity_ChecaOrdenRetiro.Msg);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding ChecaOrdenRetiro " + ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }
                result = (int)comandoSql.Parameters["@IdChecaOrdenRetiro"].Value;
            }
            return result;
        }

        /// <summary>
        /// Deletes a ChecaOrdenRetiro
        ///</summary>
        /// <param name="">   to delete </param>
        public  int DeleteChecaOrdenRetiro()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ChecaOrdenRetiroDelete", connection);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting ChecaOrdenRetiro " + ex.Message, ex);
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
        /// Edits a ChecaOrdenRetiro
        ///</summary>
        /// <param name="ChecaOrdenRetiro"> Objeto ChecaOrdenRetiro a editar </param>
        public  int EditChecaOrdenRetiro(ChecaOrdenRetiroEntity entity_ChecaOrdenRetiro)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ChecaOrdenRetiroEdit", connection);

                AssingParameter(comandoSql, "@Resultado", entity_ChecaOrdenRetiro.Resultado);

                AssingParameter(comandoSql, "@Msg", entity_ChecaOrdenRetiro.Msg);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    result = int.Parse(ExecuteNonQuery(comandoSql).ToString());

                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating ChecaOrdenRetiro " + ex.Message, ex);
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
        /// Gets all ChecaOrdenRetiro
        ///</summary>
        public  List<ChecaOrdenRetiroEntity> GetChecaOrdenRetiro(long? Contrato)
        {
            List<ChecaOrdenRetiroEntity> ChecaOrdenRetiroList = new List<ChecaOrdenRetiroEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("ChecaOrdenRetiro", connection);

                AssingParameter(comandoSql, "@Contrato", Contrato);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        ChecaOrdenRetiroList.Add(GetChecaOrdenRetiroFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ChecaOrdenRetiro " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return ChecaOrdenRetiroList;
        }

        /// <summary>
        /// Gets all ChecaOrdenRetiro by List<int>
        ///</summary>
        public  List<ChecaOrdenRetiroEntity> GetChecaOrdenRetiro(List<int> lid)
        {
            List<ChecaOrdenRetiroEntity> ChecaOrdenRetiroList = new List<ChecaOrdenRetiroEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                DataTable IdDT = BuildTableID(lid);

                SqlCommand comandoSql = CreateCommand("Softv_ChecaOrdenRetiroGetByIds", connection);
                AssingParameter(comandoSql, "@IdTable", IdDT);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        ChecaOrdenRetiroList.Add(GetChecaOrdenRetiroFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ChecaOrdenRetiro " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return ChecaOrdenRetiroList;
        }

        /// <summary>
        /// Gets ChecaOrdenRetiro by
        ///</summary>
        public  ChecaOrdenRetiroEntity GetChecaOrdenRetiroById()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ChecaOrdenRetiroGetById", connection);
                ChecaOrdenRetiroEntity entity_ChecaOrdenRetiro = null;


                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
                    if (rd.Read())
                        entity_ChecaOrdenRetiro = GetChecaOrdenRetiroFromReader(rd);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ChecaOrdenRetiro " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_ChecaOrdenRetiro;
            }

        }



        /// <summary>
        ///Get ChecaOrdenRetiro
        ///</summary>
        public  SoftvList<ChecaOrdenRetiroEntity> GetPagedList(int pageIndex, int pageSize)
        {
            SoftvList<ChecaOrdenRetiroEntity> entities = new SoftvList<ChecaOrdenRetiroEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ChecaOrdenRetiroGetPaged", connection);

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
                        entities.Add(GetChecaOrdenRetiroFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ChecaOrdenRetiro " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetChecaOrdenRetiroCount();
                return entities ?? new SoftvList<ChecaOrdenRetiroEntity>();
            }
        }

        /// <summary>
        ///Get ChecaOrdenRetiro
        ///</summary>
        public  SoftvList<ChecaOrdenRetiroEntity> GetPagedList(int pageIndex, int pageSize, String xml)
        {
            SoftvList<ChecaOrdenRetiroEntity> entities = new SoftvList<ChecaOrdenRetiroEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ChecaOrdenRetiroGetPagedXml", connection);

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
                        entities.Add(GetChecaOrdenRetiroFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ChecaOrdenRetiro " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetChecaOrdenRetiroCount(xml);
                return entities ?? new SoftvList<ChecaOrdenRetiroEntity>();
            }
        }

        /// <summary>
        ///Get Count ChecaOrdenRetiro
        ///</summary>
        public int GetChecaOrdenRetiroCount()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ChecaOrdenRetiroGetCount", connection);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ChecaOrdenRetiro " + ex.Message, ex);
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
        ///Get Count ChecaOrdenRetiro
        ///</summary>
        public int GetChecaOrdenRetiroCount(String xml)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_ChecaOrdenRetiroGetCountXml", connection);

                AssingParameter(comandoSql, "@xml", xml);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data ChecaOrdenRetiro " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }

        protected virtual ChecaOrdenRetiroEntity GetChecaOrdenRetiroFromReader(IDataReader reader)
        {
            ChecaOrdenRetiroEntity entity_ChecaOrdenRetiro = null;
            try
            {
                entity_ChecaOrdenRetiro = new ChecaOrdenRetiroEntity();
                entity_ChecaOrdenRetiro.Resultado = (int?)(GetFromReader(reader, "Resultado"));
                entity_ChecaOrdenRetiro.Msg = (String)(GetFromReader(reader, "Msg", IsString: true));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting ChecaOrdenRetiro data to entity", ex);
            }
            return entity_ChecaOrdenRetiro;
        }
    }
}