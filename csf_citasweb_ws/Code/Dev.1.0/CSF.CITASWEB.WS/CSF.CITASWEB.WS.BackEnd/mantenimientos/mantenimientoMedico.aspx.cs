using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSF.CITASWEB.WS.BackEnd.util;
using CSF.CITASWEB.WS.DA;
using CSF.CITASWEB.WS.BE;

namespace CSF.CITASWEB.WS.BackEnd.mantenimientos
{
    public partial class mantenimientoMedico : paginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                gvConsultar.DataSource = new MedicoDA().Listar(txtFiltroCMP.Text, txtFiltroNombre.Text);
                gvConsultar.DataBind();
                upGeneral.Update();
            }
            catch (Exception ex)
            {
                RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "mantenimientoMedico.aspx");
                MostarMensaje(varError.mensaje);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCMP.Text))
                {
                    MostarMensaje("Debes llenar el campo CMP");
                    return;
                }

                MantMedicoBE varEntidad = new MantMedicoBE()
                {
                    Nombres = txtNombres.Text,
                    ApellidoPaterno = txtApellidoPaterno.Text,
                    ApellidoMaterno = txtApellidoMaterno.Text,
                    Cargo = txtCargo.Text,
                    MuestraCV = bool.Parse(ddlMuestraCV.SelectedValue),
                    Foto = txtFoto.Text,
                    TituloMedico = txtTituloMedico.Text,
                    Premios = txtPremios.Text,
                    PertenenciaSociedad = txtPertenenciaSociedad.Text,
                    Investigaciones = txtInvestigaciones.Text,
                    RNE = string.IsNullOrEmpty(txtRNE.Text) ? (int?)null : int.Parse(txtRNE.Text),
                    Idiomas = txtIdiomas.Text,
                    InformacionAdicional = txtInformacionAdicional.Text
                };

                if (string.IsNullOrEmpty(hfCMP.Value))
                {
                    varEntidad.CMP = txtCMP.Text.Trim();
                    new MedicoDA().Insertar(varEntidad);
                }
                else
                {
                    varEntidad.CMP = hfCMP.Value.Trim();
                    new MedicoDA().Modificar(varEntidad);
                }
                MostarMensaje("Transacción realizada correctamente");
                hfCMP.Value = "";
                txtCMP.Text = "";
                txtNombres.Text = "";
                txtApellidoPaterno.Text = "";
                txtApellidoMaterno.Text = "";
                txtCargo.Text = "";
                ddlMuestraCV.SelectedValue = "True";
                txtFoto.Text = "";
                txtTituloMedico.Text = "";
                txtPremios.Text = "";
                txtPertenenciaSociedad.Text = "";
                txtInvestigaciones.Text = "";
                txtRNE.Text = "";
                txtIdiomas.Text = "";
                txtInformacionAdicional.Text = "";
                btnConsultar_Click(null, null);
            }
            catch (Exception ex)
            {
                RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "mantenimientoMedico.aspx");
                MostarMensaje(varError.mensaje);
            }
        }

        protected void gvConsultar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Actualizar")
            {
                try
                {
                    int indice = int.Parse(e.CommandArgument.ToString().Split('|')[0]);
                    string CMP = e.CommandArgument.ToString().Split('|')[1];
                    TableCell varCelda = gvConsultar.Rows[indice].Cells[0];
                    hfCMP.Value = CMP;
                    txtCMP.Text = CMP;
                    txtNombres.Text = ((HiddenField)varCelda.FindControl("hfNombres")).Value;
                    txtApellidoPaterno.Text = ((HiddenField)varCelda.FindControl("hfApellidoPaterno")).Value;
                    txtApellidoMaterno.Text = ((HiddenField)varCelda.FindControl("hfApellidoMaterno")).Value;
                    txtCargo.Text = ((HiddenField)varCelda.FindControl("hfCargo")).Value;
                    ddlMuestraCV.SelectedValue = ((HiddenField)varCelda.FindControl("hfMuestraCV")).Value;
                    txtFoto.Text = ((HiddenField)varCelda.FindControl("hfFoto")).Value;
                    txtTituloMedico.Text = ((HiddenField)varCelda.FindControl("hfTituloMedico")).Value;
                    txtPremios.Text = ((HiddenField)varCelda.FindControl("hfPremios")).Value;
                    txtPertenenciaSociedad.Text = ((HiddenField)varCelda.FindControl("hfPertenenciaSociedad")).Value;
                    txtInvestigaciones.Text = ((HiddenField)varCelda.FindControl("hfInvestigaciones")).Value;
                    txtRNE.Text = ((HiddenField)varCelda.FindControl("hfRNE")).Value;
                    txtIdiomas.Text = ((HiddenField)varCelda.FindControl("hfIdiomas")).Value;
                    txtInformacionAdicional.Text = ((HiddenField)varCelda.FindControl("hfInformacionAdicional")).Value;

                    lblTitulo.Text = "Modificar médico " + ((HiddenField)varCelda.FindControl("hfNombres")).Value;

                    upGeneral.Update();
                    EjecutarScript("$('#parteConsulta').fadeOut(500,function(){$('#placeCuerpo_txtCMP').attr('readonly', true);$('#parteFormulario').fadeIn();});$('#areaTrabajo').scrollTop(0);");
                }
                catch (Exception ex)
                {
                    RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "mantenimientoMedico.aspx");
                    MostarMensaje(varError.mensaje);
                }
            }
            /*else if (e.CommandName == "Eliminar")
            {
                try
                {
                    new MedicoDA().Eliminar(e.CommandArgument.ToString());
                    MostarMensaje("Transacción realizada correctamente");
                    btnConsultar_Click(null, null);
                }
                catch (Exception ex)
                {
                    RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "mantenimientoMedico.aspx");
                    MostarMensaje(varError.mensaje);
                }
            }*/
        }
    }
}