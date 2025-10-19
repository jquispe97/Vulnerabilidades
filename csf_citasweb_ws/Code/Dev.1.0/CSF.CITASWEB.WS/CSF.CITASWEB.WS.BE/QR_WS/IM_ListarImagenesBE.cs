using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.CITASWEB.WS.BE
{
    public class IM_ListarImagenesBE
    {

    }
    public class IM_ListarImagenesResponseBE
    {
        public string codatencion { get; set; }
        public string fec_registra { get; set; }
        public string nombrePaciente { get; set; }
        public string numDocumento { get; set; }
        public string nombreExamen { get; set; }
        public bool esInformeResultado { get; set; }
        public bool esImagenResultado { get; set; }
        public string idImagenResultado { get; set; }
        public string idInformeResultado { get; set; }
    }

    public class IM_ListarImagenesResPresentacionBE
    {
        public string codigoAtencion { get; set; }
        public string fechaHoraAtencion { get; set; }
        public string nombrePaciente { get; set; }
        public string numeroDocumentoPaciente { get; set; }
        public string nombreExamen { get; set; }
        public bool esInformeResultado { get; set; }
        public bool esImagenResultado { get; set; }
        public string idImagenResultado { get; set; }
        public string idInformeResultado { get; set; }
    }
}
