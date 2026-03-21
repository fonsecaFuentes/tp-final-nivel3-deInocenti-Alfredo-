using GridFlow.Dominio;
using GridFlow.Negocio.Utilidades;
using GridFlow.Negocio.Validaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridFlow.Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> Listar()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id, Descripcion AS Marca From MARCAS");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = datos.Lector["Marca"] is DBNull ? null : (string)datos.Lector["Marca"];

                    lista.Add(aux);
                }

                return lista;
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

        public void Agregar(Marca nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("Insert into MARCAS(Descripcion) values(@Descripcion)");
                datos.SetearParametros("@Descripcion", DBHelper.TextoNull(nuevo.Descripcion));
                datos.EjecutarAccion();
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void Modificar(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                ValidadorGeneral.ValidarId(marca.Id);
                datos.SetearConsulta("Update MARCAS set Descripcion = @Descripcion Where Id = @Id");
                datos.SetearParametros("@Descripcion", DBHelper.TextoNull(marca.Descripcion));
                datos.SetearParametros("@Id", marca.Id);
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

        public void Eliminar(Marca marca)
        {
            RelacionadorDatos relacionador = new RelacionadorDatos();
            relacionador.ActualizarDatos("ARTICULOS", "IdMarca", marca.Id);

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("Delete From MARCAS Where Id = @Id");
                datos.SetearParametros("@Id", marca.Id);
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
