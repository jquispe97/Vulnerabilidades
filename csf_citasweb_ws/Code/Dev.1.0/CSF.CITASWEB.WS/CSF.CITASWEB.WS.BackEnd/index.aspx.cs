using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.Web.Security;

namespace CSF.CITASWEB.WS.BackEnd
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["TipoDocumento"].ToString() + "_" + Session["NumeroDocumento"]
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            List<string> varCredenciales = ConfigurationManager.AppSettings["usuariosValidos"].Split('|').ToList<string>();
            bool usuarioEncontrado = false;
            foreach (string credencial in varCredenciales)
            {
                if (credencial == (txtUsuario.Text + "," + txtPassword.Text))
                {
                    usuarioEncontrado = true;
                    break;
                }
            }
            if (usuarioEncontrado)
            {
                Session["TipoDocumento"] = "1";
                Session["NumeroDocumento"] = txtUsuario.Text;
                FormsAuthentication.RedirectFromLoginPage(txtUsuario.Text, false);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alerta", "alert('El usuario o password ingresados son incorrectos');", true);
            }
        }


        static string Encriptar(string cadena)
        {
            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(cadena), 0, Encoding.UTF8.GetByteCount(cadena));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}