<%@ Page Title="Detalle Torneo" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="DetalleTorneo.aspx.cs"
    Inherits="hada_ProyectoGrupo.Public.DetalleTorneo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="pnlDetalle" runat="server">
        <div class="container py-4">

            <div class="d-flex gap-2 align-items-center mb-4">
                <a href="Torneos.aspx" class="btn btn-outline-secondary btn-sm">
                    ← Volver a Torneos
                </a>
                <asp:Button ID="btnInscribirse" runat="server"
                    Text="Inscribirse"
                    CssClass="btn btn-success btn-sm"
                    OnClick="btnInscribirse_Click" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar"
                    OnClick="btnModificar_Click" CssClass="btn btn-warning btn-sm" Visible="false" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar"
                    OnClick="btnEliminar_Click" CssClass="btn btn-danger btn-sm" Visible="false" />
                <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />
            </div>

            <div class="card shadow">
                <div class="card-header bg-primary text-white">
                    <h3 class="mb-0"><asp:Label ID="lblNombre" runat="server" /></h3>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <h5 class="text-muted mb-3">Información general</h5>
                            <table class="table table-borderless table-sm">
                                <tr>
                                    <th>Videojuego</th>
                                    <td><asp:Label ID="lblVideojuego" runat="server" /></td>
                                </tr>
                                <tr>
                                    <th>Nivel</th>
                                    <td><asp:Label ID="lblProfesional" runat="server" /></td>
                                </tr>
                                <tr>
                                    <th>Descripción</th>
                                    <td><asp:Label ID="lblDescripcion" runat="server" /></td>
                                </tr>
                                <tr>
                                    <th>Ubicacion</th>
                                    <td><asp:Label ID="lblUbicacion" runat="server" /></td>
                                </tr>
                                <tr>
                                    <th>Capacidad</th>
                                    <td><asp:Label ID="lblCapacidad" runat="server" /></td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-md-6">
                            <h5 class="text-muted mb-3">Datos económicos</h5>
                            <table class="table table-borderless table-sm">
                                <tr>
                                    <th>Precio inscripción</th>
                                    <td><asp:Label ID="lblPrecioInscripcion" runat="server" /></td>
                                </tr>
                                <tr>
                                    <th>Coste organización</th>
                                    <td><asp:Label ID="lblCosteOrganizacion" runat="server" /></td>
                                </tr>
                                <tr>
                                    <th>Premio</th>
                                    <td><asp:Label ID="lblPremio" runat="server" /></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

            <div class="container mt-4">
                <h4>Equipos inscritos</h4>
                <asp:Repeater ID="rptEquipos" runat="server">
                    <HeaderTemplate>
                        <ul class="list-group">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li class="list-group-item d-flex justify-content-between align-items-center">
                            <%# Eval("Nombre") %>
                            <span class="badge bg-success">Inscrito</span>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Label ID="lblSinEquipos" runat="server" Text="No hay equipos inscritos aún."
                    CssClass="text-muted" Visible="false" />
            </div>

        </div>
        <div class="container mt-4">
            <h4>Partidas jugadadas</h4>
            <asp:Repeater ID="rptPartidas" runat="server">
            <HeaderTemplate>
                <ul class="list-group">
            </HeaderTemplate>
            <ItemTemplate>
                <li class="list-group-item d-flex justify-content-between align-items-center">
                    <a href="DetallesPartida.aspx?torneo=<%# Eval("Torneo") %>&codigo=<%# Eval("Code") %>"> <%# Eval("Code") %></a>
                    <span class="badge bg-success">Jugado</span>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
            </asp:Repeater>

            <asp:Label ID="lblSinPartidas" runat="server" Text="No hay partidas jugadas aún." 
                   CssClass="text-muted" Visible="false"/>
            <br>
            <asp:Button id="btnCreatePartida" visible="false" class="btn btn-success" Text="+ Crear partida" runat="server" OnClick="btnCreatePartida_Click"/>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="text-center py-5">
        <h3 class="text-danger">Torneo no encontrado</h3>
        <a href="Torneos.aspx" class="btn btn-primary mt-3">Volver a la lista</a>
    </asp:Panel>

</asp:Content>