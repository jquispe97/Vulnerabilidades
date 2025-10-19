using CSF.CITASWEB.WS.BE;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CSF.CITASWEB.WS.DA
{
    public class RoyalDA
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
        private SqlDataReader varDataReader;
        
        public bool GrabarDatosGenerales(string NumeroAutorizacion, string ApellidoMaternoAfiliado, string ApellidoMaternoTitular, string ApellidoPaternoAfiliado, string ApellidoPaternoTitular, string CodEstado, string CodEstadoCivil,
            string CodFechaActualizacionFoto, string CodGenero, string CodMoneda, string CodProducto, string CodTipoAfiliacion, string CodTipoDocumentoAfiliado, string CodTipoDocumentoContratante, string CodTipoDocumentoTitular, string CodigoAfiliado
            ,string CodigoTitular, string DesEstado, string DesProducto, string Edad, string FechaFinVigencia, string FechaInicioVigencia, string FechaNacimiento, string NombreContratante, string NombresAfiliado, string NombresTitular,
            string NumeroCertificado, string NumeroDocumentoAfiliado, string NumeroDocumentoContratante ,string NumeroDocumentoTitular, string NumeroPlan, string NumeroPoliza, string IdReceptor, string iafas
            , string CodParentesco, string RUClinica, string FechaAfiliacion,string Tarjeta , string NumeroContrato,  string CondicionesEspeciales, string Observaciones, int idClinica)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[42];
                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@ApellidoMaternoAfiliado", SqlDbType.VarChar);
                varParametros[1].Value = ApellidoMaternoAfiliado;
                varParametros[2] = new SqlParameter("@ApellidoMaternoTitular", SqlDbType.VarChar);
                varParametros[2].Value = ApellidoMaternoTitular;
                varParametros[3] = new SqlParameter("@ApellidoPaternoAfiliado", SqlDbType.VarChar);
                varParametros[3].Value = ApellidoPaternoAfiliado;
                varParametros[4] = new SqlParameter("@ApellidoPaternoTitular", SqlDbType.VarChar);
                varParametros[4].Value = ApellidoPaternoTitular;
                varParametros[5] = new SqlParameter("@CodEstado", SqlDbType.VarChar);
                varParametros[5].Value = CodEstado;
                varParametros[6] = new SqlParameter("@CodEstadoCivil", SqlDbType.VarChar);
                varParametros[6].Value = CodEstadoCivil;
                varParametros[7] = new SqlParameter("@CodFechaActualizacionFoto", SqlDbType.VarChar);
                varParametros[7].Value = CodFechaActualizacionFoto;
                varParametros[8] = new SqlParameter("@CodGenero", SqlDbType.VarChar);
                varParametros[8].Value = CodGenero;
                varParametros[9] = new SqlParameter("@CodMoneda", SqlDbType.VarChar);
                varParametros[9].Value = CodMoneda;
                varParametros[10] = new SqlParameter("@CodProducto", SqlDbType.VarChar);
                varParametros[10].Value = CodProducto;
                varParametros[11] = new SqlParameter("@CodTipoAfiliacion", SqlDbType.VarChar);
                varParametros[11].Value = CodTipoAfiliacion;
                varParametros[12] = new SqlParameter("@CodTipoDocumentoAfiliado", SqlDbType.VarChar);
                varParametros[12].Value = CodTipoDocumentoAfiliado;
                varParametros[13] = new SqlParameter("@CodTipoDocumentoContratante", SqlDbType.VarChar);
                varParametros[13].Value = CodTipoDocumentoContratante;
                varParametros[14] = new SqlParameter("@CodTipoDocumentoTitular", SqlDbType.VarChar);
                varParametros[14].Value = CodTipoDocumentoTitular;
                varParametros[15] = new SqlParameter("@CodigoAfiliado", SqlDbType.VarChar);
                varParametros[15].Value = CodigoAfiliado;
                varParametros[16] = new SqlParameter("@CodigoTitular", SqlDbType.VarChar);
                varParametros[16].Value = CodigoTitular;
                varParametros[17] = new SqlParameter("@DesEstado", SqlDbType.VarChar);
                varParametros[17].Value = DesEstado;
                varParametros[18] = new SqlParameter("@DesProducto", SqlDbType.VarChar);
                varParametros[18].Value = DesProducto;
                varParametros[19] = new SqlParameter("@Edad", SqlDbType.SmallInt);
                varParametros[19].Value = Edad;
                varParametros[20] = new SqlParameter("@FechaFinVigencia", SqlDbType.VarChar);
                varParametros[20].Value = FechaFinVigencia;
                varParametros[21] = new SqlParameter("@FechaInicioVigencia", SqlDbType.VarChar);
                varParametros[21].Value = FechaInicioVigencia;
                varParametros[22] = new SqlParameter("@FechaNacimiento", SqlDbType.VarChar);
                varParametros[22].Value = FechaNacimiento;
                varParametros[23] = new SqlParameter("@NombreContratante", SqlDbType.VarChar);
                varParametros[23].Value = NombreContratante;
                varParametros[24] = new SqlParameter("@NombresAfiliado", SqlDbType.VarChar);
                varParametros[24].Value = NombresAfiliado;
                varParametros[25] = new SqlParameter("@NombresTitular", SqlDbType.VarChar);
                varParametros[25].Value = NombresTitular;
                varParametros[26] = new SqlParameter("@NumeroCertificado", SqlDbType.VarChar);
                varParametros[26].Value = NumeroCertificado;
                varParametros[27] = new SqlParameter("@NumeroDocumentoAfiliado", SqlDbType.VarChar);
                varParametros[27].Value = NumeroDocumentoAfiliado;
                varParametros[28] = new SqlParameter("@NumeroDocumentoContratante", SqlDbType.VarChar);
                varParametros[28].Value = NumeroDocumentoContratante;
                varParametros[29] = new SqlParameter("@NumeroDocumentoTitular", SqlDbType.VarChar);
                varParametros[29].Value = NumeroDocumentoTitular;
                varParametros[30] = new SqlParameter("@NumeroPlan ", SqlDbType.VarChar);
                varParametros[30].Value = NumeroPlan;
                varParametros[31] = new SqlParameter("@NumeroPoliza", SqlDbType.VarChar);
                varParametros[31].Value = NumeroPoliza;
                varParametros[32] = new SqlParameter("@IdReceptor", SqlDbType.VarChar);
                varParametros[32].Value = IdReceptor;

                varParametros[33] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                varParametros[33].Value = iafas;
                varParametros[34] = new SqlParameter("@CodParentesco", SqlDbType.VarChar);
                varParametros[34].Value = CodParentesco;
                varParametros[35] = new SqlParameter("@RUCClinica", SqlDbType.VarChar);
                varParametros[35].Value = RUClinica;
                varParametros[36] = new SqlParameter("@Tarjeta", SqlDbType.VarChar);
                varParametros[36].Value = Tarjeta;
                varParametros[37] = new SqlParameter("@NumeroContrato", SqlDbType.VarChar);
                varParametros[37].Value = NumeroContrato;
                varParametros[38] = new SqlParameter("@Condiciones", SqlDbType.VarChar);
                varParametros[38].Value = CondicionesEspeciales;
                varParametros[39] = new SqlParameter("@Observaciones", SqlDbType.VarChar);
                varParametros[39].Value = Observaciones;
                varParametros[40] = new SqlParameter("@FechaAfiliacion", SqlDbType.VarChar);
                varParametros[40].Value = Observaciones;
                varParametros[41] = new SqlParameter("@NumeroAutorizacionNuevo", SqlDbType.VarChar);
                varParametros[41].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspSITEDS_GrabarDatosGenerales", varParametros, TipoProcesamiento.DataReader, false);
                if (varDataReader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }

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
        public bool GrabarCoberturaAcreditacion(string NumeroAutorizacion, string Beneficios, string CodIndicadorRestriccion, string BeneficioMaximoInicial, string Observaciones, string CodigoTipoCobertura, string CodigoSubTipoCobertura, string CodCopagoFijo
            , string CodCopagoVariable, string CodCalificacionServicio, string CodTipoMoneda, string iafas, int idClinica)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[13];
                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@Beneficios", SqlDbType.VarChar);
                varParametros[1].Value = Beneficios;
                varParametros[2] = new SqlParameter("@CodIndicadorRestriccion", SqlDbType.VarChar);
                varParametros[2].Value = CodIndicadorRestriccion;
                varParametros[3] = new SqlParameter("@BeneficioMaximoInicial", SqlDbType.Int);
                varParametros[3].Value = Convert.ToInt32(Convert.ToDouble(BeneficioMaximoInicial));
                varParametros[4] = new SqlParameter("@Observaciones", SqlDbType.Text);
                varParametros[4].Value = Observaciones;
                varParametros[5] = new SqlParameter("@CodigoTipoCobertura", SqlDbType.VarChar);
                varParametros[5].Value = CodigoTipoCobertura;
                varParametros[6] = new SqlParameter("@CodigoSubTipoCobertura", SqlDbType.VarChar);
                varParametros[6].Value = CodigoSubTipoCobertura;
                varParametros[7] = new SqlParameter("@CodCopagoFijo", SqlDbType.Decimal);
                varParametros[7].Value = CodCopagoFijo;
                varParametros[8] = new SqlParameter("@CodCopagoVariable", SqlDbType.Decimal);
                varParametros[8].Value = CodCopagoVariable;
                varParametros[9] = new SqlParameter("@CodCalificacionServicio", SqlDbType.VarChar);
                varParametros[9].Value = CodCalificacionServicio;
                varParametros[10] = new SqlParameter("@CodTipoMoneda", SqlDbType.VarChar);
                varParametros[10].Value = CodTipoMoneda;
                varParametros[11] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                varParametros[11].Value = iafas;
                varParametros[12] = new SqlParameter("@NumeroAutorizacionNuevo", SqlDbType.VarChar);
                varParametros[12].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspSITEDS_GrabarCoberturaAcreditacion", varParametros, TipoProcesamiento.DataReader, false);
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
        public bool GrabarCondicionesMedicaAntecedentes(string NumeroAutorizacion, string Codigo, string Diagnostico, string Observaciones, string iafas, int idClinica)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[6];
                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@Codigo", SqlDbType.VarChar);
                varParametros[1].Value = Codigo;
                varParametros[2] = new SqlParameter("@Diagnostico", SqlDbType.VarChar);
                varParametros[2].Value = Diagnostico;
                varParametros[3] = new SqlParameter("@Observaciones", SqlDbType.Text);
                varParametros[3].Value = Observaciones;
                varParametros[4] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                varParametros[4].Value = iafas;
                varParametros[5] = new SqlParameter("@NumeroAutorizacionNuevo", SqlDbType.VarChar);
                varParametros[5].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspSITEDS_GrabarCondicionesMedicaAntecedentes", varParametros, TipoProcesamiento.DataReader, false);
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
        public bool GrabarCondicionesMedicaCarencia(string NumeroAutorizacion, string Codigo, string Diagnostico, string Observaciones, string iafas, int idClinica)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[6];
                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@Codigo", SqlDbType.VarChar);
                varParametros[1].Value = Codigo;
                varParametros[2] = new SqlParameter("@Diagnostico", SqlDbType.VarChar);
                varParametros[2].Value = Diagnostico;
                varParametros[3] = new SqlParameter("@Observaciones", SqlDbType.Text);
                varParametros[3].Value = Observaciones;
                varParametros[4] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                varParametros[4].Value = iafas;
                varParametros[5] = new SqlParameter("@NumeroAutorizacionNuevo", SqlDbType.VarChar);
                varParametros[5].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspSITEDS_GrabarCondicionesMedicaCarencia", varParametros, TipoProcesamiento.DataReader, false);
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
        public bool GrabarCondicionesMedicaEnfermedad(string NumeroAutorizacion, string Codigo, string Diagnostico, string Observaciones, string iafas, int idClinica)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[6];
                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@Codigo", SqlDbType.VarChar);
                varParametros[1].Value = Codigo;
                varParametros[2] = new SqlParameter("@Diagnostico", SqlDbType.VarChar);
                varParametros[2].Value = Diagnostico;
                varParametros[3] = new SqlParameter("@Observaciones", SqlDbType.Text);
                varParametros[3].Value = Observaciones;
                varParametros[4] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                varParametros[4].Value = iafas;
                varParametros[5] = new SqlParameter("@NumeroAutorizacionNuevo", SqlDbType.VarChar);
                varParametros[5].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspSITEDS_GrabarCondicionesMedicaEnfermedad", varParametros, TipoProcesamiento.DataReader, false);
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
        public bool GrabarCondicionesMedicaExclusiones(string NumeroAutorizacion, string Codigo, string Diagnostico, string Observaciones, string iafas, int idClinica)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[6];
                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@Codigo", SqlDbType.VarChar);
                varParametros[1].Value = Codigo;
                varParametros[2] = new SqlParameter("@Diagnostico", SqlDbType.VarChar);
                varParametros[2].Value = Diagnostico;
                varParametros[3] = new SqlParameter("@Observaciones", SqlDbType.Text);
                varParametros[3].Value = Observaciones;
                varParametros[4] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                varParametros[4].Value = iafas;
                varParametros[5] = new SqlParameter("@NumeroAutorizacionNuevo", SqlDbType.VarChar);
                varParametros[5].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspSITEDS_GrabarCondicionesMedicaExclusiones", varParametros, TipoProcesamiento.DataReader, false);
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
        public bool GrabarCondicionesMedicaPreexistencia(string NumeroAutorizacion, string Codigo, string Diagnostico, string Observaciones, string iafas, int idClinica)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[6];
                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@Codigo", SqlDbType.VarChar);
                varParametros[1].Value = Codigo;
                varParametros[2] = new SqlParameter("@Diagnostico", SqlDbType.VarChar);
                varParametros[2].Value = Diagnostico;
                varParametros[3] = new SqlParameter("@Observaciones", SqlDbType.Text);
                varParametros[3].Value = Observaciones;
                varParametros[4] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                varParametros[4].Value = iafas;
                varParametros[5] = new SqlParameter("@NumeroAutorizacionNuevo", SqlDbType.VarChar);
                varParametros[5].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspSITEDS_GrabarCondicionesMedicaPreexistencia", varParametros, TipoProcesamiento.DataReader, false);
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
        public bool GrabarExcepcionCarencia(string NumeroAutorizacion, string CodigoTipoCobertura, string CodigoSubTipoCobertura, string Codigo, string GrupoDiagnostico, string Observaciones, string iafas, int idClinica)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[8];
                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@CodigoTipoCobertura", SqlDbType.VarChar);
                varParametros[1].Value = CodigoTipoCobertura;
                varParametros[2] = new SqlParameter("@CodigoSubTipoCobertura", SqlDbType.VarChar);
                varParametros[2].Value = CodigoSubTipoCobertura;
                varParametros[3] = new SqlParameter("@Codigo", SqlDbType.VarChar);
                varParametros[3].Value = Codigo;
                varParametros[4] = new SqlParameter("@GrupoDiagnostico", SqlDbType.VarChar);
                varParametros[4].Value = GrupoDiagnostico;
                varParametros[5] = new SqlParameter("@Observaciones", SqlDbType.Text);
                varParametros[5].Value = Observaciones;
                varParametros[6] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                varParametros[6].Value = iafas;
                varParametros[7] = new SqlParameter("@NumeroAutorizacionNuevo", SqlDbType.VarChar);
                varParametros[7].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspSITEDS_GrabarExcepcionCarencia", varParametros, TipoProcesamiento.DataReader, false);
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
        public bool GrabarProcedimientos(string NumeroAutorizacion, string CodigoTipoCobertura, string CodigoSubTipoCobertura, string Codigo, string Procedimiento, string Genero, string DesCopagoFijo, string DesCopagoVariable, string Frecuencia, string Tiempo , string Observaciones, string iafas, int idClinica)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[13];
                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@CodigoTipoCobertura", SqlDbType.VarChar);
                varParametros[1].Value = CodigoTipoCobertura;
                varParametros[2] = new SqlParameter("@CodigoSubTipoCobertura", SqlDbType.VarChar);
                varParametros[2].Value = CodigoSubTipoCobertura;
                varParametros[3] = new SqlParameter("@Codigo", SqlDbType.VarChar);
                varParametros[3].Value = Codigo;
                varParametros[4] = new SqlParameter("@Procedimiento", SqlDbType.VarChar);
                varParametros[4].Value = Procedimiento;
                varParametros[5] = new SqlParameter("@Genero", SqlDbType.VarChar);
                varParametros[5].Value = Genero;
                varParametros[6] = new SqlParameter("@DesCopagoFijo", SqlDbType.Decimal);
                varParametros[6].Value = DesCopagoFijo;
                varParametros[7] = new SqlParameter("@DesCopagoVariable", SqlDbType.Decimal);
                varParametros[7].Value = DesCopagoVariable;
                varParametros[8] = new SqlParameter("@Frecuencia", SqlDbType.SmallInt);
                varParametros[8].Value = Frecuencia;
                varParametros[9] = new SqlParameter("@Tiempo", SqlDbType.SmallInt);
                varParametros[9].Value = Tiempo;
                varParametros[10] = new SqlParameter("@Observaciones", SqlDbType.Text);
                varParametros[10].Value = Observaciones;
                varParametros[11] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                varParametros[11].Value = iafas;
                varParametros[12] = new SqlParameter("@NumeroAutorizacionNuevo", SqlDbType.VarChar);
                varParametros[12].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspSITEDS_GrabarProcedimientos", varParametros, TipoProcesamiento.DataReader, false);
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
        public bool GrabarTiempoEspera(string NumeroAutorizacion, string CodigoTipoCobertura, string CodigoSubTipoCobertura, string Codigo, string GrupoDiagnostico, string Observaciones, string iafas, int idClinica)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[8];
                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@CodigoTipoCobertura", SqlDbType.VarChar);
                varParametros[1].Value = CodigoTipoCobertura;
                varParametros[2] = new SqlParameter("@CodigoSubTipoCobertura", SqlDbType.VarChar);
                varParametros[2].Value = CodigoSubTipoCobertura;
                varParametros[3] = new SqlParameter("@Codigo", SqlDbType.VarChar);
                varParametros[3].Value = Codigo;
                varParametros[4] = new SqlParameter("@GrupoDiagnostico", SqlDbType.VarChar);
                varParametros[4].Value = GrupoDiagnostico;
                varParametros[5] = new SqlParameter("@Observaciones", SqlDbType.Text);
                varParametros[5].Value = Observaciones;
                varParametros[6] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                varParametros[6].Value = iafas;
                varParametros[7] = new SqlParameter("@NumeroAutorizacionNuevo", SqlDbType.VarChar);
                varParametros[7].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspSITEDS_GrabarTiempoEspera", varParametros, TipoProcesamiento.DataReader, false);
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
        public bool GrabarTransaccion(string NumeroAutorizacion, string Sucursal, string TipoSeguro, string IDPaciente, string IDAseguradora, string IDCitaSpring, 
            string iafas, string TipoComprobante, string SerieComprobante, string SerieCobranza, string TipoTarjeta, string Tarjeta , string monto, string firma, string tipo, int idClinica, int estado, string ruc, string razonsocial, string direccion, string email, string NumeroOperacion, string NumeroAutorizacionFarmacia = "",
            string tipoPaciente = "0", string tipoDocumentoBoleta = "", string numeroDocumentoBoleta = "",
            string nombresBoleta = "", string apellidoPaternoBoleta = "", string apellidoMaternoBoleta = "",
            string direccionBoleta = "", string fechaNacimientoBoleta = "", string celularBoleta = "",
            string emailBoleta = "", string codigoParentesco = "", string codigoAfiliado = "", string tipoDocumentoContratante = "",
            string numeroDocumentoContratante = "", string idCita = "")
        {
            string cadena = idCita + "¯" + IDCitaSpring + "¯" + NumeroAutorizacion + "¦" + Sucursal + "¦" + TipoSeguro + "¦" + IDPaciente + "¦" + IDAseguradora + "¦" + IDCitaSpring + "¦" + iafas + "¦" + TipoComprobante + "¦" + TipoTarjeta + "¦" + Tarjeta + "¦" + monto + "¦" + firma + "¦" + tipo + "¦" +estado.ToString() + "¦" + ruc + "¦" + razonsocial + "¦" + direccion + "¦" + email;
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros;
                varParametros = new SqlParameter[37];
                //bool indEnvioAutorizacionFarmacia = true;
                //if (indEnvioAutorizacionFarmacia)
                //{
                //    varParametros = new SqlParameter[23];
                //}
                //else
                //{
                //    varParametros = new SqlParameter[22];
                //}

                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@Sucursal", SqlDbType.Char);
                varParametros[1].Value = Sucursal;
                varParametros[2] = new SqlParameter("@TipoSeguro", SqlDbType.Int);
                if (string.IsNullOrEmpty(TipoSeguro)) varParametros[2].Value = null;
                else varParametros[2].Value = TipoSeguro;
                varParametros[3] = new SqlParameter("@IDPaciente", SqlDbType.Int);
                varParametros[3].Value = IDPaciente;
                varParametros[4] = new SqlParameter("@IDAseguradora", SqlDbType.Int);
                varParametros[4].Value = IDAseguradora;
                varParametros[5] = new SqlParameter("@IDCitaSpring", SqlDbType.Int);
                varParametros[5].Value = IDCitaSpring;
                varParametros[6] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                varParametros[6].Value = iafas;
                varParametros[7] = new SqlParameter("@TipoComprobante", SqlDbType.VarChar);
                varParametros[7].Value = TipoComprobante;
                varParametros[8] = new SqlParameter("@SerieComprobante", SqlDbType.VarChar);
                varParametros[8].Value = (TipoComprobante=="BV")?"B001":"F0001";
                varParametros[9] = new SqlParameter("@SerieCobranza", SqlDbType.VarChar);
                varParametros[9].Value = "";
                varParametros[10] = new SqlParameter("@TipoTarjeta", SqlDbType.VarChar);
                if (string.IsNullOrEmpty(TipoTarjeta)) varParametros[10].Value = null;
                else varParametros[10].Value = TipoTarjeta;
                varParametros[11] = new SqlParameter("@Tarjeta", SqlDbType.VarChar);
                if(string.IsNullOrEmpty(Tarjeta)) varParametros[11].Value = null;
                else varParametros[11].Value = Tarjeta;
                //else varParametros[11].Value = Tarjeta.Substring(Tarjeta.Length - 4);
                varParametros[12] = new SqlParameter("@NumeroAutorizacionNuevo", SqlDbType.VarChar);
                varParametros[12].Value = null;
                varParametros[13] = new SqlParameter("@Usuario", SqlDbType.VarChar);
                varParametros[13].Value = "Clínica SF";
                varParametros[14] = new SqlParameter("@Monto", SqlDbType.Decimal);
                varParametros[14].Value = monto;
                if (!string.IsNullOrEmpty(firma))
                {
                    varParametros[15] = new SqlParameter("@Firma", SqlDbType.Image, System.Convert.FromBase64String(firma.Replace("data:image/png;base64,", "")).Length);
                    varParametros[15].Value = System.Convert.FromBase64String(firma.Replace("data:image/png;base64,", ""));
                }
                else
                {
                    firma = "";
                    varParametros[15] = new SqlParameter("@Firma", SqlDbType.Image, System.Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAMAAAC6V+0/AAADAFBMVEX///8AAAACAgIDAwMEBAQFBQUGBgYHBwcICAgJCQkKCgoLCwsMDAwNDQ0ODg4PDw8QEBARERESEhITExMUFBQVFRUWFhYXFxcYGBgZGRkaGhobGxscHBwdHR0eHh4fHx8gICAhISEiIiIjIyMkJCQlJSUmJiYnJycoKCgpKSkqKiorKyssLCwtLS0uLi4vLy8wMDAxMTEyMjIzMzM0NDQ1NTU2NjY3Nzc4ODg5OTk6Ojo7Ozs8PDw9PT0+Pj4/Pz9AQEBBQUFCQkJDQ0NERERFRUVGRkZHR0dISEhJSUlKSkpLS0tMTExNTU1OTk5PT09QUFBRUVFSUlJTU1NUVFRVVVVWVlZXV1dYWFhZWVlaWlpbW1tcXFxdXV1eXl5fX19gYGBhYWFiYmJjY2NkZGRlZWVmZmZnZ2doaGhpaWlqampra2tsbGxtbW1ubm5vb29wcHBxcXFycnJzc3N0dHR1dXV2dnZ3d3d4eHh5eXl6enp7e3t8fHx9fX1+fn5/f3+AgICBgYGCgoKDg4OEhISFhYWGhoaHh4eIiIiJiYmKioqLi4uMjIyNjY2Ojo6Pj4+QkJCRkZGSkpKTk5OUlJSVlZWWlpaXl5eYmJiZmZmampqbm5ucnJydnZ2enp6fn5+goKChoaGioqKjo6OkpKSlpaWmpqanp6eoqKipqamqqqqrq6usrKytra2urq6vr6+wsLCxsbGysrKzs7O0tLS1tbW2tra3t7e4uLi5ubm6urq7u7u8vLy9vb2+vr6/v7/AwMDBwcHCwsLDw8PExMTFxcXGxsbHx8fIyMjJycnKysrLy8vMzMzNzc3Ozs7Pz8/Q0NDR0dHS0tLT09PU1NTV1dXW1tbX19fY2NjZ2dna2trb29vc3Nzd3d3e3t7f39/g4ODh4eHi4uLj4+Pk5OTl5eXm5ubn5+fo6Ojp6enq6urr6+vs7Ozt7e3u7u7v7+/w8PDx8fHy8vLz8/P09PT19fX29vb39/f4+Pj5+fn6+vr7+/v8/Pz9/f3+/v7///+VceJeAAAAAXRSTlMAQObYZgAAAAFiS0dE/6UH8sUAAAAJcEhZcwAADsQAAA7EAZUrDhsAAAARSURBVHjaYziDBTCMCg4mQQCKbz7Q5y936AAAAABJRU5ErkJggg==").Length);
                    varParametros[15].Value = System.Convert.FromBase64String(firma.Replace("data:image/png;base64,", ""));
                }
                varParametros[16] = new SqlParameter("@Tipo", SqlDbType.Int);
                varParametros[16].Value = tipo;
                varParametros[17] = new SqlParameter("@Estado", SqlDbType.Int);
                varParametros[17].Value = estado;
                //Se agrego parametros //se agrero entidades 16042021
                varParametros[18] = new SqlParameter("@RUC", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(ruc)) varParametros[18].Value = null;
                else varParametros[18].Value = ruc;
                varParametros[19] = new SqlParameter("@RazonSocial", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(razonsocial)) varParametros[19].Value = null;
                else varParametros[19].Value = razonsocial;
                varParametros[20] = new SqlParameter("@Direccion", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(direccion)) varParametros[20].Value = null;
                else varParametros[20].Value = direccion;
                varParametros[21] = new SqlParameter("@Email", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(email)) varParametros[21].Value = null;
                else varParametros[21].Value = email;
                varParametros[22] = new SqlParameter("@NumeroAutorizacionFarmacia", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(NumeroAutorizacionFarmacia)) varParametros[22].Value = null;
                else varParametros[22].Value = NumeroAutorizacionFarmacia;
                varParametros[23] = new SqlParameter("@TipoPaciente", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(tipoPaciente)) varParametros[23].Value = null;
                else varParametros[23].Value = tipoPaciente;

                varParametros[24] = new SqlParameter("@TipoDocumentoBoleta", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(tipoDocumentoBoleta)) varParametros[24].Value = null;
                else varParametros[24].Value = tipoDocumentoBoleta;
                varParametros[25] = new SqlParameter("@NumeroDocumentoBoleta", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(numeroDocumentoBoleta)) varParametros[25].Value = null;
                else varParametros[25].Value = numeroDocumentoBoleta;
                varParametros[26] = new SqlParameter("@NombresBoleta", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(nombresBoleta)) varParametros[26].Value = null;
                else varParametros[26].Value = nombresBoleta;
                varParametros[27] = new SqlParameter("@ApellidoPaternoBoleta", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(apellidoPaternoBoleta)) varParametros[27].Value = null;
                else varParametros[27].Value = apellidoPaternoBoleta;
                varParametros[28] = new SqlParameter("@ApellidoMaternoBoleta", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(apellidoMaternoBoleta)) varParametros[28].Value = null;
                else varParametros[28].Value = apellidoMaternoBoleta;
                varParametros[29] = new SqlParameter("@DireccionBoleta", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(direccionBoleta)) varParametros[29].Value = null;
                else varParametros[29].Value = direccionBoleta;
                varParametros[30] = new SqlParameter("@FechaNacimientoBoleta", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(fechaNacimientoBoleta)) varParametros[30].Value = null;
                else varParametros[30].Value = fechaNacimientoBoleta;
                varParametros[31] = new SqlParameter("@CelularBoleta", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(celularBoleta)) varParametros[31].Value = null;
                else varParametros[31].Value = celularBoleta;
                varParametros[32] = new SqlParameter("@EmailBoleta", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(emailBoleta)) varParametros[32].Value = null;
                else varParametros[32].Value = emailBoleta;

                varParametros[33] = new SqlParameter("@pCodigoParentesco", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(codigoParentesco)) varParametros[33].Value = null;
                else varParametros[33].Value = codigoParentesco;
                varParametros[34] = new SqlParameter("@pCodigoAfiliado", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(codigoAfiliado)) varParametros[34].Value = null;
                else varParametros[34].Value = codigoAfiliado;
                varParametros[35] = new SqlParameter("@pTipoDocumentoContratante", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(tipoDocumentoContratante)) varParametros[35].Value = null;
                else varParametros[35].Value = tipoDocumentoContratante;
                varParametros[36] = new SqlParameter("@pNumeroDocumentoContratante", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(numeroDocumentoContratante)) varParametros[36].Value = null;
                else varParametros[36].Value = numeroDocumentoContratante;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspGrabarCitasTransaccionales", varParametros, TipoProcesamiento.DataReader, false);
                
                if (varDataReader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                new ErrorDA().RegistrarError(ex, "WS", "GrabarTransaccion", cadena);
                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }
        public bool GrabarTransaccionVirtual(string data, string firma, string idCita)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                string[] parametros = data.Split('¦');
                string NumeroAutorizacion = parametros[1];
                string Sucursal = parametros[2];
                string TipoSeguro = parametros[3];
                string IDPaciente = parametros[4];
                string IDAseguradora = parametros[5];
                string IDCitaSpring = parametros[6];
                string iafas = parametros[7];
                string TipoComprobante = parametros[8];
                string SerieComprobante = parametros[9];
                string SerieCobranza = parametros[10];
                string TipoTarjeta = parametros[11];
                string Tarjeta = parametros[12];
                string NumeroAutorizacionNuevo = parametros[13];
                string UsuarioCreacion = parametros[14];
                decimal CodCopagoFijo = decimal.Parse(parametros[15]);
                //string firma = parametros[16];
                int tipo = int.Parse(parametros[17]);
                int estado = int.Parse(parametros[18]);
                int idClinica = int.Parse(parametros[19]);
                //se agrero entidades 16042021
                string RUC = parametros[20];
                string RazonSocial = parametros[21];
                string Direccion = parametros[22];
                string Email = parametros[23];
                string NumeroAutorizacionFarmacia = parametros[24];
                string NumeroOperacion = parametros[25];

                string tipoPaciente = parametros[26];
                string tipoDocumentoBoleta = parametros[27];
                string numeroDocumentoBoleta = parametros[28];
                string nombresBoleta = parametros[29];
                string apellidoPaternoBoleta = parametros[30];
                string apellidoMaternoBoleta = parametros[31];
                string direccionBoleta = parametros[32];
                string fechaNacimientoBoleta = parametros[33];
                string celularBoleta = parametros[34];
                string emailBoleta = parametros[35];
                string codigoParentesco = parametros[36];
                string codigoAfiliado = parametros[37];
                string tipoDocumentoContratante = parametros[38];
                string numeroDocumentoContratante = parametros[39];

                SqlParameter[] varParametros;
                varParametros = new SqlParameter[37];

                // SqlParameter[] varParametros = new SqlParameter[23];//se agrero entidades 16042021 18 a 22
                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                if(String.IsNullOrEmpty(NumeroAutorizacion)) varParametros[0].Value = null;
                else varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@Sucursal", SqlDbType.Char);
                varParametros[1].Value = Sucursal;
                varParametros[2] = new SqlParameter("@TipoSeguro", SqlDbType.Int);
                if(TipoSeguro.Equals("NULL")) varParametros[2].Value = null;
                else varParametros[2].Value = TipoSeguro;
                varParametros[3] = new SqlParameter("@IDPaciente", SqlDbType.Int);
                varParametros[3].Value = IDPaciente;
                varParametros[4] = new SqlParameter("@IDAseguradora", SqlDbType.Int);
                if (String.IsNullOrEmpty(IDAseguradora) || IDAseguradora.Equals("NULL")) varParametros[4].Value = null;
                else varParametros[4].Value = IDAseguradora;
                varParametros[5] = new SqlParameter("@IDCitaSpring", SqlDbType.Int);
                varParametros[5].Value = IDCitaSpring;
                varParametros[6] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(iafas)) varParametros[6].Value = null;
                else varParametros[6].Value = iafas;
                varParametros[7] = new SqlParameter("@TipoComprobante", SqlDbType.VarChar);
                varParametros[7].Value = TipoComprobante;
                varParametros[8] = new SqlParameter("@SerieComprobante", SqlDbType.VarChar);
                varParametros[8].Value = SerieComprobante;
                //varParametros[8].Value = (TipoComprobante == "BV") ? "B001" : "F0001";
                varParametros[9] = new SqlParameter("@SerieCobranza", SqlDbType.VarChar);
                varParametros[9].Value = SerieCobranza;
                //varParametros[9].Value = "";
                varParametros[10] = new SqlParameter("@TipoTarjeta", SqlDbType.VarChar);
                varParametros[10].Value = TipoTarjeta;
                varParametros[11] = new SqlParameter("@Tarjeta", SqlDbType.VarChar);
                varParametros[11].Value = Tarjeta;
                //varParametros[11].Value = Tarjeta.Substring(Tarjeta.Length - 4);
                varParametros[12] = new SqlParameter("@NumeroAutorizacionNuevo", SqlDbType.VarChar);
                if (NumeroAutorizacionNuevo.Equals("NULL")) varParametros[12].Value = null;
                else varParametros[12].Value = NumeroAutorizacionNuevo;
                //varParametros[12].Value = null;
                varParametros[13] = new SqlParameter("@Usuario", SqlDbType.VarChar);
                varParametros[13].Value = "Clínica SF";
                varParametros[14] = new SqlParameter("@Monto", SqlDbType.Decimal);
                varParametros[14].Value = CodCopagoFijo;
                //varParametros[15] = new SqlParameter("@Firma", SqlDbType.Image, System.Convert.FromBase64String(firma.Replace("data:image/png;base64,", "")).Length);
                //varParametros[15].Value = System.Convert.FromBase64String(firma.Replace("data:image/png;base64,", ""));
                if (!String.IsNullOrEmpty(firma) && !firma.Equals("0x"))
                {
                    varParametros[15] = new SqlParameter("@Firma", SqlDbType.Image, System.Convert.FromBase64String(firma.Replace("data:image/png;base64,", "")).Length);
                    varParametros[15].Value = System.Convert.FromBase64String(firma.Replace("data:image/png;base64,", ""));
                }
                else
                {
                    firma = "";
                    varParametros[15] = new SqlParameter("@Firma", SqlDbType.Image, System.Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAMAAAC6V+0/AAADAFBMVEX///8AAAACAgIDAwMEBAQFBQUGBgYHBwcICAgJCQkKCgoLCwsMDAwNDQ0ODg4PDw8QEBARERESEhITExMUFBQVFRUWFhYXFxcYGBgZGRkaGhobGxscHBwdHR0eHh4fHx8gICAhISEiIiIjIyMkJCQlJSUmJiYnJycoKCgpKSkqKiorKyssLCwtLS0uLi4vLy8wMDAxMTEyMjIzMzM0NDQ1NTU2NjY3Nzc4ODg5OTk6Ojo7Ozs8PDw9PT0+Pj4/Pz9AQEBBQUFCQkJDQ0NERERFRUVGRkZHR0dISEhJSUlKSkpLS0tMTExNTU1OTk5PT09QUFBRUVFSUlJTU1NUVFRVVVVWVlZXV1dYWFhZWVlaWlpbW1tcXFxdXV1eXl5fX19gYGBhYWFiYmJjY2NkZGRlZWVmZmZnZ2doaGhpaWlqampra2tsbGxtbW1ubm5vb29wcHBxcXFycnJzc3N0dHR1dXV2dnZ3d3d4eHh5eXl6enp7e3t8fHx9fX1+fn5/f3+AgICBgYGCgoKDg4OEhISFhYWGhoaHh4eIiIiJiYmKioqLi4uMjIyNjY2Ojo6Pj4+QkJCRkZGSkpKTk5OUlJSVlZWWlpaXl5eYmJiZmZmampqbm5ucnJydnZ2enp6fn5+goKChoaGioqKjo6OkpKSlpaWmpqanp6eoqKipqamqqqqrq6usrKytra2urq6vr6+wsLCxsbGysrKzs7O0tLS1tbW2tra3t7e4uLi5ubm6urq7u7u8vLy9vb2+vr6/v7/AwMDBwcHCwsLDw8PExMTFxcXGxsbHx8fIyMjJycnKysrLy8vMzMzNzc3Ozs7Pz8/Q0NDR0dHS0tLT09PU1NTV1dXW1tbX19fY2NjZ2dna2trb29vc3Nzd3d3e3t7f39/g4ODh4eHi4uLj4+Pk5OTl5eXm5ubn5+fo6Ojp6enq6urr6+vs7Ozt7e3u7u7v7+/w8PDx8fHy8vLz8/P09PT19fX29vb39/f4+Pj5+fn6+vr7+/v8/Pz9/f3+/v7///+VceJeAAAAAXRSTlMAQObYZgAAAAFiS0dE/6UH8sUAAAAJcEhZcwAADsQAAA7EAZUrDhsAAAARSURBVHjaYziDBTCMCg4mQQCKbz7Q5y936AAAAABJRU5ErkJggg==").Length);
                    varParametros[15].Value = System.Convert.FromBase64String(firma.Replace("data:image/png;base64,", ""));
                }
                //varParametros[15].Value = System.Convert.FromBase64String(firma.Replace("data:image/png;base64,", ""));
                varParametros[16] = new SqlParameter("@Tipo", SqlDbType.Int);
                varParametros[16].Value = tipo;
                varParametros[17] = new SqlParameter("@Estado", SqlDbType.Int);
                varParametros[17].Value = estado;
                //se agrero entidades 16042021
                varParametros[18] = new SqlParameter("@RUC", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(RUC)) varParametros[18].Value = null;
                else varParametros[18].Value = RUC;
                varParametros[19] = new SqlParameter("@RazonSocial", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(RazonSocial)) varParametros[19].Value = null;
                else varParametros[19].Value = RazonSocial;
                varParametros[20] = new SqlParameter("@Direccion", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(Direccion)) varParametros[20].Value = null;
                else varParametros[20].Value = Direccion;
                varParametros[21] = new SqlParameter("@Email", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(Email)) varParametros[21].Value = null;
                else varParametros[21].Value = Email;

                varParametros[22] = new SqlParameter("@NumeroAutorizacionFarmacia", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(NumeroAutorizacionFarmacia)) varParametros[22].Value = null;
                else varParametros[22].Value = NumeroAutorizacionFarmacia;
                varParametros[23] = new SqlParameter("@TipoPaciente", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(tipoPaciente)) varParametros[23].Value = null;
                else varParametros[23].Value = tipoPaciente;

                varParametros[24] = new SqlParameter("@TipoDocumentoBoleta", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(tipoDocumentoBoleta)) varParametros[24].Value = null;
                else varParametros[24].Value = tipoDocumentoBoleta;
                varParametros[25] = new SqlParameter("@NumeroDocumentoBoleta", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(numeroDocumentoBoleta)) varParametros[25].Value = null;
                else varParametros[25].Value = numeroDocumentoBoleta;
                varParametros[26] = new SqlParameter("@NombresBoleta", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(nombresBoleta)) varParametros[26].Value = null;
                else varParametros[26].Value = nombresBoleta;
                varParametros[27] = new SqlParameter("@ApellidoPaternoBoleta", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(apellidoPaternoBoleta)) varParametros[27].Value = null;
                else varParametros[27].Value = apellidoPaternoBoleta;
                varParametros[28] = new SqlParameter("@ApellidoMaternoBoleta", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(apellidoMaternoBoleta)) varParametros[28].Value = null;
                else varParametros[28].Value = apellidoMaternoBoleta;
                varParametros[29] = new SqlParameter("@DireccionBoleta", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(direccionBoleta)) varParametros[29].Value = null;
                else varParametros[29].Value = direccionBoleta;
                varParametros[30] = new SqlParameter("@FechaNacimientoBoleta", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(fechaNacimientoBoleta)) varParametros[30].Value = null;
                else varParametros[30].Value = fechaNacimientoBoleta;
                varParametros[31] = new SqlParameter("@CelularBoleta", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(celularBoleta)) varParametros[31].Value = null;
                else varParametros[31].Value = celularBoleta;
                varParametros[32] = new SqlParameter("@EmailBoleta", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(emailBoleta)) varParametros[32].Value = null;
                else varParametros[32].Value = emailBoleta;

                varParametros[33] = new SqlParameter("@pCodigoParentesco", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(codigoParentesco)) varParametros[33].Value = null;
                else varParametros[33].Value = codigoParentesco;
                varParametros[34] = new SqlParameter("@pCodigoAfiliado", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(codigoAfiliado)) varParametros[34].Value = null;
                else varParametros[34].Value = codigoAfiliado;
                varParametros[35] = new SqlParameter("@pTipoDocumentoContratante", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(tipoDocumentoContratante)) varParametros[35].Value = null;
                else varParametros[35].Value = tipoDocumentoContratante;
                varParametros[36] = new SqlParameter("@pNumeroDocumentoContratante", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(numeroDocumentoContratante)) varParametros[36].Value = null;
                else varParametros[36].Value = numeroDocumentoContratante;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspGrabarCitasTransaccionales", varParametros, TipoProcesamiento.DataReader, false);//, CadenaClinica(idClinica));

                if (varDataReader.HasRows || String.IsNullOrEmpty(NumeroAutorizacion))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                //DateTime now = DateTime.Now;
                //string Archivo = $"{"ErrorGrabarTransaccionVirtual"}_{now.Year}_{now.Month.ToString().PadLeft(2, '0')}_{now.Day.ToString().PadLeft(2, '0')}{".txt"}";
                //string rutaArchivo = string.Format("{0}{1}", ConfigurationManager.AppSettings["LogCitaBDSpring"], Archivo);
                //string nombreArchivo = "";
                //using (FileStream stream = new FileStream(rutaArchivo + nombreArchivo, FileMode.Append, FileAccess.Write, FileShare.Write))
                //{
                //    using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.Default))
                //    {
                //        streamWriter.WriteLine("Fecha = " + DateTime.Now);
                //        streamWriter.WriteLine("Mensaje = " + ex.Message);
                //        streamWriter.WriteLine("Mensaje = " + ex.StackTrace);
                //        streamWriter.WriteLine(new string('_', 50));
                //    }
                //}
                new ErrorDA().RegistrarError(ex, "WSV", "GrabarTransaccionVirtual",idCita + "¯" + data);
                return false;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }
        public Dictionary<string, string> GrabarTransaccionSpring(string data, string firma, string idCita)
        {
            ConexionUtil varConexion = new ConexionUtil();
            Dictionary<string, string> varResultado = new Dictionary<string, string>();
            try
            {
                string[] parametros = data.Split('¦');
                string NumeroAutorizacion = parametros[1];
                string Sucursal = parametros[2];
                string TipoSeguro = parametros[3];
                string IDPaciente = parametros[4];
                string IDAseguradora = parametros[5];
                string IDCitaSpring = parametros[6];
                string iafas = parametros[7];
                string TipoComprobante = parametros[8];
                string SerieComprobante = parametros[9];
                string SerieCobranza = parametros[10];
                string TipoTarjeta = parametros[11];
                string Tarjeta = parametros[12];
                string NumeroAutorizacionNuevo = parametros[13];
                string UsuarioCreacion = parametros[14];
                decimal CodCopagoFijo = decimal.Parse(parametros[15]);
                //string firma = parametros[16];
                int tipo = int.Parse(parametros[17]);
                int estado = int.Parse(parametros[18]);
                int idClinica = int.Parse(parametros[19]);
                //se agrero entidades 16042021
                string RUC = parametros[20];
                string RazonSocial = parametros[21];
                string Direccion = parametros[22];
                string Email = parametros[23];
                string NumeroAutorizacionFarmacia = "";//parametros[24]; //[24] es la firma en base64
                string NumeroOperacion = parametros[25];
                string TipoPaciente = parametros[26];
                int NumeroContrato = !String.IsNullOrEmpty(parametros[27]) ? int.Parse(parametros[27]) : 0;//IdContrato

                SqlParameter[] varParametros;

                // Lógica implementada para parametrizar el envio de los campos autorización farmacia y numero operacion.
                // aplica en los casos que unas sedes no tengan los parametros compilados y otras sí en producción.
                // Preguntar a Enrique Espinal o Gustavo Reque.

                bool indEnvioAutorizacionFarmacia = false,
                     indEnviaNumeroOperacion = false;

                //15 - Sanchez Ferrer
                if (idClinica == 15)
                {
                    indEnviaNumeroOperacion = true;
                }
                else if (idClinica == 2 || idClinica == 3 || idClinica == 4 || idClinica == 5 || idClinica == 6 || idClinica == 7 ||
                      idClinica == 10 || idClinica == 13 || idClinica == 14 || idClinica == 16 || idClinica == 22)
                {
                    indEnvioAutorizacionFarmacia = true;
                }
                else if (idClinica == 100)
                {
                    indEnviaNumeroOperacion = true;
                    indEnvioAutorizacionFarmacia = true;
                }

                if (indEnviaNumeroOperacion && indEnvioAutorizacionFarmacia)
                {
                    varParametros = new SqlParameter[26];
                }
                else if (indEnviaNumeroOperacion || indEnvioAutorizacionFarmacia)
                {
                    varParametros = new SqlParameter[25];
                }
                else
                {
                    varParametros = new SqlParameter[24];
                }
                // SqlParameter[] varParametros = new SqlParameter[23];//se agrero entidades 16042021 18 a 22
                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(NumeroAutorizacion)) varParametros[0].Value = null;
                else varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@Sucursal", SqlDbType.Char);
                varParametros[1].Value = Sucursal;
                varParametros[2] = new SqlParameter("@TipoSeguro", SqlDbType.Int);
                if (TipoSeguro.Equals("NULL")) varParametros[2].Value = null;
                else varParametros[2].Value = TipoSeguro;
                varParametros[3] = new SqlParameter("@IDPaciente", SqlDbType.Int);
                varParametros[3].Value = IDPaciente;
                varParametros[4] = new SqlParameter("@IDAseguradora", SqlDbType.Int);
                if (String.IsNullOrEmpty(IDAseguradora) || IDAseguradora.Equals("NULL")) varParametros[4].Value = null;
                else varParametros[4].Value = IDAseguradora;
                varParametros[5] = new SqlParameter("@IDCitaSpring", SqlDbType.Int);
                varParametros[5].Value = IDCitaSpring;
                varParametros[6] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(iafas)) varParametros[6].Value = null;
                else varParametros[6].Value = iafas;
                varParametros[7] = new SqlParameter("@TipoComprobante", SqlDbType.VarChar);
                varParametros[7].Value = TipoComprobante;
                varParametros[8] = new SqlParameter("@SerieComprobante", SqlDbType.VarChar);
                varParametros[8].Value = SerieComprobante;
                //varParametros[8].Value = (TipoComprobante == "BV") ? "B001" : "F0001";
                varParametros[9] = new SqlParameter("@SerieCobranza", SqlDbType.VarChar);
                varParametros[9].Value = SerieCobranza;
                //varParametros[9].Value = "";
                varParametros[10] = new SqlParameter("@TipoTarjeta", SqlDbType.VarChar);
                varParametros[10].Value = TipoTarjeta;
                varParametros[11] = new SqlParameter("@Tarjeta", SqlDbType.VarChar);
                varParametros[11].Value = Tarjeta;
                //varParametros[11].Value = Tarjeta.Substring(Tarjeta.Length - 4);
                varParametros[12] = new SqlParameter("@NumeroAutorizacionNuevo", SqlDbType.VarChar);
                if (NumeroAutorizacionNuevo.Equals("NULL")) varParametros[12].Value = null;
                else varParametros[12].Value = NumeroAutorizacionNuevo;
                //varParametros[12].Value = null;
                varParametros[13] = new SqlParameter("@Usuario", SqlDbType.VarChar);
                varParametros[13].Value = "Clínica SF";
                varParametros[14] = new SqlParameter("@Monto", SqlDbType.Decimal);
                varParametros[14].Value = CodCopagoFijo;
                //varParametros[15] = new SqlParameter("@Firma", SqlDbType.Image, System.Convert.FromBase64String(firma.Replace("data:image/png;base64,", "")).Length);
                //varParametros[15].Value = System.Convert.FromBase64String(firma.Replace("data:image/png;base64,", ""));
                if (!String.IsNullOrEmpty(firma) && !firma.Equals("0x"))
                {
                    varParametros[15] = new SqlParameter("@Firma", SqlDbType.Image, System.Convert.FromBase64String(firma.Replace("data:image/png;base64,", "")).Length);
                    varParametros[15].Value = System.Convert.FromBase64String(firma.Replace("data:image/png;base64,", ""));
                }
                else
                {
                    firma = "";
                    varParametros[15] = new SqlParameter("@Firma", SqlDbType.Image, System.Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAMAAAC6V+0/AAADAFBMVEX///8AAAACAgIDAwMEBAQFBQUGBgYHBwcICAgJCQkKCgoLCwsMDAwNDQ0ODg4PDw8QEBARERESEhITExMUFBQVFRUWFhYXFxcYGBgZGRkaGhobGxscHBwdHR0eHh4fHx8gICAhISEiIiIjIyMkJCQlJSUmJiYnJycoKCgpKSkqKiorKyssLCwtLS0uLi4vLy8wMDAxMTEyMjIzMzM0NDQ1NTU2NjY3Nzc4ODg5OTk6Ojo7Ozs8PDw9PT0+Pj4/Pz9AQEBBQUFCQkJDQ0NERERFRUVGRkZHR0dISEhJSUlKSkpLS0tMTExNTU1OTk5PT09QUFBRUVFSUlJTU1NUVFRVVVVWVlZXV1dYWFhZWVlaWlpbW1tcXFxdXV1eXl5fX19gYGBhYWFiYmJjY2NkZGRlZWVmZmZnZ2doaGhpaWlqampra2tsbGxtbW1ubm5vb29wcHBxcXFycnJzc3N0dHR1dXV2dnZ3d3d4eHh5eXl6enp7e3t8fHx9fX1+fn5/f3+AgICBgYGCgoKDg4OEhISFhYWGhoaHh4eIiIiJiYmKioqLi4uMjIyNjY2Ojo6Pj4+QkJCRkZGSkpKTk5OUlJSVlZWWlpaXl5eYmJiZmZmampqbm5ucnJydnZ2enp6fn5+goKChoaGioqKjo6OkpKSlpaWmpqanp6eoqKipqamqqqqrq6usrKytra2urq6vr6+wsLCxsbGysrKzs7O0tLS1tbW2tra3t7e4uLi5ubm6urq7u7u8vLy9vb2+vr6/v7/AwMDBwcHCwsLDw8PExMTFxcXGxsbHx8fIyMjJycnKysrLy8vMzMzNzc3Ozs7Pz8/Q0NDR0dHS0tLT09PU1NTV1dXW1tbX19fY2NjZ2dna2trb29vc3Nzd3d3e3t7f39/g4ODh4eHi4uLj4+Pk5OTl5eXm5ubn5+fo6Ojp6enq6urr6+vs7Ozt7e3u7u7v7+/w8PDx8fHy8vLz8/P09PT19fX29vb39/f4+Pj5+fn6+vr7+/v8/Pz9/f3+/v7///+VceJeAAAAAXRSTlMAQObYZgAAAAFiS0dE/6UH8sUAAAAJcEhZcwAADsQAAA7EAZUrDhsAAAARSURBVHjaYziDBTCMCg4mQQCKbz7Q5y936AAAAABJRU5ErkJggg==").Length);
                    varParametros[15].Value = System.Convert.FromBase64String(firma.Replace("data:image/png;base64,", ""));
                }
                //varParametros[15].Value = System.Convert.FromBase64String(firma.Replace("data:image/png;base64,", ""));
                varParametros[16] = new SqlParameter("@Tipo", SqlDbType.Int);
                varParametros[16].Value = tipo;
                varParametros[17] = new SqlParameter("@Estado", SqlDbType.Int);
                varParametros[17].Value = estado;
                //se agrero entidades 16042021
                varParametros[18] = new SqlParameter("@RUC", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(RUC)) varParametros[18].Value = null;
                else varParametros[18].Value = RUC;
                varParametros[19] = new SqlParameter("@RazonSocial", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(RazonSocial)) varParametros[19].Value = null;
                else varParametros[19].Value = RazonSocial;
                varParametros[20] = new SqlParameter("@Direccion", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(Direccion)) varParametros[20].Value = null;
                else varParametros[20].Value = Direccion;
                varParametros[21] = new SqlParameter("@Email", SqlDbType.VarChar);
                if (String.IsNullOrEmpty(Email)) varParametros[21].Value = null;
                else varParametros[21].Value = Email;
                varParametros[22] = new SqlParameter("@TipoPacienteChqMed", SqlDbType.Int);
                if (String.IsNullOrEmpty(TipoPaciente)) varParametros[22].Value = null;
                else varParametros[22].Value = int.Parse(TipoPaciente);
                varParametros[23] = new SqlParameter("@NumeroContrato", SqlDbType.Int);
                varParametros[23].Value = NumeroContrato;

                if (indEnviaNumeroOperacion && indEnvioAutorizacionFarmacia)
                {
                    varParametros[24] = new SqlParameter("@NumeroOperacion", SqlDbType.VarChar);
                    if (String.IsNullOrEmpty(NumeroOperacion)) varParametros[24].Value = null;
                    else varParametros[24].Value = NumeroOperacion;

                    varParametros[25] = new SqlParameter("@NumeroAutorizacionFarmacia", SqlDbType.VarChar);
                    if (String.IsNullOrEmpty(NumeroAutorizacionFarmacia)) varParametros[25].Value = null;
                    else varParametros[25].Value = NumeroAutorizacionFarmacia;
                }
                else if (indEnviaNumeroOperacion)
                {
                    varParametros[24] = new SqlParameter("@NumeroOperacion", SqlDbType.VarChar);
                    if (String.IsNullOrEmpty(NumeroOperacion)) varParametros[24].Value = null;
                    else varParametros[24].Value = NumeroOperacion;
                }
                else if (indEnvioAutorizacionFarmacia)
                {
                    varParametros[24] = new SqlParameter("@NumeroAutorizacionFarmacia", SqlDbType.VarChar);
                    if (String.IsNullOrEmpty(NumeroAutorizacionFarmacia)) varParametros[24].Value = null;
                    else varParametros[24].Value = NumeroAutorizacionFarmacia;
                }
                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspGrabarCitasTransaccionales", varParametros, TipoProcesamiento.DataReader, false, CadenaClinica(idClinica));
                if (varDataReader.HasRows || String.IsNullOrEmpty(NumeroAutorizacion))
                {
                    varDataReader.Read();
                    varResultado.Add("IdTransaccion", varDataReader.GetInt64(varDataReader.GetOrdinal("IdTransaccion")).ToString());
                    varResultado.Add("IdClinica", idClinica.ToString());
                    return varResultado;
                }
                else
                {
                    return varResultado;
                }

            }
            catch (Exception ex)
            {
                new ErrorDA().RegistrarError(ex, "WSV", "GrabarTransaccionSpring", idCita + "¯" + data);
                return varResultado;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }
        public bool GrabarDatosGeneralesVirtual(string data, string idCita)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                string[] parametros = data.Split('¦');
                string NumeroAutorizacion = parametros[1];
                string ApellidoMaternoAfiliado = parametros[2];
                string ApellidoMaternoTitular = parametros[3];
                string ApellidoPaternoAfiliado = parametros[4];
                string ApellidoPaternoTitular = parametros[5];
                string CodEstado = parametros[6];
                string CodEstadoCivil = parametros[7];
                string CodFechaActualizacionFoto = parametros[8];
                string CodGenero = parametros[9];
                string CodMoneda = parametros[10];
                string CodProducto = parametros[11];
                string CodTipoAfiliacion = parametros[12];
                string CodTipoDocumentoAfiliado = parametros[13];
                string CodTipoDocumentoContratante = parametros[14];
                string CodTipoDocumentoTitular = parametros[15];
                string CodigoAfiliado = parametros[16];
                string CodigoTitular = parametros[17];
                string DesEstado = parametros[18];
                string DesProducto = parametros[19];
                string Edad = parametros[20];
                string FechaFinVigencia = parametros[21];
                string FechaInicioVigencia = parametros[22];
                string FechaNacimiento = parametros[23];
                string NombreContratante = parametros[24];
                string NombresAfiliado = parametros[25];
                string NombresTitular = parametros[26];
                string NumeroCertificado = parametros[27];
                string NumeroDocumentoAfiliado = parametros[28];
                string NumeroDocumentoContratante = parametros[29];
                string NumeroDocumentoTitular = parametros[30];
                string NumeroPlan = parametros[31];
                string NumeroPoliza = parametros[32];
                string IdReceptor = parametros[33];
                string iafas = parametros[34];
                string CodParentesco = parametros[35];
                string RUClinica = parametros[36];
                string Tarjeta = parametros[37];
                string NumeroContrato = parametros[38];
                string CondicionesEspeciales = parametros[39];
                string Observaciones = parametros[40];
                string FechaAfiliacion = parametros[41];
                string NumeroAutorizacionNuevo = parametros[42];
                int idClinica = int.Parse(parametros[43]);

                SqlParameter[] varParametros = new SqlParameter[42];
                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@ApellidoMaternoAfiliado", SqlDbType.VarChar);
                varParametros[1].Value = ApellidoMaternoAfiliado;
                varParametros[2] = new SqlParameter("@ApellidoMaternoTitular", SqlDbType.VarChar);
                varParametros[2].Value = ApellidoMaternoTitular;
                varParametros[3] = new SqlParameter("@ApellidoPaternoAfiliado", SqlDbType.VarChar);
                varParametros[3].Value = ApellidoPaternoAfiliado;
                varParametros[4] = new SqlParameter("@ApellidoPaternoTitular", SqlDbType.VarChar);
                varParametros[4].Value = ApellidoPaternoTitular;
                varParametros[5] = new SqlParameter("@CodEstado", SqlDbType.VarChar);
                varParametros[5].Value = CodEstado;
                varParametros[6] = new SqlParameter("@CodEstadoCivil", SqlDbType.VarChar);
                varParametros[6].Value = CodEstadoCivil;
                varParametros[7] = new SqlParameter("@CodFechaActualizacionFoto", SqlDbType.VarChar);
                varParametros[7].Value = CodFechaActualizacionFoto;
                varParametros[8] = new SqlParameter("@CodGenero", SqlDbType.VarChar);
                varParametros[8].Value = CodGenero;
                varParametros[9] = new SqlParameter("@CodMoneda", SqlDbType.VarChar);
                varParametros[9].Value = CodMoneda;
                varParametros[10] = new SqlParameter("@CodProducto", SqlDbType.VarChar);
                varParametros[10].Value = CodProducto;
                varParametros[11] = new SqlParameter("@CodTipoAfiliacion", SqlDbType.VarChar);
                varParametros[11].Value = CodTipoAfiliacion;
                varParametros[12] = new SqlParameter("@CodTipoDocumentoAfiliado", SqlDbType.VarChar);
                varParametros[12].Value = CodTipoDocumentoAfiliado;
                varParametros[13] = new SqlParameter("@CodTipoDocumentoContratante", SqlDbType.VarChar);
                varParametros[13].Value = CodTipoDocumentoContratante;
                varParametros[14] = new SqlParameter("@CodTipoDocumentoTitular", SqlDbType.VarChar);
                varParametros[14].Value = CodTipoDocumentoTitular;
                varParametros[15] = new SqlParameter("@CodigoAfiliado", SqlDbType.VarChar);
                varParametros[15].Value = CodigoAfiliado;
                varParametros[16] = new SqlParameter("@CodigoTitular", SqlDbType.VarChar);
                varParametros[16].Value = CodigoTitular;
                varParametros[17] = new SqlParameter("@DesEstado", SqlDbType.VarChar);
                varParametros[17].Value = DesEstado;
                varParametros[18] = new SqlParameter("@DesProducto", SqlDbType.VarChar);
                varParametros[18].Value = DesProducto;
                varParametros[19] = new SqlParameter("@Edad", SqlDbType.SmallInt);
                varParametros[19].Value = Edad;
                varParametros[20] = new SqlParameter("@FechaFinVigencia", SqlDbType.VarChar);
                varParametros[20].Value = FechaFinVigencia;
                varParametros[21] = new SqlParameter("@FechaInicioVigencia", SqlDbType.VarChar);
                varParametros[21].Value = FechaInicioVigencia;
                varParametros[22] = new SqlParameter("@FechaNacimiento", SqlDbType.VarChar);
                varParametros[22].Value = FechaNacimiento;
                varParametros[23] = new SqlParameter("@NombreContratante", SqlDbType.VarChar);
                varParametros[23].Value = NombreContratante;
                varParametros[24] = new SqlParameter("@NombresAfiliado", SqlDbType.VarChar);
                varParametros[24].Value = NombresAfiliado;
                varParametros[25] = new SqlParameter("@NombresTitular", SqlDbType.VarChar);
                varParametros[25].Value = NombresTitular;
                varParametros[26] = new SqlParameter("@NumeroCertificado", SqlDbType.VarChar);
                varParametros[26].Value = NumeroCertificado;
                varParametros[27] = new SqlParameter("@NumeroDocumentoAfiliado", SqlDbType.VarChar);
                varParametros[27].Value = NumeroDocumentoAfiliado;
                varParametros[28] = new SqlParameter("@NumeroDocumentoContratante", SqlDbType.VarChar);
                varParametros[28].Value = NumeroDocumentoContratante;
                varParametros[29] = new SqlParameter("@NumeroDocumentoTitular", SqlDbType.VarChar);
                varParametros[29].Value = NumeroDocumentoTitular;
                varParametros[30] = new SqlParameter("@NumeroPlan ", SqlDbType.VarChar);
                varParametros[30].Value = NumeroPlan;
                varParametros[31] = new SqlParameter("@NumeroPoliza", SqlDbType.VarChar);
                varParametros[31].Value = NumeroPoliza;
                varParametros[32] = new SqlParameter("@IdReceptor", SqlDbType.VarChar);
                varParametros[32].Value = IdReceptor;

                varParametros[33] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                varParametros[33].Value = iafas;
                varParametros[34] = new SqlParameter("@CodParentesco", SqlDbType.VarChar);
                varParametros[34].Value = CodParentesco;
                varParametros[35] = new SqlParameter("@RUCClinica", SqlDbType.VarChar);
                varParametros[35].Value = RUClinica;
                varParametros[36] = new SqlParameter("@Tarjeta", SqlDbType.VarChar);
                varParametros[36].Value = Tarjeta;
                varParametros[37] = new SqlParameter("@NumeroContrato", SqlDbType.VarChar);
                varParametros[37].Value = NumeroContrato;
                varParametros[38] = new SqlParameter("@Condiciones", SqlDbType.VarChar);
                varParametros[38].Value = CondicionesEspeciales;
                varParametros[39] = new SqlParameter("@Observaciones", SqlDbType.VarChar);
                varParametros[39].Value = Observaciones;
                varParametros[40] = new SqlParameter("@FechaAfiliacion", SqlDbType.VarChar);
                varParametros[40].Value = Observaciones;
                varParametros[41] = new SqlParameter("@NumeroAutorizacionNuevo", SqlDbType.VarChar);
                if (NumeroAutorizacionNuevo.Equals("NULL")) varParametros[41].Value = null;
                else varParametros[41].Value = NumeroAutorizacionNuevo;
                //varParametros[41].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspSITEDS_GrabarDatosGenerales", varParametros, TipoProcesamiento.DataReader, false);//, CadenaClinica(idClinica));

                if (varDataReader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                //DateTime now = DateTime.Now;
                //string Archivo = $"{"ErrorGrabarDatosGeneralesVirtual"}_{now.Year}_{now.Month.ToString().PadLeft(2, '0')}_{now.Day.ToString().PadLeft(2, '0')}{".txt"}";
                //string rutaArchivo = string.Format("{0}{1}", ConfigurationManager.AppSettings["LogCitaBDSpring"], Archivo);
                //string nombreArchivo = "";
                //using (FileStream stream = new FileStream(rutaArchivo + nombreArchivo, FileMode.Append, FileAccess.Write, FileShare.Write))
                //{
                //    using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.Default))
                //    {
                //        streamWriter.WriteLine("Fecha = " + DateTime.Now);
                //        streamWriter.WriteLine("Mensaje = " + ex.Message);
                //        streamWriter.WriteLine("Mensaje = " + ex.StackTrace);
                //        streamWriter.WriteLine(new string('_', 50));
                //    }
                //}
                new ErrorDA().RegistrarError(ex, "WSV", "GrabarDatosGeneralesVirtual", idCita + "¯" + data);
                return false;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }
        public bool GrabarCoberturaAcreditacionVirtual(string data, string idCita)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                string[] parametros = data.Split('¦');
                string NumeroAutorizacion = parametros[1];
                string Beneficios = parametros[2];
                string CodIndicadorRestriccion = parametros[3];
                string BeneficioMaximoInicial = parametros[4];
                string Observaciones = parametros[5];
                string CodigoTipoCobertura = parametros[6];
                string CodigoSubTipoCobertura = parametros[7];
                string CodCopagoFijo = parametros[8];
                string CodCopagoVariable = parametros[9];
                string CodCalificacionServicio = parametros[10];
                string CodTipoMoneda = parametros[11];
                string iafas = parametros[12];
                string NumeroAutorizacionNuevo = parametros[13];
                int idClinica = int.Parse(parametros[14]);

                SqlParameter[] varParametros = new SqlParameter[13];
                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@Beneficios", SqlDbType.VarChar);
                varParametros[1].Value = Beneficios;
                varParametros[2] = new SqlParameter("@CodIndicadorRestriccion", SqlDbType.VarChar);
                varParametros[2].Value = CodIndicadorRestriccion;
                varParametros[3] = new SqlParameter("@BeneficioMaximoInicial", SqlDbType.Int);
                varParametros[3].Value = Convert.ToInt32(Convert.ToDouble(BeneficioMaximoInicial));
                varParametros[4] = new SqlParameter("@Observaciones", SqlDbType.Text);
                varParametros[4].Value = Observaciones;
                varParametros[5] = new SqlParameter("@CodigoTipoCobertura", SqlDbType.VarChar);
                varParametros[5].Value = CodigoTipoCobertura;
                varParametros[6] = new SqlParameter("@CodigoSubTipoCobertura", SqlDbType.VarChar);
                varParametros[6].Value = CodigoSubTipoCobertura;
                varParametros[7] = new SqlParameter("@CodCopagoFijo", SqlDbType.Decimal);
                varParametros[7].Value = CodCopagoFijo;
                varParametros[8] = new SqlParameter("@CodCopagoVariable", SqlDbType.Decimal);
                varParametros[8].Value = CodCopagoVariable;
                varParametros[9] = new SqlParameter("@CodCalificacionServicio", SqlDbType.VarChar);
                varParametros[9].Value = CodCalificacionServicio;
                varParametros[10] = new SqlParameter("@CodTipoMoneda", SqlDbType.VarChar);
                varParametros[10].Value = CodTipoMoneda;
                varParametros[11] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                varParametros[11].Value = iafas;
                varParametros[12] = new SqlParameter("@NumeroAutorizacionNuevo", SqlDbType.VarChar);
                if (NumeroAutorizacionNuevo.Equals("NULL")) varParametros[12].Value = null;
                else varParametros[12].Value = NumeroAutorizacionNuevo;

                //varParametros[12].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspSITEDS_GrabarCoberturaAcreditacion", varParametros, TipoProcesamiento.DataReader, false);//, CadenaClinica(idClinica));

                if (varDataReader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                //DateTime now = DateTime.Now;
                //string Archivo = $"{"ErrorGrabarCoberturaAcreditacionVirtual"}_{now.Year}_{now.Month.ToString().PadLeft(2, '0')}_{now.Day.ToString().PadLeft(2, '0')}{".txt"}";
                //string rutaArchivo = string.Format("{0}{1}", ConfigurationManager.AppSettings["LogCitaBDSpring"], Archivo);
                //string nombreArchivo = "";
                //using (FileStream stream = new FileStream(rutaArchivo + nombreArchivo, FileMode.Append, FileAccess.Write, FileShare.Write))
                //{
                //    using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.Default))
                //    {
                //        streamWriter.WriteLine("Fecha = " + DateTime.Now);
                //        streamWriter.WriteLine("Mensaje = " + ex.Message);
                //        streamWriter.WriteLine("Mensaje = " + ex.StackTrace);
                //        streamWriter.WriteLine(new string('_', 50));
                //    }
                //}
                new ErrorDA().RegistrarError(ex, "WSV", "GrabarCoberturaAcreditacionVirtual", idCita + "¯" + data);
                return false;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }
        public bool GrabarProcedimientosVirtual(string data, string idCita)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                string[] parametros = data.Split('¦');
                string NumeroAutorizacion = parametros[1];
                string CodigoTipoCobertura = parametros[2];
                string CodigoSubTipoCobertura = parametros[3];
                string Codigo = parametros[4];
                string Procedimiento = parametros[5];
                string Genero = parametros[6];
                string DesCopagoFijo = parametros[7];
                string DesCopagoVariable = parametros[8];
                string Frecuencia = parametros[9];
                string Tiempo = parametros[10];
                string Observaciones = parametros[11];
                string iafas = parametros[12];
                string NumeroAutorizacionNuevo = parametros[13];
                int idClinica = int.Parse(parametros[14]);

                SqlParameter[] varParametros = new SqlParameter[13];
                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@CodigoTipoCobertura", SqlDbType.VarChar);
                varParametros[1].Value = CodigoTipoCobertura;
                varParametros[2] = new SqlParameter("@CodigoSubTipoCobertura", SqlDbType.VarChar);
                varParametros[2].Value = CodigoSubTipoCobertura;
                varParametros[3] = new SqlParameter("@Codigo", SqlDbType.VarChar);
                varParametros[3].Value = Codigo;
                varParametros[4] = new SqlParameter("@Procedimiento", SqlDbType.VarChar);
                varParametros[4].Value = Procedimiento;
                varParametros[5] = new SqlParameter("@Genero", SqlDbType.SmallInt);
                varParametros[5].Value = Genero;
                varParametros[6] = new SqlParameter("@DesCopagoFijo", SqlDbType.Decimal);
                varParametros[6].Value = DesCopagoFijo;
                varParametros[7] = new SqlParameter("@DesCopagoVariable", SqlDbType.Decimal);
                varParametros[7].Value = DesCopagoVariable;
                varParametros[8] = new SqlParameter("@Frecuencia", SqlDbType.SmallInt);
                varParametros[8].Value = Frecuencia;
                varParametros[9] = new SqlParameter("@Tiempo", SqlDbType.SmallInt);
                varParametros[9].Value = Tiempo;
                varParametros[10] = new SqlParameter("@Observaciones", SqlDbType.Text);
                varParametros[10].Value = Observaciones;
                varParametros[11] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                varParametros[11].Value = iafas;
                varParametros[12] = new SqlParameter("@NumeroAutorizacionNuevo", SqlDbType.VarChar);
                if (NumeroAutorizacionNuevo.Equals("NULL")) varParametros[12].Value = null;
                else varParametros[12].Value = NumeroAutorizacionNuevo;
                //varParametros[12].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspSITEDS_GrabarProcedimientos", varParametros, TipoProcesamiento.DataReader, false);//, CadenaClinica(idClinica));

                if (varDataReader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                //DateTime now = DateTime.Now;
                //string Archivo = $"{"ErrorGrabarProcedimientosVirtual"}_{now.Year}_{now.Month.ToString().PadLeft(2, '0')}_{now.Day.ToString().PadLeft(2, '0')}{".txt"}";
                //string rutaArchivo = string.Format("{0}{1}", ConfigurationManager.AppSettings["LogCitaBDSpring"], Archivo);
                //string nombreArchivo = "";
                //using (FileStream stream = new FileStream(rutaArchivo + nombreArchivo, FileMode.Append, FileAccess.Write, FileShare.Write))
                //{
                //    using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.Default))
                //    {
                //        streamWriter.WriteLine("Fecha = " + DateTime.Now);
                //        streamWriter.WriteLine("Mensaje = " + ex.Message);
                //        streamWriter.WriteLine("Mensaje = " + ex.StackTrace);
                //        streamWriter.WriteLine(new string('_', 50));
                //    }
                //}
                new ErrorDA().RegistrarError(ex, "WSV", "GrabarProcedimientosVirtual", idCita + "¯" + data);
                return false;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }
        public bool GrabarCondicionesMedicaAntecedentesVirtual(string data, string idCita)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                string[] parametros = data.Split('¦');
                string NumeroAutorizacion = parametros[1];
                string Codigo = parametros[2];
                string Diagnostico = parametros[3];
                string Observaciones = parametros[4];
                string iafas = parametros[5];
                string NumeroAutorizacionNuevo = parametros[6];
                int idClinica = int.Parse(parametros[7]);

                SqlParameter[] varParametros = new SqlParameter[6];
                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@Codigo", SqlDbType.VarChar);
                varParametros[1].Value = Codigo;
                varParametros[2] = new SqlParameter("@Diagnostico", SqlDbType.VarChar);
                varParametros[2].Value = Diagnostico;
                varParametros[3] = new SqlParameter("@Observaciones", SqlDbType.Text);
                varParametros[3].Value = Observaciones;
                varParametros[4] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                varParametros[4].Value = iafas;
                varParametros[5] = new SqlParameter("@NumeroAutorizacionNuevo", SqlDbType.VarChar);
                if (NumeroAutorizacionNuevo.Equals("NULL")) varParametros[5].Value = null;
                else varParametros[5].Value = NumeroAutorizacionNuevo;
                //varParametros[5].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspSITEDS_GrabarCondicionesMedicaAntecedentes", varParametros, TipoProcesamiento.DataReader, false);//, CadenaClinica(idClinica));

                if (varDataReader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                //DateTime now = DateTime.Now;
                //string Archivo = $"{"ErrorGrabarCondicionesMedicaAntecedentesVirtual"}_{now.Year}_{now.Month.ToString().PadLeft(2, '0')}_{now.Day.ToString().PadLeft(2, '0')}{".txt"}";
                //string rutaArchivo = string.Format("{0}{1}", ConfigurationManager.AppSettings["LogCitaBDSpring"], Archivo);
                //string nombreArchivo = "";
                //using (FileStream stream = new FileStream(rutaArchivo + nombreArchivo, FileMode.Append, FileAccess.Write, FileShare.Write))
                //{
                //    using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.Default))
                //    {
                //        streamWriter.WriteLine("Fecha = " + DateTime.Now);
                //        streamWriter.WriteLine("Mensaje = " + ex.Message);
                //        streamWriter.WriteLine("Mensaje = " + ex.StackTrace);
                //        streamWriter.WriteLine(new string('_', 50));
                //    }
                //}
                new ErrorDA().RegistrarError(ex, "WSV", "GrabarCondicionesMedicaAntecedentesVirtual", idCita + "¯" + data);
                return false;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }
        public bool GrabarCondicionesMedicaCarenciaVirtual(string data, string idCita)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                string[] parametros = data.Split('¦');
                string NumeroAutorizacion = parametros[1];
                string Codigo = parametros[2];
                string Diagnostico = parametros[3];
                string Observaciones = parametros[4];
                string iafas = parametros[5];
                string NumeroAutorizacionNuevo = parametros[6];
                int idClinica = int.Parse(parametros[7]);

                SqlParameter[] varParametros = new SqlParameter[6];
                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@Codigo", SqlDbType.VarChar);
                varParametros[1].Value = Codigo;
                varParametros[2] = new SqlParameter("@Diagnostico", SqlDbType.VarChar);
                varParametros[2].Value = Diagnostico;
                varParametros[3] = new SqlParameter("@Observaciones", SqlDbType.Text);
                varParametros[3].Value = Observaciones;
                varParametros[4] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                varParametros[4].Value = iafas;
                varParametros[5] = new SqlParameter("@NumeroAutorizacionNuevo", SqlDbType.VarChar);
                if (NumeroAutorizacionNuevo.Equals("NULL")) varParametros[5].Value = null;
                else varParametros[5].Value = NumeroAutorizacionNuevo;
                //varParametros[5].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspSITEDS_GrabarCondicionesMedicaCarencia", varParametros, TipoProcesamiento.DataReader, false);//, CadenaClinica(idClinica));

                if (varDataReader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                //DateTime now = DateTime.Now;
                //string Archivo = $"{"ErrorGrabarCondicionesMedicaCarenciaVirtual"}_{now.Year}_{now.Month.ToString().PadLeft(2, '0')}_{now.Day.ToString().PadLeft(2, '0')}{".txt"}";
                //string rutaArchivo = string.Format("{0}{1}", ConfigurationManager.AppSettings["LogCitaBDSpring"], Archivo);
                //string nombreArchivo = "";
                //using (FileStream stream = new FileStream(rutaArchivo + nombreArchivo, FileMode.Append, FileAccess.Write, FileShare.Write))
                //{
                //    using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.Default))
                //    {
                //        streamWriter.WriteLine("Fecha = " + DateTime.Now);
                //        streamWriter.WriteLine("Mensaje = " + ex.Message);
                //        streamWriter.WriteLine("Mensaje = " + ex.StackTrace);
                //        streamWriter.WriteLine(new string('_', 50));
                //    }
                //}
                new ErrorDA().RegistrarError(ex, "WSV", "GrabarCondicionesMedicaCarenciaVirtual", idCita + "¯" + data);
                return false;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }
        public bool GrabarCondicionesMedicaEnfermedadVirtual(string data, string idCita)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                string[] parametros = data.Split('¦');
                string NumeroAutorizacion = parametros[1];
                string Codigo = parametros[2];
                string Diagnostico = parametros[3];
                string Observaciones = parametros[4];
                string iafas = parametros[5];
                string NumeroAutorizacionNuevo = parametros[6];
                int idClinica = int.Parse(parametros[7]);

                SqlParameter[] varParametros = new SqlParameter[6];
                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@Codigo", SqlDbType.VarChar);
                varParametros[1].Value = Codigo;
                varParametros[2] = new SqlParameter("@Diagnostico", SqlDbType.VarChar);
                varParametros[2].Value = Diagnostico;
                varParametros[3] = new SqlParameter("@Observaciones", SqlDbType.Text);
                varParametros[3].Value = Observaciones;
                varParametros[4] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                varParametros[4].Value = iafas;
                varParametros[5] = new SqlParameter("@NumeroAutorizacionNuevo", SqlDbType.VarChar);
                if (NumeroAutorizacionNuevo.Equals("NULL")) varParametros[5].Value = null;
                else varParametros[5].Value = NumeroAutorizacionNuevo;
                //varParametros[5].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspSITEDS_GrabarCondicionesMedicaEnfermedad", varParametros, TipoProcesamiento.DataReader, false);//, CadenaClinica(idClinica));

                if (varDataReader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                //DateTime now = DateTime.Now;
                //string Archivo = $"{"ErrorGrabarCondicionesMedicaEnfermedadVirtual"}_{now.Year}_{now.Month.ToString().PadLeft(2, '0')}_{now.Day.ToString().PadLeft(2, '0')}{".txt"}";
                //string rutaArchivo = string.Format("{0}{1}", ConfigurationManager.AppSettings["LogCitaBDSpring"], Archivo);
                //string nombreArchivo = "";
                //using (FileStream stream = new FileStream(rutaArchivo + nombreArchivo, FileMode.Append, FileAccess.Write, FileShare.Write))
                //{
                //    using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.Default))
                //    {
                //        streamWriter.WriteLine("Fecha = " + DateTime.Now);
                //        streamWriter.WriteLine("Mensaje = " + ex.Message);
                //        streamWriter.WriteLine("Mensaje = " + ex.StackTrace);
                //        streamWriter.WriteLine(new string('_', 50));
                //    }
                //}
                new ErrorDA().RegistrarError(ex, "WSV", "GrabarCondicionesMedicaEnfermedadVirtual", idCita + "¯" + data);
                return false;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }
        public bool GrabarCondicionesMedicaExclusionesVirtual(string data, string idCita)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                string[] parametros = data.Split('¦');
                string NumeroAutorizacion = parametros[1];
                string Codigo = parametros[2];
                string Diagnostico = parametros[3];
                string Observaciones = parametros[4];
                string iafas = parametros[5];
                string NumeroAutorizacionNuevo = parametros[6];
                int idClinica = int.Parse(parametros[7]);

                SqlParameter[] varParametros = new SqlParameter[6];
                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@Codigo", SqlDbType.VarChar);
                varParametros[1].Value = Codigo;
                varParametros[2] = new SqlParameter("@Diagnostico", SqlDbType.VarChar);
                varParametros[2].Value = Diagnostico;
                varParametros[3] = new SqlParameter("@Observaciones", SqlDbType.Text);
                varParametros[3].Value = Observaciones;
                varParametros[4] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                varParametros[4].Value = iafas;
                varParametros[5] = new SqlParameter("@NumeroAutorizacionNuevo", SqlDbType.VarChar);
                if (NumeroAutorizacionNuevo.Equals("NULL")) varParametros[5].Value = null;
                else varParametros[5].Value = NumeroAutorizacionNuevo;
                //varParametros[5].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspSITEDS_GrabarCondicionesMedicaExclusiones", varParametros, TipoProcesamiento.DataReader, false);//, CadenaClinica(idClinica));

                if (varDataReader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                //DateTime now = DateTime.Now;
                //string Archivo = $"{"ErrorGrabarCondicionesMedicaExclusionesVirtual"}_{now.Year}_{now.Month.ToString().PadLeft(2, '0')}_{now.Day.ToString().PadLeft(2, '0')}{".txt"}";
                //string rutaArchivo = string.Format("{0}{1}", ConfigurationManager.AppSettings["LogCitaBDSpring"], Archivo);
                //string nombreArchivo = "";
                //using (FileStream stream = new FileStream(rutaArchivo + nombreArchivo, FileMode.Append, FileAccess.Write, FileShare.Write))
                //{
                //    using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.Default))
                //    {
                //        streamWriter.WriteLine("Fecha = " + DateTime.Now);
                //        streamWriter.WriteLine("Mensaje = " + ex.Message);
                //        streamWriter.WriteLine("Mensaje = " + ex.StackTrace);
                //        streamWriter.WriteLine(new string('_', 50));
                //    }
                //}
                new ErrorDA().RegistrarError(ex, "WSV", "GrabarCondicionesMedicaExclusionesVirtual", idCita + "¯" + data);
                return false;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }
        public bool GrabarCondicionesMedicaPreexistenciaVirtual(string data, string idCita)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                string[] parametros = data.Split('¦');
                string NumeroAutorizacion = parametros[1];
                string Codigo = parametros[2];
                string Diagnostico = parametros[3];
                string Observaciones = parametros[4];
                string iafas = parametros[5];
                string NumeroAutorizacionNuevo = parametros[6];
                int idClinica = int.Parse(parametros[7]);

                SqlParameter[] varParametros = new SqlParameter[6];
                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@Codigo", SqlDbType.VarChar);
                varParametros[1].Value = Codigo;
                varParametros[2] = new SqlParameter("@Diagnostico", SqlDbType.VarChar);
                varParametros[2].Value = Diagnostico;
                varParametros[3] = new SqlParameter("@Observaciones", SqlDbType.Text);
                varParametros[3].Value = Observaciones;
                varParametros[4] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                varParametros[4].Value = iafas;
                varParametros[5] = new SqlParameter("@NumeroAutorizacionNuevo", SqlDbType.VarChar);
                if (NumeroAutorizacionNuevo.Equals("NULL")) varParametros[5].Value = null;
                else varParametros[5].Value = NumeroAutorizacionNuevo;
                //varParametros[5].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspSITEDS_GrabarCondicionesMedicaPreexistencia", varParametros, TipoProcesamiento.DataReader, false);//, CadenaClinica(idClinica));

                if (varDataReader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                //DateTime now = DateTime.Now;
                //string Archivo = $"{"ErrorGrabarCondicionesMedicaPreexistenciaVirtual"}_{now.Year}_{now.Month.ToString().PadLeft(2, '0')}_{now.Day.ToString().PadLeft(2, '0')}{".txt"}";
                //string rutaArchivo = string.Format("{0}{1}", ConfigurationManager.AppSettings["LogCitaBDSpring"], Archivo);
                //string nombreArchivo = "";
                //using (FileStream stream = new FileStream(rutaArchivo + nombreArchivo, FileMode.Append, FileAccess.Write, FileShare.Write))
                //{
                //    using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.Default))
                //    {
                //        streamWriter.WriteLine("Fecha = " + DateTime.Now);
                //        streamWriter.WriteLine("Mensaje = " + ex.Message);
                //        streamWriter.WriteLine("Mensaje = " + ex.StackTrace);
                //        streamWriter.WriteLine(new string('_', 50));
                //    }
                //}
                new ErrorDA().RegistrarError(ex, "WSV", "GrabarCondicionesMedicaPreexistenciaVirtual", idCita + "¯" + data);
                return false;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }
        public bool GrabarExcepcionCarenciaVirtual(string data, string idCita)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                string[] parametros = data.Split('¦');
                string NumeroAutorizacion = parametros[1];
                string CodigoTipoCobertura = parametros[2];
                string CodigoSubTipoCobertura = parametros[3];
                string Codigo = parametros[4];
                string GrupoDiagnostico = parametros[5];
                string Observaciones = parametros[6];
                string iafas = parametros[7];
                string NumeroAutorizacionNuevo = parametros[8];
                int idClinica = int.Parse(parametros[9]);

                SqlParameter[] varParametros = new SqlParameter[8];
                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@CodigoTipoCobertura", SqlDbType.VarChar);
                varParametros[1].Value = CodigoTipoCobertura;
                varParametros[2] = new SqlParameter("@CodigoSubTipoCobertura", SqlDbType.VarChar);
                varParametros[2].Value = CodigoSubTipoCobertura;
                varParametros[3] = new SqlParameter("@Codigo", SqlDbType.VarChar);
                varParametros[3].Value = Codigo;
                varParametros[4] = new SqlParameter("@GrupoDiagnostico", SqlDbType.VarChar);
                varParametros[4].Value = GrupoDiagnostico;
                varParametros[5] = new SqlParameter("@Observaciones", SqlDbType.Text);
                varParametros[5].Value = Observaciones;
                varParametros[6] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                varParametros[6].Value = iafas;
                varParametros[7] = new SqlParameter("@NumeroAutorizacionNuevo", SqlDbType.VarChar);
                if (NumeroAutorizacionNuevo.Equals("NULL")) varParametros[7].Value = null;
                else varParametros[7].Value = NumeroAutorizacionNuevo;
                //varParametros[7].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspSITEDS_GrabarExcepcionCarencia", varParametros, TipoProcesamiento.DataReader, false);//, CadenaClinica(idClinica));

                if (varDataReader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                //DateTime now = DateTime.Now;
                //string Archivo = $"{"ErrorGrabarExcepcionCarenciaVirtual"}_{now.Year}_{now.Month.ToString().PadLeft(2, '0')}_{now.Day.ToString().PadLeft(2, '0')}{".txt"}";
                //string rutaArchivo = string.Format("{0}{1}", ConfigurationManager.AppSettings["LogCitaBDSpring"], Archivo);
                //string nombreArchivo = "";
                //using (FileStream stream = new FileStream(rutaArchivo + nombreArchivo, FileMode.Append, FileAccess.Write, FileShare.Write))
                //{
                //    using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.Default))
                //    {
                //        streamWriter.WriteLine("Fecha = " + DateTime.Now);
                //        streamWriter.WriteLine("Mensaje = " + ex.Message);
                //        streamWriter.WriteLine("Mensaje = " + ex.StackTrace);
                //        streamWriter.WriteLine(new string('_', 50));
                //    }
                //}
                new ErrorDA().RegistrarError(ex, "WSV", "GrabarExcepcionCarenciaVirtual", idCita + "¯" + data);
                return false;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }
        public bool GrabarTiempoEsperaVirtual(string data, string idCita)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                string[] parametros = data.Split('¦');
                string NumeroAutorizacion = parametros[1];
                string CodigoTipoCobertura = parametros[2];
                string CodigoSubTipoCobertura = parametros[3];
                string Codigo = parametros[4];
                string GrupoDiagnostico = parametros[5];
                string Observaciones = parametros[6];
                string iafas = parametros[7];
                string NumeroAutorizacionNuevo = parametros[8];
                int idClinica = int.Parse(parametros[9]);

                SqlParameter[] varParametros = new SqlParameter[8];
                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@CodigoTipoCobertura", SqlDbType.VarChar);
                varParametros[1].Value = CodigoTipoCobertura;
                varParametros[2] = new SqlParameter("@CodigoSubTipoCobertura", SqlDbType.VarChar);
                varParametros[2].Value = CodigoSubTipoCobertura;
                varParametros[3] = new SqlParameter("@Codigo", SqlDbType.VarChar);
                varParametros[3].Value = Codigo;
                varParametros[4] = new SqlParameter("@GrupoDiagnostico", SqlDbType.VarChar);
                varParametros[4].Value = GrupoDiagnostico;
                varParametros[5] = new SqlParameter("@Observaciones", SqlDbType.Text);
                varParametros[5].Value = Observaciones;
                varParametros[6] = new SqlParameter("@IAFAS", SqlDbType.VarChar);
                varParametros[6].Value = iafas;
                varParametros[7] = new SqlParameter("@NumeroAutorizacionNuevo", SqlDbType.VarChar);
                if (NumeroAutorizacionNuevo.Equals("NULL")) varParametros[7].Value = null;
                else varParametros[7].Value = NumeroAutorizacionNuevo;
                //varParametros[7].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspSITEDS_GrabarTiempoEspera", varParametros, TipoProcesamiento.DataReader, false);//, CadenaClinica(idClinica));

                if (varDataReader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                //DateTime now = DateTime.Now;
                //string Archivo = $"{"ErrorGrabarTiempoEsperaVirtual"}_{now.Year}_{now.Month.ToString().PadLeft(2, '0')}_{now.Day.ToString().PadLeft(2, '0')}{".txt"}";
                //string rutaArchivo = string.Format("{0}{1}", ConfigurationManager.AppSettings["LogCitaBDSpring"], Archivo);
                //string nombreArchivo = "";
                //using (FileStream stream = new FileStream(rutaArchivo + nombreArchivo, FileMode.Append, FileAccess.Write, FileShare.Write))
                //{
                //    using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.Default))
                //    {
                //        streamWriter.WriteLine("Fecha = " + DateTime.Now);
                //        streamWriter.WriteLine("Mensaje = " + ex.Message);
                //        streamWriter.WriteLine("Mensaje = " + ex.StackTrace);
                //        streamWriter.WriteLine(new string('_', 50));
                //    }
                //}
                new ErrorDA().RegistrarError(ex, "WSV", "GrabarTiempoEsperaVirtual", idCita + "¯" + data);
                return false;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }

        public bool GrabarTransaccionVirtualDetalle(string data, string idCita)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                string[] parametros = data.Split('¦');
                string NumeroAutorizacion = parametros[1];
                int Cantidad = int.Parse(parametros[2]);
                string CodigoComponente = parametros[3];
                string Moneda = parametros[4];
                decimal MontoValor = decimal.Parse(parametros[5]);
                decimal MontoPago = decimal.Parse(parametros[6]);
                int Estado = int.Parse(parametros[7]);
                string UsuarioCreacion = parametros[8];
                int IdCitaSpring = int.Parse(parametros[9]);
                int idClinica = int.Parse(parametros[10]);
                int IDCitaSpringComponente = String.IsNullOrEmpty(parametros[11]) ? 0 : int.Parse(parametros[11]); 

                SqlParameter[] varParametros = new SqlParameter[10];
                varParametros[0] = new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar);
                varParametros[0].Value = NumeroAutorizacion;
                varParametros[1] = new SqlParameter("@Cantidad", SqlDbType.Int);
                varParametros[1].Value = Cantidad;
                varParametros[2] = new SqlParameter("@CodigoComponente", SqlDbType.VarChar);
                varParametros[2].Value = CodigoComponente;
                varParametros[3] = new SqlParameter("@Moneda", SqlDbType.VarChar);
                varParametros[3].Value = Moneda;
                varParametros[4] = new SqlParameter("@MontoValor", SqlDbType.Decimal);
                varParametros[4].Value = MontoValor;
                varParametros[5] = new SqlParameter("@MontoPago", SqlDbType.Decimal);
                varParametros[5].Value = MontoPago;
                varParametros[6] = new SqlParameter("@Estado", SqlDbType.Int);
                varParametros[6].Value = Estado;
                varParametros[7] = new SqlParameter("@UsuarioCreacion", SqlDbType.VarChar);
                varParametros[7].Value = UsuarioCreacion;
                varParametros[8] = new SqlParameter("@IdCitaSpring", SqlDbType.Int);
                varParametros[8].Value = IdCitaSpring;
                varParametros[9] = new SqlParameter("@IDCitaSpringComponente", SqlDbType.Int);
                if (IDCitaSpringComponente != 0) {
                    varParametros[9].Value = IDCitaSpringComponente;
                } else {
                    varParametros[9].Value = null;
                }

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("uspGrabarCitasTransaccionalesDetalle", varParametros, TipoProcesamiento.DataReader, false);

                if (varDataReader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                new ErrorDA().RegistrarError(ex, "WSV", "GrabarTransaccionVirtualDetalle", idCita + "¯" + data);
                return false;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }

        public bool VerificarTransaccionSpring(int idTransaccion, int idClinica)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@IdTransaccion", SqlDbType.Int);
                varParametros[0].Value = idTransaccion;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("eureka_VerificacionTransaccionales", varParametros, TipoProcesamiento.DataReader, false, CadenaClinica(idClinica));

                if (varDataReader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                new ErrorDA().RegistrarError(ex, "WSV", "VerificarTransaccionSpring", idTransaccion.ToString());
                return false;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }
    }
}
