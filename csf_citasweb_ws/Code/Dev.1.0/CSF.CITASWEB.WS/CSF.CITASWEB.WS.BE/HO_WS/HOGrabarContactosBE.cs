using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.CITASWEB.WS.BE
{
    public class HOGrabarContactosRequestBE
    {
        public string ApiKey { get; set; }
        public string idAmbulatorio { get; set; }
        public string codAtencion { get; set; }
        public List<HOGContactosPersonaReqBE> personas { get; set; }
        public string documento1 { get; set; }
        public string documento2 { get; set; }
        public string accion { get; set; } //Si es 0 enviar "I" (Insertar) de lo contrario enviar "A" (Actualizar)
        public int idRegistro { get; set; }
        public string ResponsableEnvio { get; set; } // Siempre vacío
    }
    public class HOGContactosPersonaReqBE
    {
        public int IdRegistroDet { get; set; }
        public string apellidos { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string nombres { get; set; }
        public string dni { get; set; }
        public string TipoDocumento { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string parentesco { get; set; }
        public string codparentesco { get; set; }
        public int nro_orden { get; set; } // 1, 2 o 3
        public int flg_copia { get; set; } // 0 no es copia, 1 es copia
    }
    public class HOGContactosPersonaReqPresentacionBE
    {
        public string idRegistroDet { get; set; }
        public string codTipoDocumento { get; set; }
        public string desTipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string nombres { get; set; }
        public string correo { get; set; }
        public string celular { get; set; }
        public string codParentesco { get; set; }
        public string desParentesco { get; set; }
        public string nro_orden { get; set; } // 1, 2 o 3
        public string flg_copia { get; set; } // 0 (No) o 1 (Sí)
    }
    #region Response
    public class HOGrabarContactosResponseBE
{
        public string codEstado { get; set; }
        public string desEstado { get; set; }
    }
    public class HOGrabarContactosPresentacionBE
{
        public string codEstado { get; set; }
        public string desEstado { get; set; }
    }
    #endregion
}
