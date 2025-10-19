namespace CSF.CITASWEB.WS.BE
{
    public class HOGenericoBE
    {
        public bool success { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public string data { get; set; }
        public HOGenericoBE()
        {
            code = 0;
            message = "";
            data = "";
        }
    }
}
