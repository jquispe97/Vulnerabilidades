namespace CSF.CITASWEB.WS.BE
{
    public class DMCreacionCitaBE
    {
        public int tipo_servicio { get; set; }
        public int tipo_documento { get; set; }
        public string numero_documento { get; set; }
        public string fecha_nacimiento { get; set; }
        public string genero { get; set; }
        public string nombre_paciente { get; set; }
        public string apellido_paterno { get; set; }
        public string apellido_materno { get; set; }
        public string correo_electronico { get; set; }
        public string telefono_movil { get; set; }
        public string motivo_consulta { get; set; }
        public string fecha_atencion { get; set; }
        public string hora_atencion { get; set; }
        public string cmp_medico { get; set; }
        public int id_cita { get; set; }//Enviar 0 por defecto
        public string email_user_app { get; set; }
    }
}
