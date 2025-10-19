using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CSF.CITASWEB.WS.BatchWhatsApp.Clases
{
    public static class ClasesGenericas
    {
        public static string ObtenerFormatoFecha(DateTime oFecha)
        {
            string valor = "";
            if (oFecha != null)
            {
                string[] aSemana = new string[] {
                    "lunes", "martes", "miércoles",
                    "jueves", "viernes", "sábado",
                    "domingo"
                };
                string[] aMes = new string[] {
                    "enero", "febrero", "marzo",
                    "abril", "mayo", "junio",
                    "julio", "agosto", "setiembre",
                    "octubre", "noviembre", "diciembre"
                };
                string fechaHora = oFecha.ToString("dd/MM/yyyy HH:mm");
                string[] aFechaHora = fechaHora.Split(' ');
                string[] aFecha = aFechaHora[0].Split('/');
                //string[] aHora = aFechaHora[0].Split(':');
                int diaSemana = (int)oFecha.DayOfWeek;
                diaSemana = diaSemana == 0 ? 7 : diaSemana;
                valor = aSemana[diaSemana - 1].ToString();
                valor += " " + aFecha[0] + " de ";
                valor += aMes[((int)oFecha.Month) - 1];
            }
            return valor;
        }
        public static string ObtenerFormatoHora(string strHora, bool es24Horas = false)
        {
            string valor = "";
            if (!String.IsNullOrEmpty(strHora))
            {
                string[] aHora = strHora.Split(':');
                int hora = int.Parse(aHora[0]);
                if (es24Horas)
                {
                    //valor = hora.ToString().PadLeft(2, '0') + ":" + aHora[1];
                    valor = strHora;
                } 
                else
                {
                    string sufijo = hora >= 12 ? "pm" : "am";
                    if (hora >= 13)
                    {
                        hora = hora - 12;
                    }
                    valor = hora.ToString().PadLeft(2, '0') + ":" + aHora[1] + " " + sufijo;
                }
            }
            return valor;
        }
        public static string GetSetting(string key)
        {
            string value = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value.ToString();
            }
            return "";
        }
        public static string GetCapitalize(string texto, bool soloPrimeraPalabra = false)
        {
            List<String> lstPalabra = new List<String>();
            if (!String.IsNullOrWhiteSpace(texto))
            {
                texto = texto.Trim().ToLower();
                string[] lPalabra = texto.Split(' ');
                int i = 0, nPalabras = lPalabra.Length;
                string primerCaracter, palabraTmp;
                for (; i < nPalabras; i++)
                {
                    primerCaracter = lPalabra[i].Substring(0, 1).ToUpper();
                    palabraTmp = primerCaracter;
                    if (lPalabra[i].Length > 1)
                    {
                        palabraTmp += lPalabra[i].Substring(1, lPalabra[i].Length - 1);
                    }
                    lstPalabra.Add(palabraTmp);
                    if (soloPrimeraPalabra)
                    {
                        break;
                    }
                }
            }
            return String.Join(" ", lstPalabra.ToArray());
        }
    }
}
