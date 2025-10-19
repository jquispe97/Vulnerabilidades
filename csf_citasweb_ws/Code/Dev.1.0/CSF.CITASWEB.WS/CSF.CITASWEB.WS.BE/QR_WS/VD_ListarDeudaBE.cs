using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.CITASWEB.WS.BE
{
    public class VD_ListarDeudaBE
    {
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string TipoMensaje { get; set; }
    }
    public class VD_ListarDeudaResponseBE
    {
        public string flg_bloquearatencion { get; set; }
        public string mensaje { get; set; }
    }
    public class VD_ListarDeudaResponsePresBE
    {
        public string indicadorBloqueo { get; set; }
        public string mensaje { get; set; }
    }
}
