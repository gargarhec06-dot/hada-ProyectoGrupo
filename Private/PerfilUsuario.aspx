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
                        <div class="col-md-4 text-center">
                            <img src="https://via.placeholder.com/150" alt="Avatar" class="img-fluid rounded-circle mb-3" style="border: 3px solid #eee;">
                            <button class="btn btn-outline-secondary btn-sm">Cambiar Foto</button>
                        </div>

                        <div class="col-md-8">
                            <div class="mb-3">
                                <label class="fw-bold">Nombre de Usuario:</label>
                                <p class="text-muted">JuanPerez99</p>
                            </div>
                            <div class="mb-3">
                                <label class="fw-bold">Correo Electrónico:</label>
                                <p class="text-muted">juan.perez@ejemplo.com</p>
                            </div>
                            <div class="mb-3">
                                <label class="fw-bold">Torneos Ganados:</label>
                                <span class="badge bg-success">5 Victorias</span>
                            </div>
                            <div class="mb-3">
                                <label class="fw-bold">Biografía:</label>
                                <p class="small text-muted">Amante de los eSports y jugador competitivo de League of Legends desde 2020.</p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-footer text-end">
                    <asp:Button ID="btnEditarPerfil" runat="server" Text="Editar Perfil" 
                      OnClick="btnEditarPerfil_Click" CssClass="btn btn-primary" />
                     <asp:Button ID="btnMisJugadores" runat="server" Text="Mis Jugadores" 
                        OnClick="btnMisJugadores_Click" CssClass="btn btn-info" />
                    <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesión" 
                     OnClick="btnCerrarSesion_Click" CssClass="btn btn-danger" />
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
