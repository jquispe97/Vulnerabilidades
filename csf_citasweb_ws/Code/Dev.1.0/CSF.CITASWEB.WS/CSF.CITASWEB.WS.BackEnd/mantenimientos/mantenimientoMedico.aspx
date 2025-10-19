<%@ Page Title="" Language="C#" MasterPageFile="~/master/sanna.Master" AutoEventWireup="true"
    CodeBehind="mantenimientoMedico.aspx.cs" Inherits="CSF.CITASWEB.WS.BackEnd.mantenimientos.mantenimientoMedico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="placeCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeTitulo" runat="server">
    Mantenimiento de médicos
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="placeCuerpo" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/js/mantenimientoMedico.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="upGeneral" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div id="parteConsulta">
                <div id="segmentoFiltro">
                    <div class="linea">
                        <div class="divInput" style="width: 30%;">
                            <asp:TextBox ID="txtFiltroCMP" runat="server" CssClass="input" placeholder=""></asp:TextBox>
                            <label>
                                CMP</label>
                            <span class="focus-border"></span>
                        </div>
                        <div class="divInput" style="width: 30%;">
                            <asp:TextBox ID="txtFiltroNombre" runat="server" CssClass="input" placeholder=""></asp:TextBox>
                            <label>
                                Nombre</label>
                            <span class="focus-border"></span>
                        </div>
                    </div>
                    <div class="lineaBotones">
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" />
                        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClientClick="$('#parteConsulta').fadeOut(500,function(){$('#placeCuerpo_txtCMP').attr('readonly', false);$('#placeCuerpo_hfCMP').val('');$('#parteFormulario').fadeIn();});$('#placeCuerpo_lblTitulo').html('Nuevo médico');$('#areaTrabajo').scrollTop(0);return false;" />
                    </div>
                </div>
                <div id="segmentoConsultar">
                    <span style="font-size: 12px;">*Sólo se muestran los primeros 100 registros encontrados</span>
                    <asp:GridView ID="gvConsultar" CssClass="grilla" runat="server" AutoGenerateColumns="false"
                        RowStyle-HorizontalAlign="Center" OnRowCommand="gvConsultar_RowCommand" style="margin-top:10px;">
                        <Columns>
                            <asp:TemplateField HeaderText="CMP" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkID" runat="server" CommandName="Actualizar" CommandArgument='<%# Container.DataItemIndex + "|" + Eval("CMP") %>'><%#Eval("CMP") %></asp:LinkButton>
                                    <asp:HiddenField ID="hfNombres" runat="server" Value='<%# Eval("Nombres")  %>' />
                                    <asp:HiddenField ID="hfApellidoPaterno" runat="server" Value='<%# Eval("ApellidoPaterno")  %>' />
                                    <asp:HiddenField ID="hfApellidoMaterno" runat="server" Value='<%# Eval("ApellidoMaterno")  %>' />
                                    <asp:HiddenField ID="hfCargo" runat="server" Value='<%# Eval("Cargo")  %>' />
                                    <asp:HiddenField ID="hfMuestraCV" runat="server" Value='<%# Eval("MuestraCV")  %>' />
                                    <asp:HiddenField ID="hfFoto" runat="server" Value='<%# Eval("Foto")  %>' />
                                    <asp:HiddenField ID="hfTituloMedico" runat="server" Value='<%# Eval("TituloMedico")  %>' />
                                    <asp:HiddenField ID="hfPremios" runat="server" Value='<%# Eval("Premios")  %>' />
                                    <asp:HiddenField ID="hfPertenenciaSociedad" runat="server" Value='<%# Eval("PertenenciaSociedad")  %>' />
                                    <asp:HiddenField ID="hfInvestigaciones" runat="server" Value='<%# Eval("Investigaciones")  %>' />
                                    <asp:HiddenField ID="hfRNE" runat="server" Value='<%# Eval("RNE")  %>' />
                                    <asp:HiddenField ID="hfIdiomas" runat="server" Value='<%# Eval("Idiomas")  %>' />
                                    <asp:HiddenField ID="hfInformacionAdicional" runat="server" Value='<%# Eval("InformacionAdicional")  %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Nombres" HeaderText="Nombres" ItemStyle-Width="40%" />
                            <asp:BoundField DataField="ApellidoPaterno" HeaderText="ApellidoPaterno" ItemStyle-Width="25%" />
                            <asp:BoundField DataField="ApellidoMaterno" HeaderText="ApellidoMaterno" ItemStyle-Width="25%" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div id="parteFormulario" style="display: none;">
                <div style="width: 90%; margin-left: 5%; border-bottom: 1px solid #CCC; font-size: 130%;
                    padding: 30px 0px 15px 0px;">
                    <asp:Label ID="lblTitulo" runat="server" Text="Nuevo médico"></asp:Label>
                    <asp:HiddenField ID="hfCMP" runat="server" />
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtCMP" runat="server" CssClass="input obligatorio" placeholder=""></asp:TextBox>
                    <label>
                        CMP</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtNombres" runat="server" CssClass="input obligatorio" placeholder=""></asp:TextBox>
                    <label>
                        Nombres</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtApellidoPaterno" runat="server" CssClass="input obligatorio"
                        placeholder=""></asp:TextBox>
                    <label>
                        Apellido paterno</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtApellidoMaterno" runat="server" CssClass="input obligatorio"
                        placeholder=""></asp:TextBox>
                    <label>
                        Apellido materno</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtCargo" runat="server" CssClass="input" placeholder=""></asp:TextBox>
                    <label>
                        Cargo</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:DropDownList ID="ddlMuestraCV" runat="server" CssClass="input">
                        <asp:ListItem Value="True" Text="Si" />
                        <asp:ListItem Value="False" Text="No" />
                    </asp:DropDownList>
                    <label>
                        Muestra CV</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtFoto" runat="server" CssClass="input imagen" placeholder=""></asp:TextBox>
                    <img src="../images/iconos/link.png" class="botonLink" alt="" onclick="verFoto('placeCuerpo_txtFoto');" />
                    <label>
                        Foto</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtTituloMedico" runat="server" CssClass="input" placeholder=""></asp:TextBox>
                    <label>
                        Titulo médico</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtPremios" TextMode="MultiLine" Rows="3" runat="server" CssClass="input"
                        placeholder=""></asp:TextBox>
                    <label>
                        Premios</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtPertenenciaSociedad" TextMode="MultiLine" Rows="3" runat="server"
                        CssClass="input" placeholder=""></asp:TextBox>
                    <label>
                        Pertenencia en sociedades</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtInvestigaciones" TextMode="MultiLine" Rows="3" runat="server"
                        CssClass="input" placeholder=""></asp:TextBox>
                    <label>
                        Investigaciones</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtRNE" runat="server" CssClass="input" placeholder=""></asp:TextBox>
                    <label>
                        RNE</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtIdiomas" runat="server" CssClass="input" placeholder=""></asp:TextBox>
                    <label>
                        Idiomas</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtInformacionAdicional" TextMode="MultiLine" Rows="3" runat="server"
                        CssClass="input" placeholder=""></asp:TextBox>
                    <label>
                        Informacion adicional</label>
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
