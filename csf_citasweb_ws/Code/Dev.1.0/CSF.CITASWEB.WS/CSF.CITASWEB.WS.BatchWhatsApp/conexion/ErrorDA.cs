using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using System.ServiceModel;

namespace CSF.CITASWEB.WS.BatchWhatsApp.conexion
{
    public class ErrorDA
    {
        private SqlDataReader varDataReader;

        public void RegistrarError(Exception ex, string origen, string servicio)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                string request = "";
                try
                {
                    request = OperationContext.Current.RequestContext.RequestMessage.ToString();
                }
                catch (Exception)
                {
                }

                SqlParameter[] varParametros = new SqlParameter[5];
                varParametros[0] = new SqlParameter("@Mensaje", SqlDbType.VarChar);
                varParametros[0].Value = string.IsNullOrEmpty(ex.Message) ? "" : ex.Message.Substring(0, ex.Message.Length > 5000 ? 5000 : ex.Message.Length);
                varParametros[1] = new SqlParameter("@Ubicacion", SqlDbType.VarChar);
                varParametros[1].Value = string.IsNullOrEmpty(ex.StackTrace) ? "" : ex.StackTrace.Substring(0, ex.StackTrace.Length > 5000 ? 5000 : ex.StackTrace.Length);
                varParametros[2] = new SqlParameter("@Origen", SqlDbType.VarChar);
                varParametros[2].Value = origen;
                varParametros[3] = new SqlParameter("@Servicio", SqlDbType.VarChar);
                varParametros[3].Value = servicio;
                varParametros[4] = new SqlParameter("@Request", SqlDbType.VarChar);
                varParametros[4].Value = request;

                varConexion.EjecutarProcedimiento("Gen_Proc_Error_Insertar", varParametros, TipoProcesamiento.Scalar, false);
            }
            catch (Exception ex1)
            {
                try
                {
                    using (StreamWriter varEscritor = new StreamWriter(ConfigurationManager.AppSettings["rutaLog"].ToString()+DateTime.Now.ToString("yyyyMMdd"), true))
                    {
                        varEscritor.WriteLine(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + ": " + ex.Message + "\n" + ex.StackTrace + "\n" + ex1.Message + "\n");
                        varEscritor.Flush();
                        varEscritor.Close();
                    }
                }
                catch (Exception)
                {
                }
            }
            finally
            {
                varConexion.Desconectar();
            }
        }
    }
}
