<%@ Page Title="Detalle Torneo" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="DetalleTorneo.aspx.cs"
    Inherits="hada_ProyectoGrupo.Public.DetalleTorneo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="pnlDetalle" runat="server">
        <div class="container py-4">
            <a href="Torneos.aspx" class="btn btn-outline-secondary btn-sm mb-4">
                ← Volver a Torneos
            </a>
            <asp:Button ID="btnInscribirse" runat="server"
            Text="Inscribirse"
            CssClass="btn btn-success btn-sm mb-4 ms-2"
            OnClick="btnInscribirse_Click" />
            <div class="card shadow">
                <div class="card-header bg-primary text-white">
                    <h3 class="mb-0"><asp:Label ID="lblNombre" runat="server" /></h3>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <h5 class="text-muted mb-3"> Información General</h5>
                            <table class="table table-borderless table-sm">
                                <tr>
                                    <th> Código</th>
                                    <td><asp:Label ID="lblCodigo" runat="server" /></td>
                                </tr>
                                <tr>
                                    <th> Nivel</th>
                                    <td><asp:Label ID="lblProfesional" runat="server" /></td>
                                </tr>
                                <tr>
                                    <th> Descripción</th>
                                    <td><asp:Label ID="lblDescripcion" runat="server" /></td>
                                </tr>
                                <tr>
                                <th>Ubicacion</th>
                                <td><asp:Label ID="lblUbicacion" runat="server" /></td>
                            </tr>
                            </table>
                        </div>
                        <div class="col-md-6">
                            <h5 class="text-muted mb-3"> Datos Económicos</h5>
                            <table class="table table-borderless table-sm">
                                <tr>
                                    <th> Precio Inscripción</th>
                                    <td><asp:Label ID="lblPrecioInscripcion" runat="server" /></td>
                                </tr>
                                <tr>
                                    <th> Coste Organización</th>
                                    <td><asp:Label ID="lblCosteOrganizacion" runat="server" /></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="text-center py-5">
        <h3 class="text-danger"> Torneo no encontrado</h3>
        <a href="Torneos.aspx" class="btn btn-primary mt-3">Volver a la lista</a>
    </asp:Panel>

</asp:Content>