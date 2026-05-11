<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PerfilUsuario.aspx.cs" Inherits="hada_ProyectoGrupo.Private.PerfilUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card shadow">
                    <div class="card-header bg-primary text-white text-center">
                        <h3>Perfil de Usuario</h3>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-8">
                                <div class="mb-3">
                                    <label class="fw-bold">Nombre de Usuario:</label>
                                    <asp:Label ID="lblNombre" runat="server" CssClass="text-muted" />
                                </div>
                                <div class="mb-3">
                                    <label class="fw-bold">Correo Electrónico:</label>
                                    <asp:Label ID="lblEmail" runat="server" CssClass="text-muted" />
                                </div>
                                <div class="mb-3">
                                    <label class="fw-bold">Rol:</label>
                                    <asp:Label ID="lblRol" runat="server" CssClass="text-muted" />
                                </div>
                                <div class="mb-3">
                                    <label class="fw-bold">Fecha de Registro:</label>
                                    <asp:Label ID="lblFechaRegistro" runat="server" CssClass="text-muted" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <div style="display: flex; justify-content: space-between; align-items: center;">
                            <!-- Botón izquierda -->
                            <div class="d-flex gap-2">
                                <asp:Panel ID="pnlMisJugadores" runat="server" Visible="false">
                                    <asp:Button ID="btnMisJugadores" runat="server" Text="Mis Jugadores" 
                                        OnClick="btnMisJugadores_Click" CssClass="btn btn-info" />
                                </asp:Panel>
                                <asp:Panel ID="pnlSumarFondos" runat="server">
                                    <asp:Button ID="btnSumarFondos" runat="server" Text="Añadir fondos" 
                                        OnClick="btnSumarFondos_Click" CssClass="btn btn-success"/>
                                </asp:Panel>
                            </div>
                            

                            <!-- Botones derecha -->
                            <div>
                                <asp:Button ID="btnEditarPerfil" runat="server" Text="Editar Perfil" 
                                    OnClick="btnEditarPerfil_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesión" 
                                    OnClick="btnCerrarSesion_Click" CssClass="btn btn-danger" />
                                <asp:Button ID="btnEliminarCuenta" runat="server" Text="Eliminar Cuenta" 
                                    OnClick="btnEliminarCuenta_Click" CssClass="btn btn-outline-danger" 
                                    OnClientClick="return confirm('¿Estás seguro de que quieres eliminar tu cuenta? Esta acción no se puede deshacer.');" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>