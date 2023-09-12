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
    public class Usuario: DataAccess
    {
        public  int AddUsuario(UsuarioEntity entity_Usuario)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_UsuarioAdd", connection);

                AssingParameter(comandoSql, "@IdUsuario", null, pd: ParameterDirection.Output, IsKey: true);

                AssingParameter(comandoSql, "@IdRol", entity_Usuario.IdRol);

                //     AssingParameter(comandoSql, "@Nombre", entity_Usuario.Nombre);

                //   AssingParameter(comandoSql, "@Email", entity_Usuario.Email);

                AssingParameter(comandoSql, "@Login", entity_Usuario.Login);

                AssingParameter(comandoSql, "@Pasaporte", entity_Usuario.Pasaporte);
                AssingParameter(comandoSql, "@Contrato", entity_Usuario.Contrato);

                //AssingParameter(comandoSql, "@Estado", entity_Usuario.Estado);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error adding Usuario " + ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }
                result = (int)comandoSql.Parameters["@IdUsuario"].Value;
            }
            return result;
        }

        /// <summary>
        /// Deletes a Usuario
        ///</summary>
        /// <param name="">  IdUsuario to delete </param>
        public  int DeleteUsuario(int? IdUsuario)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Ecom_UsuarioDelete", connection);

                AssingParameter(comandoSql, "@IdUsuario", IdUsuario);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error deleting Usuario " + ex.Message, ex);
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
        /// Edits a Usuario
        ///</summary>
        /// <param name="Usuario"> Objeto Usuario a editar </param>
        public  int EditUsuario(UsuarioEntity entity_Usuario)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                SqlCommand comandoSql = CreateCommand("Ecom_UsuarioEdit", connection);

                AssingParameter(comandoSql, "@IdUsuario", entity_Usuario.IdUsuario);

                AssingParameter(comandoSql, "@IdRol", entity_Usuario.IdRol);

                //   AssingParameter(comandoSql, "@Nombre", entity_Usuario.Nombre);
                //    AssingParameter(comandoSql, "@Email", entity_Usuario.Email);

                AssingParameter(comandoSql, "@Login", entity_Usuario.Login);

                AssingParameter(comandoSql, "@Pasaporte", entity_Usuario.Pasaporte);
                AssingParameter(comandoSql, "@Contrato", entity_Usuario.Contrato);
                //AssingParameter(comandoSql, "@Estado", entity_Usuario.Estado);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = int.Parse(ExecuteNonQuery(comandoSql).ToString());
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error updating Usuario " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }

        public  bool GetSP_DIMESITIENECONTRATACION(long? contrato)
        {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    SqlCommand strSQL = new SqlCommand("SP_DIMESITIENECONTRATACION");
                    strSQL.Connection = connection;
                    strSQL.CommandType = CommandType.StoredProcedure;
                    strSQL.CommandTimeout = 0;

                    SqlParameter par1 = new SqlParameter("@CONTRATO", SqlDbType.BigInt);
                    par1.Value = contrato;
                    par1.Direction = ParameterDirection.Input;
                    strSQL.Parameters.Add(par1);

                    SqlParameter par2 = new SqlParameter("@BND", SqlDbType.Bit);
                    par2.Direction = ParameterDirection.Output;
                    strSQL.Parameters.Add(par2);

                    strSQL.ExecuteNonQuery();
                    result = bool.Parse(par2.Value.ToString());


                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error updating GetSP_DIMESITIENECONTRATACION " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }

            //if(result == true)
            //{
            //    throw new WebFaultException<string>("Acceso no autorizado, favor de validar autenticación", HttpStatusCode.Unauthorized);

            //   // throw new Exception("No puedes realizar un pago , debido a que tu servicio solo esta contratado ");
            //}

            return result;
        }

        



        public void GuardaRedireccionFreeAuth_Ecommerce(String successIndicator, long? nivel, String laUrl, long? contrato)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    SqlCommand comandoSql = CreateCommand("GuardaRedireccionFreeAuth_Ecommerce", connection);
                    AssingParameter(comandoSql, "@contrato", @contrato);
                    AssingParameter(comandoSql, "@successIndicator", successIndicator);
                    AssingParameter(comandoSql, "@nivel", nivel);
                    AssingParameter(comandoSql, "@laUrl", laUrl);
                    comandoSql.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error GuardaRedireccionFreeAuth_Ecommerce " + ex.Message, ex);
                }
                finally
                {

                    if (connection != null)
                        connection.Close();
                }

            }
        }


        public UsuarioLoginEntity GetusuarioByUserAndPass(string Usuariox, string Pass)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("AccesoWeb_PorUsuario_linea", connection);
                //SqlCommand comandoSql = CreateCommand("Softv_UsuarioGetusuarioByUserAndPass", connection);
                UsuarioLoginEntity entity_Usuario = null;
                AssingParameter(comandoSql, "@Login", Usuariox);
                AssingParameter(comandoSql, "@Pasaporte", Pass);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
                    if (rd.Read())
                        entity_Usuario = GetUsuarioFromReader(rd);

                    rd.Close();

                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error Get usuario By User And Pass " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_Usuario;
            }

        }

        public  UsuarioEntity GetDatosCliente(long? Contrato)
        {
            UsuarioEntity Cliente = new UsuarioEntity();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("DatosCliente", connection);
                AssingParameter(comandoSql, "@Contrato", Contrato);
                comandoSql.CommandTimeout = 0;
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        Cliente.Nombre = rd[0].ToString();
                        Cliente.Telefono = rd[1].ToString();
                        Cliente.ContratoCompuesto = rd[2].ToString();
                        Cliente.Direccion = rd[3].ToString();
                        Cliente.Colonia = rd[4].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data GetDatosCliente " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return Cliente;
        }

        protected virtual UsuarioLoginEntity GetUsuarioFromReader(IDataReader reader)
        {
            UsuarioLoginEntity entity_Usuario = null;
            try
            {
                entity_Usuario = new UsuarioLoginEntity();
                entity_Usuario.Contrato = (long?)(GetFromReader(reader, "Contrato"));
                entity_Usuario.ContratoCompuesto = (String)(GetFromReader(reader, "ContratoCompuesto", IsString: true));
            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Usuario data to entity", ex);
            }
            return entity_Usuario;
        }

        protected virtual UsuarioEntity GetUsuarioRolFromReader(IDataReader reader)
        {
            UsuarioEntity entity_Usuario = null;
            try
            {
                entity_Usuario = new UsuarioEntity();
                entity_Usuario.IdUsuario = (int?)(GetFromReader(reader, "IdUsuario"));
                entity_Usuario.IdRol = (int?)(GetFromReader(reader, "IdRol"));
                //   entity_Usuario.Nombre = (String)(GetFromReader(reader, "Nombre", IsString: true));
                // entity_Usuario.Email = (String)(GetFromReader(reader, "Email", IsString: true));
                entity_Usuario.Login = (String)(GetFromReader(reader, "Login", IsString: true));
                entity_Usuario.Contrato = (long?)(GetFromReader(reader, "Contrato"));
                entity_Usuario.Pasaporte = (String)(GetFromReader(reader, "Pasaporte", IsString: true));
                entity_Usuario.Estado = (bool?)(GetFromReader(reader, "Estado"));
                entity_Usuario.NombreRol = (String)(GetFromReader(reader, "NombreRol", IsString: true));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Usuario data to entity", ex);
            }
            return entity_Usuario;
        }

        protected virtual UsuarioEntity guardaDatosLogueoFromReader(IDataReader reader)
        {
            UsuarioEntity entity_Usuario = null;
            try
            {
                entity_Usuario = new UsuarioEntity();
                entity_Usuario.Login = (String)(GetFromReader(reader, "Login", IsString: true));
                entity_Usuario.Pasaporte = (String)(GetFromReader(reader, "Pasaporte", IsString: true));
                entity_Usuario.Contrato = (long?)(GetFromReader(reader, "Contrato"));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Usuario data to entity", ex);
            }
            return entity_Usuario;
        }

        protected virtual UsuarioEntity dameContratoReader(IDataReader reader)
        {
            UsuarioEntity entity_Usuario = null;
            try
            {
                entity_Usuario = new UsuarioEntity();
                entity_Usuario.Contrato = (long?)(GetFromReader(reader, "Contrato"));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Usuario data to entity", ex);
            }
            return entity_Usuario;
        }



        protected virtual UsuarioEntity GetExisteUserFromReader(IDataReader reader)
        {
            UsuarioEntity entity_Usuario = null;
            try
            {
                entity_Usuario = new UsuarioEntity();
                entity_Usuario.Bnd = (int?)(GetFromReader(reader, "Bnd"));
                entity_Usuario.Msg = (String)(GetFromReader(reader, "Msg", IsString: true));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Usuario data to entity", ex);
            }
            return entity_Usuario;
        }
    }
}