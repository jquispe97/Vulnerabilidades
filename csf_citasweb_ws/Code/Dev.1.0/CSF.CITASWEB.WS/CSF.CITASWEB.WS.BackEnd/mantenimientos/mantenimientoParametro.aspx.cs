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
    public partial class mantenimientoParametro : paginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                gvConsultar.DataSource = new ParametrizacionDA().Listar(txtFiltroCodParametro.Text);
                gvConsultar.DataBind();
                upGeneral.Update();
            }
            catch (Exception ex)
            {
                RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "mantenimientoParametro.aspx");
                MostarMensaje(varError.mensaje);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtValorParametro.Text))
                {
                    MostarMensaje("El campo Valor es obligatorio", "$('#placeCuerpo_txtValorParametro').focus();");
                    return;
                }

                new ParametrizacionDA().Modificar(hfCodParametro.Value, txtValorParametro.Text);
                MostarMensaje("Transacción realizada correctamente");
                hfCodParametro.Value = "";
                txtCodParametro.Text = "";
                txtValorParametro.Text = "";
                btnConsultar_Click(null, null);
            }
            catch (Exception ex)
            {
                RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "mantenimientoParametro.aspx");
                MostarMensaje(varError.mensaje);
            }
        }

        protected void gvConsultar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Actualizar")
            {
                try
                {
                    string codParametro = e.CommandArgument.ToString().Split('|')[0];
                    string valorParametro = e.CommandArgument.ToString().Split('|')[1];
                    hfCodParametro.Value = codParametro;
                    txtCodParametro.Text = codParametro;
                    txtValorParametro.Text = valorParametro;
                    lblTitulo.Text = "Modificar Parametro " + codParametro;
                    upGeneral.Update();
                    EjecutarScript("$('#parteConsulta').fadeOut(500,function(){$('#parteFormulario').fadeIn();});");
                }
                catch (Exception ex)
                {
                    RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "mantenimientoParametro.aspx");
                    MostarMensaje(varError.mensaje);
                }
            }
        }
    }
}