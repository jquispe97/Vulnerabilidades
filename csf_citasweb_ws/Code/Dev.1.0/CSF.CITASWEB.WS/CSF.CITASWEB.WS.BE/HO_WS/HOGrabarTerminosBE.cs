namespace CSF.CITASWEB.WS.BE
{
    public class HOGrabarTerminosBE
    {
        public string ApiKey { get; set; }
        public string idAmbulatorio { get; set; }
        public string codAtencion { get; set; }
        public string indTerminosAdmision { get; set; } //Enviar siempre S (Sí)
        public string documento { get; set; } 
        public string DscTexto { get; set; } //Siempre vacío
        public string DscRpta { get; set; } //Siempre vacío
        public int flg_autorizacion1 { get; set; } // 0 (No) o 1 (si)
        public int flg_autorizacion2 { get; set; } // 0 (No) o 1 (si)
        public string accion { get; set; } //Si es 0 enviar "I" (Insertar) de lo contrario enviar "A" (Actualizar)
        public int idRegistro { get; set; } //Si es 0 es registro nuevo de lo contrario es uno existente
        public string ResponsableEnvio { get; set; } //Siempre vacío
    }
    public class HOGTextoAdicionalPresentacionBE
    {
        public string codigoRegistro { get; set; } // 01 o 02
        public string codigoBoton { get; set; } // S (Si) o N (No)
    }
    #region Response
    public class HOGrabarTerminosResponseBE
    {
        public string codEstado { get; set; }
        public string desEstado { get; set; }
    }
    public class HOGrabarTerminosPresentacionBE
    {
        public string codEstado { get; set; }
        public string desEstado { get; set; }
    }
    #endregion
}
