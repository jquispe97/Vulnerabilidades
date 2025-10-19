using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.CITASWEB.WS.BE
{
    public class HOGrabarAlergiasRiesgosBE
    {
        public string ApiKey { get; set; }
        public string idAmbulatorio { get; set; }
        public string codAtencion { get; set; }
        public string indAlergia { get; set; } //Si tiene alergia enviar "S" de lo contrario enviar "N"
        public string hospitalizado { get; set; } //Enviar vacío
        public string representante { get; set; } //Enviar vacío
        public string habitacion { get; set; } //Enviar vacío
        public List<HOGRAMedicamentoBE> medicamentos { get; set; }
        public string alimentos { get; set; }
        public string otros { get; set; }
        public List<HOGRAMedicamentoRiesgoBE> medicamentosRiesgos { get; set; }
        public string documentoAlergias { get; set; }
        public string documentoRiesgos { get; set; }
        public string accion { get; set; } //Si es 0 enviar "I" (Insertar) de lo contrario enviar "A" (Actualizar)
        public int IdRegistro { get; set; } //Si es 0 es registro nuevo de lo contrario es uno existente
        public string RespEnvio { get; set; } //Enviar vacío
        public HOGrabarAlergiasRiesgosBE()
        {
            medicamentos = new List<HOGRAMedicamentoBE>();
            medicamentosRiesgos = new List<HOGRAMedicamentoRiesgoBE>();
        }
    }
    public class HOGRAMedicamentoBE
    {
        public int IdeAlergiaMedicamentosDet { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }
    public class HOGRAMedicamentoRiesgoBE
    {
        public string IdeMedicacionDet { get; set; } //Identificador del registro guardado
        public int Codigo { get; set; } //Identificador del riesgo
        public string Indicador { get; set; } //Si está marcado entonces enviar "S" de lo contrario enviar "N"
        public string DscItem { get; set; }
        public string DscSubItem { get; set; }
    }
    public class HOGRAMedicamentoPresentacionBE
    {
        public string IdeAlergiaMedicamentosDet { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }
    public class HOGRAMedicamentoRiesgoPresentacionBE
    {
        public string IdeMedicacionDet { get; set; } //Identificador del registro guardado
        public string Codigo { get; set; } //Identificador del riesgo
        public string Indicador { get; set; } //Si está marcado entonces enviar "S" de lo contrario enviar "N"
        public string DscItem { get; set; }
        public string DscSubItem { get; set; }
    }
    #region Response
    public class HOGrabarAlergiasRiesgosResponseBE 
    {
        public string codEstado { get; set; }
        public string desEstado { get; set; }
    }
    public class HOGrabarAlergiasRiesgosPresentacionBE
    {
        public string codEstado { get; set; }
        public string desEstado { get; set; }
    }
    #endregion
}
