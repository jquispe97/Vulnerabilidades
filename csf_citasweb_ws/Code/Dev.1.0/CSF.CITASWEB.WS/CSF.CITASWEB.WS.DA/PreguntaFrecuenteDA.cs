using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSF.CITASWEB.WS.BE;
using System.Data.SqlClient;
using System.Data;

namespace CSF.CITASWEB.WS.DA
{
    public class PreguntaFrecuenteDA
    {
        private SqlDataReader varDataReader;

        public List<PreguntaFrecuenteBE> Listar()
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("Web_Proc_PreguntaFrecuente_Listar", null, TipoProcesamiento.DataReader, false);

                List<PreguntaFrecuenteBE> varResultado = new List<PreguntaFrecuenteBE>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new PreguntaFrecuenteBE()
                    {
                        IDPreguntaFrecuente = varDataReader.GetInt32(varDataReader.GetOrdinal("IDPreguntaFrecuente")).ToString(),
                        Pregunta = varDataReader.GetString(varDataReader.GetOrdinal("Pregunta")),
                        Respuesta = varDataReader.GetString(varDataReader.GetOrdinal("Respuesta"))
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

        public void Insertar(PreguntaFrecuenteBE entidad)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@Pregunta", SqlDbType.VarChar);
                varParametros[0].Value = entidad.Pregunta;
                varParametros[1] = new SqlParameter("@Respuesta", SqlDbType.VarChar);
                varParametros[1].Value = entidad.Respuesta;

                varConexion.EjecutarProcedimiento("Web_Proc_PreguntaFrecuente_Insertar", varParametros, TipoProcesamiento.NonQuery);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public void Modificar(PreguntaFrecuenteBE entidad)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[3];
                varParametros[0] = new SqlParameter("@IDPreguntaFrecuente", SqlDbType.Int);
                varParametros[0].Value = entidad.IDPreguntaFrecuente;
                varParametros[1] = new SqlParameter("@Pregunta", SqlDbType.VarChar);
                varParametros[1].Value = entidad.Pregunta;
                varParametros[2] = new SqlParameter("@Respuesta", SqlDbType.VarChar);
                varParametros[2].Value = entidad.Respuesta;

                varConexion.EjecutarProcedimiento("Web_Proc_PreguntaFrecuente_Modificar", varParametros, TipoProcesamiento.NonQuery);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public void Eliminar(string IDClinica)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@IDPreguntaFrecuente", SqlDbType.Int);
                varParametros[0].Value = IDClinica;

                varConexion.EjecutarProcedimiento("Web_Proc_PreguntaFrecuente_Eliminar", varParametros, TipoProcesamiento.NonQuery);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }
        public EstadoTriaje ListarPreguntasTriaje(string tipoDocumento, string numeroDocumento)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[0].Value = tipoDocumento;
                varParametros[1] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[1].Value = numeroDocumento;
                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_PreguntaTriaje_Listar", varParametros, TipoProcesamiento.DataReader, false);

                EstadoTriaje varResultado = new EstadoTriaje();
                varResultado.preguntas = new List<PreguntaTriajeBE>();
                
                while (varDataReader.Read())
                {
                    varResultado.preguntas.Add(new PreguntaTriajeBE()
                    {
                        idPreguntaTriaje = varDataReader.GetInt32(varDataReader.GetOrdinal("IDPreguntaTriaje")).ToString(),
                        pregunta = varDataReader.GetString(varDataReader.GetOrdinal("Pregunta")),
                        tipoRespuesta = varDataReader.GetString(varDataReader.GetOrdinal("TipoRespuesta")),
                        mandatorio= varDataReader.GetBoolean(varDataReader.GetOrdinal("Mandatoria"))
                    });
                }
                varDataReader.NextResult();
                varDataReader.Read();
                if (varDataReader.HasRows)
                {
                    varResultado.usuarioRestringido = false;
                }
                else
                {
                    varResultado.usuarioRestringido = true;
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
        public bool RegistrarRespuestaTriaje(string tipoDocumento, string numeroDocumento, string idPregunta, string respuesta, string idEspecialidad, string cmp)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[6];
                varParametros[0] = new SqlParameter("@IDPregunta", SqlDbType.Int);
                varParametros[0].Value = idPregunta;
                varParametros[1] = new SqlParameter("@Respuesta", SqlDbType.Bit);
                varParametros[1].Value = Convert.ToBoolean(respuesta);
                varParametros[2] = new SqlParameter("@TipoDocumento", SqlDbType.Int);
                varParametros[2].Value = tipoDocumento;
                varParametros[3] = new SqlParameter("@NumeroDocumento", SqlDbType.VarChar);
                varParametros[3].Value = numeroDocumento;
                varParametros[4] = new SqlParameter("@IDEspecialidad", SqlDbType.VarChar);
                varParametros[4].Value = idEspecialidad;
                varParametros[5] = new SqlParameter("@CMP", SqlDbType.VarChar);
                varParametros[5].Value = cmp;
                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_PreguntaTriaje_Respuestas", varParametros, TipoProcesamiento.DataReader, false);

                
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
    }
}
