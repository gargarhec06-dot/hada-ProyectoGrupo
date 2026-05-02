<%@ Page Title="Editar Patrocinador" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeBehind="EditarPatrocinador.aspx.cs" 
    Inherits="hada_ProyectoGrupo.Private.EditarPatrocinador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Editar Patrocinador</h2>

    <div style="width: 500px;">
        <div style="margin-bottom: 10px;">
            <strong>Nombre:</strong><br />
            <asp:TextBox ID="txtNombre" runat="server" Width="100%" />
            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                ControlToValidate="txtNombre" ErrorMessage="*" ForeColor="Red" />
        </div>

        <div style="margin-bottom: 10px;">
            <strong>Email:</strong><br />
            <asp:TextBox ID="txtEmail" runat="server" Width="100%" />
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                ControlToValidate="txtEmail" ErrorMessage="*" ForeColor="Red" />
            <asp:RegularExpressionValidator ID="revEmail" runat="server"
                ControlToValidate="txtEmail" 
                ValidationExpression="^[\w\.-]+@[\w\.-]+\.\w+$"
                ErrorMessage="Email no válido" ForeColor="Red" />
        </div>

        <div style="margin-bottom: 10px;">
            <strong>Página Web:</strong><br />
            <asp:TextBox ID="txtWeb" runat="server" Width="100%" />
        </div>

        <div style="margin-bottom: 10px;">
            <strong>Inicio Contrato:</strong><br />
            <asp:TextBox ID="txtInicioContrato" runat="server" Width="100%" TextMode="Date" />
        </div>

        <div style="margin-bottom: 10px;">
            <strong>Fin Contrato:</strong><br />
            <asp:TextBox ID="txtFinContrato" runat="server" Width="100%" TextMode="Date" />
        </div>

        <div style="margin-bottom: 10px;">
            <strong>Activo:</strong><br />
            <asp:CheckBox ID="chkActivo" runat="server" Checked="true" />
        </div>

        <div style="margin-top: 20px;">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" 
                OnClick="btnGuardar_Click" CssClass="btn btn-primary" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                OnClick="btnCancelar_Click" CssClass="btn btn-secondary" />
        </div>

        <div style="margin-top: 10px;">
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />
        </div>
    </div>
</asp:Content>