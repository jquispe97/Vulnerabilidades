using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace CSF.CITASWEB.WS.BatchFinesAdicionales
{
    public class ArchivoTexto<T>
    {
        public static void GenerarArchivo(T obj, string rutaArchivo = null, string nombreArchivo = "")
        {
            if (rutaArchivo == null)
            {
                rutaArchivo = $"{ClasesGenericas.GetSetting("_RutaLog")}{ClasesGenericas.AnioMesDia(ClasesGenericas.GetSetting("_NombreLog"), ".txt")}";
            }
            PropertyInfo[] properties = obj.GetType().GetProperties();
            using (FileStream stream = new FileStream(rutaArchivo + nombreArchivo, FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter writer = new StreamWriter(stream, Encoding.Default))
                {
                    writer.WriteLine("Fecha y Hora = " + DateTime.Now);
                    foreach (PropertyInfo info in properties)
                    {
                        writer.Write(info.Name);
                        writer.Write(" = ");
                        writer.WriteLine((info.GetValue(obj, null) == null) ? "" : info.GetValue(obj, null).ToString());
                    }
                    writer.WriteLine(new string('_', 50));
                }
            }
        }

        public static void GenerarArchivoTexto(string contenido, string rutaArchivo = null, string nombreArchivo = "")
        {
            if (rutaArchivo == null)
            {
                rutaArchivo = $"{ClasesGenericas.GetSetting("_RutaLog")}{ClasesGenericas.AnioMesDia(ClasesGenericas.GetSetting("_NombreLog"), ".txt")}";
            }
            using (FileStream stream = new FileStream(rutaArchivo + nombreArchivo, FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter writer = new StreamWriter(stream, Encoding.Default))
                {
                    writer.WriteLine(contenido);
                    writer.WriteLine(new string('_', 50));
                }
            }
        }
    }
}
