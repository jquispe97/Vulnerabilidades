using System;
using System.Collections.Generic;
using System.Diagnostics;
using CSF.CITASWEB.WS.BatchFinesAdicionales.Conexion;
using System.Net;
using System.IO;
using System.Configuration;
using System.Reflection;
using System.Linq;

namespace CSF.CITASWEB.WS.BatchFinesAdicionales
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
                    varError.RegistrarError(new Exception("La aplicación ya está ejecutandose"), "Batch", "BatchFinesAdicionales");
                    return;
                }
                Debug.WriteLine("paso 0");
                NotificacionBE varNotificacionBE = new BatchDA().ObtenerNotificacion();
                Debug.WriteLine("paso 1");

                varConexion.Conectar();
                Debug.WriteLine("paso 2");

                if (varNotificacionBE != null 
                    && varNotificacionBE.IDReporteEnvio != 0 
                    && !String.IsNullOrEmpty(varNotificacionBE.Reporte)
                    && !String.IsNullOrEmpty(varNotificacionBE.CorreosPara))
                {
                    List<string> lCabecera = new List<string>();
                    List<string> lHojas = new List<string>();
                    lHojas.Add(varNotificacionBE.Reporte);

                    byte[] buffer = ExcelMemory.Exportar("", new string[] { "Usuarios" }, lHojas, lCabecera, "0043A5", 0, 0, 0);
                    string nombre = "Reporte de usuarios.xlsx";
                    string rutaLog = ClasesGenericas.GetSetting("_RutaLog");
                    string archivoExcel = Path.Combine(rutaLog, nombre);
                    if (File.Exists(archivoExcel))
                    {
                        File.Delete(archivoExcel);
                    }
                    File.WriteAllBytes(archivoExcel, buffer);

                    bool esEnviado = ClasesGenericas.EnviarCorreo(new CorreoBE()
                    {
                        De = ConfigurationManager.AppSettings["_CorreoDe"],
                        Clave = ConfigurationManager.AppSettings["_CorreoClave"],
                        Asunto = varNotificacionBE.Asunto,
                        Contenido = varNotificacionBE.Cuerpo,
                        Para = varNotificacionBE.CorreosPara.Split(','),
                        CC = !string.IsNullOrEmpty(varNotificacionBE.CorreosCC) ? varNotificacionBE.CorreosCC.Split(',') : new string[0],
                        CCO = !string.IsNullOrEmpty(varNotificacionBE.CorreosCCO) ? varNotificacionBE.CorreosCCO.Split(',') : new string[0],
                        Archivo = new string[] { archivoExcel }
                    });

                    new BatchDA().ActualizarEnvio(varNotificacionBE.IDReporteEnvio, esEnviado, varConexion);

                }
                varConexion.Desconectar();
            }
            catch (Exception ex)
            {
                varConexion.Desconectar();
                varError.RegistrarError(ex, "Batch", "BatchFinesAdicionales");
                //throw;
            }
        }
    }
}
