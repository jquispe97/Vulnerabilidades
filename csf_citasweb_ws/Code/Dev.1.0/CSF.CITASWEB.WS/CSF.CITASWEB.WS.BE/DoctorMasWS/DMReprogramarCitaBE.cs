namespace CSF.CITASWEB.WS.BE
{
    public class DMReprogramarCitaBE
    {
        public int tipo_documento { get; set; }
        public string numero_documento { get; set; }
        public int cod_atencion { get; set; }
        public string fecha_atencion { get; set; }
        public string hora_atencion { get; set; }
        public string cmp_medico { get; set; }
        public int id_cita { get; set; }
        public string email_user_app { get; set; }
    }
}
