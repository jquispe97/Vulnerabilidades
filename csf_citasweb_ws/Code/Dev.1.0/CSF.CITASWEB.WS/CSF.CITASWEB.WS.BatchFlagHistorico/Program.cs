using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSF.CITASWEB.WS.BatchFlagHistorico.conexion;
using System.Diagnostics;

namespace CSF.CITASWEB.WS.BatchFlagHistorico
{
    class Program
    {
        static void Main(string[] args)
        {
            ErrorDA varError = new ErrorDA();
            try
            {
                if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
                {
                    varError.RegistrarError(new Exception("La aplicación ya está ejecutandose"), "Batch", "BatchFlagHistorico");
                    return;
                }

                new BatchDA().ActualizaFlagHistorico();
            }
            catch (Exception ex)
            {
                varError.RegistrarError(ex, "Batch", "BatchFlagHistorico");
            }
        }
    }
}
