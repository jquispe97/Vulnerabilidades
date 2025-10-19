using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSF.CITASWEB.WS.BE;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Configuration;

namespace CSF.CITASWEB.WS.DA
{
    public class MedicoDA
    {
        private SqlDataReader varDataReader;

        #region WS
        public MedicoBE InformacionMedico(string cmp)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@CMP", SqlDbType.VarChar);
                varParametros[0].Value = cmp;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Medico_ObtenerInformacion", varParametros, TipoProcesamiento.DataReader, false);

                MedicoBE varResultado = new MedicoBE();
                while (varDataReader.Read())
                {
                    varResultado = new MedicoBE()
                    {
                        nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
                        cargo = varDataReader.GetString(varDataReader.GetOrdinal("Cargo")),
                        mostrarCV = varDataReader.GetString(varDataReader.GetOrdinal("MuestraCV")),
                        tituloMedico = varDataReader.GetString(varDataReader.GetOrdinal("TituloMedico")),
                        premiosHonores = varDataReader.GetString(varDataReader.GetOrdinal("Premios")),
                        pertenenciaSociedad = varDataReader.GetString(varDataReader.GetOrdinal("PertenenciaSociedad")),
                        investigacionPublicaciones = varDataReader.GetString(varDataReader.GetOrdinal("Investigaciones")),
                        centrosAtencion = new List<ClinicaBE>(),
                        especialidad = new List<EspecialidadBE>(),
                        foto = varDataReader.GetString(varDataReader.GetOrdinal("Foto")),
                        RNE = varDataReader.GetString(varDataReader.GetOrdinal("RNE")),
                        idiomas = varDataReader.GetString(varDataReader.GetOrdinal("Idiomas")),
                        informacionAdicional = varDataReader.GetString(varDataReader.GetOrdinal("InformacionAdicional"))
                    };
                }

                //clínicas
                varDataReader.NextResult();
                while (varDataReader.Read())
                {
                    varResultado.centrosAtencion.Add(new ClinicaBE()
                    {
                        idClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString(),
                        nombre = varDataReader.GetString(varDataReader.GetOrdinal("Nombre"))
                    });
                }

