<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Videojuegos.aspx.cs" Inherits="hada_ProyectoGrupo.Public.Videojuegos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex flex-column justify-contents-center align-items-center text-center">
        <h2>Videojuegos</h2>
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