using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridFlow.Dominio
{
    public class FiltroArticulo
    {
        public string Campo { get; set; }
        public string Operador { get; set; }
        public string Valor1 { get; set; }
        public string Valor2 { get; set; }
    }
}
