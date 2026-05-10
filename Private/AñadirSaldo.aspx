<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AñadirSaldo.aspx.cs" Inherits="hada_ProyectoGrupo.Private.AñadirSaldo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center">
        <div>
            <h2>Agregar fondos:</h2>
            <asp:TextBox ID="txtFondos" runat="server" Width="100%"/>
            <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                ControlToValidate="txtNombre" ErrorMessage="Tienes que indicar una cantidad en cifras" ForeColor="Red" />
        </div>
        <div style="margin-top: 20px;">
            <asp:Button ID="btnSumar" runat="server" Text="Añadir" OnClick="btnSumar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
        </div>
    </div>
</asp:Content>
