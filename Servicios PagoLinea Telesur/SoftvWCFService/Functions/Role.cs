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
    public class Role: DataAccess
    {
        public  int AddRole(RoleEntity entity_Role)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_RoleAdd", connection);

                AssingParameter(comandoSql, "@IdRol", null, pd: ParameterDirection.Output, IsKey: true);

                AssingParameter(comandoSql, "@Nombre", entity_Role.Nombre);

                AssingParameter(comandoSql, "@Descripcion", entity_Role.Descripcion);

                AssingParameter(comandoSql, "@Estado", entity_Role.Estado);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding Role " + ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }
                result = (int)comandoSql.Parameters["@IdRol"].Value;
            }
            return result;
        }

        /// <summary>
        /// Deletes a Role
        ///</summary>
        /// <param name="">  IdRol to delete </param>
        public  int DeleteRole(int? IdRol)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_RoleDelete", connection);

                AssingParameter(comandoSql, "@IdRol", IdRol);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting Role " + ex.Message, ex);
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
        /// Edits a Role
        ///</summary>
        /// <param name="Role"> Objeto Role a editar </param>
        public  int EditRole(RoleEntity entity_Role)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_RoleEdit", connection);

                AssingParameter(comandoSql, "@IdRol", entity_Role.IdRol);

                AssingParameter(comandoSql, "@Nombre", entity_Role.Nombre);

                AssingParameter(comandoSql, "@Descripcion", entity_Role.Descripcion);

                AssingParameter(comandoSql, "@Estado", entity_Role.Estado);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = int.Parse(ExecuteNonQuery(comandoSql).ToString());
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating Role " + ex.Message, ex);
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
        /// Gets all Role
        ///</summary>
        public  List<RoleEntity> GetRole()
        {
            List<RoleEntity> RoleList = new List<RoleEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_RoleGet", connection);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        RoleList.Add(GetRoleFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Role " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return RoleList;
        }



        public  List<RoleEntity> GetUpListPermisos(String xml)
        {
            List<RoleEntity> RoleList = new List<RoleEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_GetUpListPermisos", connection);

                AssingParameter(comandoSql, "@xml", xml);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        RoleList.Add(GetRoleFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Role " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return RoleList;
        }



        /// <summary>
        /// Gets all Role by List<int>
        ///</summary>
        public  List<RoleEntity> GetRole(List<int> lid)
        {
            List<RoleEntity> RoleList = new List<RoleEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                DataTable IdDT = BuildTableID(lid);

                SqlCommand comandoSql = CreateCommand("Ecom_RoleGetByIds", connection);
                AssingParameter(comandoSql, "@IdTable", IdDT);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        RoleList.Add(GetRoleFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Role " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return RoleList;
        }

        /// <summary>
        /// Gets Role by
        ///</summary>
        public  RoleEntity GetRoleById(int? IdRol)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_RoleGetById", connection);
                RoleEntity entity_Role = null;


                AssingParameter(comandoSql, "@IdRol", IdRol);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
                    if (rd.Read())
                        entity_Role = GetRoleFromReader(rd);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Role " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_Role;
            }

        }


        /// <summary>
        /// Change State a Role
        ///</summary>
        /// <param name="">  IdRol to delete </param>
        public  int ChangeStateRole(int? IdRol, bool State)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_RoleChangeState", connection);

                AssingParameter(comandoSql, "@IdRol", IdRol);

                AssingParameter(comandoSql, "@Estado", State);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting Role " + ex.Message, ex);
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
        ///Get Role
        ///</summary>
        public  SoftvList<RoleEntity> GetPagedList(int pageIndex, int pageSize)
        {
            SoftvList<RoleEntity> entities = new SoftvList<RoleEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_RoleGetPaged", connection);

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
                        entities.Add(GetRoleFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Role " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetRoleCount();
                return entities ?? new SoftvList<RoleEntity>();
            }
        }

        /// <summary>
        ///Get Role
        ///</summary>
        public  SoftvList<RoleEntity> GetPagedList(int pageIndex, int pageSize, String xml)
        {
            SoftvList<RoleEntity> entities = new SoftvList<RoleEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_RoleGetPagedXml", connection);

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
                        entities.Add(GetRoleFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Role " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetRoleCount(xml);
                return entities ?? new SoftvList<RoleEntity>();
            }
        }

        /// <summary>
        ///Get Count Role
        ///</summary>
        public int GetRoleCount()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_RoleGetCount", connection);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Role " + ex.Message, ex);
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
        ///Get Count Role
        ///</summary>
        public int GetRoleCount(String xml)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_RoleGetCountXml", connection);

                AssingParameter(comandoSql, "@xml", xml);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Role " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }




        public  List<RoleEntity> GetComandos(String xml)
        {
            List<RoleEntity> ContratoMaestroFacList = new List<RoleEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_RoleAddCommand", connection);

                AssingParameter(comandoSql, "@xml", xml);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

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

        protected virtual RoleEntity GetRoleFromReader(IDataReader reader)
        {
            RoleEntity entity_Role = null;
            try
            {
                entity_Role = new RoleEntity();
                entity_Role.IdRol = (int?)(GetFromReader(reader, "IdRol"));
                entity_Role.Nombre = (String)(GetFromReader(reader, "Nombre", IsString: true));
                entity_Role.Descripcion = (String)(GetFromReader(reader, "Descripcion", IsString: true));
                entity_Role.Estado = (bool?)(GetFromReader(reader, "Estado"));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Role data to entity", ex);
            }
            return entity_Role;
        }
    }
}