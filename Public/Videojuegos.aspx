<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Videojuegos.aspx.cs" Inherits="hada_ProyectoGrupo.Public.Videojuegos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex flex-column justify-contents-center align-items-center text-center">
        <h2>Videojuegos</h2>

        <div style="margin-bottom: 20px;">
            <div style="margin-bottom: 10px;">
                <strong>Buscar por nombre:</strong>
                <asp:TextBox ID="txtNombre" runat="server" Width="300px" placeholder="Ej: Counter Strike" />
            </div>
            <div style="margin-bottom: 10px;">
                <strong>Filtrar por tipo:</strong>
                <asp:DropDownList ID="ddlTipo" runat="server" Width="300px" />
            </div>
            <div style="margin-bottom: 10px;">
                <asp:TextBox ID="txtEDMin" runat="server" Width="50px" placeholder="0" TextMode="Number" />
                <strong>< Edad mínima <</strong>
                <asp:TextBox ID="txtEDMax" runat="server" Width="50px" placeholder="100" TextMode="Number" />
            </div>
            <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary" OnClick="btnFiltrar_Click" />
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar filtros" CssClass="btn btn-secondary" OnClick="btnLimpiar_Click" />
        </div>

        <table class="table w-75">
            <thead>
                <tr>
                    <th scope="col"><asp:Label Text="Nombre" runat="server"/></th>
                    <th scope="col"><asp:Label Text="Tipo" runat="server"/></th>
                    <th scope="col"><asp:Label Text="Edad mínima" runat="server"/></th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="tableGenerator" runat="server">
                    <ItemTemplate>
                    <tr>
                        <td><a href="Videojuego.aspx?codigo=<%# Eval("Codigo")%>"><%# Eval("Nombre") %></a></td>
                        <td><span><%# Eval("Tipo") %></span></td>
                        <td><span><%# Eval("EdadMinima") %></span></td>
                    </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <div>
            <asp:Button id="CreateVideojuego" visible="false" class="btn btn-success" Text="+ Crear videojuego" runat="server" OnClick="CreateVideojuego_Click"/>
        </div>
    </div>
</asp:Content>