<%@ Page Title="Nuevo torneo" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeBehind="GestionTorneo.aspx.cs" 
    Inherits="hada_ProyectoGrupo.Private.GestionTorneo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 id="tituloPagina" runat="server">Nuevo Torneo</h2>
    <div style="width: 500px;">

        <div style="margin-bottom: 10px;">
            <strong>Videojuego: </strong><br />
            <asp:DropDownList ID="ddlVideojuego" runat="server">
                <asp:ListItem Text="Selecciona un videojuego" Value="0"/>
            </asp:DropDownList>
        </div>

        <div style="margin-bottom: 10px;">
            <strong>Nombre:</strong><br />
            <asp:TextBox ID="txtNombre" runat="server" Width="100%" />
            <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                ControlToValidate="txtNombre" Text="*" ForeColor="Red" />
        </div>

        <div style="margin-bottom: 10px;">
            <strong>Descripción:</strong><br />
            <asp:TextBox ID="txtDescripcion" runat="server" Width="100%" />
        </div>

        <div style="margin-bottom: 10px;">
            <strong>Fecha:</strong><br />
            <asp:TextBox ID="txtFecha" runat="server" Width="100%" TextMode="Date" />
            <asp:RequiredFieldValidator ID="rfvFecha" runat="server"
                ControlToValidate="txtFecha" Text="*" ForeColor="Red" />
        </div>

        <div style="margin-bottom: 10px;">
            <strong>Precio Inscripción:</strong><br />
            <asp:TextBox ID="txtInscripcion" runat="server" Width="100%" />
            <asp:RequiredFieldValidator ID="rfvInscripcion" runat="server"
                ControlToValidate="txtInscripcion" Text="*" ForeColor="Red" />
        </div>

        <div style="margin-bottom: 10px;">
            <strong>Coste Organización:</strong><br />
            <asp:TextBox ID="txtOrganizacion" runat="server" Width="100%" />
            <asp:RequiredFieldValidator ID="rfvOrganizacion" runat="server"
                ControlToValidate="txtOrganizacion" Text="*" ForeColor="Red" />
        </div>

        <div style="margin-bottom: 10px;">
            <strong>Ubicacion:</strong><br />
            <asp:TextBox ID="txtUbicacion" runat="server" Width="100%" />
        </div>

        <div style="margin-bottom: 10px;">
            <strong>Premio:</strong><br />
            <asp:TextBox ID="txtPremio" runat="server" Width="100%" />
            <asp:RegularExpressionValidator ID="revPremio" runat="server" ControlToValidate="txtPremio" 
                ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="Solo se permiten números" ForeColor="Red" /> 
        </div>

        <div style="margin-bottom: 10px;">
            <strong>Capacidad:</strong><br />
            <asp:TextBox ID="txtCapacidad" runat="server" Width="100%" />
            <asp:RegularExpressionValidator ID="revCapacidad" runat="server" ControlToValidate="txtCapacidad" 
                ValidationExpression="^\d+$" ErrorMessage="Solo se permiten números enteros positivos" ForeColor="Red" /> 
        </div>

        <div style="margin-bottom: 10px;">
            <strong>Profesional:</strong><br />
            <asp:CheckBox ID="chkProfesional" runat="server" Checked="false" />
        </div>

        <div style="margin-bottom: 10px;">
            <strong>URL Logo:</strong><br />
            <asp:TextBox ID="txtUrlLogo" runat="server" Width="100%" />
        </div>

        <div style="margin-bottom: 10px;">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CausesValidation="false"/>
        </div>

        <div style="margin-bottom: 10px;">
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />
        </div>

    </div>
</asp:Content>