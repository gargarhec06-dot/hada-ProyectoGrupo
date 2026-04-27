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
                <tr>
                    <td><asp:Label Text="Hello Kitty Island Adventure" runat="server"/></td>
                    <td><asp:Label Text="Speedrun ANY%" runat="server"/></td>
                    <td><asp:Label Text="+3" runat="server"/></td>
                </tr>
                <tr>
                    <td><asp:Label Text="Doom 2016 RIP BOZO Edition" runat="server"/></td>
                    <td><asp:Label Text="Speedrun Weed%" runat="server"/></td>
                    <td><asp:Label Text="420" runat="server"/></td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>