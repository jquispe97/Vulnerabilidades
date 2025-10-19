<%@ Page Title="" Language="C#" MasterPageFile="~/master/sanna.Master" AutoEventWireup="true"
    CodeBehind="mantenimientoPreguntasFrecuentes.aspx.cs" Inherits="CSF.CITASWEB.WS.BackEnd.mantenimientos.mantenimientoPreguntasFrecuentes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="placeCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeTitulo" runat="server">
    Mantenimiento de preguntas frecuentes
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="placeCuerpo" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/js/mantenimientoPreguntasFrecuentes.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="upGeneral" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div id="parteConsulta">
                <div id="segmentoFiltro">
                    <div class="lineaBotones">
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" />
                        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClientClick="$('#parteConsulta').fadeOut(500,function(){$('#placeCuerpo_txtIDPreguntaFrecuente').attr('readonly', false);$('#placeCuerpo_hfIDPreguntaFrecuente').val('');$('#parteFormulario').fadeIn();});$('#placeCuerpo_lblTitulo').html('Nueva pregunta frecuente');$('#areaTrabajo').scrollTop(0);return false;" />
                    </div>
                </div>
                <div id="segmentoConsultar">
                    <asp:GridView ID="gvConsultar" CssClass="grilla" runat="server" AutoGenerateColumns="false"
                        RowStyle-HorizontalAlign="Center" OnRowCommand="gvConsultar_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="ID Clínica" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkID" runat="server" CommandName="Actualizar" CommandArgument='<%# Container.DataItemIndex + "|" + Eval("IDPreguntaFrecuente") %>'><%#Eval("IDPreguntaFrecuente")%></asp:LinkButton>
                                    <asp:HiddenField ID="hfPregunta" runat="server" Value='<%# Eval("Pregunta")  %>' />
                                    <asp:HiddenField ID="hfRespuesta" runat="server" Value='<%# Eval("Respuesta")  %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Pregunta" HeaderText="Pregunta" ItemStyle-Width="35%" />
                            <asp:BoundField DataField="Respuesta" HeaderText="Respuesta" ItemStyle-Width="55" />
                            <asp:TemplateField HeaderText="" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("IDPreguntaFrecuente") %>'
                                        OnClientClick="return confirm('Estás seguro que deseas eliminar el registro?');">Eliminar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div id="parteFormulario" style="display: none;">
                <div style="width: 90%; margin-left: 5%; border-bottom: 1px solid #CCC; font-size: 130%;
                    padding: 30px 0px 15px 0px;">
                    <asp:Label ID="lblTitulo" runat="server" Text="Nueva pregunta frecuente"></asp:Label>
                    <asp:HiddenField ID="hfIDPreguntaFrecuente" runat="server" />
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtPregunta" runat="server" CssClass="input obligatorio" placeholder=""></asp:TextBox>
                    <label>
                        Pregunta</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtRespuesta" runat="server" CssClass="input obligatorio" placeholder=""></asp:TextBox>
                    <label>
                        Respuesta</label>
                    <span class="focus-border"></span>
                </div>
                <div class="lineaBotones">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClientClick="return validarFormulario('obligatorio');"
                        OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClientClick="$('#parteFormulario').fadeOut(500,function(){limpiarCampos();$('#parteConsulta').fadeIn();});return false;" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
