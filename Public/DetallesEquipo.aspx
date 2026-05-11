<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetallesEquipo.aspx.cs" Inherits="hada_ProyectoGrupo.Public.DetallesEquipo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Detalle del Equipo</h2>

    <div style="display:flex; align-items:flex-start; gap:40px;">
        <div>
            <p><strong>Nombre :</strong> <asp:TextBox ID="txtNombre" runat="server" /></p>
            <p><strong>Fecha de Creación :</strong> <asp:TextBox ID="txtFecha" runat="server" /></p>
            <p><strong>Descripción :</strong> <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" /></p>
            <p><strong>URL Logo :</strong> <asp:TextBox ID="txtLogo" runat="server" /></p>
            
            <!-- NUEVO: Subir imagen -->
            <p><strong>O subir imagen (JPG/PNG, máx 2MB):</strong></p>
            <p>
                <asp:FileUpload ID="fuLogo" runat="server" accept="image/jpeg,image/png,image/jpg" />
                <asp:Button ID="btnSubirLogo" runat="server" Text="Subir imagen" OnClick="btnSubirLogo_Click" CssClass="btn btn-secondary btn-sm" />
                <asp:Label ID="lblSubidaLogo" runat="server" ForeColor="Red" />
            </p>
            <p><strong>Máximo de jugadores :</strong> 
                <asp:DropDownList ID="ddlMaxJugadores" runat="server">
                    <asp:ListItem Text="3 jugadores" Value="3" />
                    <asp:ListItem Text="4 jugadores" Value="4" />
                    <asp:ListItem Text="5 jugadores" Value="5" Selected="True" />
                    <asp:ListItem Text="6 jugadores" Value="6" />
                    <asp:ListItem Text="7 jugadores" Value="7" />
                    <asp:ListItem Text="8 jugadores" Value="8" />
                    <asp:ListItem Text="9 jugadores" Value="9" />
                    <asp:ListItem Text="10 jugadores" Value="10" />
                </asp:DropDownList>
            </p>
            <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
            <asp:HiddenField ID="hfIdCapitan" runat="server" Value="0" />
            <p><strong>Capitán :</strong> <asp:Label ID="lblCapitanNombre" runat="server" Text="No seleccionado" /></p>
        </div>
        <div>
            <asp:Image ID="imgLogo" runat="server" Width="200px" />
        </div>
    </div>

    <asp:Button ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click" CssClass="btn btn-secondary" />

    <asp:Panel ID="pnlAcciones" runat="server" Visible="false" style="margin-top: 20px;">
        <asp:Button ID="btnCrear" runat="server" Text="CREAR" OnClick="btnCrear_Click" CssClass="btn btn-success" />
        <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" CssClass="btn btn-warning" />
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" CssClass="btn btn-danger" />
        <asp:Button ID="btnUnirse" runat="server" Text="Unirse" OnClick="btnUnirse_Click" CssClass="btn btn-info" />
    </asp:Panel>

    <asp:Panel ID="pnlSeleccionJugador" runat="server" Visible="false" style="margin-top: 20px; padding: 15px; border: 1px solid #ccc; border-radius: 5px;">
        <h3>Selecciona un jugador como capitán</h3>
        <p>
            <asp:DropDownList ID="ddlJugadores" runat="server" CssClass="form-control">
            </asp:DropDownList>
        </p>
        <p>
            <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar y Crear Equipo" OnClick="btnConfirmar_Click" CssClass="btn btn-primary" />
            <asp:Button ID="btnCancelarSeleccion" runat="server" Text="Cancelar" OnClick="btnCancelarSeleccion_Click" CssClass="btn btn-secondary" />
        </p>
    </asp:Panel>

    <!-- SECCIÓN DE MIEMBROS DEL EQUIPO -->
    <h3 style="margin-top: 30px;">Miembros del Equipo</h3>
    
    <div style="display:flex; justify-content:space-between; align-items:center; margin-bottom:10px;">
    <asp:Label ID="lblNumMiembros" runat="server" ForeColor="Gray" />
    <asp:Label ID="lblMaxJugadores" runat="server" ForeColor="Gray" />
    </div>
    
    <asp:Repeater ID="rptMiembros" runat="server" OnItemDataBound="rptMiembros_ItemDataBound">
        <ItemTemplate>
            <div id="divMiembro" runat="server" style="border:1px solid #ccc; padding:10px; margin-bottom:10px; border-radius:5px;">
                <div style="display:flex; justify-content:space-between; align-items:center;">
                    <div>
                        <strong><%# Eval("Apodo") %></strong>
                        <span id="spanCapitan" runat="server" style='background-color:#28a745; color:white; padding:2px 8px; border-radius:10px; margin-left:10px; font-size:12px; display:none;'>Capitán</span>
                        <br />
                        <small>Rol: <%# Eval("Rol_principal") %></small><br />
                        <small>Nivel: <%# Eval("Nivel") %> | KDA: <%# Eval("Kda_promedio") %></small>
                    </div>
                    <div>
                        <small>Winrate: <%# Eval("Winrate") %>%</small>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    
    <!-- Mensaje cuando no hay miembros -->
    <asp:Label ID="lblNoMiembros" runat="server" Text="No hay miembros en este equipo todavía." ForeColor="Gray" Visible="false" />

    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />

</asp:Content>