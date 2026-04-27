<%@ Page Title="Detalle Patrocinador" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeBehind="DetallePatrocinador.aspx.cs" 
    Inherits="hada_ProyectoGrupo.Public.DetallePatrocinador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Detalle del Patrocinador</h2>

    <div>
        <asp:Label runat="server" Text="Nombre: "/>
        <asp:Label ID="lblNombre" runat="server"/>
    </div>

    <div>
        <asp:Label runat="server" Text="Email: "/>
        <asp:Label ID="lblEmail" runat="server"/>
    </div>

    <div>
        <asp:Label runat="server" Text="Página Web: "/>
        <asp:HyperLink ID="hlWeb" runat="server"/>
    </div>

    <div>
        <asp:Label runat="server" Text="Inicio Contrato: "/>
        <asp:Label ID="lblInicioContrato" runat="server"/>
    </div>

    <div>
        <asp:Label runat="server" Text="Fin Contrato: "/>
        <asp:Label ID="lblFinContrato" runat="server"/>
    </div>

    <div>
        <asp:Label runat="server" Text="Estado: "/>
        <asp:Label ID="lblActivo" runat="server"/>
    </div>

    <asp:Button ID="btnVolver" runat="server" Text="Volver" 
        OnClick="btnVolver_Click"/>
</asp:Content>