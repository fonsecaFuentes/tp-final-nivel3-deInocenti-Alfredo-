using GridFlow.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridFlow.Negocio.Validaciones
{
    public static class ValidadorUsuario
    {
        public static void ValidarParaRegistro(Usuario usuario)
        {
            if (usuario == null)
                throw new Exception("El usuario no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(usuario.Email))
                throw new Exception("El email es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.Pass))
                throw new Exception("La contraseña es obligatoria.");
        }
    }
}
