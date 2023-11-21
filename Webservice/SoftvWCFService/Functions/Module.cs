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
    public class Module: DataAccess
    {
        public  int AddModule(ModuleEntity entity_Module)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_ModuleAdd", connection);

                AssingParameter(comandoSql, "@IdModule", null, pd: ParameterDirection.Output, IsKey: true);

                AssingParameter(comandoSql, "@Description", entity_Module.Description);

                AssingParameter(comandoSql, "@ModulePath", entity_Module.ModulePath);

                AssingParameter(comandoSql, "@ModuleView", entity_Module.ModuleView);

                AssingParameter(comandoSql, "@ParentId", entity_Module.ParentId);

                AssingParameter(comandoSql, "@SortOrder", entity_Module.SortOrder);

                AssingParameter(comandoSql, "@OptAdd", entity_Module.OptAdd);

                AssingParameter(comandoSql, "@OptSelect", entity_Module.OptSelect);

                AssingParameter(comandoSql, "@OptUpdate", entity_Module.OptUpdate);

                AssingParameter(comandoSql, "@OptDelete", entity_Module.OptDelete);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding Module " + ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }
                result = (int)comandoSql.Parameters["@IdModule"].Value;
            }
            return result;
        }

        /// <summary>
        /// Deletes a Module
        ///</summary>
        /// <param name="">  IdModule to delete </param>
        public  int DeleteModule(int? IdModule)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_ModuleDelete", connection);

                AssingParameter(comandoSql, "@IdModule", IdModule);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting Module " + ex.Message, ex);
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
        /// Edits a Module
        ///</summary>
        /// <param name="Module"> Objeto Module a editar </param>
        public  int EditModule(ModuleEntity entity_Module)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_ModuleEdit", connection);

                AssingParameter(comandoSql, "@IdModule", entity_Module.IdModule);

                AssingParameter(comandoSql, "@Description", entity_Module.Description);

                AssingParameter(comandoSql, "@ModulePath", entity_Module.ModulePath);

                AssingParameter(comandoSql, "@ModuleView", entity_Module.ModuleView);

                AssingParameter(comandoSql, "@ParentId", entity_Module.ParentId);

                AssingParameter(comandoSql, "@SortOrder", entity_Module.SortOrder);

                AssingParameter(comandoSql, "@OptAdd", entity_Module.OptAdd);

                AssingParameter(comandoSql, "@OptSelect", entity_Module.OptSelect);

                AssingParameter(comandoSql, "@OptUpdate", entity_Module.OptUpdate);

                AssingParameter(comandoSql, "@OptDelete", entity_Module.OptDelete);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = int.Parse(ExecuteNonQuery(comandoSql).ToString());
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating Module " + ex.Message, ex);
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
        /// Gets all Module
        ///</summary>
        public  List<ModuleEntity> GetModule()
        {
            List<ModuleEntity> ModuleList = new List<ModuleEntity>();
            List<ModuleEntity> ModuleListOrdered = new List<ModuleEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_ModuleGet", connection);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        ModuleList.Add(GetModuleFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Module " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }

                List<ModuleEntity> principales = ModuleList.Where(d => d.ParentId == 0 && d.Display == true).ToList();
                principales.OrderBy(z => z.Description).ToList().ForEach(a =>
                {
                    a.tipo = 1;
                    ModuleListOrdered.Add(a);
                    List<ModuleEntity> hijos = ModuleList.Where(p => p.ParentId == a.IdModule).OrderBy(r => r.Description).ToList();
                    hijos.ForEach(d =>
                    {


                        List<ModuleEntity> nietos = ModuleList.Where(p1 => p1.ParentId == d.IdModule).OrderBy(r1 => r1.Description).ToList();
                        d.tipo = (nietos.Count > 0) ? 4 : 2;
                        ModuleListOrdered.Add(d);
                        nietos.ForEach(m =>
                        {
                            m.tipo = 3;
                            ModuleListOrdered.Add(m);
                        });
                        ;
                    });

                });
            }
            return ModuleListOrdered;
        }

        /// <summary>
        /// Gets all Module by List<int>
        ///</summary>
        public  List<ModuleEntity> GetModule(List<int> lid)
        {
            List<ModuleEntity> ModuleList = new List<ModuleEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                DataTable IdDT = BuildTableID(lid);

                SqlCommand comandoSql = CreateCommand("Ecom_ModuleGetByIds", connection);
                AssingParameter(comandoSql, "@IdTable", IdDT);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        ModuleList.Add(GetModuleFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Module " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return ModuleList;
        }

        /// <summary>
        /// Gets Module by
        ///</summary>
        public  ModuleEntity GetModuleById(int? IdModule)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_ModuleGetById", connection);
                ModuleEntity entity_Module = null;


                AssingParameter(comandoSql, "@IdModule", IdModule);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
                    if (rd.Read())
                        entity_Module = GetModuleFromReader(rd);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Module " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_Module;
            }

        }


        public  List<ModuleEntity> GetModuleByIdModule(int? IdModule)
        {
            List<ModuleEntity> ModuleList = new List<ModuleEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_ModuleGetByIdModule", connection);

                AssingParameter(comandoSql, "@IdModule", IdModule);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        ModuleList.Add(GetModuleFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Module " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return ModuleList;
        }


        /// <summary>
        ///Get Module
        ///</summary>
        public  SoftvList<ModuleEntity> GetPagedList(int pageIndex, int pageSize)
        {
            SoftvList<ModuleEntity> entities = new SoftvList<ModuleEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_ModuleGetPaged", connection);

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
                        entities.Add(GetModuleFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Module " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetModuleCount();
                return entities ?? new SoftvList<ModuleEntity>();
            }
        }

        /// <summary>
        ///Get Module
        ///</summary>
        public  SoftvList<ModuleEntity> GetPagedList(int pageIndex, int pageSize, String xml)
        {
            SoftvList<ModuleEntity> entities = new SoftvList<ModuleEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_ModuleGetPagedXml", connection);

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
                        entities.Add(GetModuleFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Module " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetModuleCount(xml);
                return entities ?? new SoftvList<ModuleEntity>();
            }
        }


        public  List<ModuleEntity> GetModulos_Permisos(int? idrol)
        {
            List<ModuleEntity> ModuleList = new List<ModuleEntity>();
            List<ModuleEntity> ModuleListOrdered = new List<ModuleEntity>();
            List<ModuleEntity> entities = new List<ModuleEntity>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("EcomGetModule_permisos", connection);

                AssingParameter(comandoSql, "@IdRol", idrol);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);
                    while (rd.Read())
                    {
                        ModuleList.Add(GetModule_permisosFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Module " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }

                List<ModuleEntity> principales = ModuleList.Where(d => d.ParentId == 0 && d.Display == true).ToList();
                principales.OrderBy(z => z.Description).ToList().ForEach(a =>
                {
                    a.tipo = 1;
                    ModuleListOrdered.Add(a);
                    List<ModuleEntity> hijos = ModuleList.Where(p => p.ParentId == a.IdModule).OrderBy(r => r.Description).ToList();
                    hijos.ForEach(d =>
                    {


                        List<ModuleEntity> nietos = ModuleList.Where(p1 => p1.ParentId == d.IdModule).OrderBy(r1 => r1.Description).ToList();
                        d.tipo = (nietos.Count > 0) ? 4 : 2;
                        ModuleListOrdered.Add(d);
                        nietos.ForEach(m =>
                        {
                            m.tipo = 3;
                            ModuleListOrdered.Add(m);
                        });
                        ;
                    });

                });
            }
            return ModuleListOrdered;








        }


        /// <summary>
        ///Get Count Module
        ///</summary>
        public int GetModuleCount()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_ModuleGetCount", connection);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Module " + ex.Message, ex);
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
        ///Get Count Module
        ///</summary>
        public int GetModuleCount(String xml)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_ModuleGetCountXml", connection);

                AssingParameter(comandoSql, "@xml", xml);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Module " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }

        protected virtual ModuleEntity GetModuleFromReader(IDataReader reader)
        {
            ModuleEntity entity_Module = null;
            try
            {
                entity_Module = new ModuleEntity();
                entity_Module.IdModule = (int?)(GetFromReader(reader, "IdModule"));
                entity_Module.Description = (String)(GetFromReader(reader, "Description", IsString: true));
                entity_Module.ModulePath = (String)(GetFromReader(reader, "ModulePath", IsString: true));
                entity_Module.ModuleView = (String)(GetFromReader(reader, "ModuleView", IsString: true));
                entity_Module.ParentId = (int?)(GetFromReader(reader, "ParentId"));
                entity_Module.SortOrder = (int?)(GetFromReader(reader, "SortOrder"));
                entity_Module.OptAdd = (bool?)(GetFromReader(reader, "OptAdd"));
                entity_Module.OptSelect = (bool?)(GetFromReader(reader, "OptSelect"));
                entity_Module.OptUpdate = (bool?)(GetFromReader(reader, "OptUpdate"));
                entity_Module.OptDelete = (bool?)(GetFromReader(reader, "OptDelete"));
                entity_Module.Icono = (String)(GetFromReader(reader, "Icono"));
                entity_Module.Display = (bool?)(GetFromReader(reader, "Display"));
                entity_Module.Viewname = (String)(GetFromReader(reader, "Viewname"));
                //entity_Module.DisplayName = (String)(GetFromReader(reader, "DisplayName"));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Module data to entity", ex);
            }
            return entity_Module;
        }



        protected virtual ModuleEntity GetModule_permisosFromReader(IDataReader reader)
        {
            ModuleEntity entity_Module = null;
            try
            {
                entity_Module = new ModuleEntity();
                entity_Module.IdModule = (int?)(GetFromReader(reader, "Id"));
                entity_Module.IdModule = (int?)(GetFromReader(reader, "IdModule"));
                entity_Module.Description = (String)(GetFromReader(reader, "Description", IsString: true));
                entity_Module.ModulePath = (String)(GetFromReader(reader, "ModulePath", IsString: true));
                entity_Module.ModuleView = (String)(GetFromReader(reader, "ModuleView", IsString: true));
                entity_Module.ParentId = (int?)(GetFromReader(reader, "ParentId"));
                entity_Module.SortOrder = (int?)(GetFromReader(reader, "SortOrder"));
                entity_Module.OptAdd = (bool?)(GetFromReader(reader, "OptAdd"));
                entity_Module.OptSelect = (bool?)(GetFromReader(reader, "OptSelect"));
                entity_Module.OptUpdate = (bool?)(GetFromReader(reader, "OptUpdate"));
                entity_Module.OptDelete = (bool?)(GetFromReader(reader, "OptDelete"));
                entity_Module.Icono = (String)(GetFromReader(reader, "Icono"));
                entity_Module.Display = (bool?)(GetFromReader(reader, "Display"));
                entity_Module.Viewname = (String)(GetFromReader(reader, "Viewname"));
                entity_Module.DisplayName = (String)(GetFromReader(reader, "DisplayName"));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Module data to entity", ex);
            }
            return entity_Module;
        }
    }
}