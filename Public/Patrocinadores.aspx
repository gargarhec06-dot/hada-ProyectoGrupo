<%@ Page Title="Patrocinadores" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeBehind="Patrocinadores.aspx.cs" 
    Inherits="hada_ProyectoGrupo.Public.Patrocinadores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Patrocinadores</h2>

    <asp:Repeater ID="rptPatrocinadores" runat="server">
        <ItemTemplate>
            <div>
                <h3><%# Eval("Nombre") %></h3>
                <p>Email: <%# Eval("Email") %></p>
                <p>Web: <a href='<%# Eval("PaginaWeb") %>'><%# Eval("PaginaWeb") %></a></p>
                <p>Estado: <%# (bool)Eval("Activo") ? "Activo" : "Inactivo" %></p>
                <asp:HyperLink runat="server" 
                    NavigateUrl='<%# "~/Public/DetallePatrocinador.aspx?id=" + Eval("IdPatrocinador") %>'
                    Text="Ver detalle"/>
                <hr/>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>