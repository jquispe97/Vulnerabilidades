using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.CITASWEB.WS.BE
{
    public class LB_ListarResultadosBE
    {
        public string numeroDocumento { get; set; }
        public string periodo { get; set; }
    }
    public class LB_ListarResultadosResponseBE
    {
        public string ordenAtencion { get; set; }
        public string codatencion { get; set; }
        public string fec_registra { get; set; }
        public string numDocumento { get; set; }
        public string nombrePaciente { get; set; }
        public string nombreExamen { get; set; }
        public bool esInformeResultado { get; set; }
        public bool esInformeHistorico { get; set; }
    }
    public class LB_ListarResultadosResPresentacionBE
    {
        public string ordenAtencion { get; set; }
        public string codigoAtencion { get; set; }
        public string fechaHoraAtencion { get; set; }
        public string numeroDocumentoPaciente { get; set; }
        public string nombrePaciente { get; set; }
        public string nombreExamen { get; set; }
        public bool esInformeResultado { get; set; }
        public bool esInformeHistorico { get; set; }
    }
}
