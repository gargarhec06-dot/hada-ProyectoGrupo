<%@ Page Title="Torneos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Torneos.aspx.cs" Inherits="hada_ProyectoGrupo.Public.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .filtros-torneos {
            display: flex;
            align-items: center;
            gap: 12px;
            flex-wrap: wrap;
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

        .filtro-input:focus {
            outline: none;
            border-color: #00e5ff !important;
            box-shadow: 0 0 8px #00e5ff33 !important;
        }

        .btn-filtrar {
            background-color: #00e5ff;
            color: #0f0f1a;
            border: none;
            padding: 6px 16px;
            border-radius: 6px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 1px;
            font-size: 0.8rem;
            cursor: pointer;
            align-self: flex-end;
            height: 34px;
        }

        .btn-filtrar:hover { background-color: #00b8cc; }

        .btn-limpiar-filtros {
            background-color: transparent;
            color: #00e5ff;
            border: 1px solid #00e5ff;
            padding: 6px 16px;
            border-radius: 6px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 1px;
            font-size: 0.8rem;
            cursor: pointer;
            align-self: flex-end;
            height: 34px;
        }

        .btn-limpiar-filtros:hover { background-color: #00e5ff22; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="px-3 mt-4">
        <div class="d-flex align-items-center justify-content-between mb-2 flex-wrap gap-2">
            <div class="d-flex align-items-center gap-3">
                <h2 class="me-3 mb-0">TORNEOS DISPONIBLES</h2>
                <asp:Panel ID="pnlAdmin" runat="server" Visible="false">
                    <asp:Button ID="btnCrear" runat="server" Text="+ Crear Torneo" 
                        CssClass="btn btn-success" OnClick="btnCrear_Click" />
                </asp:Panel>
            </div>

            <!-- Filtros -->
            <div class="filtros-torneos">
                <div class="filtro-group">
                    <span class="filtro-label">Nivel</span>
                    <asp:DropDownList ID="ddlNivel" runat="server" CssClass="filtro-input">
                        <asp:ListItem Text="Todos" Value="" />
                        <asp:ListItem Text="Amateur" Value="false" />
                        <asp:ListItem Text="Profesional" Value="true" />
                    </asp:DropDownList>
                </div>

                <div class="filtro-group">
                    <span class="filtro-label">Precio mín (€)</span>
                    <asp:TextBox ID="txtPrecioMin" runat="server" CssClass="filtro-input" 
                        placeholder="0" Width="80px" />
                </div>

                <div class="filtro-group">
                    <span class="filtro-label">Precio máx (€)</span>
                    <asp:TextBox ID="txtPrecioMax" runat="server" CssClass="filtro-input" 
                        placeholder="999" Width="80px" />
                </div>

                <div class="filtro-group">
                    <span class="filtro-label">Ubicación</span>
                    <asp:TextBox ID="txtUbicacion" runat="server" CssClass="filtro-input" 
                        placeholder="Ej: Spain" Width="100px" />
                </div>

                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" 
                    CssClass="btn-filtrar" OnClick="btnFiltrar_Click" />
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" 
                    CssClass="btn-limpiar-filtros" OnClick="btnLimpiar_Click" />
            </div>
        </div>

        <hr class="mb-4" />
        <div class="row">
            <asp:Repeater ID="rptTorneos" runat="server">
                <ItemTemplate>
                    <div class="col-md-4 mb-4">
                        <div class="card h-100">
                            <div class="card-body d-flex justify-content-between align-items-start">
                                <div>
                                    <h5 class="card-title"><%# Eval("Nombre") %></h5>
                                    <p class="card-text small">
                                        Fecha: <%# Eval("Fecha", "{0:dd/MM/yyyy}") %><br />
                                        Nivel: <%# (bool)Eval("Profesional") ? "Profesional" : "Amateur" %><br />
                                        Inscripción: <%# Eval("PrecioInscripcion") %>€<br />
                                        Ubicacion: <%# Eval("Ubicacion") %>
                                    </p>
                                </div>
                                <asp:Image runat="server" 
                                    ImageUrl='<%# Eval("Url_logo") %>'
                                    Visible='<%# !string.IsNullOrEmpty(Eval("Url_logo").ToString()) %>'
                                    Width="80px" Height="80px" 
                                    style="object-fit: contain; margin-left: 10px;" />
                            </div>
                            <div class="card-footer bg-transparent border-top-0">
                                <asp:HyperLink runat="server" 
                                    NavigateUrl='<%# "~/Public/DetalleTorneo.aspx?codigo=" + Eval("Codigo") %>'
                                    Text="Ver detalles" CssClass="btn btn-outline-primary btn-sm w-100" />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>