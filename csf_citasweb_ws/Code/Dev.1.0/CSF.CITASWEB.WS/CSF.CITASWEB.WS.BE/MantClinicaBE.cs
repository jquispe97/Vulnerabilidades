using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSF.CITASWEB.WS.BE
{
    public class MantClinicaBE
    {
        public int IDClinica { get; set; }
        public string Nombre { get; set; }
        public string RUC { get; set; }
        public string RUCSunasa { get; set; }
        public string CodigoSunasa { get; set; }
        public int Tipo { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Foto { get; set; }
        public string Abreviatura { get; set; }
        public string HorariosAtencion { get; set; }
        public string Telefono { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public bool EstadoActivo { get; set; }
    }
}
