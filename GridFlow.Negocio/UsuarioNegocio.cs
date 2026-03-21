using GridFlow.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridFlow.Negocio
{
    public class UsuarioNegocio
    {
        public bool Login(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("Select id, email, pass, nombre, apellido, admin, urlImagenPerfil From Users Where email = @Email And pass = @Pass");
                datos.SetearParametros("@Email",usuario.Email);
                datos.SetearParametros("@Pass", usuario.Pass);

                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["id"];
                    usuario.Email = (string)datos.Lector["email"];
                    usuario.Admin = (bool)datos.Lector["admin"];
                    usuario.Nombre = datos.Lector["nombre"] is DBNull ? null : (string)datos.Lector["nombre"];
                    usuario.Apellido = datos.Lector["apellido"] is DBNull ? null : (string)datos.Lector["apellido"];
                    usuario.UrlImagenPerfil = datos.Lector["urlImagenPerfil"] is DBNull ? null : (string)datos.Lector["urlImagenPerfil"];

                    return true;
                }
                return false;
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
        
        public int Registrar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("Insert Into USERS (email, pass, admin) output inserted.id values (@Email, @Pass, 0)");
                datos.SetearParametros("@Email", usuario.Email);
                datos.SetearParametros("@Pass", usuario.Pass);

                return datos.EjecutarAccionScalar();
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
        
        public void ActualizarPerfil(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("Update Users set nombre = @Nombre, apellido = @Apellido, urlImagenPerfil = @UrlImagenPerfil Where id = @Id");
                datos.SetearParametros("@Nombre", (object)usuario.Nombre ?? DBNull.Value);
                datos.SetearParametros("@Apellido", (object)usuario.Apellido ?? DBNull.Value);
                datos.SetearParametros("@UrlImagenPerfil", (object)usuario.UrlImagenPerfil ?? DBNull.Value);
                datos.SetearParametros("@Id", usuario.Id);

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

        public Usuario ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("Select id, email, nombre, apellido, admin, urlImagenPerfil From Users Where id = @Id");
                datos.SetearParametros("@Id", id);
                datos.EjecutarLectura();

                if (!datos.Lector.Read())
                    return null;

                Usuario obtenido = new Usuario();
                obtenido.Id = (int)datos.Lector["id"];
                obtenido.Email = (string)datos.Lector["email"];
                obtenido.Nombre = datos.Lector["nombre"] is DBNull ? null : (string)datos.Lector["nombre"];
                obtenido.Apellido = datos.Lector["apellido"] is DBNull ? null : (string)datos.Lector["apellido"];
                obtenido.Admin = (bool)datos.Lector["admin"];
                obtenido.UrlImagenPerfil = datos.Lector["urlImagenPerfil"] is DBNull ? null : (string)datos.Lector["urlImagenPerfil"];

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

    }
}
