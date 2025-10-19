using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSF.CITASWEB.WS.BE;
using System.Data.SqlClient;
using System.Data;

namespace CSF.CITASWEB.WS.DA
{
    public class SeguroDA
    {
        public List<MantSeguroBE> Listar(string rucSeguro, string razonSocial)
        {
            SqlDataReader varDataReader;
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[2];
                varParametros[0] = new SqlParameter("@RUCSeguro", SqlDbType.VarChar);
                varParametros[0].Value = rucSeguro;
                varParametros[1] = new SqlParameter("@RazonSocial", SqlDbType.VarChar);
                varParametros[1].Value = razonSocial;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("Web_Proc_Seguro_Listar", varParametros, TipoProcesamiento.DataReader, false);

                List<MantSeguroBE> varResultado = new List<MantSeguroBE>();
                while (varDataReader.Read())
                {
                    varResultado.Add(new MantSeguroBE()
                    {
                        RUCSeguro = varDataReader.GetString(varDataReader.GetOrdinal("RUCSeguro")),
                        RazonSocial = varDataReader.GetString(varDataReader.GetOrdinal("RazonSocial")),
                        Orden = varDataReader.GetInt32(varDataReader.GetOrdinal("Orden")),
                        IDEquivalente = varDataReader.GetInt32(varDataReader.GetOrdinal("IDEquivalente"))
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

        public void Insertar(string rucSeguro, string razonSocial, int orden, int idEquivalente)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[4];
                varParametros[0] = new SqlParameter("@RUCSeguro", SqlDbType.VarChar);
                varParametros[0].Value = rucSeguro;
                varParametros[1] = new SqlParameter("@RazonSocial", SqlDbType.VarChar);
                varParametros[1].Value = razonSocial;
                varParametros[2] = new SqlParameter("@Orden", SqlDbType.Int);
                varParametros[2].Value = orden;
                varParametros[3] = new SqlParameter("@IDEquivalente", SqlDbType.Int);
                varParametros[3].Value = idEquivalente;

                varConexion.EjecutarProcedimiento("Web_Proc_Seguro_Insertar", varParametros, TipoProcesamiento.NonQuery);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public void Modificar(string rucSeguro, string razonSocial, int orden, int idEquivalente)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[4];
                varParametros[0] = new SqlParameter("@RUCSeguro", SqlDbType.VarChar);
                varParametros[0].Value = rucSeguro;
                varParametros[1] = new SqlParameter("@RazonSocial", SqlDbType.VarChar);
                varParametros[1].Value = razonSocial;
                varParametros[2] = new SqlParameter("@Orden", SqlDbType.Int);
                varParametros[2].Value = orden;
                varParametros[3] = new SqlParameter("@IDEquivalente", SqlDbType.Int);
                varParametros[3].Value = idEquivalente;

                varConexion.EjecutarProcedimiento("Web_Proc_Seguro_Modificar", varParametros, TipoProcesamiento.NonQuery);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public void Eliminar(string rucSeguro)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@RUCSeguro", SqlDbType.VarChar);
                varParametros[0].Value = rucSeguro;

                varConexion.EjecutarProcedimiento("Web_Proc_Seguro_Eliminar", varParametros, TipoProcesamiento.NonQuery);
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
    }
}
