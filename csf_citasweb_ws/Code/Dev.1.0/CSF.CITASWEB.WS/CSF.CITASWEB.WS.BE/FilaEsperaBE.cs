using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CSF.CITASWEB.WS.BE
{
    public class FilaEsperaBE
    {
        [DataMember]
        public string turno { get; set; }
        [DataMember]
        public string paciente { get; set; }
        [DataMember]
        public string horaCita { get; set; }
        [DataMember]
        public int idEstado { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public bool pacienteCita { get; set; }
        [DataMember]
        public string codigoTipoPaciente { get; set; }
        [DataMember]
        public string tipoPaciente { get; set; }
        [DataMember]
        public string colorEstado { get; set; }
    }

    public class FilaEsperaDetalleBE
    {
        [DataMember]
        public List<FilaEsperaBE> fila { get; set; }
        [DataMember]
        public string consultorio { get; set; }

    }
}
