using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.CITASWEB.WS.BE
{
    public class MFAAutenticarBE
    {
        public string codigoEmpresa { get; set; }
        public string codigoAplicativo { get; set; }
        public string clave { get; set; }
        public string usuario { get; set; }
        public string dispositivo { get; set; }
    }
    public class MFAAutenticarResponseBE
    {
        public string codigo { get; set; }
        public string mensaje { get; set; }
        public string expira { get; set; }
        public string token { get; set; }
    }
}
