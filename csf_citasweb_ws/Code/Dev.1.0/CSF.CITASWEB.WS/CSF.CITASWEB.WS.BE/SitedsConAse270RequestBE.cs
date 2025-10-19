using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.CITASWEB.WS.BE
{
   public class SitedsConAse270RequestBE
    {

        public string NoTransaccion { get; set; }
        public string IdRemitente { get; set; }
        public string IdReceptor { get; set; }
        public string FeTransaccion { get; set; }
        public string HoTransaccion { get; set; }
        public string IdCorrelativo { get; set; }
        public string IdTransaccion { get; set; }
        public string TiFinalidad { get; set; }
        public string CaRemitente { get; set; }
        public string NuRucRemitente { get; set; }
        public string TxRequest { get; set; }
        public string CaReceptor { get; set; }
        public string TiDocumento { get; set; }
        public string NuDocumento { get; set; }

        public string CaPaciente { get; set; }
        public string IdReContratante { get; set; }
        public string ApPaternoPaciente { get; set; }
        public string ApMaternoPaciente { get; set; }
        public string CoAfPaciente { get; set; }
        public string CoParentesco { get; set; }
        public string CoProducto { get; set; }
        public string NoContratante { get; set; }
        public string CoReContratante { get; set; }
        public string NoMaContratante { get; set; }
        public string NoPaContratante { get; set; }
        public string NoPaciente { get; set; }
        public string NuPlan { get; set; }
        public string TiCaContratante { get; set; }
        public string TiDoContratante { get; set; }

        public string DeProducto { get; set; }
        public string CoInProducto { get; set; }

        public string CoEspecialidad { get; set; }

        public string NuCobertura { get; set; }
        public string BeMaxInicial { get; set; }
        public string CoTiCobertura { get; set; }
        public string CoSuTiCobertura { get; set; }
    }
}
