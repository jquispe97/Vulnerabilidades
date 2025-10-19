using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSF.CITASWEB.WS.BatchWhatsApp.conexion
{
    public class NotificacionBE
    {
        public int IDAppLogNotificacion { get; set; }
        public string TipoNotificacion { get; set; }
        public string TipoDispositivo { get; set; }
        public string TokenPush { get; set; }
        public string NombrePaciente { get; set; }
        public string ApellidoPaternoPaciente { get; set; }
        public string ApellidoMaternoPaciente { get; set; }
        public string TelefonoPaciente { get; set; }
        public string EmailPaciente { get; set; }
        public string NombreMedico { get; set; }
        public string Especialidad { get; set; }
        public string Clinica { get; set; }
        public DateTime FechaCita { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string IDCita { get; set; }
        public string IDCitaVirtual { get; set; }
        public bool EsCitaVirtual { get; set; }
        public string HoraCita { get; set; }
        public string GeneroMedico { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string EsPagada { get; set; }
        public string EsAdicional { get; set; }
        public string HoraInicioHorario { get; set; }
        public string HoraFinHorario { get; set; }
        public string ConfiguracionPlantillaHSM { get; set; }
    }
}
