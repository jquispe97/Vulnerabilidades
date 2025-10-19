using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Configuration;

namespace CSF.CITASWEB.WS.BE
{
    [DataContract]
    public class RespuestaBE<T>
    {
        [DataMember]
        public int rpt { get; set; }
        [DataMember]
        public string mensaje { get; set; }
        [DataMember]
        public T data { get; set; }

        /*public RespuestaBE<T> ErrorPorDefecto(Exception ex)
        {
            if (ex.Message.StartsWith("INFO:"))
            {
                return new RespuestaBE<T>()
                {
                    rpt = 0,
                    mensaje = ex.Message.Replace("INFO:", ""),
                    data = default(T)
                };
            }
            else if (ex.Message.StartsWith("ERRFU:"))
            {
                return new RespuestaBE<T>()
                {
                    rpt = 1,
                    mensaje = ex.Message.Replace("ERRFU:", ""),
                    data = default(T)
                };
            }
            else
            {
                int varIDError = 0;

                //varIDError = 
                return new RespuestaBE<T>()
                {
                    rpt = 999,
                    mensaje = "Ocurrio un error al ejecutar el método." + ((ConfigurationManager.AppSettings["MostrarIDError"].ToString() == "1") ? ("\nIdentificador del error: " + varIDError.ToString()) : ""),
                    data = default(T)
                };
            }
        }*/
    }

    [DataContract]
    public class RespuestaSimpleBE
    {
        [DataMember]
        public int rpt { get; set; }
        [DataMember]
        public string mensaje { get; set; }
        [DataMember]
        public string data { get; set; }

        /*public RespuestaSimpleBE ErrorPorDefecto(Exception ex)
        {
            if (ex.Message.StartsWith("INFO:"))
            {
                return new RespuestaSimpleBE()
                {
                    rpt = 0,
                    mensaje = ex.Message.Replace("INFO:", ""),
                    data = null
                };
            }
            else if (ex.Message.StartsWith("ERRFU:"))
            {
                return new RespuestaSimpleBE()
                {
                    rpt = 1,
                    mensaje = ex.Message.Replace("ERRFU:", ""),
                    data = null
                };
            }
            else
            {
                return new RespuestaSimpleBE()
                {
                    rpt = 999,
                    mensaje = "Ocurrio un error al ejecutar el método.[ModoDebug]\nDetalle: " + ex.Message + "\nUbicacion: " + ex.StackTrace,
                    data = null
                };
            }
        }*/
    }
}
