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
    public partial class mantenimientoPreguntasFrecuentes : paginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                gvConsultar.DataSource = new PreguntaFrecuenteDA().Listar();
                gvConsultar.DataBind();
                upGeneral.Update();
            }
            catch (Exception ex)
            {
                RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "mantenimientoPreguntasFrecuentes.aspx");
                MostarMensaje(varError.mensaje);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                PreguntaFrecuenteBE varEntidad = new PreguntaFrecuenteBE()
                {
                    IDPreguntaFrecuente = string.IsNullOrEmpty(hfIDPreguntaFrecuente.Value) ? "0" : hfIDPreguntaFrecuente.Value,
                    Pregunta = txtPregunta.Text,
                    Respuesta = txtRespuesta.Text
                };

                if (string.IsNullOrEmpty(hfIDPreguntaFrecuente.Value))
                {
                    new PreguntaFrecuenteDA().Insertar(varEntidad);
                }
                else
                {
                    new PreguntaFrecuenteDA().Modificar(varEntidad);
                }
                MostarMensaje("Transacción realizada correctamente");
                hfIDPreguntaFrecuente.Value = "";
                txtPregunta.Text = "";
                txtRespuesta.Text = "";

                btnConsultar_Click(null, null);
            }
            catch (Exception ex)
            {
                RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "mantenimientoPreguntasFrecuentes.aspx");
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
                    string idPreguntaFrecuente = e.CommandArgument.ToString().Split('|')[1];
                    TableCell varCelda = gvConsultar.Rows[indice].Cells[0];
                    hfIDPreguntaFrecuente.Value = idPreguntaFrecuente;
                    txtPregunta.Text = ((HiddenField)varCelda.FindControl("hfPregunta")).Value;
                    txtRespuesta.Text = ((HiddenField)varCelda.FindControl("hfRespuesta")).Value;
                    lblTitulo.Text = "Modificar pregunta frecuente: " + ((HiddenField)varCelda.FindControl("hfPregunta")).Value;

                    upGeneral.Update();
                    EjecutarScript("$('#parteConsulta').fadeOut(500,function(){$('#parteFormulario').fadeIn();});$('#areaTrabajo').scrollTop(0);");
                }
                catch (Exception ex)
                {
                    RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "mantenimientoPreguntasFrecuentes.aspx");
                    MostarMensaje(varError.mensaje);
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    new PreguntaFrecuenteDA().Eliminar(e.CommandArgument.ToString());
                    MostarMensaje("Transacción realizada correctamente");
                    btnConsultar_Click(null, null);
                }
                catch (Exception ex)
                {
                    RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "mantenimientoPreguntasFrecuentes.aspx");
                    MostarMensaje(varError.mensaje);
                }
            }
        }
    }
}