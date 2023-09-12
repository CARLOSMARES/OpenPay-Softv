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
    public class CobraSaldo: DataAccess
    {
        public  int AddCobraSaldo(CobraSaldoEntity entity_CobraSaldo)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CobraSaldoAdd", connection);

                AssingParameter(comandoSql, "@Contrato", entity_CobraSaldo.Contrato);

                AssingParameter(comandoSql, "@ClvSession", entity_CobraSaldo.ClvSession);

                AssingParameter(comandoSql, "@Saldo", entity_CobraSaldo.Saldo);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error adding CobraSaldo " + ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }
                result = (int)comandoSql.Parameters["@IdCobraSaldo"].Value;
            }
            return result;
        }

        /// <summary>
        /// Deletes a CobraSaldo
        ///</summary>
        /// <param name="">   to delete </param>
        public  int DeleteCobraSaldo()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CobraSaldoDelete", connection);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error deleting CobraSaldo " + ex.Message, ex);
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
        /// Edits a CobraSaldo
        ///</summary>
        /// <param name="CobraSaldo"> Objeto CobraSaldo a editar </param>
        public  int EditCobraSaldo(CobraSaldoEntity entity_CobraSaldo)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CobraSaldoEdit", connection);

                AssingParameter(comandoSql, "@Contrato", entity_CobraSaldo.Contrato);

                AssingParameter(comandoSql, "@ClvSession", entity_CobraSaldo.ClvSession);

                AssingParameter(comandoSql, "@Saldo", entity_CobraSaldo.Saldo);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    result = int.Parse(ExecuteNonQuery(comandoSql).ToString());

                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error updating CobraSaldo " + ex.Message, ex);
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
        /// Gets all CobraSaldo
        ///</summary>
        public  List<CobraSaldoEntity> GetCobraSaldo()
        {
            List<CobraSaldoEntity> CobraSaldoList = new List<CobraSaldoEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CobraSaldoGet", connection);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        CobraSaldoList.Add(GetCobraSaldoFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data CobraSaldo " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return CobraSaldoList;
        }

        /// <summary>
        /// Gets all CobraSaldo by List<int>
        ///</summary>
        public  List<CobraSaldoEntity> GetCobraSaldo(List<int> lid)
        {
            List<CobraSaldoEntity> CobraSaldoList = new List<CobraSaldoEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                DataTable IdDT = BuildTableID(lid);

                SqlCommand comandoSql = CreateCommand("Softv_CobraSaldoGetByIds", connection);
                AssingParameter(comandoSql, "@IdTable", IdDT);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        CobraSaldoList.Add(GetCobraSaldoFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data CobraSaldo " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return CobraSaldoList;
        }

        /// <summary>
        /// Gets CobraSaldo by
        ///</summary>
        public  CobraSaldoEntity GetCobraSaldoById(long? Contrato)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("CobraSaldo", connection);
                CobraSaldoEntity entity_CobraSaldo = null;

                AssingParameter(comandoSql, "@Contrato", Contrato);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
                    if (rd.Read())
                        entity_CobraSaldo = GetCobraSaldoFromReader(rd);
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data CobraSaldo " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_CobraSaldo;
            }

        }



        /// <summary>
        ///Get CobraSaldo
        ///</summary>
        public  SoftvList<CobraSaldoEntity> GetPagedList(int pageIndex, int pageSize)
        {
            SoftvList<CobraSaldoEntity> entities = new SoftvList<CobraSaldoEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CobraSaldoGetPaged", connection);

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
                        entities.Add(GetCobraSaldoFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data CobraSaldo " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetCobraSaldoCount();
                return entities ?? new SoftvList<CobraSaldoEntity>();
            }
        }

        /// <summary>
        ///Get CobraSaldo
        ///</summary>
        public  SoftvList<CobraSaldoEntity> GetPagedList(int pageIndex, int pageSize, String xml)
        {
            SoftvList<CobraSaldoEntity> entities = new SoftvList<CobraSaldoEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CobraSaldoGetPagedXml", connection);

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
                        entities.Add(GetCobraSaldoFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data CobraSaldo " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetCobraSaldoCount(xml);
                return entities ?? new SoftvList<CobraSaldoEntity>();
            }
        }

        /// <summary>
        ///Get Count CobraSaldo
        ///</summary>
        public int GetCobraSaldoCount()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CobraSaldoGetCount", connection);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data CobraSaldo " + ex.Message, ex);
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
        ///Get Count CobraSaldo
        ///</summary>
        public int GetCobraSaldoCount(String xml)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CobraSaldoGetCountXml", connection);

                AssingParameter(comandoSql, "@xml", xml);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data CobraSaldo " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }

        protected virtual CobraSaldoEntity GetCobraSaldoFromReader(IDataReader reader)
        {
            CobraSaldoEntity entity_CobraSaldo = null;
            try
            {
                entity_CobraSaldo = new CobraSaldoEntity();
                //entity_CobraSaldo.Contrato = (long?)(GetFromReader(reader, "Contrato"));
                entity_CobraSaldo.ClvSession = (long?)(GetFromReader(reader, "ClvSession"));
                entity_CobraSaldo.Saldo = (Decimal?)(GetFromReader(reader, "Saldo"));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting CobraSaldo data to entity", ex);
            }
            return entity_CobraSaldo;
        }
    }
}