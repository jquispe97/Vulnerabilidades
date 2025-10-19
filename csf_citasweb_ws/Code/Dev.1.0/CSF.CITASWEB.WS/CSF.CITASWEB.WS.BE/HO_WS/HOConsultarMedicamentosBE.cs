using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.CITASWEB.WS.BE
{
    #region Integración con CSF
    public class HOConsultarMedicamentosBE
    {
        public string ApiKey { get; set; }
        public string Descripcion { get; set; }
    }
    public class HOConsultarMedicamentosResponseBE
    {
        public int IdeAlergiaMedicamentosDet { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }
    #endregion
    public class HOConsultarMedicamentosPresentacionBE
    {
        public string IdeAlergiaMedicamentosDet { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }
}
