<%@ Page Title="Añadir Saldo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="AnadirSaldo.aspx.cs" Inherits="hada_ProyectoGrupo.Private.AnadirSaldo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .saldo-wrapper {
            display: flex;
            align-items: center;
            justify-content: center;
            min-height: calc(100vh - 61px);
            padding: 40px 20px;
        }

        .saldo-card {
            background: linear-gradient(135deg, #1a1a2e 60%, #1a0f2e 100%);
            border: 1px solid #7b2fff44;
            border-radius: 12px;
            padding: 40px 50px;
            width: 100%;
            max-width: 420px;
            box-shadow: 0 0 40px #7b2fff11;
            position: relative;
            overflow: hidden;
        }

        .saldo-card::before {
            content: '';
            position: absolute;
            top: 0; left: 0;
            width: 60px; height: 60px;
            border-top: 2px solid #00e5ff;
            border-left: 2px solid #00e5ff;
            border-radius: 12px 0 0 0;
            box-shadow: -2px -2px 10px #00e5ff33;
        }

        .saldo-card::after {
            content: '';
            position: absolute;
            bottom: 0; right: 0;
            width: 60px; height: 60px;
            border-bottom: 2px solid #7b2fff;
            border-right: 2px solid #7b2fff;
            border-radius: 0 0 12px 0;
            box-shadow: 2px 2px 10px #7b2fff33;
        }

        .saldo-title {
            font-size: 1.5rem;
            font-weight: 800;
            text-transform: uppercase;
            letter-spacing: 3px;
            text-align: center;
            margin-bottom: 8px;
            background: linear-gradient(to right, #00e5ff, #7b2fff);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
        }

        .saldo-line {
            width: 40px;
            height: 2px;
            background: linear-gradient(to right, transparent, #00e5ff, transparent);
            box-shadow: 0 0 8px #00e5ff;
            margin: 0 auto 30px auto;
        }

        .saldo-field {
            margin-bottom: 20px;
        }

        .saldo-field label {
            display: block;
            font-size: 0.75rem;
            text-transform: uppercase;
            letter-spacing: 2px;
            margin-bottom: 6px;
            background: linear-gradient(to right, #00e5ff, #7b2fff);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
        }

        .saldo-field input {
            width: 100%;
            background-color: #0f0f1a;
            border: 1px solid #7b2fff44;
            color: #e0e0e0;
            border-radius: 6px;
            padding: 10px 14px;
            font-size: 0.95rem;
            transition: border-color 0.2s, box-shadow 0.2s;
        }

        .saldo-field input:focus {
            outline: none;
            border-color: #00e5ff;
            box-shadow: 0 0 10px #00e5ff33;
        }

        .field-error {
            color: #ff4444;
            font-size: 0.75rem;
            margin-top: 4px;
            display: block;
        }

        .botones-row {
            display: flex;
            gap: 12px;
            margin-top: 20px;
        }

        .btn-anadir {
            flex: 1;
            background: linear-gradient(to right, #00e5ff, #7b2fff);
            color: #ffffff;
            border: none;
            padding: 11px;
            border-radius: 6px;
            font-weight: 800;
            text-transform: uppercase;
            letter-spacing: 2px;
            font-size: 0.85rem;
            cursor: pointer;
            transition: all 0.2s;
            box-shadow: 0 0 15px #7b2fff33;
        }

        .btn-anadir:hover {
            box-shadow: 0 0 25px #7b2fff66;
            transform: translateY(-1px);
        }

        .btn-cancelar {
            flex: 1;
            background-color: transparent;
            color: #888;
            border: 1px solid #444;
            padding: 11px;
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
            text-align: center;
            color: #ff4444;
            font-size: 0.8rem;
            margin-top: 12px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="saldo-wrapper">
        <div class="saldo-card">
            <div class="saldo-title">Añadir Saldo</div>
            <div class="saldo-line"></div>

            <div class="saldo-field">
                <label>Cantidad (€)</label>
                <asp:TextBox ID="txtFondos" runat="server" placeholder="Ej: 50.00" />
                <asp:RegularExpressionValidator ID="revFondos" runat="server"
                    ControlToValidate="txtFondos"
                    ValidationExpression="^[0-9]+([.,][0-9]{1,2})?$"
                    ErrorMessage="Introduce una cantidad válida"
                    CssClass="field-error" Display="Dynamic" />
            </div>

            <div class="botones-row">
                <asp:Button ID="btnSumar" runat="server" Text="Añadir"
                    OnClick="btnSumar_Click" CssClass="btn-anadir" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                    OnClick="btnCancelar_Click" CssClass="btn-cancelar" CausesValidation="false" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="lbl-mensaje" />
        </div>
    </div>
</asp:Content>