<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inscripcion.aspx.cs" Inherits="hada_ProyectoGrupo.Public.Inscripcion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="px-3 mt-3">
        <div class="d-flex align-items-center mb-2">
            <h2 class="me-3">Inscribir equipo</h2>
        </div>
        <hr class="mb-4" />

        <asp:Panel ID="pnlFormulario" runat="server">
            <div style="width:500px;">
                <div style="margin-bottom:10px;">
                    <strong>Torneo:</strong><br />
                    <asp:Label ID="lblTorneo" runat="server" />
                </div>
                <div style="margin-bottom:10px;">
                    <strong>Selecciona tu equipo:</strong><br />
                    <asp:DropDownList ID="ddlEquipos" runat="server" CssClass="form-select" Width="100%" />
                </div>
                <div style="margin-top:20px;">
                    <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar Inscripcion"
                        CssClass="btn btn-success" OnClick="btnConfirmar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                        CssClass="btn btn-outline-secondary ms-2" OnClick="btnCancelar_Click" />
                </div>
                <div style="margin-top:10px;">
                    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="text-center py-5">
            <h3 class="text-danger">No puedes inscribirte en este torneo</h3>
            <asp:Label ID="lblError" runat="server" CssClass="d-block mb-3" />
            <a href="Torneos.aspx" class="btn btn-primary">Volver a Torneos</a>
        </asp:Panel>
    </div>
</asp:Content>
