using GridFlow.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridFlow.Negocio
{
    public class FavoritoNegocio
    {
        public List<Favorito> ListarPorUsuario(int idUsuario)
        {
            List<Favorito> lista = new List<Favorito>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT F.Id, F.IdUser, F.IdArticulo, U.Nombre, U.Apellido, A.Nombre AS NombreArticulo, A.Descripcion AS DescripcionArticulo, A.ImagenUrl, A.Precio FROM FAVORITOS F INNER JOIN ARTICULOS A ON F.IdArticulo = A.Id INNER JOIN USERS U ON F.IdUser = U.Id WHERE F.IdUser = @IdUser");
                datos.SetearParametros("@IdUser", idUsuario);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Favorito aux = new Favorito();
                    aux.Id = (int)datos.Lector["Id"];

                    aux.Usuario = new Usuario();
                    aux.Usuario.Id = (int)datos.Lector["IdUser"];
                    aux.Usuario.Nombre = datos.Lector["Nombre"] is DBNull ? null : (string)datos.Lector["Nombre"];
                    aux.Usuario.Apellido = datos.Lector["Apellido"] is DBNull ? null : (string)datos.Lector["Apellido"];

                    aux.Articulo = new Articulo();
                    aux.Articulo.Id = (int)datos.Lector["IdArticulo"];
                    aux.Articulo.Nombre = datos.Lector["NombreArticulo"] is DBNull ? null : (string)datos.Lector["NombreArticulo"];
                    aux.Articulo.Descripcion = datos.Lector["DescripcionArticulo"] is DBNull ? null : (string)datos.Lector["DescripcionArticulo"];
                    aux.Articulo.ImagenUrl = datos.Lector["ImagenUrl"] is DBNull ? null : (string)datos.Lector["ImagenUrl"];
                    aux.Articulo.Precio = datos.Lector["Precio"] is DBNull ? (decimal?)null : (decimal)datos.Lector["Precio"];


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

        public void AgregarFavorito(int idUser, int IdArticulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (ExisteFavorito(idUser, IdArticulo))
                    return;

                datos.SetearConsulta("Insert into FAVORITOS (IdUser, IdArticulo) Values (@IdUser, @IdArticulo)");
                datos.SetearParametros("@IdUser", idUser);
                datos.SetearParametros("@IdArticulo", IdArticulo);
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

        public bool ExisteFavorito(int idUser, int IdArticulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT COUNT(*) FROM FAVORITOS WHERE IdUser = @IdUser AND IdArticulo = @IdArticulo");
                datos.SetearParametros("@IdUser", idUser);
                datos.SetearParametros("@IdArticulo", IdArticulo);

                int cantidad = datos.EjecutarAccionScalar();
                return cantidad > 0;
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

        public void QuitarFavorito(int idUser, int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                datos.SetearConsulta("DELETE FROM FAVORITOS WHERE IdUser = @IdUser AND IdArticulo = @IdArticulo");
                datos.SetearParametros("@IdUser", idUser);
                datos.SetearParametros("@IdArticulo", idArticulo);
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
