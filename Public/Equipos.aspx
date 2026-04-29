<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Equipos.aspx.cs" Inherits="hada_ProyectoGrupo.Public.Equipos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <h2>Equipos</h2>

    <asp:Repeater ID="rptEquipos" runat="server">
        <ItemTemplate>
            <div>
                <h3><%# Eval("Nombre") %></h3>
                <img src='<%# Eval("Logo_url") %>' alt="Logo" width="100" />
                <asp:HyperLink runat="server" 
                    NavigateUrl='<%# "~/Public/DetallesEquipo.aspx?id=" + Eval("Id_equipo") %>'
                    Text="Ver detalle"/>
                <hr/>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
