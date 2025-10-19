using System;
using System.Collections.Generic;
using System.Diagnostics;
using CSF.CITASWEB.WS.BatchWhatsApp.conexion;
using System.Net;
using System.IO;
using System.Configuration;
using CSF.CITASWEB.WS.BatchWhatsApp.Clases;
using System.Reflection;
using System.Linq;

namespace CSF.CITASWEB.WS.BatchWhatsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ErrorDA varError = new ErrorDA();
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
                {
                    varError.RegistrarError(new Exception("La aplicación ya está ejecutandose"), "Batch", "BatchWhatsApp");
                    return;
                }
                Debug.WriteLine("paso 0");
                List<NotificacionBE> varNotificaciones = new BatchDA().ListarNotificaciones();
                Debug.WriteLine("paso 1");

                varConexion.Conectar();
                Debug.WriteLine("paso 2");
                string varJson = "";
                string fechaAtencion = "", idCita = "", indicadorVirtual = "",
                    medico = "", data = "";
                int serviceId;// = int.Parse(ClasesGenericas.GetSetting("ServiceId"));
                string elementName,// = ClasesGenericas.GetSetting("ElementName"), 
                    @namespace,// = ClasesGenericas.GetSetting("Namespace"), 
                    language = ClasesGenericas.GetSetting("Language"),
                    prefijoTelefonico = ClasesGenericas.GetSetting("PrefijoTelefonico"),
                    phoneNumber, template;// = ClasesGenericas.GetSetting("Template");
                String[] aConfiguracionPlantillaHSM;
                String fechaHoraPresentacion;
                PropertyInfo[] aPropiedad = (new NotificacionBE()).GetType().GetProperties();
                List<String> lPropiedad = new List<String>();
                for (int i = 0; i < aPropiedad.Length; i++)
                {
                    lPropiedad.Add(aPropiedad[i].Name);
                }
                String[] aPropiedadPresentacion;
                List<String> lPropiedadPresentacion, lArgumento = new List<String>();
                object objPropiedad;
                String valorPropiedad, botones;
                int cPropiedad;
                bool indicadorBotones;
                foreach (NotificacionBE notificacion in varNotificaciones)
                {
                    Debug.WriteLine("paso 3");

                    #region [Inicio] - Plantilla base
                    //    int serviceId = 2;
                    //    string phoneNumber = "51929818514";
                    //    string elementName = "recordario_proxima_cita_test",
                    //        @namespace = "da1ff248_27e6_4b9c_ba6b_f50d34a36b9d",
                    //        language = "es_AR";

                    //    varJson = "{" +
                    //        "\"serviceId\": " + serviceId + "," +
                    //        "\"phoneNumber\": \"" + phoneNumber + "\"," +
                    //        "\"text\": null," +
                    //        "\"hsm\": {" +
                    //            "\"elementName\": \"" + elementName + "\"," +
                    //            "\"namespace\": \"" + @namespace + "\"," +
                    //            "\"language\": \"" + language + "\"," +
                    //            "\"header\": null," +
                    //            "\"body\": {" +
                    //                "\"text\": \"" + "¡Hola {{1}}, espero estés bien! Tienes una cita el {{2}} en {{3}} con el/la Médico/a {{4}} ({{5}}). Por favor, confirmar tu asistencia 👇" + "\"," +
                    //                "\"1\": \"" + "Eduardo" + "\"," +
                    //                "\"2\": \"" + "22 de septiembre - 15:00 pm" + "\"," +
                    //                "\"3\": \"" + "Camacho" + "\"," +
                    //                "\"4\": \"" + "Dra. Pepita Perez" + "\"," +
                    //                "\"5\": \"" + "Traumatología" + "\"" +
                    //            "}," +
                    //            "\"buttons\": [" +
                    //                "{" +
                    //                    "\"type\": \"" + "quick_reply" + "\"," +
                    //                    "\"index\": " + "0" + "," +
                    //                    "\"parameter\": {" +
                    //                        "\"button_1\": \"" + "id_de_cita,tipo_cita" + "\"" +
                    //                    "}" +
                    //                "}," +
                    //                "{" +
                    //                    "\"type\": \"" + "quick_reply" + "\"," +
                    //                    "\"index\": " + "1" + "," +
                    //                    "\"parameter\": {" +
                    //                        "\"button_2\": \"" + "id_de_cita,tipo_cita" + "\"" +
                    //                    "}" +
                    //                "}," +
                    //                "{" +
                    //                    "\"type\": \"" + "quick_reply" + "\"," +
                    //                    "\"index\": " + "2" + "," +
                    //                    "\"parameter\": {" +
                    //                        "\"button_3\": \"" + "id_de_cita,tipo_cita" + "\"" +
                    //                    "}" +
                    //                "}" +
                    //            "]" +
                    //        "}," +
                    //        "\"tags\": null," +
                    //        "\"sendHSMIfCaseOpenAnyways\": true," +
                    //        "\"validateDoNotCallList\": true" +
                    //    "}";
                    //}
                    #endregion [Fin] - Plantilla base

                    phoneNumber = prefijoTelefonico + notificacion.TelefonoPaciente;
                    aConfiguracionPlantillaHSM = notificacion.ConfiguracionPlantillaHSM.Split('|'); //ServiceId|Namespace|ElementName
                    serviceId = int.Parse(aConfiguracionPlantillaHSM[0]);
                    @namespace = aConfiguracionPlantillaHSM[1];
                    elementName = aConfiguracionPlantillaHSM[2];
                    aPropiedadPresentacion = !String.IsNullOrEmpty(aConfiguracionPlantillaHSM[3]) ? aConfiguracionPlantillaHSM[3].Split(',') : new String[0];
                    lPropiedadPresentacion = aPropiedadPresentacion.ToList();
                    indicadorBotones = aConfiguracionPlantillaHSM[4].Equals("true");

                    indicadorVirtual = notificacion.EsCitaVirtual ? "1" : "0";
                    idCita = notificacion.EsCitaVirtual ? notificacion.IDCitaVirtual : notificacion.IDCita;
                    data = idCita + "," + indicadorVirtual + "," + notificacion.TipoDocumento + "," + notificacion.NumeroDocumento;
                    medico = (notificacion.GeneroMedico.Equals("M") ? "el Dr. " : "la Dra. ") + ClasesGenericas.GetCapitalize(notificacion.NombreMedico);

                    notificacion.Clinica += (notificacion.EsCitaVirtual ? "/Virtual" : "/Presencial");

                    //ENVIAR DESDE BD
                    template = "¡Hola {{1}}, espero estés bien! Tienes una cita el {{2}} en {{3}} con {{4}} ({{5}}). Por favor, confirmar tu asistencia 👇";

                    //if (notificacion.EsAdicional.Equals("1"))
                    //{
                    //    fechaHoraPresentacion = ClasesGenericas.ObtenerFormatoFecha(notificacion.FechaCita) + " (" + ClasesGenericas.ObtenerFormatoHora(notificacion.HoraInicioHorario) + " - " + ClasesGenericas.ObtenerFormatoHora(notificacion.HoraFinHorario) + ")";
                    //}
                    //else
                    //{
                    //    fechaHoraPresentacion = ClasesGenericas.ObtenerFormatoFecha(notificacion.FechaCita) + " - " + ClasesGenericas.ObtenerFormatoHora(notificacion.HoraCita);
                    //}

                    cPropiedad = 0;
                    lArgumento = new List<string>();
                    for (int i = 0; i < lPropiedadPresentacion.Count; i++)
                    {
                        valorPropiedad = "";
                        switch (lPropiedadPresentacion[i])
                        {
                            case "Paciente":
                                valorPropiedad = ClasesGenericas.GetCapitalize(notificacion.NombrePaciente, true);
                                break;
                            case "Clinica":
                                valorPropiedad = notificacion.Clinica;
                                break;
                            case "Medico":
                                valorPropiedad = medico;
                                break;
                            case "Especialidad":
                                valorPropiedad = notificacion.Especialidad;
                                break;
                            case "FechaCita":
                                // lunes 01 de enero
                                valorPropiedad = ClasesGenericas.ObtenerFormatoFecha(notificacion.FechaCita);
                                break;
                            case "HoraCita":
                                // 08:00 am
                                valorPropiedad = ClasesGenericas.ObtenerFormatoHora(notificacion.HoraCita);
                                break;
                            case "FechaHoraCita":
                                if (notificacion.EsAdicional.Equals("1"))
                                {
                                    // lunes 01 de enero - Adicional de 08:00 - 13:00
                                    valorPropiedad = ClasesGenericas.ObtenerFormatoFecha(notificacion.FechaCita) + " - Adicional de " + ClasesGenericas.ObtenerFormatoHora(notificacion.HoraInicioHorario, true) + " - " + ClasesGenericas.ObtenerFormatoHora(notificacion.HoraFinHorario, true);
                                }
                                else
                                {
                                    // lunes 01 de enero - 08:00 am
                                    valorPropiedad = ClasesGenericas.ObtenerFormatoFecha(notificacion.FechaCita) + " - " + ClasesGenericas.ObtenerFormatoHora(notificacion.HoraCita);
                                }
                                break;
                            case "FechaCitaAdicional":
                                // lunes 01 de enero
                                valorPropiedad = ClasesGenericas.ObtenerFormatoFecha(notificacion.FechaCita);
                                break;
                            case "HoraCitaAdicional":
                                // 08:00 - 13:00
                                valorPropiedad = ClasesGenericas.ObtenerFormatoHora(notificacion.HoraInicioHorario, true) + " - " + ClasesGenericas.ObtenerFormatoHora(notificacion.HoraFinHorario, true);
                                break;
                            case "FechaHoraCitaAdicional":
                                // lunes 01 de enero - Adicional de 08:00 - 13:00
                                valorPropiedad = ClasesGenericas.ObtenerFormatoFecha(notificacion.FechaCita) + " - Adicional de " + ClasesGenericas.ObtenerFormatoHora(notificacion.HoraInicioHorario) + " - " + ClasesGenericas.ObtenerFormatoHora(notificacion.HoraFinHorario);
                                break;
                            default:
                                objPropiedad = notificacion.GetType().GetProperty(lPropiedadPresentacion[i]).GetValue(notificacion, null);
                                if (lPropiedad.IndexOf(lPropiedadPresentacion[i]) > -1)
                                {
                                    if (objPropiedad != null)
                                    {
                                        valorPropiedad = objPropiedad.ToString();
                                    }
                                    else
                                    {
                                        valorPropiedad = "";
                                    }
                                }
                                break;
                        }
                        cPropiedad++;
                        lArgumento.Add("\"" + (cPropiedad).ToString() + "\": \"" + valorPropiedad + "\"");
                        //"\"1\": \"" + ClasesGenericas.GetCapitalize(notificacion.NombrePaciente, true) + "\"," +
                    }
                    //notificacion[aPropiedad[0]];
                    /*
                        Modificar el template en este config no cambia el contenido, para modificar el contenido se debe acceder a la plataforma de Yoizen y cambiar el template
                    */

                    botones = "\"buttons\": null";
                    if (indicadorBotones)
                    {
                        botones = "\"buttons\": [" +
                                "{" +
                                    "\"type\": \"" + "quick_reply" + "\"," +
                                    "\"index\": " + "0" + "," +
                                    "\"parameter\": {" +
                                        //"\"button_1\": \"" + data + "\"" +
                                        //"\"button_1\": {" +
                                        //    "\"tipoDocumento\": \"" + notificacion.TipoDocumento + "\"," +
                                        //    "\"numeroDocumento\": \"" + notificacion.NumeroDocumento + "\"," +
                                        //    "\"idCita\": \"" + (notificacion.EsCitaVirtual ? "" : idCita) + "\"," +
                                        //    "\"idCitaVirtual\": \"" + (notificacion.EsCitaVirtual ? idCita : "") + "\"" +
                                        //"}" +
                                        "\"button_1\": \"{\\\"tipoDocumento\\\": \\\"" + notificacion.TipoDocumento + "\\\",\\\"numeroDocumento\\\": \\\"" + notificacion.NumeroDocumento + "\\\",\\\"idCita\\\": \\\"" + (notificacion.EsCitaVirtual ? "" : idCita) + "\\\",\\\"idCitaVirtual\\\": \\\"" + (notificacion.EsCitaVirtual ? idCita : "") + "\\\",\\\"esPagada\\\": \\\"" + notificacion.EsPagada + "\\\",\\\"esAdicional\\\": \\\"" + notificacion.EsAdicional + "\\\"}\"" +
                                    "}" +
                                "}," +
                                "{" +
                                    "\"type\": \"" + "quick_reply" + "\"," +
                                    "\"index\": " + "1" + "," +
                                    "\"parameter\": {" +
                                        //"\"button_2\": \"" + data + "\"" +
                                        //"\"button_2\": {" +
                                        //    "\"tipoDocumento\": \"" + notificacion.TipoDocumento + "\"," +
                                        //    "\"numeroDocumento\": \"" + notificacion.NumeroDocumento + "\"," +
                                        //    "\"idCita\": \"" + (notificacion.EsCitaVirtual ? "" : idCita) + "\"," +
                                        //    "\"idCitaVirtual\": \"" + (notificacion.EsCitaVirtual ? idCita : "") + "\"" +
                                        //"}" +
                                        "\"button_2\": \"{\\\"tipoDocumento\\\": \\\"" + notificacion.TipoDocumento + "\\\",\\\"numeroDocumento\\\": \\\"" + notificacion.NumeroDocumento + "\\\",\\\"idCita\\\": \\\"" + (notificacion.EsCitaVirtual ? "" : idCita) + "\\\",\\\"idCitaVirtual\\\": \\\"" + (notificacion.EsCitaVirtual ? idCita : "") + "\\\",\\\"esPagada\\\": \\\"" + notificacion.EsPagada + "\\\",\\\"esAdicional\\\": \\\"" + notificacion.EsAdicional + "\\\"}\"" +
                                    "}" +
                                "}," +
                                "{" +
                                    "\"type\": \"" + "quick_reply" + "\"," +
                                    "\"index\": " + "2" + "," +
                                    "\"parameter\": {" +
                                        //"\"button_3\": \"" + data + "\"" +
                                        //"\"button_3\": {" +
                                        //    "\"tipoDocumento\": \"" + notificacion.TipoDocumento + "\"," +
                                        //    "\"numeroDocumento\": \"" + notificacion.NumeroDocumento + "\"," +
                                        //    "\"idCita\": \"" + (notificacion.EsCitaVirtual ? "" : idCita) + "\"," +
                                        //    "\"idCitaVirtual\": \"" + (notificacion.EsCitaVirtual ? idCita : "") + "\"" +
                                        //"}" +
                                        "\"button_3\": \"{\\\"tipoDocumento\\\": \\\"" + notificacion.TipoDocumento + "\\\",\\\"numeroDocumento\\\": \\\"" + notificacion.NumeroDocumento + "\\\",\\\"idCita\\\": \\\"" + (notificacion.EsCitaVirtual ? "" : idCita) + "\\\",\\\"idCitaVirtual\\\": \\\"" + (notificacion.EsCitaVirtual ? idCita : "") + "\\\",\\\"esPagada\\\": \\\"" + notificacion.EsPagada + "\\\",\\\"esAdicional\\\": \\\"" + notificacion.EsAdicional + "\\\"}\"" +
                                    "}" +
                                "}" +
                            "]";
                    }

                    varJson = "{" +
                        "\"serviceId\": " + serviceId + "," +
                        "\"phoneNumber\": \"" + phoneNumber + "\"," +
                        "\"text\": null," +
                        "\"hsm\": {" +
                            "\"elementName\": \"" + elementName + "\"," +
                            "\"namespace\": \"" + @namespace + "\"," +
                            "\"language\": \"" + language + "\"," +
                            "\"header\": null," +
                            "\"body\": {" +
                                //"\"text\": \"" + template + "\"," +
                                //"\"1\": \"" + ClasesGenericas.GetCapitalize(notificacion.NombrePaciente, true) + "\"," +
                                //"\"2\": \"" + fechaHoraPresentacion + "\"," + //"22 de septiembre - 15:00 pm" + "\"," +
                                //"\"3\": \"" + notificacion.Clinica + "\"," +
                                //"\"4\": \"" + medico + "\"," +
                                //"\"5\": \"" + notificacion.Especialidad + "\"" +
                                "\"text\": null," +
                                String.Join(",", lArgumento.ToArray()) +
                            "}," +
                            botones +
                        "}," +
                        "\"tags\": null," +
                        "\"sendHSMIfCaseOpenAnyways\": true," +
                        "\"validateDoNotCallList\": true" +
                    "}";

                    HttpWebRequest varWebRequest = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["SendURL"].ToString());
                    varWebRequest.ContentType = "application/json";
                    varWebRequest.Headers["Authorization"] = ConfigurationManager.AppSettings["SendAutorizacion"].ToString();
                    varWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(varWebRequest.GetRequestStream()))
                    {
                        streamWriter.Write(varJson);
                        streamWriter.Flush();
                    }

                    string varRespuesta = "";
                    try
                    {
                        using (WebResponse response = varWebRequest.GetResponse())
                        {
                            Stream varRespuestaStream = response.GetResponseStream();
                            if (varRespuestaStream != null)
                            {
                                using (StreamReader varReader = new StreamReader(varRespuestaStream))
                                {
                                    varRespuesta = varReader.ReadToEnd();
                                    varReader.Close();
                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        varRespuesta = ex.Message;
                    }
                    //Console.WriteLine("Request: " + varJson);
                    //Console.WriteLine("Response: " + varRespuesta);
                    //Console.ReadLine();
                    new BatchDA().ActualizarResultado(notificacion.IDAppLogNotificacion, varRespuesta, varConexion, varJson);
                }
                varConexion.Desconectar();
            }
            catch (Exception ex)
            {
                throw;
                //varConexion.Desconectar();
                //varError.RegistrarError(ex, "Batch", "BatchNotificaciones");
            }
        }
    }
}
