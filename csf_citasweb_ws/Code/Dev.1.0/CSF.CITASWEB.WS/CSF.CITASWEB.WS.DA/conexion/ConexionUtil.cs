using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;

namespace CSF.CITASWEB.WS.DA
{
    public enum TipoProcesamiento
    {
        NonQuery = 1,
        Scalar = 2,
        DataReader = 3,
        ScalarOutParameters = 4
    }

    public class ConexionUtil
    {
        private SqlConnection varConexion;
        private SqlCommand varComando;
        private SqlDataReader varDataReader;

        public void GeneraCadena(string nombreCadena)
        {
            if (ConfigurationManager.ConnectionStrings[nombreCadena] == null)
                throw new Exception("No se encontró la cadena de conexión " + nombreCadena + ". Revise el config");

            varConexion = new SqlConnection(ConfigurationManager.ConnectionStrings[nombreCadena].ConnectionString);
        }

        public void Conectar(string nombreCadena)
        {
            GeneraCadena(nombreCadena);
            varConexion.Open();
        }

        public void Desconectar()
        {
            if (varConexion != null)
            {
                if (varConexion.State == ConnectionState.Open) 
                    varConexion.Close();
                varConexion.Dispose();
                varConexion = null;
            }
            if (varDataReader != null) {
                varDataReader.Close();
                varDataReader = null;
            }
            if (varComando != null)
            {
                varComando.Dispose();
                varComando = null;
            }
        }

        public object EjecutarProcedimiento(string nombreProcedure, SqlParameter[] parametros, TipoProcesamiento tipoProceso, bool terminar = true, string nombreCadena = "CadenaConexion")
        {
            try
            {
                Conectar(nombreCadena);
                varComando = new SqlCommand(nombreProcedure, varConexion);
                varComando.CommandTimeout = 120; //2 min.
                varComando.CommandType = CommandType.StoredProcedure;
                if (parametros != null && parametros.Length > 0)
                {
                    varComando.Parameters.AddRange(parametros);
                }
                if (tipoProceso == TipoProcesamiento.NonQuery)
                {
                    varComando.ExecuteNonQuery();
                    return true;
                }
                else if (tipoProceso == TipoProcesamiento.Scalar)
                {
                    return varComando.ExecuteScalar();
                }
                else if (tipoProceso == TipoProcesamiento.DataReader)
                {
                    varDataReader = varComando.ExecuteReader();
                    return varDataReader;
                }
                else if (tipoProceso == TipoProcesamiento.ScalarOutParameters)
                {
                    List<object> varResultado = new List<object>();
                    varResultado.Add(varComando.ExecuteScalar());
                    varResultado.Add(varComando.Parameters);
                    return varResultado;
                }
                else
                {
                    throw new Exception("Tipo de procesamiento no válido");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (tipoProceso != TipoProcesamiento.DataReader || terminar == true)
                    Desconectar();
            }
        }

        public object EjecutarProcedimiento(string nombreProcedure, SqlParameter[] parametros, TipoProcesamiento tipoProceso, TransaccionUtil transaccion)
        {
            try
            {
                varConexion = transaccion.Conexion;
                varComando = new SqlCommand(nombreProcedure, varConexion);
                varComando.CommandType = CommandType.StoredProcedure;
                varComando.Transaction = transaccion.Transaccion;
                if (parametros.Length > 0)
                {
                    varComando.Parameters.AddRange(parametros);
                }
                if (tipoProceso == TipoProcesamiento.NonQuery)
                {
                    varComando.ExecuteNonQuery();
                    return true;
                }
                else if (tipoProceso == TipoProcesamiento.Scalar)
                {
                    return varComando.ExecuteScalar();
                }
                else if (tipoProceso == TipoProcesamiento.DataReader)
                {
                    varDataReader = varComando.ExecuteReader();
                    return varDataReader;
                }
                else
                {
                    throw new Exception("Tipo de procesamiento no válido");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}