<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Jugadores.aspx.cs" Inherits="hada_ProyectoGrupo.Public.Jugadores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
    .jug-filter-bar {
        display: flex; flex-wrap: wrap; gap: 8px;
        align-items: center; padding: 0 10px 16px;
    }
    .jug-search {
        flex: 1; min-width: 160px; padding: 7px 12px;
        border: 1px solid #ccc; border-radius: 20px;
        font-size: 13px; outline: none;
    }
    .jug-filter-btn {
        padding: 6px 16px; border-radius: 20px;
        border: 1px solid #ccc; background: #fff;
        color: #555; font-size: 13px; cursor: pointer;
        transition: all .15s;
    }
    .jug-filter-btn.active,
    .jug-filter-btn:hover { background: #9370DB; border-color: #9370DB; color: #fff; }
    .jug-count { padding: 0 10px 10px; font-size: 13px; color: #777; }
    .jug-grid {
        display: grid;
        grid-template-columns: repeat(3, 1fr);
        gap: 20px; padding: 0 10px;
    }
    .jug-card {
        border: 1px solid #ddd; border-radius: 10px;
        overflow: hidden; text-align: center; background: #fff;
        box-shadow: 0 1px 4px rgba(0,0,0,.07);
        transition: box-shadow .15s;
    }
    .jug-card:hover { box-shadow: 0 2px 8px rgba(0,0,0,.1); }
    .jug-header {
        background-color: #9370DB; color: white;
        padding: 16px 10px 10px; position: relative;
    }
    .jug-team-logo {
        position: absolute; top: 8px; right: 10px;
        width: 44px; height: 44px; border-radius: 50%;
        object-fit: cover; border: 2px solid rgba(255,255,255,.7);
        background: #eee;
    }
    .jug-apodo  { font-size: 20px; font-weight: 700; margin: 0; padding-right: 50px; }
    .jug-body   { padding: 10px 12px 14px; }
    .jug-rol    { font-size: 15px; color: #555; margin: 6px 0 4px; }
    .jug-equipo { font-size: 13px; color: #888; margin: 0 0 10px; }
    .jug-empty  {
        text-align: center; color: #999; padding: 2rem;
        grid-column: 1 / -1; font-size: 14px;
    }
    @media (max-width: 700px) { .jug-grid { grid-template-columns: 1fr 1fr; } }
    @media (max-width: 480px) { .jug-grid { grid-template-columns: 1fr; } }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="padding:10px; text-align:center;"><h2>Jugadores</h2></div>

    <div class="jug-filter-bar">
        <input type="text" id="jugSearch" class="jug-search"
               placeholder="Buscar por apodo..."
               oninput="filtrarJugadores()" />
        <button type="button" class="jug-filter-btn active" data-f="todos"
                onclick="setFiltro(this,'todos')">Todos</button>

        <button type="button" class="jug-filter-btn" data-f="con-equipo"
                onclick="setFiltro(this,'con-equipo')">Con equipo</button>

        <button type="button" class="jug-filter-btn" data-f="sin-equipo"
                onclick="setFiltro(this,'sin-equipo')">Sin equipo</button>

        <asp:Panel ID="pnlBtnMisJugadores" runat="server" Visible="false"
                   style="display:inline-block;">
            <button type="button" class="jug-filter-btn" data-f="mio"
                    onclick="setFiltro(this,'mio')">Mis jugadores</button>
        </asp:Panel>
    </div>

    <div class="jug-count" id="jugCount"></div>

    <div class="jug-grid" id="jugGrid">
        <asp:Repeater ID="rptJugadores" runat="server">
            <ItemTemplate>
                <div class="jug-card"
                     data-apodo="<%# Eval("Apodo").ToString().ToLower() %>"
                     data-estado="<%# Eval("EstadoFiltro") %>">

                    <div class="jug-header">
                       <asp:Image ID="imgLogoEquipo" runat="server" 
                        CssClass="jug-team-logo"
                        ImageUrl='<%# !string.IsNullOrEmpty(Eval("LogoEquipo") as string) ? ResolveUrl(Eval("LogoEquipo").ToString()) : "" %>'
                        Visible='<%# !string.IsNullOrEmpty(Eval("LogoEquipo") as string) %>'
                        AlternateText='<%# Eval("NombreEquipo") %>' 
                        toolTip='<%# Eval("NombreEquipo") %>' />
                        <h1 class="jug-apodo"><%# Eval("Apodo") %></h1>
                    </div>

                    <div class="jug-body">
                        <p class="jug-rol"><%# Eval("Rol_principal") %></p>
                        <p class="jug-equipo">
                            <strong>Equipo:</strong> <%# Eval("NombreEquipo") %>
                        </p>
                        <asp:HyperLink ID="hlDetalles" runat="server"
                            NavigateUrl='<%# "~/Public/DetallesJugador.aspx?codigo=" + Eval("Codigo") %>'
                            Text="Ver detalles"
                            CssClass="btn btn-info btn-sm"
                            Visible='<%# Session["Email"] != null
                                        && Session["Email"].ToString() == Eval("Email_usuario").ToString() %>' />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <div id="jugEmpty" style="display:none;" class="jug-empty">
        No hay jugadores que coincidan con ese filtro.
    </div>

    <asp:Panel ID="pnlAdmin3" runat="server" Visible="false"
               style="margin-top:20px; padding:0 10px;">
        <asp:Button ID="btnCrear" runat="server" Text="+ Crear Nuevo Jugador"
            OnClick="btnCrear_Click" CssClass="btn btn-success" />
    </asp:Panel>

    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"
               style="display:block; padding:10px;" />

    <script>
        var filtroActual = 'todos';

        function setFiltro(btn, f) {
            document.querySelectorAll('.jug-filter-btn').forEach(function (b) {
                b.classList.remove('active');
            });
            btn.classList.add('active');
            filtroActual = f;
            filtrarJugadores();
        }

        function filtrarJugadores() {
            var q = (document.getElementById('jugSearch').value || '').toLowerCase();
            var cards = document.querySelectorAll('#jugGrid .jug-card');
            var visible = 0;

            cards.forEach(function (card) {
                var apodo = card.dataset.apodo || '';
                var estado = card.dataset.estado || '';
                var matchApodo = apodo.indexOf(q) !== -1;
                var matchFiltro;
                if (filtroActual === 'todos') {
                    matchFiltro = true;
                } else if (filtroActual === 'con-equipo') {
                    matchFiltro = estado === 'con-equipo' || estado === 'mio';
                } else {
                    matchFiltro = estado === filtroActual;
                }
                if (matchApodo && matchFiltro) {
                    card.style.display = '';
                    visible++;
                } else {
                    card.style.display = 'none';
                }
            });

            document.getElementById('jugCount').textContent =
                visible + ' jugador' + (visible !== 1 ? 'es' : '');
            document.getElementById('jugEmpty').style.display =
                visible === 0 ? 'block' : 'none';
        }

        window.addEventListener('load', function () { filtrarJugadores(); });
    </script>
</asp:Content>
