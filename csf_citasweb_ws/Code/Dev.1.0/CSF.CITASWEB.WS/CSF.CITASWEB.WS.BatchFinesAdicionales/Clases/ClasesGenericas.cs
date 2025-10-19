using System;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Configuration;

namespace CSF.CITASWEB.WS.BatchFinesAdicionales
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
        public static string AnioMesDia(string texto, string extension = "")
        {
            DateTime now = DateTime.Now;
            return $"{texto}_{now.Year}_{now.Month.ToString().PadLeft(2, '0')}_{now.Day.ToString().PadLeft(2, '0')}{extension}";
        }
        public static bool EnviarCorreo(CorreoBE oCorreoBE)
        {
            bool flag = false;
            string str = $"{GetSetting("_RutaLog")}{AnioMesDia(GetSetting("_NombreLog"), ".txt")}";
            FileStream fs = null;
            List<FileStream> lstFS = new List<FileStream>();

            try
            {
                int num;
                oCorreoBE.Servidor = oCorreoBE.Servidor ?? GetSetting("_CorreoServidor");
                oCorreoBE.Puerto = oCorreoBE.Puerto ?? GetSetting("_CorreoPuerto");
                oCorreoBE.De = oCorreoBE.De ?? GetSetting("_CorreoDe");
                oCorreoBE.Clave = oCorreoBE.Clave ?? GetSetting("_CorreoClave");
                oCorreoBE.Asunto = oCorreoBE.Asunto ?? "(Sin asunto)";//GetSetting("_CorreoAsunto");
                oCorreoBE.Contenido = oCorreoBE.Contenido ?? "(Sin cuerpo)";//GetSetting("_CorreoContenido");
                oCorreoBE.AliasNombre = oCorreoBE.AliasNombre ?? GetSetting("_CorreoAliasNombre");
                oCorreoBE.AliasCuenta = oCorreoBE.AliasCuenta ?? GetSetting("_CorreoAliasCuenta");
                oCorreoBE.EsHTML = new bool?(!oCorreoBE.EsHTML.HasValue ? bool.Parse(GetSetting("_CorreoHabilitarCuerpoHtml")) : oCorreoBE.EsHTML.Value);
                oCorreoBE.EsSSL = new bool?(!oCorreoBE.EsSSL.HasValue ? bool.Parse(GetSetting("_CorreoHabilitarSSL")) : oCorreoBE.EsSSL.Value);
                MailMessage message = new MailMessage
                {
                    Subject = oCorreoBE.Asunto,
                    Body = oCorreoBE.Contenido,
                    IsBodyHtml = oCorreoBE.EsHTML.Value,
                    From = new MailAddress(oCorreoBE.AliasCuenta, oCorreoBE.AliasNombre),
                    Sender = new MailAddress(oCorreoBE.AliasCuenta, oCorreoBE.AliasNombre)
                };
                if ((oCorreoBE.Para != null) && (oCorreoBE.Para.Length > 0))
                {
                    foreach (string str2 in oCorreoBE.Para)
                    {
                        message.To.Add(new MailAddress(str2));
                    }
                }
                if ((oCorreoBE.CC != null) && (oCorreoBE.CC.Length > 0))
                {
                    foreach (string str3 in oCorreoBE.CC)
                    {
                        message.CC.Add(new MailAddress(str3));
                    }
                }
                if ((oCorreoBE.CCO != null) && (oCorreoBE.CCO.Length > 0))
                {
                    foreach (string str4 in oCorreoBE.CCO)
                    {
                        message.Bcc.Add(new MailAddress(str4));
                    }
                }

                if ((oCorreoBE.Archivo != null) && (oCorreoBE.Archivo.Length > 0))
                {
                    foreach (string str5 in oCorreoBE.Archivo)
                    {
                        //message.Attachments.Add(new Attachment(str5));
                        fs = new FileStream(str5, FileMode.Open, FileAccess.Read);
                        message.Attachments.Add(new Attachment(fs, Path.GetFileName(str5)));
                        lstFS.Add(fs);
                    }
                }
                SmtpClient client = new SmtpClient
                {
                    Host = oCorreoBE.Servidor
                };
                if (!int.TryParse(oCorreoBE.Puerto, out num))
                {
                    num = 0x19;
                }
                client.Port = num;
                client.EnableSsl = oCorreoBE.EsSSL.Value;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(oCorreoBE.De, oCorreoBE.Clave);
                client.Send(message);
                flag = true;
                foreach (FileStream Ofs in lstFS)
                {
                    Ofs.Close();
                }

            }
            catch (Exception exception)
            {
                ArchivoTexto<Exception>.GenerarArchivo(exception);
                if (lstFS.Count > 0)
                {
                    foreach (FileStream Ofs in lstFS)
                    {
                        Ofs.Close();
                    }
                }
            }
            return flag;

        }
    }
}
