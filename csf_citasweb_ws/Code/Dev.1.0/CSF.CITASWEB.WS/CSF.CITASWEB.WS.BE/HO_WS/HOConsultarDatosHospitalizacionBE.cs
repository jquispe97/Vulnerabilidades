using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.CITASWEB.WS.BE
{
    public class HOConsultarDatosHospitalizacionBE
    {
        public string ApiKey { get; set; }
        public string idAmbulatorio { get; set; }
    }
    public class HOConsultarDatosHospitalizacionResponseBE
    {
        public string codAtencion { get; set; }
        public string fechaHora { get; set; }
        public string idAmbulatorio { get; set; }
        public string grupoFamiliar { get; set; }
        public string codTipoDocumento { get; set; }
        public string desTipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string fechaNacimiento { get; set; }
        public string nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string sexo { get; set; }
        public string codMedico { get; set; }
        public string nomMedico { get; set; }
        public string codEspecialidad { get; set; }
        public string desEspecialidad { get; set; }
        public string nivelAtencion { get; set; }
        public string codEstado { get; set; }
        public string desEstado { get; set; }
        public string ultimoPaso { get; set; }
        public string codFamiliar { get; set; }
        public string desFamiliar { get; set; }
    }
    public class HOConsultarDatosHospitalizacionPresentacionBE
    {
        public string codAtencion { get; set; }
        public string fechaHora { get; set; }
        public string idAmbulatorio { get; set; }
        public string grupoFamiliar { get; set; }
        public string codTipoDocumento { get; set; }
        public string desTipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string fechaNacimiento { get; set; }
        public string nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string sexo { get; set; }
        public string codMedico { get; set; }
        public string nomMedico { get; set; }
        public string codEspecialidad { get; set; }
        public string desEspecialidad { get; set; }
        public string nivelAtencion { get; set; }
        public string codEstado { get; set; }
        public string desEstado { get; set; }
        public string ultimoPaso { get; set; }
        public string codFamiliar { get; set; }
        public string desFamiliar { get; set; }
    }
}
