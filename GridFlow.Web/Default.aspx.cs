using GridFlow.Dominio;
using GridFlow.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GridFlow.Web
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulos { get; set; } = new List<Articulo>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarTarjetas();
        }

        public void CargarTarjetas()
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            ListaArticulos = articuloNegocio.Listar();
        }
    }
}