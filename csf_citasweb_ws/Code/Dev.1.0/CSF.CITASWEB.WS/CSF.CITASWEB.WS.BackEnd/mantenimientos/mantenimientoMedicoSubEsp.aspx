<%@ Page Title="" Language="C#" MasterPageFile="~/master/sanna.Master" AutoEventWireup="true"
    CodeBehind="mantenimientoMedicoSubEsp.aspx.cs" Inherits="CSF.CITASWEB.WS.BackEnd.mantenimientos.mantenimientoMedicoSubEsp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="placeCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeTitulo" runat="server">
    Mantenimiento de médicos sub especialidad
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="placeCuerpo" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/js/mantenimientoMedicoSubEsp.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="upGeneral" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div id="parteConsulta">
                <div id="segmentoFiltro">
                    <div class="linea">
                        <div class="divInput" style="width: 30%;">
                            <asp:TextBox ID="txtFiltroMedico" runat="server" CssClass="input" placeholder="" autocomplete="off"></asp:TextBox>
                            <label>
                                Médico</label>
                            <span class="focus-border"></span>
                        </div>
                        <div class="divInput" style="width: 30%;">
                            <asp:TextBox ID="txtFiltroClinica" runat="server" CssClass="input" placeholder="" autocomplete="off"></asp:TextBox>
                            <label>
                                Clínica</label>
                            <span class="focus-border"></span>
                        </div>
                        <div class="divInput" style="width: 30%;">
                            <asp:TextBox ID="txtFiltroEspecialidad" runat="server" CssClass="input" placeholder="" autocomplete="off"></asp:TextBox>
                            <label>
                                Especialidad</label>
                            <span class="focus-border"></span>
                        </div>
                    </div>
                    <div class="lineaBotones">
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" />
                    </div>
                </div>
                <div id="segmentoConsultar">
                    <span style="font-size: 12px;">*Sólo se muestran los primeros 100 registros encontrados</span>
                    <asp:GridView ID="gvConsultar" CssClass="grilla" runat="server" AutoGenerateColumns="false"
                        RowStyle-HorizontalAlign="Center" OnRowCommand="gvConsultar_RowCommand" style="margin-top:10px;">
                        <Columns>
                            <asp:TemplateField HeaderText="Médico" ItemStyle-Width="50%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkID" runat="server" CommandName="Actualizar" CommandArgument='<%# Container.DataItemIndex + "|" + Eval("CMP") + "|" + Eval("IDClinica") + "|" + Eval("IDSubEspecialidad") %>'><%#Eval("NombreMedico")%></asp:LinkButton>
                                    <asp:HiddenField ID="hfNombreMedico" runat="server" Value='<%# Eval("NombreMedico")  %>' />
                                    <asp:HiddenField ID="hfNombreClinica" runat="server" Value='<%# Eval("NombreClinica")  %>' />
                                    <asp:HiddenField ID="hfNombreEspecialidad" runat="server" Value='<%# Eval("NombreEspecialidad")  %>' />
                                    <asp:HiddenField ID="hfTipoCitas" runat="server" Value='<%# Eval("TipoCitas")  %>' />
                                    <asp:HiddenField ID="hfInformacionCita" runat="server" Value='<%# Eval("InformacionCita")  %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NombreClinica" HeaderText="Clínica" ItemStyle-Width="22%" />
                            <asp:BoundField DataField="NombreEspecialidad" HeaderText="Especialidad" ItemStyle-Width="22%" />
                            <asp:BoundField DataField="TipoCitas" HeaderText="Tipo" ItemStyle-Width="6%" />
                            <asp:BoundField DataField="EsTeleorientacion" HeaderText="Teleorietacion" ItemStyle-Width="6%" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div id="parteFormulario" style="display: none;">
                <div style="width: 90%; margin-left: 5%; border-bottom: 1px solid #CCC; font-size: 130%;
                    padding: 30px 0px 15px 0px;">
                    <asp:Label ID="lblTitulo" runat="server" Text="Nuevo médico"></asp:Label>
                </div>
                <div class="divInput">
                    <asp:HiddenField ID="hfCMP" runat="server" />
                    <asp:TextBox ID="txtMedico" runat="server" CssClass="input obligatorio" placeholder="" ReadOnly="true" Enabled="false"></asp:TextBox>
                    <label>
                        Médico</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:HiddenField ID="hfIDClinica" runat="server" />
                    <asp:TextBox ID="txtClinica" runat="server" CssClass="input obligatorio" ReadOnly="true" Enabled="false"
                        placeholder=""></asp:TextBox>
                    <label>
                        Clínica</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:HiddenField ID="hfIDSubEspecialidad" runat="server" />
                    <asp:TextBox ID="txtEspecialidad" runat="server" CssClass="input obligatorio" ReadOnly="true" Enabled="false"
                        placeholder=""></asp:TextBox>
                    <label>
                        Especialidad</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:DropDownList ID="ddlTipoCitas" runat="server" CssClass="input">
                        <asp:ListItem Value="1" Text="Inactivo (No aparece en los listados)" />
                        <asp:ListItem Value="2" Text="Solo Staff (Se visualizan pero no hay ninguna acción)" />
                        <asp:ListItem Value="3" Text="Coordinacion Telefonica" />
                        <asp:ListItem Value="4" Text="Gestion de Cita Web/App" />
                        <asp:ListItem Value="5" Text="Médicos que no son de staff Sanna pero no se deben mostrar" />
                    </asp:DropDownList>
                    <label>
                        Tipo de citas</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:DropDownList ID="ddlTeleorientacion" runat="server" CssClass="input">
                        <asp:ListItem Value="0" Text="Inactivo " />
                        <asp:ListItem Value="1" Text="Activo" />
                    </asp:DropDownList>
                    <label>
                        Teleorientacion</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtInformacionCita" TextMode="MultiLine" Rows="3" runat="server"
                        CssClass="input" placeholder=""></asp:TextBox>
                    <label>
                        Informacion de cita</label>
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
