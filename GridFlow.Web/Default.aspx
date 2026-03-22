<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GridFlow.Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
        <div class="card-header border-secondary py-3">
            <p class="mb-0 text-secondary">Llegaste a la GridFlow, tu lugar de compras...</p>
        </div>

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
                            <span class="badge text-bg-secondary">Pokémon</span>
                            <a href="DetalleArticulo.aspx?id=<%: articulo.Id %>" class="link-info text-decoration-none fw-semibold">Ver más &rarr;</a>
                        </div>
                    </div>
                </div>

                <%     }
                    } %>
            </div>
        </div>
    </div>

</asp:Content>
