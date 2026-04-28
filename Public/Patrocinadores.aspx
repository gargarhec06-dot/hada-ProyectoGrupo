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