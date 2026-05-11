<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Equipos.aspx.cs" Inherits="hada_ProyectoGrupo.Public.Equipos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style=" padding:10px; text-align:center; "><h2>Equipos</h2></div>   

   <div style="display:grid; grid-template-columns:repeat(3,1fr); gap:20px;">
    <asp:Repeater ID="rptEquipos" runat="server">
        <ItemTemplate>
            <div style="border:1px solid #ccc; padding:10px; text-align:center;">
                <h3><%# Eval("Nombre") %></h3>
                <img src='<%# Eval("Logo_url") %>' alt="Logo" width="100">
                <asp:HyperLink runat="server" 
                    NavigateUrl='<%# "~/Public/DetallesEquipo.aspx?id=" + Eval("Id_equipo") %>'
                    Text="Ver detalle"/>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
    <asp:Panel ID="pnlJugador" runat="server" Visible="false" style="margin-top: 20px;">
        <asp:Button ID="btnCrear" runat="server" Text="CREAR NUEVO EQUIPO" OnClick="btnCrear_Click" CssClass="btn btn-success" />
    </asp:Panel>
</asp:Content>

