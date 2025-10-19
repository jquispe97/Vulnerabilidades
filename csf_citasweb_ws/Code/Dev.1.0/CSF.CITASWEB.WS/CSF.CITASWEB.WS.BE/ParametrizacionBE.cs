using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CSF.CITASWEB.WS.BE
{
    [DataContract]
    public class VersionAplicacionBE
    {
        [DataMember]
        public string version { get; set; }
        [DataMember]
        public bool esMandatorio { get; set; }
        //[DataMember]
        //public string token { get; set; }
    }

    [DataContract]
    public class ParametroBE
    {
        [DataMember]
        public string CodigoParametro { get; set; }
        [DataMember]
        public string ValorParametro { get; set; }
    }

    [DataContract]
    public class SeguroBE
    {
        [DataMember]
        public string RUCSeguro { get; set; }
        [DataMember]
        public string RazonSocial { get; set; }
        [DataMember]
        public string CodigoIAFAS { get; set; }
    }
    [DataContract]
    public class TerminosBE
    {
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public string contenido { get; set; }
    }
    [DataContract]
    public class DataRuc
    {
        [DataMember]
        public string Ruc { get; set; }
        [DataMember]
        public string RazonSocial { get; set; }
        //[DataMember]
        public string NombreComercial { get; set; }
        //[DataMember]
        public string Tipo { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public string Condicion { get; set; }
        //[DataMember]
        public string FechaInscripcion { get; set; }
        //[DataMember]
        public string FechaInicioActividades { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public string Distrito { get; set; }
        [DataMember]
        public string Provincia { get; set; }
        [DataMember]
        public string Departamento { get; set; }

    }
    [DataContract]
    public class Company
    {
        [DataMember]
        public string Ruc { get; set; }
        [DataMember]
        public string RazonSocial { get; set; }
        [DataMember]
        public string NombreComercial { get; set; }
        [DataMember]
        public string Tipo { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public string Condicion { get; set; }
        [DataMember]
        public string FechaInscripcion { get; set; }
        [DataMember]
        public string FechaInicioActividades { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public string Distrito { get; set; }
        [DataMember]
        public string Provincia { get; set; }
        [DataMember]
        public string Departamento { get; set; }
        [DataMember]
        public string ProfesionOficio { get; set; }
        [DataMember]
        public string SisEmisionComprobante { get; set; }
        [DataMember]
        public string SisContabilidad { get; set; }
        [DataMember]
        public string ActComercioExterior { get; set; }
        [DataMember]
        public List<string> ActividadesEconomicas { get; set; }
        [DataMember]
        public List<string> ComprobantesPagoAutImpresion { get; set; }
        [DataMember]
        public string ObligadoEmitirCPE { get; set; }
        [DataMember]
        public List<string> SisEmisionElectronica { get; set; }
        [DataMember]
        public string EmisorElectronicoDesde { get; set; }
        [DataMember]
        public List<string> ComprobantesElectronicos { get; set; }
        [DataMember]
        public string AfiliadoPLEDesde { get; set; }
        [DataMember]
        public List<string> Padrones { get; set; }
    }

    [DataContract]
    public class UbigeoBE
    {
        [DataMember]
        public string ubigeoId { get; set; }
        [DataMember]
        public string departamento { get; set; }
        [DataMember]
        public string provincia { get; set; }
        [DataMember]
        public string distrito { get; set; }
    }

    [DataContract]
    public class IndicadorPopupDrOnlineBE
    {
        [DataMember]
        public bool IndicadorPopUp { get; set; }
        [DataMember]
        public string Imagen { get; set; }
    }

    [DataContract]
    public class ValidarFuncionalidadBE
    {
        [DataMember]
        public bool IndicadorFuncionalidad { get; set; }
    }

    [DataContract]
    public class IndicadorPopUpHomeBE
    {
        [DataMember]
        public bool IndicadorPopUp { get; set; }
        [DataMember]
        public string Imagen { get; set; }
    }

    [DataContract]
    public class RUDatosBE
    {
        [DataMember]
        public List<GenericoBE> tiposDocumento { get; set; }
        [DataMember]
        public List<GenericoBE> generos { get; set; }
        [DataMember]
        public List<GenericoBE> tiposParentesco { get; set; }
        public RUDatosBE ()
        {
            tiposDocumento = new List<GenericoBE>();
            generos = new List<GenericoBE>();
            tiposParentesco = new List<GenericoBE>();
        }
    }

    [DataContract]
    public class GenericoBE
    {
        [DataMember]
        public string codigo { get; set; }
        [DataMember]
        public string valor { get; set; }
    }
    [DataContract]
    public class PaisBE
    {
        public string idUbigeo { get; set; }
        [DataMember]
        public string idPais { get; set; }
        [DataMember]
        public string pais { get; set; }
    }
    [DataContract]
    public class DepartamentoBE
    {
        public string idUbigeo { get; set; }
        [DataMember]
        public string idDepartamento { get; set; }
        [DataMember]
        public string departamento { get; set; }
    }
    [DataContract]
    public class ProvinciaBE
    {
        public string idUbigeo { get; set; }
        [DataMember]
        public string idProvincia { get; set; }
        [DataMember]
        public string provincia { get; set; }
    }
    [DataContract]
    public class DistritoBE
    {
        public string idUbigeo { get; set; }
        [DataMember]
        public string idDistrito { get; set; }
        [DataMember]
        public string distrito { get; set; }
    }
    [DataContract]
    public class TextoBE
    {
        [DataMember]
        public string titulo { get; set; }
        [DataMember]
        public string texto { get; set; }
        public string exportarPDF { get; set; }
        [DataMember]
        public List<TextoDetalleBE> textosAdicionales { get; set; }
    }
    [DataContract]
    public class TextoDetalleBE
    {
        [DataMember]
        public string codigo { get; set; }
        [DataMember]
        public string texto { get; set; }
        public string exportarPDF { get; set; }
        [DataMember]
        public List<TextoDetalleOpcionBE> botones { get; set; }
        public TextoDetalleBE()
        {
            botones = new List<TextoDetalleOpcionBE>();
        }
    }
    [DataContract]
    public class TextoDetalleOpcionBE
    {
        [DataMember]
        public string codigo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public string atributos { get; set; }
        public string codigoDetalle { get; set; }
        public string descripcionPDF { get; set; }
    }
    [DataContract]
    public class DataDocumento
    {
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidoPaterno { get; set; }
        [DataMember]
        public string apellidoMaterno { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string fechaNacimiento { get; set; }
        [DataMember]
        public string celular { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string genero { get; set; }
        public DataDocumento ()
        {
            this.nombres = "";
            this.apellidoPaterno = "";
            this.apellidoMaterno = "";
            this.direccion = "";
            this.fechaNacimiento = "";
            this.celular = "";
            this.email = "";
            this.genero = "";
        }
    }
    [DataContract]
    public class HODatosBE
    {
        [DataMember]
        public List<GenericoBE> tiposDocumento { get; set; }
        [DataMember]
        public List<GenericoBE> tiposParentesco { get; set; }
        public HODatosBE()
        {
            tiposDocumento = new List<GenericoBE>();
            tiposParentesco = new List<GenericoBE>();
        }
    }
    [DataContract]
    public class ENDatosBE
    {
        [DataMember]
        public URLBE roe { get; set; }
        [DataMember]
        public URLBE contactanos { get; set; }
        public ENDatosBE()
        {
            roe = new URLBE();
            contactanos = new URLBE();
        }
    }

    [DataContract]
    public class URLBE
    {
        [DataMember]
        public string titulo { get; set; }
        [DataMember]
        public string URL { get; set; }
        [DataMember]
        public string target { get; set; }
        [DataMember]
        public string icono { get; set; }
    }

    public class RequestLoginPersonaBE
    {
        public string usuario { get; set; }
        public string password { get; set; }
    }

    public class ResponseLoginPersonaBE
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }

    //public class RequestPersonaBE
    //{
    //    public string token { get; set; }
    //    public string dni { get; set; }
    //}

    public class ResponsePersonaBE
    {
        public string mensaje { get; set; }
        public List<PersonaBE> persona { get; set; }
        public ResponsePersonaBE ()
        {
            persona = new List<PersonaBE>();
        }
    }

    public class PersonaBE
    {
        public string codigo { get; set; }
        public string nombres { get; set; }
        public string docIdentidad { get; set; }
        public string direccion { get; set; }
        public string nombre { get; set; }
        public string apPaterno { get; set; }
        public string apMaterno { get; set; }
        public string parentesco { get; set; }
        public string correo { get; set; }
        public string celular { get; set; }
        public string tabla { get; set; }
    }

    public class RequestMigoRUCBE
    {
        public string token { get; set; }
        public string ruc { get; set; }
    }

    public class ResponseMigoRUCBE
    {
        public bool success { get; set; }
        public string ruc { get; set; }
        public string nombre_o_razon_social { get; set; }
        public string estado_del_contribuyente { get; set; }
        public string condicion_de_domicilio { get; set; }
        public string ubigeo { get; set; }
        public string tipo_de_via { get; set; }
        public string nombre_de_via { get; set; }
        public string codigo_de_zona { get; set; }
        public string tipo_de_zona { get; set; }
        public string numero { get; set; }
        public string interior { get; set; }
        public string lote { get; set; }
        public string dpto { get; set; }
        public string manzana { get; set; }
        public string kilometro { get; set; }
        public string distrito { get; set; }
        public string provincia { get; set; }
        public string departamento { get; set; }
        public string direccion_simple { get; set; }
        public string direccion { get; set; }
        public string actualizado_en { get; set; }
    }

    [DataContract]
    public class ContenidoBE
    {
        public string codigo { get; set; }
        [DataMember]
        public string contenido { get; set; }
    }

    public class ConfiguracionMFABE
    {
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string codigoEmpresa { get; set; }
        public string codigoAplicativo { get; set; }
        public string claveAplicativo { get; set; }
        public string codigoDispositivo { get; set; }
        public string tipoFlujo { get; set; }
    }

    [DataContract]
    public class ParametroSeguridadBE
    {
        [DataMember]
        public ParametroSeguridadCampoBE longitudMinimaContrasena { get; set; }
        [DataMember]
        public ParametroSeguridadCampoBE longitudMaximaContrasena { get; set; }
        [DataMember]
        public ParametroSeguridadCampoBE indicadorMinuscula { get; set; }
        [DataMember]
        public ParametroSeguridadCampoBE indicadorNumero { get; set; }
        [DataMember]
        public ParametroSeguridadCampoBE indicadorMayuscula { get; set; }
        [DataMember]
        public ParametroSeguridadCampoBE indicadorCaracterEspecial { get; set; }
    }

    [DataContract]
    public class ParametroSeguridadCampoBE
    {
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string valor { get; set; }
    }

    public class PlantillaCorreoBE

    {
        public string para { get; set; }
        public string cc { get; set; }
        public string cco { get; set; }
        public string mensajeAlerta { get; set; }
        public string asunto { get; set; }
        public string cuerpo { get; set; }
    }
}
