using GridFlow.Dominio;
using GridFlow.Negocio;
using GridFlow.Web.Helpers;
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

            if (!IsPostBack)
            {
                txtFiltro.Enabled = true;
                LimpiarControlesFiltro();
            }
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

            if (!FiltroAvanzado)
                txtFiltro.Text = string.Empty;

            LimpiarControlesFiltro();
            CargarTarjetas();
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string campo = ddlCampo.SelectedValue;

                LimpiarControlesFiltro();

                if (string.IsNullOrWhiteSpace(campo))
                    return;

                if (FiltroArticuloHelper.UsaOperador(campo))
                {
                    lblOperador.Visible = true;
                    ddlOperador.Visible = true;
                    ddlOperador.Enabled = true;

                    lblValor1.Visible = true;
                    txtValor1.Visible = true;
                    txtValor1.Enabled = true;
                    txtValor1.Attributes["placeholder"] = "Ingrese valor";

                    FiltroArticuloHelper.CargarOperadores(ddlOperador, campo);
                }
                else if (FiltroArticuloHelper.UsaLista(campo))
                {
                    lblValor.Visible = true;
                    ddlValor.Visible = true;
                    ddlValor.Enabled = true;

                    FiltroArticuloHelper.CargarValoresLista(ddlValor, lblValor, campo);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void ddlOperador_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblValor2.Visible = false;
                txtValor2.Visible = false;
                txtValor2.Enabled = false;
                txtValor2.Text = string.Empty;

                if (FiltroArticuloHelper.UsaSegundoValor(ddlCampo.SelectedValue, ddlOperador.SelectedValue))
                {
                    lblValor2.Visible = true;
                    txtValor2.Visible = true;
                    txtValor2.Enabled = true;
                    txtValor2.Attributes["placeholder"] = "Ingrese valor máximo";
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();

                FiltroArticulo filtro = FiltroArticuloHelper.CrearFiltro(ddlCampo.SelectedValue, ddlOperador.SelectedValue, txtValor1.Text, txtValor2.Text, ddlValor.SelectedValue);

                FiltroArticuloHelper.ValidarFiltro(filtro);

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
            try
            {
                ddlCampo.SelectedIndex = 0;

                if (ddlOperador.Items.Count > 0)
                    ddlOperador.SelectedIndex = -1;

                if (ddlValor.Items.Count > 0)
                    ddlValor.SelectedIndex = 0;

                txtValor1.Text = string.Empty;
                txtValor2.Text = string.Empty;

                LimpiarControlesFiltro();
                CargarTarjetas();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        private void LimpiarControlesFiltro()
        {
            ddlOperador.Items.Clear();
            ddlValor.Items.Clear();

            txtValor1.Text = string.Empty;
            txtValor2.Text = string.Empty;

            lblOperador.Visible = false;
            ddlOperador.Visible = false;
            ddlOperador.Enabled = false;

            lblValor1.Visible = false;
            txtValor1.Visible = false;
            txtValor1.Enabled = false;

            lblValor2.Visible = false;
            txtValor2.Visible = false;
            txtValor2.Enabled = false;

            lblValor.Visible = false;
            ddlValor.Visible = false;
            ddlValor.Enabled = false;
        }
    }
}