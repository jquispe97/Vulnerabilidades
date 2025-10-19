namespace CSF.CITASWEB.WS.BE
{
    public class REHR_ReporteByteBE
    {
        public string cod_atencion { get; set; }
        public int cod_paciente { get; set; }
        public string tipo_reporte { get; set; }
        public int cod_cpt { get; set; }
    }
    public class REHR_ReporteByteResponseBE
    {
        public string archivoByte { get; set; }
    }
}
