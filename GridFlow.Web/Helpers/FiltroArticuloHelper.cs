using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GridFlow.Dominio;
using GridFlow.Negocio;
using System.Web.UI.WebControls;

namespace GridFlow.Web.Helpers
{
    public static class FiltroArticuloHelper
    {
        public static bool UsaLista(string campo)
        {
            return campo == "Marca" || campo == "Categoria";
        }

        public static bool UsaOperador(string campo)
        {
            return campo == "Precio" || campo == "Nombre" || campo == "Codigo";
        }

        public static bool UsaSegundoValor(string campo, string operador)
        {
            return campo == "Precio" || operador == "Entre";
        }

        public static void CargarOperadores(DropDownList ddlOperador, string campo)
        {
            ddlOperador.Items.Clear();

            switch(campo)
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

        public static void CargarValoresLista(DropDownList ddlValor, Label lblValor, string campo)
        {
            ddlValor.Items.Clear();

            if (campo == "Marca")
            {
                lblValor.Text = "Seleccione Marca";

                MarcaNegocio marcaNegocio = new MarcaNegocio();
                ddlValor.DataSource = marcaNegocio.Listar();
                ddlValor.DataTextField = "Descripcion";
                ddlValor.DataValueField = "Id";
                ddlValor.DataBind();
            }
            else if (campo == "Categoria")
            {
                lblValor.Text = "Seleccione Categoría";

                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                ddlValor.DataSource = categoriaNegocio.Listar();
                ddlValor.DataTextField = "Descripcion";
                ddlValor.DataValueField = "Id";
                ddlValor.DataBind();
            }
        }

        public static FiltroArticulo CrearFiltro(string campo, string operador, string valor1, string valor2, string valorLista)
        {
            FiltroArticulo filtro = new FiltroArticulo();
            filtro.Campo = campo;

            if (UsaLista(campo))
            {
                filtro.Operador = "Igual";
                filtro.Valor1 = valorLista;
                filtro.Valor2 = string.Empty;
            }
            else
            {
                filtro.Operador = operador;
                filtro.Valor1 = valor1?.Trim();
                filtro.Valor2 = valor2?.Trim();
            }

            return filtro;
        }

        public static void ValidarFiltro(FiltroArticulo filtro)
        {
            if (filtro == null)
                throw new Exception("El filtro no puede ser nulo");

            if (string.IsNullOrWhiteSpace(filtro.Campo))
                throw new Exception("Debe seleccionar un campo para filtrar");

            if (UsaLista(filtro.Campo))
            {
                if (string.IsNullOrWhiteSpace(filtro.Valor1))
                    throw new Exception("Debe seleccionar un valor de la lista");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(filtro.Operador))
                    throw new Exception("Debe selseccionar un criterio");

                if (string.IsNullOrWhiteSpace(filtro.Valor1))
                    throw new Exception("Debe ingresar un valor para filtrar.");

                if (filtro.Campo == "Precio")
                {
                    decimal numero;

                    if (!decimal.TryParse(filtro.Valor1, out numero))
                        throw new Exception("El valor ingresado para Precio no es válido.");

                    if (filtro.Operador == "Entre")
                    {
                        decimal numero2;

                        if (string.IsNullOrWhiteSpace(filtro.Valor2))
                            throw new Exception("Debe ingresar el segundo valor del rango.");

                        if (!decimal.TryParse(filtro.Valor2, out numero2))
                            throw new Exception("El segundo valor ingresado para Precio no es válido.");
                    }
                }
            }
        }
    }
}