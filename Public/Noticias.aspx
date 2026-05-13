<%@ Page Title="Noticias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Noticias.aspx.cs" Inherits="hada_ProyectoGrupo.Public.Noticias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .filtros-noticias {
            display: flex;
            align-items: center;
            gap: 12px;
            flex-wrap: wrap;
            background-color: #1a1a2e;
            padding: 15px;
            border-radius: 8px;
            margin-bottom: 20px;
        }

        .filtro-label {
            color: #00e5ff;
            font-size: 0.7rem;
            text-transform: uppercase;
            letter-spacing: 1px;
            display: block;
            margin-bottom: 3px;
        }

        .filtro-group {
            display: flex;
            flex-direction: column;
        }

        .filtro-input {
            background-color: #0f0f1a !important;
            border: 1px solid #00e5ff44 !important;
            color: #e0e0e0 !important;
            border-radius: 6px;
            padding: 5px 10px;
            font-size: 0.85rem;
            height: 34px;
        }

        .btn-filtrar {
            background-color: #00e5ff;
            color: #0f0f1a;
            border: none;
            padding: 6px 16px;
            border-radius: 6px;
            font-weight: 700;
            text-transform: uppercase;
            font-size: 0.8rem;
            cursor: pointer;
            height: 34px;
            align-self: flex-end;
        }

        .btn-limpiar-filtros {
            background-color: transparent;
            color: #00e5ff;
            border: 1px solid #00e5ff;
            padding: 6px 16px;
            border-radius: 6px;
            font-weight: 700;
            text-transform: uppercase;
            font-size: 0.8rem;
            cursor: pointer;
            height: 34px;
            align-self: flex-end;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding:10px; text-align:center;">
        <h2>Noticias</h2>
    </div>   

    <div class="filtros-noticias">
        <div class="filtro-group">
            <span class="filtro-label">Autor (Email)</span>
            <asp:TextBox ID="txtFiltroAutor" runat="server" CssClass="filtro-input" placeholder="ejemplo@mail.com" Width="180px" />
        </div>

        <div class="filtro-group">
            <span class="filtro-label">Desde</span>
            <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="filtro-input" placeholder="2026-01-01" Width="110px" />
        </div>

        <div class="filtro-group">
            <span class="filtro-label">Hasta</span>
            <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="filtro-input" placeholder="2026-12-31" Width="110px" />
        </div>

        <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn-filtrar" OnClick="btnFiltrar_Click" />
        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn-limpiar-filtros" OnClick="btnLimpiar_Click" />
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
                    
                    <p style="font-size: 0.8rem; color: #aaa; margin: 5px 0;">
                        Por: <%# Eval("EmailUsuario") %><br />
                        Fecha: <%# Eval("FechaPublicacion", "{0:yyyy-MM-dd}") %>
                    </p>

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