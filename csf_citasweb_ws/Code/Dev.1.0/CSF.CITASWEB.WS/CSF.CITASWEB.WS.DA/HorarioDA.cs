using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CSF.CITASWEB.WS.DA
{
    public class HorarioDA
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

        public string RegistrarHorario(string CMP, DateTime FechaDesde, DateTime FechaHasta, DateTime HoraDesde,
          DateTime HoraHasta, string Dias, int ConsultorioId, string Consultorio, int TiempoAtencion,
          int EspecialidadId, string EspecialidadNombre, string TipoEntidad, int TipoHorario, int CantidadAdicional,
          int TipoHorarioVirtual, Boolean IndicadorCompartido, int IDServicio, Boolean EsPrePago, string Origen,
          int IdClinica)
        {
            string rpta = "0";
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[19];
                varParametros[0] = new SqlParameter("@CMP", SqlDbType.VarChar,10);
                varParametros[0].Value = CMP;
                varParametros[1] = new SqlParameter("@FechaDesde", SqlDbType.Date);
                varParametros[1].Value = FechaDesde;
                varParametros[2] = new SqlParameter("@FechaHasta", SqlDbType.Date);
                varParametros[2].Value = FechaHasta;
                varParametros[3] = new SqlParameter("@HoraDesde", SqlDbType.Time);
                varParametros[3].Value = HoraDesde.TimeOfDay;

                varParametros[4] = new SqlParameter("@HoraHasta", SqlDbType.Time);
                varParametros[4].Value = HoraHasta.TimeOfDay;
                varParametros[5] = new SqlParameter("@Dias", SqlDbType.VarChar,7);
                varParametros[5].Value = Dias;
                varParametros[6] = new SqlParameter("@ConsultorioId", SqlDbType.Int);
                varParametros[6].Value = ConsultorioId;
                varParametros[7] = new SqlParameter("@Consultorio", SqlDbType.VarChar, 80);
                varParametros[7].Value = Consultorio;
                varParametros[8] = new SqlParameter("@TiempoAtencion", SqlDbType.Int);
                varParametros[8].Value = TiempoAtencion;

                varParametros[9] = new SqlParameter("@EspecialidadId", SqlDbType.Int);
                varParametros[9].Value = EspecialidadId;
                varParametros[10] = new SqlParameter("@EspecialidadNombre", SqlDbType.VarChar,100);
                varParametros[10].Value = EspecialidadNombre;
                varParametros[11] = new SqlParameter("@TipoEntidad", SqlDbType.VarChar, 3);
                varParametros[11].Value = TipoEntidad;

                varParametros[12] = new SqlParameter("@TipoHorario", SqlDbType.Int);
                varParametros[12].Value = TipoHorario;
                varParametros[13] = new SqlParameter("@CantidadAdicional", SqlDbType.Int);
                varParametros[13].Value = CantidadAdicional;
                varParametros[14] = new SqlParameter("@TipoHorarioVirtual", SqlDbType.Int);
                varParametros[14].Value = TipoHorarioVirtual;
                varParametros[15] = new SqlParameter("@IndicadorCompartido", SqlDbType.Bit);
                varParametros[15].Value = IndicadorCompartido;
                varParametros[16] = new SqlParameter("@IDServicio", SqlDbType.Int);
                varParametros[16].Value = IDServicio;
                varParametros[17] = new SqlParameter("@EsPrePago", SqlDbType.Bit);
                varParametros[17].Value = EsPrePago;
                varParametros[18] = new SqlParameter("@Origen", SqlDbType.VarChar,10);
                varParametros[18].Value = Origen;

                //Object o = varConexion.EjecutarProcedimiento("PROC_Horario_Registrar", varParametros, TipoProcesamiento.Scalar, false, "CadenaConexionSpringSalud");
                Object o = varConexion.EjecutarProcedimiento("PROC_Horario_Registrar", varParametros, TipoProcesamiento.Scalar, false, CadenaClinica(IdClinica));
                if (o != null) {
                    rpta = o.ToString();
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

            return rpta;
        }

        public string ActualizarHorario(int IDHorarioSpring, string CMP, DateTime FechaDesde, DateTime FechaHasta,
        DateTime HoraDesde, DateTime HoraHasta, string Dias, int ConsultorioId, string Consultorio,
        int TiempoAtencion, string EstadoRegistro, int EspecialidadId, string EspecialidadNombre,
        string TipoEntidad, int TipoHorario, int CantidadAdicional, int TipoHorarioVirtual,
        Boolean IndicadorCompartido, int IDServicio, Boolean EsPrePago, string Origen,
        int IdClinica)
        {
            string rpta = "0";
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[21];
                varParametros[0] = new SqlParameter("@IDHorarioSpring", SqlDbType.Int);
                varParametros[0].Value = IDHorarioSpring;
                varParametros[1] = new SqlParameter("@CMP", SqlDbType.VarChar, 10);
                varParametros[1].Value = CMP;

                varParametros[2] = new SqlParameter("@FechaDesde", SqlDbType.Date);
                varParametros[2].Value = FechaDesde;

                varParametros[3] = new SqlParameter("@FechaHasta", SqlDbType.Date);
                varParametros[3].Value = FechaHasta;

                varParametros[4] = new SqlParameter("@HoraDesde", SqlDbType.Time);
                varParametros[4].Value = HoraDesde.TimeOfDay;

                varParametros[5] = new SqlParameter("@HoraHasta", SqlDbType.Time);
                varParametros[5].Value = HoraHasta.TimeOfDay;

                varParametros[6] = new SqlParameter("@Dias", SqlDbType.VarChar, 7);
                varParametros[6].Value = Dias;

                varParametros[7] = new SqlParameter("@ConsultorioId", SqlDbType.Int);
                varParametros[7].Value = ConsultorioId;

                varParametros[8] = new SqlParameter("@Consultorio", SqlDbType.VarChar, 80);
                varParametros[8].Value = Consultorio;

                varParametros[9] = new SqlParameter("@TiempoAtencion", SqlDbType.Int);
                varParametros[9].Value = TiempoAtencion;

                varParametros[10] = new SqlParameter("@EstadoRegistro", SqlDbType.VarChar,3);
                varParametros[10].Value = EstadoRegistro;

                varParametros[11] = new SqlParameter("@EspecialidadId", SqlDbType.Int);
                varParametros[11].Value = EspecialidadId;

                varParametros[12] = new SqlParameter("@EspecialidadNombre", SqlDbType.VarChar, 100);
                varParametros[12].Value = EspecialidadNombre;

                varParametros[13] = new SqlParameter("@TipoEntidad", SqlDbType.VarChar, 3);
                varParametros[13].Value = TipoEntidad;

                varParametros[14] = new SqlParameter("@TipoHorario", SqlDbType.Int);
                varParametros[14].Value = TipoHorario;

                varParametros[15] = new SqlParameter("@CantidadAdicional", SqlDbType.Int);
                varParametros[15].Value = CantidadAdicional;

                varParametros[16] = new SqlParameter("@TipoHorarioVirtual", SqlDbType.Int);
                varParametros[16].Value = TipoHorarioVirtual;

                varParametros[17] = new SqlParameter("@IndicadorCompartido", SqlDbType.Bit);
                varParametros[17].Value = IndicadorCompartido;

                varParametros[18] = new SqlParameter("@IDServicio", SqlDbType.Int);
                varParametros[18].Value = IDServicio;

                varParametros[19] = new SqlParameter("@EsPrePago", SqlDbType.Bit);
                varParametros[19].Value = EsPrePago;

                varParametros[20] = new SqlParameter("@Origen", SqlDbType.VarChar, 10);
                varParametros[20].Value = Origen;

                Object o = varConexion.EjecutarProcedimiento("PROC_Horario_Actualizar", varParametros, TipoProcesamiento.Scalar, false, CadenaClinica(IdClinica));
                if (o != null)
                {
                    rpta = o.ToString();
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

            return rpta;
        }

        public string AnularHorario(int IDHorarioSpring, string CMP, DateTime FechaDesde, DateTime FechaHasta,
        DateTime HoraDesde, DateTime HoraHasta, string Dias, int ConsultorioId, string Consultorio,
        int TiempoAtencion, string EstadoRegistro, int EspecialidadId, string EspecialidadNombre,
        string TipoEntidad, int TipoHorario, int CantidadAdicional, int TipoHorarioVirtual,
        Boolean IndicadorCompartido, int IDServicio, Boolean EsPrePago, string Origen, 
        int IdClinica)
        {
            string rpta = "0";
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[21];
                varParametros[0] = new SqlParameter("@IDHorarioSpring", SqlDbType.Int);
                varParametros[0].Value = IDHorarioSpring;
                varParametros[1] = new SqlParameter("@CMP", SqlDbType.VarChar, 10);
                varParametros[1].Value = CMP;

                varParametros[2] = new SqlParameter("@FechaDesde", SqlDbType.Date);
                varParametros[2].Value = FechaDesde;

                varParametros[3] = new SqlParameter("@FechaHasta", SqlDbType.Date);
                varParametros[3].Value = FechaHasta;

                varParametros[4] = new SqlParameter("@HoraDesde", SqlDbType.Time);
                varParametros[4].Value = HoraDesde.TimeOfDay;

                varParametros[5] = new SqlParameter("@HoraHasta", SqlDbType.Time);
                varParametros[5].Value = HoraHasta.TimeOfDay;

                varParametros[6] = new SqlParameter("@Dias", SqlDbType.VarChar, 7);
                varParametros[6].Value = Dias;

                varParametros[7] = new SqlParameter("@ConsultorioId", SqlDbType.Int);
                varParametros[7].Value = ConsultorioId;

                varParametros[8] = new SqlParameter("@Consultorio", SqlDbType.VarChar, 80);
                varParametros[8].Value = Consultorio;

                varParametros[9] = new SqlParameter("@TiempoAtencion", SqlDbType.Int);
                varParametros[9].Value = TiempoAtencion;

                varParametros[10] = new SqlParameter("@EstadoRegistro", SqlDbType.VarChar, 3);
                varParametros[10].Value = EstadoRegistro;

                varParametros[11] = new SqlParameter("@EspecialidadId", SqlDbType.Int);
                varParametros[11].Value = EspecialidadId;

                varParametros[12] = new SqlParameter("@EspecialidadNombre", SqlDbType.VarChar, 100);
                varParametros[12].Value = EspecialidadNombre;

                varParametros[13] = new SqlParameter("@TipoEntidad", SqlDbType.VarChar, 3);
                varParametros[13].Value = TipoEntidad;

                varParametros[14] = new SqlParameter("@TipoHorario", SqlDbType.Int);
                varParametros[14].Value = TipoHorario;

                varParametros[15] = new SqlParameter("@CantidadAdicional", SqlDbType.Int);
                varParametros[15].Value = CantidadAdicional;

                varParametros[16] = new SqlParameter("@TipoHorarioVirtual", SqlDbType.Int);
                varParametros[16].Value = TipoHorarioVirtual;

                varParametros[17] = new SqlParameter("@IndicadorCompartido", SqlDbType.Bit);
                varParametros[17].Value = IndicadorCompartido;

                varParametros[18] = new SqlParameter("@IDServicio", SqlDbType.Int);
                varParametros[18].Value = IDServicio;

                varParametros[19] = new SqlParameter("@EsPrePago", SqlDbType.Bit);
                varParametros[19].Value = EsPrePago;

                varParametros[20] = new SqlParameter("@Origen", SqlDbType.VarChar, 10);
                varParametros[20].Value = Origen;

                Object o = varConexion.EjecutarProcedimiento("PROC_Horario_Anular", varParametros, TipoProcesamiento.Scalar, false, CadenaClinica(IdClinica));
                if (o != null)
                {
                    rpta = o.ToString();
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

            return rpta;
        }


    }
}
