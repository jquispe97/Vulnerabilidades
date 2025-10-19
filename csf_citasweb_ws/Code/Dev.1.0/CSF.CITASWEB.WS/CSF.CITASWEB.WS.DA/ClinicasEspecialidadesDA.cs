using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSF.CITASWEB.WS.BE;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CSF.CITASWEB.WS.DA
{
    public class ClinicasEspecialidadesDA
    {
        #region WS
        private SqlDataReader varDataReader;

        public List<ClinicaSimpleBE> ListarClinicas()
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Clinica_Listar", null, TipoProcesamiento.DataReader, false);

                List<ClinicaSimpleBE> varResultado = new List<ClinicaSimpleBE>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new ClinicaSimpleBE()
                    {
                        idClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString(),
                        nombre = varDataReader.GetString(varDataReader.GetOrdinal("Nombre")),
                        indicadorSedeNueva = varDataReader.GetBoolean(varDataReader.GetOrdinal("IndicadorSedeNueva")),
                        indicadorVirtual = varDataReader.GetBoolean(varDataReader.GetOrdinal("EsClinicaVirtual")),
                        direccion = varDataReader.GetString(varDataReader.GetOrdinal("Direccion"))
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

        public List<CiudadClinicaBE> ListarClinicasConDetalle(string ciudad)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@Ciudad", SqlDbType.VarChar);
                varParametros[0].Value = ciudad;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Clinica_ListarDetalle", varParametros, TipoProcesamiento.DataReader, false);

                List<ClinicaBE> varResultado = new List<ClinicaBE>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new ClinicaBE()
                    {
                        categoria = varDataReader.GetString(varDataReader.GetOrdinal("Categoria")).ToString(),
                        idClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString(),
                        nombre = varDataReader.GetString(varDataReader.GetOrdinal("Nombre")),
                        direccion = varDataReader.GetString(varDataReader.GetOrdinal("Direccion")),
                        ciudad = varDataReader.GetString(varDataReader.GetOrdinal("Ciudad")),
                        foto = varDataReader.GetString(varDataReader.GetOrdinal("Foto")),
                        horariosAtencion = varDataReader.GetString(varDataReader.GetOrdinal("HorariosAtencion")),
                        telefono = varDataReader.GetString(varDataReader.GetOrdinal("Telefono")),
                        latitud = varDataReader.GetDecimal(varDataReader.GetOrdinal("Latitud")).ToString(),
                        longitud = varDataReader.GetDecimal(varDataReader.GetOrdinal("Longitud")).ToString(),
                        tipo = varDataReader.GetInt32(varDataReader.GetOrdinal("Tipo")).ToString(),
                        descripcion = varDataReader.GetString(varDataReader.GetOrdinal("Descripcion"))
                    });
                }

                List<CiudadClinicaBE> varRespuesta = new List<CiudadClinicaBE>();
                if (ciudad == "lima" || ciudad == "todos")
                    varRespuesta.Add(new CiudadClinicaBE()
                    {
                        ciudad = "Lima",
                        listaCiudades = varResultado.Where(p => p.categoria == "Lima").OrderBy(p => p.nombre).ToList()
                    });
                if (ciudad == "provincia" || ciudad == "todos")
                    varRespuesta.Add(new CiudadClinicaBE()
                    {
                        ciudad = "Provincia",
                        listaCiudades = varResultado.Where(p => p.categoria == "Provincia").OrderBy(p => p.nombre).ToList()
                    });

                return varRespuesta;
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

        public List<EspecialidadBE> EspecialidadPorClinica(string idClinica)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@IDClinica", SqlDbType.Int);
                if (string.IsNullOrEmpty(idClinica)) varParametros[0].Value = null;
                else varParametros[0].Value = idClinica;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Clinica_Especialidades", varParametros, TipoProcesamiento.DataReader, false);

                List<EspecialidadBE> varResultado = new List<EspecialidadBE>();
                EspecialidadBE oEspecialidadBE;
                String urlPublicaEspecialidad = ConfigurationManager.AppSettings["rutaPublicaEspecialidadImagen"];
                String urlImagenDefecto = urlPublicaEspecialidad + "default.png"; 
                while (varDataReader.Read())
                {
                    oEspecialidadBE = new EspecialidadBE()
                    {
                        idEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDEspecialidad")).ToString(),
                        especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Nombre")),
                        genero = varDataReader.GetString(varDataReader.GetOrdinal("Genero")),
                        edadMin = varDataReader.GetString(varDataReader.GetOrdinal("EdadMin")),
                        edadMax = varDataReader.GetString(varDataReader.GetOrdinal("EdadMax")),
                        idSubEspecialidad = varDataReader.GetString(varDataReader.GetOrdinal("IDSubEspecialidad")),
                        icono = varDataReader.GetString(varDataReader.GetOrdinal("Icono"))
                    };
                    oEspecialidadBE.icono = !String.IsNullOrEmpty(oEspecialidadBE.icono) ? oEspecialidadBE.icono : urlImagenDefecto;
                    varResultado.Add(oEspecialidadBE);
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

        public List<ClinicaSimpleBE> ClinicaPorEspecialidad(string idEspecialidad)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@IDEspecialidad", SqlDbType.Int);
                varParametros[0].Value = string.IsNullOrEmpty(idEspecialidad) ? null : idEspecialidad; ;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Clinica_PorEspecialidad", varParametros, TipoProcesamiento.DataReader, false);

                List<ClinicaSimpleBE> varResultado = new List<ClinicaSimpleBE>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new ClinicaSimpleBE()
                    {
                        idClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString(),
                        nombre = varDataReader.GetString(varDataReader.GetOrdinal("Nombre"))
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

        public List<ClinicaSimpleBE> ListarClinicasVirtuales()
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_ClinicaVirtual_Listar", null, TipoProcesamiento.DataReader, false);

                List<ClinicaSimpleBE> varResultado = new List<ClinicaSimpleBE>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new ClinicaSimpleBE()
                    {
                        idClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString(),
                        nombre = varDataReader.GetString(varDataReader.GetOrdinal("Nombre"))
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

        public List<ClinicaSimpleBE> ListarClinicasMedico(string cmp)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@CMP", SqlDbType.VarChar);
                varParametros[0].Value = cmp;
                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Clinica_Medico", varParametros, TipoProcesamiento.DataReader, false);

                List<ClinicaSimpleBE> varResultado = new List<ClinicaSimpleBE>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new ClinicaSimpleBE()
                    {
                        idClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString(),
                        nombre = varDataReader.GetString(varDataReader.GetOrdinal("Nombre"))
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

        public List<EspecialidadBE> EspecialidadPorClinicaVirtual(string idClinica)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@IDClinica", SqlDbType.Int);
                varParametros[0].Value = string.IsNullOrEmpty(idClinica) ? null : idClinica;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_ClinicaVirtual_Especialidades", varParametros, TipoProcesamiento.DataReader, false);

                List<EspecialidadBE> varResultado = new List<EspecialidadBE>();
                EspecialidadBE oEspecialidadBE;
                String urlPublicaEspecialidad = ConfigurationManager.AppSettings["rutaPublicaEspecialidadImagen"];
                String urlImagenDefecto = urlPublicaEspecialidad + "default.png";
                while (varDataReader.Read())
                {
                    oEspecialidadBE = new EspecialidadBE()
                    {
                        idEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDEspecialidad")).ToString(),
                        especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Nombre")),
                        genero = varDataReader.GetString(varDataReader.GetOrdinal("Genero")),
                        edadMin = varDataReader.GetString(varDataReader.GetOrdinal("EdadMin")),
                        edadMax = varDataReader.GetString(varDataReader.GetOrdinal("EdadMax")),
                        idSubEspecialidad = varDataReader.GetString(varDataReader.GetOrdinal("IDSubEspecialidad")),
                        icono = varDataReader.GetString(varDataReader.GetOrdinal("Icono"))
                    };
                    oEspecialidadBE.icono = !String.IsNullOrEmpty(oEspecialidadBE.icono) ? oEspecialidadBE.icono : urlImagenDefecto;
                    varResultado.Add(oEspecialidadBE);
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

        public List<EspecialidadBE> EspecialidadPorClinicaVirtualEC()
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[0];

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_ClinicaVirtual_EspecialidadesEC", varParametros, TipoProcesamiento.DataReader, false);

                List<EspecialidadBE> varResultado = new List<EspecialidadBE>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new EspecialidadBE()
                    {
                        idEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDEspecialidad")).ToString(),
                        especialidad = varDataReader.GetString(varDataReader.GetOrdinal("Nombre")),
                        genero = varDataReader.GetString(varDataReader.GetOrdinal("Genero")),
                        edadMin = varDataReader.GetString(varDataReader.GetOrdinal("EdadMin")),
                        edadMax = varDataReader.GetString(varDataReader.GetOrdinal("EdadMax")),
                        idSubEspecialidad = varDataReader.GetString(varDataReader.GetOrdinal("IDSubEspecialidad")),
                        icono = varDataReader.GetString(varDataReader.GetOrdinal("Icono"))
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

        public List<EspecialidadFrecuenteBE> EspecialidadesFrecuentes(string idClinica, string tipoDocumento, string numeroDocumento, string tipoCita)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[4];
                varParametros[0] = new SqlParameter("@IDClinica", SqlDbType.Int);
                varParametros[0].Value = string.IsNullOrEmpty(idClinica) ? null : idClinica;
                varParametros[1] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[1].Value = string.IsNullOrEmpty(tipoDocumento) ? null : tipoDocumento;
                varParametros[2] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[2].Value = string.IsNullOrEmpty(numeroDocumento) ? null : numeroDocumento;
                varParametros[3] = new SqlParameter("@TipoCita", SqlDbType.VarChar);
                varParametros[3].Value = string.IsNullOrEmpty(tipoCita) ? null : tipoCita;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Clinica_Especialidad_Frecuente", varParametros, TipoProcesamiento.DataReader, false);

                List<EspecialidadFrecuenteBE> varResultado = new List<EspecialidadFrecuenteBE>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new EspecialidadFrecuenteBE()
                    {
                        idEspecialidad = varDataReader.GetInt32(varDataReader.GetOrdinal("IDEspecialidad")).ToString(),
                        nombre = varDataReader.GetString(varDataReader.GetOrdinal("Nombre")),
                        genero = varDataReader.GetString(varDataReader.GetOrdinal("Genero")),
                        edadMin = varDataReader.GetString(varDataReader.GetOrdinal("EdadMin")),
                        edadMax = varDataReader.GetString(varDataReader.GetOrdinal("EdadMax")),
                        indicadorFrecuente = varDataReader.GetInt32(varDataReader.GetOrdinal("IndicadorFrecuente")),
                        cantidadCitasAgendadas = varDataReader.GetInt32(varDataReader.GetOrdinal("Cantidad")),
                        fechaUltimaCita = varDataReader.GetString(varDataReader.GetOrdinal("FechaUltimaCita"))
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

        #endregion

        #region MantenimientoWeb
        public List<MantClinicaBE> ClinicaListar(string nombre)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                varParametros[0].Value = nombre;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("Web_Proc_Clinica_Listar", varParametros, TipoProcesamiento.DataReader, false);

                List<MantClinicaBE> varResultado = new List<MantClinicaBE>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new MantClinicaBE()
                    {
                        IDClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")),
                        Nombre = varDataReader.GetString(varDataReader.GetOrdinal("Nombre")),
                        RUC = varDataReader.GetString(varDataReader.GetOrdinal("RUC")),
                        RUCSunasa = varDataReader.GetString(varDataReader.GetOrdinal("RUCSunasa")),
                        CodigoSunasa = varDataReader.GetString(varDataReader.GetOrdinal("CodigoSunasa")),
                        Tipo = varDataReader.GetInt32(varDataReader.GetOrdinal("Tipo")),
                        Direccion = varDataReader.GetString(varDataReader.GetOrdinal("Direccion")),
                        Ciudad = varDataReader.GetString(varDataReader.GetOrdinal("Ciudad")),
                        Foto = varDataReader.GetString(varDataReader.GetOrdinal("Foto")),
                        Abreviatura = varDataReader.GetString(varDataReader.GetOrdinal("Abreviatura")),
                        HorariosAtencion = varDataReader.GetString(varDataReader.GetOrdinal("HorariosAtencion")),
                        Telefono = varDataReader.GetString(varDataReader.GetOrdinal("Telefono")),
                        Latitud = varDataReader.GetDecimal(varDataReader.GetOrdinal("Latitud")),
                        Longitud = varDataReader.GetDecimal(varDataReader.GetOrdinal("Longitud")),
                        EstadoActivo = varDataReader.GetBoolean(varDataReader.GetOrdinal("EstadoActivo"))
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

        public void ClinicaInsertar(MantClinicaBE entidad)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[14];
                varParametros[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                varParametros[0].Value = entidad.Nombre;
                varParametros[1] = new SqlParameter("@RUC", SqlDbType.VarChar);
                varParametros[1].Value = entidad.RUC;
                varParametros[2] = new SqlParameter("@RUCSunasa", SqlDbType.VarChar);
                varParametros[2].Value = entidad.RUCSunasa;
                varParametros[3] = new SqlParameter("@CodigoSunasa", SqlDbType.VarChar);
                varParametros[3].Value = entidad.CodigoSunasa;
                varParametros[4] = new SqlParameter("@Tipo", SqlDbType.Int);
                varParametros[4].Value = entidad.Tipo;
                varParametros[5] = new SqlParameter("@Direccion", SqlDbType.VarChar);
                varParametros[5].Value = entidad.Direccion;
                varParametros[6] = new SqlParameter("@Ciudad", SqlDbType.VarChar);
                varParametros[6].Value = entidad.Ciudad;
                varParametros[7] = new SqlParameter("@Foto", SqlDbType.VarChar);
                varParametros[7].Value = entidad.Foto;
                varParametros[8] = new SqlParameter("@Abreviatura", SqlDbType.VarChar);
                varParametros[8].Value = entidad.Abreviatura;
                varParametros[9] = new SqlParameter("@HorariosAtencion", SqlDbType.VarChar);
                varParametros[9].Value = entidad.HorariosAtencion;
                varParametros[10] = new SqlParameter("@Telefono", SqlDbType.VarChar);
                varParametros[10].Value = entidad.Telefono;
                varParametros[11] = new SqlParameter("@Latitud", SqlDbType.Decimal);
                varParametros[11].Value = entidad.Latitud;
                varParametros[12] = new SqlParameter("@Longitud", SqlDbType.Decimal);
                varParametros[12].Value = entidad.Longitud;
                varParametros[13] = new SqlParameter("@EstadoActivo", SqlDbType.Bit);
                varParametros[13].Value = entidad.EstadoActivo;

                varConexion.EjecutarProcedimiento("Web_Proc_Clinica_Insertar", varParametros, TipoProcesamiento.NonQuery);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public void ClinicaModificar(MantClinicaBE entidad)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[15];
                varParametros[0] = new SqlParameter("@IDClinica", SqlDbType.Int);
                varParametros[0].Value = entidad.IDClinica;
                varParametros[1] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                varParametros[1].Value = entidad.Nombre;
                varParametros[2] = new SqlParameter("@RUC", SqlDbType.VarChar);
                varParametros[2].Value = entidad.RUC;
                varParametros[3] = new SqlParameter("@RUCSunasa", SqlDbType.VarChar);
                varParametros[3].Value = entidad.RUCSunasa;
                varParametros[4] = new SqlParameter("@CodigoSunasa", SqlDbType.VarChar);
                varParametros[4].Value = entidad.CodigoSunasa;
                varParametros[5] = new SqlParameter("@Tipo", SqlDbType.Int);
                varParametros[5].Value = entidad.Tipo;
                varParametros[6] = new SqlParameter("@Direccion", SqlDbType.VarChar);
                varParametros[6].Value = entidad.Direccion;
                varParametros[7] = new SqlParameter("@Ciudad", SqlDbType.VarChar);
                varParametros[7].Value = entidad.Ciudad;
                varParametros[8] = new SqlParameter("@Foto", SqlDbType.VarChar);
                varParametros[8].Value = entidad.Foto;
                varParametros[9] = new SqlParameter("@Abreviatura", SqlDbType.VarChar);
                varParametros[9].Value = entidad.Abreviatura;
                varParametros[10] = new SqlParameter("@HorariosAtencion", SqlDbType.VarChar);
                varParametros[10].Value = entidad.HorariosAtencion;
                varParametros[11] = new SqlParameter("@Telefono", SqlDbType.VarChar);
                varParametros[11].Value = entidad.Telefono;
                varParametros[12] = new SqlParameter("@Latitud", SqlDbType.Decimal);
                varParametros[12].Value = entidad.Latitud;
                varParametros[13] = new SqlParameter("@Longitud", SqlDbType.Decimal);
                varParametros[13].Value = entidad.Longitud;
                varParametros[14] = new SqlParameter("@EstadoActivo", SqlDbType.Bit);
                varParametros[14].Value = entidad.EstadoActivo;

                varConexion.EjecutarProcedimiento("Web_Proc_Clinica_Modificar", varParametros, TipoProcesamiento.NonQuery);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public void ClinicaEliminar(string IDClinica)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@IDClinica", SqlDbType.VarChar);
                varParametros[0].Value = IDClinica;

                varConexion.EjecutarProcedimiento("Web_Proc_Clinica_Eliminar", varParametros, TipoProcesamiento.NonQuery);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("REFERENCE constraint"))
                    throw new Exception("ERRFU:No se puede eliminar la clínica. Tiene registros dependientes.");
                else
                    throw;
            }
            finally
            {
            }
        }
        #endregion

        public ClinicaSimpleBE BuscarClinicaCita(int idCita, bool esVirtual, int idClinica)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                if (esVirtual)
                {
                    SqlParameter[] varParametros = new SqlParameter[2];
                    varParametros[0] = new SqlParameter("@IdCitaVirtual", SqlDbType.Int);
                    varParametros[0].Value = idCita;
                    varParametros[1] = new SqlParameter("@IdClinica", SqlDbType.Int);
                    varParametros[1].Value = idClinica;

                    varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Clinica_BuscarCitaVirtual", varParametros, TipoProcesamiento.DataReader, false);

                    varDataReader.Read();
                    return new ClinicaSimpleBE()
                    {
                        idClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString(),
                        nombre = varDataReader.GetString(varDataReader.GetOrdinal("Nombre")),
                        codigoSunasa = varDataReader.GetString(varDataReader.GetOrdinal("CodigoSunasa")),
                        ruc = varDataReader.GetString(varDataReader.GetOrdinal("RUCSpring"))
                    };
                }
                else
                {
                    SqlParameter[] varParametros = new SqlParameter[1];
                    varParametros[0] = new SqlParameter("@IdCita", SqlDbType.Int);
                    varParametros[0].Value = idCita;

                    varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Clinica_BuscarCita", varParametros, TipoProcesamiento.DataReader, false);

                    varDataReader.Read();
                    return new ClinicaSimpleBE()
                    {
                        idClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString(),
                        nombre = varDataReader.GetString(varDataReader.GetOrdinal("Nombre")),
                        codigoSunasa = varDataReader.GetString(varDataReader.GetOrdinal("CodigoSunasa"))
                    };
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

        public string ClinicaEspecialidadAgrupado(string idClinica, string tipoCita)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {

                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@IDClinica", SqlDbType.Int);
                varParametros[0].Value = idClinica;
                varParametros[1] = new SqlParameter("@TipoCita", SqlDbType.VarChar);
                varParametros[1].Value = tipoCita;

                object rpta = varConexion.EjecutarProcedimiento("App_Proc_Clinica_EspecialidadesAgrupado", varParametros, TipoProcesamiento.Scalar, true);

                return rpta.ToString();


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
