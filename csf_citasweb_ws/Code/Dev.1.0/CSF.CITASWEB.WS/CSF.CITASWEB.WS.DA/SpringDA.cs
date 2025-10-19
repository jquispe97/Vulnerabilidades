using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;

namespace CSF.CITASWEB.WS.DA
{
    public class SpringDA
    {
        private string NombreConexion(int idClinica)
        {
            if (ConfigurationManager.AppSettings["ConexionClinica" + idClinica.ToString()] != null)
                return ConfigurationManager.AppSettings["ConexionClinica" + idClinica.ToString()].ToString();
            else
                throw new Exception("La clínica especificada no tiene configurada la conexión a Spring");
        }

        private string CadenaClinica(int idClinica)
        {

            if (ConfigurationManager.ConnectionStrings[NombreConexion(idClinica)] != null)
                return ConfigurationManager.AppSettings["ConexionClinica" + idClinica.ToString()];
            else
                throw new Exception("La cadena de conexión indicada para la clínica no existe");

        }

        public int AnularCita(int idClinica, int idCitaClinica)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@IdCita", SqlDbType.Int);
                varParametros[0].Value = idCitaClinica;
                varParametros[1] = new SqlParameter("@EstadoCita", SqlDbType.Int);
                varParametros[1].Direction = ParameterDirection.Output;

                List<object> varOutput = (List<object>)varConexion.EjecutarProcedimiento("PROC_Cita_Cancelar", varParametros, TipoProcesamiento.ScalarOutParameters, true, CadenaClinica(idClinica));

                if (varOutput[0] != null && !string.IsNullOrEmpty(varOutput[0].ToString()))
                    throw new Exception("ERRFU:" + varOutput[0].ToString());
                else
                    return int.Parse(((SqlParameterCollection)varOutput[1])["@EstadoCita"].Value.ToString());
            }
            catch (Exception ex)
            {
                
                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }

