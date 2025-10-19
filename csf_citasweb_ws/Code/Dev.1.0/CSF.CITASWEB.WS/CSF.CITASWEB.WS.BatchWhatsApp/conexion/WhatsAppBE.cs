using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.CITASWEB.WS.BatchWhatsApp.conexion
{
    public class WhatsAppBE
    {
        public int serviceld { get; set; }
        public string phoneNumber { get; set; }
        public string text { get; set; }
        public ParametersBE parameters { get; set; }
        public bool sendHSMIfCaseOpenAnyways { get; set; }
        public string[] tags { get; set; }
        public HsmBE hsm { get; set; }
        public bool extendedCase { get; set; }
    }
    public class ParametersBE
    {
        public string templateName { get; set; }
        public string templateNamespace { get; set; }
        public string[] templateData { get; set; }
        public string language { get; set; }
    }
    public class HsmBE
    {
        public string elementName { get; set; }
        public string @namespace { get; set; }
        public string language { get; set; }
        public HeaderBE header { get; set; }
        public BodyBE body { get; set; }
        public List<ButtonBE> buttons { get; set; }
        public int extendedCaseOption { get; set; }
        public object extendedCaseData { get; set; }
    }
    public class HeaderBE
    {
        public string type { get; set; }
        public TextBE text { get; set; }
        public MediaBE media { get; set; }
    }
    public class TextBE
    {
        public ParameterBE parameter { get; set; }
    }
    public class MediaBE
    {
        public string type { get; set; }
        public DocumentBE document { get; set; }
    }
    public class DocumentBE
    {
        public string filename { get; set; }
        public string url { get; set; }
        public bool isPublic { get; set; }
        public FileBE file { get; set; }
    }
    public class FileBE
    {
        public string mimeType { get; set; }
        public string name { get; set; }
        public string data { get; set; }
    }
    public class ButtonBE
    {
        public string type { get; set; }
        public int index { get; set; }
        public string sub_type { get; set; }
        public ParameterBE parameter { get; set; }
    }

    public class ParameterBE
    {
        public string name { get; set; }
        public string value { get; set; }
    }
    public class BodyBE
    {
        public string name { get; set; }
        public string nombre { get; set; }
        public string fechahora { get; set; }
    }
}
