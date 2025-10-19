using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using CSF.CITASWEB.WS.BatchNotificaciones.conexion;
using System.Net;
using System.IO;
using System.Configuration;
using System.Threading.Tasks;
using DotNetEnv;
using Google.Apis.Auth.OAuth2;

namespace CSF.CITASWEB.WS.BatchNotificaciones
{
    class Program
    {
        static void Main(string[] args)
        {
            ErrorDA varError = new ErrorDA();
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
                {
                    varError.RegistrarError(new Exception("La aplicación ya está ejecutandose"), "Batch", "BatchNotificaciones");
                    return;
                }
                Debug.WriteLine("paso 0");
                List<NotificacionBE> varNotificaciones = new BatchDA().ListarNotificaciones();

                Debug.WriteLine("paso 1");
                varConexion.Conectar();

                Debug.WriteLine("paso 2");
                Env.Load(ConfigurationManager.AppSettings["RutaEnv"].ToString());
                
                string varJson;
                if (varNotificaciones.Count > 0)
                {
                    string serverKey = GetAccessTokenAsync().Result;
                    foreach (NotificacionBE notificacion in varNotificaciones)
                    {
                        Debug.WriteLine("paso 3");
                        varJson = "{" +
                            "\"message\": {" +
                                "\"token\":\"" + notificacion.TokenPush + "\"," +
                                "\"notification\": {" +
                                    "\"title\": \"" + notificacion.Titulo + "\"," +
                                    "\"body\": \"" + notificacion.Descripcion + "\"" +
                                //"}" + 
                                "}," + 
                                "\"data\":{" +
                                    //"\"titulo\":\"" + "Título de prueba" + "\"," +
                                    //"\"detalle\":\"" + "Detalle de prueba" + "\"," +
                                    "\"fecha\":\"" + DateTime.Now.ToString("dd/MM/yyyy hh:mm") + "\"" +
                                "}" +
                            "}" +
                        "}";

                        HttpWebRequest varWebRequest = (HttpWebRequest)WebRequest.Create(Env.GetString("fcm_url_api"));
                        varWebRequest.ContentType = "application/json";

                        varWebRequest.Headers["Authorization"] = "Bearer " + serverKey;
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
                        new BatchDA().ActualizarResultado(notificacion.IDAppLogNotificacion, varRespuesta, varConexion);
                    }
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
        public static async Task<string> GetAccessTokenAsync()
        {
            try
            {
                string jsonString = "{" +
                        "\"type\": \"" + Env.GetString("fcm_type") + "\", " +
                        "\"project_id\": \"" + Env.GetString("fcm_project_id") + "\", " +
                        "\"private_key_id\": \"" + Env.GetString("fcm_private_key_id") + "\", " +
                        "\"private_key\": \"" + Env.GetString("fcm_private_key") + "\", " +
                        "\"client_email\": \"" + Env.GetString("fcm_client_email") + "\", " +
                        "\"client_id\": \"" + Env.GetString("fcm_client_id") + "\", " +
                        "\"auth_uri\": \"" + Env.GetString("fcm_auth_uri") + "\", " +
                        "\"token_uri\": \"" + Env.GetString("fcm_token_uri") + "\", " +
                        "\"auth_provider_x509_cert_url\": \"" + Env.GetString("fcm_auth_provider_x509_cert_url") + "\", " +
                        "\"client_x509_cert_url\": \"" + Env.GetString("fcm_client_x509_cert_url") + "\", " +
                        "\"universe_domain\": \"" + Env.GetString("fcm_universe_domain") + "\"" +
                    "}";

                byte[] byteArray = Encoding.UTF8.GetBytes(jsonString);
                using (MemoryStream stream = new MemoryStream(byteArray))
                {
                    GoogleCredential credential = GoogleCredential.FromStream(stream)
                        .CreateScoped(Env.GetString("fcm_google_apis"));
                    string token = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
                    return token;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
