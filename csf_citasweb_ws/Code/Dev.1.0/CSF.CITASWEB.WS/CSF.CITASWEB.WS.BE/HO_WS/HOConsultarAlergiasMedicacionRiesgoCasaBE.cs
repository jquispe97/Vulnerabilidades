using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.CITASWEB.WS.BE
{
    public class HOConsultarAlergiasRiesgosBE
    {
        public string ApiKey { get; set; }
        public string idAmbulatorio { get; set; }
        public string codAtencion { get; set; }
    }
    public class HOConsultarAlergiasRiesgosResponseBE
    {
        public string indAlergia { get; set; }
        public string hospitalizado { get; set; }
        public string representante { get; set; }
        public string habitacion { get; set; }
        public List<HOCAMMedicamentoBE> medicamentos { get; set; }
        public string alimentos { get; set; }
        public string otros { get; set; }
        public List<HOCAMMedicamentoRiesgoBE> medicamentosRiesgos { get; set; }
        public int idRegistro { get; set; }
        public HOConsultarAlergiasRiesgosResponseBE()
        {
            medicamentos = new List<HOCAMMedicamentoBE>();
            medicamentosRiesgos = new List<HOCAMMedicamentoRiesgoBE>();
        }
    }
    public class HOCAMMedicamentoBE
    {
        public int IdeAlergiaMedicamentosDet { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }
    public class HOCAMMedicamentoRiesgoBE
    {
        public string IdeMedicacionDet { get; set; }
        public int Codigo { get; set; }
        public string Indicador { get; set; }
        public string DscItem { get; set; }
        public string DscSubItem { get; set; }
    }
    //
    public class HOConsultarAlergiasRiesgosPresentacionBE
    {
        public string indAlergia { get; set; }
        public string hospitalizado { get; set; }
        public string representante { get; set; }
        public string habitacion { get; set; }
        public List<HOCAMMedicamentoPresentacionBE> medicamentos { get; set; }
        public string alimentos { get; set; }
        public string otros { get; set; }
        public List<HOCAMMedicamentoRiesgoPresentacionBE> medicamentosRiesgos { get; set; }
        public string idRegistro { get; set; }
        public HOConsultarAlergiasRiesgosPresentacionBE()
        {
            medicamentos = new List<HOCAMMedicamentoPresentacionBE>();
            medicamentosRiesgos = new List<HOCAMMedicamentoRiesgoPresentacionBE>();
        }
    }
    public class HOCAMMedicamentoPresentacionBE
    {
        public string IdeAlergiaMedicamentosDet { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }
    public class HOCAMMedicamentoRiesgoPresentacionBE
    {
        public string IdeMedicacionDet { get; set; }
        public string Codigo { get; set; }
        public string Indicador { get; set; }
        public string DscItem { get; set; }
        public string DscSubItem { get; set; }
    }
}
