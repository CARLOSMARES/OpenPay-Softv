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
    public class Permiso: DataAccess
    {

        public  int MargePermiso(String xml)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                SqlCommand comandoSql = CreateCommand("Ecom_PermisoMarge", connection);
                AssingParameter(comandoSql, "@xml", xml);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding Permiso " + ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return result;
        }


        /// <summary>
        ///Get Permiso
        ///</summary>
        public  SoftvList<PermisoEntity> GetXml(String xml)
        {
            SoftvList<PermisoEntity> entities = new SoftvList<PermisoEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                SqlCommand comandoSql = CreateCommand("Ecom_PermisoGetXml", connection);
                AssingParameter(comandoSql, "@xml", xml);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);
                    while (rd.Read())
                    {
                        entities.Add(GetPermisoFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Permiso " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entities ?? new SoftvList<PermisoEntity>();
            }
        }


        public  List<PermisoEntity> GetPermiso()
        {
            List<PermisoEntity> PermisoList = new List<PermisoEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                SqlCommand comandoSql = CreateCommand("Ecom_PermisoGet", connection);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        PermisoList.Add(GetPermisoFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Permiso" + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return PermisoList;
        }


        public  List<PermisoEntity> GetPermisoRolList(int? IdRol)
        {
            List<PermisoEntity> PermisoList = new List<PermisoEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                SqlCommand comandoSql = CreateCommand("Ecom_GetPermisoRolList", connection);

                AssingParameter(comandoSql, "@IdRol", IdRol);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        PermisoList.Add(GetPermisoFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Permiso" + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return PermisoList;
        }

        protected virtual PermisoEntity GetPermisoFromReader(IDataReader reader)
        {
            PermisoEntity entity_Permiso = null;
            try
            {
                entity_Permiso = new PermisoEntity();
                entity_Permiso.IdRol = (int?)(GetFromReader(reader, "IdRol"));
                entity_Permiso.IdModule = (int?)(GetFromReader(reader, "IdModule"));
                entity_Permiso.OptAdd = (bool)(GetFromReader(reader, "OptAdd"));
                entity_Permiso.OptSelect = (bool)(GetFromReader(reader, "OptSelect"));
                entity_Permiso.OptUpdate = (bool)(GetFromReader(reader, "OptUpdate"));
                entity_Permiso.OptDelete = (bool)(GetFromReader(reader, "OptDelete"));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Permiso data to entity", ex);
            }
            return entity_Permiso;
        }
    }
}