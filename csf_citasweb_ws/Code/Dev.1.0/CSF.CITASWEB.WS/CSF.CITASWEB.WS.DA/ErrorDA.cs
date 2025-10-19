using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CSF.CITASWEB.WS.BE;
using System.Configuration;
using System.ServiceModel;

namespace CSF.CITASWEB.WS.DA
{
    public class ErrorDA
    {
        public RespuestaSimpleBE RegistrarError(Exception ex, string origen, string servicio, string mensajeError = null)
        {
            if (ex.Message.StartsWith("INFO:"))
            {
                return new RespuestaSimpleBE()
                {
                    rpt = 0,
                    mensaje = ex.Message.Replace("INFO:", ""),
                    data = null
                };
            }
            else if (ex.Message.StartsWith("ERRFU:"))
            {
                return new RespuestaSimpleBE()
                {
                    rpt = 1,
                    mensaje = ex.Message.Replace("ERRFU:", ""),
                    data = null
                };
            }
            else
            {
                int varIDError = 0;
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
                    Exception varErrorDetallado = ex;
                    string mensaje = ex.Message;
                    string stackTrace = ex.StackTrace;
                    while (varErrorDetallado.InnerException != null)
                    {
                        mensaje = mensaje + "\r\n**\r\n" + varErrorDetallado.InnerException.Message;
                        stackTrace = stackTrace + "\r\n**\r\n" + varErrorDetallado.InnerException.StackTrace;
                        varErrorDetallado = varErrorDetallado.InnerException;
                    }
                    

                    SqlParameter[] varParametros = new SqlParameter[5];
                    varParametros[0] = new SqlParameter("@Mensaje", SqlDbType.VarChar);
                    varParametros[0].Value = string.IsNullOrEmpty(ex.Message) ? "" : (mensaje.Substring(0, ex.Message.Length > 5000 ? 5000 : mensaje.Length));
                    varParametros[1] = new SqlParameter("@Ubicacion", SqlDbType.VarChar);
                    varParametros[1].Value = string.IsNullOrEmpty(ex.StackTrace) ? "" : (stackTrace.Substring(0, ex.StackTrace.Length > 5000 ? 5000 : stackTrace.Length));
                    varParametros[2] = new SqlParameter("@Origen", SqlDbType.VarChar);
                    varParametros[2].Value = origen;
                    varParametros[3] = new SqlParameter("@Servicio", SqlDbType.VarChar);
                    varParametros[3].Value = servicio;
                    varParametros[4] = new SqlParameter("@Request", SqlDbType.VarChar);
                    if (mensajeError == null)varParametros[4].Value = request;
                    else varParametros[4].Value = mensajeError;

                    varIDError = int.Parse(varConexion.EjecutarProcedimiento("Gen_Proc_Error_Insertar", varParametros, TipoProcesamiento.Scalar, false).ToString());
                }
                catch (Exception)
                {
                }
                finally
                {
                    varConexion.Desconectar();
                }
                return new RespuestaSimpleBE()
                {
                    rpt = 999,
                    mensaje = ex.Message,
                    data = null
                };
            }
        }

