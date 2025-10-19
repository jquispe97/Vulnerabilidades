using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace CSF.CITASWEB.WS.BatchFinesAdicionales.Conexion
{
    public class BatchDA
    {
        SqlDataReader varDataReader;

        public NotificacionBE ObtenerNotificacion()
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("Sp_ReporteFinesAdicionales_Consultar", null, TipoProcesamiento.DataReader, false);

                NotificacionBE varResultado = new NotificacionBE();
                while (varDataReader.Read())
                {
                    varResultado = new NotificacionBE()
                    {
                        IDReporteEnvio = varDataReader.GetInt32(varDataReader.GetOrdinal("IDReporteEnvio")),
                        FechaInstalacion = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaInstalacion")),
                        FechaInicio = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaInicio")),
                        FechaFin = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaFin")),
                        CantidadDiasRango = varDataReader.GetInt32(varDataReader.GetOrdinal("CantidadDiasRango")),
                        Asunto = varDataReader.GetString(varDataReader.GetOrdinal("Asunto")),
                        Cuerpo = varDataReader.GetString(varDataReader.GetOrdinal("Cuerpo")),
                        CorreosPara = varDataReader.GetString(varDataReader.GetOrdinal("CorreosPara")),
                        CorreosCC = varDataReader.GetString(varDataReader.GetOrdinal("CorreosCC")),
                        CorreosCCO = varDataReader.GetString(varDataReader.GetOrdinal("CorreosCCO")),
                        Reporte = varDataReader.GetString(varDataReader.GetOrdinal("Reporte"))
                    };
                }

                return varResultado;
            }
            catch (Exception e)
            {
                //Debug.WriteLine(e.Message);
                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }

        public void ActualizarEnvio(int idReporteEnvio, bool esEnviado, ConexionUtil conexion)
        {
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@tnIDReporteEnvio", SqlDbType.Int);
                varParametros[0].Value = idReporteEnvio;
                varParametros[1] = new SqlParameter("@tlEnviado", SqlDbType.VarChar);
                varParametros[1].Value = esEnviado;

                conexion.EjecutarProcedimientoBatch("Sp_ReporteFinesAdicionales_Update", varParametros, TipoProcesamiento.NonQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
