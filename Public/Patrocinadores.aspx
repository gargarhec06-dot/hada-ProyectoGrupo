<%@ Page Title="Patrocinadores" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeFile="Patrocinadores.aspx.cs" 
    Inherits="hada_ProyectoGrupo.Public.Patrocinadores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Patrocinadores</h2>

    <div style="margin-bottom: 20px;">
        <div style="margin-bottom: 10px;">
            <strong>Buscar por nombre:</strong><br />
            <asp:TextBox ID="txtNombre" runat="server" Width="300px" placeholder="Ej: Samsung" />
        </div>
        <div style="margin-bottom: 10px;">
            <strong>Filtrar por torneo:</strong><br />
            <asp:DropDownList ID="ddlTorneo" runat="server" Width="300px" />
        </div>
        <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar filtros" OnClick="btnLimpiar_Click" CssClass="btn btn-secondary" />
    </div>

    <asp:Label ID="lblResultado" runat="server" ForeColor="Gray" />

    <asp:Repeater ID="rptPatrocinadores" runat="server">
        <ItemTemplate>
            <div>
                <asp:HyperLink runat="server" 
                    NavigateUrl='<%# "~/Public/DetallePatrocinador.aspx?id=" + Eval("IdPatrocinador") %>'
                    Text='<%# Eval("Nombre") %>'/>
                <hr/>
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <asp:Panel ID="pnlAdmin" runat="server" Visible="false" style="margin-top: 20px;">
        <asp:Button ID="btnCrear" runat="server" Text="+ Crear Nuevo Patrocinador" 
            OnClick="btnCrear_Click" CssClass="btn btn-success" />
    </asp:Panel>
</asp:Content>