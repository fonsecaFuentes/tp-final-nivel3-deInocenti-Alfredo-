<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GridFlow.Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="d-flex align-items-center justify-content-between mb-4">
        <div>
            <h1 class="h3 mb-0 fw-bold">GridFlow</h1>
            <div class="text-secondary small mt-1">GridFlow Web • catálogo y detalle</div>
        </div>
        <a href="#" class="btn btn-outline-light btn-sm">
            <i class="bi bi-list-ul me-1"></i>Ver lista
        </a>
    </div>

    <div class="card shadow-lg border-secondary rounded-4 overflow-hidden">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <div class="card-header border-secondary py-3">
                    <div class="row align-items-center">
                        <div class="col-12 col-md-6">
                            <p class="mb-0 text-secondary">Llegaste a la GridFlow, tu lugar de compras...</p>
                        </div>

                        <div class="col-12 col-md-6 d-flex align-items-center justify-content-md-end mt-2 mt-md-0">
                            <asp:Label Text="Filtrar" runat="server" CssClass="form-label fw-semibold mb-0 me-2"></asp:Label>
                            <asp:TextBox
                                runat="server"
                                ID="txtFiltro"
                                AutoPostBack="true"
                                OnTextChanged="txtFiltro_TextChanged"
                                CssClass="form-control form-control-sm w-50"
                                placeholder="Buscar por nombre o descripción...">
                            </asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="px-4 py-3 border-bottom border-secondary-subtle">
                    <div class="d-flex align-items-center gap-2">
                        <asp:CheckBox
                            ID="chkAvanzado"
                            runat="server"
                            AutoPostBack="true"
                            OnCheckedChanged="chkAvanzado_CheckedChanged"
                            CssClass="form-check-input m-0" />

                        <div class="ms-2">
                            <asp:Label runat="server" AssociatedControlID="chkAvanzado" CssClass="form-check-label fw-bold d-block" Text="Filtro avanzado"></asp:Label>
                            <span class="text-secondary small">Más opciones de búsqueda</span>
                        </div>
                    </div>
                </div>

                <!-- Card: filtro avanzado -->
                <% if (FiltroAvanzado)
                    { %>
                <div class="p-4 bg-body-tertiary border-bottom border-secondary">
                    <div class="card shadow-sm border-secondary rounded-3">
                        <div class="card-body p-3 p-md-4">

                            <div class="d-flex align-items-center gap-2 mb-3">
                                <i class="bi bi-funnel-fill text-info"></i>
                                <h2 class="h6 m-0 fw-bold">Configuración del filtro</h2>
                            </div>

                            <div class="row g-3 align-items-end">
                                <div class="col-12 col-md-3">
                                    <asp:Label Text="Campo" runat="server" CssClass="form-label small fw-semibold" />
                                    <asp:DropDownList
                                        runat="server"
                                        AutoPostBack="true"
                                        ID="ddlCampo"
                                        CssClass="form-select form-select-sm"
                                        OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged">
                                        <asp:ListItem Text="Seleccione" Value="" />
                                        <asp:ListItem Text="Código" Value="Codigo" />
                                        <asp:ListItem Text="Categoría" Value="Categoria" />
                                        <asp:ListItem Text="Nombre" Value="Nombre" />
                                        <asp:ListItem Text="Marca" Value="Marca" />
                                        <asp:ListItem Text="Precio" Value="Precio" />
                                    </asp:DropDownList>
                                </div>

                                <div runat="server" id="grpOperador" class="col-12 col-md-3">
                                    <asp:Label runat="server" ID="lblOperador" Text="Criterio" CssClass="form-label small fw-semibold"></asp:Label>
                                    <asp:DropDownList
                                        runat="server"
                                        AutoPostBack="true"
                                        ID="ddlOperador"
                                        CssClass="form-select form-select-sm"
                                        OnSelectedIndexChanged="ddlOperador_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>

                                <div runat="server" id="grpValor1" class="col-12 col-md-3">
                                    <asp:Label Text="Valor a buscar" ID="lblValor1" runat="server" CssClass="form-label small fw-semibold" />
                                    <asp:TextBox
                                        runat="server"
                                        ID="txtValor1"
                                        CssClass="form-control form-control-sm"
                                        placeholder="Ingresar dato..." />
                                </div>

                                <div runat="server" id="grpValor2" class="col-12 col-md-3">
                                    <asp:Label Text="Hasta (Opcional)" ID="lblValor2" runat="server" CssClass="form-label small fw-semibold" />
                                    <asp:TextBox
                                        runat="server"
                                        ID="txtValor2"
                                        CssClass="form-control form-control-sm"
                                        placeholder="Límite del rango..." />
                                </div>

                                <div runat="server" id="grpValorLista" class="col-12 col-md-3">
                                    <asp:Label runat="server" ID="lblValor" CssClass="form-label small fw-semibold"></asp:Label>
                                    <asp:DropDownList
                                        runat="server"     
                                        ID="ddlValor"
                                        CssClass="form-select form-select-sm">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-12 mt-4">
                                    <div class="d-flex justify-content-end gap-2">
                                        <asp:Button
                                            Text="Limpiar"
                                            runat="server"
                                            CssClass="btn btn-outline-secondary btn-sm"
                                            ID="btnLimpiarCampos"
                                            OnClick="btnLimpiarCampos_Click" />

                                        <asp:Button
                                            Text="Aplicar Filtro"
                                            runat="server"
                                            CssClass="btn btn-primary btn-sm px-4"
                                            ID="btnBuscar"
                                            OnClick="btnBuscar_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <% } %>

                <div class="card-body p-4">
                    <div class="row row-cols-1 row-cols-sm-2 row-cols-lg-3 row-cols-xl-4 g-4">

                        <% if (ListaArticulos != null)
                            {
                                foreach (GridFlow.Dominio.Articulo articulo in ListaArticulos)
                                { %>

                        <div class="col">
                            <div class="card h-100 border-secondary shadow-sm">
                                <div class="p-3 bg-body-tertiary border-bottom border-secondary" style="height: 200px;">
                                    <img src="<%: articulo.ImagenUrl %>" class="w-100 h-100 object-fit-contain" alt="Imagen de <%: articulo.Nombre %>">
                                </div>

                                <div class="card-body d-flex flex-column">
                                    <h5 class="card-title fw-bold"><%: articulo.Nombre %></h5>
                                    <p class="card-text text-secondary mb-0 text-truncate-multiline"><%: articulo.Descripcion %></p>
                                </div>

                                <div class="card-footer bg-transparent border-0 d-flex justify-content-between align-items-center pb-3">
                                    <span class="badge text-bg-secondary"><%: articulo.Categoria.Descripcion %></span>
                                    <a href="DetalleArticulo.aspx?id=<%: articulo.Id %>" class="link-info text-decoration-none fw-semibold">Ver más &rarr;</a>
                                </div>
                            </div>
                        </div>

                        <%     }
                            } %>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
