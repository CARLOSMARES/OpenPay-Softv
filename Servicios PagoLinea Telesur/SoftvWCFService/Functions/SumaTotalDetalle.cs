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
    public class SumaTotalDetalle: DataAccess
    {

        public  SumaTotalDetalleEntity GetSumaTotalDetalleById(long? IdSession)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("DAMETOTALSumaDetalle", connection);
                SumaTotalDetalleEntity entity_SumaTotalDetalle = null;

                AssingParameter(comandoSql, "@IdSession", IdSession);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
                    if (rd.Read())
                        entity_SumaTotalDetalle = GetSumaTotalDetalleFromReader(rd);
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error getting data SumaTotalDetalle " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_SumaTotalDetalle;
            }

        }

        protected virtual SumaTotalDetalleEntity GetSumaTotalDetalleFromReader(IDataReader reader)
        {
            SumaTotalDetalleEntity entity_SumaTotalDetalle = null;
            try
            {
                entity_SumaTotalDetalle = new SumaTotalDetalleEntity();
                //entity_SumaTotalDetalle.IdSession = (long?)(GetFromReader(reader, "IdSession"));
                entity_SumaTotalDetalle.Monto = (Decimal?)(GetFromReader(reader, "Monto"));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting SumaTotalDetalle data to entity", ex);
            }
            return entity_SumaTotalDetalle;
        }

    }
}