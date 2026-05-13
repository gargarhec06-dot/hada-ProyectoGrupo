<%@ Page Title="Noticias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Noticias.aspx.cs" Inherits="hada_ProyectoGrupo.Public.Noticias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding:10px; text-align:center;">
        <h2>Noticias</h2>
    </div>   

    <div style="display:grid; grid-template-columns:repeat(3,1fr); gap:20px;">
        <asp:Repeater ID="rptNoticias" runat="server">
            <ItemTemplate>
                <div style="border:1px solid #ccc; padding:10px; text-align:center;">
                    <h3><%# Eval("Titulo") %></h3>
                    
                    
                    <img src='<%# Eval("ImagenUrl") %>' 
                         alt="Noticia" 
                         width="150" 
                         style="margin-bottom:10px; border-radius:4px; height:100px; object-fit:cover;" />
                    
                    <br />
                   
                    <asp:HyperLink runat="server" 
                        NavigateUrl='<%# "~/Public/DetallesNoticia.aspx?id=" + Eval("IdNoticia") %>'
                        Text="Ver noticia completa" />
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <asp:Panel ID="pnlAdmin" runat="server" Visible="false" style="margin-top: 20px;">
        <asp:Button ID="btnCrear" runat="server" Text="CREAR NUEVA NOTICIA" OnClick="btnCrear_Click" CssClass="btn btn-success" />
    </asp:Panel>
</asp:Content>