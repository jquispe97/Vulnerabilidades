using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CSF.CITASWEB.WS.BE;
using System.Data;

namespace CSF.CITASWEB.WS.DA
{
    public class NotificacionMasivaDA
    {
        SqlDataReader varDataReader;

        public List<NotificacionMasivaBE> Listar()
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("Web_Proc_AppNotificacionMasiva_Listar", null, TipoProcesamiento.DataReader, false);

                List<NotificacionMasivaBE> varResultado = new List<NotificacionMasivaBE>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new NotificacionMasivaBE()
                    {
                        IDAppNotificacionMasiva = varDataReader.GetInt32(varDataReader.GetOrdinal("IDAppNotificacionMasiva")),
                        FechaRegistro = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaRegistro")).ToShortDateString(),
                        Titulo = varDataReader.GetString(varDataReader.GetOrdinal("Titulo")),
                        Mensaje = varDataReader.GetString(varDataReader.GetOrdinal("Mensaje")),
                        Tipo = varDataReader.GetBoolean(varDataReader.GetOrdinal("EsFiltrado")) == true ? "Puntual" : "Masivo",
                        FueEnviado = varDataReader.IsDBNull(varDataReader.GetOrdinal("FechaEnvio")) ? "No" : "Si, el " + varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaEnvio")).ToShortDateString()
                    });
                }

                return varResultado;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }

        public void Insertar(int tipoDocumentoSolicitante, string numeroDocumentoSolicitante,
                            string titulo, string mensaje, string filtros)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[5];
                varParametros[0] = new SqlParameter("@TipoDocumentoSolicitante", SqlDbType.Int);
                varParametros[0].Value = tipoDocumentoSolicitante;
                varParametros[1] = new SqlParameter("@NumeroDocumentoSolicitante", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumentoSolicitante;
                varParametros[2] = new SqlParameter("@Titulo", SqlDbType.VarChar);
                varParametros[2].Value = titulo;
                varParametros[3] = new SqlParameter("@Mensaje", SqlDbType.VarChar);
                varParametros[3].Value = mensaje;
                varParametros[4] = new SqlParameter("@Filtros", SqlDbType.VarChar);
                varParametros[4].Value = filtros;

                varConexion.EjecutarProcedimiento("Web_Proc_AppNotificacionMasiva_Insertar", varParametros, TipoProcesamiento.NonQuery);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
