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
    public class ProcedimientoDA
    {
        private SqlDataReader varDataReader;

        public Dictionary<string, string> RegistrarProcedimiento(string tipoDocumento, string numeroDocumento, string idClinica,
                                        string codMedico, string idSubespecialidad, string idServicio,
                                        string codAtencion, string codTipoPaciente, string fecha,
                                        string horaInicio, string horaFin, string duracion,
                                        string codCpt, string cpt, string codSegus,
                                        string segus, string guarismo, string rucSeguro,
                                        string iafas, bool flgCartaGarantia, string cartaGarantia,
                                        bool flgPresupuesto, string presupuesto, string celular,
                                        bool flgAlergia, string coaseguro, string correo,
                                        string observacion, string origen, int idEstado,
                                        string idOrdenDetalle, string totalSesiones)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros;

                varParametros = new SqlParameter[32];

                varParametros[0] = new SqlParameter("@tnTipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@tvNumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@tnIdClinica", SqlDbType.Int);
                varParametros[2].Value = idClinica;
                varParametros[3] = new SqlParameter("@tvCMP", SqlDbType.VarChar);
                varParametros[3].Value = codMedico;
                varParametros[4] = new SqlParameter("@tnIdSubespecialidad", SqlDbType.Int);
                varParametros[4].Value = idSubespecialidad;
                varParametros[5] = new SqlParameter("@tnIdServicio", SqlDbType.Int);
                varParametros[5].Value = idServicio;
                varParametros[6] = new SqlParameter("@tvCodigoAtencion", SqlDbType.VarChar);
                varParametros[6].Value = codAtencion;
                varParametros[7] = new SqlParameter("@tvCodigoTipoPaciente", SqlDbType.VarChar);
                varParametros[7].Value = codTipoPaciente;
                varParametros[8] = new SqlParameter("@tdFecha", SqlDbType.Date);
                if (!string.IsNullOrEmpty(fecha)) varParametros[8].Value = fecha;
                else varParametros[8].Value = null;
                varParametros[9] = new SqlParameter("@ttHoraInicio", SqlDbType.Time);
                if (!string.IsNullOrEmpty(horaInicio)) varParametros[9].Value = horaInicio;
                else varParametros[9].Value = null;
                varParametros[10] = new SqlParameter("@ttHoraFin", SqlDbType.Time);
                if (!string.IsNullOrEmpty(horaFin)) varParametros[10].Value = horaFin;
                else varParametros[10].Value = null;
                varParametros[11] = new SqlParameter("@tnDuracion", SqlDbType.Int);
                if (!string.IsNullOrEmpty(duracion)) varParametros[11].Value = duracion;
                else varParametros[11].Value = null;
                varParametros[12] = new SqlParameter("@tvCodigoCpt", SqlDbType.VarChar);
                varParametros[12].Value = codCpt;
                varParametros[13] = new SqlParameter("@tvCpt", SqlDbType.VarChar);
                varParametros[13].Value = cpt;
                varParametros[14] = new SqlParameter("@tvCodigoSegus", SqlDbType.VarChar);
                varParametros[14].Value = codSegus;
                varParametros[15] = new SqlParameter("@tvSegus", SqlDbType.VarChar);
                varParametros[15].Value = segus;
                varParametros[16] = new SqlParameter("@tnGuarismo", SqlDbType.Int);
                if (!string.IsNullOrEmpty(guarismo)) varParametros[16].Value = guarismo;
                else varParametros[16].Value = null;
                varParametros[17] = new SqlParameter("@tvRucSeguro", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(rucSeguro)) varParametros[17].Value = rucSeguro;
                else varParametros[17].Value = null;
                varParametros[18] = new SqlParameter("@tvIafas", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(iafas)) varParametros[18].Value = iafas;
                else varParametros[18].Value = null;
                varParametros[19] = new SqlParameter("@tlCartaGarantia", SqlDbType.Bit);
                varParametros[19].Value = flgCartaGarantia;
                varParametros[20] = new SqlParameter("@tvCartaGarantia", SqlDbType.VarChar);
                if (flgCartaGarantia) varParametros[20].Value = cartaGarantia;
                else varParametros[20].Value = null;
                varParametros[21] = new SqlParameter("@tlPresupuesto", SqlDbType.Bit);
                varParametros[21].Value = flgPresupuesto;
                varParametros[22] = new SqlParameter("@tbPresupuesto", SqlDbType.Decimal);
                if (flgPresupuesto) varParametros[22].Value = presupuesto;
                else varParametros[22].Value = null;
                varParametros[23] = new SqlParameter("@tvCelular", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(celular)) varParametros[23].Value = celular;
                else varParametros[23].Value = null;
                varParametros[24] = new SqlParameter("@tlAlergia", SqlDbType.Bit);
                varParametros[24].Value = flgAlergia;
                varParametros[25] = new SqlParameter("@tbCoaseguro", SqlDbType.Decimal);
                if (!string.IsNullOrEmpty(coaseguro)) varParametros[25].Value = coaseguro;
                else varParametros[25].Value = null;
                varParametros[26] = new SqlParameter("@tvCorreo", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(correo)) varParametros[26].Value = correo;
                else varParametros[26].Value = null;
                varParametros[27] = new SqlParameter("@tvObservacion", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(observacion)) varParametros[27].Value = observacion;
                else varParametros[27].Value = null;
                varParametros[28] = new SqlParameter("@tvOrigen", SqlDbType.VarChar);
                varParametros[28].Value = origen;
                varParametros[29] = new SqlParameter("@tvEstado", SqlDbType.Int);
                varParametros[29].Value = idEstado;
                varParametros[30] = new SqlParameter("@tnIdOrdenDetalle", SqlDbType.Int);
                varParametros[30].Value = idOrdenDetalle;
                varParametros[31] = new SqlParameter("@tnTotalSesiones", SqlDbType.Int);
                if (!string.IsNullOrEmpty(totalSesiones)) varParametros[31].Value = totalSesiones;
                else varParametros[31].Value = null;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("Sp_Procedimiento_Insert", varParametros, TipoProcesamiento.DataReader, false);

                varDataReader.Read();

                Dictionary<string, string> varResultado = new Dictionary<string, string>();
                varResultado.Add("IdProcedimiento", varDataReader.GetInt32(varDataReader.GetOrdinal("IdProcedimiento")).ToString());

                return varResultado;
            }
            catch (Exception ex)
            {
                new ErrorDA().RegistrarErrorV2(ex, "WS", "01: ProcedimientoDA/RegistrarProcedimiento");
                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }

        public void ActualizarProcedimiento(string tipoDocumento, string numeroDocumento, string idOrdenDetalle,
                                        string codCpt, string cpt, string codSegus,
                                        string segus, string guarismo, string origen)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[9];
                varParametros[0] = new SqlParameter("@tnTipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@tvNumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@tnIdOrdenDetalle", SqlDbType.Int);
                varParametros[2].Value = idOrdenDetalle;
                varParametros[3] = new SqlParameter("@tvCodigoCpt", SqlDbType.VarChar);
                varParametros[3].Value = codCpt;
                varParametros[4] = new SqlParameter("@tvCpt", SqlDbType.VarChar);
                varParametros[4].Value = cpt;
                varParametros[5] = new SqlParameter("@tvCodigoSegus", SqlDbType.VarChar);
                varParametros[5].Value = codSegus;
                varParametros[6] = new SqlParameter("@tvSegus", SqlDbType.VarChar);
                varParametros[6].Value = segus;
                varParametros[7] = new SqlParameter("@tnGuarismo", SqlDbType.Int);
                if (!string.IsNullOrEmpty(guarismo)) varParametros[7].Value = guarismo;
                else varParametros[7].Value = null;
                varParametros[8] = new SqlParameter("@tvOrigen", SqlDbType.VarChar);
                varParametros[8].Value = origen;

                varConexion.EjecutarProcedimiento("Sp_Procedimiento_Update", varParametros, TipoProcesamiento.NonQuery);

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

        public void AnularProcedimiento(string tipoDocumento, string numeroDocumento, string idOrdenDetalle,
                                        string origen, int idEstado)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[5];
                varParametros[0] = new SqlParameter("@tnTipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@tvNumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varParametros[2] = new SqlParameter("@tnIdOrdenDetalle", SqlDbType.Int);
                varParametros[2].Value = idOrdenDetalle;
                varParametros[3] = new SqlParameter("@tvOrigen", SqlDbType.VarChar);
                varParametros[3].Value = origen;
                varParametros[4] = new SqlParameter("@tvEstado", SqlDbType.Int);
                varParametros[4].Value = idEstado;

                varConexion.EjecutarProcedimiento("Sp_Procedimiento_UpdateEstado", varParametros, TipoProcesamiento.NonQuery);

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
