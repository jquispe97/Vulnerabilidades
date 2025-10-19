using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.CITASWEB.WS.BE
{
    #region Integración con CSF
    public class HOConsultarMedicacionItemsBE
    {
        public string ApiKey { get; set; }
        public int Orden { get; set; } //Enviar el valor 1
    }
    public class HOConsultarMedicacionItemsResponseBE
    {
        public int IdeMedicacionItemsMae { get; set; }
        public string DscItem { get; set; }
        public string DscSubItem { get; set; }
    }
    #endregion
    public class HOConsultarMedicacionItemsPresentacionBE
    {
        public string IdeMedicacionItemsMae { get; set; }
        public string DscItem { get; set; }
        public string DscSubItem { get; set; }
    }
}
