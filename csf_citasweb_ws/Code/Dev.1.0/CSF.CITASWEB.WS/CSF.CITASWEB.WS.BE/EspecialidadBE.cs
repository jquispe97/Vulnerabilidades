using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CSF.CITASWEB.WS.BE
{
    [DataContract]
    public class EspecialidadBE
    {
        [DataMember]
        public string idEspecialidad { get; set; }
        [DataMember]
        public string especialidad { get; set; }
        [DataMember]
        public string genero { get; set; }
        [DataMember]
        public string edadMin { get; set; }
        [DataMember]
        public string edadMax { get; set; }
        [DataMember]
        public string idSubEspecialidad { get; set; }
        [DataMember]
        public string icono { get; set; }
    }

    [DataContract]
    public class EspecialidadFrecuenteBE
    {
        [DataMember]
        public string idEspecialidad { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string genero { get; set; }
        [DataMember]
        public string edadMin { get; set; }
        [DataMember]
        public string edadMax { get; set; }
        [DataMember]
        public int indicadorFrecuente { get; set; }
        [DataMember]
        public int cantidadCitasAgendadas { get; set; }
        [DataMember]
        public string fechaUltimaCita { get; set; }
    }
}
