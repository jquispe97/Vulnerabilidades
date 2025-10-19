<%@ Page Title="" Language="C#" MasterPageFile="~/master/sanna.Master" AutoEventWireup="true"
    CodeBehind="usuariosApp.aspx.cs" Inherits="CSF.CITASWEB.WS.BackEnd.metricas.usuariosApp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="placeCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeTitulo" runat="server">
    Reporte de Usuarios en el App
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="placeCuerpo" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/js/usuariosApp.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="upGeneral" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div id="parteConsulta">
                <div id="segmentoFiltro" style="border: none;">
                    <div class="lineaReporte">
                        <div class="divInput">
                            <asp:TextBox ID="txtFechaInicio" TextMode="Date" runat="server" CssClass="input obligatorio"
                                placeholder="" onfocus="(this.type='date')" onfocusout="(this.value == '' ? this.type='text' : this.type='date')"></asp:TextBox>
                            <label>
                                Fecha inicio</label>
                            <span class="focus-border"></span>
                        </div>
                        <div class="divInput">
                            <asp:TextBox ID="txtFechaFin" TextMode="Date" runat="server" CssClass="input obligatorio"
                                placeholder="" onfocus="(this.type='date')" onfocusout="(this.value == '' ? this.type='text' : this.type='date')"></asp:TextBox>
                            <label>
                                Fecha fin</label>
                            <span class="focus-border"></span>
                        </div>
                    </div>
                    <div class="lineaBotones" style="text-align: center; float: none; width: 100%;">
                        <asp:Button ID="btnDescargar" runat="server" Text="Descargar" OnClientClick="return validarFormulario('obligatorio');"
                            OnClick="btnDescargar_Click" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
