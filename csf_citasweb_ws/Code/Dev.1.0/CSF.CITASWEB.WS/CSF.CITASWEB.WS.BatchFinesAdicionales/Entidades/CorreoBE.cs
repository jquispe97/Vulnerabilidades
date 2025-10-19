using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.CITASWEB.WS.BatchFinesAdicionales
{
    public class CorreoBE
    {
        public string AliasCuenta { get; set; }
        public string AliasNombre { get; set; }
        public string[] Archivo { get; set; }
        public string Asunto { get; set; }
        public string[] CC { get; set; }
        public string[] CCO { get; set; }
        public string Clave { get; set; }
        public string Contenido { get; set; }
        public string De { get; set; }
        public bool? EsHTML { get; set; }
        public bool? EsSSL { get; set; }
        public string[] Para { get; set; }
        public string Puerto { get; set; }
        public string Servidor { get; set; }
    }
}
