using GridFlow.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridFlow.Negocio.Utilidades
{
    public static class DBHelper
    {
        public static object TextoNull(string texto)
        {
            return string.IsNullOrWhiteSpace(texto) ? (object)DBNull.Value : texto;
        }

        public static object DecimalNull(decimal? valor)
        {
            return valor.HasValue ? (object)valor.Value : DBNull.Value;
        }

        public static object CategoriaIdONull(Categoria categoria)
        {
            return (categoria != null && categoria.Id > 0) ? (object)categoria.Id : DBNull.Value;
        }

        public static object MarcaIdONull(Marca marca)
        {
            return (marca != null && marca.Id > 0) ? (object)marca.Id : DBNull.Value;
        }
    }
}
