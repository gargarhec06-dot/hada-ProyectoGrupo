<%@ Page Title="Torneos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Torneos.aspx.cs" Inherits="hada_ProyectoGrupo.Public.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="px-3 mt-4">
        <div class="d-flex align-items-center mb-2">
            <h2 class="me-3">Torneos disponibles</h2>
            <asp:Panel ID="pnlAdmin" runat="server" Visible="false">
                <asp:Button ID="btnCrear" runat="server" Text="+ Crear Torneo" 
                    CssClass="btn btn-success" OnClick="btnCrear_Click" />
            </asp:Panel>
        </div>
        <hr class="mb-4" />
        <div class="row">
            <asp:Repeater ID="rptTorneos" runat="server">
                <ItemTemplate>
                    <div class="col-md-4 mb-4">
                        <div class="card h-100 shadow-sm">
                            <div class="card-body">
                                <h5 class="card-title text-primary">
                                    <%# Eval("Nombre") %>
                                </h5>
                                <p class="card-text small text-muted">
                                    Fecha: <%# Eval("Fecha", "{0:dd/MM/yyyy}") %><br />
                                    Nivel: <%# (bool)Eval("Profesional") ? "Profesional" : "Amateur" %><br />
                                    Inscripción: <%# Eval("PrecioInscripcion") %>€<br />
                                    Ubicacion: <%# Eval("Ubicacion") %>
                                </p>
                            </div>
                            <div class="card-footer bg-transparent border-top-0">
                                <asp:HyperLink runat="server" 
                                    NavigateUrl='<%# "~/Public/DetalleTorneo.aspx?codigo=" + Eval("Codigo") %>'
                                    Text="Ver detalles" CssClass="btn btn-outline-primary btn-sm w-100" />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    
</asp:Content>
