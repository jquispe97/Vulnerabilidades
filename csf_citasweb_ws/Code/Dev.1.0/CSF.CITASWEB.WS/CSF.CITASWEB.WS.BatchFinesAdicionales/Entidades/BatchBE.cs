using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSF.CITASWEB.WS.BatchFinesAdicionales
{
    public class NotificacionBE
    {
        public int IDReporteEnvio { get; set; }
        public DateTime FechaInstalacion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int CantidadDiasRango { get; set; }
        public string Asunto { get; set; }
        public string Cuerpo { get; set; }
        public string CorreosPara { get; set; }
        public string CorreosCC { get; set; }
        public string CorreosCCO { get; set; }
        public string Reporte { get; set; }
    }
}
