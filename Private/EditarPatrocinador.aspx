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
            <strong>Teléfono:</strong><br />
            <asp:TextBox ID="txtTelefono" runat="server" Width="100%" />
            <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" 
                ControlToValidate="txtTelefono" ErrorMessage="*" ForeColor="Red" />
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
    </div>

    <!-- SECCIÓN DE TORNEOS PATROCINADOS -->
    <h3>Torneos que patrocina</h3>
    <asp:Repeater ID="rptTorneos" runat="server">
        <HeaderTemplate>
            <table border="1" cellpadding="5" cellspacing="0">
                <tr>
                    <th>Patrocina</th>
                    <th>Torneo</th>
                    <th>Cantidad (€)</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:CheckBox ID="chkPatrocina" runat="server" />
                    <asp:HiddenField ID="hfCodigoTorneo" runat="server" Value='<%# Eval("Codigo") %>' />
                </td>
                <td><%# Eval("Nombre") %></td>
                <td>
                    <asp:TextBox ID="txtCantidad" runat="server" Width="80px" Text="0" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>

    <div style="margin-top: 20px;">
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" 
            OnClick="btnGuardar_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
            OnClick="btnCancelar_Click" CssClass="btn btn-secondary" />
    </div>

    <div style="margin-top: 10px;">
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />
    </div>
</asp:Content>