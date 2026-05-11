<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Equipos.aspx.cs" Inherits="hada_ProyectoGrupo.Public.Equipos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
    .eq-header { padding: 20px 10px 10px; text-align: center; }
    .eq-filter-bar {
        display: flex; flex-wrap: wrap; gap: 8px;
        align-items: center; padding: 0 10px 16px;
    }
    .eq-search {
        flex: 1; min-width: 160px; padding: 7px 12px;
        border: 1px solid #ccc; border-radius: 20px;
        font-size: 13px; outline: none;
    }
    .eq-filter-btn {
        padding: 6px 16px; border-radius: 20px;
        border: 1px solid #ccc; background: #fff;
        color: #555; font-size: 13px; cursor: pointer;
        transition: all .15s;
    }
    .eq-filter-btn.active,
    .eq-filter-btn:hover { background: #1D9E75; border-color: #1D9E75; color: #fff; }
    .eq-count { padding: 0 10px 10px; font-size: 13px; color: #777; }
    .eq-grid {
        display: grid;
        grid-template-columns: repeat(3, 1fr);
        gap: 16px; padding: 0 10px;
    }
    .eq-card {
        border: 1px solid #ddd; border-radius: 10px;
        padding: 14px; text-align: center;
        position: relative; background: #fff;
        transition: box-shadow .15s;
    }
    .eq-card:hover { box-shadow: 0 2px 8px rgba(0,0,0,.08); }
    .eq-card.lleno { opacity: .6; }
    .eq-badge {
        position: absolute; top: 10px; right: 10px;
        font-size: 11px; padding: 3px 8px; border-radius: 20px;
        font-weight: 600;
    }
    .badge-open  { background: #E1F5EE; color: #0F6E56; }
    .badge-full  { background: #FAECE7; color: #993C1D; }
    .badge-mine  { background: #E6F1FB; color: #185FA5; }
    .eq-logo {
        width: 54px; height: 54px; border-radius: 50%;
        object-fit: cover; display: block; margin: 0 auto 8px;
        background: #eee;
    }
    .eq-slots { font-size: 12px; color: #888; margin: 4px 0 8px; }
    .eq-bar-wrap { height: 4px; background: #eee; border-radius: 4px; margin-bottom: 10px; }
    .eq-bar {
    height: 4px;
    border-radius: 4px;
}
    .eq-bar.lleno-bar { background: #D85A30; }
    .eq-empty {
        text-align: center; color: #999; padding: 2rem;
        grid-column: 1 / -1; font-size: 14px;
    }
    @media (max-width: 700px) { .eq-grid { grid-template-columns: 1fr 1fr; } }
    @media (max-width: 480px) { .eq-grid { grid-template-columns: 1fr; } }
    .bar-open {
    background: #1D9E75;
}

.bar-full {
    background: #D85A30;
}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="eq-header"><h2>Equipos</h2></div>

    <%-- ===== BARRA DE FILTROS ===== --%>
    <div class="eq-filter-bar">
        <input type="text" id="eqSearch" class="eq-search"
               placeholder="Buscar por nombre..."
               oninput="filtrarEquipos()" />

        <button class="eq-filter-btn active" data-f="todos"
                onclick="setFiltro(this,'todos')">Todos</button>

        <%-- Solo visible si el usuario tiene un equipo --%>
        <asp:Panel ID="pnlBtnMiEquipo" runat="server" Visible="false"
                   style="display:inline-block;">
            <button class="eq-filter-btn" data-f="mio"
                    onclick="setFiltro(this,'mio')">Mi equipo</button>
        </asp:Panel>

        <%-- Solo visible para jugadores autenticados --%>
        <asp:Panel ID="pnlBtnUnirse" runat="server" Visible="false"
                   style="display:inline-block;">
            <button class="eq-filter-btn" data-f="unirse"
                    onclick="setFiltro(this,'unirse')">Puedo unirme</button>
        </asp:Panel>

        <button class="eq-filter-btn" data-f="lleno"
                onclick="setFiltro(this,'lleno')">Llenos</button>
    </div>

    <div class="eq-count" id="eqCount"></div>

    <%-- ===== GRID DE TARJETAS ===== --%>
    <div class="eq-grid" id="eqGrid">
        <asp:Repeater ID="rptEquipos" runat="server"
    OnItemDataBound="rptEquipos_ItemDataBound">
            <ItemTemplate>
                <div class="eq-card <%# (int)Eval("MiembrosActuales") >= (int)Eval("MaxJugadores") ? "lleno" : "" %>"
                     data-nombre="<%# Eval("Nombre").ToString().ToLower() %>"
                     data-estado="<%# Eval("EstadoFiltro") %>">

                    <%-- Badge de estado --%>
                    <span class="eq-badge <%# Eval("BadgeClass") %>"><%# Eval("BadgeTexto") %></span>

                    <%-- Logo del equipo --%>
                    <img class="eq-logo"
                         src='<%# String.IsNullOrEmpty(Eval("Logo_url")?.ToString())
                                    ? "~/Images/default-team.png"
                                    : Eval("Logo_url") %>'
                         alt='Logo <%# Eval("Nombre") %>' />

                    <h3 style="font-size:15px; margin:0 0 4px;"><%# Eval("Nombre") %></h3>

                    <p class="eq-slots">
                        <%# Eval("MiembrosActuales") %> / <%# Eval("MaxJugadores") %> jugadores
                    </p>

                    <%-- Barra de ocupación --%>
             <div class="eq-bar-wrap">
    <div class='eq-bar <%# (int)Eval("MiembrosActuales") >= (int)Eval("MaxJugadores") ? "bar-full" : "bar-open" %>'
         runat="server"
         id="barra">
    </div>
</div>

                    <asp:HyperLink runat="server"
                        NavigateUrl='<%# "~/Public/DetallesEquipo.aspx?id=" + Eval("Id_equipo") %>'
                        Text='<%# Eval("AccionTexto") %>'
                        CssClass="btn btn-sm btn-outline-secondary w-100" />
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <div id="eqEmpty" style="display:none;" class="eq-empty">
        No hay equipos que coincidan con ese filtro.
    </div>

    <%-- Botón crear equipo (solo jugadores) --%>
    <asp:Panel ID="pnlJugador" runat="server" Visible="false"
               style="margin-top:24px; text-align:center;">
        <asp:Button ID="btnCrear" runat="server" Text="CREAR NUEVO EQUIPO"
                    OnClick="btnCrear_Click" CssClass="btn btn-success" />
    </asp:Panel>

    <script>
    var filtroActual = 'todos';

    function setFiltro(btn, f) {
        document.querySelectorAll('.eq-filter-btn').forEach(function(b) {
            b.classList.remove('active');
        });
        btn.classList.add('active');
        filtroActual = f;
        filtrarEquipos();
    }

    function filtrarEquipos() {
        var q = (document.getElementById('eqSearch').value || '').toLowerCase();
        var cards = document.querySelectorAll('#eqGrid .eq-card');
        var visible = 0;

        cards.forEach(function(card) {
            var nombre = card.dataset.nombre || '';
            var estado = card.dataset.estado || '';
            var matchNombre = nombre.indexOf(q) !== -1;
            var matchFiltro = filtroActual === 'todos' || estado === filtroActual;
            if (matchNombre && matchFiltro) {
                card.style.display = '';
                visible++;
            } else {
                card.style.display = 'none';
            }
        });

        document.getElementById('eqCount').textContent =
            visible + ' equipo' + (visible !== 1 ? 's' : '');
        document.getElementById('eqEmpty').style.display = visible === 0 ? 'block' : 'none';
    }

    // Inicializar contador al cargar
    window.addEventListener('load', function() { filtrarEquipos(); });
    </script>
</asp:Content>
