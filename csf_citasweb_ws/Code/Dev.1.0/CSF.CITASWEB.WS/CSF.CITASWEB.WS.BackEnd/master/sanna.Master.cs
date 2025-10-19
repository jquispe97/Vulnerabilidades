using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace CSF.CITASWEB.WS.BackEnd.master
{
    public partial class sanna : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    lblUsuario.Text = Session["NumeroDocumento"].ToString();
                }
                catch (Exception)
                {
                    CerrarSesion();
                }
            }
        }

        protected void btnCerrarSesion_Click(object sender, ImageClickEventArgs e)
        {
            CerrarSesion();
        }

        private void CerrarSesion()
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}