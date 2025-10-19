using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSF.CITASWEB.WS.DA;
using CSF.CITASWEB.WS.BE;
using CSF.CITASWEB.WS.BackEnd.util;
using System.Text;
using System.IO;

namespace CSF.CITASWEB.WS.BackEnd.metricas
{
    public partial class usuariosApp : paginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
        }

        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder varReporte = new ReporteUsuarioDA().GenerarReporte(DateTime.Parse(txtFechaInicio.Text + " 00:00:00"), DateTime.Parse(txtFechaFin.Text + " 23:59:59"));
                if (varReporte == null)
                {
                    MostarMensaje("No se encontraron registros que coincidan con los parámetros ingresados");
                }
                else
                {
                    string varNombreArchivo = "usuariosApp" + Session["TipoDocumento"].ToString() + "_" + Session["NumeroDocumento"].ToString() + ".csv";
                    if (File.Exists(Server.MapPath("~/temporal/" + varNombreArchivo)))
                        File.Delete(Server.MapPath("~/temporal/" + varNombreArchivo));

                    using (StreamWriter varEscritor = new StreamWriter(Server.MapPath("~/temporal/" + varNombreArchivo), false, Encoding.UTF8))
                    {
                        varEscritor.Write(varReporte.ToString());
                    }
                    EjecutarScript("window.open('../temporal/" + varNombreArchivo + "');");
                }
            }
            catch (Exception ex)
            {
                RespuestaSimpleBE varError = new ErrorDA().RegistrarError(ex, "Web", "usuariosApp.aspx");
                MostarMensaje(varError.mensaje);
            }
        }
    }
}