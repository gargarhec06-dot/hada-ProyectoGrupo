<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AnadirSaldo.aspx.cs" Inherits="hada_ProyectoGrupo.Private.AnadirSaldo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-center align-items-center" style="min-height: 60vh;">
        <div class="card p-4 shadow" style="width: 400px;">
            <h4 class="mb-4 text-center">Agregar fondos</h4>
            
            <div class="input-group mb-3">
                <span class="input-group-text">Cantidad (€)</span>
                <asp:TextBox ID="txtFondos" runat="server" CssClass="form-control" />
            </div>
            <asp:RegularExpressionValidator ID="revFondos" runat="server"
            ControlToValidate="txtFondos" 
            ValidationExpression="^[0-9]+([.,][0-9]{1,2})?$"
            ErrorMessage="Tienes que indicar una cantidad en cifras" 
            ForeColor="Red" CssClass="mb-2 d-block" />
            <div class="d-flex gap-2 mt-3">
                <asp:Button ID="btnSumar" runat="server" Text="Añadir" 
                    OnClick="btnSumar_Click" CssClass="btn btn-success w-50" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                    OnClick="btnCancelar_Click" CssClass="btn btn-outline-secondary w-50" CausesValidation="false" />
            </div>
            <div class="mt-3 text-center">
                <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />
            </div>
        </div>
    </div>
</asp:Content>
