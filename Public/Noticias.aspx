<%@ Page Title="Noticias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Noticias.aspx.cs" Inherits="hada_ProyectoGrupo.Public.Noticias" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding:10px; text-align:center;"><h2>Noticias de eSports</h2></div>   

    <div style="display:grid; grid-template-columns:repeat(3,1fr); gap:20px; padding:20px;">
        <asp:Repeater ID="rptNoticias" runat="server">
            <ItemTemplate>
                <div style="border:1px solid #ddd; padding:15px; text-align:center; border-radius:10px; background-color:#fff; box-shadow: 0 2px 5px rgba(0,0,0,0.1);">
                    <h3 style="color:#333; height:40px;"><%# Eval("Titulo") %></h3>
                    
                    <%-- Seleccionamos la imagen según el ID de la noticia --%>
                    <img src='<%# 
                        Eval("IdNoticia").ToString() == "1" ? "https://images.unsplash.com/photo-1542751371-adc38448a05e?auto=format&fit=crop&w=400&q=80" : 
                        Eval("IdNoticia").ToString() == "2" ? "https://images.unsplash.com/photo-1550745165-9bc0b252726f?auto=format&fit=crop&w=400&q=80" : 
                        "https://images.unsplash.com/photo-1511512578047-dfb367046420?auto=format&fit=crop&w=400&q=80" 
                    %>' alt="Noticia" style="width:100%; height:160px; object-fit:cover; border-radius:5px; margin-bottom:15px;" />
                    
                    <asp:HyperLink runat="server" 
                        NavigateUrl='<%# "~/Public/DetallesNoticia.aspx?id=" + Eval("IdNoticia") %>'
                        Text="Leer más" style="background-color:#007bff; color:white; padding:8px 15px; text-decoration:none; border-radius:5px; display:inline-block;" />
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <asp:Panel ID="pnlAdmin" runat="server" Visible="false" style="text-align:center; margin-top:20px;">
        <asp:Button ID="btnCrear" runat="server" Text="Nueva Noticia" OnClick="btnCrear_Click" CssClass="btn btn-primary" />
    </asp:Panel>
</asp:Content>