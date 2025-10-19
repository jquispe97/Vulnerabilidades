using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.CITASWEB.WS.BE
{
    public class HOConsultarContactosBE
    {
        public string ApiKey { get; set; }
        public string idAmbulatorio { get; set; }
        public string codAtencion { get; set; }
    }
    #region Response
    public class HOConsultarContactosResponseBE
    {
        public string IdRegistroCab { get; set; }
        public List<HOCContactosPersonaResBE> Persona { get; set; }
    }
    public class HOCContactosPersonaResBE
    {
        public string IdRegistroDet { get; set; }
        public string apellidos { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string nombres { get; set; }
        public string dni { get; set; }
        public string TipoDocumento { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string codparentesco { get; set; }
        public string parentesco { get; set; }
        public string nro_orden { get; set; }
        public string flg_copia { get; set; }
    }
    public class HOConsultarContactosPresentacionBE
    {
        public string idRegistroCab { get; set; }
        public List<HOCContactosPersonaPreBE> personas { get; set; }
        public List<HOCCTextosPreBE> textos { get; set; }
        public HOConsultarContactosPresentacionBE ()
        {
            personas = new List<HOCContactosPersonaPreBE>();
            textos = new List<HOCCTextosPreBE>();
        }
    }
    public class HOCContactosPersonaPreBE
    {
        public string idRegistroDet { get; set; }
        public string codTipoDocumento { get; set; }
        //public string desTipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string nombres { get; set; }
        public string correo { get; set; }
        public string celular { get; set; }
        public string codParentesco { get; set; }
        public string desParentesco { get; set; }
        public string nro_orden { get; set; }
        public string flg_copia { get; set; }
        public string descripcionContacto { get; set; }
    }
    public class HOCCTextosPreBE
    {
        public string codigo { get; set; }
        public string descripcion { get; set; }
    }
    #endregion
}
