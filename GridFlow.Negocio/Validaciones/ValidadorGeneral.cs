using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridFlow.Negocio.Validaciones
{
    public static class ValidadorGeneral
    {
        public static void ValidarId(int id)
        {
            if (id <= 0)
                throw new Exception("El Id debe ser mayor a cero.");
        }
    }
}
