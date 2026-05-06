<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Jugadores.aspx.cs" Inherits="hada_ProyectoGrupo.Public.Jugadores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding:10px; text-align:center;">
        <h2>Jugadores</h2>
    </div>

    <div style="display:grid; grid-template-columns:repeat(3,1fr); gap:20px;">
        <asp:Repeater ID="rptJugadores" runat="server">
            <ItemTemplate>
                <div style="border:1px solid #ccc; padding:10px; text-align:center;">
                    <div style="background-color: #9370DB; color: white; padding: 20px;">
                        <h1><%# Eval("Apodo") %></h1>
                    </div>
                    <h2><%# Eval("Rol_principal") %></h2>
                    <h3>Equipo: <%# Eval("Equipo_actual") %></h3>
                    <asp:HyperLink ID="hlDetalles" runat="server" 
                        NavigateUrl='<%# "~/Public/DetallesJugador.aspx?codigo=" + Eval("Codigo") %>'
                        Text="Ver detalles" CssClass="btn btn-info" 
                        Visible='<%# Session["Email"] != null && Session["Email"].ToString() == Eval("Email_usuario").ToString() %>' />
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />

</asp:Content>