using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;
using System.Reflection;
using System.Configuration;

namespace SoftvWCFService
{
    public class DBFuncion
    {
        private List<SqlParameter> parametros;

        private string lineaConexion = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public SqlConnection conexion;
        private SqlCommand comando;
        public Dictionary<string, Object> diccionarioOutput;

        public DBFuncion()
        {
            conexion = new SqlConnection(lineaConexion);
            parametros = new List<SqlParameter>();
            comando = new SqlCommand();
            comando.Connection = conexion;
        }

        /// <summary>
        /// Agrega parametros a la lista, con direccion Input
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="tipo"></param>
        /// <param name="valor"></param>
        public void agregarParametro(string nombre, SqlDbType tipo, Object valor)
        {
            SqlParameter parametro = new SqlParameter(nombre, tipo);
            parametro.Value = valor;
            parametros.Add(parametro);
        }

        /// <summary>
        ///  Agrega parametros a la lista, con direccion output
        ///Estos solo seràn ùtiles cuando se use consultaOutput
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="tipo"></param>
        /// <param name="direccion"></param>
        public void agregarParametro(string nombre, SqlDbType tipo, ParameterDirection direccion)
        {
            SqlParameter parametro = new SqlParameter(nombre, tipo);
            parametro.Value = 0;
            parametro.Direction = direccion;
            parametros.Add(parametro);
        }

        /// <summary>
        ///  Agrega parametros a la lista, con direccion output
        ///Estos solo seràn ùtiles cuando se use consultaOutput
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="tipo"></param>
        /// <param name="direccion"></param>
        /// /// <param name="longitud"></param>
        public void agregarParametro(string nombre, SqlDbType tipo, ParameterDirection direccion, int longitud)
        {
            SqlParameter parametro = new SqlParameter(nombre, tipo, longitud);
            parametro.Value = 0;
            parametro.Direction = direccion;
            parametros.Add(parametro);
        }

        /// <summary>
        /// consultaDS regresa mas de una tabla, cuando el procedimiento contiene mas de un un select de salida en un DataSet
        /// </summary>
        /// <param name="nombreProcedimiento"></param>
        /// <returns></returns>
        public DataSet consultaDS(string nombreProcedimiento)
        {
            conexion.Open();
            DataSet ds = new DataSet();
            comando.CommandText = nombreProcedimiento;
            comando.CommandType = CommandType.StoredProcedure;
            if (parametros.Count > 0)
            {
                comando.Parameters.AddRange(parametros.ToArray());
            }
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(ds);
            conexion.Close();
            return ds;
        }

        internal void agregarParametro(string v1, object a, int v2, int idDetalleProducto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// consultaDT regresa una tabla producto de un solo select en un DataTable
        /// </summary>
        /// <param name="nombreProcedimiento"></param>
        /// <returns></returns>
        public DataTable consultaDT(string nombreProcedimiento)
        {
            conexion.Open();
            DataTable dt = new DataTable();
            comando.CommandText = nombreProcedimiento;
            comando.CommandType = CommandType.StoredProcedure;
            if (parametros.Count > 0)
            {
                comando.Parameters.AddRange(parametros.ToArray());
            }
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);
            conexion.Close();
            return dt;
        }

        /// <summary>
        /// consultaReader regresa un SqlDataReader, este va como parametro para la llamada al de mapearlo al modelo
        /// </summary>
        /// <param name="nombreProcedimiento"></param>
        /// <returns></returns>
        public SqlDataReader consultaReader(string nombreProcedimiento)
        {
            conexion.Open();
            comando.CommandText = nombreProcedimiento;
            comando.CommandType = CommandType.StoredProcedure;
            if (parametros.Count > 0)
            {
                comando.Parameters.AddRange(parametros.ToArray());
            }
            SqlDataReader reader = comando.ExecuteReader();
            return reader;
        }

        /// <summary>
        /// consultaSinRetorno ejecuta el procedimiento sin esperar nada
        /// </summary>
        /// <param name="nombreProcedimiento"></param>
        public void consultaSinRetorno(string nombreProcedimiento)
        {
            conexion.Open();
            comando.CommandText = nombreProcedimiento;
            comando.CommandType = CommandType.StoredProcedure;
            if (parametros.Count > 0)
            {
                comando.Parameters.AddRange(parametros.ToArray());
            }
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        /// <summary>
        /// consultaSinRetorno no regresa ninguna consulta, solo los parametros que se ingresaron como output a traves del diccionario
        /// El diccionario se accesa desde el objeto pues es publico, el key es el nombre que se paso de parametro
        /// </summary>
        /// <param name="nombreProcedimiento"></param>
        public void consultaOutput(string nombreProcedimiento)
        {
            conexion.Open();
            diccionarioOutput = new Dictionary<string, object>();
            comando.CommandText = nombreProcedimiento;
            comando.CommandType = CommandType.StoredProcedure;
            if (parametros.Count > 0)
            {
                comando.Parameters.AddRange(parametros.ToArray());
            }
            comando.ExecuteNonQuery();
            foreach (SqlParameter parametro in parametros)
            {
                if (parametro.Direction == ParameterDirection.Output)
                {
                    diccionarioOutput.Add(parametro.ParameterName, parametro.Value);
                }
            }
            conexion.Close();
        }

        /// <summary>
        /// Mapea un SqlDataReader al modelo que se elija, las columnas en el reader (las que regresa la consulta deben ser del mismo nombre que los elementos del modelo)
        /// Si el IDataReader es null o está vacío, regresa null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public IEnumerable<T> MapDataToEntityCollection<T>(IDataReader dr) where T : new()
        {
            Hashtable hashtable = GetProperties<T>();
            List<T> entities = new List<T>();

            if (dr != null && dr.FieldCount > 0)
            {
                while (dr.Read())
                {
                    T newObject = new T();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        PropertyInfo info = (PropertyInfo)hashtable[dr.GetName(i).ToUpper()];

                        if ((info != null) && info.CanWrite)
                        {
                            info.SetValue(newObject, Convert.ChangeType(dr.GetValue(i), info.PropertyType), null);
                        }
                    }

                    entities.Add(newObject);
                }
                dr.Close();
            }
            else
            {
                entities = null;
            }

            return entities;
        }

        /// <summary>
        /// Limpiar parametros para nuevo uso sin crear nuevo objeto en el mismo bloque
        /// </summary>
        public void limpiarParametros()
        {
            parametros.Clear();
            parametros = new List<SqlParameter>();
            diccionarioOutput.Clear();
        }

        private Hashtable GetProperties<T>()
        {
            Type entityType = typeof(T);
            //List<T> entities = new List<T>();
            Hashtable hashtable = new Hashtable();
            PropertyInfo[] properties = entityType.GetProperties();

            foreach (PropertyInfo info in properties)
            {
                hashtable[info.Name.ToUpper()] = info;
            }

            return hashtable;
        }
    }
}