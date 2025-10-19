namespace CSF.CITASWEB.WS.BE
{
    public class DMResponseBE<T>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }
    public class DMResponseSimpleBE
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string data { get; set; }
    }
    public class DMDataBE
    {
        public int cod_atencion { get; set; }
        public string mensaje { get; set; }
        public int ind_rpta { get; set; }
    }
}
