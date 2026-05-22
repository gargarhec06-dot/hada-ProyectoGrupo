<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Videojuegos.aspx.cs" Inherits="hada_ProyectoGrupo.Public.Videojuegos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex flex-column justify-contents-center align-items-center text-center">
        <br />
        <div class="eq-header">
            <h2>Videojuegos</h2>
        </div>
        <br />
        <div class="card shadow">
            <div class="card-header">
                Filtrado
            </div>
            <div class="card-body d-flex flex-column gap-2">
                <div>
                    <strong>Buscar por nombre:</strong>
                    <asp:TextBox ID="txtNombre" runat="server" Width="300px" placeholder="Ej: Counter Strike" />
                </div>
                <div>
                    <strong>Filtrar por tipo:</strong>
                    <asp:DropDownList ID="ddlTipo" runat="server" Width="300px" />
                </div>
                <div>
                    <asp:TextBox ID="txtEDMin" runat="server" Width="50px" placeholder="0" TextMode="Number" />
                    <strong>< Edad mínima <</strong>
                    <asp:TextBox ID="txtEDMax" runat="server" Width="50px" placeholder="100" TextMode="Number" />
                </div>
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary" OnClick="btnFiltrar_Click" />
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar filtros" CssClass="btn btn-secondary" OnClick="btnLimpiar_Click" />
            </div>
        </div>

        <br />

        <div class="card shadow w-75">
            <div class="card-header">
                Juegos
            </div>
            <table class="table card-body">
                <thead>
                    <tr>
                        <th scope="col">
                            <asp:Label Text="Icon" runat="server" /></th>
                        <th scope="col">
                            <asp:Label Text="Nombre" runat="server" /></th>
                        <th scope="col">
                            <asp:Label Text="Tipo" runat="server" /></th>
                        <th scope="col">
                            <asp:Label Text="Edad mínima" runat="server" /></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="tableGenerator" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="align-middle">
                                    <img src="<%# Eval("IconUrl")%>" height="50" width="50"></img>
                                </td>
                                <td class="align-middle"><a href="Videojuego.aspx?codigo=<%# Eval("Codigo")%>"><%# Eval("Nombre") %></a></td>
                                <td class="align-middle"><span><%# Eval("Tipo") %></span></td>
                                <td class="align-middle"><span><%# Eval("EdadMinima") %></span></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <div>
            <asp:Button ID="CreateVideojuego" Visible="false" class="btn btn-success" Text="+ Crear videojuego" runat="server" OnClick="CreateVideojuego_Click" />
        </div>
    </div>
</asp:Content>
