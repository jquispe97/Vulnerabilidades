using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSF.CITASWEB.WS.BE;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using System.Diagnostics;

namespace CSF.CITASWEB.WS.DA
{
    public class UsuarioDA
    {
        private SqlDataReader varDataReader;

        public bool ValidarToken(string token)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@TokenSesion", SqlDbType.VarChar);
                varParametros[0].Value = token;

                string varRespuesta = varConexion.EjecutarProcedimiento("App_Proc_Sesiones_Consultar", varParametros, TipoProcesamiento.Scalar).ToString();
                if (varRespuesta == "1")
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ActualizarTokenPush(string tipoDocumento, string numeroDocumento, string tokenPush)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[3];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@TokenPush", SqlDbType.VarChar);
                varParametros[2].Value = tokenPush;

                varConexion.EjecutarProcedimiento("App_Proc_Sesiones_ActualizarTokenPush", varParametros, TipoProcesamiento.NonQuery);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public UsuarioBE AutenticarWeb(string tipoDocumento, string numeroDocumento, string password, 
                string tokenSesion, out string tokenReal, string userAgent,
                string ipCliente)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[6];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@Password", SqlDbType.VarChar);
                varParametros[2].Value = password;
                varParametros[3] = new SqlParameter("@TokenSesion", SqlDbType.VarChar);
                varParametros[3].Value = tokenSesion;
                varParametros[4] = new SqlParameter("@UserAgent", SqlDbType.VarChar);
                varParametros[4].Value = userAgent;
                varParametros[5] = new SqlParameter("@IPCliente", SqlDbType.VarChar);
                varParametros[5].Value = ipCliente;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Usuario_AuntenticarWeb", varParametros, TipoProcesamiento.DataReader, false);
                varDataReader.Read();

                //string rutaLocal = ConfigurationManager.AppSettings["RutaImagenes"].ToString();
                //string urlBase = ConfigurationManager.AppSettings["URLImagenes"].ToString();
                //string nombreImagen = tipoDocumento + "_" + numeroDocumento;
                //string fotoPorDefecto = ConfigurationManager.AppSettings["FotoPorDefecto"].ToString();
                string urlBase = ConfigurationManager.AppSettings["rutaPublicaUsuarioImagen"].ToString();

