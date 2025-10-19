namespace CSF.CITASWEB.WS.BE
{
    public class QR_ListarQRBE
    {
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
    }
    public class QR_ObjetoQRResponseBE
    {
        public string paciente { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string doctor { get; set; }
        public string nombreEspecialidad { get; set; }
        public string url_qr { get; set; }
        public string cod_atencion { get; set; }
        public string tipoServicio { get; set; }
    }
}
