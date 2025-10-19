using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace CSF.CITASWEB.WS.BatchFlagHistorico.conexion
{
    public class BatchDA
    {
        public void ActualizaFlagHistorico()
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                varConexion.EjecutarProcedimiento("Batch_Proc_FlagHistorico", null, TipoProcesamiento.NonQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
