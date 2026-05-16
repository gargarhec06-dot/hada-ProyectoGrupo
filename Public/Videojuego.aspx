<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Videojuego.aspx.cs" Inherits="hada_ProyectoGrupo.Public.Videojuego" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="d-flex flex-column align-items-center justify-content-center">
        <div class="card shadow gap-2 w-75 align-items-center justify-content-center mx-auto">
            <div class="card-header w-100">
                <h2>Vista detallada del videojuego</h2>
            </div>
            <div class="card-body w-100 text-center">
                <div>
                    <strong>Icono:</strong>
                    <br />
                    <asp:Image ID="imgIcon" runat="server" Height="100px" />
                    <br />
                    <asp:Label Text="" ID="lblRouteIcon" runat="server" Visible="false" />
                </div>
                <div class="d-flex flex-column">
                    <div>
                        <asp:Label Text="<b>Nombre:</b> " runat="server" />
                        <asp:Label Text="" ID="NombreLabel" runat="server" />
                        <asp:TextBox ID="NombreAdminBox" Visible="false" runat="server" />
                    </div>
                    <div>
                        <asp:Label Text="<b>Código:</b> " runat="server" />
                        <asp:Label Text="" ID="CodigoLabel" runat="server" />
                        <asp:TextBox TextMode="Number" Visible="false" ID="CodigoAdminBox" runat="server" />
                    </div>
                    <div>
                        <asp:Label Text="<b>Tipo:</b> " runat="server" />
                        <asp:Label Text="" ID="TipoLabel" runat="server" />
                        <asp:DropDownList ID="TipoAdminBox" Visible="false" runat="server" />
                    </div>
                    <div class="d-flex flex-column">
                        <asp:Label Text="<b>Descripción:</b> " runat="server" />
                        <asp:Label Text="" ID="DescripcionLabel" runat="server" />
                        <asp:TextBox TextMode="MultiLine" Visible="false" ID="DescripcionAdminBox" runat="server" />
                    </div>
                    <div>
                        <asp:Label Text="<b>Edad mínima:</b> " runat="server" />
                        <asp:Label Text="" ID="EdadMinimaLabel" runat="server" />
                        <asp:TextBox TextMode="Number" Visible="false" ID="EdadMinimaAdminBox" runat="server" />
                    </div>
                    <div>
                        <asp:Label ID="lblIconUploadStatic" Text="<b>Icono:</b> " runat="server" Visible="false" />
                        <asp:FileUpload ID="IconUpload" runat="server" accept="image/jpeg,image/png,image/jpg" Visible="false" />
                        <asp:Button ID="btnIconUpload" runat="server" Text="Subir" CssClass="btn btn-secondary btn-sm" OnClick="btnIconUpload_Click" Visible="false" />
                        <asp:Label ID="lblIconUpload" runat="server" ForeColor="Red" Visible="false" />
                    </div>
                    <div>
                        <asp:Label ID="lblCaratulaUploadStatic" Text="<b>Caratula:</b> " runat="server" Visible="false" />
                        <asp:FileUpload ID="CaratulaUpload" runat="server" accept="image/jpeg,image/png,image/jpg" Visible="false" />
                        <asp:Button ID="btnCaratulaUpload" runat="server" Text="Subir" CssClass="btn btn-secondary btn-sm" OnClick="btnCaratulaUpload_Click" Visible="false" />
                        <asp:Label ID="lblCaratulaUpload" runat="server" ForeColor="Red" Visible="false" />
                    </div>
                    <div>
                        <asp:Label Text="" ID="DebugLabel" runat="server" />
                    </div>
                    <div>
                        <asp:Button Text="Actualizar entrada" ID="AdminUpdate" runat="server" Visible="false" OnClick="AdminUpdate_Click" CssClass="btn btn-info" />
                        <asp:Button Text="Añadir entrada" ID="AdminAdd" runat="server" Visible="false" CssClass="btn btn-info" OnClick="AdminAdd_Click" />
                        <asp:Button Text="Borrar entrada" ID="AdminDelete" runat="server" Visible="false" OnClick="AdminDelete_Click" OnClientClick="return confirm('¿Estás seguro de eliminar este videojuego?')" CssClass="btn btn-danger" />
                    </div>
                </div>
                <div>
                    <strong>Carátula:</strong>
                    <br />
                    <asp:Image ID="imgCaratula" runat="server" Width="500px" />
                    <br />
                    <asp:Label Text="" ID="lblRouteCaratula" runat="server" Visible="false" />
                </div>
                <div>
                    <a href="Videojuegos.aspx" class="btn btn-secondary">Volver</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
