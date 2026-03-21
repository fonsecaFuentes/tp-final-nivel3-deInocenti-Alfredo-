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
    public class CategoriaNegocio
    {
        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id, Descripcion AS Categoria FROM CATEGORIAS");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = datos.Lector["Categoria"] is DBNull ? null : (string)datos.Lector["Categoria"];

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

        public void Agregar(Categoria nueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("Insert into CATEGORIAS(Descripcion) values(@Descripcion)");
                datos.SetearParametros("@Descripcion", DBHelper.TextoNull(nueva.Descripcion));
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

        public void Modificar(Categoria aModificar)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                ValidadorGeneral.ValidarId(aModificar.Id);
                datos.SetearConsulta("Update CATEGORIAS set Descripcion = @Descripcion Where Id = @Id");
                datos.SetearParametros("@Descripcion", DBHelper.TextoNull(aModificar.Descripcion));
                datos.SetearParametros("@Id", aModificar.Id);
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

        public void Eliminar(Categoria elemento)
        {
            RelacionadorDatos relacionador = new RelacionadorDatos();
            relacionador.ActualizarDatos("ARTICULOS", "IdCategoria", elemento.Id);

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("Delete From CATEGORIAS Where Id = @Id");
                datos.SetearParametros("@Id", elemento.Id);
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
