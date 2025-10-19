using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSF.CITASWEB.WS.BE
{
    public class MantMedicoSubEspecialidadBE
    {
        public string CMP { get; set; }
        public string NombreMedico { get; set; }
        public string IDClinica { get; set; }
        public string NombreClinica { get; set; }
        public string IDSubEspecialidad { get; set; }
        public string NombreEspecialidad { get; set; }
        public string TipoCitas { get; set; }
        public string EsTeleorientacion { get; set; }
        public string InformacionCita { get; set; }
    }
}
