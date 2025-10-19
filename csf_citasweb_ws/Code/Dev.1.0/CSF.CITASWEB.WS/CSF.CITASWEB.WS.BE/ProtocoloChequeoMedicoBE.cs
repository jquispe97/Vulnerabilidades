using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CSF.CITASWEB.WS.BE
{
    [DataContract]
    public class ProtocoloChequeoMedicoBE
    {
        [DataMember]
        public List<ProtocoloBE> detalleProtocolo { get; set; }

        [DataMember]
        public string error { get; set; }
        [DataMember]
        public string errorDetalle { get; set; }
        [DataMember]
        public string turno { get; set; }
        [DataMember]
        public string horario { get; set; }

    }
    [DataContract]
    public class ProtocoloBE {

        [DataMember]
        public string codigoProducto { get; set; }

        [DataMember]
        public string descripcionProducto { get; set; }
        [DataMember]
        public string codigoParentesco { get; set; }

        [DataMember]
        public string parentesco { get; set; }
        [DataMember]
        public string contratante { get; set; }
        [DataMember]
        public string codigoEstado { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string codigoAsegurado { get; set; }

        [DataMember]
        public string rucContratante { get; set; }
        [DataMember]
        public List<Planes> detallePlanes { get; set; }

    }
    [DataContract]
    public class Planes {

        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string codigo { get; set; }

        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public List<Componentes> detalleComponentes {get;set;}

    }

    [DataContract]
    public class Componentes
    {


        [DataMember]
        public string codigo { get; set; }

        [DataMember]
        public string descripcion { get; set; }

    }


}
