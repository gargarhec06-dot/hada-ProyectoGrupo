<%@ Page Title="Editar Perfil" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="EditarPerfil.aspx.cs" Inherits="hada_ProyectoGrupo.Private.EditarPerfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card shadow">
                    <div class="card-header bg-primary text-white text-center">
                        <h3>Editar Perfil</h3>
                    </div>
                    <div class="card-body">
                        <div class="mb-3">
                            <label class="fw-bold">Nombre:</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                                ControlToValidate="txtNombre" ErrorMessage="El nombre es obligatorio" 
                                ForeColor="Red" Display="Dynamic" />
                        </div>

                        <div class="mb-3">
                            <label class="fw-bold">Apellidos:</label>
                            <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" />
                        </div>

                        <div class="mb-3">
                            <label class="fw-bold">País:</label>
                            <asp:TextBox ID="txtPais" runat="server" CssClass="form-control" />
                        </div>

                        <div class="mb-3">
                            <label class="fw-bold">Nueva Contraseña (dejar vacío para no cambiar):</label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
                        </div>

                        <div class="mb-3">
                            <label class="fw-bold">Confirmar Contraseña:</label>
                            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" />
                            <asp:CompareValidator ID="cvPassword" runat="server" 
                                ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword"
                                ErrorMessage="Las contraseñas no coinciden" ForeColor="Red" Display="Dynamic" />
                        </div>

                        <div class="text-end mt-3">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" 
                                OnClick="btnGuardar_Click" CssClass="btn btn-primary" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                                OnClick="btnCancelar_Click" CssClass="btn btn-secondary" />
                        </div>

                        <div class="mt-3">
                            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>