                //especialidades
                varDataReader.NextResult();
                while (varDataReader.Read())
                {
                    varResultado.especialidad.Add(new EspecialidadBE()
                    {
                        idEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDEspecialidad")).ToString(),
                        especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Nombre"))
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

        public List<MedicoHorarioBE> HorariosPorMedico(string idClinica, string idEspecialidad, string tipoDocumento,
                                        string numeroDocumento, DateTime dia, string numeroDia, string nombre)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                if (!string.IsNullOrEmpty(idClinica) && !string.IsNullOrEmpty(idEspecialidad))
                {
                    SqlParameter[] varParametros = new SqlParameter[7];
                    varParametros[0] = new SqlParameter("@IDClinica", SqlDbType.Int);
                    varParametros[0].Value = idClinica;
                    varParametros[1] = new SqlParameter("@IDEspecialidad", SqlDbType.Int);
                    varParametros[1].Value = idEspecialidad;
                    varParametros[2] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                    varParametros[2].Value = tipoDocumento;
                    varParametros[3] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                    varParametros[3].Value = numeroDocumento;
                    varParametros[4] = new SqlParameter("@Dia", SqlDbType.DateTime);
                    varParametros[4].Value = dia;
                    varParametros[5] = new SqlParameter("@NumeroDia", SqlDbType.Int);
                    varParametros[5].Value = numeroDia;
                    varParametros[6] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    varParametros[6].Value = nombre;

                    varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Horario_PorClinicaEspecialidad", varParametros, TipoProcesamiento.DataReader, false);
                }
                else if (!string.IsNullOrEmpty(idClinica) && string.IsNullOrEmpty(idEspecialidad))
                {
                    SqlParameter[] varParametros = new SqlParameter[6];
                    varParametros[0] = new SqlParameter("@IDClinica", SqlDbType.Int);
                    varParametros[0].Value = idClinica;
                    varParametros[1] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                    varParametros[1].Value = tipoDocumento;
                    varParametros[2] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                    varParametros[2].Value = numeroDocumento;
                    varParametros[3] = new SqlParameter("@Dia", SqlDbType.DateTime);
                    varParametros[3].Value = dia;
                    varParametros[4] = new SqlParameter("@NumeroDia", SqlDbType.Int);
                    varParametros[4].Value = numeroDia;
                    varParametros[5] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    varParametros[5].Value = nombre;

                    varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Horario_PorClinica", varParametros, TipoProcesamiento.DataReader, false);
                }
                else if (string.IsNullOrEmpty(idClinica) && !string.IsNullOrEmpty(idEspecialidad))
                {
                    SqlParameter[] varParametros = new SqlParameter[6];
                    varParametros[0] = new SqlParameter("@IDEspecialidad", SqlDbType.Int);
                    varParametros[0].Value = idEspecialidad;
                    varParametros[1] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                    varParametros[1].Value = tipoDocumento;
                    varParametros[2] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                    varParametros[2].Value = numeroDocumento;
                    varParametros[3] = new SqlParameter("@Dia", SqlDbType.DateTime);
                    varParametros[3].Value = dia;
                    varParametros[4] = new SqlParameter("@NumeroDia", SqlDbType.Int);
                    varParametros[4].Value = numeroDia;
                    varParametros[5] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    varParametros[5].Value = nombre;

                    varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Horario_PorEspecialidad", varParametros, TipoProcesamiento.DataReader, false);
                }
                else
                {
                    SqlParameter[] varParametros = new SqlParameter[5];
                    varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                    varParametros[0].Value = tipoDocumento;
                    varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                    varParametros[1].Value = numeroDocumento;
                    varParametros[2] = new SqlParameter("@Dia", SqlDbType.DateTime);
                    varParametros[2].Value = dia;
                    varParametros[3] = new SqlParameter("@NumeroDia", SqlDbType.Int);
                    varParametros[3].Value = numeroDia;
                    varParametros[4] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    varParametros[4].Value = nombre;

                    varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Horario_SinFiltros", varParametros, TipoProcesamiento.DataReader, false);
                }

                List<MedicoHorarioBE> varResultado = new List<MedicoHorarioBE>();
                MedicoHorarioBE oMedicoHorarioBE;
                string urlBase = ConfigurationManager.AppSettings["URLImagenes2"].ToString();
                string foto;
                while (varDataReader.Read())
                {
                    oMedicoHorarioBE = new MedicoHorarioBE()
                    {
                        cmp = varDataReader.GetString(varDataReader.GetOrdinal("CMP")),
                        nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
                        especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")),
                        idClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString(),
                        idEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDEspecialidad")).ToString(),
                        centroAtencion = varDataReader.GetString(varDataReader.GetOrdinal("NombreClinica")),
                        idMedicoFavorito = varDataReader.IsDBNull(varDataReader.GetOrdinal("IDMedicoFavorito")) ? null : varDataReader.GetInt32(varDataReader.GetOrdinal("IDMedicoFavorito")).ToString(),
                        cvVisible = varDataReader.GetString(varDataReader.GetOrdinal("CVVisible")),
                        soloLlamadas = varDataReader.GetString(varDataReader.GetOrdinal("SoloLlamadas")),
                        idSubEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDSubEspecialidad")).ToString(),
                        subEspecialidad = varDataReader.GetString(varDataReader.GetOrdinal("SubEspecialidad")),
                        tipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("TipoPaciente")),
                        abreviatura = varDataReader.GetString(varDataReader.GetOrdinal("Abreviatura")),
                        sexo = varDataReader.GetString(varDataReader.GetOrdinal("Sexo")),
                        codigoTipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("CodigoTipoPaciente")),
                        unidadEspecializacion = varDataReader.GetString(varDataReader.GetOrdinal("UnidadEspecializacion")),
                        //foto = varDataReader.GetString(varDataReader.GetOrdinal("Foto")),
                        horariosEnteros = new List<HorariosBrutoBE>(),
                        horarios = new List<MedicoHorarioDetalleBE>(),
                        campoClinico = varDataReader.GetString(varDataReader.GetOrdinal("CampoClinico")),
                        prioridad = varDataReader.GetInt32(varDataReader.GetOrdinal("Prioridad"))
                    };
                    foto = varDataReader.GetString(varDataReader.GetOrdinal("Foto"));
                    oMedicoHorarioBE.foto = !string.IsNullOrEmpty(foto) ? foto : (urlBase + "Medicos/" + (oMedicoHorarioBE.sexo.Equals("M") ? "medico-m.png" : "medico-f.png"));
                    varResultado.Add(oMedicoHorarioBE);
                }

                varDataReader.NextResult();
                string varCMP,
                        varIDClinica;
                MedicoHorarioBE varMedicoHorario;
                while (varDataReader.Read())
                {
                    varCMP = varDataReader.GetString(varDataReader.GetOrdinal("CMP")).ToString();
                    varIDClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString();
                    try
                    {
                        varMedicoHorario = varResultado.Where(p => p.cmp == varCMP && p.idClinica == varIDClinica).FirstOrDefault();
                        if (varMedicoHorario != null)
                        {
                            varMedicoHorario.horariosEnteros.Add(new HorariosBrutoBE()
                            {
                                idDetalleHorarios = varDataReader.GetInt32(varDataReader.GetOrdinal("IDDetalleHorarios")),
                                idHorarioDetalle = varDataReader.GetString(varDataReader.GetOrdinal("IDHorarioDetalle")),
                                horaInicio = varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")),
                                tiempoAtencion = varDataReader.GetInt32(varDataReader.GetOrdinal("TiempoAtencion")),
                                cantidadTurnos = varDataReader.GetInt32(varDataReader.GetOrdinal("CantidadTurnos")),
                            });
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }


                if (dia.Date == DateTime.Today)
                {
                    TimeSpan varHoraActual = DateTime.Now.TimeOfDay;
                    foreach (MedicoHorarioBE medico in varResultado)
                    {
                        //2017-12-19 - Cambio para ordenar horarios
                        foreach (HorariosBrutoBE horario in medico.horariosEnteros.OrderBy(p => p.horaInicio))
                        //2017-12-19 - Cambio para ordenar horarios
                        {
                            for (int i = 0; i < horario.cantidadTurnos; i++)
                            {
                                if (horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)) >= varHoraActual)
                                    medico.horarios.Add(new MedicoHorarioDetalleBE()
                                    {
                                        idDetalleHorarios = horario.idDetalleHorarios,
                                        idHorarioDetalle = horario.idHorarioDetalle.ToString(),
                                        turno = (i + 1).ToString(),
                                        horaTime = horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)),
                                        hora = horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)).ToString(@"hh\:mm")
                                    });
                            }
                        }
                    }
                }
                else
                {
                    foreach (MedicoHorarioBE medico in varResultado)
                    {
                        //2017-12-19 - Cambio para ordenar horarios
                        foreach (HorariosBrutoBE horario in medico.horariosEnteros.OrderBy(p => p.horaInicio))
                        //2017-12-19 - Cambio para ordenar horarios
                        {
                            for (int i = 0; i < horario.cantidadTurnos; i++)
                            {
                                medico.horarios.Add(new MedicoHorarioDetalleBE()
                                {
                                    idDetalleHorarios = horario.idDetalleHorarios,
                                    idHorarioDetalle = horario.idHorarioDetalle.ToString(),
                                    turno = (i + 1).ToString(),
                                    horaTime = horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)),
                                    hora = horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)).ToString(@"hh\:mm")
                                });
                            }
                        }
                    }
                }

                varDataReader.NextResult();
                int varIDDetalleHorarios, varTurno;
                string varHoraInicioCita;
                List<MedicoHorarioDetalleBE> varMedicoHorarioDetalle;
                while (varDataReader.Read())
                {
                    varCMP = varDataReader.GetString(varDataReader.GetOrdinal("CMP")).ToString();
                    varIDDetalleHorarios = varDataReader.GetInt32(varDataReader.GetOrdinal("IDDetalleHorarios"));
                    varTurno = varDataReader.GetInt32(varDataReader.GetOrdinal("Turno"));
                    varHoraInicioCita = varDataReader.GetString(varDataReader.GetOrdinal("HoraInicio"));
                    varMedicoHorario = varResultado.Where(p => p.cmp == varCMP).FirstOrDefault();
                    if (varMedicoHorario != null)
                    {
                        //varMedicoHorarioDetalle = varMedicoHorario.horarios.Where(p => p.idDetalleHorarios == varIDDetalleHorarios && p.turno == varTurno.ToString()).ToList();
                        varMedicoHorarioDetalle = varMedicoHorario.horarios.Where(p => p.idDetalleHorarios == varIDDetalleHorarios && p.hora == varHoraInicioCita.ToString()).ToList();
                        if (varMedicoHorarioDetalle != null && varMedicoHorarioDetalle.Count > 0)
                            foreach (MedicoHorarioDetalleBE item in varMedicoHorarioDetalle)
                            {
                                varMedicoHorario.horarios.Remove(item);
                            }
                    }
                }

                varDataReader.NextResult();
                DateTime varFechaInicio, varFechaFin;
                while (varDataReader.Read())
                {
                    varCMP = varDataReader.GetString(varDataReader.GetOrdinal("CMP")).ToString();
                    varIDDetalleHorarios = varDataReader.GetInt32(varDataReader.GetOrdinal("IDDetalleHorarios"));
                    varFechaInicio = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaInicio"));
                    varFechaFin = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaFin"));
                    varMedicoHorario = varResultado.Where(p => p.cmp == varCMP).FirstOrDefault();
                    if (varMedicoHorario != null)
                    {
                        //2017-1-24 - Cambio para soportar todas las inasistencias de los médicos
                        varMedicoHorarioDetalle = varMedicoHorario.horarios.Where(p => p.idDetalleHorarios == varIDDetalleHorarios
                                                                                && (dia.Add(p.horaTime) >= varFechaInicio)
                                                                                && (dia.Add(p.horaTime) <= varFechaFin)).ToList();
                        if (varMedicoHorarioDetalle != null && varMedicoHorarioDetalle.Count > 0)
                            foreach (MedicoHorarioDetalleBE item in varMedicoHorarioDetalle)
                            {
                                varMedicoHorario.horarios.Remove(item);
                            }
                        //2017-1-24 - Cambio para soportar todas las inasistencias de los médicos
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

        public List<MedicoHorarioSimpleWebBE> HorariosPorNombreMedico(string idClinica, string idEspecialidad, string nombre,
                                        string tipoDocumento, string numeroDocumento, bool soloMedicosFavoritos = false)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[6];
                varParametros[0] = new SqlParameter("@IDEspecialidad", SqlDbType.Int);
                if (!string.IsNullOrEmpty(idEspecialidad))
                {
                    varParametros[0].Value = idEspecialidad;
                } 
                else
                {
                    varParametros[0].Value = null;
                }
                varParametros[1] = new SqlParameter("@IDClinica", SqlDbType.Int);
                if (!string.IsNullOrEmpty(idClinica))
                {
                    varParametros[1].Value = idClinica;
                }
                else
                {
                    varParametros[1].Value = null;
                }
                varParametros[2] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                varParametros[2].Value = nombre;
                varParametros[3] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[3].Value = tipoDocumento;
                varParametros[4] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[4].Value = numeroDocumento;
                varParametros[5] = new SqlParameter("@SoloMedicosFavoritos", SqlDbType.Bit);
                varParametros[5].Value = soloMedicosFavoritos;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Horario_PorNombreMedico", varParametros, TipoProcesamiento.DataReader, false);

                List<MedicoHorarioSimpleWebBE> varResultado = new List<MedicoHorarioSimpleWebBE>();
                MedicoHorarioSimpleWebBE oMedicoHorarioSimpleWebBE;
                string urlBase = ConfigurationManager.AppSettings["URLImagenes2"].ToString();
                string foto;
                while (varDataReader.Read())
                {
                    oMedicoHorarioSimpleWebBE = new MedicoHorarioSimpleWebBE()
                    {
                        cmp = varDataReader.GetString(varDataReader.GetOrdinal("CMP")),
                        nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
                        idEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDEspecialidad")).ToString(),
                        especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")),
                        idCentroAtencion = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString(),
                        centroAtencion = varDataReader.GetString(varDataReader.GetOrdinal("NombreClinica")),
                        idMedicoFavorito = varDataReader.IsDBNull(varDataReader.GetOrdinal("IDMedicoFavorito")) ? null : varDataReader.GetInt32(varDataReader.GetOrdinal("IDMedicoFavorito")).ToString(),
                        cvVisible = varDataReader.GetString(varDataReader.GetOrdinal("CVVisible")),
                        soloLlamadas = varDataReader.GetString(varDataReader.GetOrdinal("SoloLlamadas")),
                        //foto = varDataReader.GetString(varDataReader.GetOrdinal("Foto")),
                        idSubEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDSubEspecialidad")).ToString(),
                        subEspecialidad = varDataReader.GetString(varDataReader.GetOrdinal("SubEspecialidad")),
                        tipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("TipoPaciente")),
                        abreviatura = varDataReader.GetString(varDataReader.GetOrdinal("Abreviatura")),
                        sexo = varDataReader.GetString(varDataReader.GetOrdinal("Sexo")),
                        codigoTipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("CodigoTipoPaciente")),
                        unidadEspecializacion = varDataReader.GetString(varDataReader.GetOrdinal("UnidadEspecializacion")),
                        campoClinico = varDataReader.GetString(varDataReader.GetOrdinal("CampoClinico")),
                        mensajePersonalizado = varDataReader.GetString(varDataReader.GetOrdinal("MensajePersonalizado")),
                        telefonoSecretaria = varDataReader.GetString(varDataReader.GetOrdinal("TelefonoSecretaria")),
                        prioridad = varDataReader.GetInt32(varDataReader.GetOrdinal("Prioridad"))
                    };
                    foto = varDataReader.GetString(varDataReader.GetOrdinal("Foto"));
                    oMedicoHorarioSimpleWebBE.foto = !string.IsNullOrEmpty(foto) ? foto : (urlBase + "Medicos/" + (oMedicoHorarioSimpleWebBE.sexo.Equals("M") ? "medico-m.png" : "medico-f.png"));
                    varResultado.Add(oMedicoHorarioSimpleWebBE);
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

        public List<MedicoHorarioDisponibleBEV2> FechasDisponiblesPorMedico(string idClinica, string idEspecialidad, string cmp)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[3];
                varParametros[0] = new SqlParameter("@IDEspecialidad", SqlDbType.Int);
                varParametros[0].Value = idEspecialidad;
                varParametros[1] = new SqlParameter("@IDClinica", SqlDbType.Int);
                varParametros[1].Value = idClinica;
                varParametros[2] = new SqlParameter("@CMP", SqlDbType.VarChar);
                varParametros[2].Value = cmp;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Horario_MedicoFechas", varParametros, TipoProcesamiento.DataReader, false);

                List<MedicoHorarioDisponibleBEV2> varResultado = new List<MedicoHorarioDisponibleBEV2>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new MedicoHorarioDisponibleBEV2()
                    {
                        fechaDate = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")),
                        fecha = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")).ToString("dd/MM/yyyy")
                    });
                }
                varDataReader.NextResult();
                MHDHorarioBE oMHDHorarioBE = null;
                int i, nFechas = varResultado.Count;
                while (varDataReader.Read())
                {
                    oMHDHorarioBE = new MHDHorarioBE()
                    {
                        fechaDate = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")),
                        fecha = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")).ToString("dd/MM/yyyy"),
                        idHorarioDetalle = varDataReader.GetInt32(varDataReader.GetOrdinal("IDHorario")).ToString() + "|" + varDataReader.GetInt32(varDataReader.GetOrdinal("Dia")).ToString(),
                        numeroTurno = varDataReader.GetInt32(varDataReader.GetOrdinal("NumeroTurno")).ToString(),
                        horaInicio = varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")).ToString(@"hh\:mm"),
                        disponible = varDataReader.IsDBNull(varDataReader.GetOrdinal("IDCita")) ? "1" : "0"
                    };
                    for (i = 0; i < nFechas; i++)
                    {
                        if (varResultado[i].fecha.Equals(oMHDHorarioBE.fecha))
                        {
                            varResultado[i].horarios.Add(oMHDHorarioBE);
                            break;
                        }
                    }
                }
                for (i = 0; i < nFechas; i++)
                {
                    varResultado[i].horarios = varResultado[i].horarios.OrderBy(p => p.horaInicio).ToList();
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
        public List<MedicoHorarioDisponibleBE> FechasDisponiblesPorMedicoE(string idClinica, string idEspecialidad, string cmp, DateTime fecha)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[4];
                varParametros[0] = new SqlParameter("@IDEspecialidad", SqlDbType.Int);
                varParametros[0].Value = idEspecialidad;
                varParametros[1] = new SqlParameter("@IDClinica", SqlDbType.Int);
                varParametros[1].Value = idClinica;
                varParametros[2] = new SqlParameter("@CMP", SqlDbType.VarChar);
                varParametros[2].Value = cmp;
                varParametros[3] = new SqlParameter("@Fecha", SqlDbType.DateTime);
                varParametros[3].Value = fecha;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Horario_MedicoFechasEspecifica", varParametros, TipoProcesamiento.DataReader, false);

                List<MedicoHorarioDisponibleBE> varResultado = new List<MedicoHorarioDisponibleBE>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new MedicoHorarioDisponibleBE()
                    {
                        fechaDate = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")),
                        fecha = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")).ToString("dd/MM/yyyy"),
                        idHorarioDetalle = varDataReader.GetInt32(varDataReader.GetOrdinal("IDHorario")).ToString() + "|" + varDataReader.GetInt32(varDataReader.GetOrdinal("Dia")).ToString(),
                        numeroTurno = varDataReader.GetInt32(varDataReader.GetOrdinal("NumeroTurno")).ToString(),
                        horaInicio = varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")).ToString(@"hh\:mm"),
                        disponible = varDataReader.IsDBNull(varDataReader.GetOrdinal("IDCita")) ? "1" : "0"
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

        public List<DiasHorarioDisponibleBE> FechasDisponibles(string idClinica, string idEspecialidad)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@IDEspecialidad", SqlDbType.Int);
                varParametros[0].Value = idEspecialidad;
                varParametros[1] = new SqlParameter("@IDClinica", SqlDbType.Int);
                varParametros[1].Value = idClinica;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Horario_DiasDisponibles", varParametros, TipoProcesamiento.DataReader, false);

                List<DiasHorarioDisponibleBE> varResultado = new List<DiasHorarioDisponibleBE>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new DiasHorarioDisponibleBE()
                    {
                        fechaDate = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")),
                        fecha = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")).ToString("dd/MM/yyyy"),
                        turnosDisponibles = varDataReader.GetInt32(varDataReader.GetOrdinal("TurnosLibres")).ToString()
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

        //public List<MedicoHorarioBE> HorariosPorMedicoVirtual(string idClinica, string idEspecialidad, string tipoDocumento,
        //                                string numeroDocumento, DateTime dia, string numeroDia)
        //{
        //    ConexionUtil varConexion = new ConexionUtil();
        //    try
        //    {
        //        SqlParameter[] varParametros = new SqlParameter[6];
        //        varParametros[0] = new SqlParameter("@IDClinica", SqlDbType.Int);
        //        if (!string.IsNullOrEmpty(idClinica))
        //        {
        //            varParametros[0].Value = idClinica;
        //        }
        //        else
        //        {
        //            varParametros[0].Value = null;
        //        }
        //        varParametros[1] = new SqlParameter("@IDEspecialidad", SqlDbType.Int);
        //        varParametros[1].Value = idEspecialidad;
        //        varParametros[2] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
        //        varParametros[2].Value = tipoDocumento;
        //        varParametros[3] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
        //        varParametros[3].Value = numeroDocumento;
        //        varParametros[4] = new SqlParameter("@Dia", SqlDbType.DateTime);
        //        varParametros[4].Value = dia;
        //        varParametros[5] = new SqlParameter("@NumeroDia", SqlDbType.Int);
        //        varParametros[5].Value = numeroDia;

        //        varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Horario_Virtual", varParametros, TipoProcesamiento.DataReader, false);

        //        List<MedicoHorarioBE> varResultado = new List<MedicoHorarioBE>();
        //        MedicoHorarioBE oMedicoHorarioBE;
        //        string urlBase = ConfigurationManager.AppSettings["URLImagenes2"].ToString();
        //        string foto;
        //        while (varDataReader.Read())
        //        {
        //            //(new MedicoHorarioBE()
        //            oMedicoHorarioBE = new MedicoHorarioBE()
        //            {
        //                cmp = varDataReader.GetString(varDataReader.GetOrdinal("CMP")),
        //                nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
        //                especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")),
        //                idClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString(),
        //                idEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDEspecialidad")).ToString(),
        //                centroAtencion = varDataReader.GetString(varDataReader.GetOrdinal("NombreClinica")),
        //                idMedicoFavorito = varDataReader.IsDBNull(varDataReader.GetOrdinal("IDMedicoFavorito")) ? null : varDataReader.GetInt32(varDataReader.GetOrdinal("IDMedicoFavorito")).ToString(),
        //                cvVisible = varDataReader.GetString(varDataReader.GetOrdinal("CVVisible")),
        //                soloLlamadas = varDataReader.GetString(varDataReader.GetOrdinal("SoloLlamadas")),
        //                idSubEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDSubEspecialidad")).ToString(),
        //                subEspecialidad = varDataReader.GetString(varDataReader.GetOrdinal("SubEspecialidad")),
        //                tipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("TipoPaciente")),
        //                abreviatura = varDataReader.GetString(varDataReader.GetOrdinal("Abreviatura")),
        //                sexo = varDataReader.GetString(varDataReader.GetOrdinal("Sexo")),
        //                codigoTipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("CodigoTipoPaciente")),
        //                unidadEspecializacion = varDataReader.GetString(varDataReader.GetOrdinal("UnidadEspecializacion")),
        //                horariosEnteros = new List<HorariosBrutoBE>(),
        //                horarios = new List<MedicoHorarioDetalleBE>()
        //            };
        //            foto = varDataReader.GetString(varDataReader.GetOrdinal("Foto"));
        //            oMedicoHorarioBE.foto = !string.IsNullOrEmpty(foto) ? foto : (urlBase + "Medicos/" + (oMedicoHorarioBE.sexo.Equals("M") ? "medico-m.png" : "medico-f.png"));
        //            varResultado.Add(oMedicoHorarioBE);
        //        }

        //        varDataReader.NextResult();
        //        string varCMP,
        //                varIDClinica;
        //        MedicoHorarioBE varMedicoHorario;
        //        while (varDataReader.Read())
        //        {
        //            varCMP = varDataReader.GetString(varDataReader.GetOrdinal("CMP")).ToString();
        //            varIDClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString();
        //            try
        //            {
        //                varMedicoHorario = varResultado.Where(p => p.cmp == varCMP && p.idClinica == varIDClinica).FirstOrDefault();
        //                if (varMedicoHorario != null)
        //                {

        //                    varMedicoHorario.horariosEnteros.Add(new HorariosBrutoBE()
        //                    {
        //                        idDetalleHorarios = varDataReader.GetInt32(varDataReader.GetOrdinal("IDDetalleHorarios")),
        //                        idHorarioDetalle = varDataReader.GetString(varDataReader.GetOrdinal("IDHorarioDetalle")),
        //                        horaInicio = varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")),
        //                        tiempoAtencion = varDataReader.GetInt32(varDataReader.GetOrdinal("TiempoAtencion")),
        //                        cantidadTurnos = varDataReader.GetInt32(varDataReader.GetOrdinal("CantidadTurnos")),
        //                    });
        //                }
        //            }
        //            catch (Exception)
        //            {

        //                throw;
        //            }
        //        }

        //        varDataReader.NextResult();
        //        varDataReader.Read();
        //        int tiempoMinimoEspera = varDataReader.GetInt32(varDataReader.GetOrdinal("LimitePago"));

        //        DateTime fechaMinima = DateTime.Now.AddSeconds(tiempoMinimoEspera);
        //        if (dia.Date == DateTime.Today)
        //        {
        //            //TimeSpan varHoraActual = DateTime.Now.TimeOfDay;
        //            foreach (MedicoHorarioBE medico in varResultado)
        //            {
        //                foreach (HorariosBrutoBE horario in medico.horariosEnteros.OrderBy(p => p.horaInicio))
        //                {
        //                    for (int i = 0; i < horario.cantidadTurnos; i++)
        //                    {
        //                        //if (horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)) >= varHoraActual)
        //                        if ((dia + horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0))) >= fechaMinima)
        //                            medico.horarios.Add(new MedicoHorarioDetalleBE()
        //                            {
        //                                idDetalleHorarios = horario.idDetalleHorarios,
        //                                idHorarioDetalle = horario.idHorarioDetalle.ToString(),
        //                                turno = (i + 1).ToString(),
        //                                horaTime = horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)),
        //                                hora = horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)).ToString(@"hh\:mm")
        //                            });
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            foreach (MedicoHorarioBE medico in varResultado)
        //            {
        //                foreach (HorariosBrutoBE horario in medico.horariosEnteros.OrderBy(p => p.horaInicio))
        //                {
        //                    for (int i = 0; i < horario.cantidadTurnos; i++)
        //                    {
        //                        if ((dia + horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0))) >= fechaMinima)
        //                            medico.horarios.Add(new MedicoHorarioDetalleBE()
        //                            {
        //                                idDetalleHorarios = horario.idDetalleHorarios,
        //                                idHorarioDetalle = horario.idHorarioDetalle.ToString(),
        //                                turno = (i + 1).ToString(),
        //                                horaTime = horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)),
        //                                hora = horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)).ToString(@"hh\:mm")
        //                            });
        //                    }
        //                }
        //            }
        //        }

        //        varDataReader.NextResult();
        //        int varIDDetalleHorarios, varTurno;
        //        List<MedicoHorarioDetalleBE> varMedicoHorarioDetalle;
        //        while (varDataReader.Read())
        //        {
        //            varCMP = varDataReader.GetString(varDataReader.GetOrdinal("CMP")).ToString();
        //            varIDDetalleHorarios = varDataReader.GetInt32(varDataReader.GetOrdinal("IDDetalleHorarios"));
        //            varTurno = varDataReader.GetInt32(varDataReader.GetOrdinal("Turno"));
        //            varMedicoHorario = varResultado.Where(p => p.cmp == varCMP).First();
        //            varMedicoHorarioDetalle = varMedicoHorario.horarios.Where(p => p.idDetalleHorarios == varIDDetalleHorarios && p.turno == varTurno.ToString()).ToList();
        //            if (varMedicoHorarioDetalle != null && varMedicoHorarioDetalle.Count > 0)
        //                foreach (MedicoHorarioDetalleBE item in varMedicoHorarioDetalle)
        //                {
        //                    varMedicoHorario.horarios.Remove(item);
        //                }
        //        }

        //        varDataReader.NextResult();
        //        DateTime varFechaInicio, varFechaFin;
        //        while (varDataReader.Read())
        //        {
        //            varCMP = varDataReader.GetString(varDataReader.GetOrdinal("CMP")).ToString();
        //            varIDDetalleHorarios = varDataReader.GetInt32(varDataReader.GetOrdinal("IDDetalleHorarios"));
        //            varFechaInicio = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaInicio"));
        //            varFechaFin = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaFin"));
        //            varMedicoHorario = varResultado.Where(p => p.cmp == varCMP).First();
        //            varMedicoHorarioDetalle = varMedicoHorario.horarios.Where(p => p.idDetalleHorarios == varIDDetalleHorarios
        //                                                                    && (dia.Add(p.horaTime) >= varFechaInicio)
        //                                                                    && (dia.Add(p.horaTime) <= varFechaFin)).ToList();
        //            if (varMedicoHorarioDetalle != null && varMedicoHorarioDetalle.Count > 0)
        //                foreach (MedicoHorarioDetalleBE item in varMedicoHorarioDetalle)
        //                {
        //                    varMedicoHorario.horarios.Remove(item);
        //                }
        //        }

        //        return varResultado;
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

        public List<MedicoHorarioBE> HorariosPorMedicoVirtual(string idClinica, string idEspecialidad, string tipoDocumento,
                                        string numeroDocumento, DateTime dia, string numeroDia)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                //String CMP = "CMP-019465";
                SqlParameter[] varParametros = new SqlParameter[6];//7];
                varParametros[0] = new SqlParameter("@IDClinica", SqlDbType.Int);
                if (!string.IsNullOrEmpty(idClinica))
                {
                    varParametros[0].Value = idClinica;
                }
                else
                {
                    varParametros[0].Value = null;
                }
                varParametros[1] = new SqlParameter("@IDEspecialidad", SqlDbType.Int);
                varParametros[1].Value = idEspecialidad;
                varParametros[2] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[2].Value = tipoDocumento;
                varParametros[3] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[3].Value = numeroDocumento;
                varParametros[4] = new SqlParameter("@Dia", SqlDbType.DateTime);
                varParametros[4].Value = dia;
                varParametros[5] = new SqlParameter("@NumeroDia", SqlDbType.Int);
                varParametros[5].Value = numeroDia;
                //varParametros[6] = new SqlParameter("@CMP", SqlDbType.VarChar);
                //varParametros[6].Value = CMP;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Horario_Virtual", varParametros, TipoProcesamiento.DataReader, false);

                List<MedicoHorarioBE> varResultado = new List<MedicoHorarioBE>();
                MedicoHorarioBE oMedicoHorarioBE;
                string urlBase = ConfigurationManager.AppSettings["URLImagenes2"].ToString();
                string foto;
                while (varDataReader.Read())
                {
                    //(new MedicoHorarioBE()
                    oMedicoHorarioBE = new MedicoHorarioBE()
                    {
                        cmp = varDataReader.GetString(varDataReader.GetOrdinal("CMP")),
                        nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
                        especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")),
                        idClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString(),
                        idEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDEspecialidad")).ToString(),
                        centroAtencion = varDataReader.GetString(varDataReader.GetOrdinal("NombreClinica")),
                        idMedicoFavorito = varDataReader.IsDBNull(varDataReader.GetOrdinal("IDMedicoFavorito")) ? null : varDataReader.GetInt32(varDataReader.GetOrdinal("IDMedicoFavorito")).ToString(),
                        cvVisible = varDataReader.GetString(varDataReader.GetOrdinal("CVVisible")),
                        soloLlamadas = varDataReader.GetString(varDataReader.GetOrdinal("SoloLlamadas")),
                        idSubEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDSubEspecialidad")).ToString(),
                        subEspecialidad = varDataReader.GetString(varDataReader.GetOrdinal("SubEspecialidad")),
                        tipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("TipoPaciente")),
                        abreviatura = varDataReader.GetString(varDataReader.GetOrdinal("Abreviatura")),
                        sexo = varDataReader.GetString(varDataReader.GetOrdinal("Sexo")),
                        codigoTipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("CodigoTipoPaciente")),
                        unidadEspecializacion = varDataReader.GetString(varDataReader.GetOrdinal("UnidadEspecializacion")),
                        horariosEnteros = new List<HorariosBrutoBE>(),
                        horarios = new List<MedicoHorarioDetalleBE>(),
                        campoClinico = varDataReader.GetString(varDataReader.GetOrdinal("CampoClinico")),
                        prioridad = varDataReader.GetInt32(varDataReader.GetOrdinal("Prioridad"))
                    };
                    foto = varDataReader.GetString(varDataReader.GetOrdinal("Foto"));
                    oMedicoHorarioBE.foto = !string.IsNullOrEmpty(foto) ? foto : (urlBase + "Medicos/" + (oMedicoHorarioBE.sexo.Equals("M") ? "medico-m.png" : "medico-f.png"));
                    varResultado.Add(oMedicoHorarioBE);
                }

                varDataReader.NextResult();
                string varCMP,
                        varIDClinica;
                MedicoHorarioBE varMedicoHorario;
                while (varDataReader.Read())
                {
                    varCMP = varDataReader.GetString(varDataReader.GetOrdinal("CMP")).ToString();
                    //varIDClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString();
                    try
                    {
                        //varMedicoHorario = varResultado.Where(p => p.cmp == varCMP && p.idClinica == varIDClinica).FirstOrDefault();
                        varMedicoHorario = varResultado.Where(p => p.cmp == varCMP).FirstOrDefault();
                        if (varMedicoHorario != null)
                        {

                            varMedicoHorario.horariosEnteros.Add(new HorariosBrutoBE()
                            {
                                idDetalleHorarios = varDataReader.GetInt32(varDataReader.GetOrdinal("IDDetalleHorarios")),
                                idHorarioDetalle = varDataReader.GetString(varDataReader.GetOrdinal("IDHorarioDetalle")),
                                horaInicio = varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")),
                                tiempoAtencion = varDataReader.GetInt32(varDataReader.GetOrdinal("TiempoAtencion")),
                                cantidadTurnos = varDataReader.GetInt32(varDataReader.GetOrdinal("CantidadTurnos")),
                                idClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica"))
                            });
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }

                varDataReader.NextResult();
                varDataReader.Read();
                int tiempoMinimoEspera = varDataReader.GetInt32(varDataReader.GetOrdinal("LimitePago"));

                DateTime fechaMinima = DateTime.Now.AddSeconds(tiempoMinimoEspera);
                if (dia.Date == DateTime.Today)
                {
                    //TimeSpan varHoraActual = DateTime.Now.TimeOfDay;
                    foreach (MedicoHorarioBE medico in varResultado)
                    {
                        foreach (HorariosBrutoBE horario in medico.horariosEnteros.OrderBy(p => p.horaInicio))
                        {
                            for (int i = 0; i < horario.cantidadTurnos; i++)
                            {
                                //if (horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)) >= varHoraActual)
                                if ((dia + horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0))) >= fechaMinima)
                                    medico.horarios.Add(new MedicoHorarioDetalleBE()
                                    {
                                        idDetalleHorarios = horario.idDetalleHorarios,
                                        idHorarioDetalle = horario.idHorarioDetalle.ToString(),
                                        turno = (i + 1).ToString(),
                                        horaTime = horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)),
                                        hora = horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)).ToString(@"hh\:mm")
                                    });
                            }
                        }
                    }
                }
                else
                {
                    foreach (MedicoHorarioBE medico in varResultado)
                    {
                        foreach (HorariosBrutoBE horario in medico.horariosEnteros.OrderBy(p => p.horaInicio))
                        {
                            for (int i = 0; i < horario.cantidadTurnos; i++)
                            {
                                if ((dia + horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0))) >= fechaMinima)
                                    medico.horarios.Add(new MedicoHorarioDetalleBE()
                                    {
                                        idDetalleHorarios = horario.idDetalleHorarios,
                                        idHorarioDetalle = horario.idHorarioDetalle.ToString(),
                                        turno = (i + 1).ToString(),
                                        horaTime = horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)),
                                        hora = horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)).ToString(@"hh\:mm")
                                    });
                            }
                        }
                    }
                }

                varDataReader.NextResult();
                int varIDDetalleHorarios, varTurno;
                string varHoraInicioCita;
                List<MedicoHorarioDetalleBE> varMedicoHorarioDetalle;
                while (varDataReader.Read())
                {
                    varCMP = varDataReader.GetString(varDataReader.GetOrdinal("CMP")).ToString();
                    varIDDetalleHorarios = varDataReader.GetInt32(varDataReader.GetOrdinal("IDDetalleHorarios"));
                    varTurno = varDataReader.GetInt32(varDataReader.GetOrdinal("Turno"));
                    varHoraInicioCita = varDataReader.GetString(varDataReader.GetOrdinal("HoraInicio"));
                    varMedicoHorario = varResultado.Where(p => p.cmp == varCMP).First();
                    //varMedicoHorarioDetalle = varMedicoHorario.horarios.Where(p => p.idDetalleHorarios == varIDDetalleHorarios && p.turno == varTurno.ToString()).ToList();
                    varMedicoHorarioDetalle = varMedicoHorario.horarios.Where(p => p.idDetalleHorarios == varIDDetalleHorarios && p.hora == varHoraInicioCita.ToString()).ToList();
                    if (varMedicoHorarioDetalle != null && varMedicoHorarioDetalle.Count > 0)
                        foreach (MedicoHorarioDetalleBE item in varMedicoHorarioDetalle)
                        {
                            varMedicoHorario.horarios.Remove(item);
                        }
                }

                varDataReader.NextResult();
                DateTime varFechaInicio, varFechaFin;
                while (varDataReader.Read())
                {
                    varCMP = varDataReader.GetString(varDataReader.GetOrdinal("CMP")).ToString();
                    varIDDetalleHorarios = varDataReader.GetInt32(varDataReader.GetOrdinal("IDDetalleHorarios"));
                    varFechaInicio = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaInicio"));
                    varFechaFin = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaFin"));
                    varMedicoHorario = varResultado.Where(p => p.cmp == varCMP).First();
                    varMedicoHorarioDetalle = varMedicoHorario.horarios.Where(p => p.idDetalleHorarios == varIDDetalleHorarios
                                                                            && (dia.Add(p.horaTime) >= varFechaInicio)
                                                                            && (dia.Add(p.horaTime) <= varFechaFin)).ToList();
                    if (varMedicoHorarioDetalle != null && varMedicoHorarioDetalle.Count > 0)
                        foreach (MedicoHorarioDetalleBE item in varMedicoHorarioDetalle)
                        {
                            varMedicoHorario.horarios.Remove(item);
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

        public List<MedicoHorarioBE> HorariosPorMedicoVirtualCmp(string idClinica, string idEspecialidad, string tipoDocumento,
                                        string numeroDocumento, DateTime dia, string numeroDia, string cmp)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[7];
                varParametros[0] = new SqlParameter("@IDClinica", SqlDbType.Int);
                varParametros[0].Value = idClinica;
                varParametros[1] = new SqlParameter("@IDEspecialidad", SqlDbType.Int);
                varParametros[1].Value = idEspecialidad;
                varParametros[2] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[2].Value = tipoDocumento;
                varParametros[3] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[3].Value = numeroDocumento;
                varParametros[4] = new SqlParameter("@Dia", SqlDbType.DateTime);
                varParametros[4].Value = dia;
                varParametros[5] = new SqlParameter("@NumeroDia", SqlDbType.Int);
                varParametros[5].Value = numeroDia;
                varParametros[6] = new SqlParameter("@CMP", SqlDbType.VarChar);
                varParametros[6].Value = cmp;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Horario_VirtualPorMedico", varParametros, TipoProcesamiento.DataReader, false);

                List<MedicoHorarioBE> varResultado = new List<MedicoHorarioBE>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new MedicoHorarioBE()
                    {
                        cmp = varDataReader.GetString(varDataReader.GetOrdinal("CMP")),
                        nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
                        especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")),
                        centroAtencion = varDataReader.GetString(varDataReader.GetOrdinal("NombreClinica")),
                        idMedicoFavorito = varDataReader.IsDBNull(varDataReader.GetOrdinal("IDMedicoFavorito")) ? null : varDataReader.GetInt32(varDataReader.GetOrdinal("IDMedicoFavorito")).ToString(),
                        cvVisible = varDataReader.GetString(varDataReader.GetOrdinal("CVVisible")),
                        soloLlamadas = varDataReader.GetString(varDataReader.GetOrdinal("SoloLlamadas")),
                        horariosEnteros = new List<HorariosBrutoBE>(),
                        horarios = new List<MedicoHorarioDetalleBE>()
                    });
                }

                varDataReader.NextResult();
                string varCMP;
                MedicoHorarioBE varMedicoHorario;
                while (varDataReader.Read())
                {
                    varCMP = varDataReader.GetString(varDataReader.GetOrdinal("CMP")).ToString();
                    try
                    {
                        varMedicoHorario = varResultado.Where(p => p.cmp == varCMP).FirstOrDefault();
                        if (varMedicoHorario != null)
                        {

                            varMedicoHorario.horariosEnteros.Add(new HorariosBrutoBE()
                            {
                                idDetalleHorarios = varDataReader.GetInt32(varDataReader.GetOrdinal("IDDetalleHorarios")),
                                idHorarioDetalle = varDataReader.GetString(varDataReader.GetOrdinal("IDHorarioDetalle")),
                                horaInicio = varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")),
                                tiempoAtencion = varDataReader.GetInt32(varDataReader.GetOrdinal("TiempoAtencion")),
                                cantidadTurnos = varDataReader.GetInt32(varDataReader.GetOrdinal("CantidadTurnos")),
                            });
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }

                varDataReader.NextResult();
                varDataReader.Read();
                int tiempoMinimoEspera = varDataReader.GetInt32(varDataReader.GetOrdinal("LimitePago"));

                DateTime fechaMinima = DateTime.Now.AddSeconds(tiempoMinimoEspera);
                if (dia.Date == DateTime.Today)
                {
                    //TimeSpan varHoraActual = DateTime.Now.TimeOfDay;
                    foreach (MedicoHorarioBE medico in varResultado)
                    {
                        foreach (HorariosBrutoBE horario in medico.horariosEnteros.OrderBy(p => p.horaInicio))
                        {
                            for (int i = 0; i < horario.cantidadTurnos; i++)
                            {
                                //if (horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)) >= varHoraActual)
                                if ((dia + horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0))) >= fechaMinima)
                                    medico.horarios.Add(new MedicoHorarioDetalleBE()
                                    {
                                        idDetalleHorarios = horario.idDetalleHorarios,
                                        idHorarioDetalle = horario.idHorarioDetalle.ToString(),
                                        turno = (i + 1).ToString(),
                                        horaTime = horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)),
                                        hora = horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)).ToString(@"hh\:mm")
                                    });
                            }
                        }
                    }
                }
                else
                {
                    foreach (MedicoHorarioBE medico in varResultado)
                    {
                        foreach (HorariosBrutoBE horario in medico.horariosEnteros.OrderBy(p => p.horaInicio))
                        {
                            for (int i = 0; i < horario.cantidadTurnos; i++)
                            {
                                if ((dia + horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0))) >= fechaMinima)
                                    medico.horarios.Add(new MedicoHorarioDetalleBE()
                                    {
                                        idDetalleHorarios = horario.idDetalleHorarios,
                                        idHorarioDetalle = horario.idHorarioDetalle.ToString(),
                                        turno = (i + 1).ToString(),
                                        horaTime = horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)),
                                        hora = horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)).ToString(@"hh\:mm")
                                    });
                            }
                        }
                    }
                }

                varDataReader.NextResult();
                int varIDDetalleHorarios, varTurno;
                List<MedicoHorarioDetalleBE> varMedicoHorarioDetalle;
                while (varDataReader.Read())
                {
                    varCMP = varDataReader.GetString(varDataReader.GetOrdinal("CMP")).ToString();
                    varIDDetalleHorarios = varDataReader.GetInt32(varDataReader.GetOrdinal("IDDetalleHorarios"));
                    varTurno = varDataReader.GetInt32(varDataReader.GetOrdinal("Turno"));
                    varMedicoHorario = varResultado.Where(p => p.cmp == varCMP).First();
                    varMedicoHorarioDetalle = varMedicoHorario.horarios.Where(p => p.idDetalleHorarios == varIDDetalleHorarios && p.turno == varTurno.ToString()).ToList();
                    if (varMedicoHorarioDetalle != null && varMedicoHorarioDetalle.Count > 0)
                        foreach (MedicoHorarioDetalleBE item in varMedicoHorarioDetalle)
                        {
                            varMedicoHorario.horarios.Remove(item);
                        }
                }

                varDataReader.NextResult();
                DateTime varFechaInicio, varFechaFin;
                while (varDataReader.Read())
                {
                    varCMP = varDataReader.GetString(varDataReader.GetOrdinal("CMP")).ToString();
                    varIDDetalleHorarios = varDataReader.GetInt32(varDataReader.GetOrdinal("IDDetalleHorarios"));
                    varFechaInicio = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaInicio"));
                    varFechaFin = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaFin"));
                    varMedicoHorario = varResultado.Where(p => p.cmp == varCMP).First();
                    varMedicoHorarioDetalle = varMedicoHorario.horarios.Where(p => p.idDetalleHorarios == varIDDetalleHorarios
                                                                            && (dia.Add(p.horaTime) >= varFechaInicio)
                                                                            && (dia.Add(p.horaTime) <= varFechaFin)).ToList();
                    if (varMedicoHorarioDetalle != null && varMedicoHorarioDetalle.Count > 0)
                        foreach (MedicoHorarioDetalleBE item in varMedicoHorarioDetalle)
                        {
                            varMedicoHorario.horarios.Remove(item);
                        }
                }
                varDataReader.NextResult();
                DateTime fechaFinal;
                varDataReader.Read();
                fechaFinal = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaFin"));


                varResultado[0].fechaFinal = fechaFinal;


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

        public List<MedicoHorarioSimpleWebBE> BuscarMedicoWeb(string idClinica, string idEspecialidad, string nombre)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[3];
                varParametros[0] = new SqlParameter("@IDEspecialidad", SqlDbType.Int);
                varParametros[0].Value = idEspecialidad;
                varParametros[1] = new SqlParameter("@IDClinica", SqlDbType.Int);
                varParametros[1].Value = idClinica;
                varParametros[2] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                varParametros[2].Value = nombre;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Horario_BuscarMedicoWeb", varParametros, TipoProcesamiento.DataReader, false);

                List<MedicoHorarioSimpleWebBE> varResultado = new List<MedicoHorarioSimpleWebBE>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new MedicoHorarioSimpleWebBE()
                    {
                        cmp = varDataReader.GetString(varDataReader.GetOrdinal("CMP")),
                        nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
                        idEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDEspecialidad")).ToString(),
                        especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")),
                        idCentroAtencion = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString(),
                        centroAtencion = varDataReader.GetString(varDataReader.GetOrdinal("NombreClinica")),
                        idMedicoFavorito = null,
                        cvVisible = varDataReader.GetString(varDataReader.GetOrdinal("CVVisible")),
                        soloLlamadas = varDataReader.GetString(varDataReader.GetOrdinal("SoloLlamadas")),
                        foto = varDataReader.GetString(varDataReader.GetOrdinal("Foto"))
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
        public DateTime FechaFinalMedico(string idClinica, string idEspecialidad, string cmp)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[3];
                varParametros[0] = new SqlParameter("@IDEspecialidad", SqlDbType.Int);
                varParametros[0].Value = idEspecialidad;
                varParametros[1] = new SqlParameter("@IDClinica", SqlDbType.Int);
                varParametros[1].Value = idClinica;
                varParametros[2] = new SqlParameter("@CMP", SqlDbType.VarChar);
                varParametros[2].Value = cmp;

                DateTime fecha;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Horario_FechaFinal", varParametros, TipoProcesamiento.DataReader, false);
                if (varDataReader.HasRows)
                {
                    varDataReader.Read();

                    fecha = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaFin"));
                }
                else
                {
                    fecha = DateTime.Now;
                }




                return fecha;
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

        public List<MedicoHorarioSimpleWebBE> StaffMedicos(string idClinica, string idEspecialidad, string nombre)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[3];
                varParametros[0] = new SqlParameter("@IDEspecialidad", SqlDbType.Int);
                varParametros[0].Value = idEspecialidad;
                varParametros[1] = new SqlParameter("@IDClinica", SqlDbType.Int);
                varParametros[1].Value = idClinica;
                varParametros[2] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                varParametros[2].Value = nombre;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Horario_MedicoStaff", varParametros, TipoProcesamiento.DataReader, false);

                List<MedicoHorarioSimpleWebBE> varResultado = new List<MedicoHorarioSimpleWebBE>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new MedicoHorarioSimpleWebBE()
                    {
                        cmp = varDataReader.GetString(varDataReader.GetOrdinal("CMP")),
                        nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
                        idEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDEspecialidad")).ToString(),
                        especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")),
                        idCentroAtencion = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString(),
                        centroAtencion = varDataReader.GetString(varDataReader.GetOrdinal("NombreClinica")),
                        idMedicoFavorito = null,
                        cvVisible = varDataReader.GetString(varDataReader.GetOrdinal("CVVisible")),
                        soloLlamadas = varDataReader.GetString(varDataReader.GetOrdinal("SoloLlamadas")),
                        foto = varDataReader.GetString(varDataReader.GetOrdinal("Foto"))
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

        public string GuardarMedicoDomicilio(string especialidad, string tipoDocumento, string numeroDocumento,
                                                string nombre, string celular, string direccion, string latitud,
                                                string longitud)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[8];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@Especialidad", SqlDbType.VarChar);
                varParametros[2].Value = especialidad;
                varParametros[3] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                varParametros[3].Value = nombre;
                varParametros[4] = new SqlParameter("@Celular", SqlDbType.VarChar);
                varParametros[4].Value = celular;
                varParametros[5] = new SqlParameter("@Direccion", SqlDbType.VarChar);
                varParametros[5].Value = direccion;
                varParametros[6] = new SqlParameter("@Latitud", SqlDbType.VarChar);
                varParametros[6].Value = latitud;
                varParametros[7] = new SqlParameter("@Longitud", SqlDbType.VarChar);
                varParametros[7].Value = longitud;

                object varRespuesta = varConexion.EjecutarProcedimiento("App_Proc_Medico_RegistrarMedicoDomicilio", varParametros, TipoProcesamiento.Scalar);
                return (varRespuesta == null) ? "Sin seguro registrado" : varRespuesta.ToString();
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

        public string TurnosPorRangoHorario(DateTime fechaInicio, DateTime fechaFin)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {

                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@FechaInicio", SqlDbType.DateTime);
                varParametros[0].Value = fechaInicio;
                varParametros[1] = new SqlParameter("@FechaFin", SqlDbType.DateTime);
                varParametros[1].Value = fechaFin;

                return varConexion.EjecutarProcedimiento("App_Proc_Horario_MaximoTurnos", varParametros, TipoProcesamiento.Scalar).ToString();
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
        public string TurnosPorRangoHorarioV2(DateTime fechaInicio, DateTime fechaFin)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {

                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@FechaInicio", SqlDbType.DateTime);
                varParametros[0].Value = fechaInicio;
                varParametros[1] = new SqlParameter("@FechaFin", SqlDbType.DateTime);
                varParametros[1].Value = fechaFin;

                return varConexion.EjecutarProcedimiento("App_Proc_Horario_MaximoTurnos_V2", varParametros, TipoProcesamiento.Scalar).ToString();
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
        public List<MedicoHorarioBE> ListarMedicoHorarioPorEspecialidadEC(string idEspecialidad, string tipoDocumento,
                                        string numeroDocumento, DateTime dia, string numeroDia)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[5];
                varParametros[0] = new SqlParameter("@IDEspecialidad", SqlDbType.Int);
                varParametros[0].Value = idEspecialidad;
                varParametros[1] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[1].Value = tipoDocumento;
                varParametros[2] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[2].Value = numeroDocumento;
                varParametros[3] = new SqlParameter("@Dia", SqlDbType.DateTime);
                varParametros[3].Value = dia;
                varParametros[4] = new SqlParameter("@NumeroDia", SqlDbType.Int);
                varParametros[4].Value = numeroDia;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Horario_VirtualEC", varParametros, TipoProcesamiento.DataReader, false);

                List<MedicoHorarioBE> varResultado = new List<MedicoHorarioBE>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new MedicoHorarioBE()
                    {
                        cmp = varDataReader.GetString(varDataReader.GetOrdinal("CMP")),
                        nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
                        especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")),
                        centroAtencion = varDataReader.GetString(varDataReader.GetOrdinal("NombreClinica")),
                        idMedicoFavorito = varDataReader.IsDBNull(varDataReader.GetOrdinal("IDMedicoFavorito")) ? null : varDataReader.GetInt32(varDataReader.GetOrdinal("IDMedicoFavorito")).ToString(),
                        cvVisible = varDataReader.GetString(varDataReader.GetOrdinal("CVVisible")),
                        soloLlamadas = varDataReader.GetString(varDataReader.GetOrdinal("SoloLlamadas")),
                        horariosEnteros = new List<HorariosBrutoBE>(),
                        horarios = new List<MedicoHorarioDetalleBE>()
                    });
                }

                varDataReader.NextResult();
                string varCMP;
                MedicoHorarioBE varMedicoHorario;
                while (varDataReader.Read())
                {
                    varCMP = varDataReader.GetString(varDataReader.GetOrdinal("CMP")).ToString();
                    try
                    {
                        varMedicoHorario = varResultado.Where(p => p.cmp == varCMP).FirstOrDefault();
                        if (varMedicoHorario != null)
                        {

                            varMedicoHorario.horariosEnteros.Add(new HorariosBrutoBE()
                            {
                                idDetalleHorarios = varDataReader.GetInt32(varDataReader.GetOrdinal("IDDetalleHorarios")),
                                idHorarioDetalle = varDataReader.GetString(varDataReader.GetOrdinal("IDHorarioDetalle")),
                                horaInicio = varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")),
                                tiempoAtencion = varDataReader.GetInt32(varDataReader.GetOrdinal("TiempoAtencion")),
                                cantidadTurnos = varDataReader.GetInt32(varDataReader.GetOrdinal("CantidadTurnos")),
                                idClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica"))
                            });
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }

                varDataReader.NextResult();
                //varDataReader.Read();
                List<HorarioTiempoPreCitaBE> varHorarioTiempoPreCita = new List<HorarioTiempoPreCitaBE>();
                while (varDataReader.Read())
                {
                    varHorarioTiempoPreCita.Add(new HorarioTiempoPreCitaBE()
                    {
                        tiempoMinimoEspera = varDataReader.GetInt32(varDataReader.GetOrdinal("LimitePago")),
                        idClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica"))
                    });
                }
                //int tiempoMinimoEspera = varDataReader.GetInt32(varDataReader.GetOrdinal("LimitePago"));
                HorarioTiempoPreCitaBE varTiempoPreCita;
                DateTime fechaMinima;// = DateTime.Now.AddSeconds(tiempoMinimoEspera);
                if (dia.Date == DateTime.Today)
                {
                    //TimeSpan varHoraActual = DateTime.Now.TimeOfDay;
                    foreach (MedicoHorarioBE medico in varResultado)
                    {
                        foreach (HorariosBrutoBE horario in medico.horariosEnteros.OrderBy(p => p.horaInicio))
                        {
                            varTiempoPreCita = varHorarioTiempoPreCita.Where(p => p.idClinica == horario.idClinica).FirstOrDefault();
                            fechaMinima = DateTime.Now.AddSeconds(varTiempoPreCita.tiempoMinimoEspera);
                            for (int i = 0; i < horario.cantidadTurnos; i++)
                            {
                                //if (horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)) >= varHoraActual)
                                if ((dia + horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0))) >= fechaMinima)
                                    medico.horarios.Add(new MedicoHorarioDetalleBE()
                                    {
                                        idDetalleHorarios = horario.idDetalleHorarios,
                                        idHorarioDetalle = horario.idHorarioDetalle.ToString(),
                                        turno = (i + 1).ToString(),
                                        horaTime = horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)),
                                        hora = horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)).ToString(@"hh\:mm"),
                                        idClinica = varTiempoPreCita.idClinica
                                    });
                            }
                        }
                    }
                }
                else
                {
                    foreach (MedicoHorarioBE medico in varResultado)
                    {
                        foreach (HorariosBrutoBE horario in medico.horariosEnteros.OrderBy(p => p.horaInicio))
                        {
                            varTiempoPreCita = varHorarioTiempoPreCita.Where(p => p.idClinica == horario.idClinica).FirstOrDefault();
                            fechaMinima = DateTime.Now.AddSeconds(varTiempoPreCita.tiempoMinimoEspera);
                            for (int i = 0; i < horario.cantidadTurnos; i++)
                            {
                                if ((dia + horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0))) >= fechaMinima)
                                    medico.horarios.Add(new MedicoHorarioDetalleBE()
                                    {
                                        idDetalleHorarios = horario.idDetalleHorarios,
                                        idHorarioDetalle = horario.idHorarioDetalle.ToString(),
                                        turno = (i + 1).ToString(),
                                        horaTime = horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)),
                                        hora = horario.horaInicio.Add(new TimeSpan(0, horario.tiempoAtencion * i, 0)).ToString(@"hh\:mm"),
                                        idClinica = varTiempoPreCita.idClinica
                                    });
                            }
                        }
                    }
                }

                varDataReader.NextResult();
                int varIDDetalleHorarios, varTurno;
                List<MedicoHorarioDetalleBE> varMedicoHorarioDetalle;
                while (varDataReader.Read())
                {
                    varCMP = varDataReader.GetString(varDataReader.GetOrdinal("CMP")).ToString();
                    varIDDetalleHorarios = varDataReader.GetInt32(varDataReader.GetOrdinal("IDDetalleHorarios"));
                    varTurno = varDataReader.GetInt32(varDataReader.GetOrdinal("Turno"));
                    varMedicoHorario = varResultado.Where(p => p.cmp == varCMP).First();
                    varMedicoHorarioDetalle = varMedicoHorario.horarios.Where(p => p.idDetalleHorarios == varIDDetalleHorarios && p.turno == varTurno.ToString()).ToList();
                    if (varMedicoHorarioDetalle != null && varMedicoHorarioDetalle.Count > 0)
                        foreach (MedicoHorarioDetalleBE item in varMedicoHorarioDetalle)
                        {
                            varMedicoHorario.horarios.Remove(item);
                        }
                }

                varDataReader.NextResult();
                DateTime varFechaInicio, varFechaFin;
                while (varDataReader.Read())
                {
                    varCMP = varDataReader.GetString(varDataReader.GetOrdinal("CMP")).ToString();
                    varIDDetalleHorarios = varDataReader.GetInt32(varDataReader.GetOrdinal("IDDetalleHorarios"));
                    varFechaInicio = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaInicio"));
                    varFechaFin = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaFin"));
                    varMedicoHorario = varResultado.Where(p => p.cmp == varCMP).First();
                    varMedicoHorarioDetalle = varMedicoHorario.horarios.Where(p => p.idDetalleHorarios == varIDDetalleHorarios
                                                                            && (dia.Add(p.horaTime) >= varFechaInicio)
                                                                            && (dia.Add(p.horaTime) <= varFechaFin)).ToList();
                    if (varMedicoHorarioDetalle != null && varMedicoHorarioDetalle.Count > 0)
                        foreach (MedicoHorarioDetalleBE item in varMedicoHorarioDetalle)
                        {
                            varMedicoHorario.horarios.Remove(item);
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

        public List<MedicoHorarioProximaFechaBE> FechasProximasPorMedico(string idClinica, string idEspecialidad, string cmp,
                                                                string ejecucion, string registrosInicial, string registrosFinal, string tipoCita)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[7];
                varParametros[0] = new SqlParameter("@IDEspecialidad", SqlDbType.Int);
                varParametros[0].Value = idEspecialidad;
                varParametros[1] = new SqlParameter("@IDClinica", SqlDbType.Int);
                varParametros[1].Value = idClinica;
                varParametros[2] = new SqlParameter("@CMP", SqlDbType.VarChar);
                varParametros[2].Value = cmp;
                varParametros[3] = new SqlParameter("@Ejecucion", SqlDbType.VarChar);
                varParametros[3].Value = ejecucion;
                varParametros[4] = new SqlParameter("@RegistrosInicial", SqlDbType.Int);
                varParametros[4].Value = registrosInicial;
                varParametros[5] = new SqlParameter("@RegistrosFinal", SqlDbType.Int);
                varParametros[5].Value = registrosFinal;
                varParametros[6] = new SqlParameter("@Tipo", SqlDbType.VarChar);
                varParametros[6].Value = tipoCita;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Horario_MedicoFechasProximas", varParametros, TipoProcesamiento.DataReader, false);

                List<MedicoHorarioProximaFechaBE> varResultado = new List<MedicoHorarioProximaFechaBE>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new MedicoHorarioProximaFechaBE()
                    {
                        fechaDate = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")),
                        fecha = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")).ToString("dd/MM/yyyy"),
                        dia = varDataReader.GetInt32(varDataReader.GetOrdinal("Dia")),
                        horarios = new List<MedicoHorarioProximaHorarioBE>()
                    }); ;
                }

                varDataReader.NextResult();
                DateTime dtFecha;
                String varFecha;
                MedicoHorarioProximaFechaBE obeFecha;
                while (varDataReader.Read())
                {
                    dtFecha = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha"));
                    varFecha = dtFecha.ToString("dd/MM/yyyy");
                    try
                    {
                        obeFecha = varResultado.Where(p => p.fecha == varFecha).FirstOrDefault();
                        if (obeFecha != null)
                        {
                            obeFecha.horarios.Add(new MedicoHorarioProximaHorarioBE()
                            {
                                fechaDate = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")),
                                fecha = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")).ToString("dd/MM/yyyy"),
                                idHorario = varDataReader.GetInt32(varDataReader.GetOrdinal("IDHorario")),
                                turnos = new List<MedicoHorarioProximaTurnoBE>()
                            });
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

                varDataReader.NextResult();
                int iHorario, nHorarios, idHorario;
                while (varDataReader.Read())
                {
                    dtFecha = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha"));
                    varFecha = dtFecha.ToString("dd/MM/yyyy");
                    try
                    {
                        obeFecha = varResultado.Where(p => p.fecha == varFecha).FirstOrDefault();
                        if (obeFecha != null)
                        {
                            idHorario = varDataReader.GetInt32(varDataReader.GetOrdinal("IDHorario"));
                            nHorarios = obeFecha.horarios.Count;
                            for (iHorario = 0; iHorario < nHorarios; iHorario++)
                            {
                                if (obeFecha.horarios[iHorario].idHorario == idHorario)
                                {
                                    obeFecha.horarios[iHorario].turnos.Add(new MedicoHorarioProximaTurnoBE()
                                    {
                                        fechaDate = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")),
                                        fecha = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")).ToString("dd/MM/yyyy"),
                                        idHorario = varDataReader.GetInt32(varDataReader.GetOrdinal("IDHorario")),
                                        horaInicio = varDataReader.GetString(varDataReader.GetOrdinal("HoraInicio")),
                                        numeroTurno = varDataReader.GetInt32(varDataReader.GetOrdinal("NumeroTurno"))
                                    });
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
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
        //public List<CMPMedicoHorarioBE> FechasProximasPorMedico(string idClinica, string idEspecialidad, string cmp, 
        //                                                        string ejecucion, string registrosInicial, string registrosFinal, string tipoCita)
        //{
        //    ConexionUtil varConexion = new ConexionUtil();
        //    try
        //    {
        //        SqlParameter[] varParametros = new SqlParameter[7];
        //        varParametros[0] = new SqlParameter("@IDEspecialidad", SqlDbType.Int);
        //        varParametros[0].Value = idEspecialidad;
        //        varParametros[1] = new SqlParameter("@IDClinica", SqlDbType.Int);
        //        varParametros[1].Value = idClinica;
        //        varParametros[2] = new SqlParameter("@CMP", SqlDbType.VarChar);
        //        varParametros[2].Value = !String.IsNullOrEmpty(cmp) ? cmp : null;
        //        varParametros[3] = new SqlParameter("@Ejecucion", SqlDbType.VarChar);
        //        varParametros[3].Value = ejecucion;
        //        varParametros[4] = new SqlParameter("@RegistrosInicial", SqlDbType.Int);
        //        varParametros[4].Value = registrosInicial;
        //        varParametros[5] = new SqlParameter("@RegistrosFinal", SqlDbType.Int);
        //        varParametros[5].Value = registrosFinal;
        //        varParametros[6] = new SqlParameter("@Tipo", SqlDbType.VarChar);
        //        varParametros[6].Value = tipoCita;

        //        varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Horario_MedicoFechasProximas", varParametros, TipoProcesamiento.DataReader, false);

        //        List<CMPMedicoHorarioBE> varCMPMedico = new List<CMPMedicoHorarioBE>();
        //        while (varDataReader.Read())
        //        {
        //            varCMPMedico.Add(new CMPMedicoHorarioBE()
        //            {
        //                CMP = varDataReader.GetString(varDataReader.GetOrdinal("CMP")),
        //                apellidoPaterno = varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaterno")),
        //                apellidoMaterno = varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaterno")),
        //                nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
        //                fechas = new List<MedicoHorarioProximaFechaBE>()
        //            });
        //        }

        //        varDataReader.NextResult();
        //        List<MedicoHorarioProximaFechaBE> varResultado = new List<MedicoHorarioProximaFechaBE>();
        //        string CMP;
        //        CMPMedicoHorarioBE oCMPMedico;
        //        while (varDataReader.Read())
        //        {
        //            CMP = varDataReader.GetString(varDataReader.GetOrdinal("CMP"));
        //            oCMPMedico = varCMPMedico.Where(p => p.CMP == CMP).FirstOrDefault(); 
        //            if (oCMPMedico != null)
        //            {
        //                oCMPMedico.fechas.Add(new MedicoHorarioProximaFechaBE()
        //                {
        //                    fechaDate = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")),
        //                    fecha = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")).ToString("dd/MM/yyyy"),
        //                    dia = varDataReader.GetInt32(varDataReader.GetOrdinal("Dia")),
        //                    CMP = CMP,
        //                    horarios = new List<MedicoHorarioProximaHorarioBE>()
        //                });
        //            }
        //        }

        //        varDataReader.NextResult();
        //        DateTime dtFecha;
        //        String varFecha;
        //        MedicoHorarioProximaFechaBE obeFecha;
        //        while (varDataReader.Read())
        //        {
        //            dtFecha = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha"));
        //            varFecha = dtFecha.ToString("dd/MM/yyyy");
        //            CMP = varDataReader.GetString(varDataReader.GetOrdinal("CMP"));
        //            try
        //            {
        //                oCMPMedico = varCMPMedico.Where(p => p.CMP == CMP).FirstOrDefault();
        //                if (oCMPMedico != null)
        //                {
        //                    //obeFecha = varResultado.Where(p => p.fecha == varFecha && p.CMP == CMP).FirstOrDefault();
        //                    obeFecha = oCMPMedico.fechas.Where(p => p.fecha == varFecha).FirstOrDefault();
        //                    if (obeFecha != null)
        //                    {
        //                        obeFecha.horarios.Add(new MedicoHorarioProximaHorarioBE()
        //                        {
        //                            fechaDate = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")),
        //                            fecha = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")).ToString("dd/MM/yyyy"),
        //                            idHorario = varDataReader.GetInt32(varDataReader.GetOrdinal("IDHorario")),
        //                            CMP = CMP,
        //                            turnos = new List<MedicoHorarioProximaTurnoBE>()
        //                        });
        //                    }
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                throw;
        //            }
        //        }

        //        varDataReader.NextResult();
        //        int iHorario, nHorarios, idHorario;
        //        while (varDataReader.Read())
        //        {
        //            dtFecha = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha"));
        //            varFecha = dtFecha.ToString("dd/MM/yyyy"); 
        //            CMP = varDataReader.GetString(varDataReader.GetOrdinal("CMP"));
        //            try
        //            {
        //                oCMPMedico = varCMPMedico.Where(p => p.CMP == CMP).FirstOrDefault();
        //                if (oCMPMedico != null)
        //                {
        //                    obeFecha = oCMPMedico.fechas.Where(p => p.fecha == varFecha).FirstOrDefault();
        //                    if (obeFecha != null)
        //                    {
        //                        idHorario = varDataReader.GetInt32(varDataReader.GetOrdinal("IDHorario"));
        //                        nHorarios = obeFecha.horarios.Count;
        //                        for (iHorario = 0; iHorario < nHorarios; iHorario++)
        //                        {
        //                            if (obeFecha.horarios[iHorario].idHorario == idHorario)
        //                            {
        //                                obeFecha.horarios[iHorario].turnos.Add(new MedicoHorarioProximaTurnoBE()
        //                                {
        //                                    fechaDate = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")),
        //                                    fecha = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")).ToString("dd/MM/yyyy"),
        //                                    idHorario = varDataReader.GetInt32(varDataReader.GetOrdinal("IDHorario")),
        //                                    horaInicio = varDataReader.GetString(varDataReader.GetOrdinal("HoraInicio")),
        //                                    numeroTurno = varDataReader.GetInt32(varDataReader.GetOrdinal("NumeroTurno"))
        //                                });
        //                                break;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                throw;
        //            }
        //        }

        //        return varCMPMedico;
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
        public List<MedicoHorarioSimpleWebBE> HorariosPorNombreMedicoVirtual(string idClinica, string idEspecialidad, string nombre,
                                        string tipoDocumento, string numeroDocumento, bool soloMedicosFavoritos = false)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[6];
                varParametros[0] = new SqlParameter("@IDEspecialidad", SqlDbType.Int);
                if (!string.IsNullOrEmpty(idEspecialidad))
                {
                    varParametros[0].Value = idEspecialidad;
                }
                else
                {
                    varParametros[0].Value = null;
                }
                varParametros[1] = new SqlParameter("@IDClinica", SqlDbType.Int);
                if (!string.IsNullOrEmpty(idClinica))
                {
                    varParametros[1].Value = idClinica;
                }
                else
                {
                    varParametros[1].Value = null;
                }
                varParametros[2] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                varParametros[2].Value = nombre;
                varParametros[3] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[3].Value = tipoDocumento;
                varParametros[4] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[4].Value = numeroDocumento;
                varParametros[5] = new SqlParameter("@SoloMedicosFavoritos", SqlDbType.Bit);
                varParametros[5].Value = soloMedicosFavoritos;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Horario_PorNombreMedicoVirtual", varParametros, TipoProcesamiento.DataReader, false);

                List<MedicoHorarioSimpleWebBE> varResultado = new List<MedicoHorarioSimpleWebBE>();
                MedicoHorarioSimpleWebBE oMedicoHorarioSimpleWebBE;
                string urlBase = ConfigurationManager.AppSettings["URLImagenes2"].ToString();
                string foto;
                while (varDataReader.Read())
                {
                    oMedicoHorarioSimpleWebBE = new MedicoHorarioSimpleWebBE()
                    {
                        cmp = varDataReader.GetString(varDataReader.GetOrdinal("CMP")),
                        nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
                        idEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDEspecialidad")).ToString(),
                        especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")),
                        //idCentroAtencion = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString(),
                        idCentroAtencion = varDataReader.GetString(varDataReader.GetOrdinal("IDClinica")).ToString(),
                        centroAtencion = varDataReader.GetString(varDataReader.GetOrdinal("NombreClinica")),
                        idMedicoFavorito = varDataReader.IsDBNull(varDataReader.GetOrdinal("IDMedicoFavorito")) ? null : varDataReader.GetInt32(varDataReader.GetOrdinal("IDMedicoFavorito")).ToString(),
                        cvVisible = varDataReader.GetString(varDataReader.GetOrdinal("CVVisible")),
                        soloLlamadas = varDataReader.GetString(varDataReader.GetOrdinal("SoloLlamadas")),
                        //foto = varDataReader.GetString(varDataReader.GetOrdinal("Foto"))
                        idSubEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDSubEspecialidad")).ToString(),
                        subEspecialidad = varDataReader.GetString(varDataReader.GetOrdinal("SubEspecialidad")),
                        tipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("TipoPaciente")),
                        abreviatura = varDataReader.GetString(varDataReader.GetOrdinal("Abreviatura")),
                        sexo = varDataReader.GetString(varDataReader.GetOrdinal("Sexo")),
                        codigoTipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("CodigoTipoPaciente")),
                        unidadEspecializacion = varDataReader.GetString(varDataReader.GetOrdinal("UnidadEspecializacion")),
                        campoClinico = varDataReader.GetString(varDataReader.GetOrdinal("CampoClinico")),
                        mensajePersonalizado = varDataReader.GetString(varDataReader.GetOrdinal("MensajePersonalizado")),
                        telefonoSecretaria = varDataReader.GetString(varDataReader.GetOrdinal("TelefonoSecretaria")),
                        prioridad = varDataReader.GetInt32(varDataReader.GetOrdinal("Prioridad"))
                    };
                    foto = varDataReader.GetString(varDataReader.GetOrdinal("Foto"));
                    oMedicoHorarioSimpleWebBE.foto = !string.IsNullOrEmpty(foto) ? foto : (urlBase + "Medicos/" + (oMedicoHorarioSimpleWebBE.sexo.Equals("M") ? "medico-m.png" : "medico-f.png"));
                    varResultado.Add(oMedicoHorarioSimpleWebBE);
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

        public InfoMedicoBE ObtenerInformacionMedico(string cmp, string idEspecialidad)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@CMP", SqlDbType.VarChar);
                varParametros[0].Value = cmp;
                varParametros[1] = new SqlParameter("@IDEspecialidad", SqlDbType.Int);
                varParametros[1].Value = idEspecialidad;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Medico_ObtenerInformacionV2", varParametros, TipoProcesamiento.DataReader, false);

                InfoMedicoBE varResultado = null;
                string urlBase = ConfigurationManager.AppSettings["URLImagenes2"].ToString();
                string foto;
                if (varDataReader.Read())
                {
                    varResultado = new InfoMedicoBE()
                    {
                        abreviatura = varDataReader.GetString(varDataReader.GetOrdinal("Abreviatura")),
                        nombres = varDataReader.GetString(varDataReader.GetOrdinal("Medico")),
                        RNE = varDataReader.GetString(varDataReader.GetOrdinal("RNE")),
                        idEspecialidad = varDataReader.GetString(varDataReader.GetOrdinal("IDEspecialidad")),
                        especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")),
                        idSubEspecialidad = varDataReader.GetString(varDataReader.GetOrdinal("IDSubEspecialidad")),
                        subEspecialidad = varDataReader.GetString(varDataReader.GetOrdinal("SubEspecialidad")),
                        //idClinica = varDataReader.GetString(varDataReader.GetOrdinal("IDClinica")),
                        //clinica = varDataReader.GetString(varDataReader.GetOrdinal("Clinica")),
                        codigoTipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("CodigoTipoPaciente")),
                        tipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("TipoPaciente")),
                        soloLlamadas = varDataReader.GetString(varDataReader.GetOrdinal("SoloLlamadas")),
                        areasInteres = varDataReader.GetString(varDataReader.GetOrdinal("AreasInteres")),
                        unidadEspecializacion = varDataReader.GetString(varDataReader.GetOrdinal("UnidadEspecializacion")),

                        //foto = varDataReader.GetString(varDataReader.GetOrdinal("Foto")),

                        idiomas = varDataReader.GetString(varDataReader.GetOrdinal("Idiomas")),
                        informacionAdicional = varDataReader.GetString(varDataReader.GetOrdinal("InformacionAdicional")),
                        tituloMedico = varDataReader.GetString(varDataReader.GetOrdinal("TituloMedico")),
                        premiosHonores = varDataReader.GetString(varDataReader.GetOrdinal("Premios")),
                        pertenenciaSociedad = varDataReader.GetString(varDataReader.GetOrdinal("PertenenciaSociedad")),
                        investigacionPublicaciones = varDataReader.GetString(varDataReader.GetOrdinal("Investigaciones")),
                        cargo = varDataReader.GetString(varDataReader.GetOrdinal("Cargo")),
                        sexo = varDataReader.GetString(varDataReader.GetOrdinal("Sexo")),

                        mensajePersonalizado = varDataReader.GetString(varDataReader.GetOrdinal("MensajePersonalizado")),
                        telefonoSecretaria = varDataReader.GetString(varDataReader.GetOrdinal("TelefonoSecretaria"))
                    };
                    foto = varDataReader.GetString(varDataReader.GetOrdinal("Foto"));
                    varResultado.foto = !string.IsNullOrEmpty(foto) ? foto : (urlBase + "Medicos/" + (varResultado.sexo.Equals("M") ? "medico-m.png" : "medico-f.png"));
                }

                //clínicas
                varDataReader.NextResult();
                while (varDataReader.Read())
                {
                    varResultado.clinicas.Add(new ClinicaSimpleBE()
                    {
                        idClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString(),
                        nombre = varDataReader.GetString(varDataReader.GetOrdinal("Nombre")),
                        indicadorSedeNueva = varDataReader.GetBoolean(varDataReader.GetOrdinal("IndicadorSedeNueva")),
                        indicadorVirtual = varDataReader.GetBoolean(varDataReader.GetOrdinal("EsClinicaVirtual"))
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

        public List<MedicoHorarioDisponibleBEV2> FechasDisponiblesPorMedicoVirtual(string idClinica, string idEspecialidad, string cmp)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[3];
                varParametros[0] = new SqlParameter("@IDEspecialidad", SqlDbType.Int);
                varParametros[0].Value = idEspecialidad;
                varParametros[1] = new SqlParameter("@IDClinica", SqlDbType.VarChar);//SqlDbType.Int);
                if (!String.IsNullOrEmpty(idClinica)) varParametros[1].Value = idClinica;
                else varParametros[1].Value = null;
                varParametros[2] = new SqlParameter("@CMP", SqlDbType.VarChar);
                varParametros[2].Value = cmp;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_HorarioVirtual_MedicoFechas", varParametros, TipoProcesamiento.DataReader, false);

                List<MedicoHorarioDisponibleBEV2> varResultado = new List<MedicoHorarioDisponibleBEV2>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new MedicoHorarioDisponibleBEV2()
                    {
                        fechaDate = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")),
                        fecha = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")).ToString("dd/MM/yyyy")
                    });
                }
                varDataReader.NextResult();
                MHDHorarioBE oMHDHorarioBE = null;
                int i, nFechas = varResultado.Count;
                while (varDataReader.Read())
                {
                    oMHDHorarioBE = new MHDHorarioBE()
                    {
                        fechaDate = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")),
                        fecha = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")).ToString("dd/MM/yyyy"),
                        idHorarioDetalle = varDataReader.GetInt32(varDataReader.GetOrdinal("IDHorario")).ToString() + "|" + varDataReader.GetInt32(varDataReader.GetOrdinal("Dia")).ToString(),
                        numeroTurno = varDataReader.GetInt32(varDataReader.GetOrdinal("NumeroTurno")).ToString(),
                        horaInicio = varDataReader.GetTimeSpan(varDataReader.GetOrdinal("HoraInicio")).ToString(@"hh\:mm"),
                        disponible = varDataReader.IsDBNull(varDataReader.GetOrdinal("IDCita")) ? "1" : "0"
                    };
                    for (i = 0; i < nFechas; i++)
                    {
                        if (varResultado[i].fecha.Equals(oMHDHorarioBE.fecha))
                        {
                            varResultado[i].horarios.Add(oMHDHorarioBE);
                            break;
                        }
                    }
                }
                for (i = 0; i < nFechas; i++)
                {
                    varResultado[i].horarios = varResultado[i].horarios.OrderBy(p => p.horaInicio).ToList();
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

        public List<DiasHorarioDisponibleBE> FechasDisponiblesVirtual(string idClinica, string idEspecialidad)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@IDEspecialidad", SqlDbType.Int);
                varParametros[0].Value = idEspecialidad;
                varParametros[1] = new SqlParameter("@IDClinica", SqlDbType.Int);
                if (!string.IsNullOrEmpty(idClinica))
                {
                    varParametros[1].Value = idClinica;
                }
                else
                {
                    varParametros[1].Value = null;
                }

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_HorarioVirtual_DiasDisponibles", varParametros, TipoProcesamiento.DataReader, false);

                List<DiasHorarioDisponibleBE> varResultado = new List<DiasHorarioDisponibleBE>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new DiasHorarioDisponibleBE()
                    {
                        fechaDate = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")),
                        fecha = varDataReader.GetDateTime(varDataReader.GetOrdinal("Fecha")).ToString("dd/MM/yyyy"),
                        turnosDisponibles = varDataReader.GetInt32(varDataReader.GetOrdinal("TurnosLibres")).ToString()
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

        public List<DirectorioMedicoBE> DirectorioMedico(string idClinica, string idEspecialidad, string nombre,
                                        string tipoDocumento, string numeroDocumento, bool soloMedicosFavoritos = false)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[6];
                varParametros[0] = new SqlParameter("@IDEspecialidad", SqlDbType.Int);
                if (!string.IsNullOrEmpty(idEspecialidad))
                {
                    varParametros[0].Value = idEspecialidad;
                }
                else
                {
                    varParametros[0].Value = null;
                }
                varParametros[1] = new SqlParameter("@IDClinica", SqlDbType.Int);
                if (!string.IsNullOrEmpty(idClinica))
                {
                    varParametros[1].Value = idClinica;
                }
                else
                {
                    varParametros[1].Value = null;
                }
                varParametros[2] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                varParametros[2].Value = nombre;
                varParametros[3] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[3].Value = tipoDocumento;
                varParametros[4] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[4].Value = numeroDocumento;
                varParametros[5] = new SqlParameter("@SoloMedicosFavoritos", SqlDbType.Bit);
                varParametros[5].Value = soloMedicosFavoritos;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Horario_DirectorioMedico", varParametros, TipoProcesamiento.DataReader, false);

                List<DirectorioMedicoBE> varResultado = new List<DirectorioMedicoBE>();
                DirectorioMedicoBE oDirectorioMedicoBE;
                string urlBase = ConfigurationManager.AppSettings["URLImagenes2"].ToString();
                string foto;
                while (varDataReader.Read())
                {
                    oDirectorioMedicoBE = new DirectorioMedicoBE()
                    {
                        cmp = varDataReader.GetString(varDataReader.GetOrdinal("CMP")),
                        nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
                        idEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDEspecialidad")).ToString(),
                        especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")),
                        idMedicoFavorito = varDataReader.IsDBNull(varDataReader.GetOrdinal("IDMedicoFavorito")) ? null : varDataReader.GetInt32(varDataReader.GetOrdinal("IDMedicoFavorito")).ToString(),
                        cvVisible = varDataReader.GetString(varDataReader.GetOrdinal("CVVisible")),
                        soloLlamadas = varDataReader.GetString(varDataReader.GetOrdinal("SoloLlamadas")),
                        //foto = varDataReader.GetString(varDataReader.GetOrdinal("Foto")),
                        idSubEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDSubEspecialidad")).ToString(),
                        subEspecialidad = varDataReader.GetString(varDataReader.GetOrdinal("SubEspecialidad")),
                        tipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("TipoPaciente")),
                        abreviatura = varDataReader.GetString(varDataReader.GetOrdinal("Abreviatura")),
                        sexo = varDataReader.GetString(varDataReader.GetOrdinal("Sexo")),
                        codigoTipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("CodigoTipoPaciente")),
                        unidadEspecializacion = varDataReader.GetString(varDataReader.GetOrdinal("UnidadEspecializacion")),
                        campoClinico = varDataReader.GetString(varDataReader.GetOrdinal("CampoClinico")),
                        mensajePersonalizado = varDataReader.GetString(varDataReader.GetOrdinal("MensajePersonalizado")),
                        telefonoSecretaria = varDataReader.GetString(varDataReader.GetOrdinal("TelefonoSecretaria")),
                        prioridad = varDataReader.GetInt32(varDataReader.GetOrdinal("Prioridad"))
                    };
                    foto = varDataReader.GetString(varDataReader.GetOrdinal("Foto"));
                    oDirectorioMedicoBE.foto = !string.IsNullOrEmpty(foto) ? foto : (urlBase + "Medicos/" + (oDirectorioMedicoBE.sexo.Equals("M") ? "medico-m.png" : "medico-f.png"));
                    varResultado.Add(oDirectorioMedicoBE);
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

        public List<DirectorioMedicoBE> DirectorioMedicoVirtual(string idClinica, string idEspecialidad, string nombre,
                                        string tipoDocumento, string numeroDocumento, bool soloMedicosFavoritos = false)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[6];
                varParametros[0] = new SqlParameter("@IDEspecialidad", SqlDbType.Int);
                if (!string.IsNullOrEmpty(idEspecialidad))
                {
                    varParametros[0].Value = idEspecialidad;
                }
                else
                {
                    varParametros[0].Value = null;
                }
                varParametros[1] = new SqlParameter("@IDClinica", SqlDbType.Int);
                if (!string.IsNullOrEmpty(idClinica))
                {
                    varParametros[1].Value = idClinica;
                }
                else
                {
                    varParametros[1].Value = null;
                }
                varParametros[2] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                varParametros[2].Value = nombre;
                varParametros[3] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[3].Value = tipoDocumento;
                varParametros[4] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[4].Value = numeroDocumento;
                varParametros[5] = new SqlParameter("@SoloMedicosFavoritos", SqlDbType.Bit);
                varParametros[5].Value = soloMedicosFavoritos;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Horario_DirectorioMedicoVirtual", varParametros, TipoProcesamiento.DataReader, false);

                List<DirectorioMedicoBE> varResultado = new List<DirectorioMedicoBE>();
                DirectorioMedicoBE oDirectorioMedicoBE;
                string urlBase = ConfigurationManager.AppSettings["URLImagenes2"].ToString();
                string foto;
                while (varDataReader.Read())
                {
                    oDirectorioMedicoBE = new DirectorioMedicoBE()
                    {
                        cmp = varDataReader.GetString(varDataReader.GetOrdinal("CMP")),
                        nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
                        idEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDEspecialidad")).ToString(),
                        especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Especialidad")),
                        idMedicoFavorito = varDataReader.IsDBNull(varDataReader.GetOrdinal("IDMedicoFavorito")) ? null : varDataReader.GetInt32(varDataReader.GetOrdinal("IDMedicoFavorito")).ToString(),
                        cvVisible = varDataReader.GetString(varDataReader.GetOrdinal("CVVisible")),
                        soloLlamadas = varDataReader.GetString(varDataReader.GetOrdinal("SoloLlamadas")),
                        //foto = varDataReader.GetString(varDataReader.GetOrdinal("Foto"))
                        idSubEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDSubEspecialidad")).ToString(),
                        subEspecialidad = varDataReader.GetString(varDataReader.GetOrdinal("SubEspecialidad")),
                        tipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("TipoPaciente")),
                        abreviatura = varDataReader.GetString(varDataReader.GetOrdinal("Abreviatura")),
                        sexo = varDataReader.GetString(varDataReader.GetOrdinal("Sexo")),
                        codigoTipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("CodigoTipoPaciente")),
                        unidadEspecializacion = varDataReader.GetString(varDataReader.GetOrdinal("UnidadEspecializacion")),
                        campoClinico = varDataReader.GetString(varDataReader.GetOrdinal("CampoClinico")),
                        mensajePersonalizado = varDataReader.GetString(varDataReader.GetOrdinal("MensajePersonalizado")),
                        telefonoSecretaria = varDataReader.GetString(varDataReader.GetOrdinal("TelefonoSecretaria")),
                        prioridad = varDataReader.GetInt32(varDataReader.GetOrdinal("Prioridad"))
                    };
                    foto = varDataReader.GetString(varDataReader.GetOrdinal("Foto"));
                    oDirectorioMedicoBE.foto = !string.IsNullOrEmpty(foto) ? foto : (urlBase + "Medicos/" + (oDirectorioMedicoBE.sexo.Equals("M") ? "medico-m.png" : "medico-f.png"));
                    varResultado.Add(oDirectorioMedicoBE);
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
        #endregion

        #region MantenimientoWeb
        public List<MantMedicoBE> Listar(string cmp, string nombre)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@CMP", SqlDbType.VarChar);
                varParametros[0].Value = cmp;
                varParametros[1] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                varParametros[1].Value = nombre;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("Web_Proc_Medico_Listar", varParametros, TipoProcesamiento.DataReader, false);

                List<MantMedicoBE> varResultado = new List<MantMedicoBE>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new MantMedicoBE()
                    {
                        CMP = varDataReader.GetString(varDataReader.GetOrdinal("CMP")),
                        Nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
                        ApellidoPaterno = varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaterno")),
                        ApellidoMaterno = varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaterno")),
                        Cargo = varDataReader.GetString(varDataReader.GetOrdinal("Cargo")),
                        MuestraCV = varDataReader.GetBoolean(varDataReader.GetOrdinal("MuestraCV")),
                        Foto = varDataReader.GetString(varDataReader.GetOrdinal("Foto")),
                        TituloMedico = varDataReader.GetString(varDataReader.GetOrdinal("TituloMedico")),
                        Premios = varDataReader.GetString(varDataReader.GetOrdinal("Premios")),
                        PertenenciaSociedad = varDataReader.GetString(varDataReader.GetOrdinal("PertenenciaSociedad")),
                        Investigaciones = varDataReader.GetString(varDataReader.GetOrdinal("Investigaciones")),
                        RNE = varDataReader.IsDBNull(varDataReader.GetOrdinal("RNE")) ? (int?)null : varDataReader.GetInt32(varDataReader.GetOrdinal("RNE")),
                        Idiomas = varDataReader.IsDBNull(varDataReader.GetOrdinal("Idiomas")) ? "" : varDataReader.GetString(varDataReader.GetOrdinal("Idiomas")),
                        InformacionAdicional = varDataReader.IsDBNull(varDataReader.GetOrdinal("InformacionAdicional")) ? "" : varDataReader.GetString(varDataReader.GetOrdinal("InformacionAdicional")),
                    });
                }

                return varResultado.OrderBy(p => p.CMP).ToList();
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

        public void Insertar(MantMedicoBE entidad)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[14];
                varParametros[0] = new SqlParameter("@CMP", SqlDbType.VarChar);
                varParametros[0].Value = entidad.CMP;
                varParametros[1] = new SqlParameter("@Nombres", SqlDbType.VarChar);
                varParametros[1].Value = entidad.Nombres;
                varParametros[2] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
                varParametros[2].Value = entidad.ApellidoPaterno;
                varParametros[3] = new SqlParameter("@ApellidoMaterno", SqlDbType.VarChar);
                varParametros[3].Value = entidad.ApellidoMaterno;
                varParametros[4] = new SqlParameter("@Cargo", SqlDbType.VarChar);
                varParametros[4].Value = entidad.Cargo;
                varParametros[5] = new SqlParameter("@MuestraCV", SqlDbType.Bit);
                varParametros[5].Value = entidad.MuestraCV;
                varParametros[6] = new SqlParameter("@Foto", SqlDbType.VarChar);
                varParametros[6].Value = entidad.Foto;
                varParametros[7] = new SqlParameter("@TituloMedico", SqlDbType.VarChar);
                varParametros[7].Value = entidad.TituloMedico;
                varParametros[8] = new SqlParameter("@Premios", SqlDbType.VarChar);
                varParametros[8].Value = entidad.Premios;
                varParametros[9] = new SqlParameter("@PertenenciaSociedad", SqlDbType.VarChar);
                varParametros[9].Value = entidad.PertenenciaSociedad;
                varParametros[10] = new SqlParameter("@Investigaciones", SqlDbType.VarChar);
                varParametros[10].Value = entidad.Investigaciones;
                varParametros[11] = new SqlParameter("@RNE", SqlDbType.Int);
                varParametros[11].Value = entidad.RNE == null ? null : entidad.RNE;
                varParametros[12] = new SqlParameter("@Idiomas", SqlDbType.VarChar);
                varParametros[12].Value = entidad.Idiomas;
                varParametros[13] = new SqlParameter("@InformacionAdicional", SqlDbType.VarChar);
                varParametros[13].Value = entidad.InformacionAdicional;

                varConexion.EjecutarProcedimiento("Web_Proc_Medico_Insertar", varParametros, TipoProcesamiento.NonQuery);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public void Modificar(MantMedicoBE entidad)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[14];
                varParametros[0] = new SqlParameter("@CMP", SqlDbType.VarChar);
                varParametros[0].Value = entidad.CMP;
                varParametros[1] = new SqlParameter("@Nombres", SqlDbType.VarChar);
                varParametros[1].Value = entidad.Nombres;
                varParametros[2] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
                varParametros[2].Value = entidad.ApellidoPaterno;
                varParametros[3] = new SqlParameter("@ApellidoMaterno", SqlDbType.VarChar);
                varParametros[3].Value = entidad.ApellidoMaterno;
                varParametros[4] = new SqlParameter("@Cargo", SqlDbType.VarChar);
                varParametros[4].Value = entidad.Cargo;
                varParametros[5] = new SqlParameter("@MuestraCV", SqlDbType.Bit);
                varParametros[5].Value = entidad.MuestraCV;
                varParametros[6] = new SqlParameter("@Foto", SqlDbType.VarChar);
                varParametros[6].Value = entidad.Foto;
                varParametros[7] = new SqlParameter("@TituloMedico", SqlDbType.VarChar);
                varParametros[7].Value = entidad.TituloMedico;
                varParametros[8] = new SqlParameter("@Premios", SqlDbType.VarChar);
                varParametros[8].Value = entidad.Premios;
                varParametros[9] = new SqlParameter("@PertenenciaSociedad", SqlDbType.VarChar);
                varParametros[9].Value = entidad.PertenenciaSociedad;
                varParametros[10] = new SqlParameter("@Investigaciones", SqlDbType.VarChar);
                varParametros[10].Value = entidad.Investigaciones;
                varParametros[11] = new SqlParameter("@RNE", SqlDbType.Int);
                varParametros[11].Value = entidad.RNE == null ? null : entidad.RNE;
                varParametros[12] = new SqlParameter("@Idiomas", SqlDbType.VarChar);
                varParametros[12].Value = entidad.Idiomas;
                varParametros[13] = new SqlParameter("@InformacionAdicional", SqlDbType.VarChar);
                varParametros[13].Value = entidad.InformacionAdicional;

                varConexion.EjecutarProcedimiento("Web_Proc_Medico_Modificar", varParametros, TipoProcesamiento.NonQuery);
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
