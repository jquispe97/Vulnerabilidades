using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace CSF.CITASWEB.WS.BatchNotificaciones.conexion
{
    public class BatchDA
    {
        SqlDataReader varDataReader;

        public List<NotificacionBE> ListarNotificaciones()
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("Batch_Proc_NotificarDepurar", null, TipoProcesamiento.DataReader, false);

                List<NotificacionBE> varResultado = new List<NotificacionBE>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new NotificacionBE()
                    {
                        IDAppLogNotificacion = varDataReader.GetInt32(varDataReader.GetOrdinal("IDAppLogNotificacion")),
                        TipoNotificacion = varDataReader.GetString(varDataReader.GetOrdinal("TipoNotificacion")),
                        TipoDispositivo = varDataReader.GetString(varDataReader.GetOrdinal("TipoDispositivo")),
                        TokenPush = varDataReader.GetString(varDataReader.GetOrdinal("TokenPush")),
                        NombrePaciente = varDataReader.GetString(varDataReader.GetOrdinal("NombrePaciente")),
                        ApellidoPaternoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaternoPaciente")),
                        ApellidoMaternoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaternoPaciente")),
                        TelefonoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("TelefonoPaciente")),
                        EmailPaciente = varDataReader.GetString(varDataReader.GetOrdinal("EmailPaciente")),
                        NombreMedico = varDataReader.GetString(varDataReader.GetOrdinal("NombreMedico")),
                        Especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")),
                        Clinica = varDataReader.GetString(varDataReader.GetOrdinal("Clinica")),
                        FechaCita = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaCita")),
                        Titulo = varDataReader.GetString(varDataReader.GetOrdinal("Titulo")),
                        Descripcion = varDataReader.GetString(varDataReader.GetOrdinal("Descripcion")),
                        IDCita = varDataReader.GetString(varDataReader.GetOrdinal("IDCita"))
                    });
                }

                return varResultado;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }

        public void ActualizarResultado(int IDAppLogNotificacion, string ResultadoEnvio, ConexionUtil conexion)
        {
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@IDAppLogNotificacion", SqlDbType.VarChar);
                varParametros[0].Value = IDAppLogNotificacion;
                varParametros[1] = new SqlParameter("@ResultadoEnvio", SqlDbType.VarChar);
                varParametros[1].Value = ResultadoEnvio;
                conexion.EjecutarProcedimientoBatch("Batch_Proc_NotificarDepurar_Resultado", varParametros, TipoProcesamiento.NonQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
