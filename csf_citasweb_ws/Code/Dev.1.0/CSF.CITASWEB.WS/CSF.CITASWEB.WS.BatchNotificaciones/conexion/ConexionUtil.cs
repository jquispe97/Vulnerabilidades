using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;

namespace CSF.CITASWEB.WS.BatchNotificaciones.conexion
{
    public enum TipoProcesamiento
    {
        NonQuery = 1,
        Scalar = 2,
        DataReader = 3
    }

    public class ConexionUtil
    {
        private SqlConnection varConexion;
        private SqlCommand varComando;
        private SqlDataReader varDataReader;

        public void GeneraCadena()
        {
            varConexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString);
        }

        public void Conectar()
        {
            GeneraCadena();
            varConexion.Open();
        }

        public void Desconectar()
        {
            if (varConexion != null)
            {
                if (varConexion.State == ConnectionState.Open)
                    varConexion.Close();
                varConexion.Dispose();
            }
        }

        public object EjecutarProcedimiento(string nombreProcedure, SqlParameter[] parametros, TipoProcesamiento tipoProceso, bool terminar = true)
        {
            try
            {
                Conectar();
                varComando = new SqlCommand(nombreProcedure, varConexion);
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

        public object EjecutarProcedimientoBatch(string nombreProcedure, SqlParameter[] parametros, TipoProcesamiento tipoProceso)
        {
            try
            {
                varComando = new SqlCommand(nombreProcedure, varConexion);
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