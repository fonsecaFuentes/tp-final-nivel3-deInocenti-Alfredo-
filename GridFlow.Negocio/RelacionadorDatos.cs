using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridFlow.Negocio
{
    public class RelacionadorDatos
    {
        public void ActualizarDatos(string nomTabla, string nomColumna, int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = $"UPDATE {nomTabla} SET {nomColumna} = NULL WHERE {nomColumna} = @Id";
                datos.SetearConsulta(consulta);
                datos.SetearParametros("@Id", id);
                datos.EjecutarAccion();
            }
            catch
            {
                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
