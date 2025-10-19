using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSF.CITASWEB.WS.BE;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace CSF.CITASWEB.WS.DA
{
    public class MedicoSubEspecialidadDA
    {
        private SqlDataReader varDataReader;

        #region MantenimientoWeb
        public List<MantMedicoSubEspecialidadBE> Listar(string medico, string especialidad, string clinica)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[3];
                varParametros[0] = new SqlParameter("@Medico", SqlDbType.VarChar);
                varParametros[0].Value = medico;
                varParametros[1] = new SqlParameter("@Clinica", SqlDbType.VarChar);
                varParametros[1].Value = clinica;
                varParametros[2] = new SqlParameter("@Especialidad", SqlDbType.VarChar);
                varParametros[2].Value = especialidad;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("Web_Proc_MedicoSubEspecialidad_Listar", varParametros, TipoProcesamiento.DataReader, false);

                List<MantMedicoSubEspecialidadBE> varResultado = new List<MantMedicoSubEspecialidadBE>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new MantMedicoSubEspecialidadBE()
                    {
                        CMP = varDataReader.GetString(varDataReader.GetOrdinal("CMP")),
                        NombreMedico = varDataReader.GetString(varDataReader.GetOrdinal("NombreMedico")),
                        IDClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString(),
                        NombreClinica = varDataReader.GetString(varDataReader.GetOrdinal("NombreClinica")),
                        IDSubEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDSubEspecialidad")).ToString(),
                        NombreEspecialidad = varDataReader.GetString(varDataReader.GetOrdinal("NombreEspecialidad")),
                        TipoCitas = varDataReader.GetInt32(varDataReader.GetOrdinal("TipoCitas")).ToString(),
                        EsTeleorientacion = varDataReader.IsDBNull(varDataReader.GetOrdinal("EsTeleorientacion")) ? "0": varDataReader.GetBoolean(varDataReader.GetOrdinal("EsTeleorientacion"))?"1":"0",
                        InformacionCita = varDataReader.IsDBNull(varDataReader.GetOrdinal("InformacionCita")) ? "" : varDataReader.GetString(varDataReader.GetOrdinal("InformacionCita"))
                    });
                }

                return varResultado.OrderBy(p => p.NombreMedico).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }

        public void Modificar(MantMedicoSubEspecialidadBE entidad)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[5];
                varParametros[0] = new SqlParameter("@CMP", SqlDbType.VarChar);
                varParametros[0].Value = entidad.CMP;
                varParametros[1] = new SqlParameter("@IDSubEspecialidad", SqlDbType.Int);
                varParametros[1].Value = entidad.IDSubEspecialidad;
                varParametros[2] = new SqlParameter("@IDClinica", SqlDbType.Int);
                varParametros[2].Value = entidad.IDClinica;
                varParametros[3] = new SqlParameter("@TipoCitas", SqlDbType.Int);
                varParametros[3].Value = entidad.TipoCitas;
                varParametros[4] = new SqlParameter("@InformacionCita", SqlDbType.VarChar);
                varParametros[4].Value = entidad.InformacionCita;

                varConexion.EjecutarProcedimiento("Web_Proc_MedicoSubEspecialidad_Modificar", varParametros, TipoProcesamiento.NonQuery);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }
        #endregion
    }
}
