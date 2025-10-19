using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CSF.CITASWEB.WS.BE
{
    public class OTPBE
    {
        public int idOTP { get; set; }
        public DateTime FechaHoraFin { get; set; }
        public string Correo { get; set; }
        public bool IndicadorValidado { get; set; }
    }
    [DataContract]
    public class EnviarOTPBE
    {
        public string CodigoOTP { get; set; }
        [DataMember]
        public string FechaHoraInicio { get; set; }
        [DataMember]
        public string FechaHoraFin { get; set; }
        [DataMember]
        public int TiempoVigencia { get; set; }
        public string IdOTP { get; set; }
    }
    [DataContract]
    public class ActualizarOTPBE
    {
        //public string CodigoOTP { get; set; }
        [DataMember]
        public string FechaHoraInicio { get; set; }
        [DataMember]
        public string FechaHoraFin { get; set; }
        [DataMember]
        public int TiempoVigencia { get; set; }
        public bool IndicadorValidado { get; set; }
    }
}
