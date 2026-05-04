<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="GestionPatrocinador.aspx.cs" Inherits="hada_ProyectoGrupo.Private.GestionPatrocinador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 id="tituloPagina" runat="server">Nuevo Patrocinador</h2>

    <div>
        <asp:Label runat="server" Text="Nombre:"/>
        <asp:TextBox ID="txtNombre" runat="server"/>
        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
            ErrorMessage="El nombre es obligatorio" Display="Dynamic"/>
    </div>

    <div>
        <asp:Label runat="server" Text="Teléfono:"/>
        <asp:TextBox ID="txtTelefono" runat="server"/>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTelefono"
            ErrorMessage="El teléfono es obligatorio" Display="Dynamic"/>
    </div>

    <div>
        <asp:Label runat="server" Text="Email:"/>
        <asp:TextBox ID="txtEmail" runat="server"/>
        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
            ErrorMessage="El email es obligatorio" Display="Dynamic"/>
        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
            ValidationExpression="^[\w\.-]+@[\w\.-]+\.\w+$"
            ErrorMessage="Email no válido" Display="Dynamic"/>
    </div>

    <div>
        <asp:Label runat="server" Text="Página Web:"/>
        <asp:TextBox ID="txtWeb" runat="server"/>
    </div>

    <div>
        <asp:Label runat="server" Text="Inicio Contrato:"/>
        <asp:TextBox ID="txtInicioContrato" runat="server" TextMode="Date"/>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtInicioContrato"
            ErrorMessage="La fecha de inicio es obligatoria" Display="Dynamic"/>
    </div>

    <div>
        <asp:Label runat="server" Text="Fin Contrato:"/>
        <asp:TextBox ID="txtFinContrato" runat="server" TextMode="Date"/>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFinContrato"
            ErrorMessage="La fecha de fin es obligatoria" Display="Dynamic"/>
    </div>

    <%-- Sección de torneos a patrocinar --%>
    <h3>Torneos a patrocinar</h3>
    <asp:Repeater ID="rptTorneos" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>Seleccionar</th>
                    <th>Torneo</th>
                    <th>Cantidad (€)</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
          <tr>
            <td>
            <asp:CheckBox runat="server" ID="chkTorneo"/>
            <asp:HiddenField runat="server" ID="hfCodigoTorneo" Value='<%# Eval("Codigo") %>'/>
             </td>
                <td><%# Eval("Nombre") %></td>
                <td>
                    <asp:TextBox runat="server" ID="txtCantidad" Width="80px" Text="0"/>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>

    <br/>
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click"/>
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"/>
    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"/>
</asp:Content>