<%@ Page Title="Detalle Patrocinador" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeBehind="DetallePatrocinador.aspx.cs" 
    Inherits="hada_ProyectoGrupo.Public.DetallePatrocinador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Detalle del Patrocinador</h2>

    <div>
        <p><strong>Nombre:</strong> <asp:Label ID="lblNombre" runat="server"/></p>
        <p><strong>Email:</strong> <asp:Label ID="lblEmail" runat="server"/></p>
        <p><strong>Página Web:</strong> <asp:HyperLink ID="hlWeb" runat="server"/></p>
        <p><strong>Inicio Contrato:</strong> <asp:Label ID="lblInicioContrato" runat="server"/></p>
        <p><strong>Fin Contrato:</strong> <asp:Label ID="lblFinContrato" runat="server"/></p>
        <p><strong>Telefono:</strong> <asp:Label ID="lbltelefono" runat="server"/></p>
    </div>


    <h3>Torneos patrocinados</h3>
    <asp:Repeater ID="rptTorneos" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>Torneo</th>
                    <th>Cantidad aportada</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
                <tr>
                    <td><%# Eval("NombreTorneo") %></td>
                    <td><%# Eval("Cantidad") %> €</td>
                </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>

        <!-- el boton de volver siempre está visible pero el de editar u eliminar solo este cuando haya sesión iniciada con cuenta de administrador -->

<div style="margin-top: 20px;">

    <asp:Button ID="btnVolver" runat="server" Text="Volver" 
        OnClick="btnVolver_Click" CssClass="btn btn-secondary" />


    <asp:Panel ID="pnlAdmin" runat="server" Visible="false" style="display: inline-block; margin-left: 0px;">
        <asp:Button ID="btnEditar" runat="server" Text=" Editar" 
            OnClick="btnEditar_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnEliminar" runat="server" Text=" Eliminar" 
            OnClick="btnEliminar_Click" 
            OnClientClick="return confirm('¿Estás seguro de eliminar este patrocinador?');"
            CssClass="btn btn-danger" />
    </asp:Panel>
</div>
</asp:Content>