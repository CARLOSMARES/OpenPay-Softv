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
    public class Adelantar: DataAccess
    {
        public string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public AdelantarEntity GetAdelantarById(long? IdSession)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Pregunta_Si_Puedo_Adelantar", connection);
                AdelantarEntity entity_Adelantar = null;

                AssingParameter(comandoSql, "@IdSession", IdSession);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
                    if (rd.Read())
                        entity_Adelantar = GetAdelantarFromReader(rd);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Adelantar " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_Adelantar;
            }

        }


        public AdelantarEntity GetDeepChecaAdelantarPagosDif(long? Contrato)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("ChecaAdelantarPagosDif", connection);
                AdelantarEntity entity_Adelantar = null;

                AssingParameter(comandoSql, "@Contrato", Contrato);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
                    if (rd.Read())
                        entity_Adelantar = GetAdelantarFromReader(rd);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Adelantar " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_Adelantar;
            }

        }

        protected virtual AdelantarEntity GetAdelantarFromReader(IDataReader reader)
        {
            AdelantarEntity entity_Adelantar = null;
            try
            {
                entity_Adelantar = new AdelantarEntity();
                //entity_Adelantar.IdSession = (long?)(GetFromReader(reader, "IdSession"));
                //entity_Adelantar.Contrato = (long?)(GetFromReader(reader, "Contrato"));
                entity_Adelantar.Error = (int?)(GetFromReader(reader, "Error"));
                entity_Adelantar.Msg = (String)(GetFromReader(reader, "Msg", IsString: true));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Adelantar data to entity", ex);
            }
            return entity_Adelantar;
        }

    }
}