using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CSF.CITASWEB.WS.BE;
using System.Globalization;
using System.Diagnostics;
using System.Data.OleDb;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CSF.CITASWEB.WS.DA
{
    public class CitaDA
    {
        private SqlDataReader varDataReader;

        public PreDatosBE PreDatos(string idHorario, string numeroDia, string turno, string tipoDocumento, string numeroDocumento, DateTime fecha, bool esChequeo = false, int tipoCobertura = 0, string idCitaOriginal = "", string origen = "", string horaInicio = "")
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[10];
                varParametros[0] = new SqlParameter("@IDHorario", SqlDbType.Int);
                varParametros[0].Value = idHorario;
                varParametros[1] = new SqlParameter("@NumeroDia", SqlDbType.Int);
                varParametros[1].Value = numeroDia;
                varParametros[2] = new SqlParameter("@Turno", SqlDbType.Int);
                varParametros[2].Value = turno;
                varParametros[3] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[3].Value = tipoDocumento;
                varParametros[4] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[4].Value = numeroDocumento;
                varParametros[5] = new SqlParameter("@FechaAtencion", SqlDbType.DateTime);
                varParametros[5].Value = fecha;
                varParametros[6] = new SqlParameter("@EsChequeo", SqlDbType.Bit);
                varParametros[6].Value = esChequeo;
                varParametros[7] = new SqlParameter("@TipoCobertura", SqlDbType.Int);
                varParametros[7].Value = tipoCobertura;
                varParametros[8] = new SqlParameter("@IDCitaOriginal", SqlDbType.Int);
                if (!String.IsNullOrEmpty(idCitaOriginal)) varParametros[8].Value = idCitaOriginal;
                else varParametros[8].Value = null;
                varParametros[9] = new SqlParameter("@Origen", SqlDbType.VarChar);
                if (!String.IsNullOrEmpty(origen)) varParametros[9].Value = origen;
                else varParametros[9].Value = null;

                //varParametros[10] = new SqlParameter("@HoraInicio", SqlDbType.VarChar);
                //if (!String.IsNullOrEmpty(horaInicio)) varParametros[10].Value = horaInicio;
                //else varParametros[10].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_PreDatos", varParametros, TipoProcesamiento.DataReader, false);
                varDataReader.Read();
                PreDatosBE varResultado = new PreDatosBE();
                try
                {
                    varResultado.DatosCita.IDClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica"));
                    varResultado.DatosCita.IDMedicoSpring = varDataReader.GetInt32(varDataReader.GetOrdinal("IDMedicoSpring"));
                    varResultado.DatosCita.HoraAtencion = varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraAtencion")).ToString(@"hh\:mm\:ss");
                    varResultado.DatosCita.TiempoAtencion = varDataReader.GetInt32(varDataReader.GetOrdinal("TiempoAtencion"));
                    varResultado.DatosCita.IDHorarioSpring = varDataReader.GetInt32(varDataReader.GetOrdinal("IDHorarioSpring"));
                    varResultado.DatosCita.UnidadReplicacion = varDataReader.GetString(varDataReader.GetOrdinal("UnidadReplicacion"));
                    varResultado.DatosCita.CMP = varDataReader.GetString(varDataReader.GetOrdinal("CMP"));
                    varResultado.DatosCita.tipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("TipoPaciente"));
                }
                catch (Exception ex)
                {
                    throw new Exception("ERRFU:No se pudo obtener información del horario");
                }
                varDataReader.NextResult();
                varDataReader.Read();
                try
                {
                    varResultado.DatosPaciente.IDPacienteSpring = varDataReader.GetInt32(varDataReader.GetOrdinal("IDPacienteSpring"));
                    varResultado.DatosPaciente.Nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres"));
                    varResultado.DatosPaciente.ApellidoPaterno = varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaterno"));
                    varResultado.DatosPaciente.ApellidoMaterno = varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaterno"));
                    varResultado.DatosPaciente.Genero = varDataReader.GetString(varDataReader.GetOrdinal("Genero"));
                    varResultado.DatosPaciente.FechaNacimiento = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaNacimiento"));
                    varResultado.DatosPaciente.Direccion = varDataReader.GetString(varDataReader.GetOrdinal("Direccion"));
                    varResultado.DatosPaciente.TelefonoFijo = varDataReader.GetString(varDataReader.GetOrdinal("TelefonoFijo"));
                    varResultado.DatosPaciente.TelefonoCelular = varDataReader.GetString(varDataReader.GetOrdinal("TelefonoCelular"));
                    varResultado.DatosPaciente.Email = varDataReader.GetString(varDataReader.GetOrdinal("Email"));
                }
                catch (Exception)
                {
                    throw new Exception("ERRFU:No se pudo obtener información del usuario");
                }
                varDataReader.NextResult();
                varDataReader.Read();
                try
                {
                    varResultado.DatosCitaAnterior.esPagada = varDataReader.GetBoolean(varDataReader.GetOrdinal("EsPagadaCitaAnterior"));
                    varResultado.DatosCitaAnterior.tipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("TipoPacienteCitaAnterior"));
                }
                catch (Exception)
                {
                    throw new Exception("ERRFU:Error al obtener los datos de la cita anterior");
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

        public PreDatosBE PreDatosAdicional(string idHorario, string numeroDia, string tipoDocumento, string numeroDocumento, string horaInicio, string fecha, string idCitaOriginal = "", string origen = "")
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[8];
                varParametros[0] = new SqlParameter("@IDHorario", SqlDbType.Int);
                varParametros[0].Value = idHorario;
                varParametros[1] = new SqlParameter("@NumeroDia", SqlDbType.Int);
                varParametros[1].Value = numeroDia;
                varParametros[2] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[2].Value = tipoDocumento;
                varParametros[3] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[3].Value = numeroDocumento;
                varParametros[4] = new SqlParameter("@HoraInicio", SqlDbType.VarChar);
                varParametros[4].Value = horaInicio;
                varParametros[5] = new SqlParameter("@FechaAtencion", SqlDbType.VarChar);
                varParametros[5].Value = fecha;

                varParametros[6] = new SqlParameter("@IDCitaOriginal", SqlDbType.Int);
                if (!String.IsNullOrEmpty(idCitaOriginal)) varParametros[6].Value = idCitaOriginal;
                else varParametros[6].Value = null;
                varParametros[7] = new SqlParameter("@Origen", SqlDbType.VarChar);
                if (!String.IsNullOrEmpty(origen)) varParametros[7].Value = origen;
                else varParametros[7].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_PreDatosAdicional", varParametros, TipoProcesamiento.DataReader, false);
                varDataReader.Read();
                PreDatosBE varResultado = new PreDatosBE();
                try
                {
                    varResultado.DatosCita.IDClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica"));
                    varResultado.DatosCita.IDMedicoSpring = varDataReader.GetInt32(varDataReader.GetOrdinal("IDMedicoSpring"));
                    varResultado.DatosCita.HoraAtencion = horaInicio;
                    varResultado.DatosCita.TiempoAtencion = varDataReader.GetInt32(varDataReader.GetOrdinal("TiempoAtencion"));
                    varResultado.DatosCita.IDHorarioSpring = varDataReader.GetInt32(varDataReader.GetOrdinal("IDHorarioSpring"));
                    varResultado.DatosCita.UnidadReplicacion = varDataReader.GetString(varDataReader.GetOrdinal("UnidadReplicacion"));
                    varResultado.DatosCita.CMP = varDataReader.GetString(varDataReader.GetOrdinal("CMP"));
                    varResultado.DatosCita.tipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("TipoPaciente"));
                }
                catch (Exception)
                {
                    throw new Exception("ERRFU:No se pudo obtener información del horario");
                }
                varDataReader.NextResult();
                varDataReader.Read();
                try
                {
                    varResultado.DatosPaciente.IDPacienteSpring = varDataReader.GetInt32(varDataReader.GetOrdinal("IDPacienteSpring"));
                    varResultado.DatosPaciente.Nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres"));
                    varResultado.DatosPaciente.ApellidoPaterno = varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaterno"));
                    varResultado.DatosPaciente.ApellidoMaterno = varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaterno"));
                    varResultado.DatosPaciente.Genero = varDataReader.GetString(varDataReader.GetOrdinal("Genero"));
                    varResultado.DatosPaciente.FechaNacimiento = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaNacimiento"));
                    varResultado.DatosPaciente.Direccion = varDataReader.GetString(varDataReader.GetOrdinal("Direccion"));
                    varResultado.DatosPaciente.TelefonoFijo = varDataReader.GetString(varDataReader.GetOrdinal("TelefonoFijo"));
                    varResultado.DatosPaciente.TelefonoCelular = varDataReader.GetString(varDataReader.GetOrdinal("TelefonoCelular"));
                    varResultado.DatosPaciente.Email = varDataReader.GetString(varDataReader.GetOrdinal("Email"));
                }
                catch (Exception)
                {
                    throw new Exception("ERRFU:No se pudo obtener información del usuario");
                }
                varDataReader.NextResult();
                varDataReader.Read();
                try
                {
                    varResultado.DatosCitaAnterior.esPagada = varDataReader.GetBoolean(varDataReader.GetOrdinal("EsPagadaCitaAnterior"));
                    varResultado.DatosCitaAnterior.tipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("TipoPacienteCitaAnterior"));
                }
                catch (Exception)
                {
                    throw new Exception("ERRFU:Error al obtener los datos de la cita anterior");
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

        public Dictionary<string, string> RegistrarCita(string idCitaSpring, string tipoDocumento, string numeroDocumento, string idHorario,
                                        string fecha, string numeroDia, string numeroTurno, string id_persona, string origen,
                                        string observaciones, int tipoCita, bool esChequeo, string horaInicio = "", int tiempoAtencion = 0, string codigoComponente = "",
                                        string strRespuestasImagenes = "", string idCitaOriginal = "", string origenOpcion = "")
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros;

                varParametros = new SqlParameter[14];

                varParametros[0] = new SqlParameter("@IDCita", SqlDbType.Int);
                varParametros[0].Value = idCitaSpring;
                varParametros[1] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[1].Value = tipoDocumento;
                varParametros[2] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[2].Value = numeroDocumento;
                varParametros[3] = new SqlParameter("@IDHorario", SqlDbType.Int);
                varParametros[3].Value = idHorario;
                varParametros[4] = new SqlParameter("@FechaAtencion", SqlDbType.Date);
                varParametros[4].Value = fecha;
                varParametros[5] = new SqlParameter("@NumeroDia", SqlDbType.Int);
                varParametros[5].Value = numeroDia;
                varParametros[6] = new SqlParameter("@Turno", SqlDbType.Int);
                varParametros[6].Value = numeroTurno;
                varParametros[7] = new SqlParameter("@id_persona", SqlDbType.Int);
                varParametros[7].Value = id_persona;
                varParametros[8] = new SqlParameter("@Origen", SqlDbType.VarChar);
                varParametros[8].Value = origen;
                varParametros[9] = new SqlParameter("@Observaciones", SqlDbType.VarChar);
                varParametros[9].Value = observaciones;
                varParametros[10] = new SqlParameter("@TipoCita", SqlDbType.Int);
                varParametros[10].Value = tipoCita;
                varParametros[11] = new SqlParameter("@EsChequeo", SqlDbType.Bit);
                varParametros[11].Value = esChequeo;
                varParametros[12] = new SqlParameter("@IDCitaOriginal", SqlDbType.Int);
                if (!String.IsNullOrEmpty(idCitaOriginal)) varParametros[12].Value = idCitaOriginal;
                else varParametros[12].Value = null;
                varParametros[13] = new SqlParameter("@OrigenOpcion", SqlDbType.VarChar);
                if (!String.IsNullOrEmpty(origenOpcion)) varParametros[13].Value = origenOpcion;
                else varParametros[13].Value = null;

                //varParametros[13] = new SqlParameter("@HoraInicio", SqlDbType.VarChar);
                //if (!String.IsNullOrEmpty(horaInicio)) varParametros[13].Value = horaInicio;
                //else varParametros[13].Value = null;

                //if (!String.IsNullOrEmpty(codigoComponente))
                //{

                //    varParametros[14] = new SqlParameter("@TiempoAtencion", SqlDbType.Int);
                //    varParametros[14].Value = tiempoAtencion;
                //    varParametros[15] = new SqlParameter("@CodigoComponente", SqlDbType.VarChar);
                //    varParametros[15].Value = codigoComponente;
                //    varParametros[16] = new SqlParameter("@RespuestasImagenes", SqlDbType.VarChar);
                //    varParametros[16].Value = strRespuestasImagenes;
                //}

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_Registrar", varParametros, TipoProcesamiento.DataReader, false);
                //bool hayInformacion = varDataReader.Read();
                varDataReader.Read();
                //new ErrorDA().GrabarLog(hayInformacion.ToString(), "WS", "00: CitaDA/RegistrarCita");
                Dictionary<string, string> varResultado = new Dictionary<string, string>();
                //if (hayInformacion)
                //{
                varResultado.Add("IDCita", varDataReader.GetInt32(varDataReader.GetOrdinal("IDCita")).ToString());
                varResultado.Add("Email", varDataReader.GetString(varDataReader.GetOrdinal("Email")));
                varResultado.Add("NombrePaciente", varDataReader.GetString(varDataReader.GetOrdinal("NombrePaciente")));
                varResultado.Add("ApellidoPaternoPaciente", varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaternoPaciente")));
                varResultado.Add("CelularPaciente", varDataReader.GetString(varDataReader.GetOrdinal("CelularPaciente")));
                varResultado.Add("NombreMedico", varDataReader.GetString(varDataReader.GetOrdinal("NombreMedico")));
                varResultado.Add("FechaAtencion", varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dddd dd/MM/yyyy", new CultureInfo("es-PE")));
                varResultado.Add("HoraInicio", varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")).ToString(@"hh\:mm"));
                varResultado.Add("Especialidad", varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")));
                varResultado.Add("Clinica", varDataReader.GetString(varDataReader.GetOrdinal("Clinica")));
                varResultado.Add("IDClinica", varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString());
                varResultado.Add("ApellidoMaternoPaciente", varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaternoPaciente")));
                varResultado.Add("TipoPago", varDataReader.GetString(varDataReader.GetOrdinal("TipoPago")));
                //varResultado.Add("ApellidoMaternoPaciente", "");
                varResultado.Add("LinkPago", "");
                //cambio marco
                varResultado.Add("IndicadorBotonPagar", varDataReader.GetString(varDataReader.GetOrdinal("IndicadorBotonPagar")));
                varResultado.Add("EsPrepago", varDataReader.GetString(varDataReader.GetOrdinal("EsPrepago")));
                varResultado.Add("IndEnviarCorreo", varDataReader.GetString(varDataReader.GetOrdinal("IndEnviarCorreo")));//Indicador para el envío de correo de Chequeo
                //}
                return varResultado;
            }
            catch (Exception ex)
            {

                new ErrorDA().RegistrarErrorV2(ex, "WS", "01: CitaDA/RegistrarCita");
                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }

        public Dictionary<string, string> RegistrarCitaAdicional(string idCitaSpring, string tipoDocumento, string numeroDocumento, string idHorario,
                                        string fecha, string numeroDia, string horaInicio, string origen, string observaciones, int tipoCita, bool esChequeo, 
                                        string idCitaOriginal = "", string origenOpcion = "")
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[13];
                varParametros[0] = new SqlParameter("@IDCita", SqlDbType.Int);
                varParametros[0].Value = idCitaSpring;
                varParametros[1] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[1].Value = tipoDocumento;
                varParametros[2] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[2].Value = numeroDocumento;
                varParametros[3] = new SqlParameter("@IDHorario", SqlDbType.Int);
                varParametros[3].Value = idHorario;
                varParametros[4] = new SqlParameter("@FechaAtencion", SqlDbType.Date);
                varParametros[4].Value = fecha;
                varParametros[5] = new SqlParameter("@NumeroDia", SqlDbType.Int);
                varParametros[5].Value = numeroDia;
                varParametros[6] = new SqlParameter("@HoraInicio", SqlDbType.Time);
                varParametros[6].Value = horaInicio;
                varParametros[7] = new SqlParameter("@Origen", SqlDbType.VarChar);
                varParametros[7].Value = origen;
                varParametros[8] = new SqlParameter("@Observaciones", SqlDbType.VarChar);
                varParametros[8].Value = observaciones;
                varParametros[9] = new SqlParameter("@TipoCita", SqlDbType.Int);
                varParametros[9].Value = tipoCita;
                varParametros[10] = new SqlParameter("@EsChequeo", SqlDbType.Bit);
                varParametros[10].Value = esChequeo;
                varParametros[11] = new SqlParameter("@IDCitaOriginal", SqlDbType.Int);
                if (!String.IsNullOrEmpty(idCitaOriginal)) varParametros[11].Value = idCitaOriginal;
                else varParametros[11].Value = null;
                varParametros[12] = new SqlParameter("@OrigenOpcion", SqlDbType.VarChar);
                if (!String.IsNullOrEmpty(origenOpcion)) varParametros[12].Value = origenOpcion;
                else varParametros[12].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_RegistrarAdicional", varParametros, TipoProcesamiento.DataReader, false);
                varDataReader.Read();
                Dictionary<string, string> varResultado = new Dictionary<string, string>();
                varResultado.Add("IDCita", varDataReader.GetInt32(varDataReader.GetOrdinal("IDCita")).ToString());
                varResultado.Add("Email", varDataReader.GetString(varDataReader.GetOrdinal("Email")));
                varResultado.Add("NombrePaciente", varDataReader.GetString(varDataReader.GetOrdinal("NombrePaciente")));
                varResultado.Add("ApellidoPaternoPaciente", varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaternoPaciente")));
                varResultado.Add("CelularPaciente", varDataReader.GetString(varDataReader.GetOrdinal("CelularPaciente")));
                varResultado.Add("NombreMedico", varDataReader.GetString(varDataReader.GetOrdinal("NombreMedico")));
                varResultado.Add("FechaAtencion", varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dddd dd/MM/yyyy", new CultureInfo("es-PE")));
                varResultado.Add("HoraInicio", varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")).ToString(@"hh\:mm"));
                varResultado.Add("Especialidad", varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")));
                varResultado.Add("Clinica", varDataReader.GetString(varDataReader.GetOrdinal("Clinica")));
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

        public CitasListadoBE ListarCitas(string tipoDocumento, string numeroDocumento, string año,
            string origen, string fechaCita, int indicadorHistoricas, int indicadorPendientes, bool soloTitular)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[8];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@Año", SqlDbType.VarChar);
                varParametros[2].Value = año;

                varParametros[3] = new SqlParameter("@Origen", SqlDbType.VarChar);
                varParametros[3].Value = origen;

                varParametros[4] = new SqlParameter("@FechaCita", SqlDbType.VarChar);
                varParametros[4].Value = fechaCita;

                varParametros[5] = new SqlParameter("@IndPendiente", SqlDbType.Bit);
                varParametros[5].Value = indicadorPendientes;

                varParametros[6] = new SqlParameter("@IndHistorico", SqlDbType.Bit);
                varParametros[6].Value = indicadorHistoricas;

                varParametros[7] = new SqlParameter("@SoloTitular", SqlDbType.Bit);
                varParametros[7].Value = soloTitular;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_Listar", varParametros, TipoProcesamiento.DataReader, false);

                CitasListadoBE varResultadoTemporal = new CitasListadoBE();
                CitasListadoBE varResultado = new CitasListadoBE();

                varResultadoTemporal.CitasPendientes = new List<CitaVistaPreviaBE>();
                CitaVistaPreviaBE oCitaVistaPreviaBE;
                string urlBase = ConfigurationManager.AppSettings["URLImagenes2"].ToString();
                string urlBaseIcono = ConfigurationManager.AppSettings["rutaPublicaIcono"].ToString();
                string fotoMedico;
                while (varDataReader.Read())
                {
                    oCitaVistaPreviaBE = new CitaVistaPreviaBE()
                    {
                        idCita = varDataReader.GetInt32(varDataReader.GetOrdinal("IDCita")).ToString(),
                        nombrePaciente = varDataReader.GetString(varDataReader.GetOrdinal("NombreUsuario")),
                        nombrePaciente2 = varDataReader.GetString(varDataReader.GetOrdinal("NombreUsuario2")),
                        cmp = varDataReader.GetString(varDataReader.GetOrdinal("CMP")),
                        nombreMedico = varDataReader.GetString(varDataReader.GetOrdinal("NombreMedico")),
                        //fechaAtencion = char.ToUpper(varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dddd dd/MM/yyyy", new CultureInfo("es-PE"))[0]) + varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dddd dd/MM/yyyy", new CultureInfo("es-PE")).Substring(1),
                        fechaAtencion = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dd/MM/yyyy"),
                        fechaOrdenamiento = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")),
                        horaInicio = varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")).ToString(@"hh\:mm"),
                        horaFin = varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraFin")).ToString(@"hh\:mm"),
                        idEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDEspecialidad")).ToString(),
                        especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")),
                        idClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString(),
                        clinica = varDataReader.GetString(varDataReader.GetOrdinal("Clinica")),
                        idMedicoFavorito = varDataReader.IsDBNull(varDataReader.GetOrdinal("IDMedicoFavorito")) ? null : varDataReader.GetInt32(varDataReader.GetOrdinal("IDMedicoFavorito")).ToString(),
                        //esCitaVirtual = varDataReader.GetString(varDataReader.GetOrdinal("EsCitaVirtual")),
                        esCitaVirtual = varDataReader.GetString(varDataReader.GetOrdinal("EsCitaVirtual")).Equals("1"),
                        fuePagado = varDataReader.IsDBNull(varDataReader.GetOrdinal("FuePagado")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("FuePagado")),
                        descripcionPago = varDataReader.GetString(varDataReader.GetOrdinal("DescripcionPago")),
                        tipoPago = varDataReader.GetString(varDataReader.GetOrdinal("TipoPago")),
                        tiempoPrevioCita = varDataReader.GetInt32(varDataReader.GetOrdinal("TiempoPrevioCita")),
                        anular = (varDataReader.GetString(varDataReader.GetOrdinal("Anular")) == "1") ? true : false,
                        anularPago = (varDataReader.GetString(varDataReader.GetOrdinal("AnularPago")) == "Si") ? true : false,
                        mostrarBotonesPago = (varDataReader.GetString(varDataReader.GetOrdinal("MostrarBotonesPago")) == "1") ? true : false,
                        mostrarBotonesPag = (varDataReader.GetString(varDataReader.GetOrdinal("MostrarBotonesPago")) == "1") ? true : false,
                        mostrarFilaEspera = true,
                        consultorio = (varDataReader.IsDBNull(varDataReader.GetOrdinal("Consultorio"))) ? " " : varDataReader.GetString(varDataReader.GetOrdinal("Consultorio")),
                        codigoTipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("CodigoTipoPaciente")),
                        tipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("TipoPaciente")),
                        codigoTipoAtencionHorario = varDataReader.GetString(varDataReader.GetOrdinal("CodigoTipoAtencionHorario")),
                        tipoAtencionHorario = varDataReader.GetString(varDataReader.GetOrdinal("TipoAtencionHorario")),
                        codigoTipoAtencionCita = varDataReader.GetString(varDataReader.GetOrdinal("CodigoTipoAtencionCita")),
                        tipoAtencionCita = varDataReader.GetString(varDataReader.GetOrdinal("TipoAtencionCita")),
                        tipoDocumento = varDataReader.GetInt32(varDataReader.GetOrdinal("TipoDocumento")),
                        numeroDocumento = varDataReader.GetString(varDataReader.GetOrdinal("NumeroDocumento")),
                        idHorario = varDataReader.GetInt32(varDataReader.GetOrdinal("IDHorario")),
                        cantidadPersonasDelante = varDataReader.GetString(varDataReader.GetOrdinal("CantidadPersonasDelante")),
                        subEspecialidad = varDataReader.GetString(varDataReader.GetOrdinal("SubEspecialidad")),
                        idEstado = varDataReader.GetInt32(varDataReader.GetOrdinal("IDEstado")),
                        estado = varDataReader.GetString(varDataReader.GetOrdinal("Estado")),
                        tiempoPrevioColaVirtual = varDataReader.GetString(varDataReader.GetOrdinal("TiempoPrevioColaVirtual")),
                        tiempoPrevioIngresoVirtual = varDataReader.GetString(varDataReader.GetOrdinal("TiempoPrevioIngresoVirtual")),
                        tiempoPosteriorIngresoVirtual = varDataReader.GetString(varDataReader.GetOrdinal("TiempoPosteriorIngresoVirtual")),
                        abreviaturaMedico = varDataReader.GetString(varDataReader.GetOrdinal("AbreviaturaMedico")),
                        sexoMedico = varDataReader.GetString(varDataReader.GetOrdinal("SexoMedico")),
                        metodoPago = varDataReader.GetString(varDataReader.GetOrdinal("MetodoPago")),
                        //fotoMedico = varDataReader.GetString(varDataReader.GetOrdinal("FotoMedico"))

                        esAdicional = varDataReader.GetBoolean(varDataReader.GetOrdinal("EsAdicional")),
                        textoAdicional = varDataReader.GetString(varDataReader.GetOrdinal("TextoAdicional"))
                    };
                    fotoMedico = varDataReader.GetString(varDataReader.GetOrdinal("FotoMedico"));
                    oCitaVistaPreviaBE.fotoMedico = !string.IsNullOrEmpty(fotoMedico) ? fotoMedico : (urlBase + "Medicos/" + (oCitaVistaPreviaBE.sexoMedico.Equals("M") ? "medico-m.png" : "medico-f.png"));
                    oCitaVistaPreviaBE.codigoAtencion = varDataReader.GetString(varDataReader.GetOrdinal("CodigoAtencion"));
                    oCitaVistaPreviaBE.codigoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("CodigoPaciente"));
                    oCitaVistaPreviaBE.idCitaProcedimiento = varDataReader.GetString(varDataReader.GetOrdinal("IdCitaProcemiento"));
                    if (!String.IsNullOrEmpty(oCitaVistaPreviaBE.codigoAtencion))
                    {
                        oCitaVistaPreviaBE.tieneReceta = varDataReader.GetBoolean(varDataReader.GetOrdinal("TieneReceta"));
                        if (oCitaVistaPreviaBE.tieneReceta)
                        {
                            oCitaVistaPreviaBE.iconoReceta = urlBaseIcono + "ico-receta.png";
                        }
                        oCitaVistaPreviaBE.tieneHojaRuta = varDataReader.GetBoolean(varDataReader.GetOrdinal("TieneHojaRuta"));
                        if (oCitaVistaPreviaBE.tieneHojaRuta)
                        {
                            oCitaVistaPreviaBE.iconoHojaRuta = urlBaseIcono + "ico-hoja-de-ruta.png";
                        }
                    }
                    if (!string.IsNullOrEmpty(oCitaVistaPreviaBE.idCitaProcedimiento))
                    {
                        oCitaVistaPreviaBE.procedimiento = varDataReader.GetString(varDataReader.GetOrdinal("Procedimiento"));
                        oCitaVistaPreviaBE.idServicioHorario = varDataReader.GetString(varDataReader.GetOrdinal("IdServicioHorario"));
                        oCitaVistaPreviaBE.idServicio = varDataReader.GetString(varDataReader.GetOrdinal("IdServicio"));
                        oCitaVistaPreviaBE.servicio = varDataReader.GetString(varDataReader.GetOrdinal("Servicio"));
                        oCitaVistaPreviaBE.tipoTerapia = varDataReader.GetString(varDataReader.GetOrdinal("TipoTerapia"));
                        oCitaVistaPreviaBE.numeroSesion = varDataReader.GetString(varDataReader.GetOrdinal("NumeroSesion"));
                    }
                    varResultadoTemporal.CitasPendientes.Add(oCitaVistaPreviaBE);
                }
                varResultado.CitasPendientes = varResultadoTemporal.CitasPendientes.OrderBy(p => p.fechaOrdenamiento).ThenBy(p => p.horaInicio).ToList();
                varDataReader.NextResult();
                varResultadoTemporal.CitasHistoricas = new List<CitaHistoricaVistaPreviaBE>();
                CitaHistoricaVistaPreviaBE oCitaHistoricaVistaPreviaBE;
                while (varDataReader.Read())
                {
                    oCitaHistoricaVistaPreviaBE = new CitaHistoricaVistaPreviaBE()
                    {
                        nombrePaciente = varDataReader.GetString(varDataReader.GetOrdinal("NombreUsuario")),
                        nombrePaciente2 = varDataReader.GetString(varDataReader.GetOrdinal("NombreUsuario2")),
                        cmp = varDataReader.GetString(varDataReader.GetOrdinal("CMP")),
                        nombreMedico = varDataReader.GetString(varDataReader.GetOrdinal("NombreMedico")),
                        //fechaAtencion = char.ToUpper(varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dddd dd/MM/yyyy", new CultureInfo("es-PE"))[0]) + varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dddd dd/MM/yyyy", new CultureInfo("es-PE")).Substring(1),
                        fechaAtencion = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dd/MM/yyyy"),
                        fechaOrdenamiento = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")),
                        horaInicio = varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")).ToString(@"hh\:mm"),
                        horaFin = varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraFin")).ToString(@"hh\:mm"),
                        idEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDEspecialidad")).ToString(),
                        especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")),
                        subEspecialidad = varDataReader.GetString(varDataReader.GetOrdinal("SubEspecialidad")),
                        clinica = varDataReader.GetString(varDataReader.GetOrdinal("Clinica")),
                        idEstado = varDataReader.GetInt32(varDataReader.GetOrdinal("IDEstado")),
                        estado = varDataReader.GetString(varDataReader.GetOrdinal("Estado")).ToString(),
                        idMedicoFavorito = varDataReader.IsDBNull(varDataReader.GetOrdinal("IDMedicoFavorito")) ? null : varDataReader.GetInt32(varDataReader.GetOrdinal("IDMedicoFavorito")).ToString(),
                        //esCitaVirtual = varDataReader.GetString(varDataReader.GetOrdinal("EsCitaVirtual")),
                        esCitaVirtual = varDataReader.GetString(varDataReader.GetOrdinal("EsCitaVirtual")).Equals("1"),
                        fuePagado = varDataReader.IsDBNull(varDataReader.GetOrdinal("FuePagado")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("FuePagado")),
                        descripcionPago = varDataReader.GetString(varDataReader.GetOrdinal("DescripcionPago")),
                        idCita = varDataReader.IsDBNull(varDataReader.GetOrdinal("IDCita")) ? null : varDataReader.GetInt32(varDataReader.GetOrdinal("IDCita")).ToString(),
                        //resultadoLaboratorio = (varDataReader.GetInt32(varDataReader.GetOrdinal("ResultadoLaboratorio")).ToString().Equals("1") ? true : false),
                        //peticion = varDataReader.IsDBNull(varDataReader.GetOrdinal("Peticion")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Peticion")),
                        //indRecetaMedica = (varDataReader.GetInt32(varDataReader.GetOrdinal("IndRecetaMedica")).ToString().Equals("1") ? true : false),
                        //numeroOrdenAtencion = varDataReader.IsDBNull(varDataReader.GetOrdinal("NumeroOrdenAtencion")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("NumeroOrdenAtencion"))
                        consultorio = (varDataReader.IsDBNull(varDataReader.GetOrdinal("Consultorio"))) ? " " : varDataReader.GetString(varDataReader.GetOrdinal("Consultorio")),
                        codigoTipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("CodigoTipoPaciente")),
                        tipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("TipoPaciente")),
                        codigoTipoAtencionHorario = varDataReader.GetString(varDataReader.GetOrdinal("CodigoTipoAtencionHorario")),
                        tipoAtencionHorario = varDataReader.GetString(varDataReader.GetOrdinal("TipoAtencionHorario")),
                        codigoTipoAtencionCita = varDataReader.GetString(varDataReader.GetOrdinal("CodigoTipoAtencionCita")),
                        tipoAtencionCita = varDataReader.GetString(varDataReader.GetOrdinal("TipoAtencionCita")),
                        abreviaturaMedico = varDataReader.GetString(varDataReader.GetOrdinal("AbreviaturaMedico")),
                        sexoMedico = varDataReader.GetString(varDataReader.GetOrdinal("SexoMedico")),
                        metodoPago = varDataReader.GetString(varDataReader.GetOrdinal("MetodoPago")),
                        idHorario = varDataReader.GetInt32(varDataReader.GetOrdinal("IDHorario")),
                        tipoDocumento = varDataReader.GetInt32(varDataReader.GetOrdinal("TipoDocumento")),
                        numeroDocumento = varDataReader.GetString(varDataReader.GetOrdinal("NumeroDocumento")),

                        esAdicional = varDataReader.GetBoolean(varDataReader.GetOrdinal("EsAdicional")),
                        textoAdicional = varDataReader.GetString(varDataReader.GetOrdinal("TextoAdicional"))
                    };
                    fotoMedico = varDataReader.GetString(varDataReader.GetOrdinal("FotoMedico"));
                    oCitaHistoricaVistaPreviaBE.fotoMedico = !string.IsNullOrEmpty(fotoMedico) ? fotoMedico : (urlBase + "Medicos/" + (oCitaHistoricaVistaPreviaBE.sexoMedico.Equals("M") ? "medico-m.png" : "medico-f.png"));
                    oCitaHistoricaVistaPreviaBE.codigoAtencion = varDataReader.GetString(varDataReader.GetOrdinal("CodigoAtencion"));
                    oCitaHistoricaVistaPreviaBE.codigoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("CodigoPaciente"));
                    oCitaHistoricaVistaPreviaBE.idCitaProcedimiento = varDataReader.GetString(varDataReader.GetOrdinal("IdCitaProcemiento"));
                    if (!String.IsNullOrEmpty(oCitaHistoricaVistaPreviaBE.codigoAtencion))
                    {
                        oCitaHistoricaVistaPreviaBE.tieneReceta = varDataReader.GetBoolean(varDataReader.GetOrdinal("TieneReceta"));
                        if (oCitaHistoricaVistaPreviaBE.tieneReceta)
                        {
                            oCitaHistoricaVistaPreviaBE.iconoReceta = urlBaseIcono + "ico-receta.png";
                        }
                        oCitaHistoricaVistaPreviaBE.tieneHojaRuta = varDataReader.GetBoolean(varDataReader.GetOrdinal("TieneHojaRuta"));
                        if (oCitaHistoricaVistaPreviaBE.tieneHojaRuta)
                        {
                            oCitaHistoricaVistaPreviaBE.iconoHojaRuta = urlBaseIcono + "ico-hoja-de-ruta.png";
                        }
                    }
                    if (!string.IsNullOrEmpty(oCitaHistoricaVistaPreviaBE.idCitaProcedimiento))
                    {
                        oCitaHistoricaVistaPreviaBE.procedimiento = varDataReader.GetString(varDataReader.GetOrdinal("Procedimiento"));
                        oCitaHistoricaVistaPreviaBE.idServicioHorario = varDataReader.GetString(varDataReader.GetOrdinal("IdServicioHorario"));
                        oCitaHistoricaVistaPreviaBE.idServicio = varDataReader.GetString(varDataReader.GetOrdinal("IdServicio"));
                        oCitaHistoricaVistaPreviaBE.servicio = varDataReader.GetString(varDataReader.GetOrdinal("Servicio"));
                        oCitaHistoricaVistaPreviaBE.tipoTerapia = varDataReader.GetString(varDataReader.GetOrdinal("TipoTerapia"));
                        oCitaHistoricaVistaPreviaBE.numeroSesion = varDataReader.GetString(varDataReader.GetOrdinal("NumeroSesion"));
                    }
                    varResultadoTemporal.CitasHistoricas.Add(oCitaHistoricaVistaPreviaBE);
                }
                varResultado.CitasHistoricas = varResultadoTemporal.CitasHistoricas.OrderByDescending(p => p.fechaOrdenamiento).ThenByDescending(p => p.horaInicio).ToList();
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

        public ValidarAnulacionCitaBE ValidarAnulacionCita(string tipoDocumento, string numeroDocumento, string idCita, string origen, bool indReprogramacion = false)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[5];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@IDCita", SqlDbType.Int);
                varParametros[2].Value = idCita;
                varParametros[3] = new SqlParameter("@Origen", SqlDbType.VarChar);
                varParametros[3].Value = origen;
                varParametros[4] = new SqlParameter("@IndReprogramacion", SqlDbType.Bit);
                varParametros[4].Value = indReprogramacion;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_ValidarAnulacion", varParametros, TipoProcesamiento.DataReader, false);
                varDataReader.Read();
                ValidarAnulacionCitaBE varResultado = new ValidarAnulacionCitaBE();
                varResultado.IDClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica"));
                varResultado.IDCitaClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDCitaSpring"));
                return varResultado;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public void AnularCita(string tipoDocumento, string numeroDocumento, string idCita, string origen)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[4];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@IDCita", SqlDbType.Int);
                varParametros[2].Value = idCita;
                varParametros[3] = new SqlParameter("@Origen", SqlDbType.VarChar);
                if (!String.IsNullOrEmpty(origen)) varParametros[3].Value = origen;
                else varParametros[3].Value = null;

                varConexion.EjecutarProcedimiento("App_Proc_Cita_Anular", varParametros, TipoProcesamiento.NonQuery);
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
            }
        }

        public Dictionary<string, string> RegistrarCitaVirtual(string tipoDocumento, string numeroDocumento, string idHorario,
                                        string fecha, string numeroDia, string numeroTurno, string pregunta1, string respuesta1,
                                        string pregunta2, string respuesta2, string origen,
                                        string tieneAlergia, string descripcionAlergia, string tipoPago, bool tarifaSeguro, string idCita, DatosResponseSited datosSited, bool programa,
                                        string roomName, string horaInicio)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {

                SqlParameter[] varParametros = new SqlParameter[17];

                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@IDHorario", SqlDbType.Int);
                varParametros[2].Value = idHorario;
                varParametros[3] = new SqlParameter("@FechaAtencion", SqlDbType.Date);
                varParametros[3].Value = fecha;
                varParametros[4] = new SqlParameter("@NumeroDia", SqlDbType.Int);
                varParametros[4].Value = numeroDia;
                varParametros[5] = new SqlParameter("@Turno", SqlDbType.Int);
                varParametros[5].Value = numeroTurno;
                varParametros[6] = new SqlParameter("@Pregunta1", SqlDbType.VarChar);
                varParametros[6].Value = pregunta1;
                varParametros[7] = new SqlParameter("@Respuesta1", SqlDbType.VarChar);
                varParametros[7].Value = respuesta1;
                varParametros[8] = new SqlParameter("@Pregunta2", SqlDbType.VarChar);
                varParametros[8].Value = pregunta2;
                varParametros[9] = new SqlParameter("@Respuesta2", SqlDbType.VarChar);
                varParametros[9].Value = respuesta2;
                varParametros[10] = new SqlParameter("@Origen", SqlDbType.VarChar);
                varParametros[10].Value = origen;
                varParametros[11] = new SqlParameter("@TieneAlergia", SqlDbType.Bit);
                varParametros[11].Value = tieneAlergia;
                varParametros[12] = new SqlParameter("@DescripcionAlergia", SqlDbType.VarChar);
                varParametros[12].Value = descripcionAlergia;
                varParametros[13] = new SqlParameter("@Monto", SqlDbType.Decimal);
                varParametros[13].Value = datosSited.monto;
                varParametros[14] = new SqlParameter("@Moneda", SqlDbType.Int);
                varParametros[14].Value = datosSited.codTipoMoneda;
                varParametros[15] = new SqlParameter("@TarifaSeguro", SqlDbType.Bit);
                varParametros[15].Value = tarifaSeguro;
                varParametros[16] = new SqlParameter("@RoomName", SqlDbType.VarChar);
                varParametros[16].Value = roomName;
                
                //varParametros[17] = new SqlParameter("@HoraInicio", SqlDbType.VarChar);
                //if (!string.IsNullOrEmpty(horaInicio)) varParametros[17].Value = horaInicio;
                //else varParametros[17].Value = null;

                /*varParametros[16] = new SqlParameter("@IDCitaSpring", SqlDbType.Int);
                varParametros[16].Value = idCita;
                varParametros[17] = new SqlParameter("@RUCSeguroWs", SqlDbType.VarChar);
                varParametros[17].Value = datosSited.seguro;
                varParametros[18] = new SqlParameter("@CodProductoWs", SqlDbType.VarChar);
                varParametros[18].Value = datosSited.codProducto;
                varParametros[19] = new SqlParameter("@DesProductoWs", SqlDbType.VarChar);
                varParametros[19].Value = datosSited.desProducto;
                varParametros[20] = new SqlParameter("@CodCoberturaWs", SqlDbType.VarChar);
                varParametros[20].Value = datosSited.codCobertura;
                varParametros[21] = new SqlParameter("@DesCoberturaWs", SqlDbType.VarChar);
                varParametros[21].Value = datosSited.desCobertura;
                varParametros[22] = new SqlParameter("@IAFASWs", SqlDbType.VarChar);
                varParametros[22].Value = datosSited.iafas;
                varParametros[23] = new SqlParameter("@CodAseguradoWs", SqlDbType.VarChar);
                varParametros[23].Value = datosSited.codAsegurado;
                varParametros[24] = new SqlParameter("@Programa", SqlDbType.Bit);
                varParametros[24].Value = programa;*/

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_CitaVirtual_Registrar", varParametros, TipoProcesamiento.DataReader, false);
                varDataReader.Read();
                Dictionary<string, string> varResultado = new Dictionary<string, string>();
                varResultado.Add("TipoDocumento", tipoDocumento);
                varResultado.Add("NumeroDocumento", numeroDocumento);
                varResultado.Add("Pregunta1", pregunta1);
                varResultado.Add("Respuesta1", respuesta1);
                varResultado.Add("Pregunta2", pregunta2);
                varResultado.Add("Respuesta2", respuesta2);
                Debug.WriteLine(varDataReader.GetString(varDataReader.GetOrdinal("Email")));
                varResultado.Add("Email", varDataReader.GetString(varDataReader.GetOrdinal("Email")));
                varResultado.Add("NombrePaciente", varDataReader.GetString(varDataReader.GetOrdinal("NombrePaciente")));
                varResultado.Add("ApellidoPaternoPaciente", varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaternoPaciente")));
                varResultado.Add("ApellidoMaternoPaciente", varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaternoPaciente")));
                DateTime varFechaNacimiento = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaNacimiento"));
                varResultado.Add("FechaNacimiento", varFechaNacimiento.ToString("dd/MM/yyyy"));
                varResultado.Add("EdadPaciente", (DateTime.Now.Year - varFechaNacimiento.Year).ToString());
                varResultado.Add("Sexo", varDataReader.GetString(varDataReader.GetOrdinal("Genero")));
                varResultado.Add("CelularPaciente", varDataReader.GetString(varDataReader.GetOrdinal("CelularPaciente")));
                varResultado.Add("NombreMedico", varDataReader.GetString(varDataReader.GetOrdinal("NombreMedico")));
                varResultado.Add("FechaAtencion", varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dddd dd/MM/yyyy", new CultureInfo("es-PE")));
                varResultado.Add("HoraInicio", varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")).ToString(@"hh\:mm"));
                varResultado.Add("Especialidad", varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")));
                varResultado.Add("Clinica", varDataReader.GetString(varDataReader.GetOrdinal("Clinica")));
                varResultado.Add("IDClinica", varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString());
                varResultado.Add("IDCitaVirtual", varDataReader.GetInt32(varDataReader.GetOrdinal("IDCitaVirtual")).ToString());
                varResultado.Add("TipoPago", varDataReader.GetString(varDataReader.GetOrdinal("TipoPago")));
                varResultado.Add("EmailMedico", varDataReader.GetString(varDataReader.GetOrdinal("EmailMedico")));
                varResultado.Add("EmailPagos", varDataReader.GetString(varDataReader.GetOrdinal("EmailPagos")));
                varResultado.Add("Seguro", (varDataReader.GetString(varDataReader.GetOrdinal("TipoPago")) == "PostPago") ? null : (varDataReader.IsDBNull(varDataReader.GetOrdinal("Seguro"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Seguro")));
                varResultado.Add("Direccion", varDataReader.GetString(varDataReader.GetOrdinal("Direccion")));
                varResultado.Add("Monto", (varDataReader.IsDBNull(varDataReader.GetOrdinal("Monto"))) ? null : varDataReader.GetDecimal(varDataReader.GetOrdinal("Monto")).ToString("0.00"));
                varResultado.Add("LinkPago", "");

                varResultado.Add("IndicadorBotonPagar", varDataReader.GetString(varDataReader.GetOrdinal("IndicadorBotonPagar")));
                varResultado.Add("EsPrepago", varDataReader.GetString(varDataReader.GetOrdinal("EsPrepago")));
                varResultado.Add("RoomName", varDataReader.GetString(varDataReader.GetOrdinal("RoomName")));

                return varResultado;
            }
            catch (Exception e)
            {

                Debug.WriteLine(e);
                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }
        public Dictionary<string, string> RegistrarCitaVirtualAdicional(string tipoDocumento, string numeroDocumento, string idHorario,
                                        string fecha, string numeroDia, string hora, string origen, string pregunta1, string respuesta1,
                                        string pregunta2, string respuesta2,
                                        string tieneAlergia, string descripcionAlergia, string montoDefinido, string tipoPago, bool esChequeo)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                string montoAux = null;
                Debug.WriteLine(montoDefinido);
                if (montoDefinido != null)
                {
                    if (montoDefinido.Split('-').Length != 2)
                    {
                        montoDefinido = null;
                    }
                    else
                    {
                        montoAux = montoDefinido;
                    }
                }
                else
                {
                    montoAux = null;
                }

                SqlParameter[] varParametros = new SqlParameter[16];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@IDHorario", SqlDbType.Int);
                varParametros[2].Value = idHorario;
                varParametros[3] = new SqlParameter("@FechaAtencion", SqlDbType.Date);
                varParametros[3].Value = fecha;
                varParametros[4] = new SqlParameter("@NumeroDia", SqlDbType.Int);
                varParametros[4].Value = numeroDia;
                varParametros[5] = new SqlParameter("@HoraInicio", SqlDbType.Time);
                varParametros[5].Value = hora;
                varParametros[6] = new SqlParameter("@Origen", SqlDbType.VarChar);
                varParametros[6].Value = origen;
                varParametros[7] = new SqlParameter("@EsChequeo", SqlDbType.Bit);
                varParametros[7].Value = esChequeo;
                varParametros[8] = new SqlParameter("@Pregunta1", SqlDbType.VarChar);
                varParametros[8].Value = pregunta1;
                varParametros[9] = new SqlParameter("@Respuesta1", SqlDbType.VarChar);
                varParametros[9].Value = respuesta1;
                varParametros[10] = new SqlParameter("@Pregunta2", SqlDbType.VarChar);
                varParametros[10].Value = pregunta2;
                varParametros[11] = new SqlParameter("@Respuesta2", SqlDbType.VarChar);
                varParametros[11].Value = respuesta2;
                varParametros[12] = new SqlParameter("@TieneAlergia", SqlDbType.Bit);
                varParametros[12].Value = tieneAlergia;
                varParametros[13] = new SqlParameter("@DescripcionAlergia", SqlDbType.VarChar);
                varParametros[13].Value = descripcionAlergia;
                varParametros[14] = new SqlParameter("@Monto", SqlDbType.Decimal);
                varParametros[14].Value = (montoAux != null) ? montoAux.Split('-')[0] : null;
                varParametros[15] = new SqlParameter("@Moneda", SqlDbType.Int);
                varParametros[15].Value = (montoAux != null) ? montoAux.Split('-')[1] : null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_CitaVirtual_RegistrarAdicional", varParametros, TipoProcesamiento.DataReader, false);
                varDataReader.Read();
                Dictionary<string, string> varResultado = new Dictionary<string, string>();
                Debug.WriteLine("test1");
                varResultado.Add("TipoDocumento", tipoDocumento);
                varResultado.Add("NumeroDocumento", numeroDocumento);
                varResultado.Add("Pregunta1", pregunta1);
                varResultado.Add("Respuesta1", respuesta1);
                varResultado.Add("Pregunta2", pregunta2);
                varResultado.Add("Respuesta2", respuesta2);
                Debug.WriteLine(varDataReader.GetString(varDataReader.GetOrdinal("Email")));
                varResultado.Add("Email", varDataReader.GetString(varDataReader.GetOrdinal("Email")));
                varResultado.Add("NombrePaciente", varDataReader.GetString(varDataReader.GetOrdinal("NombrePaciente")));
                varResultado.Add("ApellidoPaternoPaciente", varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaternoPaciente")));
                varResultado.Add("ApellidoMaternoPaciente", varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaternoPaciente")));
                DateTime varFechaNacimiento = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaNacimiento"));
                varResultado.Add("FechaNacimiento", varFechaNacimiento.ToString("dd/MM/yyyy"));
                varResultado.Add("EdadPaciente", (DateTime.Now.Year - varFechaNacimiento.Year).ToString());
                varResultado.Add("Sexo", varDataReader.GetString(varDataReader.GetOrdinal("Genero")));
                varResultado.Add("CelularPaciente", varDataReader.GetString(varDataReader.GetOrdinal("CelularPaciente")));
                varResultado.Add("NombreMedico", varDataReader.GetString(varDataReader.GetOrdinal("NombreMedico")));
                varResultado.Add("FechaAtencion", varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dddd dd/MM/yyyy", new CultureInfo("es-PE")));
                varResultado.Add("HoraInicio", varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")).ToString(@"hh\:mm"));
                varResultado.Add("Especialidad", varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")));
                varResultado.Add("Clinica", varDataReader.GetString(varDataReader.GetOrdinal("Clinica")));
                varResultado.Add("IDClinica", varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString());
                varResultado.Add("IDCitaVirtual", varDataReader.GetInt32(varDataReader.GetOrdinal("IDCitaVirtual")).ToString());
                varResultado.Add("TipoPago", varDataReader.GetString(varDataReader.GetOrdinal("TipoPago")));
                varResultado.Add("EmailMedico", varDataReader.GetString(varDataReader.GetOrdinal("EmailMedico")));
                varResultado.Add("Seguro", varDataReader.GetString(varDataReader.GetOrdinal("Seguro")));
                varResultado.Add("Direccion", varDataReader.GetString(varDataReader.GetOrdinal("Direccion")));
                return varResultado;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }

        public Dictionary<string, string> AnularCitaVirtual(string tipoDocumento, string numeroDocumento, string idCitaVirtual, string origen)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[4];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@IDCitaVirtual", SqlDbType.Int);
                varParametros[2].Value = idCitaVirtual;
                varParametros[3] = new SqlParameter("@Origen", SqlDbType.VarChar);
                if (!String.IsNullOrEmpty(origen)) varParametros[3].Value = origen;
                else varParametros[3].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_CitaVirtual_Anular", varParametros, TipoProcesamiento.DataReader, false);
                varDataReader.Read();
                Dictionary<string, string> varResultado = new Dictionary<string, string>();
                varResultado.Add("TipoDocumento", tipoDocumento);
                varResultado.Add("NumeroDocumento", numeroDocumento);
                varResultado.Add("Pregunta1", varDataReader.GetString(varDataReader.GetOrdinal("Pregunta1")));
                varResultado.Add("Respuesta1", varDataReader.GetString(varDataReader.GetOrdinal("Respuesta1")));
                varResultado.Add("Pregunta2", varDataReader.GetString(varDataReader.GetOrdinal("Pregunta2")));
                varResultado.Add("Respuesta2", varDataReader.GetString(varDataReader.GetOrdinal("Respuesta2")));

                varResultado.Add("Email", varDataReader.GetString(varDataReader.GetOrdinal("Email")));
                varResultado.Add("NombrePaciente", varDataReader.GetString(varDataReader.GetOrdinal("NombrePaciente")));
                varResultado.Add("ApellidoPaternoPaciente", varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaternoPaciente")));
                varResultado.Add("ApellidoMaternoPaciente", varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaternoPaciente")));
                DateTime varFechaNacimiento = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaNacimiento"));
                varResultado.Add("FechaNacimiento", varFechaNacimiento.ToString("dd/MM/yyyy"));
                varResultado.Add("EdadPaciente", (DateTime.Now.Year - varFechaNacimiento.Year).ToString());
                varResultado.Add("Sexo", varDataReader.GetString(varDataReader.GetOrdinal("Genero")));
                varResultado.Add("CelularPaciente", varDataReader.GetString(varDataReader.GetOrdinal("CelularPaciente")));
                varResultado.Add("NombreMedico", varDataReader.GetString(varDataReader.GetOrdinal("NombreMedico")));
                varResultado.Add("FechaAtencion", varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dddd dd/MM/yyyy", new CultureInfo("es-PE")));
                varResultado.Add("HoraInicio", varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")).ToString(@"hh\:mm"));
                varResultado.Add("Especialidad", varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")));
                varResultado.Add("Clinica", varDataReader.GetString(varDataReader.GetOrdinal("Clinica")));
                varResultado.Add("IDCitaVirtual", varDataReader.GetInt32(varDataReader.GetOrdinal("IDCitaVirtual")).ToString());
                varResultado.Add("TipoPago", varDataReader.GetString(varDataReader.GetOrdinal("TipoPago")));
                varResultado.Add("EmailMedico", varDataReader.GetString(varDataReader.GetOrdinal("EmailMedico")));
                varResultado.Add("Seguro", varDataReader.GetString(varDataReader.GetOrdinal("Seguro")));
                varResultado.Add("Direccion", varDataReader.GetString(varDataReader.GetOrdinal("Direccion")));
                return varResultado;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public Dictionary<string, string> ReprogramarCitaVirtual(string tipoDocumento, string numeroDocumento, string idCitaVirtual,
                                        string idHorario, string fecha, string numeroDia, string numeroTurno, string pregunta1, string respuesta1,
                                        string pregunta2, string respuesta2,
                                        string tieneAlergia, string descripcionAlergia, string origen, bool indReprogramacion = false)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[15];

                varParametros[2] = new SqlParameter("@IDCitaVirtual", SqlDbType.Int);
                varParametros[2].Value = idCitaVirtual;
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[3] = new SqlParameter("@IDHorario", SqlDbType.Int);
                varParametros[3].Value = idHorario;
                varParametros[4] = new SqlParameter("@FechaAtencion", SqlDbType.Date);
                varParametros[4].Value = fecha;
                varParametros[5] = new SqlParameter("@NumeroDia", SqlDbType.Int);
                varParametros[5].Value = numeroDia;
                varParametros[6] = new SqlParameter("@Turno", SqlDbType.Int);
                varParametros[6].Value = numeroTurno;
                varParametros[7] = new SqlParameter("@Pregunta1", SqlDbType.VarChar);
                varParametros[7].Value = pregunta1;
                varParametros[8] = new SqlParameter("@Respuesta1", SqlDbType.VarChar);
                varParametros[8].Value = respuesta1;
                varParametros[9] = new SqlParameter("@Pregunta2", SqlDbType.VarChar);
                varParametros[9].Value = pregunta2;
                varParametros[10] = new SqlParameter("@Respuesta2", SqlDbType.VarChar);
                varParametros[10].Value = respuesta2;
                varParametros[11] = new SqlParameter("@TieneAlergia", SqlDbType.Bit);
                varParametros[11].Value = tieneAlergia;
                varParametros[12] = new SqlParameter("@DescripcionAlergia", SqlDbType.VarChar);
                varParametros[12].Value = descripcionAlergia;
                varParametros[13] = new SqlParameter("@IndReprogramacion", SqlDbType.Bit);
                varParametros[13].Value = indReprogramacion;
                varParametros[14] = new SqlParameter("@Origen", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(origen)) varParametros[14].Value = origen;
                else varParametros[14].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_CitaVirtual_Reprogramar", varParametros, TipoProcesamiento.DataReader, false);
                varDataReader.Read();
                Dictionary<string, string> varResultado = new Dictionary<string, string>();
                varResultado.Add("TipoDocumento", tipoDocumento);
                varResultado.Add("NumeroDocumento", numeroDocumento);
                varResultado.Add("Pregunta1", pregunta1);
                varResultado.Add("Respuesta1", respuesta1);
                varResultado.Add("Pregunta2", pregunta2);
                varResultado.Add("Respuesta2", respuesta2);

                varResultado.Add("FechaAtencionOriginal", varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencionOriginal")).ToString("dddd dd/MM/yyyy", new CultureInfo("es-PE")));
                varResultado.Add("HoraInicioOriginal", varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicioOriginal")).ToString(@"hh\:mm"));

                varResultado.Add("Email", varDataReader.GetString(varDataReader.GetOrdinal("Email")));
                varResultado.Add("NombrePaciente", varDataReader.GetString(varDataReader.GetOrdinal("NombrePaciente")));
                varResultado.Add("ApellidoPaternoPaciente", varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaternoPaciente")));
                varResultado.Add("ApellidoMaternoPaciente", varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaternoPaciente")));
                DateTime varFechaNacimiento = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaNacimiento"));
                varResultado.Add("FechaNacimiento", varFechaNacimiento.ToString("dd/MM/yyyy"));
                varResultado.Add("EdadPaciente", (DateTime.Now.Year - varFechaNacimiento.Year).ToString());
                varResultado.Add("Sexo", varDataReader.GetString(varDataReader.GetOrdinal("Genero")));
                varResultado.Add("CelularPaciente", varDataReader.GetString(varDataReader.GetOrdinal("CelularPaciente")));
                varResultado.Add("NombreMedico", varDataReader.GetString(varDataReader.GetOrdinal("NombreMedico")));
                varResultado.Add("FechaAtencion", varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dddd dd/MM/yyyy", new CultureInfo("es-PE")));
                varResultado.Add("HoraInicio", varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")).ToString(@"hh\:mm"));
                varResultado.Add("Especialidad", varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")));
                varResultado.Add("Clinica", varDataReader.GetString(varDataReader.GetOrdinal("Clinica")));
                varResultado.Add("IDClinica", varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString());
                varResultado.Add("IDCitaVirtual", varDataReader.GetInt32(varDataReader.GetOrdinal("IDCitaVirtual")).ToString());
                varResultado.Add("TipoPago", varDataReader.GetString(varDataReader.GetOrdinal("TipoPago")));
                varResultado.Add("EmailMedico", varDataReader.GetString(varDataReader.GetOrdinal("EmailMedico")));
                varResultado.Add("Seguro", varDataReader.GetString(varDataReader.GetOrdinal("Seguro")));
                varResultado.Add("Direccion", varDataReader.GetString(varDataReader.GetOrdinal("Direccion")));

                varResultado.Add("IndicadorBotonPagar", varDataReader.GetString(varDataReader.GetOrdinal("IndicadorBotonPagar")));
                varResultado.Add("EsPrepago", varDataReader.GetString(varDataReader.GetOrdinal("EsPrepago")));

                return varResultado;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public DatosPagoBE ObtenerDatosPago(string idCita, string idCitaVirtual, string canal, string rucSeguro = "")
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                if (!string.IsNullOrEmpty(idCita) && string.IsNullOrEmpty(idCitaVirtual))
                {
                    SqlParameter[] varParametros = new SqlParameter[2];
                    varParametros[0] = new SqlParameter("@IDCita", SqlDbType.Int);
                    varParametros[0].Value = idCita;
                    varParametros[1] = new SqlParameter("@Canal", SqlDbType.VarChar);
                    varParametros[1].Value = canal;



                    varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_DatosPago", varParametros, TipoProcesamiento.DataReader, false);
                }
                if (string.IsNullOrEmpty(idCita) && !string.IsNullOrEmpty(idCitaVirtual))
                {
                    SqlParameter[] varParametros = new SqlParameter[3];
                    varParametros[0] = new SqlParameter("@IDCitaVirtual", SqlDbType.Int);
                    varParametros[0].Value = idCitaVirtual;
                    varParametros[1] = new SqlParameter("@Canal", SqlDbType.VarChar);
                    varParametros[1].Value = canal;
                    varParametros[2] = new SqlParameter("@RucSeguro", SqlDbType.VarChar);
                    if (!string.IsNullOrEmpty(rucSeguro)) varParametros[2].Value = rucSeguro;
                    else varParametros[2].Value = null;

                    varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_CitaVirtual_DatosPago", varParametros, TipoProcesamiento.DataReader, false);
                }


                DatosPagoBE varResultado = new DatosPagoBE();
                varDataReader.Read();

                varResultado = new DatosPagoBE()
                {
                    monto = varDataReader.GetDecimal(varDataReader.GetOrdinal("Monto")).ToString("0.00"),
                    user = varDataReader.GetString(varDataReader.GetOrdinal("UserVisa")).ToString(),
                    password = varDataReader.GetString(varDataReader.GetOrdinal("PasswordVisa")).ToString(),
                    merchant = varDataReader.GetString(varDataReader.GetOrdinal("MerchantVisa")).ToString(),
                    purchaseNumber = varDataReader.GetInt64(varDataReader.GetOrdinal("CodeTransaccion")).ToString(),
                    especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")).ToString(),
                    nombreMedico = varDataReader.GetString(varDataReader.GetOrdinal("NombreMedico")).ToString(),
                    nombrePaciente = varDataReader.GetString(varDataReader.GetOrdinal("NombrePaciente")).ToString(),
                    fechaAtencion = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dd/MM/yyyy"),
                    horaInicio = varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")).ToString(@"hh\:mm"),
                    motivoCobro = varDataReader.GetString(varDataReader.GetOrdinal("MotivoCobro")).ToString(),
                    tokenEmail = varDataReader.GetString(varDataReader.GetOrdinal("DataUserToken")).ToString(),
                    clinica = varDataReader.GetString(varDataReader.GetOrdinal("Clinica")).ToString(),
                    aseguradora = varDataReader.GetString(varDataReader.GetOrdinal("Aseguradora")).ToString(),
                    consultorio = varDataReader.GetString(varDataReader.GetOrdinal("Consultorio")).ToString(),
                    tiempoAtencion = varDataReader.GetString(varDataReader.GetOrdinal("TiempoAtencion")).ToString()

                };


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

        public RegistroPagoBE RegistrarPagoCita(string idCita, string idCitaVirtual, string codigo, string mensaje, string fecha,
                                                    string hora, long purchaseNumber, string transactionID, string numeroTarjeta,
                                                    string deseaBoleta, string ruc, string razonSocial, string direccion, string origen,
                                                    string monto, string IDUnico, string tokenEmail, string nombreVisa, string apellidoVisa,
                                                    string firma, string tipoTarjeta, string tipoDocumentoBoleta, string numeroDocumentoBoleta,
                                                    string nombresBoleta, string apellidoPaternoBoleta, string apellidoMaternoBoleta,
                                                    string direccionBoleta, string fechaNacimientoBoleta, string celularBoleta,
                                                    string emailBoleta,
                                                    string RUCSeguro, string codigoCobertura, string origenMonto,
                                                    string fechaPago, string codigoProducto, string IAFAS,
                                                    string codigoParentesco, string codigoAfiliado, string tipoDocumentoContratante,
                                                    string numeroDocumentoContratante, string codigoTipoPago, string payMethod)
        {
            ConexionUtil varConexion = new ConexionUtil();
            RegistroPagoBE oRegistroPagoBE = new RegistroPagoBE();
            try
            {
                var tipo = "";

                if (!String.IsNullOrEmpty(idCita) && String.IsNullOrEmpty(idCitaVirtual))
                {

                    tipo = "p";
                    idCitaVirtual = "0";

                }
                else
                {
                    tipo = "t";
                    idCita = "0";
                    firma = "-";

                }
                bool esOrigenIPN = origen.Equals("IPN");

                SqlParameter[] varParametros = new SqlParameter[45];
                varParametros[0] = new SqlParameter("@IDCitaVirtual", SqlDbType.Int);
                varParametros[0].Value = idCitaVirtual;
                varParametros[1] = new SqlParameter("@CodigoError", SqlDbType.VarChar);
                varParametros[1].Value = codigo;
                varParametros[2] = new SqlParameter("@MensajeError", SqlDbType.VarChar);
                varParametros[2].Value = mensaje;
                varParametros[3] = new SqlParameter("@Fecha", SqlDbType.Date);
                varParametros[3].Value = fecha;
                varParametros[4] = new SqlParameter("@Hora", SqlDbType.Time);
                varParametros[4].Value = hora;
                varParametros[5] = new SqlParameter("@NumeroOperacion", SqlDbType.BigInt);
                varParametros[5].Value = purchaseNumber;
                varParametros[6] = new SqlParameter("@TransaccionID", SqlDbType.VarChar);
                varParametros[6].Value = transactionID;
                varParametros[7] = new SqlParameter("@CodigoWallet", SqlDbType.VarChar);
                varParametros[7].Value = "";
                varParametros[8] = new SqlParameter("@NumeroTarjeta", SqlDbType.VarChar);
                varParametros[8].Value = numeroTarjeta;
                varParametros[9] = new SqlParameter("@DeseaBoleta", SqlDbType.Bit);
                if (!string.IsNullOrEmpty(deseaBoleta)) varParametros[9].Value = deseaBoleta;
                else varParametros[9].Value = null;
                varParametros[10] = new SqlParameter("@RUC", SqlDbType.VarChar);
                varParametros[10].Value = ruc;
                varParametros[11] = new SqlParameter("@RazonSocial", SqlDbType.VarChar);
                varParametros[11].Value = razonSocial;
                varParametros[12] = new SqlParameter("@Direccion", SqlDbType.VarChar);
                varParametros[12].Value = direccion;
                varParametros[13] = new SqlParameter("@Origen", SqlDbType.VarChar);
                if (esOrigenIPN) varParametros[13].Value = null;
                else varParametros[13].Value = origen;
                varParametros[14] = new SqlParameter("@Monto", SqlDbType.VarChar);
                varParametros[14].Value = monto;
                varParametros[15] = new SqlParameter("@IDUnico", SqlDbType.VarChar);
                varParametros[15].Value = IDUnico;
                varParametros[16] = new SqlParameter("@TokenEmail", SqlDbType.VarChar);
                varParametros[16].Value = tokenEmail;
                varParametros[17] = new SqlParameter("@NombreVisa", SqlDbType.VarChar);
                varParametros[17].Value = nombreVisa;
                varParametros[18] = new SqlParameter("@ApellidoVisa", SqlDbType.VarChar);
                varParametros[18].Value = apellidoVisa;
                varParametros[19] = new SqlParameter("@IDCita", SqlDbType.Int);
                varParametros[19].Value = idCita;
                varParametros[20] = new SqlParameter("@Tipo", SqlDbType.VarChar);
                varParametros[20].Value = tipo;
                varParametros[21] = new SqlParameter("@Firma", SqlDbType.VarChar);
                varParametros[21].Value = !String.IsNullOrEmpty(firma) ? firma : "";
                varParametros[22] = new SqlParameter("@TipoTarjeta", SqlDbType.VarChar);
                varParametros[22].Value = tipoTarjeta;
                //Boleta
                varParametros[23] = new SqlParameter("@TipoDocumentoBoleta", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(tipoDocumentoBoleta)) varParametros[23].Value = null;
                else varParametros[23].Value = tipoDocumentoBoleta;
                varParametros[24] = new SqlParameter("@NumeroDocumentoBoleta", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(numeroDocumentoBoleta)) varParametros[24].Value = null;
                else varParametros[24].Value = numeroDocumentoBoleta;
                varParametros[25] = new SqlParameter("@NombresBoleta", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(nombresBoleta)) varParametros[25].Value = null;
                else varParametros[25].Value = nombresBoleta;
                varParametros[26] = new SqlParameter("@ApellidoPaternoBoleta", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(apellidoPaternoBoleta)) varParametros[26].Value = null;
                else varParametros[26].Value = apellidoPaternoBoleta;
                varParametros[27] = new SqlParameter("@ApellidoMaternoBoleta", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(apellidoMaternoBoleta)) varParametros[27].Value = null;
                else varParametros[27].Value = apellidoMaternoBoleta;
                varParametros[28] = new SqlParameter("@DireccionBoleta", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(direccionBoleta)) varParametros[28].Value = null;
                else varParametros[28].Value = direccionBoleta;
                varParametros[29] = new SqlParameter("@FechaNacimientoBoleta", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(fechaNacimientoBoleta)) varParametros[29].Value = null;
                else varParametros[29].Value = fechaNacimientoBoleta;
                varParametros[30] = new SqlParameter("@CelularBoleta", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(celularBoleta)) varParametros[30].Value = null;
                else varParametros[30].Value = celularBoleta;
                varParametros[31] = new SqlParameter("@EmailBoleta", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(emailBoleta)) varParametros[31].Value = null;
                else varParametros[31].Value = emailBoleta;

                varParametros[32] = new SqlParameter("@pRUCSeguro", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(RUCSeguro)) varParametros[32].Value = null;
                else varParametros[32].Value = RUCSeguro;
                varParametros[33] = new SqlParameter("@pCoberturaSITEDS", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(codigoCobertura)) varParametros[33].Value = null;
                else varParametros[33].Value = codigoCobertura;
                varParametros[34] = new SqlParameter("@pOrigenMonto", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(origenMonto)) varParametros[34].Value = null;
                else varParametros[34].Value = origenMonto;
                varParametros[35] = new SqlParameter("@pFechaPago", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(fechaPago)) varParametros[35].Value = null;
                else varParametros[35].Value = fechaPago;
                varParametros[36] = new SqlParameter("@pCodigoProducto", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(codigoProducto)) varParametros[36].Value = null;
                else varParametros[36].Value = codigoProducto;
                varParametros[37] = new SqlParameter("@pIAFAS", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(IAFAS)) varParametros[37].Value = null;
                else varParametros[37].Value = IAFAS;
                varParametros[38] = new SqlParameter("@pCodigoParentesco", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(codigoParentesco)) varParametros[38].Value = null;
                else varParametros[38].Value = codigoParentesco;
                varParametros[39] = new SqlParameter("@pCodigoAfiliado", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(codigoAfiliado)) varParametros[39].Value = null;
                else varParametros[39].Value = codigoAfiliado;
                varParametros[40] = new SqlParameter("@pTipoDocumentoContratante", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(tipoDocumentoContratante)) varParametros[40].Value = null;
                else varParametros[40].Value = tipoDocumentoContratante;
                varParametros[41] = new SqlParameter("@pNumeroDocumentoContratante", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(numeroDocumentoContratante)) varParametros[41].Value = null;
                else varParametros[41].Value = numeroDocumentoContratante;
                varParametros[42] = new SqlParameter("@pCodigoTipoPago", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(codigoTipoPago)) varParametros[42].Value = null;
                else varParametros[42].Value = codigoTipoPago;
                varParametros[43] = new SqlParameter("@tlEsIPN", SqlDbType.Bit);
                varParametros[43].Value = esOrigenIPN;
                varParametros[44] = new SqlParameter("@tvPayMethod", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(payMethod)) varParametros[44].Value = null;
                else varParametros[44].Value = payMethod;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_CitaVirtual_Pagar", varParametros, TipoProcesamiento.DataReader, false);

                DatosPagoBE varResultado = new DatosPagoBE();
                varDataReader.Read();
                if (varDataReader.HasRows)
                {
                    oRegistroPagoBE.indEnviarCorreo = varDataReader.GetBoolean(varDataReader.GetOrdinal("IndEviarCorreo"));
                    oRegistroPagoBE.success = true;
                }
                return oRegistroPagoBE;

                //if (varDataReader.HasRows)
                //    return true;
                //else
                //    return false;

            }
            catch (Exception e)
            {

                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }
        public bool AnularPagoCita(string idCita, string idCitaVirtual, string tipo, string idTransaccion, string origen, string motivo, bool result, string metodo)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {



                if (!String.IsNullOrEmpty(idCita) && String.IsNullOrEmpty(idCitaVirtual))
                {


                    idCitaVirtual = "0";

                }
                else
                {

                    idCita = "0";

                }


                SqlParameter[] varParametros = new SqlParameter[8];
                varParametros[0] = new SqlParameter("@IDCitaVirtual", SqlDbType.Int);
                varParametros[0].Value = idCitaVirtual;
                varParametros[1] = new SqlParameter("@IDCita", SqlDbType.Int);
                varParametros[1].Value = idCita;
                varParametros[2] = new SqlParameter("@Tipo", SqlDbType.Int);
                varParametros[2].Value = tipo;
                varParametros[3] = new SqlParameter("@IDTransaccion", SqlDbType.VarChar);
                varParametros[3].Value = idTransaccion;
                varParametros[4] = new SqlParameter("@Origen", SqlDbType.VarChar);
                varParametros[4].Value = origen;
                varParametros[5] = new SqlParameter("@Motivo", SqlDbType.VarChar);
                varParametros[5].Value = motivo;
                varParametros[6] = new SqlParameter("@Result", SqlDbType.Bit);
                varParametros[6].Value = result;
                varParametros[7] = new SqlParameter("@Metodo", SqlDbType.VarChar);
                varParametros[7].Value = metodo;




                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_CitaVirtual_AnularPago", varParametros, TipoProcesamiento.DataReader, false);

                if (varDataReader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception e)
            {

                return false;
                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }
        public DatosAnularCita AnularPagoCitaDatos(string idCita, string tipo)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {




                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@IDCita", SqlDbType.Int);
                varParametros[0].Value = idCita;
                varParametros[1] = new SqlParameter("@Tipo", SqlDbType.Int);
                varParametros[1].Value = tipo;




                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_CitaVirtual_AnularPagoDatos", varParametros, TipoProcesamiento.DataReader, false);

                var result = new DatosAnularCita();
                varDataReader.Read();
                result.merchant = varDataReader.GetString(varDataReader.GetOrdinal("Merchant"));
                result.user = varDataReader.GetString(varDataReader.GetOrdinal("User"));
                result.password = varDataReader.GetString(varDataReader.GetOrdinal("Password"));
                result.purchase = varDataReader.GetInt32(varDataReader.GetOrdinal("Purchase")).ToString();

                return result;



            }
            catch (Exception e)
            {

                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }


        public DatosRoomBE IniciarCitaVirtual(string idCita)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@IDCitaVirtual", SqlDbType.Int);
                varParametros[0].Value = idCita;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_CitaVirtual_InicioCita", varParametros, TipoProcesamiento.DataReader, false);

                DatosRoomBE varResultado = new DatosRoomBE();
                varDataReader.Read();
                //Uri url = new Uri("https://jitsi.diversolatam.com/");//se agrero entidades 16042021
                Uri url = new Uri("https://jitsi01.diversolatam.com/");
                varResultado = new DatosRoomBE()
                {
                    roomName = varDataReader.GetString(varDataReader.GetOrdinal("RoomName")),
                    roomPassword = varDataReader.GetString(varDataReader.GetOrdinal("RoomPassword")),
                    //doctorEnLinea = false,
                    //mensaje=null
                    doctorEnLinea = varDataReader.GetBoolean(varDataReader.GetOrdinal("EnLinea")),
                    mensaje = (varDataReader.GetBoolean(varDataReader.GetOrdinal("EnLinea"))) ? null : "Espere a que el doctor se conecte",
                    urlJitsi = url.ToString()
                };
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
        public DataTeleconsulta EstadoChat()
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[0];


                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Horario_Teleconsulta", varParametros, TipoProcesamiento.DataReader, false);

                DataTeleconsulta varResultado = new DataTeleconsulta();
                varDataReader.Read();
                varResultado = new DataTeleconsulta()
                {
                    Estado = varDataReader.GetBoolean(varDataReader.GetOrdinal("estado")),
                    MensajeAtencion = varDataReader.GetString(varDataReader.GetOrdinal("message"))
                };
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

        public List<FilaEsperaBE> ConsultarFilaEspera(string idCitaPresencial, string idCitaVirtual)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@IDCita", SqlDbType.Int);
                if (!string.IsNullOrEmpty(idCitaPresencial)) varParametros[0].Value = idCitaPresencial;
                else varParametros[0].Value = null;
                varParametros[1] = new SqlParameter("@IDCitaVirtual", SqlDbType.Int);
                if (!string.IsNullOrEmpty(idCitaVirtual)) varParametros[1].Value = idCitaVirtual;
                else varParametros[1].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_ListaEsperaPaciente", varParametros, TipoProcesamiento.DataReader, false);

                List<FilaEsperaBE> varResultado = new List<FilaEsperaBE>();
                FilaEsperaDetalleBE result = new FilaEsperaDetalleBE();

                while (varDataReader.Read())
                {
                    varResultado.Add(new FilaEsperaBE()
                    {
                        turno = varDataReader.GetInt32(varDataReader.GetOrdinal("Turno")).ToString(),
                        paciente = varDataReader.GetString(varDataReader.GetOrdinal("Paciente")),
                        horaCita = varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraCita")).ToString(@"hh\:mm"),
                        idEstado = varDataReader.GetInt32(varDataReader.GetOrdinal("IDEstado")),
                        estado = varDataReader.GetString(varDataReader.GetOrdinal("Estado")),
                        colorEstado = varDataReader.GetString(varDataReader.GetOrdinal("ColorEstado")),
                        codigoTipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("CodigoTipoAtencion")),
                        tipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("TipoAtencion")),
                        pacienteCita = (varDataReader.GetString(varDataReader.GetOrdinal("PacienteCita")) == "1") ? true : false
                    });
                }
                varDataReader.NextResult();
                varDataReader.Read();
                result.fila = varResultado;
                result.consultorio = (varDataReader.IsDBNull(varDataReader.GetOrdinal("Consultorio"))) ? " " : varDataReader.GetString(varDataReader.GetOrdinal("Consultorio"));

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

        public string DefinirMonto(string idCita, string monto, bool esVirtual, bool asegurado)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                if (esVirtual)
                {

                    SqlParameter[] varParametros = new SqlParameter[3];
                    varParametros[0] = new SqlParameter("@IDCitaVirtual", SqlDbType.Int);
                    varParametros[0].Value = idCita;
                    varParametros[1] = new SqlParameter("@Monto", SqlDbType.Decimal);
                    varParametros[1].Value = (monto != null) ? monto.Split('-')[0] : null;
                    varParametros[2] = new SqlParameter("@Moneda", SqlDbType.Int);
                    varParametros[2].Value = (monto != null) ? monto.Split('-')[1] : null;
                    Debug.WriteLine(varParametros[1].Value);

                    varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_CitaVirtual_InsertarTarifaSeguro", varParametros, TipoProcesamiento.DataReader, false);

                    DatosPagoBE varResultado = new DatosPagoBE();
                    varDataReader.Read();
                    var MontoString = varDataReader.GetDecimal(varDataReader.GetOrdinal("Monto")).ToString("0.00");

                    return MontoString;
                }
                else
                {
                    // Aca  se debe realizar algunos cambios en el procdure ya que se cruza las cias virtuales con las presenciales
                    if (monto != null)
                    {
                        SqlParameter[] varParametros = new SqlParameter[2];
                        varParametros[0] = new SqlParameter("@IDCita", SqlDbType.Int);
                        varParametros[0].Value = idCita;
                        varParametros[1] = new SqlParameter("@Monto", SqlDbType.Decimal);
                        varParametros[1].Value = monto;


                        varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_InsertarTarifaSeguro", varParametros, TipoProcesamiento.DataReader, false);
                    }
                    else
                    {
                        SqlParameter[] varParametros = new SqlParameter[1];
                        varParametros[0] = new SqlParameter("@IDCita", SqlDbType.Int);
                        varParametros[0].Value = idCita;


                        varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_ObtenerTarifaCita", varParametros, TipoProcesamiento.DataReader, false);
                    }




                    DatosPagoBE varResultado = new DatosPagoBE();
                    varDataReader.Read();
                    var MontoString = varDataReader.GetDecimal(varDataReader.GetOrdinal("Monto")).ToString("0.00");

                    return MontoString;
                }

            }
            catch (Exception)
            {
                //throw;
                return null;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }
        public string DefinirMontoP(
            string idCita, string monto, DatosResponseSited datosSited,
            string codigoCobertura, string rucSeguro, string origenMonto,
            string fechaPago, string codigoProducto, string IAFAS, string codigoAfiliado,
            string codigoParentesco, string tipoDocumentoContratante, string numeroDocumentoContratante,
            string deseaBoleta, string ruc, string razonSocial, string direccion,
            string tipoDocumentoBoleta, string numeroDocumentoBoleta, string nombresBoleta,
            string apellidoPaternoBoleta, string apellidoMaternoBoleta, string direccionBoleta,
            string fechaNacimientoBoleta, string celularBoleta, string emailBoleta,
            string origen
            )
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                //if (monto != null)
                //{
                //    SqlParameter[] varParametros = new SqlParameter[11];
                //    varParametros[0] = new SqlParameter("@IDCita", SqlDbType.Int);
                //    varParametros[0].Value = idCita;
                //    varParametros[1] = new SqlParameter("@Monto", SqlDbType.Decimal);
                //    varParametros[1].Value = monto;
                //    varParametros[2] = new SqlParameter("@RUCSeguro", SqlDbType.VarChar);
                //    varParametros[2].Value = datosSited.seguro;
                //    varParametros[3] = new SqlParameter("@CodProducto", SqlDbType.VarChar);
                //    varParametros[3].Value = datosSited.codProducto;
                //    varParametros[4] = new SqlParameter("@DesProducto", SqlDbType.VarChar);
                //    varParametros[4].Value = datosSited.desProducto;
                //    varParametros[5] = new SqlParameter("@CodCobertura", SqlDbType.VarChar);
                //    varParametros[5].Value = datosSited.codCobertura;
                //    varParametros[6] = new SqlParameter("@DesCobertura", SqlDbType.VarChar);
                //    varParametros[6].Value = datosSited.desCobertura;
                //    varParametros[7] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                //    varParametros[7].Value = datosSited.iafas;
                //    varParametros[8] = new SqlParameter("@CodAsegurado", SqlDbType.VarChar);
                //    varParametros[8].Value = datosSited.codAsegurado;
                //    varParametros[9] = new SqlParameter("@TipoMoneda", SqlDbType.Int);
                //    varParametros[9].Value = datosSited.codTipoMoneda;
                //    varParametros[10] = new SqlParameter("@CoberturaSITEDS", SqlDbType.VarChar);
                //    varParametros[10].Value = idCobertura;

                //    varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_InsertarTarifaSeguro", varParametros, TipoProcesamiento.DataReader, false);
                //}
                //else
                //{
                SqlParameter[] varParametros = new SqlParameter[26];
                varParametros[0] = new SqlParameter("@IDCita", SqlDbType.Int);
                varParametros[0].Value = idCita;
                varParametros[1] = new SqlParameter("@CoberturaSITEDS", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(codigoCobertura)) varParametros[1].Value = codigoCobertura;
                else varParametros[1].Value = null;
                varParametros[2] = new SqlParameter("@RucSeguro", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(rucSeguro)) varParametros[2].Value = rucSeguro;
                else varParametros[2].Value = null;
                varParametros[3] = new SqlParameter("@pMonto", SqlDbType.Decimal);
                if (!string.IsNullOrEmpty(monto)) varParametros[3].Value = monto;
                else varParametros[3].Value = null;
                varParametros[4] = new SqlParameter("@pOrigenMonto", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(origenMonto)) varParametros[4].Value = origenMonto;
                else varParametros[4].Value = null;
                varParametros[5] = new SqlParameter("@pFechaPago", SqlDbType.DateTime);
                if (!string.IsNullOrEmpty(fechaPago)) varParametros[5].Value = DateTime.Parse(fechaPago);
                else varParametros[5].Value = null;
                varParametros[6] = new SqlParameter("@pCodigoProducto", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(codigoProducto)) varParametros[6].Value = codigoProducto;
                else varParametros[6].Value = null;
                varParametros[7] = new SqlParameter("@pIAFAS", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(IAFAS)) varParametros[7].Value = IAFAS;
                else varParametros[7].Value = null;

                varParametros[8] = new SqlParameter("@pCodigoAfiliado", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(codigoAfiliado)) varParametros[8].Value = null;
                else varParametros[8].Value = codigoAfiliado;
                varParametros[9] = new SqlParameter("@pCodigoParentesco", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(codigoParentesco)) varParametros[9].Value = null;
                else varParametros[9].Value = codigoParentesco;
                varParametros[10] = new SqlParameter("@pTipoDocumentoContratante", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(tipoDocumentoContratante)) varParametros[10].Value = null;
                else varParametros[10].Value = tipoDocumentoContratante;
                varParametros[11] = new SqlParameter("@pNumeroDocumentoContratante", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(numeroDocumentoContratante)) varParametros[11].Value = null;
                else varParametros[11].Value = numeroDocumentoContratante;
                varParametros[12] = new SqlParameter("@pDeseaBoleta", SqlDbType.Bit);
                if (!string.IsNullOrEmpty(deseaBoleta)) varParametros[12].Value = deseaBoleta;
                else varParametros[12].Value = null;
                //Factura
                varParametros[13] = new SqlParameter("@pRUCFactura", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(ruc)) varParametros[13].Value = null;
                else varParametros[13].Value = ruc;
                varParametros[14] = new SqlParameter("@pRazonSocialFactura", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(razonSocial)) varParametros[14].Value = null;
                else varParametros[14].Value = razonSocial;
                varParametros[15] = new SqlParameter("@pDireccionFactura", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(direccion)) varParametros[15].Value = null;
                else varParametros[15].Value = direccion;
                //Boleta
                varParametros[16] = new SqlParameter("@pTipoDocumentoBoleta", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(tipoDocumentoBoleta)) varParametros[16].Value = null;
                else varParametros[16].Value = tipoDocumentoBoleta;
                varParametros[17] = new SqlParameter("@pNumeroDocumentoBoleta", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(numeroDocumentoBoleta)) varParametros[17].Value = null;
                else varParametros[17].Value = numeroDocumentoBoleta;
                varParametros[18] = new SqlParameter("@pNombresBoleta", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(nombresBoleta)) varParametros[18].Value = null;
                else varParametros[18].Value = nombresBoleta;
                varParametros[19] = new SqlParameter("@pApellidoPaternoBoleta", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(apellidoPaternoBoleta)) varParametros[19].Value = null;
                else varParametros[19].Value = apellidoPaternoBoleta;
                varParametros[20] = new SqlParameter("@pApellidoMaternoBoleta", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(apellidoMaternoBoleta)) varParametros[20].Value = null;
                else varParametros[20].Value = apellidoMaternoBoleta;
                varParametros[21] = new SqlParameter("@pDireccionBoleta", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(direccionBoleta)) varParametros[21].Value = null;
                else varParametros[21].Value = direccionBoleta;
                varParametros[22] = new SqlParameter("@pFechaNacimientoBoleta", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(fechaNacimientoBoleta)) varParametros[22].Value = null;
                else varParametros[22].Value = fechaNacimientoBoleta;
                varParametros[23] = new SqlParameter("@pCelularBoleta", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(celularBoleta)) varParametros[23].Value = null;
                else varParametros[23].Value = celularBoleta;
                varParametros[24] = new SqlParameter("@pEmailBoleta", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(emailBoleta)) varParametros[24].Value = null;
                else varParametros[24].Value = emailBoleta;
                varParametros[25] = new SqlParameter("@tvOrigen", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(origen)) varParametros[25].Value = null;
                else varParametros[25].Value = origen;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_ObtenerTarifaCita", varParametros, TipoProcesamiento.DataReader, false);
                //}

                DatosPagoBE varResultado = new DatosPagoBE();
                varDataReader.Read();
                var MontoString = varDataReader.GetDecimal(varDataReader.GetOrdinal("Monto")).ToString("0.00");

                return MontoString;
            }
            catch (Exception ex)
            {
                //throw;
                return null;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }
        public int VerificarHistorialPaciente(string idHorario, string tipoDocumento, string numeroDocumento)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[3];
                varParametros[0] = new SqlParameter("@IDHorario", SqlDbType.Int);
                varParametros[0].Value = idHorario;
                varParametros[1] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[1].Value = tipoDocumento;
                varParametros[2] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[2].Value = numeroDocumento;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Historial_Verificar", varParametros, TipoProcesamiento.DataReader, false);

                varDataReader.Read();
                var registros = varDataReader.GetInt32(varDataReader.GetOrdinal("Registros"));

                return registros;

            }
            catch (Exception)
            {
                //throw;
                return -1;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }
        public Dictionary<string, string> DatosCita(string idCita)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@IDCita", SqlDbType.Int);
                varParametros[0].Value = idCita;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_DatosCita", varParametros, TipoProcesamiento.DataReader, false);
                varDataReader.Read();
                Dictionary<string, string> varResultado = new Dictionary<string, string>();
                varResultado.Add("Email", varDataReader.GetString(varDataReader.GetOrdinal("Email")));
                varResultado.Add("TipoDocumento", varDataReader.GetInt32(varDataReader.GetOrdinal("TipoDocumento")).ToString());
                varResultado.Add("NumeroDocumento", varDataReader.GetString(varDataReader.GetOrdinal("NumeroDocumento")));
                varResultado.Add("NombrePaciente", varDataReader.GetString(varDataReader.GetOrdinal("NombrePaciente")));
                varResultado.Add("ApellidoPaternoPaciente", varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaternoPaciente")));
                varResultado.Add("ApellidoMaternoPaciente", varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaternoPaciente")));
                DateTime varFechaNacimiento = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaNacimiento"));
                varResultado.Add("FechaNacimiento", varFechaNacimiento.ToString("dd/MM/yyyy"));
                varResultado.Add("EdadPaciente", (DateTime.Now.Year - varFechaNacimiento.Year).ToString());
                varResultado.Add("Sexo", varDataReader.GetString(varDataReader.GetOrdinal("Genero")));
                varResultado.Add("CelularPaciente", varDataReader.GetString(varDataReader.GetOrdinal("CelularPaciente")));
                varResultado.Add("NombreMedico", varDataReader.GetString(varDataReader.GetOrdinal("NombreMedico")));
                varResultado.Add("FechaAtencion", varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dddd dd/MM/yyyy", new CultureInfo("es-PE")));
                varResultado.Add("FechaAtencionAux", varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("yyyy/MM/dd"));
                varResultado.Add("HoraInicio", varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")).ToString(@"hh\:mm"));
                varResultado.Add("Especialidad", varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")));
                varResultado.Add("Clinica", varDataReader.GetString(varDataReader.GetOrdinal("Clinica")));
                varResultado.Add("Seguro", varDataReader.GetString(varDataReader.GetOrdinal("Seguro")));
                varResultado.Add("CodigoSunasa", varDataReader.GetString(varDataReader.GetOrdinal("CodigoSunasa")));
                varResultado.Add("RUCSpring", varDataReader.GetString(varDataReader.GetOrdinal("RUCSpring")));
                varResultado.Add("EmailPago", varDataReader.GetString(varDataReader.GetOrdinal("EmailPago")));
                varResultado.Add("IDClinica", varDataReader.GetString(varDataReader.GetOrdinal("IDClinica")));
                varResultado.Add("EsAdicional", varDataReader.GetString(varDataReader.GetOrdinal("EsAdicional")));
                varResultado.Add("HoraInicioHorario", varDataReader.GetString(varDataReader.GetOrdinal("HoraInicioHorario")));
                varResultado.Add("HoraFinHorario", varDataReader.GetString(varDataReader.GetOrdinal("HoraFinHorario")));
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
        public Dictionary<string, string> DatosCitaVirtual(string idCitaVirtual)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@IDCitaVirtual", SqlDbType.Int);
                varParametros[0].Value = idCitaVirtual;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_CitaVirtual_DatosCita", varParametros, TipoProcesamiento.DataReader, false);
                varDataReader.Read();
                Dictionary<string, string> varResultado = new Dictionary<string, string>();

                varResultado.Add("Email", varDataReader.GetString(varDataReader.GetOrdinal("Email")));
                varResultado.Add("NombrePaciente", varDataReader.GetString(varDataReader.GetOrdinal("NombrePaciente")));
                varResultado.Add("ApellidoPaternoPaciente", varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaternoPaciente")));
                varResultado.Add("ApellidoMaternoPaciente", varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaternoPaciente")));
                DateTime varFechaNacimiento = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaNacimiento"));
                varResultado.Add("FechaNacimiento", varFechaNacimiento.ToString("dd/MM/yyyy"));
                varResultado.Add("EdadPaciente", (DateTime.Now.Year - varFechaNacimiento.Year).ToString());
                varResultado.Add("Sexo", varDataReader.GetString(varDataReader.GetOrdinal("Genero")));
                varResultado.Add("CelularPaciente", varDataReader.GetString(varDataReader.GetOrdinal("CelularPaciente")));
                varResultado.Add("NombreMedico", varDataReader.GetString(varDataReader.GetOrdinal("NombreMedico")));
                varResultado.Add("FechaAtencion", varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dddd dd/MM/yyyy", new CultureInfo("es-PE")));
                varResultado.Add("HoraInicio", varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")).ToString(@"hh\:mm"));
                varResultado.Add("Especialidad", varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")));
                varResultado.Add("Clinica", varDataReader.GetString(varDataReader.GetOrdinal("Clinica")));
                varResultado.Add("EmailMedico", varDataReader.GetString(varDataReader.GetOrdinal("EmailMedico")));
                varResultado.Add("Seguro", varDataReader.GetString(varDataReader.GetOrdinal("Seguro")));
                varResultado.Add("Direccion", varDataReader.GetString(varDataReader.GetOrdinal("Direccion")));
                varResultado.Add("EmailPago", varDataReader.GetString(varDataReader.GetOrdinal("EmailPago")));
                varResultado.Add("IDClinica", varDataReader.GetString(varDataReader.GetOrdinal("IDClinica")));
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
        public bool IndicadorVideollamada(string idCitaVirtual)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@IDCitaVirtual", SqlDbType.Int);
                varParametros[0].Value = idCitaVirtual;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_CitaVirtual_Indicador", varParametros, TipoProcesamiento.DataReader, false);
                varDataReader.Read();
                bool indicador = varDataReader.GetBoolean(varDataReader.GetOrdinal("Calificacion"));
                return indicador;
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
        public List<CitaHistoricaVistaPreviaBE> ListarCitasMedico(string cmp, string idClinica, DateTime fecha)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[3];
                varParametros[0] = new SqlParameter("@CMP", SqlDbType.VarChar);
                varParametros[0].Value = cmp;
                varParametros[1] = new SqlParameter("@IDClinica", SqlDbType.Int);
                varParametros[1].Value = idClinica;
                varParametros[2] = new SqlParameter("@Fecha", SqlDbType.DateTime);
                varParametros[2].Value = fecha;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_ListarMedico", varParametros, TipoProcesamiento.DataReader, false);

                List<CitaHistoricaVistaPreviaBE> varResultadoTemporal = new List<CitaHistoricaVistaPreviaBE>();
                while (varDataReader.Read())
                {
                    varResultadoTemporal.Add(new CitaHistoricaVistaPreviaBE()
                    {
                        nombrePaciente = varDataReader.GetString(varDataReader.GetOrdinal("NombreUsuario")),
                        cmp = varDataReader.GetString(varDataReader.GetOrdinal("CMP")),
                        nombreMedico = varDataReader.GetString(varDataReader.GetOrdinal("NombreMedico")),
                        fechaAtencion = char.ToUpper(varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dddd dd/MM/yyyy", new CultureInfo("es-PE"))[0]) + varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dddd dd/MM/yyyy", new CultureInfo("es-PE")).Substring(1),
                        fechaOrdenamiento = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")),
                        horaInicio = varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")).ToString(@"hh\:mm"),
                        horaFin = varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraFin")).ToString(@"hh\:mm"),
                        especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")),
                        clinica = varDataReader.GetString(varDataReader.GetOrdinal("Clinica")),
                        estado = varDataReader.GetInt32(varDataReader.GetOrdinal("Estado")).ToString(),
                        esCitaVirtual = varDataReader.GetString(varDataReader.GetOrdinal("EsCitaVirtual")).Equals("1"),
                        fuePagado = varDataReader.IsDBNull(varDataReader.GetOrdinal("FuePagado")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("FuePagado"))//,
                        //seguro = varDataReader.IsDBNull(varDataReader.GetOrdinal("Seguro")) ? "" : varDataReader.GetString(varDataReader.GetOrdinal("Seguro")),
                        //tipo = (varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")) == 17) ? "Teleorientacion" : (varDataReader.GetString(varDataReader.GetOrdinal("EsCitaVirtual")) == "1") ? "Cita Virtual" : "Presencial",
                        //estadoFlujo = (varDataReader.GetInt32(varDataReader.GetOrdinal("EsHistorica")) == 1) ? "Atendida" : varDataReader.IsDBNull(varDataReader.GetOrdinal("FuePagado")) ? "Pendiente" : (varDataReader.GetString(varDataReader.GetOrdinal("FuePagado")) == "Si") ? "Pagado" : "Pendiente de Pago"
                    });
                }
                varResultadoTemporal = varResultadoTemporal.OrderByDescending(p => p.fechaOrdenamiento).ThenByDescending(p => p.horaInicio).ToList();
                return varResultadoTemporal;
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
        public PreDatosCitasSited DatosCitaSeguro(string idCita, int tipo)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@IDCita", SqlDbType.Int);
                varParametros[0].Value = idCita;
                //se quito este comenario pool 11032021
                //varParametros[1] = new SqlParameter("@Tipo", SqlDbType.Int);
                //varParametros[1].Value = tipo;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cital_DatosSeguroCita", varParametros, TipoProcesamiento.DataReader, false);

                PreDatosCitasSited preDatos = null;
                if (varDataReader != null)
                {
                    if (varDataReader.Read())
                    {
                        if (varDataReader.HasRows)
                        {
                            preDatos = new PreDatosCitasSited();
                            preDatos.tipoDocumento = varDataReader.GetInt32(varDataReader.GetOrdinal("TipoDocumento")).ToString();
                            preDatos.numeroDocumento = varDataReader.GetString(varDataReader.GetOrdinal("NumeroDocumento"));
                            preDatos.rucClinica = varDataReader.GetString(varDataReader.GetOrdinal("RUCSpring"));
                            preDatos.codigoSunasa = (varDataReader.IsDBNull(varDataReader.GetOrdinal("CodigoSunasa"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("CodigoSunasa"));
                            preDatos.iafas = (varDataReader.IsDBNull(varDataReader.GetOrdinal("CodigoIAFAS"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("CodigoIAFAS"));
                            preDatos.codProducto = (varDataReader.IsDBNull(varDataReader.GetOrdinal("CodProducto"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("CodProducto"));
                            preDatos.codCobertura = (varDataReader.IsDBNull(varDataReader.GetOrdinal("CodCobertura"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("CodCobertura"));
                            preDatos.idPaciente = (varDataReader.IsDBNull(varDataReader.GetOrdinal("IDPaciente"))) ? null : varDataReader.GetInt32(varDataReader.GetOrdinal("IDPaciente")).ToString();
                            preDatos.sucursal = (varDataReader.IsDBNull(varDataReader.GetOrdinal("Sucursal"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Sucursal"));
                            preDatos.idCitaSpring = (varDataReader.IsDBNull(varDataReader.GetOrdinal("IDCitaSpring"))) ? null : varDataReader.GetInt32(varDataReader.GetOrdinal("IDCitaSpring")).ToString();
                            preDatos.idAseguradora = (varDataReader.IsDBNull(varDataReader.GetOrdinal("IDAseguradora"))) ? null : varDataReader.GetInt32(varDataReader.GetOrdinal("IDAseguradora")).ToString();
                            preDatos.tipoSeguro = (varDataReader.IsDBNull(varDataReader.GetOrdinal("TipoSeguro"))) ? null : varDataReader.GetInt32(varDataReader.GetOrdinal("TipoSeguro")).ToString();
                            preDatos.deseaBoleta = (varDataReader.IsDBNull(varDataReader.GetOrdinal("DeseaBoleta"))) ? true : varDataReader.GetBoolean(varDataReader.GetOrdinal("DeseaBoleta"));
                            preDatos.monto = (varDataReader.IsDBNull(varDataReader.GetOrdinal("Monto"))) ? null : varDataReader.GetDecimal(varDataReader.GetOrdinal("Monto")).ToString("0.00");
                            preDatos.firma = (varDataReader.IsDBNull(varDataReader.GetOrdinal("Firma"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Firma"));
                            preDatos.tipo = (varDataReader.IsDBNull(varDataReader.GetOrdinal("Tipo"))) ? null : varDataReader.GetInt32(varDataReader.GetOrdinal("Tipo")).ToString();
                            preDatos.idClinica = (varDataReader.IsDBNull(varDataReader.GetOrdinal("IDClinica"))) ? 0 : varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica"));
                            preDatos.hoy = (varDataReader.GetString(varDataReader.GetOrdinal("Hoy")) == "SI") ? true : false;
                            //se agrero entidades 16042021
                            preDatos.ruc = (varDataReader.IsDBNull(varDataReader.GetOrdinal("RUC"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("RUC"));
                            preDatos.razonsocial = (varDataReader.IsDBNull(varDataReader.GetOrdinal("RazonSocial"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("RazonSocial"));
                            preDatos.direccion = (varDataReader.IsDBNull(varDataReader.GetOrdinal("Direccion"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Direccion"));
                            preDatos.email = (varDataReader.IsDBNull(varDataReader.GetOrdinal("Email"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Email"));
                            preDatos.nombres = (varDataReader.IsDBNull(varDataReader.GetOrdinal("Nombres"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Nombres"));
                            preDatos.apellidoPaterno = (varDataReader.IsDBNull(varDataReader.GetOrdinal("ApellidoPaterno"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaterno"));
                            preDatos.apellidoMaterno = (varDataReader.IsDBNull(varDataReader.GetOrdinal("ApellidoMaterno"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaterno"));
                            preDatos.numeroOperacion = (varDataReader.IsDBNull(varDataReader.GetOrdinal("NumeroOperacion"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("NumeroOperacion"));
                            preDatos.rucAseguradora = (varDataReader.IsDBNull(varDataReader.GetOrdinal("RUCSeguro"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("RUCSeguro"));
                            preDatos.tipoPaciente = (varDataReader.IsDBNull(varDataReader.GetOrdinal("TipoPaciente"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TipoPaciente"));

                            preDatos.tipoDocumentoBoleta = (varDataReader.IsDBNull(varDataReader.GetOrdinal("TipoDocumentoBoleta"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TipoDocumentoBoleta"));
                            preDatos.numeroDocumentoBoleta = (varDataReader.IsDBNull(varDataReader.GetOrdinal("NumeroDocumentoBoleta"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("NumeroDocumentoBoleta"));
                            preDatos.nombresBoleta = (varDataReader.IsDBNull(varDataReader.GetOrdinal("NombresBoleta"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("NombresBoleta"));
                            preDatos.apellidoPaternoBoleta = (varDataReader.IsDBNull(varDataReader.GetOrdinal("ApellidoPaternoBoleta"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaternoBoleta"));
                            preDatos.apellidoMaternoBoleta = (varDataReader.IsDBNull(varDataReader.GetOrdinal("ApellidoMaternoBoleta"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaternoBoleta"));
                            preDatos.direccionBoleta = (varDataReader.IsDBNull(varDataReader.GetOrdinal("DireccionBoleta"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("DireccionBoleta"));
                            preDatos.fechaNacimientoBoleta = (varDataReader.IsDBNull(varDataReader.GetOrdinal("FechaNacimientoBoleta"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("FechaNacimientoBoleta"));
                            preDatos.celularBoleta = (varDataReader.IsDBNull(varDataReader.GetOrdinal("CelularBoleta"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("CelularBoleta"));
                            preDatos.emailBoleta = (varDataReader.IsDBNull(varDataReader.GetOrdinal("EmailBoleta"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("EmailBoleta"));

                            preDatos.codigoParentesco = (varDataReader.IsDBNull(varDataReader.GetOrdinal("CodigoParentesco"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("CodigoParentesco"));
                            preDatos.codigoAfiliado = (varDataReader.IsDBNull(varDataReader.GetOrdinal("CodigoAfiliado"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("CodigoAfiliado"));
                            preDatos.tipoDocumentoContratante = (varDataReader.IsDBNull(varDataReader.GetOrdinal("TipoDocumentoContratante"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TipoDocumentoContratante"));
                            preDatos.numeroDocumentoContratante = (varDataReader.IsDBNull(varDataReader.GetOrdinal("NumeroDocumentoContratante"))) ? null : varDataReader.GetString(varDataReader.GetOrdinal("NumeroDocumentoContratante"));
                        }
                    }
                }

                return preDatos;
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
        public CitaReprogramarPago ReprogramacionCitaConPago(string idCita, string idCitaAntiguo)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@IDCita", SqlDbType.Int);
                varParametros[0].Value = idCita;
                varParametros[1] = new SqlParameter("@IDCitaAntiguo", SqlDbType.Int);
                varParametros[1].Value = idCitaAntiguo;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_ReprogramarPago", varParametros, TipoProcesamiento.DataReader, false);

                CitaReprogramarPago datos = new CitaReprogramarPago();
                varDataReader.Read();
                if (varDataReader.HasRows)
                {
                    datos.pago = (varDataReader.GetInt32(varDataReader.GetOrdinal("Pago")) == 1) ? true : false;
                    datos.tarjeta = varDataReader.GetString(varDataReader.GetOrdinal("Tarjeta"));
                    datos.tipoTarjeta = varDataReader.GetString(varDataReader.GetOrdinal("TipoTarjeta"));
                }

                return datos;
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
        public bool LogInserccionRoyal(string idCita, string estado, string numero, string mensaje, string numeroAutorizacionFarmacia = "", string detalle = "")
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[6];
                varParametros[0] = new SqlParameter("@IDCita", SqlDbType.Int);
                varParametros[0].Value = idCita;
                varParametros[1] = new SqlParameter("@Estados", SqlDbType.VarChar);
                varParametros[1].Value = estado;
                varParametros[2] = new SqlParameter("@Numero", SqlDbType.VarChar);
                varParametros[2].Value = numero;
                varParametros[3] = new SqlParameter("@Mensaje", SqlDbType.VarChar);
                varParametros[3].Value = mensaje;
                varParametros[4] = new SqlParameter("@NumeroAutorizacionFarmacia", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(numeroAutorizacionFarmacia))
                {
                    varParametros[4].Value = null;
                }
                else
                {
                    varParametros[4].Value = numeroAutorizacionFarmacia;
                }
                varParametros[5] = new SqlParameter("@Detalle", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(detalle))
                {
                    varParametros[5].Value = null;
                }
                else
                {
                    varParametros[5].Value = detalle;
                }
                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_InsertarEstadoRoyal", varParametros, TipoProcesamiento.DataReader, false);


                varDataReader.Read();
                if (varDataReader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
        public void accessDatabase()
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\SUSALUD\SITEDS 10.0 (Rev. 5.3)\IPRESSLOG.mdb");

            con.Open();
            OleDbCommand select = new OleDbCommand();
            select.Connection = con;
            select.CommandText = "select * from SSTD_ESTADO";
            OleDbDataReader reader = select.ExecuteReader();
            List<Estado> estados = new List<Estado>();
            while (reader.Read())
            {

                var estado = new Estado();
                estado.CO_ESTACODE = Convert.ToInt32(reader["CO_ESTACODE"]);
                estados.Add(estado);
            }

            con.Close();

            foreach (var estado in estados)
            {
                Console.WriteLine(estado.CO_ESTACODE);
            }
        }

        public String ResultadoLaboratorioTabla(string tipoDocumento, string numeroDocumento, DateTime fechaInicio, DateTime fechaFin)
        {
            string rpta = "";
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[4];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar, 20);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@FechaInicio", SqlDbType.Date);
                varParametros[2].Value = fechaInicio;
                varParametros[3] = new SqlParameter("@FechaFin", SqlDbType.Date);
                varParametros[3].Value = fechaFin;

                rpta = varConexion.EjecutarProcedimiento("App_Proc_Cita_ResultadoLaboratorioListar", varParametros, TipoProcesamiento.Scalar, false).ToString();

                return rpta;
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
        public String TipoCoberturaPorEspecialidad(string codigo, string rucSeguro)
        {
            string rpta = "";
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@Codigo", SqlDbType.VarChar, 50);
                varParametros[0].Value = codigo;

                varParametros[1] = new SqlParameter("@RucSeguro", SqlDbType.VarChar, 50);
                varParametros[1].Value = rucSeguro;


                rpta = varConexion.EjecutarProcedimiento("App_Proc_TipoCobertura_Por_Especialidad", varParametros, TipoProcesamiento.Scalar, false).ToString();

                return rpta;
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

        public DatosPagoIzipayBE ObtenerDatosPagoIzipay(
            string idCita, string idCitaVirtual, string canal,
            string monto, string codigoCobertura, string rucSeguro,
            string origenMonto, string fechaPago, string codigoProducto,
            string IAFAS, string codigoAfiliado,
            string codigoParentesco, string tipoDocumentoContratante, string numeroDocumentoContratante,
            string deseaBoleta, string ruc, string razonSocial, string direccion,
            string tipoDocumentoBoleta, string numeroDocumentoBoleta, string nombresBoleta,
            string apellidoPaternoBoleta, string apellidoMaternoBoleta, string direccionBoleta,
            string fechaNacimientoBoleta, string celularBoleta, string emailBoleta,
            string origen
        )
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                if (!string.IsNullOrEmpty(idCita) && string.IsNullOrEmpty(idCitaVirtual))
                {
                    SqlParameter[] varParametros = new SqlParameter[2];
                    varParametros[0] = new SqlParameter("@IDCita", SqlDbType.Int);
                    varParametros[0].Value = idCita;
                    varParametros[1] = new SqlParameter("@Canal", SqlDbType.VarChar);
                    varParametros[1].Value = canal;

                    varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_DatosPagoIzipay", varParametros, TipoProcesamiento.DataReader, false);
                }
                if (string.IsNullOrEmpty(idCita) && !string.IsNullOrEmpty(idCitaVirtual))
                {
                    SqlParameter[] varParametros = new SqlParameter[27];
                    varParametros[0] = new SqlParameter("@IDCitaVirtual", SqlDbType.Int);
                    varParametros[0].Value = idCitaVirtual;
                    varParametros[1] = new SqlParameter("@Canal", SqlDbType.VarChar);
                    varParametros[1].Value = canal;
                    varParametros[2] = new SqlParameter("@CoberturaSITEDS", SqlDbType.VarChar);
                    if (!string.IsNullOrEmpty(codigoCobertura)) varParametros[2].Value = codigoCobertura;
                    else varParametros[2].Value = null;
                    varParametros[3] = new SqlParameter("@RucSeguro", SqlDbType.VarChar);
                    if (!string.IsNullOrEmpty(rucSeguro)) varParametros[3].Value = rucSeguro;
                    else varParametros[3].Value = null;
                    varParametros[4] = new SqlParameter("@pMonto", SqlDbType.Decimal);
                    if (!string.IsNullOrEmpty(monto)) varParametros[4].Value = monto;
                    else varParametros[4].Value = null;
                    varParametros[5] = new SqlParameter("@pOrigenMonto", SqlDbType.VarChar);
                    if (!string.IsNullOrEmpty(origenMonto)) varParametros[5].Value = origenMonto;
                    else varParametros[5].Value = null;
                    varParametros[6] = new SqlParameter("@pFechaPago", SqlDbType.DateTime);
                    if (!string.IsNullOrEmpty(fechaPago)) varParametros[6].Value = DateTime.Parse(fechaPago);
                    else varParametros[6].Value = null;
                    varParametros[7] = new SqlParameter("@pCodigoProducto", SqlDbType.VarChar);
                    if (!string.IsNullOrEmpty(codigoProducto)) varParametros[7].Value = codigoProducto;
                    else varParametros[7].Value = null;
                    varParametros[8] = new SqlParameter("@pIAFAS", SqlDbType.VarChar);
                    if (!string.IsNullOrEmpty(IAFAS)) varParametros[8].Value = IAFAS;
                    else varParametros[8].Value = null;

                    varParametros[9] = new SqlParameter("@pCodigoAfiliado", SqlDbType.VarChar);
                    if (string.IsNullOrEmpty(codigoAfiliado)) varParametros[9].Value = null;
                    else varParametros[9].Value = codigoAfiliado;
                    varParametros[10] = new SqlParameter("@pCodigoParentesco", SqlDbType.VarChar);
                    if (string.IsNullOrEmpty(codigoParentesco)) varParametros[10].Value = null;
                    else varParametros[10].Value = codigoParentesco;
                    varParametros[11] = new SqlParameter("@pTipoDocumentoContratante", SqlDbType.VarChar);
                    if (string.IsNullOrEmpty(tipoDocumentoContratante)) varParametros[11].Value = null;
                    else varParametros[11].Value = tipoDocumentoContratante;
                    varParametros[12] = new SqlParameter("@pNumeroDocumentoContratante", SqlDbType.VarChar);
                    if (string.IsNullOrEmpty(numeroDocumentoContratante)) varParametros[12].Value = null;
                    else varParametros[12].Value = numeroDocumentoContratante;
                    varParametros[13] = new SqlParameter("@pDeseaBoleta", SqlDbType.Bit);
                    if (!string.IsNullOrEmpty(deseaBoleta)) varParametros[13].Value = deseaBoleta;
                    else varParametros[13].Value = null;
                    //Factura
                    varParametros[14] = new SqlParameter("@pRUCFactura", SqlDbType.VarChar);
                    if (string.IsNullOrEmpty(ruc)) varParametros[14].Value = null;
                    else varParametros[14].Value = ruc;
                    varParametros[15] = new SqlParameter("@pRazonSocialFactura", SqlDbType.VarChar);
                    if (string.IsNullOrEmpty(razonSocial)) varParametros[15].Value = null;
                    else varParametros[15].Value = razonSocial;
                    varParametros[16] = new SqlParameter("@pDireccionFactura", SqlDbType.VarChar);
                    if (string.IsNullOrEmpty(direccion)) varParametros[16].Value = null;
                    else varParametros[16].Value = direccion;
                    //Boleta
                    varParametros[17] = new SqlParameter("@pTipoDocumentoBoleta", SqlDbType.VarChar);
                    if (string.IsNullOrEmpty(tipoDocumentoBoleta)) varParametros[17].Value = null;
                    else varParametros[17].Value = tipoDocumentoBoleta;
                    varParametros[18] = new SqlParameter("@pNumeroDocumentoBoleta", SqlDbType.VarChar);
                    if (string.IsNullOrEmpty(numeroDocumentoBoleta)) varParametros[18].Value = null;
                    else varParametros[18].Value = numeroDocumentoBoleta;
                    varParametros[19] = new SqlParameter("@pNombresBoleta", SqlDbType.VarChar);
                    if (string.IsNullOrEmpty(nombresBoleta)) varParametros[19].Value = null;
                    else varParametros[19].Value = nombresBoleta;
                    varParametros[20] = new SqlParameter("@pApellidoPaternoBoleta", SqlDbType.VarChar);
                    if (string.IsNullOrEmpty(apellidoPaternoBoleta)) varParametros[20].Value = null;
                    else varParametros[20].Value = apellidoPaternoBoleta;
                    varParametros[21] = new SqlParameter("@pApellidoMaternoBoleta", SqlDbType.VarChar);
                    if (string.IsNullOrEmpty(apellidoMaternoBoleta)) varParametros[21].Value = null;
                    else varParametros[21].Value = apellidoMaternoBoleta;
                    varParametros[22] = new SqlParameter("@pDireccionBoleta", SqlDbType.VarChar);
                    if (string.IsNullOrEmpty(direccionBoleta)) varParametros[22].Value = null;
                    else varParametros[22].Value = direccionBoleta;
                    varParametros[23] = new SqlParameter("@pFechaNacimientoBoleta", SqlDbType.VarChar);
                    if (string.IsNullOrEmpty(fechaNacimientoBoleta)) varParametros[23].Value = null;
                    else varParametros[23].Value = fechaNacimientoBoleta;
                    varParametros[24] = new SqlParameter("@pCelularBoleta", SqlDbType.VarChar);
                    if (string.IsNullOrEmpty(celularBoleta)) varParametros[24].Value = null;
                    else varParametros[24].Value = celularBoleta;
                    varParametros[25] = new SqlParameter("@pEmailBoleta", SqlDbType.VarChar);
                    if (string.IsNullOrEmpty(emailBoleta)) varParametros[25].Value = null;
                    else varParametros[25].Value = emailBoleta;
                    varParametros[26] = new SqlParameter("@tvOrigen", SqlDbType.VarChar);
                    if (string.IsNullOrEmpty(origen)) varParametros[26].Value = null;
                    else varParametros[26].Value = origen;

                    varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_CitaVirtual_DatosPagoIzipay", varParametros, TipoProcesamiento.DataReader, false);
                }

                DatosPagoIzipayBE varResultado = new DatosPagoIzipayBE();
                varDataReader.Read();

                varResultado = new DatosPagoIzipayBE()
                {
                    monto = varDataReader.GetDecimal(varDataReader.GetOrdinal("Monto")).ToString("0.00"),
                    user = varDataReader.GetString(varDataReader.GetOrdinal("UserIzipay")).ToString(),
                    password = varDataReader.GetString(varDataReader.GetOrdinal("PasswordIzipay")).ToString(),
                    merchant = varDataReader.GetString(varDataReader.GetOrdinal("MerchantIzipay")).ToString(),
                    purchaseNumber = varDataReader.GetInt64(varDataReader.GetOrdinal("CodeTransaccion")).ToString(),
                    especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")).ToString(),
                    nombreMedico = varDataReader.GetString(varDataReader.GetOrdinal("NombreMedico")).ToString(),
                    nombrePaciente = varDataReader.GetString(varDataReader.GetOrdinal("NombrePaciente")).ToString(),
                    fechaAtencion = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dd/MM/yyyy"),
                    horaInicio = varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")).ToString(@"hh\:mm"),
                    motivoCobro = varDataReader.GetString(varDataReader.GetOrdinal("MotivoCobro")).ToString(),
                    tokenEmail = varDataReader.GetString(varDataReader.GetOrdinal("DataUserToken")).ToString(),
                    clinica = varDataReader.GetString(varDataReader.GetOrdinal("Clinica")).ToString(),
                    aseguradora = varDataReader.GetString(varDataReader.GetOrdinal("Aseguradora")).ToString(),
                    correo = varDataReader.GetString(varDataReader.GetOrdinal("Correo")).ToString(),

                    nombreCompletoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("NombreCompletoPaciente")).ToString(),
                    apellidoCompletoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("ApellidoCompletoPaciente")).ToString(),
                    direccionPaciente = varDataReader.GetString(varDataReader.GetOrdinal("DireccionPaciente")).ToString(),
                    numeroDireccionPaciente = varDataReader.GetString(varDataReader.GetOrdinal("DireccionNumeroPaciente")).ToString(),
                    telefono = varDataReader.GetString(varDataReader.GetOrdinal("Telefono")).ToString(),
                    idCita = varDataReader.GetString(varDataReader.GetOrdinal("IDCita"))

                };

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

        public DatosPagoIzipayBE ObtenerDatosPagoIzipayFarmacia(string idClinica, string canal)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                if (!string.IsNullOrEmpty(idClinica))
                {
                    SqlParameter[] varParametros = new SqlParameter[2];
                    varParametros[0] = new SqlParameter("@IDClinica", SqlDbType.Int);
                    varParametros[0].Value = idClinica;
                    varParametros[1] = new SqlParameter("@Canal", SqlDbType.VarChar);
                    varParametros[1].Value = canal;

                    varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_DatosPagoIzipayFarmacia", varParametros, TipoProcesamiento.DataReader, false);
                }


                DatosPagoIzipayBE varResultado = new DatosPagoIzipayBE();
                varDataReader.Read();

                varResultado = new DatosPagoIzipayBE()
                {
                    monto = varDataReader.GetDecimal(varDataReader.GetOrdinal("Monto")).ToString("0.00"),
                    user = varDataReader.GetString(varDataReader.GetOrdinal("UserIzipay")).ToString(),
                    password = varDataReader.GetString(varDataReader.GetOrdinal("PasswordIzipay")).ToString(),
                    merchant = varDataReader.GetString(varDataReader.GetOrdinal("MerchantIzipay")).ToString(),
                    purchaseNumber = varDataReader.GetInt64(varDataReader.GetOrdinal("CodeTransaccion")).ToString(),
                    especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")).ToString(),
                    nombreMedico = varDataReader.GetString(varDataReader.GetOrdinal("NombreMedico")).ToString(),
                    nombrePaciente = varDataReader.GetString(varDataReader.GetOrdinal("NombrePaciente")).ToString(),
                    fechaAtencion = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dd/MM/yyyy"),
                    horaInicio = varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")).ToString(@"hh\:mm"),
                    motivoCobro = varDataReader.GetString(varDataReader.GetOrdinal("MotivoCobro")).ToString(),
                    tokenEmail = varDataReader.GetString(varDataReader.GetOrdinal("DataUserToken")).ToString(),
                    clinica = varDataReader.GetString(varDataReader.GetOrdinal("Clinica")).ToString(),
                    aseguradora = varDataReader.GetString(varDataReader.GetOrdinal("Aseguradora")).ToString(),
                    correo = varDataReader.GetString(varDataReader.GetOrdinal("Correo")).ToString(),

                    nombreCompletoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("NombreCompletoPaciente")).ToString(),
                    apellidoCompletoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("ApellidoCompletoPaciente")).ToString(),
                    direccionPaciente = varDataReader.GetString(varDataReader.GetOrdinal("DireccionPaciente")).ToString(),
                    numeroDireccionPaciente = varDataReader.GetString(varDataReader.GetOrdinal("DireccionNumeroPaciente")).ToString(),
                    telefono = varDataReader.GetString(varDataReader.GetOrdinal("Telefono")).ToString()


                };


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

        public IzipaySDKBE ObtenerDatosIzipaySDK(string idCitaPresencial, string idCita, string canal)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                String vCanal = String.IsNullOrEmpty(canal) ? "mobile" : canal;
                SqlParameter[] varParametros = new SqlParameter[3];
                varParametros[0] = new SqlParameter("@IDCitaPresencial", SqlDbType.Int);
                if (String.IsNullOrEmpty(idCitaPresencial))
                {
                    varParametros[0].Value = null;
                }
                else
                {
                    varParametros[0].Value = idCitaPresencial;
                }
                //Virtual
                varParametros[1] = new SqlParameter("@IDCita", SqlDbType.Int);
                if (String.IsNullOrEmpty(idCita))
                {
                    varParametros[1].Value = null;
                }
                else
                {
                    varParametros[1].Value = idCita;
                }
                //Canal
                varParametros[2] = new SqlParameter("@Canal", SqlDbType.VarChar);
                varParametros[2].Value = vCanal;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_IzipaySDKListar", varParametros, TipoProcesamiento.DataReader, false);

                IzipaySDKBE varResultado = new IzipaySDKBE();
                varDataReader.Read();

                varResultado = new IzipaySDKBE()
                {
                    usuarioIzipaySDK = varDataReader.GetString(varDataReader.GetOrdinal("UsuarioIzipaySDK")).ToString(),
                    passwordIzipaySDK = varDataReader.GetString(varDataReader.GetOrdinal("PasswordIzipaySDK")).ToString(),
                    urlBaseIzipaySDK = varDataReader.GetString(varDataReader.GetOrdinal("URLBaseIzipaySDK")).ToString(),
                };

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

        public IzipaySDKBE ObtenerDatosIzipaySDKFarmacia(string idClinica, string canal)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                String vCanal = String.IsNullOrEmpty(canal) ? "mobile" : canal;
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@IDClinica", SqlDbType.Int);
                if (String.IsNullOrEmpty(idClinica))
                {
                    varParametros[0].Value = null;
                }
                else
                {
                    varParametros[0].Value = idClinica;
                }
                //Canal
                varParametros[1] = new SqlParameter("@Canal", SqlDbType.VarChar);
                varParametros[1].Value = vCanal;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_IzipaySDKListarFarmacia", varParametros, TipoProcesamiento.DataReader, false);

                IzipaySDKBE varResultado = new IzipaySDKBE();
                varDataReader.Read();

                varResultado = new IzipaySDKBE()
                {
                    usuarioIzipaySDK = varDataReader.GetString(varDataReader.GetOrdinal("UsuarioIzipaySDK")).ToString(),
                    passwordIzipaySDK = varDataReader.GetString(varDataReader.GetOrdinal("PasswordIzipaySDK")).ToString(),
                    urlBaseIzipaySDK = varDataReader.GetString(varDataReader.GetOrdinal("URLBaseIzipaySDK")).ToString(),
                };

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
        public String RecetaMedicaListar(string idCita, string numeroAtencion)
        {
            string rpta = "";
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@IDCita", SqlDbType.Int);
                varParametros[0].Value = idCita;
                varParametros[1] = new SqlParameter("@NumeroOrdenAtencion", SqlDbType.VarChar, 15);
                varParametros[1].Value = numeroAtencion;


                rpta = varConexion.EjecutarProcedimiento("App_Proc_Cita_Receta", varParametros, TipoProcesamiento.Scalar, false).ToString();

                return rpta;
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

        public void ActualizarEnvioChequeo(string idCita, bool indEnvioCorreo = false)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@IDCita", SqlDbType.Int);
                varParametros[0].Value = idCita;
                varParametros[1] = new SqlParameter("@IndEnvio", SqlDbType.Bit);
                varParametros[1].Value = indEnvioCorreo;

                varConexion.EjecutarProcedimiento("App_Proc_Cita_ActualizarEnvioChequeo", varParametros, TipoProcesamiento.NonQuery);
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

        public String ObtenerProtocolosChequeoMedico(string tipoDocumento, string numeroDocumento, string clinicaId)
        {
            string rpta = "";
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@lstParametros", SqlDbType.VarChar, 500);
                varParametros[0].Value = tipoDocumento + "¦" + numeroDocumento + "¦" + clinicaId;

                rpta = varConexion.EjecutarProcedimiento("App_Proc_Citas_AsignarProtocolo_Listas", varParametros, TipoProcesamiento.Scalar, false).ToString();

                return rpta;
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

        public String ObtenerPlanesComponentesChequeoMedico(string companiaId, string clinicaId)
        {
            string rpta = "";
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@lstParametros", SqlDbType.VarChar, 500);
                varParametros[0].Value = companiaId + "¦" + clinicaId;

                rpta = varConexion.EjecutarProcedimiento("uspCitasPlanes_Componentes_Listas", varParametros, TipoProcesamiento.Scalar, false, "BDSANNA_Chqprv").ToString();

                return rpta;
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

        public Dictionary<string, string> ObtenerDatosCita(string idCita)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@IDCita", SqlDbType.Int);
                varParametros[0].Value = idCita;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_ObtenerDatos", varParametros, TipoProcesamiento.DataReader, false);
                varDataReader.Read();
                Dictionary<string, string> varResultado = new Dictionary<string, string>();
                varResultado.Add("IDCita", varDataReader.GetInt32(varDataReader.GetOrdinal("IDCita")).ToString());
                varResultado.Add("Email", varDataReader.GetString(varDataReader.GetOrdinal("Email")));
                varResultado.Add("NombrePaciente", varDataReader.GetString(varDataReader.GetOrdinal("NombrePaciente")));
                varResultado.Add("ApellidoPaternoPaciente", varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaternoPaciente")));
                varResultado.Add("CelularPaciente", varDataReader.GetString(varDataReader.GetOrdinal("CelularPaciente")));
                varResultado.Add("NombreMedico", varDataReader.GetString(varDataReader.GetOrdinal("NombreMedico")));
                varResultado.Add("FechaAtencion", varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dddd dd/MM/yyyy", new CultureInfo("es-PE")));
                varResultado.Add("HoraInicio", varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")).ToString(@"hh\:mm"));
                varResultado.Add("Especialidad", varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")));
                varResultado.Add("Clinica", varDataReader.GetString(varDataReader.GetOrdinal("Clinica")));
                varResultado.Add("IDClinica", varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString());
                varResultado.Add("ApellidoMaternoPaciente", varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaternoPaciente")));
                varResultado.Add("TipoPago", varDataReader.GetString(varDataReader.GetOrdinal("TipoPago")));
                varResultado.Add("LinkPago", "");
                varResultado.Add("IndicadorBotonPagar", varDataReader.GetString(varDataReader.GetOrdinal("IndicadorBotonPagar")));
                varResultado.Add("EsPrepago", varDataReader.GetString(varDataReader.GetOrdinal("EsPrepago")));
                varResultado.Add("IndEnviarCorreo", varDataReader.GetString(varDataReader.GetOrdinal("IndEnviarCorreo")));//Indicador para el envío de correo de Chequeo

                return varResultado;
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

        public String ValidarRegistrarCita(string tipoDocumento, string numeroDocumento, string idHorario,
                                        string fecha, string numeroDia, string numeroTurno,
                                        string origen, string observaciones, int tipoCita,
                                        bool esChequeo, string horaInicio = "", int tiempoAtencion = 0,
                                        string codigoComponente = "", string strRespuestasImagenes = "")
        {
            String rpta = "";
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[12];

                varParametros[1] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[1].Value = tipoDocumento;
                varParametros[2] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[2].Value = numeroDocumento;
                varParametros[3] = new SqlParameter("@IDHorario", SqlDbType.Int);
                varParametros[3].Value = idHorario;
                varParametros[4] = new SqlParameter("@FechaAtencion", SqlDbType.Date);
                varParametros[4].Value = fecha;
                varParametros[5] = new SqlParameter("@NumeroDia", SqlDbType.Int);
                varParametros[5].Value = numeroDia;
                varParametros[6] = new SqlParameter("@Turno", SqlDbType.Int);
                varParametros[6].Value = numeroTurno;
                varParametros[8] = new SqlParameter("@Origen", SqlDbType.VarChar);
                varParametros[8].Value = origen;
                varParametros[9] = new SqlParameter("@Observaciones", SqlDbType.VarChar);
                varParametros[9].Value = observaciones;
                varParametros[10] = new SqlParameter("@TipoCita", SqlDbType.Int);
                varParametros[10].Value = tipoCita;
                varParametros[11] = new SqlParameter("@EsChequeo", SqlDbType.Bit);
                varParametros[11].Value = esChequeo;
                if (!String.IsNullOrEmpty(codigoComponente))
                {
                    varParametros[12] = new SqlParameter("@HoraInicio", SqlDbType.VarChar);
                    varParametros[12].Value = horaInicio;
                    varParametros[13] = new SqlParameter("@TiempoAtencion", SqlDbType.Int);
                    varParametros[13].Value = tiempoAtencion;
                    varParametros[14] = new SqlParameter("@CodigoComponente", SqlDbType.VarChar);
                    varParametros[14].Value = codigoComponente;
                    varParametros[15] = new SqlParameter("@RespuestasImagenes", SqlDbType.VarChar);
                    varParametros[15].Value = strRespuestasImagenes;
                }

                rpta = varConexion.EjecutarProcedimiento("App_Proc_Cita_ValidarRegistrar", varParametros, TipoProcesamiento.Scalar, false).ToString();

                return rpta;
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

        public Dictionary<string, string> ObtenerParametrosCorreoCitaPresencial(string idCita)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];

                varParametros[0] = new SqlParameter("@IDCita", SqlDbType.Int);
                varParametros[0].Value = idCita;


                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspParametroCorreoCitaPresencial", varParametros, TipoProcesamiento.DataReader, false);
                varDataReader.Read();
                Dictionary<string, string> varResultado = new Dictionary<string, string>();
                varResultado.Add("IDCita", varDataReader.GetInt32(varDataReader.GetOrdinal("IDCita")).ToString());
                varResultado.Add("Email", varDataReader.GetString(varDataReader.GetOrdinal("Email")));
                varResultado.Add("NombrePaciente", varDataReader.GetString(varDataReader.GetOrdinal("NombrePaciente")));
                varResultado.Add("ApellidoPaternoPaciente", varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaternoPaciente")));
                varResultado.Add("CelularPaciente", varDataReader.GetString(varDataReader.GetOrdinal("CelularPaciente")));
                varResultado.Add("NombreMedico", varDataReader.GetString(varDataReader.GetOrdinal("NombreMedico")));
                varResultado.Add("FechaAtencion", varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dd/MM/yyyy"));
                varResultado.Add("HoraInicio", varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")).ToString(@"hh\:mm"));
                varResultado.Add("Especialidad", varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")));
                varResultado.Add("Clinica", varDataReader.GetString(varDataReader.GetOrdinal("Clinica")));
                varResultado.Add("IDClinica", varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString());
                varResultado.Add("ApellidoMaternoPaciente", varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaternoPaciente")));
                varResultado.Add("Consultorio", varDataReader.GetString(varDataReader.GetOrdinal("Consultorio")));
                varResultado.Add("TiempoAtencion", varDataReader.GetString(varDataReader.GetOrdinal("TiempoAtencion")));
                varResultado.Add("NumeroDocumentoPaciente", varDataReader.GetString(varDataReader.GetOrdinal("NumeroDocumentoPaciente")));
                varResultado.Add("LinkPago", "");
                varResultado.Add("LinkCalendario", "");
                varResultado.Add("IndicadorBotonPagar", varDataReader.GetString(varDataReader.GetOrdinal("IndicadorBotonPagar")));
                varResultado.Add("EsPrePago", varDataReader.GetString(varDataReader.GetOrdinal("EsPrePago")));
                varResultado.Add("EsAdicional", varDataReader.GetString(varDataReader.GetOrdinal("EsAdicional")));
                varResultado.Add("HoraInicioHorario", varDataReader.GetString(varDataReader.GetOrdinal("HoraInicioHorario")));
                varResultado.Add("HoraFinHorario", varDataReader.GetString(varDataReader.GetOrdinal("HoraFinHorario")));
                varResultado.Add("Ubicacion", varDataReader.GetString(varDataReader.GetOrdinal("Ubicacion")));
                varResultado.Add("DireccionCorta", varDataReader.GetString(varDataReader.GetOrdinal("DireccionCorta")));
                varResultado.Add("Referencia", varDataReader.GetString(varDataReader.GetOrdinal("Referencia")));
                varResultado.Add("URLBanner", varDataReader.GetString(varDataReader.GetOrdinal("URLBanner")));

                return varResultado;
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

        public String ServicioInfoListar(string idClinica)
        {
            string rpta = "";
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@IdClinica", SqlDbType.Int);
                varParametros[0].Value = idClinica;


                rpta = varConexion.EjecutarProcedimiento("App_Proc_ServicioInfo_Listar", varParametros, TipoProcesamiento.Scalar, false).ToString();

                return rpta;
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


        public Dictionary<string, string> EnviarOTP(string codigoOTP, string tipoDocumento, string numerodocumento,
            string correo, DateTime fechaHoraInicio, DateTime fechaHoraFin,
            bool indicadorReenvio, String estado, string tipo)
        {
            string rpta = "";
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[9];
                varParametros[0] = new SqlParameter("@CodigoOTP", SqlDbType.VarChar);
                varParametros[0].Value = codigoOTP;
                varParametros[1] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[1].Value = tipoDocumento;
                varParametros[2] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[2].Value = numerodocumento;
                varParametros[3] = new SqlParameter("@Correo", SqlDbType.VarChar);
                varParametros[3].Value = correo;
                varParametros[4] = new SqlParameter("@FechaHoraInicio", SqlDbType.DateTime);
                varParametros[4].Value = fechaHoraInicio;
                varParametros[5] = new SqlParameter("@FechaHoraFin", SqlDbType.DateTime);
                varParametros[5].Value = fechaHoraFin;
                varParametros[6] = new SqlParameter("@IndicadorReenvio", SqlDbType.Bit);
                varParametros[6].Value = indicadorReenvio;
                varParametros[7] = new SqlParameter("@IndicadorReenvio", SqlDbType.Bit);
                varParametros[7].Value = indicadorReenvio;
                varParametros[7] = new SqlParameter("@EstadoRegistro", SqlDbType.VarChar);
                varParametros[7].Value = estado;
                varParametros[8] = new SqlParameter("@Tipo", SqlDbType.VarChar);
                varParametros[8].Value = tipo;

                //rpta = varConexion.EjecutarProcedimiento("App_Proc_EnviarOTP", varParametros, TipoProcesamiento.Scalar, false).ToString();
                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_EnviarOTP", varParametros, TipoProcesamiento.DataReader, false);

                Dictionary<string, string> varResultado = null;
                if (varDataReader != null)
                {
                    if (varDataReader.Read())
                    {
                        varResultado = new Dictionary<string, string>();
                        varResultado.Add("IdOTP", varDataReader.GetString(varDataReader.GetOrdinal("IdOTP")));
                        varResultado.Add("FechaHoraInicio", varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaHoraInicio")).ToString("dd/MM/yyyy HH:mm:ss.fff"));
                        varResultado.Add("FechaHoraFin", varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaHoraFin")).ToString("dd/MM/yyyy HH:mm:ss.fff"));
                    }
                }

                //return rpta;
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

        public String AnularOTP(string idOTP)
        {
            string rpta = "";
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@IdOTP", SqlDbType.Int);
                varParametros[0].Value = idOTP;

                rpta = varConexion.EjecutarProcedimiento("App_Proc_AnularOTP", varParametros, TipoProcesamiento.Scalar, false).ToString();

                return rpta;
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

        public Dictionary<string, string> ActualizarOTP(string tipoDocumento, string numerodocumento, string codigoOTP, int tiempoAdicional)
        {
            string rpta = "";
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[4];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numerodocumento;
                varParametros[2] = new SqlParameter("@CodigoOTP", SqlDbType.VarChar);
                varParametros[2].Value = codigoOTP;
                varParametros[3] = new SqlParameter("@TiempoAdicional", SqlDbType.Int);
                varParametros[3].Value = tiempoAdicional;

                //rpta = varConexion.EjecutarProcedimiento("App_Proc_ActualizarOTP", varParametros, TipoProcesamiento.Scalar, false).ToString();
                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_ActualizarOTP", varParametros, TipoProcesamiento.DataReader, false);
                Dictionary<string, string> varResultado = null;
                if (varDataReader != null)
                {
                    if (varDataReader.Read())
                    {
                        varResultado = new Dictionary<string, string>();
                        varResultado.Add("IdOTP", varDataReader.GetString(varDataReader.GetOrdinal("IdOTP")));
                        varResultado.Add("FechaHoraInicio", varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaHoraInicio")).ToString("dd/MM/yyyy HH:mm:ss.fff"));
                        varResultado.Add("FechaHoraFin", varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaHoraFin")).ToString("dd/MM/yyyy HH:mm:ss.fff"));
                        varResultado.Add("IndicadorValidado", varDataReader.GetString(varDataReader.GetOrdinal("IndicadorValidado")));
                    }
                }

                //return rpta;
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

        public OTPBE ObtenerOTP(string tipoDocumento, string numeroDocumento, string codigoOTP,
            string tipo, string ipCliente)
        {
            ConexionUtil varConexion = new ConexionUtil();
            OTPBE varResultado = null;
            try
            {
                SqlParameter[] varParametros = new SqlParameter[5];

                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;

                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;

                varParametros[2] = new SqlParameter("@CodigoOTP", SqlDbType.VarChar);
                varParametros[2].Value = codigoOTP;

                varParametros[3] = new SqlParameter("@Tipo", SqlDbType.VarChar);
                varParametros[3].Value = tipo;

                varParametros[4] = new SqlParameter("@IPCliente", SqlDbType.VarChar);
                varParametros[4].Value = ipCliente;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_ObtenerOTP", varParametros, TipoProcesamiento.DataReader, false);

                //varDataReader.Read();
                if (varDataReader != null)
                {
                    if (varDataReader.Read())
                    {
                        varResultado = new OTPBE();

                        varResultado.idOTP = varDataReader.GetInt32(varDataReader.GetOrdinal("IdOTP"));
                        varResultado.FechaHoraFin = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaHoraFin"));
                        varResultado.Correo = varDataReader.GetString(varDataReader.GetOrdinal("Correo"));
                        varResultado.IndicadorValidado = varDataReader.GetString(varDataReader.GetOrdinal("IndicadorValidado")).Equals("1");
                    }
                }

            }
            catch (Exception ex)
            {
                //varResultado = null;
                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
            return varResultado;
        }

        public bool RegistrarPagoCitaSynapsis(string idCita, string idCitaVirtual, string codigo, string mensaje, string fecha,
                                                    string hora, long purchaseNumber, string transactionID, string numeroTarjeta,
                                                    string deseaBoleta, string ruc, string razonSocial, string direccion, string origen,
                                                    string monto, string IDUnico, string tokenEmail, string nombreVisa, string apellidoVisa,
                                                    string firma, string tipoTarjeta)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                var tipo = "";

                if (!String.IsNullOrEmpty(idCita) && String.IsNullOrEmpty(idCitaVirtual))
                {

                    tipo = "p";
                    idCitaVirtual = "0";

                }
                else
                {
                    tipo = "t";
                    idCita = "0";
                    firma = "-";

                }


                SqlParameter[] varParametros = new SqlParameter[23];
                varParametros[0] = new SqlParameter("@IDCitaVirtual", SqlDbType.Int);
                varParametros[0].Value = idCitaVirtual;
                varParametros[1] = new SqlParameter("@CodigoError", SqlDbType.VarChar);
                varParametros[1].Value = codigo;
                varParametros[2] = new SqlParameter("@MensajeError", SqlDbType.VarChar);
                varParametros[2].Value = mensaje;
                varParametros[3] = new SqlParameter("@Fecha", SqlDbType.Date);
                varParametros[3].Value = fecha;
                varParametros[4] = new SqlParameter("@Hora", SqlDbType.Time);
                varParametros[4].Value = hora;
                varParametros[5] = new SqlParameter("@NumeroOperacion", SqlDbType.BigInt);
                varParametros[5].Value = purchaseNumber;
                varParametros[6] = new SqlParameter("@TransaccionID", SqlDbType.VarChar);
                varParametros[6].Value = transactionID;
                varParametros[7] = new SqlParameter("@CodigoWallet", SqlDbType.VarChar);
                varParametros[7].Value = "";
                varParametros[8] = new SqlParameter("@NumeroTarjeta", SqlDbType.VarChar);
                varParametros[8].Value = numeroTarjeta;
                varParametros[9] = new SqlParameter("@DeseaBoleta", SqlDbType.Bit);
                varParametros[9].Value = deseaBoleta;
                varParametros[10] = new SqlParameter("@RUC", SqlDbType.VarChar);
                varParametros[10].Value = ruc;
                varParametros[11] = new SqlParameter("@RazonSocial", SqlDbType.VarChar);
                varParametros[11].Value = razonSocial;
                varParametros[12] = new SqlParameter("@Direccion", SqlDbType.VarChar);
                varParametros[12].Value = direccion;
                varParametros[13] = new SqlParameter("@Origen", SqlDbType.VarChar);
                varParametros[13].Value = origen;
                varParametros[14] = new SqlParameter("@Monto", SqlDbType.VarChar);
                varParametros[14].Value = monto;
                varParametros[15] = new SqlParameter("@IDUnico", SqlDbType.VarChar);
                varParametros[15].Value = IDUnico;
                varParametros[16] = new SqlParameter("@TokenEmail", SqlDbType.VarChar);
                varParametros[16].Value = tokenEmail;
                varParametros[17] = new SqlParameter("@NombreVisa", SqlDbType.VarChar);
                varParametros[17].Value = nombreVisa;
                varParametros[18] = new SqlParameter("@ApellidoVisa", SqlDbType.VarChar);
                varParametros[18].Value = apellidoVisa;
                varParametros[19] = new SqlParameter("@IDCita", SqlDbType.Int);
                varParametros[19].Value = idCita;
                varParametros[20] = new SqlParameter("@Tipo", SqlDbType.VarChar);
                varParametros[20].Value = tipo;
                varParametros[21] = new SqlParameter("@Firma", SqlDbType.VarChar);
                varParametros[21].Value = firma;
                varParametros[22] = new SqlParameter("@TipoTarjeta", SqlDbType.VarChar);
                varParametros[22].Value = tipoTarjeta;


                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_RegistrarPagoSynapsis", varParametros, TipoProcesamiento.DataReader, false);

                DatosPagoBE varResultado = new DatosPagoBE();
                varDataReader.Read();
                if (varDataReader.HasRows)

                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {

                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }

        public string ObtenerDatosPorHorario(string idHorario, string tipoAtencion, bool esAdicional,
            string idServicioHorario, bool esProcedimiento)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[5];
                varParametros[0] = new SqlParameter("@IDHorario", SqlDbType.Int);
                if (!string.IsNullOrEmpty(idHorario)) varParametros[0].Value = idHorario;
                else varParametros[0].Value = null;
                varParametros[1] = new SqlParameter("@TipoAtencion", SqlDbType.VarChar);
                varParametros[1].Value = tipoAtencion;
                varParametros[2] = new SqlParameter("@EsAdicional", SqlDbType.VarChar);
                varParametros[2].Value = esAdicional ? "1" : "0";
                varParametros[3] = new SqlParameter("@tnIdServicioHorario", SqlDbType.Int);
                if (!string.IsNullOrEmpty(idServicioHorario)) varParametros[3].Value = idServicioHorario;
                else varParametros[3].Value = null;
                varParametros[4] = new SqlParameter("@tlEsProcedimiento", SqlDbType.Bit);
                varParametros[4].Value = esProcedimiento;
                string data = "";
                object obj = varConexion.EjecutarProcedimiento("APP_Proc_Horario_ObtenerDatos", varParametros, TipoProcesamiento.Scalar, false);
                if (obj != null)
                {
                    data = obj.ToString();
                }
                return data;
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

        public ValidarAccionBE ValidarAccionCita(string idCitaPresencial, string idCita, string tipoAccion, string origen)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[4];
                varParametros[0] = new SqlParameter("@IDCitaPresencial", SqlDbType.Int);
                if (!string.IsNullOrEmpty(idCitaPresencial))
                {
                    varParametros[0].Value = idCitaPresencial;
                }
                else
                {
                    varParametros[0].Value = null;
                }
                varParametros[1] = new SqlParameter("@IDCitaVirtual", SqlDbType.Int);
                if (!string.IsNullOrEmpty(idCita))
                {
                    varParametros[1].Value = idCita;
                }
                else
                {
                    varParametros[1].Value = null;
                }
                varParametros[2] = new SqlParameter("@TipoAccion", SqlDbType.VarChar);
                varParametros[2].Value = tipoAccion;
                varParametros[3] = new SqlParameter("@Origen", SqlDbType.VarChar);
                varParametros[3].Value = origen;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_ValidarAccion", varParametros, TipoProcesamiento.DataReader, false);

                ValidarAccionBE varResultado = null;
                if (varDataReader.Read())
                {
                    varResultado = new ValidarAccionBE();
                    //varResultado.indValido = varDataReader.GetString(varDataReader.GetOrdinal("IndError")).Equals("0");
                    varResultado.mensaje = varDataReader.GetString(varDataReader.GetOrdinal("Mensaje"));
                    varResultado.indValido = String.IsNullOrEmpty(varResultado.mensaje);
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

        public VideollamadaBE ObtenerDatosVideollamada(string idCita, string tipoAmbienteTwilio = "DESA")
        {
            ConexionUtil varConexion = new ConexionUtil();
            VideollamadaBE oVideollamadaBE = null;
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];

                varParametros[0] = new SqlParameter("@IDCitaVirtual", SqlDbType.Int);
                varParametros[0].Value = idCita;
                varParametros[1] = new SqlParameter("@TipoAmbienteTwilio", SqlDbType.VarChar);
                varParametros[1].Value = tipoAmbienteTwilio;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Twilio_ObtenerDatosVideollamada", varParametros, TipoProcesamiento.DataReader, false);
                if (varDataReader != null)
                {
                    if (varDataReader.Read()) 
                    {
                        oVideollamadaBE = new VideollamadaBE()
                        {
                            AccountSid = varDataReader.GetString(varDataReader.GetOrdinal("AccountSid")),
                            ApiKey = varDataReader.GetString(varDataReader.GetOrdinal("ApiKey")),
                            ApiSecret = varDataReader.GetString(varDataReader.GetOrdinal("ApiSecret")),
                            room_name = varDataReader.GetString(varDataReader.GetOrdinal("RoomName")),
                            paciente = varDataReader.GetString(varDataReader.GetOrdinal("Paciente")),
                            medico = varDataReader.GetString(varDataReader.GetOrdinal("Medico")),
                            especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")),
                            fechaAtencion = varDataReader.GetString(varDataReader.GetOrdinal("FechaAtencion")),
                            horaInicio = varDataReader.GetString(varDataReader.GetOrdinal("HoraInicio"))
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                //oVideollamadaBE = null;
                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
            return oVideollamadaBE;
        }

        public VideollamadaMedicoBE ObtenerDatosVideollamadaMedico(string room_name, string tipoAmbienteTwilio = "DESA")
        {
            ConexionUtil varConexion = new ConexionUtil();
            VideollamadaMedicoBE oVideollamadaMedicoBE = null;
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];

                varParametros[0] = new SqlParameter("@RoomName", SqlDbType.VarChar);
                varParametros[0].Value = room_name;
                varParametros[1] = new SqlParameter("@TipoAmbienteTwilio", SqlDbType.VarChar);
                varParametros[1].Value = tipoAmbienteTwilio;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Twilio_ObtenerDatosVideollamadaMedico", varParametros, TipoProcesamiento.DataReader, false);
                if (varDataReader != null)
                {
                    if (varDataReader.Read())
                    {
                        oVideollamadaMedicoBE = new VideollamadaMedicoBE()
                        {
                            AccountSid = varDataReader.GetString(varDataReader.GetOrdinal("AccountSid")),
                            ApiKey = varDataReader.GetString(varDataReader.GetOrdinal("ApiKey")),
                            ApiSecret = varDataReader.GetString(varDataReader.GetOrdinal("ApiSecret")),
                            medico = varDataReader.GetString(varDataReader.GetOrdinal("Medico")),
                            paciente = varDataReader.GetString(varDataReader.GetOrdinal("Paciente")),
                            fechaAtencion = varDataReader.GetString(varDataReader.GetOrdinal("FechaAtencion")),
                            horaInicio = varDataReader.GetString(varDataReader.GetOrdinal("HoraInicio")),
                            idCitaVirtual = varDataReader.GetString(varDataReader.GetOrdinal("IDCitaVirtual"))
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                //oVideollamadaBE = null;
                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
            return oVideollamadaMedicoBE;
        }

        public List<NovedadesBE> Novedades_ListarCitas(string tipoDocumento, string numeroDocumento)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Novedades_ListarCitas", varParametros, TipoProcesamiento.DataReader, false);

                NovedadesBE varResultadoTemporal = new NovedadesBE();
                NovedadesBE varResultado = new NovedadesBE();

                List<NovedadesBE> lNovedadesBE = new List<NovedadesBE>();
                NovedadesBE oNovedadesBE;

                string urlBase = ConfigurationManager.AppSettings["URLImagenes2"].ToString();
                string urlBaseIcono = ConfigurationManager.AppSettings["rutaPublicaIcono"].ToString();
                string fotoMedico;
                if (varDataReader != null)
                {
                    while (varDataReader.Read())
                    {
                        oNovedadesBE = new NovedadesBE()
                        {
                            idCita = varDataReader.GetInt32(varDataReader.GetOrdinal("IDCita")).ToString(),
                            nombrePaciente = varDataReader.GetString(varDataReader.GetOrdinal("NombreUsuario")),
                            nombrePaciente2 = varDataReader.GetString(varDataReader.GetOrdinal("NombreUsuario2")),
                            cmp = varDataReader.GetString(varDataReader.GetOrdinal("CMP")),
                            nombreMedico = varDataReader.GetString(varDataReader.GetOrdinal("NombreMedico")),
                            //fechaAtencion = char.ToUpper(varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dddd dd/MM/yyyy", new CultureInfo("es-PE"))[0]) + varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dddd dd/MM/yyyy", new CultureInfo("es-PE")).Substring(1),
                            fechaAtencion = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dd/MM/yyyy"),
                            fechaOrdenamiento = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaHoraOrdenamiento")),
                            horaInicio = varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")).ToString(@"hh\:mm"),
                            horaFin = varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraFin")).ToString(@"hh\:mm"),
                            idEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDEspecialidad")).ToString(),
                            especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")),
                            idClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString(),
                            clinica = varDataReader.GetString(varDataReader.GetOrdinal("Clinica")),
                            idMedicoFavorito = varDataReader.IsDBNull(varDataReader.GetOrdinal("IDMedicoFavorito")) ? null : varDataReader.GetInt32(varDataReader.GetOrdinal("IDMedicoFavorito")).ToString(),
                            //esCitaVirtual = varDataReader.GetString(varDataReader.GetOrdinal("EsCitaVirtual")),
                            esCitaVirtual = varDataReader.GetString(varDataReader.GetOrdinal("EsCitaVirtual")).Equals("1"),
                            fuePagado = varDataReader.IsDBNull(varDataReader.GetOrdinal("FuePagado")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("FuePagado")),
                            descripcionPago = varDataReader.GetString(varDataReader.GetOrdinal("DescripcionPago")),
                            tipoPago = varDataReader.GetString(varDataReader.GetOrdinal("TipoPago")),
                            tiempoPrevioCita = varDataReader.GetInt32(varDataReader.GetOrdinal("TiempoPrevioCita")),
                            anular = (varDataReader.GetString(varDataReader.GetOrdinal("Anular")) == "1") ? true : false,
                            anularPago = (varDataReader.GetString(varDataReader.GetOrdinal("AnularPago")) == "Si") ? true : false,
                            mostrarBotonesPago = (varDataReader.GetString(varDataReader.GetOrdinal("MostrarBotonesPago")) == "1") ? true : false,
                            mostrarBotonesPag = (varDataReader.GetString(varDataReader.GetOrdinal("MostrarBotonesPago")) == "1") ? true : false,
                            mostrarFilaEspera = true,
                            consultorio = (varDataReader.IsDBNull(varDataReader.GetOrdinal("Consultorio"))) ? " " : varDataReader.GetString(varDataReader.GetOrdinal("Consultorio")),
                            codigoTipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("CodigoTipoPaciente")),
                            tipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("TipoPaciente")),
                            codigoTipoAtencionHorario = varDataReader.GetString(varDataReader.GetOrdinal("CodigoTipoAtencionHorario")),
                            tipoAtencionHorario = varDataReader.GetString(varDataReader.GetOrdinal("TipoAtencionHorario")),
                            codigoTipoAtencionCita = varDataReader.GetString(varDataReader.GetOrdinal("CodigoTipoAtencionCita")),
                            tipoAtencionCita = varDataReader.GetString(varDataReader.GetOrdinal("TipoAtencionCita")),
                            tipoDocumento = varDataReader.GetInt32(varDataReader.GetOrdinal("TipoDocumento")),
                            numeroDocumento = varDataReader.GetString(varDataReader.GetOrdinal("NumeroDocumento")),
                            idHorario = varDataReader.GetInt32(varDataReader.GetOrdinal("IDHorario")),
                            cantidadPersonasDelante = varDataReader.GetString(varDataReader.GetOrdinal("CantidadPersonasDelante")),
                            subEspecialidad = varDataReader.GetString(varDataReader.GetOrdinal("SubEspecialidad")),
                            idEstado = varDataReader.GetInt32(varDataReader.GetOrdinal("IDEstado")),
                            estado = varDataReader.GetString(varDataReader.GetOrdinal("Estado")),
                            tiempoPrevioColaVirtual = varDataReader.GetString(varDataReader.GetOrdinal("TiempoPrevioColaVirtual")),
                            tiempoPrevioIngresoVirtual = varDataReader.GetString(varDataReader.GetOrdinal("TiempoPrevioIngresoVirtual")),
                            tiempoPosteriorIngresoVirtual = varDataReader.GetString(varDataReader.GetOrdinal("TiempoPosteriorIngresoVirtual")),
                            abreviaturaMedico = varDataReader.GetString(varDataReader.GetOrdinal("AbreviaturaMedico")),
                            sexoMedico = varDataReader.GetString(varDataReader.GetOrdinal("SexoMedico")),
                            metodoPago = varDataReader.GetString(varDataReader.GetOrdinal("MetodoPago")),
                            //fotoMedico = varDataReader.GetString(varDataReader.GetOrdinal("FotoMedico")),

                            esAdicional = varDataReader.GetBoolean(varDataReader.GetOrdinal("EsAdicional")),
                            textoAdicional = varDataReader.GetString(varDataReader.GetOrdinal("TextoAdicional"))
                        };
                        fotoMedico = varDataReader.GetString(varDataReader.GetOrdinal("FotoMedico"));
                        oNovedadesBE.fotoMedico = !string.IsNullOrEmpty(fotoMedico) ? fotoMedico : (urlBase + "Medicos/" + (oNovedadesBE.sexoMedico.Equals("M") ? "medico-m.png" : "medico-f.png"));
                        oNovedadesBE.indicadorHospitalizacion = false;
                        oNovedadesBE.codigoAtencion = varDataReader.GetString(varDataReader.GetOrdinal("CodigoAtencion"));
                        oNovedadesBE.codigoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("CodigoPaciente"));
                        oNovedadesBE.idCitaProcedimiento = varDataReader.GetString(varDataReader.GetOrdinal("IdCitaProcemiento"));
                        if (!String.IsNullOrEmpty(oNovedadesBE.codigoAtencion))
                        {
                            oNovedadesBE.tieneReceta = varDataReader.GetBoolean(varDataReader.GetOrdinal("TieneReceta"));
                            if (oNovedadesBE.tieneReceta)
                            {
                                oNovedadesBE.iconoReceta = urlBaseIcono + "ico-receta.png";
                            }
                            oNovedadesBE.tieneHojaRuta = varDataReader.GetBoolean(varDataReader.GetOrdinal("TieneHojaRuta"));
                            if (oNovedadesBE.tieneHojaRuta)
                            {
                                oNovedadesBE.iconoHojaRuta = urlBaseIcono + "ico-hoja-de-ruta.png";
                            }
                        }
                        if (!string.IsNullOrEmpty(oNovedadesBE.idCitaProcedimiento))
                        {
                            oNovedadesBE.procedimiento = varDataReader.GetString(varDataReader.GetOrdinal("Procedimiento"));
                            oNovedadesBE.idServicioHorario = varDataReader.GetString(varDataReader.GetOrdinal("IdServicioHorario"));
                            oNovedadesBE.idServicio = varDataReader.GetString(varDataReader.GetOrdinal("IdServicio"));
                            oNovedadesBE.servicio = varDataReader.GetString(varDataReader.GetOrdinal("Servicio"));
                            oNovedadesBE.tipoTerapia = varDataReader.GetString(varDataReader.GetOrdinal("TipoTerapia"));
                            oNovedadesBE.numeroSesion = varDataReader.GetString(varDataReader.GetOrdinal("NumeroSesion"));
                        }
                        lNovedadesBE.Add(oNovedadesBE);
                    }
                }

                return lNovedadesBE;
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

        public TurnoPacienteBE CrearTurnoPaciente(string idCitaPresencial, string idCitaVirtual, string origen, string idUsuario, string observaciones)
        {
            TurnoPacienteBE oTurnoPacienteBE = null; //IDCita de la tabla Cita
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[5];
                varParametros[0] = new SqlParameter("@IDCita", SqlDbType.Int);
                if (!String.IsNullOrEmpty(idCitaPresencial)) varParametros[0].Value = idCitaPresencial;
                else varParametros[0].Value = null;
                varParametros[1] = new SqlParameter("@IDCitaVirtual", SqlDbType.Int);
                if (!String.IsNullOrEmpty(idCitaVirtual)) varParametros[1].Value = idCitaVirtual;
                else varParametros[1].Value = null; 
                varParametros[2] = new SqlParameter("@Origen", SqlDbType.VarChar);
                if (!String.IsNullOrEmpty(origen)) varParametros[2].Value = origen;
                else varParametros[2].Value = null;
                varParametros[3] = new SqlParameter("@IDUsuario", SqlDbType.Int);
                if (!String.IsNullOrEmpty(idUsuario)) varParametros[3].Value = idUsuario;
                else varParametros[3].Value = null;
                varParametros[4] = new SqlParameter("@Observaciones", SqlDbType.VarChar);
                if (!String.IsNullOrEmpty(observaciones)) varParametros[4].Value = observaciones;
                else varParametros[4].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_CrearTurnoPaciente", varParametros, TipoProcesamiento.DataReader, false);

                if (varDataReader != null)
                {
                    if (varDataReader.Read())
                    {
                        oTurnoPacienteBE = new TurnoPacienteBE();
                        oTurnoPacienteBE.indGestorColasWS = varDataReader.GetBoolean(varDataReader.GetOrdinal("IndGestorColasWS"));
                        oTurnoPacienteBE.idCita = varDataReader.GetString(varDataReader.GetOrdinal("IDCita"));
                    }
                }

                return oTurnoPacienteBE;

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

        public void CrearTurnoPaciente_Log(string idCita, string codigoError, string descripcionError, string origenBMatic)
        {
           ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[4];
                varParametros[0] = new SqlParameter("@IDCita", SqlDbType.Int);
                varParametros[0].Value = idCita;
                varParametros[1] = new SqlParameter("@CodigoError", SqlDbType.VarChar);
                varParametros[1].Value = codigoError;
                varParametros[2] = new SqlParameter("@MensajeError", SqlDbType.VarChar);
                varParametros[2].Value = descripcionError;
                varParametros[3] = new SqlParameter("@Origen", SqlDbType.VarChar);
                if (!String.IsNullOrEmpty(origenBMatic)) varParametros[3].Value = origenBMatic;
                else varParametros[3].Value = null;

                object data = varConexion.EjecutarProcedimiento("uspLogApiRegTurno", varParametros, TipoProcesamiento.NonQuery);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //varConexion.Desconectar();
            }
        }

        //public List<OpcionPagoBE> ListarOpcionesPago(string tipoDocumento, string numeroDocumento, string idCitaPresencial, string idCitaVirtual, string tipoPaciente)
        //{
        //    ConexionUtil varConexion = new ConexionUtil();
        //    try
        //    {
        //        SqlParameter[] varParametros = new SqlParameter[5];
        //        varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
        //        varParametros[0].Value = tipoDocumento;
        //        varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
        //        varParametros[1].Value = numeroDocumento;
        //        varParametros[2] = new SqlParameter("@TipoPaciente", SqlDbType.Int);
        //        if (!string.IsNullOrEmpty(tipoPaciente)) varParametros[2].Value = tipoPaciente;
        //        else varParametros[2].Value = null;
        //        varParametros[3] = new SqlParameter("@IDCita", SqlDbType.Int);
        //        if (!string.IsNullOrEmpty(idCitaPresencial)) varParametros[3].Value = idCitaPresencial;
        //        else varParametros[3].Value = null;
        //        varParametros[4] = new SqlParameter("@IDCitaVirtual", SqlDbType.Int);
        //        if (!string.IsNullOrEmpty(idCitaVirtual)) varParametros[4].Value = idCitaVirtual;
        //        else varParametros[4].Value = null;

        //        varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_ListarSeguroParticular", varParametros, TipoProcesamiento.DataReader, false);

        //        List<OpcionPagoBE> lOpcionPagoBE = new List<OpcionPagoBE>();
        //        OpcionPagoBE oOpcionPagoBE;

        //        if (varDataReader != null)
        //        {
        //            while (varDataReader.Read())
        //            {
        //                if (!varDataReader.IsDBNull(varDataReader.GetOrdinal("CopagoFijo")))
        //                {
        //                    oOpcionPagoBE = new OpcionPagoBE()
        //                    {
        //                        tipoTarifa = varDataReader.GetString(varDataReader.GetOrdinal("TipoTarifa")),
        //                        RUCSeguro = varDataReader.IsDBNull(varDataReader.GetOrdinal("RucSeguro")) ? "" : varDataReader.GetString(varDataReader.GetOrdinal("RucSeguro")),
        //                        IAFAS = varDataReader.GetString(varDataReader.GetOrdinal("IAFA")),
        //                        nombre = varDataReader.IsDBNull(varDataReader.GetOrdinal("NombreIAFA")) ? "" : varDataReader.GetString(varDataReader.GetOrdinal("NombreIAFA")),
        //                        codigoProducto = varDataReader.GetString(varDataReader.GetOrdinal("CodigoProducto")),
        //                        nombreProducto = varDataReader.GetString(varDataReader.GetOrdinal("NombreProducto")),
        //                        codigoCobertura = varDataReader.GetString(varDataReader.GetOrdinal("CodigoCobertura")),
        //                        nombreCobertura = varDataReader.GetString(varDataReader.GetOrdinal("NombreCobertura")),
        //                        monto = varDataReader.GetDecimal(varDataReader.GetOrdinal("CopagoFijo")).ToString(),
        //                        origenMonto = varDataReader.GetString(varDataReader.GetOrdinal("OrigenMonto")),
        //                        fechaPago = varDataReader.IsDBNull(varDataReader.GetOrdinal("FechaPago")) ? null : varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaPago")).ToString("dd/MM/yyyy HH:mm:ss.fff"),
        //                        parentesco = varDataReader.IsDBNull(varDataReader.GetOrdinal("Parentesco")) ? "" : varDataReader.GetString(varDataReader.GetOrdinal("Parentesco")),
        //                        codigoParentesco = varDataReader.IsDBNull(varDataReader.GetOrdinal("CodParentesco")) ? "" : varDataReader.GetString(varDataReader.GetOrdinal("CodParentesco")),
        //                        codigoAfiliado = varDataReader.IsDBNull(varDataReader.GetOrdinal("CodigoAfiliado")) ? "" : varDataReader.GetString(varDataReader.GetOrdinal("CodigoAfiliado")),
        //                        tipoDocumentoContratante = varDataReader.IsDBNull(varDataReader.GetOrdinal("CodTipoDocumentoContratante")) ? "" : varDataReader.GetString(varDataReader.GetOrdinal("CodTipoDocumentoContratante")),
        //                        numeroDocumentoContratante = varDataReader.IsDBNull(varDataReader.GetOrdinal("NumeroDocumentoContratante")) ? "" : varDataReader.GetString(varDataReader.GetOrdinal("NumeroDocumentoContratante"))
        //                    };
        //                    if (oOpcionPagoBE.tipoTarifa.Equals("Particular") && String.IsNullOrEmpty(oOpcionPagoBE.nombre))
        //                    {
        //                        oOpcionPagoBE.nombre = "Particular";
        //                    }
        //                    lOpcionPagoBE.Add(oOpcionPagoBE);
        //                }
        //            }
        //        }
                
        //        return lOpcionPagoBE;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        varConexion.Desconectar();
        //    }
        //}
        public DatosPagoBEV2 ObtenerDatosPagoV2(string idCita, string idCitaVirtual)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                if (!string.IsNullOrEmpty(idCita) && string.IsNullOrEmpty(idCitaVirtual))
                {
                    SqlParameter[] varParametros = new SqlParameter[1];
                    varParametros[0] = new SqlParameter("@IDCita", SqlDbType.Int);
                    varParametros[0].Value = idCita;

                    varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_ObtenerDatosPago", varParametros, TipoProcesamiento.DataReader, false);
                }
                if (string.IsNullOrEmpty(idCita) && !string.IsNullOrEmpty(idCitaVirtual))
                {
                    SqlParameter[] varParametros = new SqlParameter[1];
                    varParametros[0] = new SqlParameter("@IDCitaVirtual", SqlDbType.Int);
                    varParametros[0].Value = idCitaVirtual;

                    varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_CitaVirtual_ObtenerDatosPago", varParametros, TipoProcesamiento.DataReader, false);
                }

                DatosPagoBEV2 varResultado = new DatosPagoBEV2();
                if (varDataReader != null)
                {
                    if (varDataReader.Read()) 
                    {
                        varResultado = new DatosPagoBEV2()
                        {
                            purchaseNumber = varDataReader.GetInt64(varDataReader.GetOrdinal("CodeTransaccion")).ToString(),
                            especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")).ToString(),
                            nombreMedico = varDataReader.GetString(varDataReader.GetOrdinal("NombreMedico")).ToString(),
                            nombrePaciente = varDataReader.GetString(varDataReader.GetOrdinal("NombrePaciente")).ToString(),
                            fechaAtencion = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaAtencion")).ToString("dd/MM/yyyy"),
                            horaInicio = varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")).ToString(@"hh\:mm"),
                            tokenEmail = varDataReader.GetString(varDataReader.GetOrdinal("DataUserToken")).ToString(),
                            clinica = varDataReader.GetString(varDataReader.GetOrdinal("Clinica")).ToString(),
                            consultorio = varDataReader.GetString(varDataReader.GetOrdinal("Consultorio")).ToString(),
                            tiempoAtencion = varDataReader.GetString(varDataReader.GetOrdinal("TiempoAtencion")).ToString()
                        };
                    }
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

        public bool RegistrarRoomSid(string idCitaVirtual, string origen, string room_sid, bool esMedico)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[4];

                varParametros[0] = new SqlParameter("@IDCitaVirtual", SqlDbType.Int);
                varParametros[0].Value = idCitaVirtual;

                varParametros[1] = new SqlParameter("@Origen", SqlDbType.VarChar);
                if (!String.IsNullOrEmpty(origen)) varParametros[1].Value = origen;
                else varParametros[1].Value = null;

                varParametros[2] = new SqlParameter("@RoomSid", SqlDbType.VarChar);
                varParametros[2].Value = room_sid;

                varParametros[3] = new SqlParameter("@EsMedico", SqlDbType.Bit);
                varParametros[3].Value = esMedico;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_RegistrarRoomSid", varParametros, TipoProcesamiento.DataReader, false);

                if (varDataReader != null)
                {
                    if (varDataReader.Read())
                    {
                        return true;
                    }
                }

                return false;

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
        public VideollamadaPendienteBE ConsultaVideollamadaPendiente(string type, string status)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                VideollamadaPendienteBE oVideollamadaPendienteBE = new VideollamadaPendienteBE();
                SqlParameter[] varParametros = new SqlParameter[2];

                varParametros[0] = new SqlParameter("@Type", SqlDbType.VarChar);
                varParametros[0].Value = type;

                varParametros[1] = new SqlParameter("@Status", SqlDbType.VarChar);
                varParametros[1].Value = status;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_ConsultarVideollamadaPendiente", varParametros, TipoProcesamiento.DataReader, false);

                List<string> lRoomName = new List<string>();
                if (varDataReader != null)
                {
                    while (varDataReader.Read())
                    {
                        lRoomName.Add(varDataReader.GetString(varDataReader.GetOrdinal("RoomName")));
                    }
                } 
                //oVideollamadaPendienteBE.twilio_room_sid = lRoomSid.ToArray();
                oVideollamadaPendienteBE.room_name = lRoomName.ToArray();

                return oVideollamadaPendienteBE;

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
        public bool ActualizarEstadoVideollamada(string type, string status, string room_name, string[] twilio_room_sid)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[4];

                varParametros[0] = new SqlParameter("@Type", SqlDbType.VarChar);
                varParametros[0].Value = type;

                varParametros[1] = new SqlParameter("@Status", SqlDbType.VarChar);
                varParametros[1].Value = status;

                varParametros[2] = new SqlParameter("@RoomName", SqlDbType.VarChar);
                varParametros[2].Value = room_name;

                twilio_room_sid = twilio_room_sid != null ? twilio_room_sid : new string[0];
                varParametros[3] = new SqlParameter("@ListaTwillioRoomSid", SqlDbType.VarChar);
                varParametros[3].Value = String.Join("¬", twilio_room_sid);

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_ActualizarEstadoVideollamada", varParametros, TipoProcesamiento.DataReader, false);

                if (varDataReader != null)
                {
                    if (varDataReader.Read())
                    {
                        return true;
                    }
                }

                return false;

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

        public bool ReprogramarCitaClinicaCSF(string idCitaOriginal, string idCita)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];

                varParametros[0] = new SqlParameter("@IdCitaReagendada", SqlDbType.Int);
                varParametros[0].Value = idCitaOriginal;
                varParametros[1] = new SqlParameter("@IdCitaNuevo", SqlDbType.Int);
                varParametros[1].Value = idCita;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("Sp_CitaEjecutaReagendamiento", varParametros, TipoProcesamiento.DataReader, false);
                
            }
            catch (Exception ex)
            {
                new ErrorDA().RegistrarErrorV2(ex, "WS", "CitaDA/RegistrarCita: Sp_CitaEjecutaReagendamiento");
                return false;
            }
            return true;
        }

        public bool AnularCitaClinicaCSF(string idCita)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@ide_cita", SqlDbType.Int);
                varParametros[0].Value = idCita;

                varConexion.EjecutarProcedimiento("Sp_Cita_Anulacion_EnviaCorreo", varParametros, TipoProcesamiento.NonQuery);
            }
            catch (Exception ex)
            {
                new ErrorDA().RegistrarErrorV2(ex, "WS", "CitaDA/AnularCita: Sp_Cita_Anulacion_EnviaCorreo");
                return false;
            }
            return true;
        }

        public List<OpcionPagoBE> ListarOpcionesPago(string tipoDocumento, string numeroDocumento, string idCitaPresencial, string idCitaVirtual, string tipoPaciente,
            string IAFA, string guid
            )
        {
            List<OpcionPagoBE> lOpcionPagoBE = new List<OpcionPagoBE>();
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[7];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@TipoPaciente", SqlDbType.Int);
                if (!string.IsNullOrEmpty(tipoPaciente)) varParametros[2].Value = tipoPaciente;
                else varParametros[2].Value = null;
                varParametros[3] = new SqlParameter("@IDCita", SqlDbType.Int);
                if (!string.IsNullOrEmpty(idCitaPresencial)) varParametros[3].Value = idCitaPresencial;
                else varParametros[3].Value = null;
                varParametros[4] = new SqlParameter("@IDCitaVirtual", SqlDbType.Int);
                if (!string.IsNullOrEmpty(idCitaVirtual)) varParametros[4].Value = idCitaVirtual;
                else varParametros[4].Value = null;
                varParametros[5] = new SqlParameter("@pIAFAS", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(IAFA)) varParametros[5].Value = IAFA;
                else varParametros[5].Value = null;
                varParametros[6] = new SqlParameter("@pGuid", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(guid)) varParametros[6].Value = guid;
                else varParametros[6].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_ListarSeguroParticularParalelo", varParametros, TipoProcesamiento.DataReader, false);

                OpcionPagoBE oOpcionPagoBE;

                if (varDataReader != null)
                {
                    while (varDataReader.Read())
                    {
                        if (!varDataReader.IsDBNull(varDataReader.GetOrdinal("CopagoFijo")))
                        {
                            oOpcionPagoBE = new OpcionPagoBE()
                            {
                                tipoTarifa = varDataReader.GetString(varDataReader.GetOrdinal("TipoTarifa")),
                                RUCSeguro = varDataReader.IsDBNull(varDataReader.GetOrdinal("RucSeguro")) ? "" : varDataReader.GetString(varDataReader.GetOrdinal("RucSeguro")),
                                IAFAS = varDataReader.GetString(varDataReader.GetOrdinal("IAFA")),
                                nombre = varDataReader.IsDBNull(varDataReader.GetOrdinal("NombreIAFA")) ? "" : varDataReader.GetString(varDataReader.GetOrdinal("NombreIAFA")),
                                codigoProducto = varDataReader.GetString(varDataReader.GetOrdinal("CodigoProducto")),
                                nombreProducto = varDataReader.GetString(varDataReader.GetOrdinal("NombreProducto")),
                                codigoCobertura = varDataReader.GetString(varDataReader.GetOrdinal("CodigoCobertura")),
                                nombreCobertura = varDataReader.GetString(varDataReader.GetOrdinal("NombreCobertura")),
                                monto = varDataReader.GetDecimal(varDataReader.GetOrdinal("CopagoFijo")).ToString(),
                                origenMonto = varDataReader.GetString(varDataReader.GetOrdinal("OrigenMonto")),
                                fechaPago = varDataReader.IsDBNull(varDataReader.GetOrdinal("FechaPago")) ? null : varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaPago")).ToString("dd/MM/yyyy HH:mm:ss.fff"),
                                parentesco = varDataReader.IsDBNull(varDataReader.GetOrdinal("Parentesco")) ? "" : varDataReader.GetString(varDataReader.GetOrdinal("Parentesco")),
                                codigoParentesco = varDataReader.IsDBNull(varDataReader.GetOrdinal("CodParentesco")) ? "" : varDataReader.GetString(varDataReader.GetOrdinal("CodParentesco")),
                                codigoAfiliado = varDataReader.IsDBNull(varDataReader.GetOrdinal("CodigoAfiliado")) ? "" : varDataReader.GetString(varDataReader.GetOrdinal("CodigoAfiliado")),
                                tipoDocumentoContratante = varDataReader.IsDBNull(varDataReader.GetOrdinal("CodTipoDocumentoContratante")) ? "" : varDataReader.GetString(varDataReader.GetOrdinal("CodTipoDocumentoContratante")),
                                numeroDocumentoContratante = varDataReader.IsDBNull(varDataReader.GetOrdinal("NumeroDocumentoContratante")) ? "" : varDataReader.GetString(varDataReader.GetOrdinal("NumeroDocumentoContratante"))
                            };
                            if (oOpcionPagoBE.tipoTarifa.Equals("Particular") && String.IsNullOrEmpty(oOpcionPagoBE.nombre))
                            {
                                oOpcionPagoBE.nombre = "Particular";
                            }
                            lOpcionPagoBE.Add(oOpcionPagoBE);
                        }
                    }
                }
                return lOpcionPagoBE;
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

        public List<string> ObtenerConfiguracionIAFAS()
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Cita_ObtenerConfiguracionIAFAS", null, TipoProcesamiento.DataReader, false);

                List<string> lIAFA = new List<string>();

                if (varDataReader != null)
                {
                    while (varDataReader.Read())
                    {
                        lIAFA.Add(varDataReader.GetString(varDataReader.GetOrdinal("Codigo")));
                    }
                }
                return lIAFA;
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

        public bool ValidarUsuarioCorreo(string tipoDocumento, string numeroDocumento, string correo)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[3];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@Correo", SqlDbType.VarChar);
                varParametros[2].Value = correo;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Usuario_ValidarUsuarioCorreo", varParametros, TipoProcesamiento.DataReader, false);

                varDataReader.Read();

                return true;

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

        public void ActualizarEstado(string tipoDocumento, string numeroDocumento, string idCitaPresencial,
            string idCitaVirtual, string origen, string estado, string idUsuario, string observaciones)
        {
            //TurnoPacienteBE oTurnoPacienteBE = null; //IDCita de la tabla Cita
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[8];
                varParametros[0] = new SqlParameter("@tnTipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@tvNumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@tnIDCita", SqlDbType.Int);
                if (!String.IsNullOrEmpty(idCitaPresencial)) varParametros[2].Value = idCitaPresencial;
                else varParametros[2].Value = null;
                varParametros[3] = new SqlParameter("@tnIDCitaVirtual", SqlDbType.Int);
                if (!String.IsNullOrEmpty(idCitaVirtual)) varParametros[3].Value = idCitaVirtual;
                else varParametros[3].Value = null;
                varParametros[4] = new SqlParameter("@tvOrigen", SqlDbType.VarChar);
                if (!String.IsNullOrEmpty(origen)) varParametros[4].Value = origen;
                else varParametros[4].Value = null;
                varParametros[5] = new SqlParameter("@tnEstadoSpring", SqlDbType.Int);
                if (!String.IsNullOrEmpty(estado)) varParametros[5].Value = estado;
                else varParametros[5].Value = null;
                varParametros[6] = new SqlParameter("@tnIDUsuario", SqlDbType.Int);
                if (!String.IsNullOrEmpty(idUsuario)) varParametros[6].Value = idUsuario;
                else varParametros[6].Value = null;
                varParametros[7] = new SqlParameter("@tvObservaciones", SqlDbType.VarChar);
                if (!String.IsNullOrEmpty(observaciones)) varParametros[7].Value = observaciones;
                else varParametros[7].Value = null;

                varConexion.EjecutarProcedimiento("Sp_Cita_UpdateEstadoSpring", varParametros, TipoProcesamiento.NonQuery);

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

        public CitaChatBotBE ObtenerPorId(string tipoDocumento, string numeroDocumento, string idCitaPresencial, string idCitaVirtual)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[4];

                varParametros[0] = new SqlParameter("@tnTipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@tvNumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@tnIDCita", SqlDbType.Int);
                if (!String.IsNullOrEmpty(idCitaPresencial)) varParametros[2].Value = idCitaPresencial;
                else varParametros[2].Value = null;
                varParametros[3] = new SqlParameter("@tnIDCitaVirtual", SqlDbType.Int);
                if (!String.IsNullOrEmpty(idCitaVirtual)) varParametros[3].Value = idCitaVirtual;
                else varParametros[3].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("Sp_Cita_ConsultaPorId", varParametros, TipoProcesamiento.DataReader, false);

                CitaChatBotBE oCitaChatBotBE = null;

                if (varDataReader != null)
                {
                    while (varDataReader.Read())
                    {
                        oCitaChatBotBE = new CitaChatBotBE()
                        {
                            clinica = varDataReader.GetString(varDataReader.GetOrdinal("Clinica")),
                            especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")),
                            generoMedico = varDataReader.GetString(varDataReader.GetOrdinal("GeneroMedico")),
                            medico = varDataReader.GetString(varDataReader.GetOrdinal("Medico")),
                            nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
                            apellidoPaterno = varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaterno")),
                            apellidoMaterno = varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaterno")),
                            fechaCita = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaCita")),
                            horaCita = varDataReader.GetString(varDataReader.GetOrdinal("HoraCita")),
                            esAdicional = varDataReader.GetBoolean(varDataReader.GetOrdinal("EsAdicional")),
                            esCitaVirtual = varDataReader.GetBoolean(varDataReader.GetOrdinal("EsCitaVirtual")),
                            horaInicioHorario = varDataReader.GetString(varDataReader.GetOrdinal("HoraInicioHorario")),
                            horaFinHorario = varDataReader.GetString(varDataReader.GetOrdinal("HoraFinHorario"))
                        };
                    }
                }

                return oCitaChatBotBE;

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

        public IndicadorFlyerBE ObtenerFlyer(string idCita, string idCitaVirtual)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@tnIdCita", SqlDbType.Int);
                if (!String.IsNullOrEmpty(idCita)) varParametros[0].Value = idCita;
                else varParametros[0].Value = null;
                varParametros[1] = new SqlParameter("@tnIdCitaVirtual", SqlDbType.Int);
                if (!String.IsNullOrEmpty(idCitaVirtual)) varParametros[1].Value = idCitaVirtual;
                else varParametros[1].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("Sp_Flyer_Consulta", varParametros, TipoProcesamiento.DataReader, false);

                IndicadorFlyerBE varResultado = null;
                while (varDataReader.Read())
                {
                    varResultado = new IndicadorFlyerBE()
                    {
                        mostrarFlyer = (varDataReader.GetString(varDataReader.GetOrdinal("mostrarFlyer"))).Equals("1"),
                        imagen = varDataReader.GetString(varDataReader.GetOrdinal("imagen"))
                    };
                    break;
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
    }
}