                UsuarioBE varRespuesta = new UsuarioBE()
                {
                    tipoDocumento = tipoDocumento,
                    numeroDocumento = numeroDocumento,
                    nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
                    apellidoPaterno = varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaterno")),
                    apellidoMaterno = varDataReader.IsDBNull(varDataReader.GetOrdinal("ApellidoMaterno")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaterno")),
                    fechaNacimiento = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaNacimiento")).ToString("dd/MM/yyyy"),
                    telefono = varDataReader.IsDBNull(varDataReader.GetOrdinal("TelefonoFijo")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TelefonoFijo")),
                    celular = varDataReader.IsDBNull(varDataReader.GetOrdinal("TelefonoCelular")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TelefonoCelular")),
                    email = varDataReader.IsDBNull(varDataReader.GetOrdinal("Email")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Email")),
                    genero = varDataReader.GetString(varDataReader.GetOrdinal("Genero")),

                    tipoPaciente = varDataReader.IsDBNull(varDataReader.GetOrdinal("TipoPaciente")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TipoPaciente")),
                    seguroPlanSaludCodigo = varDataReader.IsDBNull(varDataReader.GetOrdinal("RUCSeguro")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("RUCSeguro")),
                    tipoUsuario = varDataReader.GetString(varDataReader.GetOrdinal("TipoUsuario")),
                    cmp = varDataReader.GetString(varDataReader.GetOrdinal("CMP")),
                    esMedico = (varDataReader.GetString(varDataReader.GetOrdinal("TipoUsuario")) == "PACIENTE") ? false : true,

                    idPais = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdPais")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdPais")),
                    idDepartamento = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDepartamento")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDepartamento")),
                    idProvincia = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdProvincia")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdProvincia")),
                    idDistrito = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDistrito")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDistrito")),
                    pais = varDataReader.IsDBNull(varDataReader.GetOrdinal("Pais")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Pais")),
                    departamento = varDataReader.IsDBNull(varDataReader.GetOrdinal("Departamento")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Departamento")),
                    provincia = varDataReader.IsDBNull(varDataReader.GetOrdinal("Provincia")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Provincia")),
                    distrito = varDataReader.IsDBNull(varDataReader.GetOrdinal("Distrito")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Distrito")),

                    idPaisNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdPaisNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdPaisNac")),
                    idDepartamentoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDepartamentoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDepartamentoNac")),
                    idProvinciaNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdProvinciaNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdProvinciaNac")),
                    idDistritoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDistritoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDistritoNac")),
                    paisNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("PaisNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("PaisNac")),
                    departamentoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("DepartamentoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("DepartamentoNac")),
                    provinciaNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("ProvinciaNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("ProvinciaNac")),
                    distritoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("DistritoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("DistritoNac")),

                    observacion = varDataReader.IsDBNull(varDataReader.GetOrdinal("Observacion")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Observacion")),
                    direccion = varDataReader.IsDBNull(varDataReader.GetOrdinal("Direccion")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Direccion")),
                    finalidadesAdiciona = varDataReader.GetBoolean(varDataReader.GetOrdinal("FinalidadesAdiciona")),
                    idAmbulatorio = varDataReader.GetString(varDataReader.GetOrdinal("IdAmbulatorio")),
                    esRENIEC = varDataReader.GetBoolean(varDataReader.GetOrdinal("esRENIEC")),
                    iconoEsRENIEC = varDataReader.GetString(varDataReader.GetOrdinal("IconoEsRENIEC")),
                    textoEsRENIEC = varDataReader.GetString(varDataReader.GetOrdinal("TextoEsRENIEC"))

                    //foto = File.Exists(Path.Combine(rutaLocal, nombreImagen + ".jpg")) ? (urlBase + nombreImagen + ".jpg")
                    //                    : File.Exists(Path.Combine(rutaLocal, nombreImagen + ".png")) ? (urlBase + nombreImagen + ".png")
                    //                    : (urlBase + fotoPorDefecto)
                    //nombreArchivoUsuario = (varDataReader.IsDBNull(varDataReader.GetOrdinal("NombreArchivoUsuario")) || varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoUsuario")).Equals("")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoUsuario"))
                };
                string nombreArchivoFisico = varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoFisico"));
                string nombreArchivoUsuario = (varDataReader.IsDBNull(varDataReader.GetOrdinal("NombreArchivoUsuario")) || varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoUsuario")).Equals("")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoUsuario"));
                varRespuesta.foto = urlBase + (!String.IsNullOrEmpty(nombreArchivoFisico) ? nombreArchivoFisico : (varRespuesta.genero.Equals("M") ? "user-hombre-azul.png" : "user-mujer-azul.png"));
                varRespuesta.nombreArchivoUsuario = !String.IsNullOrEmpty(nombreArchivoUsuario) ? nombreArchivoUsuario : (varRespuesta.genero.Equals("M") ? "user-hombre-azul.png" : "user-mujer-azul.png");
                varRespuesta.nombre = varRespuesta.nombres.Split(' ')[0];

                varDataReader.NextResult();
                varDataReader.Read();
                tokenReal = varDataReader.GetString(varDataReader.GetOrdinal("TokenSesion"));
                
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

        public UsuarioBE ValidarExistenciaUsuario(string tipoDocumento, string numeroDocumento)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Usuario_ValidarExistencia", varParametros, TipoProcesamiento.DataReader, false);

                string urlBase = ConfigurationManager.AppSettings["rutaPublicaUsuarioImagen"].ToString();
                
                UsuarioBE varRespuesta = null;
                if (varDataReader != null)
                {
                    if (varDataReader.Read())
                    {
                        varRespuesta = new UsuarioBE()
                        {
                            tipoDocumento = tipoDocumento,
                            numeroDocumento = numeroDocumento,
                            nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
                            apellidoPaterno = varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaterno")),
                            apellidoMaterno = varDataReader.IsDBNull(varDataReader.GetOrdinal("ApellidoMaterno")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaterno")),
                            fechaNacimiento = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaNacimiento")).ToString("dd/MM/yyyy"),
                            telefono = varDataReader.IsDBNull(varDataReader.GetOrdinal("TelefonoFijo")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TelefonoFijo")),
                            celular = varDataReader.IsDBNull(varDataReader.GetOrdinal("TelefonoCelular")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TelefonoCelular")),
                            email = varDataReader.IsDBNull(varDataReader.GetOrdinal("Email")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Email")),
                            genero = varDataReader.GetString(varDataReader.GetOrdinal("Genero")),

                            tipoPaciente = varDataReader.IsDBNull(varDataReader.GetOrdinal("TipoPaciente")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TipoPaciente")),
                            seguroPlanSaludCodigo = varDataReader.IsDBNull(varDataReader.GetOrdinal("RUCSeguro")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("RUCSeguro")),

                            idPais = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdPais")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdPais")),
                            idDepartamento = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDepartamento")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDepartamento")),
                            idProvincia = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdProvincia")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdProvincia")),
                            idDistrito = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDistrito")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDistrito")),
                            pais = varDataReader.IsDBNull(varDataReader.GetOrdinal("Pais")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Pais")),
                            departamento = varDataReader.IsDBNull(varDataReader.GetOrdinal("Departamento")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Departamento")),
                            provincia = varDataReader.IsDBNull(varDataReader.GetOrdinal("Provincia")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Provincia")),
                            distrito = varDataReader.IsDBNull(varDataReader.GetOrdinal("Distrito")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Distrito")),

                            idPaisNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdPaisNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdPaisNac")),
                            idDepartamentoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDepartamentoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDepartamentoNac")),
                            idProvinciaNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdProvinciaNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdProvinciaNac")),
                            idDistritoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDistritoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDistritoNac")),
                            paisNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("PaisNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("PaisNac")),
                            departamentoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("DepartamentoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("DepartamentoNac")),
                            provinciaNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("ProvinciaNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("ProvinciaNac")),
                            distritoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("DistritoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("DistritoNac")),

                            observacion = varDataReader.IsDBNull(varDataReader.GetOrdinal("Observacion")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Observacion")),
                            direccion = varDataReader.IsDBNull(varDataReader.GetOrdinal("Direccion")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Direccion")),
                            finalidadesAdiciona = varDataReader.GetBoolean(varDataReader.GetOrdinal("FinalidadesAdiciona")),
                            idAmbulatorio = varDataReader.GetString(varDataReader.GetOrdinal("IdAmbulatorio")),
                            password = varDataReader.GetString(varDataReader.GetOrdinal("Password"))
                        };

                        string nombreArchivoFisico = varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoFisico"));
                        string nombreArchivoUsuario = (varDataReader.IsDBNull(varDataReader.GetOrdinal("NombreArchivoUsuario")) || varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoUsuario")).Equals("")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoUsuario"));
                        varRespuesta.foto = urlBase + (!String.IsNullOrEmpty(nombreArchivoFisico) ? nombreArchivoFisico : (varRespuesta.genero.Equals("M") ? "user-hombre-azul.png" : "user-mujer-azul.png"));
                        varRespuesta.nombreArchivoUsuario = !String.IsNullOrEmpty(nombreArchivoUsuario) ? nombreArchivoUsuario : (varRespuesta.genero.Equals("M") ? "user-hombre-azul.png" : "user-mujer-azul.png");
                    }
                }

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

        public UsuarioBE ValidarExistenciaPaciente(string tipoDocumento, string numeroDocumento)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Paciente_ValidarExistencia", varParametros, TipoProcesamiento.DataReader, false);

                string urlBase = ConfigurationManager.AppSettings["rutaPublicaUsuarioImagen"].ToString();

                UsuarioBE varRespuesta = null;
                if (varDataReader != null)
                {
                    if (varDataReader.Read())
                    {
                        varRespuesta = new UsuarioBE()
                        {
                            tipoDocumento = tipoDocumento,
                            numeroDocumento = numeroDocumento,
                            nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
                            apellidoPaterno = varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaterno")),
                            apellidoMaterno = varDataReader.IsDBNull(varDataReader.GetOrdinal("ApellidoMaterno")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaterno")),
                            fechaNacimiento = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaNacimiento")).ToString("dd/MM/yyyy"),
                            telefono = varDataReader.IsDBNull(varDataReader.GetOrdinal("TelefonoFijo")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TelefonoFijo")),
                            celular = varDataReader.IsDBNull(varDataReader.GetOrdinal("TelefonoCelular")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TelefonoCelular")),
                            email = varDataReader.IsDBNull(varDataReader.GetOrdinal("Email")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Email")),
                            genero = varDataReader.GetString(varDataReader.GetOrdinal("Genero")),

                            tipoPaciente = varDataReader.IsDBNull(varDataReader.GetOrdinal("TipoPaciente")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TipoPaciente")),
                            seguroPlanSaludCodigo = varDataReader.IsDBNull(varDataReader.GetOrdinal("RUCSeguro")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("RUCSeguro")),

                            idPais = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdPais")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdPais")),
                            idDepartamento = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDepartamento")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDepartamento")),
                            idProvincia = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdProvincia")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdProvincia")),
                            idDistrito = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDistrito")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDistrito")),
                            pais = varDataReader.IsDBNull(varDataReader.GetOrdinal("Pais")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Pais")),
                            departamento = varDataReader.IsDBNull(varDataReader.GetOrdinal("Departamento")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Departamento")),
                            provincia = varDataReader.IsDBNull(varDataReader.GetOrdinal("Provincia")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Provincia")),
                            distrito = varDataReader.IsDBNull(varDataReader.GetOrdinal("Distrito")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Distrito")),

                            idPaisNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdPaisNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdPaisNac")),
                            idDepartamentoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDepartamentoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDepartamentoNac")),
                            idProvinciaNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdProvinciaNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdProvinciaNac")),
                            idDistritoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDistritoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDistritoNac")),
                            paisNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("PaisNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("PaisNac")),
                            departamentoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("DepartamentoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("DepartamentoNac")),
                            provinciaNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("ProvinciaNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("ProvinciaNac")),
                            distritoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("DistritoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("DistritoNac")),

                            observacion = varDataReader.IsDBNull(varDataReader.GetOrdinal("Observacion")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Observacion")),
                            direccion = varDataReader.IsDBNull(varDataReader.GetOrdinal("Direccion")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Direccion")),
                            finalidadesAdiciona = varDataReader.GetBoolean(varDataReader.GetOrdinal("FinalidadesAdiciona")),
                            idAmbulatorio = varDataReader.GetString(varDataReader.GetOrdinal("IdAmbulatorio")),
                            esRENIEC = varDataReader.GetBoolean(varDataReader.GetOrdinal("esRENIEC")),
                            iconoEsRENIEC = varDataReader.GetString(varDataReader.GetOrdinal("IconoEsRENIEC")),
                            textoEsRENIEC = varDataReader.GetString(varDataReader.GetOrdinal("TextoEsRENIEC"))
                        };

                        string nombreArchivoFisico = varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoFisico"));
                        string nombreArchivoUsuario = (varDataReader.IsDBNull(varDataReader.GetOrdinal("NombreArchivoUsuario")) || varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoUsuario")).Equals("")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoUsuario"));
                        varRespuesta.foto = urlBase + (!String.IsNullOrEmpty(nombreArchivoFisico) ? nombreArchivoFisico : (varRespuesta.genero.Equals("M") ? "user-hombre-azul.png" : "user-mujer-azul.png"));
                        varRespuesta.nombreArchivoUsuario = !String.IsNullOrEmpty(nombreArchivoUsuario) ? nombreArchivoUsuario : (varRespuesta.genero.Equals("M") ? "user-hombre-azul.png" : "user-mujer-azul.png");
                    }
                }

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


        public string RecordarPassword(string tipoDocumento, string numeroDocumento, string password, string canal, string correo,
            string codigoOTP, string tipoOTP, string origen)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[8];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@Password", SqlDbType.VarChar);
                varParametros[2].Value = password;
                varParametros[3] = new SqlParameter("@Canal", SqlDbType.VarChar);
                varParametros[3].Value = canal;
                varParametros[4] = new SqlParameter("@Email", SqlDbType.VarChar);
                varParametros[4].Value = correo;
                varParametros[5] = new SqlParameter("@CodigoOTP", SqlDbType.VarChar);
                if (!String.IsNullOrEmpty(codigoOTP)) varParametros[5].Value = codigoOTP;
                else varParametros[5].Value = null;
                varParametros[6] = new SqlParameter("@TipoOTP", SqlDbType.VarChar);
                if (!String.IsNullOrEmpty(tipoOTP)) varParametros[6].Value = tipoOTP;
                else varParametros[6].Value = null;
                varParametros[7] = new SqlParameter("@tvOrigen", SqlDbType.VarChar);
                if (!String.IsNullOrEmpty(origen)) varParametros[7].Value = origen;
                else varParametros[7].Value = null;


                return varConexion.EjecutarProcedimiento("App_Proc_Usuario_RegenerarClave", varParametros, TipoProcesamiento.Scalar).ToString();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }
        public DataPassword RecordarPasswordDatos(string tipoDocumento, string numeroDocumento)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                //Debug.Write("Entro");
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Usuario_DatosCambiarClave", varParametros, TipoProcesamiento.DataReader, false);

                varDataReader.Read();
                return new DataPassword()
                {
                    email = varDataReader.IsDBNull(varDataReader.GetOrdinal("Email")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Email")),
                    celular = varDataReader.IsDBNull(varDataReader.GetOrdinal("TelefonoCelular")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TelefonoCelular"))
                };
                // userDta.email = varDataReader.IsDBNull(varDataReader.GetOrdinal("Email")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Email"));
                // userDta.cellphone =  varDataReader.IsDBNull(varDataReader.GetOrdinal("TelefonoCelular")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TelefonoCelular"));


            }
            catch (Exception)
            {
                //Debug.Write("Entro error");
                throw;
            }
            finally
            {
            }
        }

        public void CambiarPassword(string token, string password)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@Token", SqlDbType.VarChar);
                varParametros[0].Value = token;
                varParametros[1] = new SqlParameter("@Password", SqlDbType.VarChar);
                varParametros[1].Value = password;

                varConexion.EjecutarProcedimiento("App_Proc_Usuario_CambiarClave", varParametros, TipoProcesamiento.NonQuery);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public void RegistrarUsuario(string tipoDocumento, string numeroDocumento, string nombres,
                                            string apellidoPaterno, string apellidoMaterno, DateTime fechaNacimiento,
                                            string genero, string email, string celular, string seguroPlanSalud,
                                            string password, bool finalidadesAdiciona, string direccion,
                                            string numero, string departamento, string referencia,
                                            string latlong, string origen, string nombreArchivo, string nombreGenerado,
                                            string telefono, string tipoPaciente, string observacion,
                                            string idPais, string idDepartamento, string idProvincia, string idDistrito,
                                            string idPaisNac, string idDepartamentoNac, string idProvinciaNac, string idDistritoNac,
                                            string codigoOTP, string tipoOTP, bool esIntranet, string finesAdicionales)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[35];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@Nombres", SqlDbType.VarChar);
                varParametros[2].Value = nombres;
                varParametros[3] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
                varParametros[3].Value = apellidoPaterno;
                varParametros[4] = new SqlParameter("@ApellidoMaterno", SqlDbType.VarChar);
                varParametros[4].Value = apellidoMaterno;
                varParametros[5] = new SqlParameter("@Genero", SqlDbType.Char);
                varParametros[5].Value = genero;
                varParametros[6] = new SqlParameter("@FechaNacimiento", SqlDbType.DateTime);
                varParametros[6].Value = fechaNacimiento;
                varParametros[7] = new SqlParameter("@Email", SqlDbType.VarChar);
                varParametros[7].Value = email;
                varParametros[8] = new SqlParameter("@TelefonoCelular", SqlDbType.VarChar);
                varParametros[8].Value = celular;
                varParametros[9] = new SqlParameter("@RUC", SqlDbType.Char);
                varParametros[9].Value = seguroPlanSalud;
                varParametros[10] = new SqlParameter("@Password", SqlDbType.VarChar);
                if (!String.IsNullOrEmpty(password)) varParametros[10].Value = password;
                else varParametros[10].Value = null;
                varParametros[11] = new SqlParameter("@FinalidadesAdiciona", SqlDbType.Bit);
                varParametros[11].Value = finalidadesAdiciona;

                varParametros[12] = new SqlParameter("@Direccion", SqlDbType.VarChar, 200);
                varParametros[12].Value = direccion;
                varParametros[13] = new SqlParameter("@Numero", SqlDbType.VarChar, 50);
                varParametros[13].Value = numero;
                varParametros[14] = new SqlParameter("@Departamento", SqlDbType.VarChar, 20);
                varParametros[14].Value = departamento;
                varParametros[15] = new SqlParameter("@Referencia", SqlDbType.VarChar, 200);
                varParametros[15].Value = referencia;

                varParametros[16] = new SqlParameter("@LatLong", SqlDbType.VarChar, 50);
                varParametros[16].Value = latlong;

                varParametros[17] = new SqlParameter("@Origen", SqlDbType.VarChar, 50);
                varParametros[17].Value = origen;

                varParametros[18] = new SqlParameter("@NombreArchivoUsuario", SqlDbType.VarChar, 120);
                varParametros[18].Value = nombreArchivo;

                varParametros[19] = new SqlParameter("@NombreArchivoFisico", SqlDbType.VarChar, 80);
                varParametros[19].Value = nombreGenerado;

                varParametros[20] = new SqlParameter("@TelefonoFijo", SqlDbType.VarChar, 9);
                if (!String.IsNullOrEmpty(telefono))
                {
                    varParametros[20].Value = telefono;
                }
                else
                {
                    varParametros[20].Value = null;
                }
                varParametros[21] = new SqlParameter("@TipoPaciente", SqlDbType.VarChar, 3);
                if (!String.IsNullOrEmpty(tipoPaciente))
                {
                    varParametros[21].Value = tipoPaciente;
                }
                else
                {
                    varParametros[21].Value = null;
                }
                varParametros[22] = new SqlParameter("@Observaciones", SqlDbType.VarChar);
                if (!String.IsNullOrEmpty(observacion))
                {
                    varParametros[22].Value = observacion;
                }
                else
                {
                    varParametros[22].Value = null;
                }
                varParametros[23] = new SqlParameter("@PaisId", SqlDbType.VarChar, 3);
                if (!String.IsNullOrEmpty(idPais))
                {
                    varParametros[23].Value = idPais;
                }
                else
                {
                    varParametros[23].Value = null;
                }
                varParametros[24] = new SqlParameter("@DepartamentoId", SqlDbType.VarChar, 2);
                if (!String.IsNullOrEmpty(idDepartamento))
                {
                    varParametros[24].Value = idDepartamento;
                }
                else
                {
                    varParametros[24].Value = null;
                }
                varParametros[25] = new SqlParameter("@ProvinciaId", SqlDbType.VarChar, 2);
                if (!String.IsNullOrEmpty(idProvincia))
                {
                    varParametros[25].Value = idProvincia;
                }
                else
                {
                    varParametros[25].Value = null;
                }
                varParametros[26] = new SqlParameter("@DistritoId", SqlDbType.VarChar, 2);
                if (!String.IsNullOrEmpty(idDistrito))
                {
                    varParametros[26].Value = idDistrito;
                }
                else
                {
                    varParametros[26].Value = null;
                }
                varParametros[27] = new SqlParameter("@PaisIdNac", SqlDbType.VarChar, 3);
                if (!String.IsNullOrEmpty(idPaisNac))
                {
                    varParametros[27].Value = idPaisNac;
                }
                else
                {
                    varParametros[27].Value = null;
                }
                varParametros[28] = new SqlParameter("@DepartamentoIdNac", SqlDbType.VarChar, 2);
                if (!String.IsNullOrEmpty(idDepartamentoNac))
                {
                    varParametros[28].Value = idDepartamentoNac;
                }
                else
                {
                    varParametros[28].Value = null;
                }
                varParametros[29] = new SqlParameter("@ProvinciaIdNac", SqlDbType.VarChar, 2);
                if (!String.IsNullOrEmpty(idProvinciaNac))
                {
                    varParametros[29].Value = idProvinciaNac;
                }
                else
                {
                    varParametros[29].Value = null;
                }
                varParametros[30] = new SqlParameter("@DistritoIdNac", SqlDbType.VarChar, 2);
                if (!String.IsNullOrEmpty(idDistritoNac))
                {
                    varParametros[30].Value = idDistritoNac;
                }
                else
                {
                    varParametros[30].Value = null;
                }
                varParametros[31] = new SqlParameter("@CodigoOTP", SqlDbType.VarChar);
                if (!String.IsNullOrEmpty(codigoOTP)) varParametros[31].Value = codigoOTP;
                else varParametros[31].Value = null;
                varParametros[32] = new SqlParameter("@TipoOTP", SqlDbType.VarChar);
                if (!String.IsNullOrEmpty(tipoOTP)) varParametros[32].Value = tipoOTP;
                else varParametros[32].Value = null;
                varParametros[33] = new SqlParameter("@tlEsIntranet", SqlDbType.Bit);
                varParametros[33].Value = esIntranet;
                varParametros[34] = new SqlParameter("@tlFinesAdicionales", SqlDbType.Bit);
                if (!String.IsNullOrEmpty(finesAdicionales)) varParametros[34].Value = bool.Parse(finesAdicionales);
                else varParametros[34].Value = null;

                varConexion.EjecutarProcedimiento("App_Proc_Usuario_Crear", varParametros, TipoProcesamiento.NonQuery);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public string RegistrarMedicoFavorito(string tipoDocumento, string numeroDocumento, string cmp, string idEspecialidad)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[4];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@CMP", SqlDbType.VarChar);
                varParametros[2].Value = cmp;
                varParametros[3] = new SqlParameter("@IDEspecialidad", SqlDbType.Int);
                varParametros[3].Value = idEspecialidad;

                return varConexion.EjecutarProcedimiento("App_Proc_MedicoFavorito_Agregar", varParametros, TipoProcesamiento.Scalar).ToString();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public List<MedicoFavoritoBE> ListarMedicosFavoritos(string tipoDocumento, string numeroDocumento)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_MedicoFavorito_Listar", varParametros, TipoProcesamiento.DataReader, false);

                List<MedicoFavoritoBE> varResultado = new List<MedicoFavoritoBE>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new MedicoFavoritoBE()
                    {
                        idMedicoFavorito = varDataReader.GetInt32(varDataReader.GetOrdinal("IDMedicoFavorito")).ToString(),
                        medico = new MedicoBE()
                        {
                            cmp = varDataReader.GetString(varDataReader.GetOrdinal("CMP")),
                            nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
                            cargo = varDataReader.GetString(varDataReader.GetOrdinal("Cargo")),
                            mostrarCV = varDataReader.GetString(varDataReader.GetOrdinal("MuestraCV")),
                            tituloMedico = varDataReader.GetString(varDataReader.GetOrdinal("TituloMedico")),
                            premiosHonores = varDataReader.GetString(varDataReader.GetOrdinal("Premios")),
                            pertenenciaSociedad = varDataReader.GetString(varDataReader.GetOrdinal("PertenenciaSociedad")),
                            investigacionPublicaciones = varDataReader.GetString(varDataReader.GetOrdinal("Investigaciones")),
                            centrosAtencion = new List<ClinicaBE>(),
                            especialidad = new List<EspecialidadBE>(),
                            foto = varDataReader.GetString(varDataReader.GetOrdinal("Foto"))
                        }
                    });
                }

                //clínicas
                varDataReader.NextResult();
                while (varDataReader.Read())
                {
                    varResultado.Where(p => p.medico.cmp == varDataReader.GetString(varDataReader.GetOrdinal("CMP")).ToString())
                                                        .First().medico.centrosAtencion.Add(new ClinicaBE()
                                                        {
                                                            idClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString(),
                                                            nombre = varDataReader.GetString(varDataReader.GetOrdinal("Nombre")),
                                                            soloLlamadas = varDataReader.GetString(varDataReader.GetOrdinal("SoloLlamadas"))
                                                        });
                }

                //especialidades
                varDataReader.NextResult();
                while (varDataReader.Read())
                {
                    varResultado.Where(p => p.medico.cmp == varDataReader.GetString(varDataReader.GetOrdinal("CMP")).ToString())
                                                        .First().medico.especialidad.Add(new EspecialidadBE()
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

        public bool EliminarMedicoFavorito(int idMedicoFavorito)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@IDMedicoFavorito", SqlDbType.Int);
                varParametros[0].Value = idMedicoFavorito;

                varConexion.EjecutarProcedimiento("App_Proc_MedicoFavorito_Eliminar", varParametros, TipoProcesamiento.NonQuery);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public void AgregarFamiliar(string tipoDocumento, string numeroDocumento, string tipoDocumentoTitular,
                                    string numeroDocumentoTitular, string nombres,
                                    string apellidoPaterno, string apellidoMaterno, string genero,
                                    DateTime fechaNacimiento, string email, string celular, string seguroPlanSalud,
                                    string direccion, string numeroDireccion, string numeroDepartamento,
                                    string referencia, string latlong, string tipoParentesco, string nombreArchivo,
                                    string nombreGenerado, string telefono, string tipoPaciente, string observacion,
                                    string idPais = "", string idDepartamento = "", string idProvincia = "", string idDistrito = "",
                                    string idPaisNac = "", string idDepartamentoNac = "", string idProvinciaNac = "", string idDistritoNac = "",
                                    bool finalidadesAdiciona = false)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[32];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@TipoDocumentoTitular", SqlDbType.VarChar);
                varParametros[2].Value = tipoDocumentoTitular;
                varParametros[3] = new SqlParameter("@NumeroDocumentoTitular", SqlDbType.VarChar);
                varParametros[3].Value = numeroDocumentoTitular;
                varParametros[4] = new SqlParameter("@Nombres", SqlDbType.VarChar);
                varParametros[4].Value = nombres;
                varParametros[5] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
                varParametros[5].Value = apellidoPaterno;
                varParametros[6] = new SqlParameter("@ApellidoMaterno", SqlDbType.VarChar);
                varParametros[6].Value = apellidoMaterno;
                varParametros[7] = new SqlParameter("@Genero", SqlDbType.Char);
                varParametros[7].Value = genero;
                varParametros[8] = new SqlParameter("@FechaNacimiento", SqlDbType.DateTime);
                varParametros[8].Value = fechaNacimiento;
                varParametros[9] = new SqlParameter("@Email", SqlDbType.VarChar);
                varParametros[9].Value = email;
                varParametros[10] = new SqlParameter("@TelefonoCelular", SqlDbType.VarChar);
                varParametros[10].Value = celular;
                varParametros[11] = new SqlParameter("@RUC", SqlDbType.Char);
                varParametros[11].Value = seguroPlanSalud;

                varParametros[12] = new SqlParameter("@Direccion", SqlDbType.VarChar, 200);
                varParametros[12].Value = direccion;
                varParametros[13] = new SqlParameter("@Numero", SqlDbType.VarChar, 50);
                varParametros[13].Value = numeroDireccion;
                varParametros[14] = new SqlParameter("@Departamento", SqlDbType.VarChar, 20);
                varParametros[14].Value = numeroDepartamento;
                varParametros[15] = new SqlParameter("@Referencia", SqlDbType.VarChar, 200);
                varParametros[15].Value = referencia;

                varParametros[16] = new SqlParameter("@LatLong", SqlDbType.VarChar, 50);
                varParametros[16].Value = latlong;
                varParametros[17] = new SqlParameter("@TipoParentesco", SqlDbType.VarChar, 3);
                varParametros[17].Value = tipoParentesco;

                varParametros[18] = new SqlParameter("@NombreArchivoUsuario", SqlDbType.VarChar, 120);
                varParametros[18].Value = nombreArchivo;

                varParametros[19] = new SqlParameter("@NombreArchivoFisico", SqlDbType.VarChar, 80);
                varParametros[19].Value = nombreGenerado;

                varParametros[20] = new SqlParameter("@TelefonoFijo", SqlDbType.VarChar, 9);
                if (!String.IsNullOrEmpty(telefono))
                {
                    varParametros[20].Value = telefono;
                }
                else
                {
                    varParametros[20].Value = null;
                }
                varParametros[21] = new SqlParameter("@TipoPaciente", SqlDbType.VarChar, 3);
                if (!String.IsNullOrEmpty(tipoPaciente))
                {
                    varParametros[21].Value = tipoPaciente;
                }
                else
                {
                    varParametros[21].Value = null;
                }
                varParametros[22] = new SqlParameter("@Observaciones", SqlDbType.VarChar);
                if (!String.IsNullOrEmpty(observacion))
                {
                    varParametros[22].Value = observacion;
                }
                else
                {
                    varParametros[22].Value = null;
                }
                varParametros[23] = new SqlParameter("@PaisId", SqlDbType.VarChar, 3);
                if (!String.IsNullOrEmpty(idPais))
                {
                    varParametros[23].Value = idPais;
                }
                else
                {
                    varParametros[23].Value = null;
                }
                varParametros[24] = new SqlParameter("@DepartamentoId", SqlDbType.VarChar, 2);
                if (!String.IsNullOrEmpty(idDepartamento))
                {
                    varParametros[24].Value = idDepartamento;
                }
                else
                {
                    varParametros[24].Value = null;
                }
                varParametros[25] = new SqlParameter("@ProvinciaId", SqlDbType.VarChar, 2);
                if (!String.IsNullOrEmpty(idProvincia))
                {
                    varParametros[25].Value = idProvincia;
                }
                else
                {
                    varParametros[25].Value = null;
                }
                varParametros[26] = new SqlParameter("@DistritoId", SqlDbType.VarChar, 2);
                if (!String.IsNullOrEmpty(idDistrito))
                {
                    varParametros[26].Value = idDistrito;
                }
                else
                {
                    varParametros[26].Value = null;
                }
                varParametros[27] = new SqlParameter("@PaisIdNac", SqlDbType.VarChar, 3);
                if (!String.IsNullOrEmpty(idPaisNac))
                {
                    varParametros[27].Value = idPaisNac;
                }
                else
                {
                    varParametros[27].Value = null;
                }
                varParametros[28] = new SqlParameter("@DepartamentoIdNac", SqlDbType.VarChar, 2);
                if (!String.IsNullOrEmpty(idDepartamentoNac))
                {
                    varParametros[28].Value = idDepartamentoNac;
                }
                else
                {
                    varParametros[28].Value = null;
                }
                varParametros[29] = new SqlParameter("@ProvinciaIdNac", SqlDbType.VarChar, 2);
                if (!String.IsNullOrEmpty(idProvinciaNac))
                {
                    varParametros[29].Value = idProvinciaNac;
                }
                else
                {
                    varParametros[29].Value = null;
                }
                varParametros[30] = new SqlParameter("@DistritoIdNac", SqlDbType.VarChar, 2);
                if (!String.IsNullOrEmpty(idDistritoNac))
                {
                    varParametros[30].Value = idDistritoNac;
                }
                else
                {
                    varParametros[30].Value = null;
                }
                varParametros[31] = new SqlParameter("@FinalidadesAdiciona", SqlDbType.Bit);
                varParametros[31].Value = finalidadesAdiciona;

                varConexion.EjecutarProcedimiento("App_Proc_Familiar_Agregar", varParametros, TipoProcesamiento.NonQuery);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public void ModificarFamiliar(string tipoDocumento, string numeroDocumento, string tipoDocumentoTitular,
                                    string numeroDocumentoTitular, string genero, string fechaNacimiento,
                                    string email, string celular, string seguroPlanSalud, string tipoParentesco, string direccion, 
                                    string numeroDireccion, string numeroDepartamento, string referencia, string latlong, string nombres = "", 
                                    string apellidoPaterno = "", string apellidoMaterno = "", string nombreArchivo = "", string nombreGenerado = "",
                                    string telefono = "", string tipoPaciente = "", string observacion = "",
                                    string idPais = "", string idDepartamento = "", string idProvincia = "", string idDistrito = "",
                                    string idPaisNac = "", string idDepartamentoNac = "", string idProvinciaNac = "", string idDistritoNac = "",
                                    bool finalidadesAdiciona = false, bool esIntranet = false)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[33];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@TipoDocumentoTitular", SqlDbType.Int);
                varParametros[2].Value = tipoDocumentoTitular;
                varParametros[3] = new SqlParameter("@NumeroDocumentoTitular", SqlDbType.VarChar);
                varParametros[3].Value = numeroDocumentoTitular;
                varParametros[4] = new SqlParameter("@Genero", SqlDbType.Char);
                varParametros[4].Value = genero;
                varParametros[5] = new SqlParameter("@FechaNacimiento", SqlDbType.VarChar);
                varParametros[5].Value = fechaNacimiento;
                varParametros[6] = new SqlParameter("@Email", SqlDbType.VarChar);
                varParametros[6].Value = email;
                varParametros[7] = new SqlParameter("@TelefonoCelular", SqlDbType.VarChar);
                varParametros[7].Value = celular;
                varParametros[8] = new SqlParameter("@RUC", SqlDbType.Char);
                varParametros[8].Value = seguroPlanSalud;

                varParametros[9] = new SqlParameter("@Direccion", SqlDbType.VarChar, 200);
                varParametros[9].Value = direccion;
                varParametros[10] = new SqlParameter("@Numero", SqlDbType.VarChar, 50);
                varParametros[10].Value = numeroDireccion;
                varParametros[11] = new SqlParameter("@Departamento", SqlDbType.VarChar, 20);
                varParametros[11].Value = numeroDepartamento;
                varParametros[12] = new SqlParameter("@Referencia", SqlDbType.VarChar, 200);
                varParametros[12].Value = referencia;

                varParametros[13] = new SqlParameter("@LatLong", SqlDbType.VarChar, 50);
                varParametros[13].Value = latlong;

                varParametros[14] = new SqlParameter("@Nombres", SqlDbType.VarChar, 50);
                if (String.IsNullOrEmpty(nombres)) varParametros[14].Value = null;
                else varParametros[14].Value = nombres;
                varParametros[15] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar, 50);
                if (String.IsNullOrEmpty(apellidoPaterno)) varParametros[15].Value = null;
                else varParametros[15].Value = apellidoPaterno;
                varParametros[16] = new SqlParameter("@ApellidoMaterno", SqlDbType.VarChar, 50);
                if (String.IsNullOrEmpty(apellidoMaterno)) varParametros[16].Value = null;
                else varParametros[16].Value = apellidoMaterno;

                varParametros[17] = new SqlParameter("@TipoParentesco", SqlDbType.VarChar, 3);
                varParametros[17].Value = tipoParentesco;

                varParametros[18] = new SqlParameter("@NombreArchivoUsuario", SqlDbType.VarChar, 120);
                varParametros[18].Value = nombreArchivo;

                varParametros[19] = new SqlParameter("@NombreArchivoFisico", SqlDbType.VarChar, 80);
                varParametros[19].Value = nombreGenerado;

                varParametros[20] = new SqlParameter("@TelefonoFijo", SqlDbType.VarChar, 9);
                if (!String.IsNullOrEmpty(telefono))
                {
                    varParametros[20].Value = telefono;
                }
                else
                {
                    varParametros[20].Value = null;
                }
                varParametros[21] = new SqlParameter("@TipoPaciente", SqlDbType.VarChar, 3);
                if (!String.IsNullOrEmpty(tipoPaciente))
                {
                    varParametros[21].Value = tipoPaciente;
                }
                else
                {
                    varParametros[21].Value = null;
                }
                varParametros[22] = new SqlParameter("@Observaciones", SqlDbType.VarChar);
                if (!String.IsNullOrEmpty(observacion))
                {
                    varParametros[22].Value = observacion;
                }
                else
                {
                    varParametros[22].Value = null;
                }
                varParametros[23] = new SqlParameter("@PaisId", SqlDbType.VarChar, 3);
                if (!String.IsNullOrEmpty(idPais))
                {
                    varParametros[23].Value = idPais;
                }
                else
                {
                    varParametros[23].Value = null;
                }
                varParametros[24] = new SqlParameter("@DepartamentoId", SqlDbType.VarChar, 2);
                if (!String.IsNullOrEmpty(idDepartamento))
                {
                    varParametros[24].Value = idDepartamento;
                }
                else
                {
                    varParametros[24].Value = null;
                }
                varParametros[25] = new SqlParameter("@ProvinciaId", SqlDbType.VarChar, 2);
                if (!String.IsNullOrEmpty(idProvincia))
                {
                    varParametros[25].Value = idProvincia;
                }
                else
                {
                    varParametros[25].Value = null;
                }
                varParametros[26] = new SqlParameter("@DistritoId", SqlDbType.VarChar, 2);
                if (!String.IsNullOrEmpty(idDistrito))
                {
                    varParametros[26].Value = idDistrito;
                }
                else
                {
                    varParametros[26].Value = null;
                }
                varParametros[27] = new SqlParameter("@PaisIdNac", SqlDbType.VarChar, 3);
                if (!String.IsNullOrEmpty(idPaisNac))
                {
                    varParametros[27].Value = idPaisNac;
                }
                else
                {
                    varParametros[27].Value = null;
                }
                varParametros[28] = new SqlParameter("@DepartamentoIdNac", SqlDbType.VarChar, 2);
                if (!String.IsNullOrEmpty(idDepartamentoNac))
                {
                    varParametros[28].Value = idDepartamentoNac;
                }
                else
                {
                    varParametros[28].Value = null;
                }
                varParametros[29] = new SqlParameter("@ProvinciaIdNac", SqlDbType.VarChar, 2);
                if (!String.IsNullOrEmpty(idProvinciaNac))
                {
                    varParametros[29].Value = idProvinciaNac;
                }
                else
                {
                    varParametros[29].Value = null;
                }
                varParametros[30] = new SqlParameter("@DistritoIdNac", SqlDbType.VarChar, 2);
                if (!String.IsNullOrEmpty(idDistritoNac))
                {
                    varParametros[30].Value = idDistritoNac;
                }
                else
                {
                    varParametros[30].Value = null;
                }
                varParametros[31] = new SqlParameter("@FinalidadesAdiciona", SqlDbType.Bit);
                varParametros[31].Value = finalidadesAdiciona;
                varParametros[32] = new SqlParameter("@EsIntranet", SqlDbType.Bit);
                varParametros[32].Value = esIntranet;

                varConexion.EjecutarProcedimiento("App_Proc_Familiar_Modificar", varParametros, TipoProcesamiento.NonQuery);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public void EliminarFamiliar(string tipoDocumento, string numeroDocumento,
                                            string tipoDocumentoTitular, string numeroDocumentoTitular)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[4];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@TipoDocumentoTitular", SqlDbType.Int);
                varParametros[2].Value = tipoDocumentoTitular;
                varParametros[3] = new SqlParameter("@NumeroDocumentoTitular", SqlDbType.VarChar);
                varParametros[3].Value = numeroDocumentoTitular;

                varConexion.EjecutarProcedimiento("App_Proc_Familiar_Eliminar", varParametros, TipoProcesamiento.NonQuery);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public List<FamiliarBE> ConsultarFamiliar(string tipoDocumentoTitular, string numeroDocumentoTitular)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@TipoDocumentoTitular", SqlDbType.Int);
                varParametros[0].Value = tipoDocumentoTitular;
                varParametros[1] = new SqlParameter("@NumeroDocumentoTitular", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumentoTitular;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Familiar_Listar", varParametros, TipoProcesamiento.DataReader, false);

                //string rutaLocal = ConfigurationManager.AppSettings["RutaImagenes"].ToString();
                //string urlBase = ConfigurationManager.AppSettings["URLImagenes"].ToString();
                //string fotoPorDefecto = ConfigurationManager.AppSettings["FotoPorDefecto"].ToString();

                //string rutaPublica = ConfigurationManager.AppSettings["rutaPublicaUsuarioImagen"].ToString();
                string urlBase = ConfigurationManager.AppSettings["rutaPublicaUsuarioImagen"].ToString();

                List<FamiliarBE> varResultado = new List<FamiliarBE>();
                FamiliarBE oFamiliarBE;
                string nombreArchivoFisico, nombreArchivoUsuario;
                while (varDataReader.Read())
                {
                    oFamiliarBE = new FamiliarBE()
                    {
                        tipoDocumento = varDataReader.GetInt32(varDataReader.GetOrdinal("TipoDocumento")).ToString(),
                        numeroDocumento = varDataReader.GetString(varDataReader.GetOrdinal("NumeroDocumento")),
                        codigoTipoParentesco = varDataReader.GetString(varDataReader.GetOrdinal("CodigoTipoParentesco")),
                        tipoParentesco = varDataReader.GetString(varDataReader.GetOrdinal("TipoParentesco")),
                        nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
                        apellidoPaterno = varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaterno")),
                        apellidoMaterno = varDataReader.IsDBNull(varDataReader.GetOrdinal("ApellidoMaterno")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaterno")),
                        fechaNacimiento = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaNacimiento")).ToString("dd/MM/yyyy"),
                        telefono = varDataReader.IsDBNull(varDataReader.GetOrdinal("TelefonoFijo")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TelefonoFijo")),
                        celular = varDataReader.IsDBNull(varDataReader.GetOrdinal("TelefonoCelular")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TelefonoCelular")),
                        email = varDataReader.IsDBNull(varDataReader.GetOrdinal("Email")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Email")),
                        genero = varDataReader.GetString(varDataReader.GetOrdinal("Genero")),
                        nombreArchivoUsuario = (varDataReader.IsDBNull(varDataReader.GetOrdinal("NombreArchivoUsuario")) || varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoUsuario")).Equals("")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoUsuario")),
                        tipoPaciente = varDataReader.GetString(varDataReader.GetOrdinal("TipoPaciente")),
                        seguroPlanSaludCodigo = varDataReader.IsDBNull(varDataReader.GetOrdinal("RUCSeguro")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("RUCSeguro")),
                        
                        idPais = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdPais")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdPais")),
                        idDepartamento = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDepartamento")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDepartamento")),
                        idProvincia = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdProvincia")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdProvincia")),
                        idDistrito = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDistrito")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDistrito")),
                        pais = varDataReader.IsDBNull(varDataReader.GetOrdinal("Pais")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Pais")),
                        departamento = varDataReader.IsDBNull(varDataReader.GetOrdinal("Departamento")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Departamento")),
                        provincia = varDataReader.IsDBNull(varDataReader.GetOrdinal("Provincia")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Provincia")),
                        distrito = varDataReader.IsDBNull(varDataReader.GetOrdinal("Distrito")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Distrito")),
                        direccion = varDataReader.GetString(varDataReader.GetOrdinal("Direccion")),

                        idPaisNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdPaisNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdPaisNac")),
                        idDepartamentoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDepartamentoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDepartamentoNac")),
                        idProvinciaNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdProvinciaNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdProvinciaNac")),
                        idDistritoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDistritoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDistritoNac")),
                        paisNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("PaisNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("PaisNac")),
                        departamentoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("DepartamentoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("DepartamentoNac")),
                        provinciaNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("ProvinciaNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("ProvinciaNac")),
                        distritoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("DistritoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("DistritoNac")),

                        observacion = varDataReader.IsDBNull(varDataReader.GetOrdinal("Observacion")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Observacion")),
                        finalidadesAdiciona = varDataReader.GetBoolean(varDataReader.GetOrdinal("FinalidadesAdiciona")),
                        idAmbulatorio = varDataReader.GetString(varDataReader.GetOrdinal("IdAmbulatorio")),
                        esRENIEC = varDataReader.GetBoolean(varDataReader.GetOrdinal("esRENIEC")),
                        iconoEsRENIEC = varDataReader.GetString(varDataReader.GetOrdinal("IconoEsRENIEC")),
                        textoEsRENIEC = varDataReader.GetString(varDataReader.GetOrdinal("TextoEsRENIEC"))

                        //foto = File.Exists(Path.Combine(rutaLocal, (varDataReader.GetInt32(varDataReader.GetOrdinal("TipoDocumento")).ToString() + "_" + varDataReader.GetString(varDataReader.GetOrdinal("NumeroDocumento"))) + ".jpg")) ? (urlBase + (varDataReader.GetInt32(varDataReader.GetOrdinal("TipoDocumento")).ToString() + "_" + varDataReader.GetString(varDataReader.GetOrdinal("NumeroDocumento"))) + ".jpg")
                        //                : File.Exists(Path.Combine(rutaLocal, (varDataReader.GetInt32(varDataReader.GetOrdinal("TipoDocumento")).ToString() + "_" + varDataReader.GetString(varDataReader.GetOrdinal("NumeroDocumento"))) + ".png")) ? (urlBase + (varDataReader.GetInt32(varDataReader.GetOrdinal("TipoDocumento")).ToString() + "_" + varDataReader.GetString(varDataReader.GetOrdinal("NumeroDocumento"))) + ".png")
                        //                : (urlBase + fotoPorDefecto),
                        //numeroDireccion = varDataReader.GetString(varDataReader.GetOrdinal("NumeroDireccion")),
                        //numeroDepartamento = varDataReader.GetString(varDataReader.GetOrdinal("NumeroDepartamento")),
                        //referencia = varDataReader.GetString(varDataReader.GetOrdinal("Referencia")),
                        //idUbigeo = varDataReader.GetString(varDataReader.GetOrdinal("UbigeoId")),
                        //latlong = varDataReader.GetString(varDataReader.GetOrdinal("LatLong")),
                        //ubigeoDescripcion = varDataReader.GetString(varDataReader.GetOrdinal("UbigeoDescripcion")),
                        //imagen = (varDataReader.IsDBNull(varDataReader.GetOrdinal("NombreArchivoFisico")) || varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoFisico")).Equals("")) ? null : (Path.Combine(rutaPublica, varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoFisico")))),
                    };
                    nombreArchivoUsuario = (varDataReader.IsDBNull(varDataReader.GetOrdinal("NombreArchivoUsuario")) || varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoUsuario")).Equals("")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoUsuario"));
                    nombreArchivoFisico = varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoFisico"));
                    oFamiliarBE.foto = urlBase + (!String.IsNullOrEmpty(nombreArchivoFisico) ? nombreArchivoFisico : (oFamiliarBE.genero.Equals("M") ? "user-hombre-azul.png" : "user-mujer-azul.png"));
                    oFamiliarBE.nombreArchivoUsuario = !String.IsNullOrEmpty(nombreArchivoUsuario) ? nombreArchivoUsuario : (oFamiliarBE.genero.Equals("M") ? "user-hombre-azul.png" : "user-mujer-azul.png");
                    varResultado.Add(oFamiliarBE);
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

        public string ObtenerSeguro(string tipoDocumento, string numeroDocumento)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;

                object varRespuesta = varConexion.EjecutarProcedimiento("App_Proc_Usuario_ObtenerSeguro", varParametros, TipoProcesamiento.Scalar);
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


        public void CrearPacienteSpring(string idClinica, string tipoDocumento, string numeroDocumento, string idPacienteSpring, string idPacienteSpringOriginal)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[5];
                varParametros[0] = new SqlParameter("@IDClinica", SqlDbType.VarChar);
                varParametros[0].Value = idClinica;
                varParametros[1] = new SqlParameter("@TipoDocumento", SqlDbType.VarChar);
                varParametros[1].Value = tipoDocumento;
                varParametros[2] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[2].Value = numeroDocumento;
                varParametros[3] = new SqlParameter("@IDPacienteSpring", SqlDbType.Int);
                varParametros[3].Value = idPacienteSpring;
                varParametros[4] = new SqlParameter("@IDPacienteSpringOriginal", SqlDbType.Int);
                varParametros[4].Value = idPacienteSpringOriginal;



                varConexion.EjecutarProcedimiento("App_Proc_Usuario_CrearSpring", varParametros, TipoProcesamiento.NonQuery);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }
        public UsuarioBE DatosUsuario(string idCita, string idCitaVirtual, string tipoDocumento, string numeroDocumento, string idHorario)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                string tipo = (idCita != null) ? "p" : "v";
                string id = (idCita != null) ? idCita : idCitaVirtual;

                SqlParameter[] varParametros = new SqlParameter[5];
                varParametros[0] = new SqlParameter("@IDCita", SqlDbType.Int);
                varParametros[0].Value = id;
                varParametros[1] = new SqlParameter("@Tipo", SqlDbType.VarChar);
                varParametros[1].Value = tipo;
                varParametros[2] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[2].Value = tipoDocumento;
                varParametros[3] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[3].Value = numeroDocumento;
                varParametros[4] = new SqlParameter("@IDHorario", SqlDbType.Int);
                varParametros[4].Value = idHorario;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Usuario_Datos", varParametros, TipoProcesamiento.DataReader, false);

                varDataReader.Read();

                return new UsuarioBE()
                {
                    nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
                    apellidoPaterno = varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaterno")),
                    apellidoMaterno = varDataReader.IsDBNull(varDataReader.GetOrdinal("ApellidoMaterno")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaterno")),
                    tipoDocumento = varDataReader.GetInt32(varDataReader.GetOrdinal("TipoDocumento")).ToString(),
                    numeroDocumento = varDataReader.GetString(varDataReader.GetOrdinal("NumeroDocumento")).ToString(),
                    rucSeguro = varDataReader.IsDBNull(varDataReader.GetOrdinal("RUCSeguro")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("RUCSeguro")),
                    tipoPago = varDataReader.IsDBNull(varDataReader.GetOrdinal("TipoPago")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TipoPago")),
                    idClinica = varDataReader.GetInt32(varDataReader.GetOrdinal("IDClinica")).ToString(),
                    monto = varDataReader.IsDBNull(varDataReader.GetOrdinal("Monto")) ? null : varDataReader.GetDecimal(varDataReader.GetOrdinal("Monto")).ToString("0.00"),
                    rucSeguroPrograma = varDataReader.IsDBNull(varDataReader.GetOrdinal("RUCSeguroPrograma")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("RUCSeguroPrograma")),
                    beneficio = varDataReader.IsDBNull(varDataReader.GetOrdinal("Beneficio")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Beneficio"))

                };
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
        public bool EncuestaPacienteCovid(string tipoDocumento, string numeroDocumento)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {

                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Usuario_Covid", varParametros, TipoProcesamiento.DataReader, false);

                varDataReader.Read();

                bool pacienteCovid = varDataReader.GetBoolean(varDataReader.GetOrdinal("PacienteCovid"));

                return pacienteCovid;
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
        public UsuarioFamiliaresBE DatosUsuarioFamiliares(string tipoDocumento, string numeroDocumento)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {

                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@TipoDocumentoTitular", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumentoTitular", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Usuario_Familiares", varParametros, TipoProcesamiento.DataReader, false);

                varDataReader.Read();

                string urlBase = ConfigurationManager.AppSettings["rutaPublicaUsuarioImagen"];
                UsuarioFamiliaresBE rpta = new UsuarioFamiliaresBE();
                UsuarioDatosBE oUsuarioDatosBE;
                string nombreArchivoFisico, nombreArchivoUsuario;
                rpta.titular = new UsuarioDatosBE()
                {
                    nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
                    apellidoPaterno = varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaterno")),
                    apellidoMaterno = varDataReader.IsDBNull(varDataReader.GetOrdinal("ApellidoMaterno")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaterno")),
                    tipoDocumento = varDataReader.GetInt32(varDataReader.GetOrdinal("TipoDocumento")).ToString(),
                    numeroDocumento = varDataReader.GetString(varDataReader.GetOrdinal("NumeroDocumento")).ToString(),
                    genero = varDataReader.GetString(varDataReader.GetOrdinal("Genero")).ToString(),
                    fechaNacimiento = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaNacimiento")).ToString("dd/MM/yyyy"),
                    email = varDataReader.IsDBNull(varDataReader.GetOrdinal("Email")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Email")),
                    celular = varDataReader.IsDBNull(varDataReader.GetOrdinal("TelefonoCelular")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TelefonoCelular")),
                    seguroPlanSaludCodigo = varDataReader.IsDBNull(varDataReader.GetOrdinal("RUCSeguro")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("RUCSeguro")),
                    direccion = varDataReader.IsDBNull(varDataReader.GetOrdinal("Direccion")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Direccion")),
                    referencia = varDataReader.IsDBNull(varDataReader.GetOrdinal("Referencia")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Referencia")),
                    ciudad = varDataReader.IsDBNull(varDataReader.GetOrdinal("Ciudad")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Ciudad")),
                    numeroDireccion = varDataReader.IsDBNull(varDataReader.GetOrdinal("NumeroDireccion")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("NumeroDireccion")),
                    numeroDepartamento = varDataReader.IsDBNull(varDataReader.GetOrdinal("NumeroDepartamento")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("NumeroDepartamento")),
                    urbanizacion = varDataReader.IsDBNull(varDataReader.GetOrdinal("Urbanizacion")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Urbanizacion")),
                    direccionCompleta = varDataReader.IsDBNull(varDataReader.GetOrdinal("DireccionCompleta")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("DireccionCompleta")),
                    ubigeoId = varDataReader.IsDBNull(varDataReader.GetOrdinal("UbigeoId")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("UbigeoId")),
                    tipoParentesco = varDataReader.IsDBNull(varDataReader.GetOrdinal("TipoParentesco")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TipoParentesco")),
                    //imagen = (varDataReader.IsDBNull(varDataReader.GetOrdinal("NombreArchivoFisico")) || varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoFisico")).Equals("")) ? null : (Path.Combine(rutaPublica, varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoFisico")))),
                    //nombreArchivoUsuario = (varDataReader.IsDBNull(varDataReader.GetOrdinal("NombreArchivoUsuario")) || varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoUsuario")).Equals("")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoUsuario"))
                    idPais = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdPais")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdPais")),
                    idDepartamento = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDepartamento")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDepartamento")),
                    idProvincia = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdProvincia")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdProvincia")),
                    idDistrito = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDistrito")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDistrito")),
                    pais = varDataReader.IsDBNull(varDataReader.GetOrdinal("Pais")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Pais")),
                    departamento = varDataReader.IsDBNull(varDataReader.GetOrdinal("Departamento")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Departamento")),
                    provincia = varDataReader.IsDBNull(varDataReader.GetOrdinal("Provincia")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Provincia")),
                    distrito = varDataReader.IsDBNull(varDataReader.GetOrdinal("Distrito")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Distrito")),
                    
                    idPaisNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdPaisNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdPaisNac")),
                    idDepartamentoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDepartamentoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDepartamentoNac")),
                    idProvinciaNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdProvinciaNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdProvinciaNac")),
                    idDistritoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDistritoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDistritoNac")),
                    paisNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("PaisNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("PaisNac")),
                    departamentoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("DepartamentoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("DepartamentoNac")),
                    provinciaNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("ProvinciaNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("ProvinciaNac")),
                    distritoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("DistritoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("DistritoNac")),

                    telefono = varDataReader.IsDBNull(varDataReader.GetOrdinal("TelefonoFijo")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TelefonoFijo")),
                    tipoPaciente = varDataReader.IsDBNull(varDataReader.GetOrdinal("TipoPaciente")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TipoPaciente")),
                    observacion = varDataReader.IsDBNull(varDataReader.GetOrdinal("Observacion")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Observacion")),
                    idAmbulatorio = varDataReader.GetString(varDataReader.GetOrdinal("IdAmbulatorio")),
                    esRENIEC = varDataReader.GetBoolean(varDataReader.GetOrdinal("esRENIEC")),
                    iconoEsRENIEC = varDataReader.GetString(varDataReader.GetOrdinal("IconoEsRENIEC")),
                    textoEsRENIEC = varDataReader.GetString(varDataReader.GetOrdinal("TextoEsRENIEC"))
                };
                nombreArchivoUsuario = (varDataReader.IsDBNull(varDataReader.GetOrdinal("NombreArchivoUsuario")) || varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoUsuario")).Equals("")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoUsuario"));
                nombreArchivoFisico = varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoFisico"));
                rpta.titular.foto = urlBase + (!String.IsNullOrEmpty(nombreArchivoFisico) ? nombreArchivoFisico : (rpta.titular.genero.Equals("M") ? "user-hombre-azul.png" : "user-mujer-azul.png"));
                rpta.titular.nombreArchivoUsuario = !String.IsNullOrEmpty(nombreArchivoUsuario) ? nombreArchivoUsuario : (rpta.titular.genero.Equals("M") ? "user-hombre-azul.png" : "user-mujer-azul.png");

                varDataReader.NextResult();
                rpta.familiares = new List<UsuarioDatosBE>();
                while (varDataReader.Read())
                {
                    oUsuarioDatosBE = new UsuarioDatosBE()
                    {
                        nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
                        apellidoPaterno = varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaterno")),
                        apellidoMaterno = varDataReader.IsDBNull(varDataReader.GetOrdinal("ApellidoMaterno")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaterno")),
                        tipoDocumento = varDataReader.GetInt32(varDataReader.GetOrdinal("TipoDocumento")).ToString(),
                        numeroDocumento = varDataReader.GetString(varDataReader.GetOrdinal("NumeroDocumento")).ToString(),
                        genero = varDataReader.GetString(varDataReader.GetOrdinal("Genero")).ToString(),
                        fechaNacimiento = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaNacimiento")).ToString("dd/MM/yyyy"),
                        email = varDataReader.IsDBNull(varDataReader.GetOrdinal("Email")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Email")),
                        celular = varDataReader.IsDBNull(varDataReader.GetOrdinal("TelefonoCelular")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TelefonoCelular")),
                        seguroPlanSaludCodigo = varDataReader.IsDBNull(varDataReader.GetOrdinal("RUCSeguro")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("RUCSeguro")),
                        direccion = varDataReader.IsDBNull(varDataReader.GetOrdinal("Direccion")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Direccion")),
                        
                        referencia = varDataReader.IsDBNull(varDataReader.GetOrdinal("Referencia")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Referencia")),
                        ciudad = varDataReader.IsDBNull(varDataReader.GetOrdinal("Ciudad")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Ciudad")),
                        numeroDireccion = varDataReader.IsDBNull(varDataReader.GetOrdinal("NumeroDireccion")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("NumeroDireccion")),
                        numeroDepartamento = varDataReader.IsDBNull(varDataReader.GetOrdinal("NumeroDepartamento")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("NumeroDepartamento")),
                        urbanizacion = varDataReader.IsDBNull(varDataReader.GetOrdinal("Urbanizacion")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Urbanizacion")),
                        direccionCompleta = varDataReader.IsDBNull(varDataReader.GetOrdinal("DireccionCompleta")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("DireccionCompleta")),
                        ubigeoId = varDataReader.IsDBNull(varDataReader.GetOrdinal("UbigeoId")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("UbigeoId")),
                        tipoParentesco = varDataReader.IsDBNull(varDataReader.GetOrdinal("TipoParentesco")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TipoParentesco")),
                        //imagen = (varDataReader.IsDBNull(varDataReader.GetOrdinal("NombreArchivoFisico")) || varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoFisico")).Equals("")) ? null : (Path.Combine(rutaPublica, varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoFisico")))),
                        //nombreArchivoUsuario = (varDataReader.IsDBNull(varDataReader.GetOrdinal("NombreArchivoUsuario")) || varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoUsuario")).Equals("")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoUsuario"))
                        idPais = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdPais")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdPais")),
                        idDepartamento = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDepartamento")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDepartamento")),
                        idProvincia = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdProvincia")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdProvincia")),
                        idDistrito = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDistrito")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDistrito")),
                        pais = varDataReader.IsDBNull(varDataReader.GetOrdinal("Pais")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Pais")),
                        departamento = varDataReader.IsDBNull(varDataReader.GetOrdinal("Departamento")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Departamento")),
                        provincia = varDataReader.IsDBNull(varDataReader.GetOrdinal("Provincia")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Provincia")),
                        distrito = varDataReader.IsDBNull(varDataReader.GetOrdinal("Distrito")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Distrito")),

                        idPaisNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdPaisNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdPaisNac")),
                        idDepartamentoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDepartamentoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDepartamentoNac")),
                        idProvinciaNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdProvinciaNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdProvinciaNac")),
                        idDistritoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDistritoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDistritoNac")),
                        paisNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("PaisNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("PaisNac")),
                        departamentoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("DepartamentoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("DepartamentoNac")),
                        provinciaNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("ProvinciaNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("ProvinciaNac")),
                        distritoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("DistritoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("DistritoNac")),

                        telefono = varDataReader.IsDBNull(varDataReader.GetOrdinal("TelefonoFijo")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TelefonoFijo")),
                        tipoPaciente = varDataReader.IsDBNull(varDataReader.GetOrdinal("TipoPaciente")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TipoPaciente")),
                        observacion = varDataReader.IsDBNull(varDataReader.GetOrdinal("Observacion")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Observacion")),
                        idAmbulatorio = varDataReader.GetString(varDataReader.GetOrdinal("IdAmbulatorio")),
                        esRENIEC = varDataReader.GetBoolean(varDataReader.GetOrdinal("esRENIEC")),
                        iconoEsRENIEC = varDataReader.GetString(varDataReader.GetOrdinal("IconoEsRENIEC")),
                        textoEsRENIEC = varDataReader.GetString(varDataReader.GetOrdinal("TextoEsRENIEC"))
                    };
                    nombreArchivoUsuario = (varDataReader.IsDBNull(varDataReader.GetOrdinal("NombreArchivoUsuario")) || varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoUsuario")).Equals("")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoUsuario"));
                    nombreArchivoFisico = varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoFisico"));
                    oUsuarioDatosBE.foto = urlBase + (!String.IsNullOrEmpty(nombreArchivoFisico) ? nombreArchivoFisico : (oUsuarioDatosBE.genero.Equals("M") ? "user-hombre-azul.png" : "user-mujer-azul.png"));
                    oUsuarioDatosBE.nombreArchivoUsuario = !String.IsNullOrEmpty(nombreArchivoUsuario) ? nombreArchivoUsuario : (oUsuarioDatosBE.genero.Equals("M") ? "user-hombre-azul.png" : "user-mujer-azul.png");
                    rpta.familiares.Add(oUsuarioDatosBE);
                }

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

        public void ActualizarCorreoUsuario(String tipoDocumento, String numeroDocumento, string email)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[3];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@Email", SqlDbType.VarChar);
                varParametros[2].Value = email;

                varConexion.EjecutarProcedimiento("App_Proc_Usuario_ActualizarCorreo", varParametros, TipoProcesamiento.NonQuery);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public string EliminarCuenta(string tipoDocumento, string numeroDocumento, string origen)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[3];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@Origen", SqlDbType.VarChar);
                varParametros[2].Value = origen;

                return varConexion.EjecutarProcedimiento("App_Proc_Usuario_Eliminar_Cuenta", varParametros, TipoProcesamiento.Scalar).ToString();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public string CifrarClave(string clave)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@Clave", SqlDbType.VarChar);
                varParametros[0].Value = clave;


                return varConexion.EjecutarProcedimiento("App_Proc_CifrarClave", varParametros, TipoProcesamiento.Scalar).ToString();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public UsuarioBE ObtenerDatos(string tipoDocumento, string numeroDocumento)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.VarChar);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Usuario_ObtenerDatos", varParametros, TipoProcesamiento.DataReader, false);
                UsuarioBE varRespuesta = null;
                if (varDataReader != null)
                {
                    if (varDataReader.Read())
                    {
                        //string rutaLocal = ConfigurationManager.AppSettings["RutaImagenes"].ToString();
                        //string urlBase = ConfigurationManager.AppSettings["URLImagenes"].ToString();
                        //string nombreImagen = tipoDocumento + "_" + numeroDocumento;
                        //string fotoPorDefecto = ConfigurationManager.AppSettings["FotoPorDefecto"].ToString();
                        string urlBase = ConfigurationManager.AppSettings["rutaPublicaUsuarioImagen"].ToString();

                        varRespuesta = new UsuarioBE()
                        {
                            tipoDocumento = tipoDocumento,
                            numeroDocumento = numeroDocumento,
                            nombres = varDataReader.GetString(varDataReader.GetOrdinal("Nombres")),
                            apellidoPaterno = varDataReader.GetString(varDataReader.GetOrdinal("ApellidoPaterno")),
                            apellidoMaterno = varDataReader.IsDBNull(varDataReader.GetOrdinal("ApellidoMaterno")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("ApellidoMaterno")),
                            fechaNacimiento = varDataReader.GetDateTime(varDataReader.GetOrdinal("FechaNacimiento")).ToString("dd/MM/yyyy"),
                            telefono = varDataReader.IsDBNull(varDataReader.GetOrdinal("TelefonoFijo")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TelefonoFijo")),
                            celular = varDataReader.IsDBNull(varDataReader.GetOrdinal("TelefonoCelular")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TelefonoCelular")),
                            email = varDataReader.IsDBNull(varDataReader.GetOrdinal("Email")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Email")),
                            genero = varDataReader.GetString(varDataReader.GetOrdinal("Genero")),

                            tipoPaciente = varDataReader.IsDBNull(varDataReader.GetOrdinal("TipoPaciente")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("TipoPaciente")),
                            seguroPlanSaludCodigo = varDataReader.IsDBNull(varDataReader.GetOrdinal("RUCSeguro")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("RUCSeguro")),
                            tipoUsuario = varDataReader.GetString(varDataReader.GetOrdinal("TipoUsuario")),
                            cmp = varDataReader.GetString(varDataReader.GetOrdinal("CMP")),
                            esMedico = (varDataReader.GetString(varDataReader.GetOrdinal("TipoUsuario")) == "PACIENTE") ? false : true,

                            idPais = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdPais")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdPais")),
                            idDepartamento = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDepartamento")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDepartamento")),
                            idProvincia = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdProvincia")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdProvincia")),
                            idDistrito = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDistrito")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDistrito")),
                            pais = varDataReader.IsDBNull(varDataReader.GetOrdinal("Pais")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Pais")),
                            departamento = varDataReader.IsDBNull(varDataReader.GetOrdinal("Departamento")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Departamento")),
                            provincia = varDataReader.IsDBNull(varDataReader.GetOrdinal("Provincia")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Provincia")),
                            distrito = varDataReader.IsDBNull(varDataReader.GetOrdinal("Distrito")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Distrito")),

                            idPaisNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdPaisNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdPaisNac")),
                            idDepartamentoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDepartamentoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDepartamentoNac")),
                            idProvinciaNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdProvinciaNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdProvinciaNac")),
                            idDistritoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("IdDistritoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("IdDistritoNac")),
                            paisNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("PaisNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("PaisNac")),
                            departamentoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("DepartamentoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("DepartamentoNac")),
                            provinciaNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("ProvinciaNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("ProvinciaNac")),
                            distritoNac = varDataReader.IsDBNull(varDataReader.GetOrdinal("DistritoNac")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("DistritoNac")),

                            observacion = varDataReader.IsDBNull(varDataReader.GetOrdinal("Observacion")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Observacion")),
                            direccion = varDataReader.IsDBNull(varDataReader.GetOrdinal("Direccion")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("Direccion")),
                            finalidadesAdiciona = varDataReader.GetBoolean(varDataReader.GetOrdinal("FinalidadesAdiciona")),
                            idAmbulatorio = varDataReader.GetString(varDataReader.GetOrdinal("IdAmbulatorio"))

                        };
                        string nombreArchivoFisico = varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoFisico"));
                        string nombreArchivoUsuario = (varDataReader.IsDBNull(varDataReader.GetOrdinal("NombreArchivoUsuario")) || varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoUsuario")).Equals("")) ? null : varDataReader.GetString(varDataReader.GetOrdinal("NombreArchivoUsuario"));
                        varRespuesta.foto = urlBase + (!String.IsNullOrEmpty(nombreArchivoFisico) ? nombreArchivoFisico : (varRespuesta.genero.Equals("M") ? "user-hombre-azul.png" : "user-mujer-azul.png"));
                        varRespuesta.nombreArchivoUsuario = !String.IsNullOrEmpty(nombreArchivoUsuario) ? nombreArchivoUsuario : (varRespuesta.genero.Equals("M") ? "user-hombre-azul.png" : "user-mujer-azul.png");
                    }
                }

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

        public UsuarioDocumentoBE ObtenerPorToken(string tokenSesion, string tipoDocumento, string numeroDocumento, bool validarFamiliar = false)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[4];
                varParametros[0] = new SqlParameter("@TokenSesion", SqlDbType.VarChar);
                varParametros[0].Value = tokenSesion;
                varParametros[1] = new SqlParameter("@tnTipoDocumentoPaciente", SqlDbType.Int);
                if(!String.IsNullOrEmpty(tipoDocumento)) varParametros[1].Value = tipoDocumento;
                else varParametros[1].Value = null;
                varParametros[2] = new SqlParameter("@tvNumeroDocumentoPaciente", SqlDbType.VarChar);
                if (!String.IsNullOrEmpty(numeroDocumento)) varParametros[2].Value = numeroDocumento;
                else varParametros[2].Value = null;
                varParametros[3] = new SqlParameter("@tlValidarFamiliar", SqlDbType.Bit);
                varParametros[3].Value = validarFamiliar;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Usuario_ObtenerPorToken", varParametros, TipoProcesamiento.DataReader, false);

                UsuarioDocumentoBE varRespuesta = null;
                varDataReader.Read();
                varRespuesta = new UsuarioDocumentoBE()
                {
                    tipoDocumento = varDataReader.GetString(varDataReader.GetOrdinal("TipoDocumento")),
                    numeroDocumento = varDataReader.GetString(varDataReader.GetOrdinal("NumeroDocumento")),
                    indicadorInvitado = varDataReader.GetBoolean(varDataReader.GetOrdinal("IndicadorInvitado"))
                };
                if (varRespuesta.indicadorInvitado)
                {
                    varRespuesta.tipoDocumento = tipoDocumento;
                    varRespuesta.numeroDocumento = numeroDocumento;
                }
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

        public UsuarioDatosContactoMFABE ObtenerDatosContactoMFA(int codigoEmpresa, int codigoAplicacion, int tipoDocumento, 
            string numeroDocumento, string dispositivo, string tokenAutenticacion, string usuario)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[7];
                varParametros[0] = new SqlParameter("@tnCodigoEmpresa", SqlDbType.Int);
                varParametros[0].Value = codigoEmpresa;
                varParametros[1] = new SqlParameter("@tnCodigoAplicativo", SqlDbType.Int);
                varParametros[1].Value = codigoAplicacion;
                varParametros[2] = new SqlParameter("@tnTipoDocumento", SqlDbType.Int);
                varParametros[2].Value = tipoDocumento;
                varParametros[3] = new SqlParameter("@tvNumeroDocumento", SqlDbType.VarChar);
                varParametros[3].Value = numeroDocumento;
                varParametros[4] = new SqlParameter("@tvCodigoDispositivo", SqlDbType.VarChar);
                varParametros[4].Value = dispositivo;
                varParametros[5] = new SqlParameter("@tvTokenAutenticacion", SqlDbType.VarChar);
                varParametros[5].Value = tokenAutenticacion;
                varParametros[6] = new SqlParameter("@tvCodigoUsuario", SqlDbType.VarChar);
                varParametros[6].Value = usuario;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("Sp_UsuarioContactoPorTokenMFA_Consulta", varParametros, TipoProcesamiento.DataReader, false);

                UsuarioDatosContactoMFABE varRespuesta = null;
                if (varDataReader != null)
                {
                    if (varDataReader.Read())
                    {
                        varRespuesta = new UsuarioDatosContactoMFABE()
                        {
                            celular = varDataReader.GetString(varDataReader.GetOrdinal("Celular")),
                            correo = varDataReader.GetString(varDataReader.GetOrdinal("Correo"))
                        };
                    }
                }
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

        public void GrabarTokenMFA(string codigoEmpresa, string codigoAplicacion, string codigoUsuario,
                                            string codigoDispositivo, string tokenAutenticacion, DateTime fechaExpiracion,
                                            string tipoDocumento, string numeroDocumento)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[8];
                varParametros[0] = new SqlParameter("@tnCodigoEmpresa", SqlDbType.Int);
                varParametros[0].Value = codigoEmpresa;
                varParametros[1] = new SqlParameter("@tnCodigoAplicativo", SqlDbType.Int);
                varParametros[1].Value = codigoAplicacion;
                varParametros[2] = new SqlParameter("@tvCodigoUsuario", SqlDbType.VarChar);
                varParametros[2].Value = codigoUsuario;
                varParametros[3] = new SqlParameter("@tvCodigoDispositivo", SqlDbType.VarChar);
                varParametros[3].Value = codigoDispositivo;
                varParametros[4] = new SqlParameter("@tvTokenAutenticacion", SqlDbType.VarChar);
                varParametros[4].Value = tokenAutenticacion;
                varParametros[5] = new SqlParameter("@tdFechaExpiracion", SqlDbType.DateTime);
                varParametros[5].Value = fechaExpiracion;
                varParametros[6] = new SqlParameter("@tnTipoDocumento", SqlDbType.Int);
                varParametros[6].Value = tipoDocumento;
                varParametros[7] = new SqlParameter("@tvNumeroDocumento", SqlDbType.VarChar);
                varParametros[7].Value = numeroDocumento;

                varConexion.EjecutarProcedimiento("Sp_TokenMFA_Update", varParametros, TipoProcesamiento.NonQuery);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }
        public RespuestaSimpleBE ValidarClave(string tipoDocumento, string numeroDocumento, string passwordSinCifrar)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[3];
                varParametros[0] = new SqlParameter("@tvTipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@tvNumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@tvPasswordSinCifrar", SqlDbType.VarChar);
                varParametros[2].Value = passwordSinCifrar;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("Sp_Usuario_ValidarClave", varParametros, TipoProcesamiento.DataReader, false);

                RespuestaSimpleBE varRespuesta = null;
                varDataReader.Read();
                varRespuesta = new RespuestaSimpleBE()
                {
                    rpt = varDataReader.GetInt32(varDataReader.GetOrdinal("CodigoError")),
                    mensaje = varDataReader.GetString(varDataReader.GetOrdinal("MensajeError"))
                };
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
    }
}
