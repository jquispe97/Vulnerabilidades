namespace CSF.CITASWEB.WS.BE
{
    public class DMPagoCitaBE
    {
        public int cod_atencion { get; set; }
        public string ruc_aseguradora { get; set; }
        public decimal copago_fijo_inc_igv { get; set; }
        public string correo_receptor { get; set; }
        public string fechahora_registro { get; set; }
        public int tipo_comprobante { get; set; }
        public string ruc_comprobante { get; set; }
        public string razon_social { get; set; }
        public string direccion { get; set; }
        public string num_operacion { get; set; }
        public string email_user_app { get; set; }
    }

    public class DMPagoCitaOperacionBE
    {
        public int cod_atencion { get; set; }
        public string ruc_aseguradora { get; set; }
        public decimal copago_fijo_inc_igv { get; set; }
        public string correo_receptor { get; set; }
        public string fechahora_registro { get; set; }
        public int tipo_comprobante { get; set; }
        public string ruc_comprobante { get; set; }
        public string razon_social { get; set; }
        public string direccion { get; set; }
        public string num_operacion { get; set; }
        public string fecha_operacion { get; set; }
        public string hora_operacion { get; set; }
        public string numero_tarjeta { get; set; }
        public string tipo_tarjeta { get; set; }
        public string email_user_app { get; set; }
    }
}
