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

        public bool FiltroAvanzado { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            FiltroAvanzado = chkAvanzado.Checked;

            CargarTarjetas();
        }

        public void CargarTarjetas()
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            ListaArticulos = articuloNegocio.Listar();
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                List<Articulo> listaCompleta = articuloNegocio.Listar();

                string texto = txtFiltro.Text.ToUpper();

                ListaArticulos = listaCompleta.FindAll(x => 
                        (!string.IsNullOrEmpty(x.Nombre) && x.Nombre.ToUpper().Contains(texto)) || 
                        (!string.IsNullOrEmpty(x.Descripcion) && x.Descripcion.ToUpper().Contains(texto))
                );
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void chkAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            FiltroAvanzado = chkAvanzado.Checked;
            txtFiltro.Enabled = !FiltroAvanzado;
            LimpiarControlesFiltro();
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string campo = ddlCampo.SelectedValue;

            LimpiarControlesFiltro();

            if (string.IsNullOrWhiteSpace(campo))
                return;

            CargarOperadores(campo);

            ddlOperador.Visible = true;
            ddlOperador.Enabled = true;
            lblOperador.Visible = true;

            switch (campo)
            {
                case "Precio":
                    CargarOperadores(campo);
                    grpOperador.Visible = true;
                    grpValor1.Visible = true;

                    ddlOperador.Enabled = true;
                    txtValor1.Enabled = true;
                    txtValor1.Attributes["placeholder"] = "Ingrese valor";
                    break;

                case "Nombre":
                case "Codigo":
                    CargarOperadores(campo);
                    grpOperador.Visible = true;
                    grpValor1.Visible = true;

                    ddlOperador.Enabled = true;
                    txtValor1.Enabled = true;
                    txtValor1.Attributes["placeholder"] = "Ingrese valor";
                    break;

                case "Marca":
                case "Categoria":
                    grpValorLista.Visible = true;
                    ddlValor.Enabled = true;
                    CargarValoresLista(campo);
                    break;
            }
        }

        protected void ddlOperador_SelectedIndexChanged(object sender, EventArgs e)
        {
            string campo = ddlCampo.SelectedValue;
            string operador = ddlOperador.SelectedValue;

            grpValor2.Visible = false;
            txtValor2.Enabled = false;
            txtValor2.Text = string.Empty;

            if (campo == "Precio" && operador == "Entre")
            {
                grpValor2.Visible = true;
                txtValor2.Enabled = true;
                txtValor2.Attributes["placeholder"] = "Ingrese valor máximo";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();

                FiltroArticulo filtro = new FiltroArticulo();
                filtro.Campo = ddlCampo.SelectedValue;

                if (filtro.Campo == "Marca" || filtro.Campo == "Categoria")
                {
                    filtro.Operador = "Igual";
                    filtro.Valor1 = ddlValor.SelectedValue;
                }
                     
                else
                {
                    filtro.Operador = ddlOperador.SelectedValue;
                    filtro.Valor1 = txtValor1.Text;
                    filtro.Valor2 = txtValor2.Text;
                }

                ListaArticulos = negocio.Filtrar(filtro);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnLimpiarCampos_Click(object sender, EventArgs e)
        {
            ddlCampo.SelectedIndex = 0;
            ddlOperador.Items.Clear();
            ddlValor.Items.Clear();

            txtValor1.Text = string.Empty;
            txtValor2.Text = string.Empty;

            LimpiarControlesFiltro();
            CargarTarjetas();
        }

        private void LimpiarControlesFiltro()
        {
            ddlOperador.Items.Clear();
            ddlValor.Items.Clear();

            txtValor1.Text = string.Empty;
            txtValor2.Text = string.Empty;

            grpOperador.Visible = false;
            grpValor1.Visible = false;
            grpValor2.Visible = false;
            grpValorLista.Visible = false;

            txtValor1.Enabled = false;
            txtValor2.Enabled = false;
            ddlValor.Enabled = false;
            ddlOperador.Enabled = false;
        }

        private void CargarOperadores(string campo)
        {
            ddlOperador.Items.Clear();

            switch (campo)
            {
                case "Precio":
                    ddlOperador.Items.Add(new ListItem("Mayor a", "Mayor"));
                    ddlOperador.Items.Add(new ListItem("Menor a", "Menor"));
                    ddlOperador.Items.Add(new ListItem("Entre", "Entre"));
                    break;

                case "Nombre":
                case "Codigo":
                    ddlOperador.Items.Add(new ListItem("Comienza con", "Comienza"));
                    ddlOperador.Items.Add(new ListItem("Termina con", "Termina"));
                    ddlOperador.Items.Add(new ListItem("Contiene", "Contiene"));
                    break;

                case "Marca":
                case "Categoria":
                    ddlOperador.Items.Add(new ListItem("Igual a", "Igual"));
                    break;
            }
        }

        private void CargarValoresLista(string campo)
        {
            ddlValor.Items.Clear();

            if (campo == "Marca")
            {
                lblValor.Text = "Seleccione Marca";

                MarcaNegocio negocio = new MarcaNegocio();
                ddlValor.DataSource = negocio.Listar();
                ddlValor.DataTextField = "Descripcion";
                ddlValor.DataValueField = "Id";
                ddlValor.DataBind();
            }
            else if (campo == "Categoria")
            {
                lblValor.Text = "Seleccione Categoría";

                CategoriaNegocio negocio = new CategoriaNegocio();
                ddlValor.DataSource = negocio.Listar();
                ddlValor.DataTextField = "Descripcion";
                ddlValor.DataValueField = "Id";
                ddlValor.DataBind();
            }
        }

    }
}