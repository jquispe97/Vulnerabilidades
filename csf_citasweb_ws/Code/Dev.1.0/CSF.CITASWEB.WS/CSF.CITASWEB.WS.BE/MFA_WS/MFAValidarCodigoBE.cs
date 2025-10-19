using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.CITASWEB.WS.BE
{
    public class MFAValidarCodigoBE
    {
        public string codigoEmpresa { get; set; }
        public string codigoAplicativo { get; set; }
        public string usuario { get; set; }
        public string dispositivo { get; set; }
        public string codigoMFA { get; set; }
    }
    public class MFAValidarCodigoResponseBE
    {
        public string codigo { get; set; }
        public string mensaje { get; set; }
        public string indicadorBloqueado { get; set; }
        public string expiraBloqueo { get; set; }
    }
}
