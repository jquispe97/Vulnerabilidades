using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.CITASWEB.WS.BE
{
    public class MFAEnviarCodigoBE
    {
        public string codigoEmpresa { get; set; }
        public string codigoAplicativo { get; set; }
        public string usuario { get; set; }
        public string dispositivo { get; set; }
        public string viaEnvio { get; set; }
        public string identificadorEquipo { get; set; }
        public string sistemaOperativo { get; set; }
        public string hashDispositivo { get; set; }
    }
    public class MFAEnviarCodigoResponseBE
    {
        public string codigo { get; set; }
        public string mensaje { get; set; }
        public string expira { get; set; }
        public string minutosVigencia { get; set; }
    }
}
