using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSF.CITASWEB.WS.BE;
using System.Data.SqlClient;
using System.Data;

namespace CSF.CITASWEB.WS.DA
{
    public class ReporteUsuarioDA
    {
        public StringBuilder GenerarReporte(DateTime fechaInicio, DateTime fechaFin)
        {
            SqlDataReader varDataReader;
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@FechaInicio", SqlDbType.DateTime);
                varParametros[0].Value = fechaInicio;
                varParametros[1] = new SqlParameter("@FechaFin", SqlDbType.DateTime);
                varParametros[1].Value = fechaFin;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("Web_Proc_Reporte_UsuariosApp", varParametros, TipoProcesamiento.DataReader, false);


                StringBuilder varResultado = new StringBuilder();
                string varCabecera = "Tipo de documento,Numero de documento,Nombres,Apellido paterno,Apellido materno,Genero,Email,Fecha de creacion,Fecha de ultimo acceso,Cantidad de familiares";
                varResultado.AppendLine(varCabecera);
                while (varDataReader.Read())
                {
                    varResultado.AppendLine(
                        "\"" + varDataReader.GetInt32(varDataReader.GetOrdinal("TipoDocumento")).ToString() + "\",\"" +
                        varDataReader.GetString(varDataReader.GetOrdinal("NumeroDocumento")) + "\",\"" +
                        varDataReader.GetString(varDataReader.GetOrdinal("Nombres")) + "\",\"" +
                        varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaterno")) + "\",\"" +
                        varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaterno")) + "\",\"" +
                        varDataReader.GetString(varDataReader.GetOrdinal("Genero")) + "\",\"" +
                        varDataReader.GetString(varDataReader.GetOrdinal("Email")) + "\",\"" +
                        varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaCreacion")).ToString("dd/mm/yyyy hh:MM:ss") + "\",\"" +
                        varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaUltimoAcceso")).ToString() + "\",\"" +
                        varDataReader.GetInt32(varDataReader.GetOrdinal("CantidadFamiliares")).ToString() + "\"");
                }
                return varResultado.Length <= (varCabecera.Length + 3) ? null : varResultado;
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
    }
}
