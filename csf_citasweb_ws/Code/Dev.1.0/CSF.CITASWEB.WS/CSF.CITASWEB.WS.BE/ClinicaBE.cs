using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CSF.CITASWEB.WS.BE
{
    [DataContract]
    public class ClinicaSimpleBE
    {
        [DataMember]
        public string idClinica { get; set; }
        [DataMember]
        public string nombre { get; set; }

        public string codigoSunasa { get; set; }

        public string ruc { get; set; }
        [DataMember]
        public bool indicadorSedeNueva { get; set; }
        [DataMember]
        public bool indicadorVirtual { get; set; }
        [DataMember]
        public string direccion { get; set; }
    }



    [DataContract]
    public class CiudadClinicaBE
    {
        [DataMember]
        public string ciudad { get; set; }
        [DataMember]
        public List<ClinicaBE> listaCiudades { get; set; }
    }

    [DataContract]
    public class ClinicaBE
    {
        public string categoria { get; set; }
        [DataMember]
        public string idClinica { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string ciudad { get; set; }
        [DataMember]
        public string foto { get; set; }
        [DataMember]
        public string horariosAtencion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string latitud { get; set; }
        [DataMember]
        public string longitud { get; set; }
        [DataMember]
        public string soloLlamadas { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
    }
    [DataContract]
    public class ClinicaEspecialidadAgrupadoBE
    {
        [DataMember]
        public int idEspecialidad { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string genero { get; set; }
        [DataMember]
        public int edadMin { get; set; }
        [DataMember]
        public int edadMax { get; set; }
        [DataMember]
        public string icono { get; set; }
        [DataMember]
        public int idEspecialidadPadre { get; set; }

        [DataMember]
        public List<ClinicaEspecialidadAgrupadoDetalleBE> detalle { get; set; }

    }
    [DataContract]
    public class ClinicaEspecialidadAgrupadoDetalleBE
    {
        [DataMember]
        public int idEspecialidad { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string genero { get; set; }
        [DataMember]
        public int edadMin { get; set; }
        [DataMember]
        public int edadMax { get; set; }
        [DataMember]
        public string icono { get; set; }
        [DataMember]
        public int idEspecialidadPadre { get; set; }

    }

    [DataContract]
    public class ClinicaConsultorioBE
    {
        [DataMember]
        public CCClinicaBE clinica { get; set; }
        [DataMember]
        public CCConsultorioBE consultorio { get; set; }
        [DataMember]
        public CCTextoInformativoBE textoInformativo { get; set; }
        [DataMember]
        public CCHorarioBE horario { get; set; }
        public ClinicaConsultorioBE()
        {
            clinica = new CCClinicaBE();
            consultorio = new CCConsultorioBE();
            textoInformativo = new CCTextoInformativoBE();
            horario = new CCHorarioBE();
        }
    }

    [DataContract]
    public class CCClinicaBE
    {
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string latitud { get; set; }
        [DataMember]
        public string longitud { get; set; }
    }
    [DataContract]
    public class CCConsultorioBE
    {
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string foto { get; set; }
        [DataMember]
        public string ubicacion { get; set; }
    }
    [DataContract]
    public class CCTextoInformativoBE
    {
        [DataMember]
        public string titulo { get; set; }
        [DataMember]
        public string[] contenido { get; set; }
    }
    [DataContract]
    public class CCHorarioBE
    {
        [DataMember]
        public string horaInicio { get; set; }
        [DataMember]
        public string horaFin { get; set; }
        [DataMember]
        public string rangoHorario { get; set; }
    }
}