        public int CrearPaciente(int idClinica, string tipoDocumento, string numeroDocumento, string nombres, string apellidoPaterno, string apellidoMaterno,
                                    string sexo, DateTime fechaNacimiento, string direccion, string telefonoFijo, string telefonoCelular, string email, string idPacienteSpring)
        {
            ConexionUtil varConexion = new ConexionUtil();
            bool indGraboLog = false;
            String[] aParametro = new String[13];
            try
            {
                SqlParameter[] varParametros = new SqlParameter[13];
                varParametros[0] = new SqlParameter("@numdoc", SqlDbType.VarChar);
                varParametros[0].Value = numeroDocumento;
                varParametros[1] = new SqlParameter("@nombres", SqlDbType.VarChar);
                varParametros[1].Value = nombres;
                varParametros[2] = new SqlParameter("@apellidopaterno", SqlDbType.VarChar);
                varParametros[2].Value = apellidoPaterno;
                varParametros[3] = new SqlParameter("@apellidomaterno", SqlDbType.VarChar);
                varParametros[3].Value = apellidoMaterno;
                varParametros[4] = new SqlParameter("@sexo", SqlDbType.VarChar);
                varParametros[4].Value = sexo;
                varParametros[5] = new SqlParameter("@fechanacimiento", SqlDbType.DateTime);
                varParametros[5].Value = fechaNacimiento;
                varParametros[6] = new SqlParameter("@direccion", SqlDbType.VarChar);
                varParametros[6].Value = direccion;
                varParametros[7] = new SqlParameter("@telefono_fijo", SqlDbType.VarChar);
                varParametros[7].Value = telefonoFijo;
                varParametros[8] = new SqlParameter("@telefono_celular", SqlDbType.VarChar);
                varParametros[8].Value = telefonoCelular;
                varParametros[9] = new SqlParameter("@tipodoc", SqlDbType.VarChar);
                varParametros[9].Value = tipoDocumento;
                varParametros[10] = new SqlParameter("@email", SqlDbType.VarChar);
                varParametros[10].Value = email;
                varParametros[11] = new SqlParameter("@usuario_creacion", SqlDbType.VarChar);
                varParametros[11].Value = "133";
                varParametros[12] = new SqlParameter("@idPacienteSpring", SqlDbType.Int);
                varParametros[12].Value = idPacienteSpring;

                string varRespuesta = varConexion.EjecutarProcedimiento("PROC_Paciente_Registrar", varParametros, TipoProcesamiento.Scalar, true, CadenaClinica(idClinica)).ToString();

                aParametro[0] = !String.IsNullOrEmpty(numeroDocumento) ? numeroDocumento : "NULL";
                aParametro[1] = !String.IsNullOrEmpty(nombres) ? nombres : "NULL";
                aParametro[2] = !String.IsNullOrEmpty(apellidoPaterno) ? apellidoPaterno : "NULL";
                aParametro[3] = !String.IsNullOrEmpty(apellidoMaterno) ? apellidoMaterno : "NULL";
                aParametro[4] = !String.IsNullOrEmpty(sexo) ? sexo : "NULL";
                aParametro[5] = fechaNacimiento != null ? fechaNacimiento.ToString("dd/MM/yyyy") : "NULL";
                aParametro[6] = !String.IsNullOrEmpty(direccion) ? direccion : "NULL";
                aParametro[7] = !String.IsNullOrEmpty(telefonoFijo) ? telefonoFijo : "NULL";
                aParametro[8] = !String.IsNullOrEmpty(telefonoCelular) ? telefonoCelular : "NULL";
                aParametro[9] = !String.IsNullOrEmpty(tipoDocumento) ? tipoDocumento : "NULL";
                aParametro[10] = !String.IsNullOrEmpty(email) ? email : "NULL";
                aParametro[11] = "133";
                aParametro[12] = !String.IsNullOrEmpty(idPacienteSpring) ? idPacienteSpring : "NULL";
                if (String.IsNullOrEmpty(varRespuesta))
                {
                    new ErrorDA().GrabarLog("PROC_Paciente_Registrar", "WS", "SpringDA/CrearPaciente", String.Join("¦", aParametro));
                    indGraboLog = true;
                }

                int varIDPersona;
                if (int.TryParse(varRespuesta, out varIDPersona))
                    return varIDPersona;
                else
                {
                    if (!indGraboLog)
                    {
                        indGraboLog = true;
                        new ErrorDA().GrabarLog("PROC_Paciente_Registrar", "WS", "SpringDA/CrearPaciente", String.Join("¦", aParametro));
                    }
                    throw new Exception("ERRFU:" + varRespuesta);
                }
            }
            catch (Exception)
            {
                if (!indGraboLog)
                {
                    new ErrorDA().GrabarLog("PROC_Paciente_Registrar", "WS", "SpringDA/CrearPaciente", String.Join("¦", aParametro));
                }
                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }

        public int CrearCita(int idClinica, string idPaciente, int idMedico, string fechaCita, int duracion, int idHorario,
                                string unidadReplicacion, string origen, string comentarioSituacion, int tipoCita, string telefono, string email,
                                string codigoComponente = "", bool indTipoHorarioImagenes = false)
        {
            origen = origen.ToLower();
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros;
                //if (!String.IsNullOrEmpty(codigoComponente))
                if (indTipoHorarioImagenes && idClinica == 9)
                {
                    //TipoHorario: Imágenes && ALIADA
                    varParametros = new SqlParameter[13];
                }
                else 
                {
                    varParametros = new SqlParameter[12];
                }
                varParametros[0] = new SqlParameter("@id_paciente", SqlDbType.Int);
                varParametros[0].Value = idPaciente;
                varParametros[1] = new SqlParameter("@id_medico", SqlDbType.Int);
                varParametros[1].Value = idMedico;
                varParametros[2] = new SqlParameter("@id_horario", SqlDbType.Int);
                varParametros[2].Value = idHorario;
                varParametros[3] = new SqlParameter("@comentario_situacion", SqlDbType.VarChar);
                varParametros[3].Value = comentarioSituacion;
                varParametros[4] = new SqlParameter("@fechacita", SqlDbType.DateTime);
                varParametros[4].Value = fechaCita;
                varParametros[5] = new SqlParameter("@DuracionPromedio", SqlDbType.Decimal);
                varParametros[5].Value = duracion;
                varParametros[6] = new SqlParameter("@TipoCita", SqlDbType.Int);
                varParametros[6].Value = tipoCita;
                varParametros[7] = new SqlParameter("@UnidadReplicacion", SqlDbType.VarChar);
                varParametros[7].Value = unidadReplicacion;
                varParametros[8] = new SqlParameter("@usuario_creacion", SqlDbType.VarChar);
                varParametros[8].Value =
                    (origen == "app" ? (ConfigurationManager.AppSettings["UsuarioSpring"] == null ? "Clínica SF" : ConfigurationManager.AppSettings["UsuarioSpring"].ToString()) :
                    (origen == "android" ? (ConfigurationManager.AppSettings["UsuarioSpring"] == null ? "Clínica SF" : ConfigurationManager.AppSettings["UsuarioSpring"].ToString()) :
                    (origen == "ios" ? (ConfigurationManager.AppSettings["UsuarioSpring"] == null ? "Clínica SF" : ConfigurationManager.AppSettings["UsuarioSpring"].ToString()) :
                    (origen == "huawei" ? (ConfigurationManager.AppSettings["UsuarioSpring"] == null ? "Clínica SF" : ConfigurationManager.AppSettings["UsuarioSpring"].ToString()) :
                    (origen == "web" ? (ConfigurationManager.AppSettings["UsuarioSpringWeb"] == null ? "Clínica SF" : ConfigurationManager.AppSettings["UsuarioSpringWeb"].ToString()) :
                    (origen == "intranet" ? (ConfigurationManager.AppSettings["UsuarioSpringIntranet"] == null ? "Clínica SF" : ConfigurationManager.AppSettings["UsuarioSpringIntranet"].ToString()) :
                    "NA"))))));
                varParametros[9] = new SqlParameter("@app_origen", SqlDbType.VarChar);
                varParametros[9].Value = "2";
                varParametros[10] = new SqlParameter("@TELEFONO", SqlDbType.VarChar);
                varParametros[10].Value = telefono;
                varParametros[11] = new SqlParameter("@EMAIL", SqlDbType.VarChar);
                varParametros[11].Value = email;
                //if (!String.IsNullOrEmpty(codigoComponente))
                if (indTipoHorarioImagenes && idClinica == 9)
                {
                    //ALIADA
                    varParametros[12] = new SqlParameter("@CodigoComponente", SqlDbType.VarChar);
                    varParametros[12].Value = codigoComponente;
                }

                string varRespuesta = varConexion.EjecutarProcedimiento("PROC_Cita_Registrar", varParametros, TipoProcesamiento.Scalar, true, CadenaClinica(idClinica)).ToString();

                
                int varIDCita;
                if (int.TryParse(varRespuesta, out varIDCita))
                    return varIDCita;
                else
                    throw new Exception("ERRFU:" + varRespuesta);
            }
            catch (Exception ex)
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
