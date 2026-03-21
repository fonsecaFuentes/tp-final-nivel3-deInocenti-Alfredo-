using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridFlow.Negocio
{
    public class AccesoDatos
    {
        // Atributos
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        // Constructor
        public AccesoDatos()
        {
            conexion = new SqlConnection(ConfigurationManager.AppSettings["cadenaConexion"]);
        }

        // Métodos

        // Propiedad lectura
        public SqlDataReader Lector
        {
            get { return lector; }
        }

        // Configuración de comando
        public void SetearConsulta(string consulta)
        {
            comando = new SqlCommand();
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
            comando.Connection = conexion;
        }

        // Ejecutar lectura (SELECT)
        public void EjecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch
            {
                throw;
            }
        }

        // Ejecutar accion (INSERT, UPDATE, DELETE)
        public void EjecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        // Consulta para devuelver un único valor.
        public int EjecutarAccionScalar()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                object resultado = comando.ExecuteScalar();
                if (resultado != null)
                    return int.Parse(resultado.ToString());

                return 0;
            }
            catch
            {
                throw;
            }
        }

        // Setear parametros
        public void SetearParametros(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre,valor);
        }

        // Limpiar parametros
        public void LimpiarParametros()
        {
            if (comando != null)
                comando.Parameters.Clear();
        }

        // Cerrar conexión
        public void CerrarConexion()
        {
            if (lector != null)
                lector.Close();

            conexion.Close();
        }
    }
}
