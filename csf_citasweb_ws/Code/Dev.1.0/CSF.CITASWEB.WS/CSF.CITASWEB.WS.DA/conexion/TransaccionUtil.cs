using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace CSF.CITASWEB.WS.DA
{
    public class TransaccionUtil
    {
        private SqlTransaction varTransaccion;
        private SqlConnection varConexion;

        public TransaccionUtil()
        {
            varConexion = new SqlConnection();
        }

        public SqlConnection Conexion
        {
            get { return varConexion; }
            set { varConexion = value; }
        }

        public SqlTransaction Transaccion
        {
            get { return varTransaccion; }
            set { varTransaccion = value; }
        }

        public void IniciarTransaccion(string nombreCadena = "CadenaConexion")
        {
            varConexion = GeneraCadena(nombreCadena);
            varConexion.Open();
            varTransaccion = varConexion.BeginTransaction();
        }

        public void Commit()
        {
            varTransaccion.Commit();
            CerrarConexion();
        }

        public void Rollback()
        {
            varTransaccion.Rollback();
            CerrarConexion();
        }

        private void CerrarConexion()
        {
            varConexion.Close();
            varConexion.Dispose();
        }

        private SqlConnection GeneraCadena(string nombreCadena)
        {
            if (ConfigurationManager.ConnectionStrings[nombreCadena] == null)
                throw new Exception("No se encontró la cadena de conexión " + nombreCadena + ". Revise el config");

            return new SqlConnection(ConfigurationManager.ConnectionStrings[nombreCadena].ConnectionString);
        }
    }
}