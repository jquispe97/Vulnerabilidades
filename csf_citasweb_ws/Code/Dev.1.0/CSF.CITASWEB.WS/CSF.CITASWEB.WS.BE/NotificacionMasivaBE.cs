using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSF.CITASWEB.WS.BE
{
    public class NotificacionMasivaBE
    {
        public int IDAppNotificacionMasiva { get; set; }
        public string FechaRegistro { get; set; }
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
        public string Tipo { get; set; }
        public string FueEnviado { get; set; }
    }
}