        public RespuestaBE<T> RegistrarError<T>(Exception ex, string origen, string servicio)
        {
            if (ex.Message.StartsWith("INFO:"))
            {
                return new RespuestaBE<T>()
                {
                    rpt = 0,
                    mensaje = ex.Message.Replace("INFO:", ""),
                    data = default(T)
                };
            }
            else if (ex.Message.StartsWith("ERRFU:"))
            {
                return new RespuestaBE<T>()
                {
                    rpt = 1,
                    mensaje = ex.Message.Replace("ERRFU:", ""),
                    data = default(T)
                };
            }
            else
            {
                int varIDError = 0;
                ConexionUtil varConexion = new ConexionUtil();
                if (ex != null)
                {
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
                        varParametros[0].Value = string.IsNullOrEmpty(ex.Message) ? "" : (ex.Message).Substring(0, ex.Message.Length > 5000 ? 5000 : ex.Message.Length);
                        varParametros[1] = new SqlParameter("@Ubicacion", SqlDbType.VarChar);
                        varParametros[1].Value = string.IsNullOrEmpty(ex.StackTrace) ? "" : ex.StackTrace.Substring(0, ex.StackTrace.Length > 5000 ? 5000 : ex.StackTrace.Length);
                        varParametros[2] = new SqlParameter("@Origen", SqlDbType.VarChar);
                        varParametros[2].Value = origen;
                        varParametros[3] = new SqlParameter("@Servicio", SqlDbType.VarChar);
                        varParametros[3].Value = servicio;
                        varParametros[4] = new SqlParameter("@Request", SqlDbType.VarChar);
                        varParametros[4].Value = request;

                        varIDError = int.Parse(varConexion.EjecutarProcedimiento("Gen_Proc_Error_Insertar", varParametros, TipoProcesamiento.Scalar, false).ToString());
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        varConexion.Desconectar();
                    }
                }
                return new RespuestaBE<T>()
                {
                    rpt = 999,
                    mensaje = "Ocurrio un error al ejecutar el método." + ((ConfigurationManager.AppSettings["MostrarIDError"].ToString() == "1") ? ("\nIdentificador del error: " + varIDError.ToString()) : ""),
                    data = default(T)
                };
            }
        }

        public RespuestaSimpleBE RegistrarErrorServiciosWeb(Exception ex, string origen, string servicio)
        {
            if (ex.Message.StartsWith("INFO:"))
            {
                return new RespuestaSimpleBE()
                {
                    rpt = 0,
                    mensaje = ex.Message.Replace("INFO:", ""),
                    data = null
                };
            }
            else if (ex.Message.StartsWith("ERRFU:"))
            {
                return new RespuestaSimpleBE()
                {
                    rpt = 1,
                    mensaje = ex.Message.Replace("ERRFU:", ""),
                    data = null
                };
            }
            else
            {
                int varIDError = 0;
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

                    varIDError = int.Parse(varConexion.EjecutarProcedimiento("Gen_Proc_Error_Insertar", varParametros, TipoProcesamiento.Scalar, false).ToString());
                }
                catch (Exception)
                {
                }
                finally
                {
                    varConexion.Desconectar();
                }
                return new RespuestaSimpleBE()
                {
                    rpt = 999,
                    mensaje = ex.Message,
                    data = null
                };
            }
        }

        public RespuestaBE<T> RegistrarErrorServiciosWeb<T>(Exception ex, string origen, string servicio)
        {
            if (ex.Message.StartsWith("INFO:"))
            {
                return new RespuestaBE<T>()
                {
                    rpt = 0,
                    mensaje = ex.Message.Replace("INFO:", ""),
                    data = default(T)
                };
            }
            else if (ex.Message.StartsWith("ERRFU:"))
            {
                return new RespuestaBE<T>()
                {
                    rpt = 1,
                    mensaje = ex.Message.Replace("ERRFU:", ""),
                    data = default(T)
                };
            }
            else
            {
                int varIDError = 0;
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

                    varIDError = int.Parse(varConexion.EjecutarProcedimiento("Gen_Proc_Error_Insertar", varParametros, TipoProcesamiento.Scalar, false).ToString());
                }
                catch (Exception)
                {
                }
                finally
                {
                    varConexion.Desconectar();
                }
                return new RespuestaBE<T>()
                {
                    rpt = 999,
                    mensaje = "Ocurrio un error al ejecutar el método." + ((ConfigurationManager.AppSettings["MostrarIDError"].ToString() == "1") ? ("\nIdentificador del error: " + varIDError.ToString()) : ""),
                    data = default(T)
                };
            }
        }

        public RespuestaSimpleBE RegistrarErrorV2(Exception ex, string origen, string servicio, string mensajeError = null)
        {
            int varIDError = 0;
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
                Exception varErrorDetallado = ex;
                string mensaje = ex.Message;
                string stackTrace = ex.StackTrace;
                while (varErrorDetallado.InnerException != null)
                {
                    mensaje = mensaje + "\r\n**\r\n" + varErrorDetallado.InnerException.Message;
                    stackTrace = stackTrace + "\r\n**\r\n" + varErrorDetallado.InnerException.StackTrace;
                    varErrorDetallado = varErrorDetallado.InnerException;
                }


                SqlParameter[] varParametros = new SqlParameter[5];
                varParametros[0] = new SqlParameter("@Mensaje", SqlDbType.VarChar);
                varParametros[0].Value = string.IsNullOrEmpty(ex.Message) ? "" : (mensaje.Substring(0, ex.Message.Length > 5000 ? 5000 : mensaje.Length));
                varParametros[1] = new SqlParameter("@Ubicacion", SqlDbType.VarChar);
                varParametros[1].Value = string.IsNullOrEmpty(ex.StackTrace) ? "" : (stackTrace.Substring(0, ex.StackTrace.Length > 5000 ? 5000 : stackTrace.Length));
                varParametros[2] = new SqlParameter("@Origen", SqlDbType.VarChar);
                varParametros[2].Value = origen;
                varParametros[3] = new SqlParameter("@Servicio", SqlDbType.VarChar);
                varParametros[3].Value = servicio;
                varParametros[4] = new SqlParameter("@Request", SqlDbType.VarChar);
                if (mensajeError == null) varParametros[4].Value = request;
                else varParametros[4].Value = mensajeError;

                varIDError = int.Parse(varConexion.EjecutarProcedimiento("Gen_Proc_Error_Insertar", varParametros, TipoProcesamiento.Scalar, false).ToString());
            }
            catch (Exception)
            {
            }
            finally
            {
                varConexion.Desconectar();
            }
            return new RespuestaSimpleBE()
            {
                rpt = 999,
                mensaje = ex.Message,
                data = null
            };
        }

        public void GrabarLog(string titulo, string origen, string servicio, string mensajeLog = "", string request = "")
        {
            int varIDError = 0;
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[5];
                varParametros[0] = new SqlParameter("@Mensaje", SqlDbType.VarChar);
                varParametros[0].Value = string.IsNullOrEmpty(titulo) ? "" : (titulo.Substring(0, titulo.Length > 5000 ? 5000 : titulo.Length));
                varParametros[1] = new SqlParameter("@Ubicacion", SqlDbType.VarChar);
                varParametros[1].Value = string.IsNullOrEmpty(mensajeLog) ? "" : (mensajeLog.Substring(0, mensajeLog.Length > 5000 ? 5000 : mensajeLog.Length));
                varParametros[2] = new SqlParameter("@Origen", SqlDbType.VarChar);
                varParametros[2].Value = origen;
                varParametros[3] = new SqlParameter("@Servicio", SqlDbType.VarChar);
                varParametros[3].Value = servicio;
                varParametros[4] = new SqlParameter("@Request", SqlDbType.VarChar);
                varParametros[4].Value = request;

                varIDError = int.Parse(varConexion.EjecutarProcedimiento("Gen_Proc_Error_Insertar", varParametros, TipoProcesamiento.Scalar, false).ToString());
            }
            catch (Exception)
            {
            }
            finally
            {
                varConexion.Desconectar();
            }
        }
    }
}
