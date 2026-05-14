<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Equipos.aspx.cs" Inherits="hada_ProyectoGrupo.Public.Equipos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
    /* Estilos base actualizados para mayor tamaño y claridad */
    .eq-header { padding: 30px 10px 20px; text-align: center; }
    
    .eq-filter-bar { 
        display: flex; flex-wrap: wrap; gap: 10px; 
        align-items: center; padding: 0 10px 25px; 
    }

    .eq-search { 
        flex: 1; min-width: 200px; padding: 10px 15px; 
        border: 1px solid #ccc; border-radius: 25px; 
        font-size: 15px; outline: none; box-shadow: inset 0 1px 3px rgba(0,0,0,0.05);
    }

    .eq-filter-btn { 
        padding: 8px 20px; border-radius: 20px; 
        border: 1px solid #ccc; background: #fff; 
        color: #555; font-size: 14px; cursor: pointer; 
        transition: all .2s; 
    }

    .eq-filter-btn.active, .eq-filter-btn:hover { 
        background: #1D9E75; border-color: #1D9E75; color: #fff; 
        transform: translateY(-1px);
    }

    .eq-count { padding: 0 10px 15px; font-size: 14px; color: #666; font-weight: 600; }

    /* Grid ajustado para tarjetas más grandes */
    .eq-grid { 
        display: grid; 
        grid-template-columns: repeat(auto-fill, minmax(300px, 1fr)); 
        gap: 25px; padding: 0 10px; 
    }

    /* Tarjeta más grande y elegante */
    .eq-card { 
        border: 1px solid #eee; border-radius: 15px; 
        padding: 25px; text-align: center; position: relative; 
        background: #fff; transition: all .2s ease;
        box-shadow: 0 4px 15px rgba(0,0,0,0.08);
    }

    .eq-card:hover { 
        transform: translateY(-5px); 
        box-shadow: 0 8px 25px rgba(0,0,0,0.12); 
        border-color: #1D9E75;
    }

    .eq-card.lleno { opacity: .7; filter: grayscale(50%); }

    /* Badge más visible */
    .eq-badge { 
        position: absolute; top: 15px; right: 15px; 
        font-size: 12px; padding: 4px 12px; 
        border-radius: 20px; font-weight: 700; 
        text-transform: uppercase;
    }

    .badge-open { background: #E1F5EE; color: #0F6E56; }
    .badge-full { background: #FAECE7; color: #993C1D; }
    .badge-mine { background: #E6F1FB; color: #185FA5; }

    /* LOGO MUCHO MÁS GRANDE */
    .eq-logo { 
        width: 100px; height: 100px; /* Aumentado de 54px a 100px */
        border-radius: 50%; object-fit: cover; 
        display: block; margin: 10px auto 15px; 
        background: #f8f8f8; border: 3px solid #f0f0f0;
    }

    /* NOMBRE DEL EQUIPO MÁS CLARO Y GRANDE */
    .eq-nombre { 
        font-size: 22px; 
        font-weight: 800; 
        color: #222; 
        margin: 10px 0 5px;
        letter-spacing: -0.5px;
    }

    .eq-slots { font-size: 14px; color: #777; margin: 8px 0 12px; font-weight: 500; }

    /* Barra de progreso más gruesa */
    .eq-bar-wrap { height: 8px; background: #eee; border-radius: 10px; margin-bottom: 15px; overflow: hidden; }
    .eq-bar { height: 8px; border-radius: 10px; transition: width 0.5s ease; }
    .bar-open { background: #1D9E75; }
    .bar-full { background: #D85A30; }

    .eq-empty { text-align: center; color: #999; padding: 3rem; grid-column: 1 / -1; font-size: 16px; }

    /* Responsivo */
    @media (max-width: 700px) { .eq-grid { grid-template-columns: 1fr 1fr; } }
    @media (max-width: 480px) { .eq-grid { grid-template-columns: 1fr; } }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="eq-header"><h2>Equipos</h2></div>

    <div class="eq-filter-bar">
    <input type="text" id="eqSearch" class="eq-search" 
           placeholder="Buscar equipo por nombre..." oninput="filtrarEquipos()" />

    <button type="button" class="eq-filter-btn active" data-f="todos" 
            onclick="setFiltro(this,'todos')">Todos</button>

    <button type="button" class="eq-filter-btn" data-f="mio" 
            onclick="setFiltro(this,'mio')">Mis Equipos</button>

    <button type="button" class="eq-filter-btn" data-f="puedo-unirme" 
            onclick="setFiltro(this,'puedo-unirme')">Puedo Unirme</button>

    <button type="button" class="eq-filter-btn" data-f="lleno" 
            onclick="setFiltro(this,'lleno')">Llenos</button>
</div>

    <div class="eq-count" id="eqCount"></div>

    <div class="eq-grid" id="eqGrid">
        <asp:Repeater ID="rptEquipos" runat="server" OnItemDataBound="rptEquipos_ItemDataBound">
    <ItemTemplate>
        <div class="eq-card <%# Eval("EstadoFiltro").ToString() == "lleno" ? "lleno" : "" %>"
             data-nombre="<%# Eval("Nombre").ToString().ToLower() %>"
             data-estado="<%# Eval("EstadoFiltro") %>">

            <span class="eq-badge <%# Eval("BadgeClass") %>"><%# Eval("BadgeTexto") %></span>

            <%-- Imagen simplificada: ya viene resuelta desde el C# --%>
            <img class="eq-logo" 
                 src='<%# Eval("Logo_url") %>' 
                 alt='Logo <%# Eval("Nombre") %>' />

            <h2 class="eq-nombre"><%# Eval("Nombre") %></h2>
            
            <p class="eq-slots"><strong><%# Eval("MiembrosActuales") %> / <%# Eval("MaxJugadores") %></strong> jugadores</p>

            <div class="eq-bar-wrap">
                <div class="eq-bar" runat="server" id="barra"></div>
            </div>

            <div class="eq-actions" style="margin-top:10px;">
                <%-- Botón Modificar: Solo si EstadoFiltro == 'mio' --%>
                <asp:HyperLink runat="server" 
                    NavigateUrl='<%# "~/Public/DetallesEquipo.aspx?id=" + Eval("Id_equipo") %>'
                    Text="Modificar" CssClass="btn btn-sm btn-primary w-100 mb-1" 
                    Visible='<%# Eval("EstadoFiltro").ToString() == "mio" %>' />

                <%-- Botón Unirse: Solo si EstadoFiltro == 'abierto' --%>
                <asp:HyperLink runat="server" 
                    NavigateUrl='<%# "~/Public/DetallesEquipo.aspx?id=" + Eval("Id_equipo") %>'
                    Text="Unirse" CssClass="btn btn-sm btn-outline-success w-100" 
                    Visible='<%# Eval("EstadoFiltro").ToString() == "abierto" %>' />

                <%-- Botón Lleno: Solo si EstadoFiltro == 'lleno' --%>
                <button type="button" class="btn btn-sm btn-secondary w-100 disabled" 
                    runat="server" visible='<%# Eval("EstadoFiltro").ToString() == "lleno" %>'>
                    Equipo Lleno
                </button>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
    </div>

    <div id="eqEmpty" style="display:none;" class="eq-empty">No hay equipos que coincidan con ese filtro.</div>

    <asp:Panel ID="pnlJugador" runat="server" Visible="false" style="margin-top:24px; text-align:center;">
        <asp:Button ID="btnCrear" runat="server" Text="CREAR NUEVO EQUIPO" OnClick="btnCrear_Click" CssClass="btn btn-success" />
    </asp:Panel>

    <script>
        var filtroActual = 'todos';

        function setFiltro(btn, f) {
            document.querySelectorAll('.eq-filter-btn').forEach(function (b) { b.classList.remove('active'); });
            btn.classList.add('active');
            filtroActual = f;
            filtrarEquipos();
        }

        function filtrarEquipos() {
            var q = (document.getElementById('eqSearch').value || '').toLowerCase();
            var cards = document.querySelectorAll('#eqGrid .eq-card');
            var visible = 0;

            cards.forEach(function (card) {
                var nombre = card.dataset.nombre || '';
                var estado = card.dataset.estado || ''; // "mio", "abierto", "lleno"

                var matchNombre = nombre.indexOf(q) !== -1;
                var matchFiltro = false;

                if (filtroActual === 'todos') {
                    matchFiltro = true;
                }
                else if (filtroActual === 'mio') {
                    // Solo muestra equipos donde soy capitán
                    matchFiltro = (estado === 'mio');
                }
                else if (filtroActual === 'puedo-unirme') {
                    // IMPORTANTE: Solo equipos abiertos donde NO soy capitán
                    matchFiltro = (estado === 'abierto');
                }
                else if (filtroActual === 'lleno') {
                    matchFiltro = (estado === 'lleno');
                }

                if (matchNombre && matchFiltro) {
                    card.style.display = '';
                    visible++;
                } else {
                    card.style.display = 'none';
                }
            });

            // Actualizar el contador de equipos visibles
            var countEl = document.getElementById('eqCount');
            if (countEl) countEl.textContent = visible + ' equipo' + (visible !== 1 ? 's' : '');
        }

        window.addEventListener('load', function () { filtrarEquipos(); });
    </script>
</asp:Content>