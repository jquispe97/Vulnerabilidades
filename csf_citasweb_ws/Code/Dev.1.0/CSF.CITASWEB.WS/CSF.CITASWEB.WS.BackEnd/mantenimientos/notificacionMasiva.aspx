<%@ Page Title="" Language="C#" MasterPageFile="~/master/sanna.Master" AutoEventWireup="true"
    CodeBehind="notificacionMasiva.aspx.cs" Inherits="CSF.CITASWEB.WS.BackEnd.mantenimientos.notificacionMasiva" %>

<asp:Content ID="Content1" ContentPlaceHolderID="placeCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeTitulo" runat="server">
    Notificaciones masivas
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="placeCuerpo" runat="server">
    <asp:UpdatePanel ID="upGeneral" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div id="parteConsulta">
                <div id="segmentoFiltro">
                    <div class="lineaBotones">
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" />
                        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClientClick="$('#parteConsulta').fadeOut(500,function(){$('#parteFormulario').fadeIn();});return false;" />
                    </div>
                </div>
                <div id="segmentoConsultar">
                    <asp:GridView ID="gvConsultar" CssClass="grilla" runat="server" AutoGenerateColumns="false"
                        RowStyle-HorizontalAlign="Center">
                        <Columns>
                            <asp:BoundField DataField="IDAppNotificacionMasiva" HeaderText="ID" ItemStyle-Width="5%" />
                            <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de registro" ItemStyle-Width="10%" />
                            <asp:BoundField DataField="Titulo" HeaderText="Titulo" ItemStyle-Width="25%" />
                            <asp:BoundField DataField="Mensaje" HeaderText="Mensaje" ItemStyle-Width="35%" />
                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" ItemStyle-Width="10%" />
                            <asp:BoundField DataField="FueEnviado" HeaderText="Enviado?" ItemStyle-Width="15%" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div id="parteFormulario" style="display: none;">
                <div style="width: 90%; margin-left: 5%; border-bottom: 1px solid #CCC; font-size: 130%;
                    padding: 30px 0px 15px 0px;">
                    Nueva notificación</div>
                <div class="divInput">
                    <asp:TextBox ID="txtTitulo" runat="server" CssClass="input obligatorio" placeholder=""></asp:TextBox>
                    <label>
                        Titulo</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtMensaje" TextMode="MultiLine" Rows="6" runat="server" CssClass="input obligatorio"
                        placeholder=""></asp:TextBox>
                    <label>
                        Mensaje</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtFiltro" TextMode="MultiLine" Rows="8" runat="server" CssClass="input"
                        placeholder=""></asp:TextBox>
                    <label>
                        Filtro</label>
                    <span class="focus-border"></span>
                </div>
                <div style="width: 80%; padding-left: 10%; text-align: right; font-size: 60%;">
                    Llene este campo en caso desee enviar la notificación a ciertos usuarios. <span style="text-decoration: underline;
                        cursor: pointer;" onclick="alert('Cada filtro va en una línea diferente. El formato es:\n            TipoDocumento|NumeroDocumentos\n\nLos tipos de documento son: 1 = DNI, 2 = Carnet de Extranjería, 3 = Pasaporte\n\nEjemplo:\n            1|44556677\n            1|55667788\n            1|66778899');">
                        Más info</span>
                </div>
                <div class="lineaBotones">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClientClick="return validarFormulario('obligatorio');" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClientClick="$('#parteFormulario').fadeOut(500,function(){limpiarCampos();$('#parteConsulta').fadeIn();});return false;" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
