<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetallesEquipo.aspx.cs" Inherits="hada_ProyectoGrupo.Public.DetallesEquipo" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="max-width:800px; margin:0 auto; padding:20px;">
    <h2>Detalle del Equipo</h2>

    <div style="display:flex; align-items:flex-start; gap:40px;">
        <div>
            <p><strong>Nombre :</strong> <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" /></p>
            <p><strong>Fecha de Creación :</strong> <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" ReadOnly="true" /></p>
            <p><strong>Descripción :</strong> <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" CssClass="form-control" /></p>
            <p><strong>URL Logo :</strong> <asp:TextBox ID="txtLogo" runat="server" CssClass="form-control" /></p>
            
            <asp:Panel ID="pnlSubidaImagen" runat="server">
                <p><strong>O subir imagen (JPG/PNG, máx 2MB):</strong></p>
                <p>
                    <asp:FileUpload ID="fuLogo" runat="server" accept="image/jpeg,image/png,image/jpg" />
                    <asp:Button ID="btnSubirLogo" runat="server" Text="Subir" OnClick="btnSubirLogo_Click" CssClass="btn btn-secondary btn-sm" />
                    <asp:Label ID="lblSubidaLogo" runat="server" ForeColor="Red" />
                </p>
            </asp:Panel>

            <p><strong>Máximo de jugadores :</strong> 
                <asp:DropDownList ID="ddlMaxJugadores" runat="server" CssClass="form-control">
                    <asp:ListItem Text="3 jugadores" Value="3" />
                    <asp:ListItem Text="5 jugadores" Value="5" Selected="True" />
                    <asp:ListItem Text="10 jugadores" Value="10" />
                </asp:DropDownList>
            </p>
            <asp:HiddenField ID="hfIdCapitan" runat="server" Value="0" />
            <p><strong>Capitán :</strong> <asp:Label ID="lblCapitanNombre" runat="server" Font-Bold="true" /></p>
        </div>
        <div>
            <asp:Image ID="imgLogo" runat="server" Width="200px" Style="border-radius:10px; border: 1px solid #ddd;" />
        </div>
    </div>

    <div style="margin-top:20px;">
        <asp:Button ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click" CssClass="btn btn-secondary" />
        
        <asp:Panel ID="pnlAcciones" runat="server" style="display:inline-block;">
            <asp:Button ID="btnCrear" runat="server" Text="CREAR" OnClick="btnCrear_Click" CssClass="btn btn-success" />
            <asp:Button ID="btnModificar" runat="server" Text="Guardar Cambios" OnClick="btnModificar_Click" CssClass="btn btn-warning" />
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar Equipo" OnClick="btnEliminar_Click" CssClass="btn btn-danger" OnClientClick="return confirm('¿Estás seguro de eliminar este equipo?');" />
            <asp:Button ID="btnUnirse" runat="server" Text="Unirse al Equipo" OnClick="btnUnirse_Click" CssClass="btn btn-info" />
        </asp:Panel>
    </div>

    <asp:Panel ID="pnlSeleccionJugador" runat="server" Visible="false" style="margin-top: 20px; padding: 15px; border: 1px solid #17a2b8; border-radius: 5px; background-color: #f8f9fa;">
        <h3>Selecciona tu jugador</h3>
        <asp:DropDownList ID="ddlJugadores" runat="server" CssClass="form-control" style="max-width:300px; margin-bottom:10px;"></asp:DropDownList>
        <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" OnClick="btnConfirmar_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnCancelarSeleccion" runat="server" Text="Cancelar" OnClick="btnCancelarSeleccion_Click" CssClass="btn btn-secondary" />
    </asp:Panel>

    <h3 style="margin-top: 30px;">Miembros del Equipo</h3>
    <asp:Label ID="lblNumMiembros" runat="server" CssClass="text-muted" />

    <asp:Repeater ID="rptMiembros" runat="server" OnItemDataBound="rptMiembros_ItemDataBound">
        <ItemTemplate>
            <div id="divMiembro" runat="server" style="padding:15px; margin-bottom:10px; border-radius:8px; border:1px solid #ddd;">
                <div style="display:flex; justify-content:space-between; align-items:center;">
                    <div>
                        <strong style="font-size:1.1em;"><%# Eval("Apodo") %></strong>
                        <span id="spanCapitan" runat="server" style='background-color:#ffd700; color:#000; padding:2px 10px; border-radius:15px; margin-left:10px; font-size:11px; font-weight:bold; display:none;'>CAPITÁN</span>
                        <br />
                        <small>Rol: <%# Eval("Rol_principal") %> | Nivel: <%# Eval("Nivel") %></small>
                    </div>
                    <div style="text-align:right;">
                        <span class="badge badge-light">WR: <%# Eval("Winrate") %>%</span>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    
    <asp:Label ID="lblNoMiembros" runat="server" Text="No hay miembros aún." Visible="false" />
    <br />
    <asp:Label ID="lblMensaje" runat="server" Font-Bold="true" />
        </div>
</asp:Content>