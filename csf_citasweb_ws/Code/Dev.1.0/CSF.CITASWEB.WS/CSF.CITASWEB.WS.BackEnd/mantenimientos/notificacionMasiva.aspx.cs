using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSF.CITASWEB.WS.DA;
using CSF.CITASWEB.WS.BE;
using CSF.CITASWEB.WS.BackEnd.util;
using System.Text.RegularExpressions;

namespace CSF.CITASWEB.WS.BackEnd.mantenimientos
{
    public partial class notificacionMasiva : paginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                gvConsultar.DataSource = new NotificacionMasivaDA().Listar();
                gvConsultar.DataBind();
                upGeneral.Update();
            }
            catch (Exception ex)
            {
                RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "notificacionMasiva.aspx");
                MostarMensaje(varError.mensaje);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTitulo.Text))
                {
                    MostarMensaje("El campo Título es obligatorio","$('#txtTitulo').focus();");
                    return;
                }
                else if (string.IsNullOrEmpty(txtMensaje.Text))
                {
                    MostarMensaje("El campo Mensaje es obligatorio", "$('#txtMensaje').focus();");
                    return;
                }
                else if (!Regex.IsMatch(txtFiltro.Text, @"^((,)?[0-9]{1}\|[0-9]{8,20})*$"))
                {
                    MostarMensaje("El campo Filtro no tiene el formato correcto", "$('#txtFiltro').focus();");
                    return;
                }
                new NotificacionMasivaDA().Insertar(int.Parse(Session["TipoDocumento"].ToString()), Session["NumeroDocumento"].ToString(), txtTitulo.Text, txtMensaje.Text, txtFiltro.Text);
                MostarMensaje("Transacción realizada correctamente");
                txtTitulo.Text = "";
                txtMensaje.Text = "";
                txtFiltro.Text = "";
                btnConsultar_Click(null, null);
            }
            catch (Exception ex)
            {
                RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "notificacionMasiva.aspx");
                MostarMensaje(varError.mensaje);
            }
        }
    }
}