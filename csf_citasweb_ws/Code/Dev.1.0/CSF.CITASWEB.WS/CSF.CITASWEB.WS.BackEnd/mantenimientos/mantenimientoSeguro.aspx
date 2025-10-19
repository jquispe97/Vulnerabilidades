<%@ Page Title="" Language="C#" MasterPageFile="~/master/sanna.Master" AutoEventWireup="true"
    CodeBehind="mantenimientoSeguro.aspx.cs" Inherits="CSF.CITASWEB.WS.BackEnd.mantenimientos.mantenimientoSeguro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="placeCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeTitulo" runat="server">
    Mantenimiento de seguros
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="placeCuerpo" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/js/mantenimientoSeguro.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="upGeneral" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div id="parteConsulta">
                <div id="segmentoFiltro">
                    <div class="linea">
                        <div class="divInput">
                            <asp:TextBox ID="txtFiltroRUC" runat="server" CssClass="input" placeholder=""></asp:TextBox>
                            <label>
                                RUC</label>
                            <span class="focus-border"></span>
                        </div>
                        <div class="divInput">
                            <asp:TextBox ID="txtFiltroRazonSocial" runat="server" CssClass="input" placeholder=""></asp:TextBox>
                            <label>
                                Razon social</label>
                            <span class="focus-border"></span>
                        </div>
                    </div>
                    <div class="lineaBotones">
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" />
                        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClientClick="$('#parteConsulta').fadeOut(500,function(){$('#placeCuerpo_txtRUCSeguro').attr('readonly', false);$('#placeCuerpo_hfRUCSeguro').val('');$('#parteFormulario').fadeIn();});$('#placeCuerpo_lblTitulo').html('Nuevo seguro');$('#areaTrabajo').scrollTop(0);return false;" />
                    </div>
                </div>
                <div id="segmentoConsultar">
                    <asp:GridView ID="gvConsultar" CssClass="grilla" runat="server" AutoGenerateColumns="false"
                        RowStyle-HorizontalAlign="Center" OnRowCommand="gvConsultar_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="RUC" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkRUC" runat="server" CommandName="Actualizar" CommandArgument='<%# Container.DataItemIndex + "|" + Eval("RUCSeguro") %>'><%#Eval("RUCSeguro") %></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RazonSocial" HeaderText="RazonSocial" ItemStyle-Width="55%" />
                            <asp:BoundField DataField="Orden" HeaderText="Orden" ItemStyle-Width="10%" />
                            <asp:BoundField DataField="IDEquivalente" HeaderText="ID Equivalente" ItemStyle-Width="10%" />
                            <asp:TemplateField HeaderText="" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("RUCSeguro") %>' OnClientClick="return confirm('Estás seguro que deseas eliminar el registro?');">Eliminar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div id="parteFormulario" style="display: none;">
                <div style="width: 90%; margin-left: 5%; border-bottom: 1px solid #CCC; font-size: 130%;
                    padding: 30px 0px 15px 0px;">
                    <asp:Label ID="lblTitulo" runat="server" Text="Nuevo seguro"></asp:Label>
                    <asp:HiddenField ID="hfRUCSeguro" runat="server" />
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtRUCSeguro" TextMode="Number" runat="server" CssClass="input obligatorio"
                        placeholder=""></asp:TextBox>
                    <label>
                        RUC del Seguro</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="input obligatorio" placeholder=""
                        MaxLength="50"></asp:TextBox>
                    <label>
                        Razon social</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtOrden" TextMode="Number" runat="server" CssClass="input obligatorio"
                        placeholder=""></asp:TextBox>
                    <label>
                        Orden</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtIDEquivalente" TextMode="Number" runat="server" CssClass="input obligatorio"
                        placeholder=""></asp:TextBox>
                    <label>
                        ID Equivalente</label>
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
