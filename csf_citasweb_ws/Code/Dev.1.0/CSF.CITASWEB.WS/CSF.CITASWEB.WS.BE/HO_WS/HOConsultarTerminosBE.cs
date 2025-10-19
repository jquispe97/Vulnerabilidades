using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.CITASWEB.WS.BE
{
    public class HOConsultarTerminosBE
    {
        public string ApiKey { get; set; }
        public string idAmbulatorio { get; set; }
        public string codAtencion { get; set; }
    }
    #region Response
    public class HOConsultarTerminosResponseBE
    {
        public string indTerminosAdmision { get; set; }
        public string idRegistro { get; set; }
        public string flg_autorizacion1 { get; set; }
        public string flg_autorizacion2 { get; set; }
    }
    public class HOConsultarTerminosPresentacionBE
    {
        public string indTerminosAdmision { get; set; }
        public string idRegistro { get; set; }
        public List<HOCTOpcionesPresentacionBE> opciones { get; set; }

        //public string flg_autorizacion1 { get; set; }
        //public string flg_autorizacion2 { get; set; }
    }
    public class HOCTOpcionesPresentacionBE
    {
        public string codigoRegistro { get; set; } // 01 o 02
        public string codigoBoton { get; set; } // S (Si) o N (No)
    }
    #endregion
}
