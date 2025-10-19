<%@ Page Title="" Language="C#" MasterPageFile="~/master/sanna.Master" AutoEventWireup="true"
    CodeBehind="mantenimientoClinica.aspx.cs" Inherits="CSF.CITASWEB.WS.BackEnd.mantenimientos.mantenimientoClinica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="placeCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeTitulo" runat="server">
    Mantenimiento de clínicas
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="placeCuerpo" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/js/mantenimientoClinica.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="upGeneral" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div id="parteConsulta">
                <div id="segmentoFiltro">
                    <div class="linea">
                        <div class="divInput" style="width: 30%;">
                            <asp:TextBox ID="txtFiltroNombre" runat="server" CssClass="input" placeholder=""></asp:TextBox>
                            <label>
                                Nombre</label>
                            <span class="focus-border"></span>
                        </div>
                    </div>
                    <div class="lineaBotones">
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" />
                        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClientClick="$('#parteConsulta').fadeOut(500,function(){$('#placeCuerpo_txtIDClinica').attr('readonly', false);$('#placeCuerpo_hfIDClinica').val('');$('#parteFormulario').fadeIn();});$('#placeCuerpo_lblTitulo').html('Nueva clínica');$('#areaTrabajo').scrollTop(0);return false;" />
                    </div>
                </div>
                <div id="segmentoConsultar">
                    <asp:GridView ID="gvConsultar" CssClass="grilla" runat="server" AutoGenerateColumns="false"
                        RowStyle-HorizontalAlign="Center" OnRowCommand="gvConsultar_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="ID Clínica" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkID" runat="server" CommandName="Actualizar" CommandArgument='<%# Container.DataItemIndex + "|" + Eval("IDClinica") %>'><%#Eval("IDClinica") %></asp:LinkButton>                                    
                                    <asp:HiddenField ID="hfNombre" runat="server" Value='<%# Eval("Nombre")  %>' />
                                    <asp:HiddenField ID="hfRUC" runat="server" Value='<%# Eval("RUC")  %>' />
                                    <asp:HiddenField ID="hfRUCSunasa" runat="server" Value='<%# Eval("RUCSunasa")  %>' />
                                    <asp:HiddenField ID="hfCodigoSunasa" runat="server" Value='<%# Eval("CodigoSunasa")  %>' />
                                    <asp:HiddenField ID="hfTipo" runat="server" Value='<%# Eval("Tipo")  %>' />
                                    <asp:HiddenField ID="hfDireccion" runat="server" Value='<%# Eval("Direccion")  %>' />
                                    <asp:HiddenField ID="hfCiudad" runat="server" Value='<%# Eval("Ciudad")  %>' />
                                    <asp:HiddenField ID="hfFoto" runat="server" Value='<%# Eval("Foto")  %>' />
                                    <asp:HiddenField ID="hfAbreviatura" runat="server" Value='<%# Eval("Abreviatura")  %>' />
                                    <asp:HiddenField ID="hfHorariosAtencion" runat="server" Value='<%# Eval("HorariosAtencion")  %>' />
                                    <asp:HiddenField ID="hfTelefono" runat="server" Value='<%# Eval("Telefono")  %>' />
                                    <asp:HiddenField ID="hfLatitud" runat="server" Value='<%# Eval("Latitud")  %>' />
                                    <asp:HiddenField ID="hfLongitud" runat="server" Value='<%# Eval("Longitud")  %>' />
                                    <asp:HiddenField ID="hfEstadoActivo" runat="server" Value='<%# Eval("EstadoActivo")  %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-Width="50%" />
                            <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" ItemStyle-Width="20%" />
                            <asp:TemplateField HeaderText="Estado" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblEstado" runat="server"><%# bool.Parse(Eval("EstadoActivo").ToString()) == true ? "Activo" : "No activo"%></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("IDClinica") %>'
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
                    <asp:Label ID="lblTitulo" runat="server" Text="Nueva clínica"></asp:Label>
                    <asp:HiddenField ID="hfIDClinica" runat="server" />
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="input obligatorio" placeholder=""></asp:TextBox>
                    <label>
                        Nombre</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtRUC" runat="server" CssClass="input" placeholder=""></asp:TextBox>
                    <label>
                        RUC</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtRUCSunasa" runat="server" CssClass="input" placeholder=""></asp:TextBox>
                    <label>
                        RUC Sunasa</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtCodigoSunasa" runat="server" CssClass="input" placeholder=""></asp:TextBox>
                    <label>
                        Codigo Sunasa</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:DropDownList ID="ddlTipo" runat="server" CssClass="input obligatorio">
                        <asp:ListItem Value="1" Text="Clínica" />
                        <asp:ListItem Value="2" Text="Centro Médico" />
                    </asp:DropDownList>
                    <label>
                        Tipo</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="input obligatorio" placeholder=""></asp:TextBox>
                    <label>
                        Direccion</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtCiudad" runat="server" CssClass="input obligatorio" placeholder=""></asp:TextBox>
                    <label>
                        Ciudad</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtFoto" runat="server" CssClass="input obligatorio imagen" placeholder=""></asp:TextBox>
                    <img src="../images/iconos/link.png" class="botonLink" alt="" onclick="verFoto('placeCuerpo_txtFoto');" />
                    <label>
                        Foto</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtAbreviatura" runat="server" CssClass="input" placeholder=""></asp:TextBox>
                    <label>
                        Abreviatura</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtHorariosAtencion" TextMode="MultiLine" Rows="3" runat="server" CssClass="input obligatorio"
                        placeholder=""></asp:TextBox>
                    <label>
                        Horarios de atencion</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="input obligatorio" placeholder=""></asp:TextBox>
                    <label>
                        Telefono</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtLatitud" runat="server" CssClass="input obligatorio" placeholder=""></asp:TextBox>
                    <label>
                        Latitud</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:TextBox ID="txtLongitud" runat="server" CssClass="input obligatorio" placeholder=""></asp:TextBox>
                    <label>
                        Longitud</label>
                    <span class="focus-border"></span>
                </div>
                <div class="divInput">
                    <asp:DropDownList ID="ddlEstadoActivo" runat="server" CssClass="input obligatorio">
                        <asp:ListItem Value="True" Text="Activo" />
                        <asp:ListItem Value="False" Text="Inactivo" />
                    </asp:DropDownList>
                    <label>
                        Estado</label>
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
