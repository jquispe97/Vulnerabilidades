using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CSF.CITASWEB.WS.BE
{
    [DataContract]
    public class AutenticacionBE
    {
        [DataMember]
        public UsuarioBE Usuario { get; set; }
        [DataMember]
        public TokenBE Token { get; set; }
    }

    [DataContract]
    public class TokenBE
    {
        [DataMember]
        public string token { get; set; }
    }
    [DataContract]
    public class DataPassword
    {
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string celular { get; set; }
    }
    [DataContract]
    public class DataSms
    {
        [DataMember]
        public string Estado { get; set; }
    }

    [DataContract]
    public class UsuarioBE
    {
        [DataMember]
        public string tipoDocumento { get; set; }
        [DataMember]
        public string numeroDocumento { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidoPaterno { get; set; }
        [DataMember]
        public string apellidoMaterno { get; set; }
        [DataMember]
        public string fechaNacimiento { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string celular { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string genero { get; set; }
        [DataMember]
        public string foto { get; set; }
        [DataMember]
        public string nombreArchivoUsuario { get; set; }
        [DataMember]
        public string tipoPaciente { get; set; }
        [DataMember]
        public string seguroPlanSaludCodigo { get; set; }
        [DataMember]
        public string idPais { get; set; }
        [DataMember]
        public string idDepartamento { get; set; }
        [DataMember]
        public string idProvincia { get; set; }
        [DataMember]
        public string idDistrito { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string idPaisNac { get; set; }
        [DataMember]
        public string idDepartamentoNac { get; set; }
        [DataMember]
        public string idProvinciaNac { get; set; }
        [DataMember]
        public string idDistritoNac { get; set; }
        [DataMember]
        public string observacion { get; set; }
        [DataMember]
        public string tipoUsuario { get; set; }
        [DataMember]
        public string cmp { get; set; }
        [DataMember]
        public bool esMedico { get; set; }
        [DataMember]
        public bool finalidadesAdiciona { get; set; }
        [DataMember]
        public string idAmbulatorio { get; set; }
        public string numeroDireccion { get; set; }
        public string numeroDepartamento { get; set; }
        public string referencia { get; set; }
        public string idUbigeo { get; set; }
        public string latlong { get; set; }
        public string idClinica { get; set; }
        public string rucSeguro { get; set; }
        public string rucSeguroPrograma { get; set; }
        public string beneficio { get; set; }
        public string tipoPago { get; set; }
        public string monto { get; set; }
        public string pais { get; set; }
        [DataMember]
        public string departamento { get; set; }
        [DataMember]
        public string provincia { get; set; }
        [DataMember]
        public string distrito { get; set; }
        public string paisNac { get; set; }
        public string departamentoNac { get; set; }
        public string provinciaNac { get; set; }
        public string distritoNac { get; set; }
        public string password { get; set; }
        [DataMember]
        public bool esRENIEC { get; set; }
        public string hashUsuario { get; set; }
        [DataMember]
        public string iconoEsRENIEC { get; set; }
        [DataMember]
        public string textoEsRENIEC { get; set; }
        [DataMember]
        public string abrPais { get; set; }
        [DataMember]
        public string desTipoDocumento { get; set; }
        [DataMember]
        public string codPostal { get; set; }
        public string codTipoPaciente { get; set; }
        public string iafas { get; set; }
    }

    [DataContract]
    public class FamiliarBE
    {
        [DataMember]
        public string tipoDocumento { get; set; }
        [DataMember]
        public string numeroDocumento { get; set; }
        [DataMember]
        public string codigoTipoParentesco { get; set; }
        [DataMember]
        public string tipoParentesco { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidoPaterno { get; set; }
        [DataMember]
        public string apellidoMaterno { get; set; }
        [DataMember]
        public string fechaNacimiento { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string celular { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string genero { get; set; }
        [DataMember]
        public string foto { get; set; }
        [DataMember]
        public string nombreArchivoUsuario { get; set; }
        [DataMember]
        public string tipoPaciente { get; set; }
        [DataMember]
        public string seguroPlanSaludCodigo { get; set; }
        [DataMember]
        public string idPais { get; set; }
        [DataMember]
        public string idDepartamento { get; set; }
        [DataMember]
        public string idProvincia { get; set; }
        [DataMember]
        public string idDistrito { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string idPaisNac { get; set; }
        [DataMember]
        public string idDepartamentoNac { get; set; }
        [DataMember]
        public string idProvinciaNac { get; set; }
        [DataMember]
        public string idDistritoNac { get; set; }
        [DataMember]
        public string observacion { get; set; }
        [DataMember]
        public bool finalidadesAdiciona { get; set; }
        [DataMember]
        public string idAmbulatorio { get; set; }
        public string numeroDireccion { get; set; }
        public string numeroDepartamento { get; set; }
        public string referencia { get; set; }
        public string idUbigeo { get; set; }
        public string latlong { get; set; }
        public string ubigeoDescripcion { get; set; }
        public string imagen { get; set; }
        public string rucSeguro { get; set; }
        public string pais { get; set; }
        public string departamento { get; set; }
        public string provincia { get; set; }
        public string distrito { get; set; }
        public string paisNac { get; set; }
        public string departamentoNac { get; set; }
        public string provinciaNac { get; set; }
        public string distritoNac { get; set; }
        [DataMember]
        public bool esRENIEC { get; set; }
        [DataMember]
        public string iconoEsRENIEC { get; set; }
        [DataMember]
        public string textoEsRENIEC { get; set; }
        [DataMember]
        public string codEstadoSolicitud { get; set; }
    }
    public class PacienteCovid
    {
        [DataMember]
        public bool esPacienteCovid { get; set; }
        [DataMember]
        public string backgroundBannerHex { get; set; }
        [DataMember]
        public BackgroundBannerRgb backgroundBannerRgb { get; set; }
        [DataMember]
        public string fontBannerHex { get; set; }
        [DataMember]
        public FontBannerRgb fontBannerRgb { get; set; }
        [DataMember]
        public string mensaje { get; set; }

    }
    public class BackgroundBannerRgb
    {
        [DataMember]
        public int red { get; set; }
        [DataMember]
        public int green { get; set; }
        [DataMember]
        public int blue { get; set; }
    }
    public class FontBannerRgb
    {
        [DataMember]
        public int red { get; set; }
        [DataMember]
        public int green { get; set; }
        [DataMember]
        public int blue { get; set; }
    }
    [DataContract]
    public class UsuarioDatosBE
    {
        [DataMember]
        public string tipoDocumento { get; set; }
        [DataMember]
        public string numeroDocumento { get; set; }
        [DataMember]
        public string tipoParentesco { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidoPaterno { get; set; }
        [DataMember]
        public string apellidoMaterno { get; set; }
        [DataMember]
        public string fechaNacimiento { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string celular { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string genero { get; set; }
        [DataMember]
        public string foto { get; set; }
        [DataMember]
        public string nombreArchivoUsuario { get; set; }
        [DataMember]
        public string tipoPaciente { get; set; }
        [DataMember]
        public string seguroPlanSaludCodigo { get; set; }
        [DataMember]
        public string idPais { get; set; }
        [DataMember]
        public string idDepartamento { get; set; }
        [DataMember]
        public string idProvincia { get; set; }
        [DataMember]
        public string idDistrito { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string idPaisNac { get; set; }
        [DataMember]
        public string idDepartamentoNac { get; set; }
        [DataMember]
        public string idProvinciaNac { get; set; }
        [DataMember]
        public string idDistritoNac { get; set; }
        [DataMember]
        public string observacion { get; set; }
        [DataMember]
        public string idAmbulatorio { get; set; }
        public string referencia { get; set; }
        public string ciudad { get; set; }
        public string numeroDireccion { get; set; }
        public string numeroDepartamento { get; set; }
        public string urbanizacion { get; set; }
        public string direccionCompleta { get; set; }
        public string ubigeoId { get; set; }
        public string pais { get; set; }
        [DataMember]
        public string departamento { get; set; }
        [DataMember]
        public string provincia { get; set; }
        [DataMember]
        public string distrito { get; set; }
        public string paisNac { get; set; }
        public string departamentoNac { get; set; }
        public string provinciaNac { get; set; }
        public string distritoNac { get; set; }
        public string rucSeguro { get; set; }
        //[DataMember]
        //public string imagen { get; set; }
        [DataMember]
        public bool esRENIEC { get; set; }
        [DataMember]
        public string iconoEsRENIEC { get; set; }
        [DataMember]
        public string textoEsRENIEC { get; set; }
        [DataMember]
        public string codEstadoSolicitud { get; set; }
    }
    public class UsuarioFamiliaresBE
    {
        [DataMember]
        public UsuarioDatosBE titular { get; set; }
        [DataMember]
        public List<UsuarioDatosBE> familiares { get; set; }

    }

    [DataContract]
    public class PacienteDatosBE
    {

        [DataMember]
        public string tipoDocumento { get; set; }

        [DataMember]
        public string numeroDocumento { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidoPaterno { get; set; }
        [DataMember]
        public string apellidoMaterno { get; set; }
        [DataMember]
        public string genero { get; set; }
        [DataMember]
        public string fechaNacimiento { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string telefonoFijo { get; set; }
        [DataMember]
        public string telefonoCelular { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string muestraHistorico { get; set; }
        [DataMember]
        public string indicadorInscritoPrograma { get; set; }
        [DataMember]
        public string finalidadesAdiciona { get; set; }
        [DataMember]
        public string citaRestringida { get; set; }
        [DataMember]
        public string validacionReniec { get; set; }
        [DataMember]
        public string ubigeoId { get; set; }
        [DataMember]
        public string referencia { get; set; }

        [DataMember]
        public string numeroDireccion { get; set; }
        [DataMember]
        public string numeroDepartamento { get; set; }
        [DataMember]
        public string urbanizacion { get; set; }
        [DataMember]
        public string direccionCompleta { get; set; }
        [DataMember]
        public string ciudad { get; set; }
        [DataMember]
        public string latLong { get; set; }

    }
    public class UsuarioDocumentoBE
    {
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public bool indicadorInvitado { get; set; }
    }
    [DataContract]
    public class UsuarioDatosContactoMFABE
    {
        [DataMember]
        public string codigo { get; set; }
        [DataMember]
        public string mensaje { get; set; }
        [DataMember]
        public string celular { get; set; }
        [DataMember]
        public string correo { get; set; }
    }
    [DataContract]
    public class AutenticarMFABE
    {
        [DataMember]
        public bool requiereMFA { get; set; }
        [DataMember]
        public string tokenAutenticacion { get; set; }
        [DataMember]
        public string expiracionTokenAutenticacion { get; set; }
        [DataMember]
        public string expiracionCodigoMFA { get; set; }
        [DataMember]
        public string minutosVigenciaCodigoMFA { get; set; }
    }
    [DataContract]
    public class ValidarCodigoMFABE
    {
        public bool indicadorBloqueado { get; set; }
        [DataMember]
        public string expiracionBloqueo { get; set; }
    }
    [DataContract]
    public class ValidarClaveBE
    {
        public bool indicadorBloqueado { get; set; }
        [DataMember]
        public string expiracionBloqueo { get; set; }
    }
    [DataContract]
    public class PersonaRegistroBE
    {
        [DataMember]
        public string celular { get; set; }
        [DataMember]
        public string mensajeAlerta { get; set; }
        [DataMember]
        public string celularMascara { get; set; }
    }
    [DataContract]
    public class ValidaFamiliarBE
    {
        [DataMember]
        public string celular { get; set; }
        [DataMember]
        public string mensajeAlerta { get; set; }
        [DataMember]
        public string celularMascara { get; set; }
        [DataMember]
        public string codEstadoSolicitud { get; set; }
    }
    [DataContract]
    public class ValidarPersonaSitedsBE
    {
        [DataMember]
        public string formatos { get; set; }
        [DataMember]
        public string codEstadoSolicitud { get; set; }
    }

    [DataContract]
    public class RegistrarUsuarioBE

    {
        [DataMember]
        public string fcmTokenNotificacion { get; set; }
        [DataMember]
        public string tituloNotificacion { get; set; }
        [DataMember]
        public string textoNotificacion { get; set; }
    }

    [DataContract]
    public class ConfiguracionSitedsBE

    {
        [DataMember]
        public string urlBase { get; set; }
        [DataMember]
        public string rucClinica { get; set; }
        [DataMember]
        public string sunasa { get; set; }
        [DataMember]
        public string formatos { get; set; }
    }

    public class AgregarFamiliarBE
    {
        public string nombreCompleto { get; set; }
        
        public string fechaSolicitud { get; set; }
        
        public string horaSolicitud { get; set; }
        
        public string tipoDocumentoDes { get; set; }
        
        public string nombreCompletoTitular { get; set; }
        
        public PlantillaCorreoBE plantillaCorreo { get; set; }
    }

    public class EliminarFamiliarBE
    {
        public string nombreCompleto { get; set; }

        public string fechaSolicitud { get; set; }

        public string horaSolicitud { get; set; }

        public PlantillaCorreoBE plantillaCorreo { get; set; }
    }
}
