using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridFlow.Dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ImagenUrl { get; set; }
        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }
        private decimal? precio;
        public decimal? Precio
        {
            get { return precio.HasValue ? Math.Round(precio.Value, 2) : (decimal?)null; }
            set { precio = value; }
        }
    }
}
