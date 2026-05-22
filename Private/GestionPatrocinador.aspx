<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeFile="GestionPatrocinador.aspx.cs" Inherits="hada_ProyectoGrupo.Private.GestionPatrocinador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .gestion-wrapper {
            max-width: 800px;
            margin: 40px auto;
            padding: 0 20px;
        }

        .page-title {
            font-size: 2rem;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 3px;
            padding-left: 15px;
            margin-bottom: 30px;
            background: linear-gradient(to right, #00e5ff, #7b2fff);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
            border-left: 4px solid #7b2fff;
            filter: drop-shadow(0 0 8px #7b2fff44);
        }

        .form-card {
            background: linear-gradient(135deg, #1a1a2e 60%, #1a0f2e 100%);
            border: 1px solid #7b2fff44;
            border-radius: 12px;
            padding: 30px 35px;
            margin-bottom: 30px;
            box-shadow: 0 0 30px #7b2fff11;
            position: relative;
            overflow: hidden;
        }

        .form-card::before {
            content: '';
            position: absolute;
            top: 0; left: 0;
            width: 60px; height: 60px;
            border-top: 2px solid #00e5ff;
            border-left: 2px solid #00e5ff;
            border-radius: 12px 0 0 0;
            box-shadow: -2px -2px 10px #00e5ff33;
        }

        .form-card::after {
            content: '';
            position: absolute;
            bottom: 0; right: 0;
            width: 60px; height: 60px;
            border-bottom: 2px solid #7b2fff;
            border-right: 2px solid #7b2fff;
            border-radius: 0 0 12px 0;
            box-shadow: 2px 2px 10px #7b2fff33;
        }

        .form-field {
            margin-bottom: 20px;
        }

        .form-field label {
            display: block;
            font-size: 0.7rem;
            text-transform: uppercase;
            letter-spacing: 2px;
            margin-bottom: 6px;
            background: linear-gradient(to right, #00e5ff, #7b2fff);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
        }

        .form-field input[type=text],
        .form-field input[type=email],
        .form-field input[type=date] {
            width: 100%;
            background-color: #0f0f1a;
            border: 1px solid #7b2fff44;
            color: #e0e0e0;
            border-radius: 6px;
            padding: 10px 14px;
            font-size: 0.95rem;
            transition: border-color 0.2s, box-shadow 0.2s;
        }

        .form-field input:focus {
            outline: none;
            border-color: #00e5ff;
            box-shadow: 0 0 10px #00e5ff33;
        }

        .field-error {
            font-size: 0.75rem;
            color: #ff4444;
            margin-top: 4px;
            display: block;
        }

        .section-title {
            font-size: 1rem;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 2px;
            margin-bottom: 15px;
            background: linear-gradient(to right, #00e5ff, #7b2fff);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
        }

        .torneos-table {
            width: 100%;
            border-collapse: collapse;
            background: linear-gradient(135deg, #1a1a2e 60%, #1a0f2e 100%);
            border: 1px solid #7b2fff44;
            border-radius: 10px;
            overflow: hidden;
            margin-bottom: 30px;
            box-shadow: 0 0 20px #7b2fff11;
        }

        .torneos-table th {
            background: linear-gradient(to right, #0f0f1a, #1a0f2e);
            font-size: 0.7rem;
            text-transform: uppercase;
            letter-spacing: 2px;
            padding: 14px 16px;
            text-align: left;
            border-bottom: 1px solid #7b2fff44;
            color: #00e5ff;
        }

        .torneos-table th:last-child { color: #7b2fff; }

        .torneos-table td {
            padding: 12px 16px;
            color: #e0e0e0;
            border-bottom: 1px solid #7b2fff11;
            font-size: 0.9rem;
        }

        .torneos-table tr:last-child td { border-bottom: none; }

        .torneos-table tr:hover td {
            background: linear-gradient(to right, #00e5ff08, #7b2fff08);
        }

        .torneos-table input[type=text] {
            background-color: #0f0f1a;
            border: 1px solid #7b2fff44;
            color: #e0e0e0;
            border-radius: 6px;
            padding: 6px 10px;
            width: 80px;
            text-align: center;
        }

        .torneos-table input[type=text]:focus {
            outline: none;
            border-color: #00e5ff;
            box-shadow: 0 0 8px #00e5ff33;
        }

        .torneos-table input[type=checkbox] {
            width: 16px;
            height: 16px;
            accent-color: #00e5ff;
            cursor: pointer;
        }

        .botones-row {
            display: flex;
            gap: 12px;
            align-items: center;
            flex-wrap: wrap;
            margin-top: 10px;
        }

        .btn-guardar {
            background: linear-gradient(to right, #00e5ff, #7b2fff);
            color: #ffffff;
            border: none;
            padding: 10px 25px;
            border-radius: 6px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 1px;
            font-size: 0.85rem;
            cursor: pointer;
            transition: all 0.2s;
            box-shadow: 0 0 15px #7b2fff33;
        }

        .btn-guardar:hover {
            box-shadow: 0 0 25px #7b2fff66;
            transform: translateY(-1px);
        }

        .btn-cancelar {
            background-color: transparent;
            color: #888;
            border: 1px solid #444;
            padding: 10px 25px;
            border-radius: 6px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 1px;
            font-size: 0.85rem;
            cursor: pointer;
            transition: all 0.2s;
        }

        .btn-cancelar:hover {
            border-color: #888;
            color: #e0e0e0;
        }

        .lbl-mensaje {
            display: block;
            color: #ff4444;
            font-size: 0.85rem;
            margin-top: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="gestion-wrapper">

        <h2 id="tituloPagina" runat="server" class="page-title">Nuevo Patrocinador</h2>

        <div class="form-card">
            <div class="form-field">
                <asp:Label runat="server" Text="Nombre" />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
                    ErrorMessage="El nombre es obligatorio" Display="Dynamic" CssClass="field-error" />
            </div>
            <div class="form-field">
                <asp:Label runat="server" Text="Teléfono" />
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTelefono"
                    ErrorMessage="El teléfono es obligatorio" Display="Dynamic" CssClass="field-error" />
            </div>
            <div class="form-field">
                <asp:Label runat="server" Text="Email" />
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="El email es obligatorio" Display="Dynamic" CssClass="field-error" />
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                    ValidationExpression="^[\w\.-]+@[\w\.-]+\.\w+$"
                    ErrorMessage="Email no válido" Display="Dynamic" CssClass="field-error" />
            </div>
            <div class="form-field">
                <asp:Label runat="server" Text="Página Web" />
                <asp:TextBox ID="txtWeb" runat="server" CssClass="form-control" />
            </div>
            <div class="form-field">
                <asp:Label runat="server" Text="Inicio Contrato" />
                <asp:TextBox ID="txtInicioContrato" runat="server" TextMode="Date" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtInicioContrato"
                    ErrorMessage="La fecha de inicio es obligatoria" Display="Dynamic" CssClass="field-error" />
            </div>
            <div class="form-field">
                <asp:Label runat="server" Text="Fin Contrato" />
                <asp:TextBox ID="txtFinContrato" runat="server" TextMode="Date" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFinContrato"
                    ErrorMessage="La fecha de fin es obligatoria" Display="Dynamic" CssClass="field-error" />
            </div>
        </div>

        <div class="section-title">Torneos a patrocinar</div>

        <asp:Repeater ID="rptTorneos" runat="server">
            <HeaderTemplate>
                <table class="torneos-table">
                    <tr>
                        <th>Seleccionar</th>
                        <th>Torneo</th>
                        <th>Cantidad (€)</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="text-align: center;">
                        <asp:CheckBox runat="server" ID="chkTorneo" />
                        <asp:HiddenField runat="server" ID="hfCodigoTorneo" Value='<%# Eval("Codigo") %>' />
                    </td>
                    <td><%# Eval("Nombre") %></td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCantidad" Width="80px" Text="0" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <div class="botones-row">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar"
                OnClick="btnGuardar_Click" CssClass="btn-guardar" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                OnClick="btnCancelar_Click" CssClass="btn-cancelar" CausesValidation="false" />
        </div>

        <asp:Label ID="lblMensaje" runat="server" CssClass="lbl-mensaje" />

    </div>
</asp:Content>