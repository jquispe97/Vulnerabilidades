using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace CSF.CITASWEB.WS.BackEnd.util
{
    public class paginaBase : System.Web.UI.Page
    {
        public void MostarMensaje(string mensaje,string codigoAdicional = "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + mensaje.Replace("\'","").Replace("\r", "").Replace("\n", "") + "');" + codigoAdicional, true);
        }

        public void EjecutarScript(string codigo)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", codigo, true);
        }
    }
}