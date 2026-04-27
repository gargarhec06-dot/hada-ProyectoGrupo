<%@ Page Title="Patrocinadores" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeBehind="Patrocinadores.aspx.cs" 
    Inherits="hada_ProyectoGrupo.Public.Patrocinadores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Patrocinadores</h2>

    <asp:GridView ID="gvPatrocinadores" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre"/>
            <asp:BoundField DataField="Email" HeaderText="Email"/>
            <asp:BoundField DataField="PaginaWeb" HeaderText="Página Web"/>
            <asp:BoundField DataField="Activo" HeaderText="Activo"/>
            <asp:HyperLinkField HeaderText="Detalle" Text="Ver detalle"
                DataNavigateUrlFields="IdPatrocinador"
                DataNavigateUrlFormatString="~/Public/DetallePatrocinador.aspx?id={0}"/>
        </Columns>
    </asp:GridView>
</asp:Content>