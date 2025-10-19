using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CSF.CITASWEB.WS.BE
{
    [DataContract]
    public class PreguntaFrecuenteBE
    {
        [DataMember]
        public string IDPreguntaFrecuente { get; set; }
        [DataMember]
        public string Pregunta { get; set; }
        [DataMember]
        public string Respuesta { get; set; }
    }
    [DataContract]
    public class PreguntaTriajeBE
    {
        [DataMember]
        public string idPreguntaTriaje { get; set; }
        [DataMember]
        public string pregunta { get; set; }
        [DataMember]
        public string tipoRespuesta { get; set; }
        [DataMember]
        public bool mandatorio { get; set; }
    }
    [DataContract]
    public class EstadoTriaje
    {
        [DataMember]
        public bool usuarioRestringido { get; set; }
        [DataMember]
        public List<PreguntaTriajeBE> preguntas { get; set; }
    }
    [DataContract]
    public class RespuestaTriaje
    {
        [DataMember]
        public string idPregunta { get; set; }
        [DataMember]
        public string respuesta { get; set; }
    }
}
