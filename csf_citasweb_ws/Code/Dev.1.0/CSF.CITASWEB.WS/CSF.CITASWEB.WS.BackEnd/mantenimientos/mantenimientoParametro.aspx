<%@ Page Title="" Language="C#" MasterPageFile="~/master/sanna.Master" AutoEventWireup="true"
    CodeBehind="mantenimientoParametro.aspx.cs" Inherits="CSF.CITASWEB.WS.BackEnd.mantenimientos.mantenimientoParametro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="placeCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeTitulo" runat="server">
    Mantenimiento de parámetros
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="placeCuerpo" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/js/mantenimientoParametro.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="upGeneral" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div id="parteConsulta">
                <div id="segmentoFiltro">
                    <div class="linea">
                        <div class="divInput">
                            <asp:TextBox ID="txtFiltroCodParametro" runat="server" CssClass="input" placeholder=""></asp:TextBox>
                            <label>
                                Código</label>
                            <span class="focus-border"></span>
                        </div>
                    </div>
                    <div class="lineaBotones">
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" />
                    </div>
                </div>
                <div id="segmentoConsultar">
                    <asp:GridView ID="gvConsultar" CssClass="grilla" runat="server" AutoGenerateColumns="false"
                        RowStyle-HorizontalAlign="Center" OnRowCommand="gvConsultar_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Codigo" ItemStyle-Width="50%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkCodigo" runat="server" CommandName="Actualizar" CommandArgument='<%# Eval("CodParametro") + "|" + Eval("ValorParametro") %>'><%#Eval("CodParametro")%></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ValorParametro" HeaderText="Valor del parámetro" ItemStyle-Width="50%" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div id="parteFormulario" style="display: none;">
                <div style="width: 90%; margin-left: 5%; border-bottom: 1px solid #CCC; font-size: 130%;
                    padding: 30px 0px 15px 0px;">
                    <asp:Label ID="lblTitulo" runat="server" Text="Modificar parámetro"></asp:Label>
                    <asp:HiddenField ID="hfCodParametro" runat="server" />
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtCodParametro" ReadOnly="true" runat="server" CssClass="input obligatorio" placeholder=""></asp:TextBox>
                    <label>
                        Código</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtValorParametro" runat="server" CssClass="input obligatorio" placeholder=""></asp:TextBox>
                    <label>
                        Valor</label>
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
