using System.Runtime.Serialization;

namespace CSF.CITASWEB.WS.BE
{
    [DataContract]
    public class beRespuestaImagen
    {
        [DataMember]
        public string Codigo { get; set; }
        [DataMember]
        public string Respuesta { get; set; }
    }
}
