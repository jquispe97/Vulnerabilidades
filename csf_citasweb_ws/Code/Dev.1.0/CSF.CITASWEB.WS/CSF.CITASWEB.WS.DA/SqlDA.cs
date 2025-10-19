using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CSF.CITASWEB.WS.DA
{
    public class SqlDA
    {
        public string CadenaConexion { get; set; }
        public SqlDA (string nombreConexion = "CadenaConexion")
        {
            CadenaConexion = ConfigurationManager.ConnectionStrings[nombreConexion].ConnectionString;
        }
        public string EjecutarComando (string nombreSP, string nombreParametro = "", string valorParametro = "")
        {
            string rpta = "";
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(nombreSP, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (!String.IsNullOrEmpty(nombreParametro) && !String.IsNullOrEmpty(valorParametro))
                        {
                            cmd.Parameters.AddWithValue(nombreParametro, valorParametro);
                        }
                        object data = cmd.ExecuteScalar();
                        if (data != null)
                        {
                            rpta = data.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return rpta;
        }
        public string ValidarCorreo(string tipoDocumento, string numeroDocumento, string idClinica,
            string idHorario, string fechaAtencion, string turno, string horaAproximada)
        {
            string rpta = "";
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("uspCitaValidarCorreo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] aSqlParametro = new SqlParameter[7];
                        aSqlParametro[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                        aSqlParametro[0].Value = tipoDocumento;
                        aSqlParametro[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                        aSqlParametro[1].Value = numeroDocumento;
                        aSqlParametro[2] = new SqlParameter("@IDClinica", SqlDbType.Int);
                        aSqlParametro[2].Value = idClinica;
                        aSqlParametro[3] = new SqlParameter("@IdHorario", SqlDbType.Int);
                        aSqlParametro[3].Value = idHorario;
                        aSqlParametro[4] = new SqlParameter("@FechaAtencion", SqlDbType.Date);
                        aSqlParametro[4].Value = fechaAtencion;
                        aSqlParametro[5] = new SqlParameter("@Turno", SqlDbType.Int);
                        aSqlParametro[5].Value = turno;
                        aSqlParametro[6] = new SqlParameter("@HoraAproximada", SqlDbType.Time);
                        aSqlParametro[6].Value = horaAproximada;
                        cmd.Parameters.AddRange(aSqlParametro);
                        object data = cmd.ExecuteScalar();
                        if (data != null)
                        {
                            rpta = data.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return rpta;
        }
    }
}
