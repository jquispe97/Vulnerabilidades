using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSF.CITASWEB.WS.BackEnd.util;
using CSF.CITASWEB.WS.DA;
using CSF.CITASWEB.WS.BE;
using System.Diagnostics;

namespace CSF.CITASWEB.WS.BackEnd.mantenimientos
{
    public partial class mantenimientoMedicoSubEsp : paginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                gvConsultar.DataSource = new MedicoSubEspecialidadDA().Listar(txtFiltroMedico.Text, txtFiltroEspecialidad.Text, txtFiltroClinica.Text);
                gvConsultar.DataBind();
                upGeneral.Update();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "mantenimientoMedicoSubEsp.aspx");
                MostarMensaje(varError.mensaje);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                MantMedicoSubEspecialidadBE varEntidad = new MantMedicoSubEspecialidadBE()
                {
                    CMP = hfCMP.Value,
                    IDSubEspecialidad = hfIDSubEspecialidad.Value,
                    IDClinica = hfIDClinica.Value,
                    TipoCitas = ddlTipoCitas.SelectedValue,
                    InformacionCita = txtInformacionCita.Text
                };

                new MedicoSubEspecialidadDA().Modificar(varEntidad);

                MostarMensaje("Transacción realizada correctamente");
                hfCMP.Value = "";
                txtMedico.Text = "";
                hfIDClinica.Value = "";
                txtClinica.Text = "";
                hfIDSubEspecialidad.Value = "";
                txtEspecialidad.Text = "";
                ddlTipoCitas.SelectedValue = "1";
                txtInformacionCita.Text = "";
                btnConsultar_Click(null, null);
            }
            catch (Exception ex)
            {
                RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "mantenimientoMedicoSubEsp.aspx");
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
                    string IDClinica = e.CommandArgument.ToString().Split('|')[2];
                    string IDSubEspecialidad = e.CommandArgument.ToString().Split('|')[3];
                    TableCell varCelda = gvConsultar.Rows[indice].Cells[0];
                    hfCMP.Value = CMP;
                    hfIDClinica.Value = IDClinica;
                    hfIDSubEspecialidad.Value = IDSubEspecialidad;
                    txtMedico.Text = "[" + CMP + "] " + ((HiddenField)varCelda.FindControl("hfNombreMedico")).Value;
                    txtClinica.Text = "[" + IDClinica + "] " + ((HiddenField)varCelda.FindControl("hfNombreClinica")).Value;
                    txtEspecialidad.Text = "[" + IDSubEspecialidad + "] " + ((HiddenField)varCelda.FindControl("hfNombreEspecialidad")).Value;

                    ddlTipoCitas.SelectedValue = ((HiddenField)varCelda.FindControl("hfTipoCitas")).Value;
                    txtInformacionCita.Text = ((HiddenField)varCelda.FindControl("hfInformacionCita")).Value;

                    lblTitulo.Text = "Modificar médico " + txtMedico.Text;

                    upGeneral.Update();
                    EjecutarScript("$('#parteConsulta').fadeOut(500,function(){$('#parteFormulario').fadeIn();});$('#areaTrabajo').scrollTop(0);");
                }
                catch (Exception ex)
                {
                    RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "mantenimientoMedicoSubEsp.aspx");
                    MostarMensaje(varError.mensaje);
                }
            }
            /*else if (e.CommandName == "Eliminar")
            {
                try
                {
                    new MedicoSubEspecialidaDA().Eliminar(e.CommandArgument.ToString());
                    MostarMensaje("Transacción realizada correctamente");
                    btnConsultar_Click(null, null);
                }
                catch (Exception ex)
                {
                    RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "mantenimientoMedicoSubEsp.aspx");
                    MostarMensaje(varError.mensaje);
                }
            }*/
        }
    }
}