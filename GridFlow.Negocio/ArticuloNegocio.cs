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
    public class ArticuloNegocio
    {
        public List<Articulo> Listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("Select A.Id, A.Codigo, A.Nombre, A.Descripcion, A.ImagenUrl, A.Precio, COALESCE(C.Descripcion, 'Sin categoría') AS Categoria, C.Id AS IdCategoria, COALESCE(M.Descripcion, 'Sin marca') AS Marca, M.Id AS IdMarca From ARTICULOS A LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id LEFT JOIN MARCAS M ON A.IdMarca = M.Id");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = datos.Lector["Codigo"] is DBNull ? null : (string)datos.Lector["Codigo"];
                    aux.Nombre = datos.Lector["Nombre"] is DBNull ? null : (string)datos.Lector["Nombre"];
                    aux.Descripcion = datos.Lector["Descripcion"] is DBNull ? null : (string)datos.Lector["Descripcion"];
                    aux.Precio = datos.Lector["Precio"] is DBNull ? (decimal?) null : (decimal)datos.Lector["Precio"];
                    aux.ImagenUrl = datos.Lector["ImagenUrl"] is DBNull ? null : (string)datos.Lector["ImagenUrl"];

                    // Categoria
                    if (!(datos.Lector["IdCategoria"] is DBNull))
                    {
                        aux.Categoria = new Categoria();
                        aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                        aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    }

                    // Marca
                    if (!(datos.Lector["IdMarca"] is DBNull))
                    {
                        aux.Marca = new Marca();
                        aux.Marca.Id = (int)datos.Lector["IdMarca"];
                        aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    }

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

        public Articulo ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("Select A.Id, A.Codigo, A.Nombre, A.Descripcion, A.ImagenUrl, A.Precio, COALESCE(C.Descripcion, 'Sin categoría') AS Categoria, C.Id AS IdCategoria, COALESCE(M.Descripcion, 'Sin marca') AS Marca, M.Id AS IdMarca From ARTICULOS AS A LEFT JOIN CATEGORIAS AS C ON A.IdCategoria = C.Id LEFT JOIN MARCAS AS M ON A.IdMarca = M.Id Where A.Id = @Id");
                datos.SetearParametros("@Id", id);
                datos.EjecutarLectura();

                if (!datos.Lector.Read())
                    return null;

                Articulo obtenido = new Articulo();
                obtenido.Id = (int)datos.Lector["Id"];
                obtenido.Codigo = datos.Lector["Codigo"] is DBNull ? null : (string)datos.Lector["Codigo"];
                obtenido.Nombre = datos.Lector["Nombre"] is DBNull ? null : (string)datos.Lector["Nombre"];
                obtenido.Descripcion = datos.Lector["Descripcion"] is DBNull ? null : (string)datos.Lector["Descripcion"];
                obtenido.Precio = datos.Lector["Precio"] is DBNull ? (decimal?)null : (decimal)datos.Lector["Precio"];
                obtenido.ImagenUrl = datos.Lector["ImagenUrl"] is DBNull ? null : (string)datos.Lector["ImagenUrl"];

                // Categoria
                if (!(datos.Lector["IdCategoria"] is DBNull))
                {
                    obtenido.Categoria = new Categoria();
                    obtenido.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    obtenido.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                }

                // Marca
                if (!(datos.Lector["IdMarca"] is DBNull))
                {
                    obtenido.Marca = new Marca();
                    obtenido.Marca.Id = (int)datos.Lector["IdMarca"];
                    obtenido.Marca.Descripcion = (string)datos.Lector["Marca"];
                }

                return obtenido;
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

        public void Agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO ARTICULOS(Codigo, Nombre, Descripcion, ImagenUrl, Precio, IdCategoria, IdMarca) VALUES(@Codigo, @Nombre, @Descripcion, @ImagenUrl, @Precio, @IdCategoria, @IdMarca)");
                datos.SetearParametros("@Codigo", DBHelper.TextoNull(nuevo.Codigo));
                datos.SetearParametros("@Nombre", DBHelper.TextoNull(nuevo.Nombre));
                datos.SetearParametros("@Descripcion", DBHelper.TextoNull(nuevo.Descripcion));
                datos.SetearParametros("@ImagenUrl", DBHelper.TextoNull(nuevo.ImagenUrl));
                datos.SetearParametros("@Precio", DBHelper.DecimalNull(nuevo.Precio));
                datos.SetearParametros("@IdCategoria", DBHelper.CategoriaIdONull(nuevo.Categoria));
                datos.SetearParametros("@IdMarca", DBHelper.MarcaIdONull(nuevo.Marca));

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

        public void Modificar(Articulo aModificar)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                ValidadorGeneral.ValidarId(aModificar.Id);

                datos.SetearConsulta("UPDATE ARTICULOS SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, ImagenUrl = @ImagenUrl, Precio = @Precio, IdCategoria = @IdCategoria, IdMarca = @IdMarca WHERE Id = @Id");
                datos.SetearParametros("@Codigo", DBHelper.TextoNull(aModificar.Codigo));
                datos.SetearParametros("@Nombre", DBHelper.TextoNull(aModificar.Nombre));
                datos.SetearParametros("@Descripcion", DBHelper.TextoNull(aModificar.Descripcion));
                datos.SetearParametros("@ImagenUrl", DBHelper.TextoNull(aModificar.ImagenUrl));
                datos.SetearParametros("@Precio", DBHelper.DecimalNull(aModificar.Precio));
                datos.SetearParametros("@Id", aModificar.Id);
                datos.SetearParametros("@IdCategoria", DBHelper.CategoriaIdONull(aModificar.Categoria));
                datos.SetearParametros("@IdMarca", DBHelper.MarcaIdONull(aModificar.Marca));

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

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("Delete From ARTICULOS Where Id = @Id");
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

       //public List<Articulo> Filtrar(string campo, string criterio, string filtro, string filtroExtra = null)
       // {
            
       // }
    }
}
