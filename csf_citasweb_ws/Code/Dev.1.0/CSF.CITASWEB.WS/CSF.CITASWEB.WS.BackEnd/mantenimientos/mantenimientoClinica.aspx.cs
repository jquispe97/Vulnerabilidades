using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSF.CITASWEB.WS.BackEnd.util;
using CSF.CITASWEB.WS.DA;
using CSF.CITASWEB.WS.BE;
using System.Text.RegularExpressions;

namespace CSF.CITASWEB.WS.BackEnd.mantenimientos
{
    public partial class mantenimientoClinica : paginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                gvConsultar.DataSource = new ClinicasEspecialidadesDA().ClinicaListar(txtFiltroNombre.Text);
                gvConsultar.DataBind();
                upGeneral.Update();
            }
            catch (Exception ex)
            {
                RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "mantenimientoClinica.aspx");
                MostarMensaje(varError.mensaje);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                MantClinicaBE varEntidad = new MantClinicaBE()
                {
                    IDClinica = string.IsNullOrEmpty(hfIDClinica.Value) ? 0 : int.Parse(hfIDClinica.Value),
                    Nombre = txtNombre.Text,
                    RUC = txtRUC.Text,
                    RUCSunasa = txtRUCSunasa.Text,
                    CodigoSunasa = txtCodigoSunasa.Text,
                    Tipo = int.Parse(ddlTipo.SelectedValue),
                    Direccion = txtDireccion.Text,
                    Ciudad = txtCiudad.Text,
                    Foto = txtFoto.Text,
                    Abreviatura = txtAbreviatura.Text,
                    HorariosAtencion = txtHorariosAtencion.Text,
                    Telefono = txtTelefono.Text,
                    Latitud = decimal.Parse(txtLatitud.Text),
                    Longitud = decimal.Parse(txtLongitud.Text),
                    EstadoActivo = bool.Parse(ddlEstadoActivo.SelectedValue)
                };

                if (string.IsNullOrEmpty(hfIDClinica.Value))
                {
                    new ClinicasEspecialidadesDA().ClinicaInsertar(varEntidad);
                }
                else
                {
                    new ClinicasEspecialidadesDA().ClinicaModificar(varEntidad);
                }
                MostarMensaje("Transacción realizada correctamente");
                hfIDClinica.Value = "";
                txtNombre.Text = "";
                txtRUC.Text = "";
                txtRUCSunasa.Text = "";
                txtCodigoSunasa.Text = "";
                ddlTipo.SelectedValue = "1";
                txtDireccion.Text = "";
                txtCiudad.Text = "";
                txtFoto.Text = "";
                txtAbreviatura.Text = "";
                txtHorariosAtencion.Text = "";
                txtTelefono.Text = "";
                txtLatitud.Text = "";
                txtLongitud.Text = "";
                ddlEstadoActivo.SelectedValue = "True";
                btnConsultar_Click(null, null);
            }
            catch (Exception ex)
            {
                RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "mantenimientoClinica.aspx");
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
                    string idClinica = e.CommandArgument.ToString().Split('|')[1];
                    TableCell varCelda = gvConsultar.Rows[indice].Cells[0];
                    hfIDClinica.Value = idClinica;
                    txtNombre.Text = ((HiddenField)varCelda.FindControl("hfNombre")).Value;
                    txtRUC.Text = ((HiddenField)varCelda.FindControl("hfRUC")).Value;
                    txtRUCSunasa.Text = ((HiddenField)varCelda.FindControl("hfRUCSunasa")).Value;
                    txtCodigoSunasa.Text = ((HiddenField)varCelda.FindControl("hfCodigoSunasa")).Value;
                    ddlTipo.SelectedValue = ((HiddenField)varCelda.FindControl("hfTipo")).Value;
                    txtDireccion.Text = ((HiddenField)varCelda.FindControl("hfDireccion")).Value;
                    txtCiudad.Text = ((HiddenField)varCelda.FindControl("hfCiudad")).Value;
                    txtFoto.Text = ((HiddenField)varCelda.FindControl("hfFoto")).Value;
                    txtAbreviatura.Text = ((HiddenField)varCelda.FindControl("hfAbreviatura")).Value;
                    txtHorariosAtencion.Text = ((HiddenField)varCelda.FindControl("hfHorariosAtencion")).Value;
                    txtTelefono.Text = ((HiddenField)varCelda.FindControl("hfTelefono")).Value;
                    txtLatitud.Text = ((HiddenField)varCelda.FindControl("hfLatitud")).Value;
                    txtLongitud.Text = ((HiddenField)varCelda.FindControl("hfLongitud")).Value;
                    ddlEstadoActivo.SelectedValue = ((HiddenField)varCelda.FindControl("hfEstadoActivo")).Value;
                    lblTitulo.Text = "Modificar clínica " + ((HiddenField)varCelda.FindControl("hfNombre")).Value;

                    upGeneral.Update();
                    EjecutarScript("$('#parteConsulta').fadeOut(500,function(){$('#placeCuerpo_txtRUCSeguro').attr('readonly', true);$('#parteFormulario').fadeIn();});$('#areaTrabajo').scrollTop(0);");
                }
                catch (Exception ex)
                {
                    RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "mantenimientoClinica.aspx");
                    MostarMensaje(varError.mensaje);
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    new ClinicasEspecialidadesDA().ClinicaEliminar(e.CommandArgument.ToString());
                    MostarMensaje("Transacción realizada correctamente");
                    btnConsultar_Click(null, null);
                }
                catch (Exception ex)
                {
                    RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "mantenimientoClinica.aspx");
                    MostarMensaje(varError.mensaje);
                }
            }
        }
    }
}