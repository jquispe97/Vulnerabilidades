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
    public partial class mantenimientoSeguro : paginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                gvConsultar.DataSource = new SeguroDA().Listar(txtFiltroRUC.Text, txtFiltroRazonSocial.Text);
                gvConsultar.DataBind();
                upGeneral.Update();
            }
            catch (Exception ex)
            {
                RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "mantenimientoSeguro.aspx");
                MostarMensaje(varError.mensaje);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRUCSeguro.Text))
                {
                    MostarMensaje("El campo RUC es obligatorio", "$('#placeCuerpo_txtRUCSeguro').focus();");
                    return;
                }
                else if (string.IsNullOrEmpty(txtRazonSocial.Text))
                {
                    MostarMensaje("El campo Razon social es obligatorio", "$('#placeCuerpo_txtRazonSocial').focus();");
                    return;
                }
                else if (string.IsNullOrEmpty(txtOrden.Text))
                {
                    MostarMensaje("El campo Orden es obligatorio", "$('#placeCuerpo_txtOrden').focus();");
                    return;
                }
                else if (!Regex.IsMatch(txtOrden.Text, @"^[0-9]+$"))
                {
                    MostarMensaje("El campo Orden debe ser numérico", "$('#placeCuerpo_txtOrden').focus();");
                    return;
                }
                else if (!Regex.IsMatch(txtIDEquivalente.Text, @"^[0-9]+$"))
                {
                    MostarMensaje("El campo ID Equivalente debe ser numérico", "$('#placeCuerpo_txtIDEquivalente').focus();");
                    return;
                }
                if (string.IsNullOrEmpty(hfRUCSeguro.Value))
                {
                    new SeguroDA().Insertar(txtRUCSeguro.Text, txtRazonSocial.Text, int.Parse(txtOrden.Text), int.Parse(txtIDEquivalente.Text));
                }
                else
                {
                    new SeguroDA().Modificar(hfRUCSeguro.Value, txtRazonSocial.Text, int.Parse(txtOrden.Text), int.Parse(txtIDEquivalente.Text));
                }
                MostarMensaje("Transacción realizada correctamente");
                hfRUCSeguro.Value = "";
                txtRUCSeguro.Text = "";
                txtRazonSocial.Text = "";
                txtOrden.Text = "";
                txtIDEquivalente.Text = "";
                btnConsultar_Click(null, null);
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("Violation of PRIMARY KEY"))
                {
                    MostarMensaje("El RUC ingresado ya existe en la BD");
                }
                else
                {
                    RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "mantenimientoSeguro.aspx");
                    MostarMensaje(varError.mensaje);
                }
            }
        }

        protected void gvConsultar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Actualizar")
            {
                try
                {
                    int indice = int.Parse(e.CommandArgument.ToString().Split('|')[0]);
                    string ruc = e.CommandArgument.ToString().Split('|')[1];
                    hfRUCSeguro.Value = ruc;
                    txtRUCSeguro.Text = ruc;
                    txtRazonSocial.Text = gvConsultar.Rows[indice].Cells[1].Text;
                    txtOrden.Text = gvConsultar.Rows[indice].Cells[2].Text;
                    txtIDEquivalente.Text = gvConsultar.Rows[indice].Cells[3].Text;
                    lblTitulo.Text = "Modificar seguro " + ruc;
                    upGeneral.Update();
                    EjecutarScript("$('#parteConsulta').fadeOut(500,function(){$('#placeCuerpo_txtRUCSeguro').attr('readonly', true);$('#parteFormulario').fadeIn();});$('#areaTrabajo').scrollTop(0);");
                }
                catch (Exception ex)
                {
                    RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "mantenimientoSeguro.aspx");
                    MostarMensaje(varError.mensaje);
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    new SeguroDA().Eliminar(e.CommandArgument.ToString());
                    MostarMensaje("Transacción realizada correctamente");
                    btnConsultar_Click(null, null);
                }
                catch (Exception ex)
                {
                    RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "mantenimientoSeguro.aspx");
                    MostarMensaje(varError.mensaje);
                }
            }
        }
    }
}