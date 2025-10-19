using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Configuration;

namespace CSF.CITASWEB.WS.BE
{
    [DataContract]
    public class QR_RespuestaBE<T>
    {
        [DataMember]
        public bool success { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public T result { get; set; }
    }

    [DataContract]
    public class QR_RespuestaSimpleBE
    {
        [DataMember]
        public bool success { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public string result { get; set; }
    }
}
