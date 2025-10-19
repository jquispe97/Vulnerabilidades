using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CSF.CITASWEB.WS.BE
{
    [DataContract]
    public class CitaBE
    {
        [DataMember]
        public MedicoBE medico { get; set; }
        [DataMember]
        public string fecha { get; set; }
        [DataMember]
        public EspecialidadBE especialidad { get; set; }
        [DataMember]
        public ClinicaBE clinica { get; set; }
        [DataMember]
        public UsuarioBE paciente { get; set; }
    }

    [DataContract]
    public class CitasListadoBE
    {
        [DataMember]
        public List<CitaVistaPreviaBE> CitasPendientes { get; set; }
        [DataMember]
        public List<CitaHistoricaVistaPreviaBE> CitasHistoricas { get; set; }
    }
    [DataContract]
    public class RegistrarCitaBE
    {
        [DataMember]
        public string idCita { get; set; }
        [DataMember]
        public bool pagar { get; set; }
        [DataMember]
        public bool esPrepago { get; set; }
        public bool procesarPago { get; set; }
        public string tarjeta { get; set; }
        public string tipoTarjeta { get; set; }
        public bool enviarCorreo { get; set; }
        public string idClinica { get; set; }
        public string horaInicio { get; set; }
    }

    [DataContract]
    public class CitaVistaPreviaBE
    {
        public DateTime fechaOrdenamiento { get; set; }
        [DataMember]
        public string idCita { get; set; }
        [DataMember]
        public string nombrePaciente { get; set; }
        [DataMember]
        public string nombrePaciente2 { get; set; }
        [DataMember]
        public string cmp { get; set; }
        [DataMember]
        public string nombreMedico { get; set; }
        [DataMember]
        public string fechaAtencion { get; set; }
        [DataMember]
        public string horaInicio { get; set; }
        [DataMember]
        public string horaFin { get; set; }
        [DataMember]
        public string idEspecialidad { get; set; }
        [DataMember]
        public string especialidad { get; set; }
        [DataMember]
        public string idClinica { get; set; }
        [DataMember]
        public string clinica { get; set; }
        //[DataMember]
        public string idMedicoFavorito { get; set; }
        [DataMember]
        public bool esCitaVirtual { get; set; }
        [DataMember]
        public string fuePagado { get; set; }
        [DataMember]
        public string descripcionPago { get; set; }
        [DataMember]
        public string tipoPago { get; set; }
        //[DataMember]
        public int tiempoPrevioCita { get; set; }
        //[DataMember]
        public bool anular { get; set; }
        //[DataMember]
        public bool anularPago { get; set; }
        [DataMember]
        public bool mostrarBotonesPago { get; set; }
        //[DataMember]
        public bool mostrarBotonesPag { get; set; }
        [DataMember]
        public bool mostrarFilaEspera { get; set; }
        [DataMember]
        public string consultorio { get; set; }
        [DataMember]
        public string codigoTipoPaciente { get; set; }
        [DataMember]
        public string tipoPaciente { get; set; }
        [DataMember]
        public string codigoTipoAtencionHorario { get; set; }
        [DataMember]
        public string tipoAtencionHorario { get; set; }
        [DataMember]
        public string codigoTipoAtencionCita { get; set; }
        [DataMember]
        public string tipoAtencionCita { get; set; }
        [DataMember]
        public int tipoDocumento { get; set; }
        [DataMember]
        public string numeroDocumento { get; set; }
        [DataMember]
        public int idHorario { get; set; }
        [DataMember]
        public string cantidadPersonasDelante { get; set; }
        [DataMember]
        public string subEspecialidad { get; set; }
        [DataMember]
        public int idEstado { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string tiempoPrevioColaVirtual { get; set; }
        [DataMember]
        public string tiempoPrevioIngresoVirtual { get; set; }
        [DataMember]
        public string tiempoPosteriorIngresoVirtual { get; set; }
        [DataMember]
        public string abreviaturaMedico { get; set; }
        public string sexoMedico { get; set; }
        [DataMember]
        public string fotoMedico { get; set; }
        [DataMember]
        public string metodoPago { get; set; }
        [DataMember]
        public bool esAdicional { get; set; }
        [DataMember]
        public string textoAdicional { get; set; }
        [DataMember]
        public string codigoAtencion { get; set; }
        [DataMember]
        public bool tieneReceta { get; set; }
        [DataMember]
        public string iconoReceta { get; set; }
        [DataMember]
        public bool tieneHojaRuta { get; set; }
        [DataMember]
        public string iconoHojaRuta { get; set; }
        [DataMember]
        public string codigoPaciente { get; set; }

        //Procedimientos

        [DataMember]
        public string idCitaProcedimiento { get; set; }
        [DataMember]
        public string procedimiento { get; set; }
        [DataMember]
        public string idServicioHorario { get; set; }
        [DataMember]
        public string idServicio { get; set; }
        [DataMember]
        public string servicio { get; set; }
        [DataMember]
        public string tipoTerapia { get; set; }
        [DataMember]
        public string numeroSesion { get; set; }
    }

    [DataContract]
    public class CitaHistoricaVistaPreviaBE
    {
        public DateTime fechaOrdenamiento { get; set; }
        [DataMember]
        public string nombrePaciente { get; set; }
        [DataMember]
        public string nombrePaciente2 { get; set; }
        [DataMember]
        public string cmp { get; set; }
        [DataMember]
        public string nombreMedico { get; set; }
        [DataMember]
        public string fechaAtencion { get; set; }
        [DataMember]
        public string horaInicio { get; set; }
        [DataMember]
        public string horaFin { get; set; }
        [DataMember]
        public string idEspecialidad { get; set; }
        [DataMember]
        public string especialidad { get; set; }
        [DataMember]
        public string subEspecialidad { get; set; }
        [DataMember]
        public string clinica { get; set; }
        [DataMember]
        public string idMedicoFavorito { get; set; }
        [DataMember]
        public int idEstado { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public bool esCitaVirtual { get; set; }
        [DataMember]
        public string fuePagado { get; set; }
        [DataMember]
        public string descripcionPago { get; set; }
        //[DataMember]
        //public string seguro { get; set; }
        //[DataMember]
        //public string estadoFlujo { get; set; }
        //[DataMember]
        //public bool resultadoLaboratorio { get; set; }
        //[DataMember]
        //public string peticion { get; set; }
        [DataMember]
        public string idCita { get; set; }

        //[DataMember]
        //public bool indRecetaMedica { get; set; }
        //[DataMember]
        //public string numeroOrdenAtencion { get; set; }
        [DataMember]
        public string consultorio { get; set; }
        [DataMember]
        public string codigoTipoPaciente { get; set; }
        [DataMember]
        public string tipoPaciente { get; set; }
        [DataMember]
        public string codigoTipoAtencionHorario { get; set; }
        [DataMember]
        public string tipoAtencionHorario { get; set; }
        [DataMember]
        public string codigoTipoAtencionCita { get; set; }
        [DataMember]
        public string tipoAtencionCita { get; set; }
        [DataMember]
        public string abreviaturaMedico { get; set; }
        public string sexoMedico { get; set; }
        [DataMember]
        public string fotoMedico { get; set; }
        [DataMember]
        public string metodoPago { get; set; }
        [DataMember]
        public int idHorario { get; set; }
        [DataMember]
        public int tipoDocumento { get; set; }
        [DataMember]
        public string numeroDocumento { get; set; }
        [DataMember]
        public bool esAdicional { get; set; }
        [DataMember]
        public string textoAdicional { get; set; }
        [DataMember]
        public string codigoAtencion { get; set; }
        [DataMember]
        public bool tieneReceta { get; set; }
        [DataMember]
        public string iconoReceta { get; set; }
        [DataMember]
        public bool tieneHojaRuta { get; set; }
        [DataMember]
        public string iconoHojaRuta { get; set; }
        [DataMember]
        public string codigoPaciente { get; set; }

        //Procedimientos

        [DataMember]
        public string idCitaProcedimiento { get; set; }
        [DataMember]
        public string procedimiento { get; set; }
        [DataMember]
        public string idServicioHorario { get; set; }
        [DataMember]
        public string idServicio { get; set; }
        [DataMember]
        public string servicio { get; set; }
        [DataMember]
        public string tipoTerapia { get; set; }
        [DataMember]
        public string numeroSesion { get; set; }
    }

    [DataContract]
    public class DatosPagoBE
    {
        [DataMember]
        public string monto { get; set; }
        [DataMember]
        public string user { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public string merchant { get; set; }
        [DataMember]
        public string purchaseNumber { get; set; }
        [DataMember]
        public string especialidad { get; set; }
        [DataMember]
        public string nombreMedico { get; set; }
        [DataMember]
        public string nombrePaciente { get; set; }
        [DataMember]
        public string fechaAtencion { get; set; }
        [DataMember]
        public string horaInicio { get; set; }
        [DataMember]
        public string motivoCobro { get; set; }
        [DataMember]
        public string tokenEmail { get; set; }
        [DataMember]
        public string clinica { get; set; }
        [DataMember]
        public string aseguradora { get; set; }
        [DataMember]
        public string consultorio { get; set; }
        [DataMember]
        public string tiempoAtencion { get; set; }
    }

    [DataContract]
    public class CitaVirtualBE
    {
        [DataMember]
        public string idCitaVirtual { get; set; }
        [DataMember]
        public string tipoPago { get; set; }
        [DataMember]
        public bool pagar { get; set; }
        [DataMember]
        public string emailNotificacion { get; set; }
        [DataMember]
        public string idClinica { get; set; }
        [DataMember]
        public string seguro { get; set; }
        [DataMember]
        public string monto { get; set; }
        [DataMember]
        public bool esPrepago { get; set; }

        public string origen2 { get; set; }
        public string idCita { get; set; }
        public string strEsMontoCero { get; set; }

    }

    [DataContract]
    public class DatosRoomBE
    {
        [DataMember]
        public string roomName { get; set; }
        [DataMember]
        public string roomPassword { get; set; }
        [DataMember]
        public bool doctorEnLinea { get; set; }
        [DataMember]
        public string mensaje { get; set; }
        [DataMember]
        public string urlJitsi { get; set; }
    }


    [DataContract]
    public class DatosResponseSited
    {
        [DataMember]
        public string seguro { get; set; }
        [DataMember]
        public string codProducto { get; set; }
        [DataMember]
        public string desProducto { get; set; }
        [DataMember]
        public string codCobertura { get; set; }
        [DataMember]
        public string desCobertura { get; set; }
        [DataMember]
        public string monto { get; set; }
        [DataMember]
        public string codTipoMoneda { get; set; }
        [DataMember]
        public string iafas { get; set; }
        [DataMember]
        public bool vigente { get; set; }
        [DataMember]
        public bool wsError { get; set; }
        [DataMember]
        public string codAsegurado { get; set; }
    }


    [DataContract]
    public class DataTeleconsulta
    {
        [DataMember]
        public bool Estado { get; set; }
        [DataMember]
        public string MensajeAtencion { get; set; }

    }
    [DataContract]
    public class CalificacionTelecondulta
    {
        [DataMember]
        public bool mostrarCalificacion { get; set; }


    }
    [DataContract]
    public class EstadoTeleconsulta
    {
        [DataMember]
        public bool estado { get; set; }
        [DataMember]
        public string mensaje { get; set; }

    }
    [DataContract]
    public class DatosAnularCita
    {
        [DataMember]
        public string merchant { get; set; }
        [DataMember]
        public string user { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public string purchase { get; set; }

    }

    public class PreDatosBE
    {
        public DatosCitaBE DatosCita { get; set; }
        public DatosPacienteBE DatosPaciente { get; set; }
        public DatosCitaAnteriorBE DatosCitaAnterior { get; set; }

        public PreDatosBE()
        {
            DatosCita = new DatosCitaBE();
            DatosPaciente = new DatosPacienteBE();
            DatosCitaAnterior = new DatosCitaAnteriorBE();
        }

        public class DatosCitaBE
        {
            public int IDClinica { get; set; }
            public int IDMedicoSpring { get; set; }
            public string HoraAtencion { get; set; }
            public int TiempoAtencion { get; set; }
            public int IDHorarioSpring { get; set; }
            public string UnidadReplicacion { get; set; }
            public string CMP { get; set; }
            public string tipoPaciente { get; set; }
        }

        public class DatosPacienteBE
        {
            public int IDPacienteSpring { get; set; }
            public string Nombres { get; set; }
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
            public string Genero { get; set; }
            public DateTime FechaNacimiento { get; set; }
            public string Direccion { get; set; }
            public string TelefonoFijo { get; set; }
            public string TelefonoCelular { get; set; }
            public string Email { get; set; }
        }

        public class DatosCitaAnteriorBE
        {
            public bool esPagada { get; set; }
            public string tipoPaciente { get; set; }
        }
    }

    public class ParametrosCorreoBE
    {
        public string Email { get; set; }
        public string NombrePaciente { get; set; }
        public string ApellidoPaternoPaciente { get; set; }
        public string CelularPaciente { get; set; }
        public string NombreMedico { get; set; }
        public string FechaAtencion { get; set; }
        public string HoraInicio { get; set; }
        public string Especialidad { get; set; }
        public string Clinica { get; set; }
    }

    public class ValidarAnulacionCitaBE
    {
        public int IDClinica { get; set; }
        public int IDCitaClinica { get; set; }
    }
    public class ConsultAsegNom
    {
        public string CodTipoDocumentoAfiliado { get; set; }
        public string NumeroDocumentoAfiliado { get; set; }
        public string RUC { get; set; }
        public string SUNASA { get; set; }
        public string IAFAS { get; set; }
        public string NombresAfiliado { get; set; }
        public string ApellidoPaternoAfiliado { get; set; }
        public string ApellidoMaternoAfiliado { get; set; }
        public string CodEspecialidad { get; set; }

    }
    public class ConsultAsegNomResponse
    {
        public string CodProducto { get; set; }
        public string DesProducto { get; set; }
        public string ApellidoPaternoAfiliado { get; set; }
        public string ApellidoMaternoAfiliado { get; set; }
        public string NombresAfiliado { get; set; }
        public string CodParentesco { get; set; }
        public string DesParentesco { get; set; }
        public string NombreContratante { get; set; }
        public string CodEstado { get; set; }
        public string DesEstado { get; set; }
        public string CodigoAfiliado { get; set; }
        public string FechaNacimiento { get; set; }
        public string CodGenero { get; set; }
        public string DesGenero { get; set; }
        public string CodTipoDocumentoAfiliado { get; set; }
        public string DesTipoDocumentoAfiliado { get; set; }
        public string NumeroDocumentoAfiliado { get; set; }
        public string NumeroPlan { get; set; }
        public string NumeroContratoAfiliado { get; set; }
        public string NumeroDocumentoContratante { get; set; }
        public string TipoCalificadorContratante { get; set; }
        public string CodTipoDocumentoContratante { get; set; }
        public string DesTipoDocumentoContratante { get; set; }

    }
    public class ConsultAsegCod
    {
        public string SUNASA { get; set; }
        public string IAFAS { get; set; }
        public string RUC { get; set; }
        public string NombresAfiliado { get; set; }
        public string ApellidoPaternoAfiliado { get; set; }
        public string ApellidoMaternoAfiliado { get; set; }
        public string CodigoAfiliado { get; set; }
        public string CodTipoDocumentoAfiliado { get; set; }
        public string NumeroDocumentoAfiliado { get; set; }
        public string CodProducto { get; set; }
        public string DesProducto { get; set; }
        public string NumeroPlan { get; set; }
        public string CodTipoDocumentoContratante { get; set; }
        public string NumeroDocumentoContratante { get; set; }
        public string NombreContratante { get; set; }
        public string CodParentesco { get; set; }
        public string TipoCalificadorContratante { get; set; }
        public string CodEspecialidad { get; set; }

    }
    public class ConsultAsegCodResponse
    {
        public DatosAfiliado DatosAfiliado { get; set; }
        public List<Cobertura> Coberturas { get; set; }

        public ConsultAsegCodResponse()
        {
            Coberturas = new List<Cobertura>();
        }
    }
    public class DatosAfiliado
    {
        public string CodigoAfiliado { get; set; }
        public string NumeroPoliza { get; set; }
        public string NumeroContrato { get; set; }
        public string NumeroCertificado { get; set; }
        public string CodProducto { get; set; }
        public string DesProducto { get; set; }
        public string ApellidoPaternoAfiliado { get; set; }
        public string ApellidoMaternoAfiliado { get; set; }
        public string NombresAfiliado { get; set; }
        public string CodGenero { get; set; }
        public string DesGenero { get; set; }
        public string CodFechaNacimiento { get; set; }
        public string FechaNacimiento { get; set; }
        public string CodParentesco { get; set; }
        public string DesParentesco { get; set; }
        public string CodTipoDocumentoAfiliado { get; set; }
        public string DesTipoDocumentoAfiliado { get; set; }
        public string NumeroDocumentoAfiliado { get; set; }
        public string Edad { get; set; }
        public string CodFechaInicioVigencia { get; set; }
        public string FechaInicioVigencia { get; set; }
        public string CodFechaFinVigencia { get; set; }
        public string FechaFinVigencia { get; set; }
        public string CodEstadoCivil { get; set; }
        public string DesEstadoCivil { get; set; }
        public string CodTipoPlan { get; set; }
        public string DesTipoPlan { get; set; }
        public string NumeroPlan { get; set; }
        public string CodEstado { get; set; }
        public string DesEstado { get; set; }
        public string CodFechaActualizacionFoto { get; set; }
        public string FechaActualizacionFoto { get; set; }
        public string ApellidoPaternoTitular { get; set; }
        public string ApellidoMaternoTitular { get; set; }
        public string NombresTitular { get; set; }
        public string CodigoTitular { get; set; }
        public string CodTipoDocumentoTitular { get; set; }
        public string DesTipoDocumentoTitular { get; set; }
        public string NumeroDocumentoTitular { get; set; }
        public string CodMoneda { get; set; }
        public string DesMoneda { get; set; }
        public string NombreContratante { get; set; }
        public string CodTipoDocumentoContratante { get; set; }
        public string DesTipoDocumentoContratante { get; set; }
        public string CodTipoAfiliacion { get; set; }
        public string DesTipoAfiliacion { get; set; }
        public string CodFechaAfiliacion { get; set; }
        public string FechaAfiliacion { get; set; }
        public string NumeroDocumentoContratante { get; set; }
    }
    public class Cobertura
    {
        public string CodigoTipoCobertura { get; set; }
        public string CodigoSubTipoCobertura { get; set; }
        public string CodigoCobertura { get; set; }
        public string Beneficios { get; set; }
        public string CodIndicadorRestriccion { get; set; }
        public string Restricciones { get; set; }
        public string CodCopagoFijo { get; set; }
        public string DesCopagoFijo { get; set; }
        public string CodCopagoVariable { get; set; }
        public string DesCopagoVariable { get; set; }
        public string CodFechaFinCarencia { get; set; }
        public string FechaFinCarencia { get; set; }
        public string CondicionesEspeciales { get; set; }
        public string Observaciones { get; set; }
        public string CodCalificacionServicio { get; set; }
        public string DesCalificacionServicio { get; set; }
        public string BeneficioMaximoInicial { get; set; }
        public string NumeroCobertura { get; set; }
        public string CodTipoMoneda { get; set; }
        public string DesTipoMoneda { get; set; }

    }
    public class OrderAnularPagoRequest
    {
        public AnularPagoRequest order { get; set; }
    }
    public class AnularPagoRequest
    {
        public string purchaseNumber { get; set; }
        public string transactionDate { get; set; }
    }
    public class AnularPagoResponse
    {
        public HeaderVisa header { get; set; }
        public FulfillmentVisa fulfillment { get; set; }
        public OrderVisa order { get; set; }
        public DataMapVisa dataMap { get; set; }
    }
    public class DataMapVisa
    {
        public string CURRENCY { get; set; }
        public string ORIGINAL_DATETIME { get; set; }
        public string TRANSACTION_DATE { get; set; }
        public string TERMINAL { get; set; }
        public string ACTION_CODE { get; set; }
        public string TRACE_NUMBER { get; set; }
        public string ORIGINAL_TRACE { get; set; }
        public string CARD { get; set; }
        public string MERCHANT { get; set; }
        public string STATUS { get; set; }
        public string ADQUIRENTE { get; set; }
        public string AMOUNT { get; set; }
        public string PROCESS_CODE { get; set; }
        public string TRANSACTION_ID { get; set; }
    }
    public class HeaderVisa
    {
        public string ecoreTransactionUUID { get; set; }
        public string ecoreTransactionDate { get; set; }
    }
    public class FulfillmentVisa
    {
        public string channel { get; set; }
        public string merchantId { get; set; }
        public string terminalId { get; set; }
        public string captureType { get; set; }
        public bool countable { get; set; }
        public bool fastPayment { get; set; }
        public string signature { get; set; }
    }
    public class OrderVisa
    {
        public string authorizationCode { get; set; }
        public string actionCode { get; set; }
        public string traceNumber { get; set; }
        public string transactionDate { get; set; }
        public string transactionId { get; set; }
        public string originalTraceNumber { get; set; }
        public string originalDateTime { get; set; }
    }



    [DataContract]
    public class CoberturaAcreditacion
    {
        [DataMember]
        public int NumeroAutorizacion { get; set; }
        [DataMember]
        public string CO_IAFASCODE { get; set; }
        [DataMember]
        public string CO_ASEGCODE { get; set; }
        [DataMember]
        public string CO_AUTOCODE { get; set; }
        [DataMember]
        public string CO_COBERCODE { get; set; }
        [DataMember]
        public string CO_SUBTIPOCOBER { get; set; }
        [DataMember]
        public string NO_SUBTIPOCOBER { get; set; }
        [DataMember]
        public string CO_INDIPROCODE { get; set; }
        [DataMember]
        public double NU_COPGFIJO { get; set; }
        [DataMember]
        public int DE_BENEFICIOMAX { get; set; }
        [DataMember]
        public string FE_FECARDATE { get; set; }
        [DataMember]
        public string FE_ESPERDATE { get; set; }
        [DataMember]
        public string CO_FLAGCARTA { get; set; }
        [DataMember]
        public string CO_PRODCODE { get; set; }
        [DataMember]
        public string CO_ESPECODE { get; set; }
        [DataMember]
        public string DE_IPSSHOST { get; set; }
        [DataMember]
        public string CO_ORIGENATE { get; set; }
        [DataMember]
        public string CO_ACCIDENTES { get; set; }
        [DataMember]
        public string CO_TIDECLARACION { get; set; }
        [DataMember]
        public string DE_OBSERVA { get; set; }
        [DataMember]
        public string DE_COMERCNAME { get; set; }
        [DataMember]
        public double FE_ACCIDENTES { get; set; }
        [DataMember]
        public double FE_ORIGENATEN { get; set; }
        [DataMember]
        public string CO_USERCODE { get; set; }
        [DataMember]
        public string NO_USERNAME { get; set; }
        [DataMember]
        public string CO_FLAGCGCODE { get; set; }
        [DataMember]
        public string DE_FLAGNAME { get; set; }
        [DataMember]
        public string CO_INDIRESTRICO { get; set; }
        [DataMember]
        public string DE_OBSCARENCIA { get; set; }
        [DataMember]
        public int TipoTabla { get; set; }
    }

    [DataContract]
    public class DatosGenerales
    {
        [DataMember]
        public int NumeroAutorizacion { get; set; }
        [DataMember]
        public string cEPSsCode { get; set; }
        [DataMember]
        public string cAsegCode { get; set; }
        [DataMember]
        public string cAutoCode { get; set; }
        [DataMember]
        public string dAutoDate { get; set; }
        [DataMember]
        public string cClinCode { get; set; }
        [DataMember]
        public string cClinRucs { get; set; }
        [DataMember]
        public string sAsegAPat { get; set; }
        [DataMember]
        public string sAsegAMat { get; set; }
        [DataMember]
        public string sAsegName { get; set; }
        [DataMember]
        public string sTituAPat { get; set; }
        [DataMember]
        public string sTituAMat { get; set; }
        [DataMember]
        public string sTituName { get; set; }
        [DataMember]
        public string dNacmDate { get; set; }
        [DataMember]
        public int nEdadNumb { get; set; }
        [DataMember]
        public string cSexoCode { get; set; }
        [DataMember]
        public string cTituCode { get; set; }
        [DataMember]
        public string cPlanCode { get; set; }
        [DataMember]
        public string cPlanVers { get; set; }
        [DataMember]
        public string cRamoCode { get; set; }
        [DataMember]
        public string sPlanDesc { get; set; }
        [DataMember]
        public string cProdCode { get; set; }
        [DataMember]
        public string sProdDesc { get; set; }
        [DataMember]
        public string sPolitype { get; set; }
        [DataMember]
        public string cCntrRucs { get; set; }
        [DataMember]
        public string cCntrCode { get; set; }
        [DataMember]
        public string sCntrName { get; set; }
        [DataMember]
        public string sCntrType { get; set; }
        [DataMember]
        public string dlVigDate { get; set; }
        [DataMember]
        public string dFVigDate { get; set; }
        [DataMember]
        public string dlnclDate { get; set; }
        [DataMember]
        public string dFlngDate { get; set; }
        [DataMember]
        public string dUChkDate { get; set; }
        [DataMember]
        public string cPamCode { get; set; }
        [DataMember]
        public string sPamDesc { get; set; }
        [DataMember]
        public string cDocuType { get; set; }
        [DataMember]
        public string sDocuNumb { get; set; }
        [DataMember]
        public string cMoneCode { get; set; }
        [DataMember]
        public string sAsegStat { get; set; }
        [DataMember]
        public string cAtenType { get; set; }
        [DataMember]
        public string cPagoStat { get; set; }
        [DataMember]
        public string sPagoDesc { get; set; }
        [DataMember]
        public string cCamNumb { get; set; }
        [DataMember]
        public string sObseDesc { get; set; }
        [DataMember]
        public string sCondDesc { get; set; }
        [DataMember]
        public string sDiagDesc { get; set; }
        [DataMember]
        public string nCertNumb { get; set; }
        [DataMember]
        public string nPoliNumb { get; set; }
        [DataMember]
        public string nAsegiden { get; set; }
        [DataMember]
        public string nPolilden { get; set; }
        [DataMember]
        public string cPoliCode { get; set; }
        [DataMember]
        public string cAsegDNIs { get; set; }
        [DataMember]
        public string cEstaCode { get; set; }
        [DataMember]
        public string dFotoDate { get; set; }
        [DataMember]
        public string sAsegPlan { get; set; }
        [DataMember]
        public string nCntrNumb { get; set; }
        [DataMember]
        public string cAfilType { get; set; }
        [DataMember]
        public string cSCTRCode { get; set; }
        [DataMember]
        public string cSoliNumb { get; set; }
        [DataMember]
        public string UsuarioCreacion { get; set; }
        [DataMember]
        public double FechaCreacion { get; set; }
        [DataMember]
        public string CO_SEDECODE { get; set; }
        [DataMember]
        public string DE_FOTORUTA { get; set; }
        [DataMember]
        public string NU_ASEGPLAN { get; set; }
        [DataMember]
        public string NU_DNITITULAR { get; set; }
        [DataMember]
        public string CO_DNITITULAR { get; set; }
        [DataMember]
        public string CO_CNTRTIPOD { get; set; }
        [DataMember]
        public string DE_LOGSUSALUD { get; set; }
        [DataMember]
        public string CO_ESCIVIL { get; set; }
        [DataMember]
        public string NU_CERTIFICADO { get; set; }
        [DataMember]
        public string NU_CODSEGURI { get; set; }
        [DataMember]
        public string CO_IAFASCODE { get; set; }
        [DataMember]
        public string DE_CONDESPEC { get; set; }
        [DataMember]
        public string CO_RENIPRESS { get; set; }
        [DataMember]
        public int CO_DNICODE { get; set; }

    }
    [DataContract]
    public class NombreTabla
    {
        [DataMember]
        public int NumeroAutorizacion { get; set; }
        [DataMember]
        public string CO_IAFASCODE { get; set; }
        [DataMember]
        public string CO_ASEGCODE { get; set; }
        [DataMember]
        public string CO_AUTOCODE { get; set; }
        [DataMember]
        public string CO_DXPREEXT { get; set; }
        [DataMember]
        public string NO_DXPREEXT { get; set; }
        [DataMember]
        public string DE_OBSPREEXT { get; set; }
        [DataMember]
        public double NU_TOPEPREEXT { get; set; }
        [DataMember]
        public string CO_DXEXCLUS { get; set; }
        [DataMember]
        public string NO_DXEXCLUS { get; set; }
        [DataMember]
        public string DE_OBSEXCLUS { get; set; }
        [DataMember]
        public double NU_TOPEEXCLUS { get; set; }
        [DataMember]
        public string CO_DXENFCAR { get; set; }
        [DataMember]
        public string NO_DXENFCAR { get; set; }
        [DataMember]
        public string DE_OBSENFCAR { get; set; }
        [DataMember]
        public double NU_TOPEENFCAR { get; set; }
        [DataMember]
        public string CO_DXANTECED { get; set; }
        [DataMember]
        public string NO_DXANTECED { get; set; }
        [DataMember]
        public string DE_OBSANTECTED { get; set; }
        [DataMember]
        public double NU_TOPEANTECTED { get; set; }
        [DataMember]
        public string CO_DXENFERMED { get; set; }
        [DataMember]
        public string NO_DXENFERMED { get; set; }
        [DataMember]
        public string DE_OBSENFERMED { get; set; }
        [DataMember]
        public double NU_TOPEENFER { get; set; }
        [DataMember]
        public string DE_FLAGCONMEDI { get; set; }

    }

    [DataContract]
    public class Procedimientos
    {
        [DataMember]
        public int NumeroAutorizacion { get; set; }
        [DataMember]
        public string CO_IAFASCODE { get; set; }
        [DataMember]
        public string CO_ASEGCODE { get; set; }
        [DataMember]
        public string CO_AUTOCODE { get; set; }
        [DataMember]
        public string CO_COBERCODE { get; set; }
        [DataMember]
        public string CO_SUBTIPOCOBER { get; set; }
        [DataMember]
        public string CO_ITEM { get; set; }
        [DataMember]
        public string CO_TIPOPROCINT { get; set; }
        [DataMember]
        public string NO_TIPOPROCNAME { get; set; }
        [DataMember]
        public int CO_GECODE { get; set; }
        [DataMember]
        public double NU_COPGFIJO { get; set; }
        [DataMember]
        public double NU_COPGVARI { get; set; }
        [DataMember]
        public int NU_FRECNUMB { get; set; }
        [DataMember]
        public int NU_DIASCANNT { get; set; }
        [DataMember]
        public string DE_OBSERPROC { get; set; }

    }

    [DataContract]
    public class ExcepcionCarenciaTable
    {
        [DataMember]
        public int NumeroAutorizacion { get; set; }
        [DataMember]
        public string CO_IAFASCODE { get; set; }
        [DataMember]
        public string CO_ASEGCODE { get; set; }
        [DataMember]
        public string CO_AUTOCODE { get; set; }
        [DataMember]
        public string CO_COBERCODE { get; set; }
        [DataMember]
        public string CO_SUBTIPOCOBER { get; set; }
        [DataMember]
        public string CO_ITEM { get; set; }
        [DataMember]
        public string CO_DXEXCEPCAREN { get; set; }
        [DataMember]
        public string NO_DXEXCEPCAREN { get; set; }
        [DataMember]
        public string DE_OBSEXCECEPCAREN { get; set; }

    }

    [DataContract]
    public class TiempoEsperaTable
    {
        [DataMember]
        public int NumeroAutorizacion { get; set; }
        [DataMember]
        public string CO_IAFASCODE { get; set; }
        [DataMember]
        public string CO_ASEGCODE { get; set; }
        [DataMember]
        public string CO_AUTOCODE { get; set; }
        [DataMember]
        public string CO_COBERCODE { get; set; }
        [DataMember]
        public string CO_SUBTIPOCOBER { get; set; }
        [DataMember]
        public string CO_ITEM { get; set; }
        [DataMember]
        public string CO_DXTIMEESP { get; set; }
        [DataMember]
        public string NO_DXTIMEESP { get; set; }
        [DataMember]
        public string DE_OBSTIMEESP { get; set; }

    }
    [DataContract]
    public class Estado
    {
        [DataMember]
        public int CO_ESTACODE { get; set; }
    }
    [DataContract]
    public class ListadoAtencionesRequest
    {
        [DataMember]
        public string tipoDocumento { get; set; }
        [DataMember]
        public string documento { get; set; }
        [DataMember]
        public string fechaInicio { get; set; }
        [DataMember]
        public string fechaFin { get; set; }
    }
    [DataContract]
    public class ListadoAtencionesResponse
    {
        [DataMember]
        public string ordenatencion { get; set; }
        [DataMember]
        public string peticion { get; set; }
        [DataMember]
        public string sede { get; set; }
        [DataMember]
        public string medico { get; set; }
        [DataMember]
        public string especialidad { get; set; }
        [DataMember]
        public string fechaCreacion { get; set; }
        [DataMember]
        public string situacion { get; set; }
    }
    [DataContract]
    public class OrdenAtencionRequest
    {
        [DataMember]
        public string peticion { get; set; }
    }
    [DataContract]
    public class OrdenAtencionResponse
    {
        [DataMember]
        public string codigo { get; set; }
        [DataMember]
        public string mensaje { get; set; }
        [DataMember]
        public string archivoBinario { get; set; }
    }
    [DataContract]
    public class ResultadoLaboratorio
    {
        [DataMember]
        public string documento { get; set; }
    }
    [DataContract]
    public class ResultadosLaboratorio
    {
        [DataMember]
        public string identificadorResultado { get; set; }
    }
    [DataContract]
    public class DataResponseLaboratorio
    {
        [DataMember]
        public string ordenAtencion { get; set; }
        [DataMember]
        public string peticion { get; set; }
        [DataMember]
        public string sede { get; set; }
        [DataMember]
        public string medico { get; set; }
        [DataMember]
        public string especialidad { get; set; }
        [DataMember]
        public string fechaCreacion { get; set; }
        [DataMember]
        public string situacion { get; set; }
        [DataMember]
        public string idClinica { get; set; }
        [DataMember]
        public string idEspecialidad { get; set; }

        [DataMember]
        public bool indRecetaMedica { get; set; }
        [DataMember]
        public string idCita { get; set; }


    }
    [DataContract]
    public class ResponseLaboratorio
    {
        [DataMember]
        public string codigo { get; set; }
        [DataMember]
        public string mensaje { get; set; }
        [DataMember]
        public List<DataResponseLaboratorio> data { get; set; }

    }
    [DataContract]
    public class ResponseLaboratorioDocument
    {
        [DataMember]
        public string codigo { get; set; }
        [DataMember]
        public string mensaje { get; set; }
        [DataMember]
        public string archivoBinario { get; set; }

    }
    [DataContract]
    public class CitaReprogramarPago
    {
        [DataMember]
        public bool pago { get; set; }
        [DataMember]
        public string tarjeta { get; set; }
        [DataMember]
        public string tipoTarjeta { get; set; }

    }
    [DataContract]
    public class PreDatosCitasSited
    {
        [DataMember]
        public string tipoDocumento { get; set; }
        [DataMember]
        public string numeroDocumento { get; set; }
        [DataMember]
        public string archivoBinario { get; set; }
        [DataMember]
        public string rucClinica { get; set; }
        [DataMember]
        public string codigoSunasa { get; set; }
        [DataMember]
        public string iafas { get; set; }
        [DataMember]
        public string codProducto { get; set; }
        [DataMember]
        public string codCobertura { get; set; }
        [DataMember]
        public string idPaciente { get; set; }
        [DataMember]
        public string sucursal { get; set; }
        [DataMember]
        public string idCitaSpring { get; set; }
        [DataMember]
        public string idAseguradora { get; set; }
        [DataMember]
        public string tipoSeguro { get; set; }
        [DataMember]
        public bool deseaBoleta { get; set; }
        [DataMember]
        public string monto { get; set; }
        [DataMember]
        public string firma { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public int idClinica { get; set; }
        [DataMember]
        public bool hoy { get; set; }
        //se agrero entidades 16042021
        [DataMember]

        public string ruc { get; set; }
        [DataMember]
        public string razonsocial { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidoPaterno { get; set; }
        [DataMember]
        public string apellidoMaterno { get; set; }
        [DataMember]
        public string numeroOperacion { get; set; }
        public string rucAseguradora { get; set; }
        public string tipoPaciente { get; set; }

        [DataMember]
        public string tipoDocumentoBoleta { get; set; }
        [DataMember]
        public string numeroDocumentoBoleta { get; set; }
        [DataMember]
        public string nombresBoleta { get; set; }
        [DataMember]
        public string apellidoPaternoBoleta { get; set; }
        [DataMember]
        public string apellidoMaternoBoleta { get; set; }
        [DataMember]
        public string direccionBoleta { get; set; }
        [DataMember]
        public string fechaNacimientoBoleta { get; set; }
        [DataMember]
        public string celularBoleta { get; set; }
        [DataMember]
        public string emailBoleta { get; set; }
        [DataMember]
        public string codigoParentesco { get; set; }
        [DataMember]
        public string codigoAfiliado { get; set; }
        [DataMember]
        public string tipoDocumentoContratante { get; set; }
        [DataMember]
        public string numeroDocumentoContratante { get; set; }
        [DataMember]
        public string coberturasCsv { get; set; }
    }
    [DataContract]
    public class RespuestaRoyal
    {
        [DataMember]
        public bool wsConsultaAsegNom { get; set; }
        [DataMember]
        public bool wsConsultaObservacion { get; set; }
        [DataMember]
        public bool wsConsultaDatosAdicionales { get; set; }
        [DataMember]
        public bool wsConsultaAsegCod { get; set; }
        [DataMember]
        public bool wsConsultaCondicionMedica { get; set; }
        [DataMember]
        public bool wsObtenerFoto { get; set; }
        [DataMember]
        public bool wsObtenerNumeroAutorizacion { get; set; }
        [DataMember]
        public bool wsConsultaProcedimientosEspeciales { get; set; }
        [DataMember]
        public bool vigente { get; set; }
        [DataMember]
        public bool idCitaConSeguro { get; set; }
        [DataMember]
        public bool insertTranc { get; set; }
        [DataMember]
        public bool insertOne { get; set; }
        [DataMember]
        public bool insertTwo { get; set; }
        [DataMember]
        public bool insertThree { get; set; }
        [DataMember]
        public bool insertFour { get; set; }
        [DataMember]
        public bool insertFive { get; set; }
        [DataMember]
        public bool insertSix { get; set; }
        [DataMember]
        public bool insertSeven { get; set; }
        [DataMember]
        public bool insertEigth { get; set; }
        [DataMember]
        public bool insertNine { get; set; }
        [DataMember]
        public bool insertTen { get; set; }

    }
    [DataContract]
    public class ConsultaObservacionRequest
    {
        [DataMember]
        public string SUNASA { get; set; }
        [DataMember]
        public string IAFAS { get; set; }
        [DataMember]
        public string RUC { get; set; }
        [DataMember]
        public string NombresAfiliado { get; set; }
        [DataMember]
        public string ApellidoPaternoAfiliado { get; set; }
        [DataMember]
        public string ApellidoMaternoAfiliado { get; set; }
        [DataMember]
        public string CodigoAfiliado { get; set; }
        [DataMember]
        public string CodTipoDocumentoAfiliado { get; set; }
        [DataMember]
        public string NumeroDocumentoAfiliado { get; set; }
        [DataMember]
        public string CodProducto { get; set; }
        [DataMember]
        public string DesProducto { get; set; }
        [DataMember]
        public string NumeroPlan { get; set; }
        [DataMember]
        public string CodTipoDocumentoContratante { get; set; }
        [DataMember]
        public string NumeroDocumentoContratante { get; set; }
        [DataMember]
        public string NombreContratante { get; set; }
        [DataMember]
        public string CodParentesco { get; set; }
        [DataMember]
        public string TipoCalificadorContratante { get; set; }
        [DataMember]
        public string CodEspecialidad { get; set; }
    }
    [DataContract]
    public class ConsultaObservacionResponse
    {

        [DataMember]
        public string IndObservacionAsegurado { get; set; }
        [DataMember]
        public string IndObservacionesEspeciales { get; set; }
        [DataMember]
        public string Observaciones { get; set; }

    }
    [DataContract]
    public class ConsultaDatosAdicionalesRequest
    {
        [DataMember]
        public string SUNASA { get; set; }
        [DataMember]
        public string IAFAS { get; set; }
        [DataMember]
        public string RUC { get; set; }
        [DataMember]
        public string NombresAfiliado { get; set; }
        [DataMember]
        public string ApellidoPaternoAfiliado { get; set; }
        [DataMember]
        public string ApellidoMaternoAfiliado { get; set; }
        [DataMember]
        public string CodigoAfiliado { get; set; }
        [DataMember]
        public string CodTipoDocumentoAfiliado { get; set; }
        [DataMember]
        public string NumeroDocumentoAfiliado { get; set; }
        [DataMember]
        public string CodProducto { get; set; }
        [DataMember]
        public string DesProducto { get; set; }
        [DataMember]
        public string NumeroPlan { get; set; }
        [DataMember]
        public string CodTipoDocumentoContratante { get; set; }
        [DataMember]
        public string NumeroDocumentoContratante { get; set; }
        [DataMember]
        public string NombreContratante { get; set; }
        [DataMember]
        public string CodParentesco { get; set; }
        [DataMember]
        public string TipoCalificadorContratante { get; set; }
        [DataMember]
        public string CodEspecialidad { get; set; }
    }
    [DataContract]
    public class ConsultaDatosAdicionalesResponse
    {
        [DataMember]
        public string Direccion1 { get; set; }
        [DataMember]
        public string Direccion2 { get; set; }
        [DataMember]
        public string Ubigeo { get; set; }
        [DataMember]
        public string Contacto { get; set; }
        [DataMember]
        public string Calificador { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string NumeroTelefono { get; set; }
    }
    [DataContract]
    public class ConsultaCondicionMedicaRequest
    {
        [DataMember]
        public string SUNASA { get; set; }
        [DataMember]
        public string IAFAS { get; set; }
        [DataMember]
        public string RUC { get; set; }
        [DataMember]
        public string NombresAfiliado { get; set; }
        [DataMember]
        public string ApellidoPaternoAfiliado { get; set; }
        [DataMember]
        public string ApellidoMaternoAfiliado { get; set; }
        [DataMember]
        public string CodigoAfiliado { get; set; }
        [DataMember]
        public string CodTipoDocumentoAfiliado { get; set; }
        [DataMember]
        public string NumeroDocumentoAfiliado { get; set; }
        [DataMember]
        public string CodProducto { get; set; }
        [DataMember]
        public string DesProducto { get; set; }
        [DataMember]
        public string NumeroPlan { get; set; }
        [DataMember]
        public string CodTipoDocumentoContratante { get; set; }
        [DataMember]
        public string NumeroDocumentoContratante { get; set; }
        [DataMember]
        public string NombreContratante { get; set; }
        [DataMember]
        public string CodParentesco { get; set; }
        [DataMember]
        public string TipoCalificadorContratante { get; set; }
        [DataMember]
        public string CodEspecialidad { get; set; }

    }

    [DataContract]
    public class Condicion
    {
        [DataMember]
        public string Codigo { get; set; }
        [DataMember]
        public string Diagnostico { get; set; }
        [DataMember]
        public string Observaciones { get; set; }
    }

    [DataContract]
    public class Preexistencia
    {
        [DataMember]
        public string CodRestriccion { get; set; }
        [DataMember]
        public string DesRestriccion { get; set; }
        [DataMember]
        public List<Condicion> Condicion { get; set; }
    }

    [DataContract]
    public class Exclusiones
    {
        [DataMember]
        public string CodRestriccion { get; set; }
        [DataMember]
        public string DesRestriccion { get; set; }
        [DataMember]
        public List<Condicion> Condicion { get; set; }
    }

    [DataContract]
    public class Carencia
    {
        [DataMember]
        public string CodRestriccion { get; set; }
        [DataMember]
        public string DesRestriccion { get; set; }
        [DataMember]
        public List<Condicion> Condicion { get; set; }
    }

    [DataContract]
    public class Antecedentes
    {
        [DataMember]
        public string CodRestriccion { get; set; }
        [DataMember]
        public string DesRestriccion { get; set; }
        [DataMember]
        public List<Condicion> Condicion { get; set; }
    }

    [DataContract]
    public class Enfermedad
    {
        [DataMember]
        public string CodRestriccion { get; set; }
        [DataMember]
        public string DesRestriccion { get; set; }
        [DataMember]
        public List<Condicion> Condicion { get; set; }
    }
    [DataContract]
    public class ConsultaCondicionMedicaResponse
    {
        [DataMember]
        public Preexistencia Preexistencia { get; set; }
        [DataMember]
        public Exclusiones Exclusiones { get; set; }
        [DataMember]
        public Carencia Carencia { get; set; }
        [DataMember]
        public Antecedentes Antecedentes { get; set; }
        [DataMember]
        public Enfermedad Enfermedad { get; set; }
    }
    [DataContract]
    public class ObtenerFotoRequest
    {
        [DataMember]
        public string Iafas { get; set; }
        [DataMember]
        public string CodigoAfiliado { get; set; }
        [DataMember]
        public string CodFechaActualizacionFoto { get; set; }

    }
    [DataContract]
    public class ObtenerFotoResponse
    {
        [DataMember]
        public string Foto { get; set; }

    }
    [DataContract]
    public class ObtenerNumeroAutorizacionRequest
    {
        [DataMember]
        public string ApellidoMaternoAfiliado { get; set; }
        [DataMember]
        public string ApellidoPaternoAfiliado { get; set; }
        [DataMember]
        public string BeneficioMaximoInicial { get; set; }
        [DataMember]
        public string CodigoAfiliado { get; set; }
        [DataMember]
        public string CodigoTitular { get; set; }
        [DataMember]
        public string CodCalificacionServicio { get; set; }
        [DataMember]
        public string CodEstado { get; set; }
        [DataMember]
        public string CodEspecialidad { get; set; }
        [DataMember]
        public string CodMoneda { get; set; }
        [DataMember]
        public string CodCopagoFijo { get; set; }
        [DataMember]
        public string CodCopagoVariable { get; set; }
        [DataMember]
        public string CodParentesco { get; set; }
        [DataMember]
        public string CodProducto { get; set; }
        [DataMember]
        public string NumeroDocumentoContratante { get; set; }
        [DataMember]
        public string CodSubTipoCobertura { get; set; }
        [DataMember]
        public string CodTipoCobertura { get; set; }
        [DataMember]
        public string CodTipoAfiliacion { get; set; }
        [DataMember]
        public string DesProducto { get; set; }
        [DataMember]
        public string CodEstadoMarital { get; set; }
        [DataMember]
        public string CodFechaFinCarencia { get; set; }
        [DataMember]
        public string CodFechaAfiliacion { get; set; }
        [DataMember]
        public string CodFechaInicioVigencia { get; set; }
        [DataMember]
        public string CodFechaNacimiento { get; set; }
        [DataMember]
        public string CodGenero { get; set; }
        [DataMember]
        public string SUNASA { get; set; }
        [DataMember]
        public string IAFAS { get; set; }
        [DataMember]
        public string CondicionesEspeciales { get; set; }
        [DataMember]
        public string ApellidoMaternoTitular { get; set; }
        [DataMember]
        public string NombreContratante { get; set; }
        [DataMember]
        public string ApellidoPaternoTitular { get; set; }
        [DataMember]
        public string NombresAfiliado { get; set; }
        [DataMember]
        public string NombresTitular { get; set; }
        [DataMember]
        public string NumeroCertificado { get; set; }
        [DataMember]
        public string NumeroContrato { get; set; }
        [DataMember]
        public string NumeroDocumentoAfiliado { get; set; }
        [DataMember]
        public string NumeroDocumentoTitular { get; set; }
        [DataMember]
        public string NumeroPlan { get; set; }
        [DataMember]
        public string NumeroPoliza { get; set; }
        [DataMember]
        public string RUC { get; set; }
        [DataMember]
        public string CodTipoDocumentoContratante { get; set; }
        [DataMember]
        public string CodTipoDocumentoAfiliado { get; set; }
        [DataMember]
        public string CodTipoDocumentoTitular { get; set; }
        [DataMember]
        public string CodTipoPlan { get; set; }
        [DataMember]
        public string CodIndicadorRestriccion { get; set; }
        [DataMember]
        public string CodFechaActualizacionFoto { get; set; }
        [DataMember]
        public string CodTipoMoneda { get; set; }

    }
    [DataContract]
    public class ObtenerNumeroAutorizacionResponse
    {
        [DataMember]
        public string NumeroAutorizacion { get; set; }
        [DataMember]
        public string Documento { get; set; }
        [DataMember]
        public string NumeroAutorizacionExistente { get; set; }

    }

    [DataContract]
    public class ConsultaProcedimientosEspecialesRequest
    {
        [DataMember]
        public string SUNASA { get; set; }
        [DataMember]
        public string IAFAS { get; set; }
        [DataMember]
        public string RUC { get; set; }
        [DataMember]
        public string NombresAfiliado { get; set; }
        [DataMember]
        public string ApellidoPaternoAfiliado { get; set; }
        [DataMember]
        public string ApellidoMaternoAfiliado { get; set; }
        [DataMember]
        public string CodigoAfiliado { get; set; }
        [DataMember]
        public string CodTipoDocumentoAfiliado { get; set; }
        [DataMember]
        public string NumeroDocumentoAfiliado { get; set; }
        [DataMember]
        public string CodProducto { get; set; }
        [DataMember]
        public string DesProducto { get; set; }
        [DataMember]
        public string NumeroPlan { get; set; }
        [DataMember]
        public string CodTipoDocumentoContratante { get; set; }
        [DataMember]
        public string NumeroDocumentoContratante { get; set; }
        [DataMember]
        public string NombreContratante { get; set; }
        [DataMember]
        public string CodParentesco { get; set; }
        [DataMember]
        public string NumeroCobertura { get; set; }
        [DataMember]
        public string BeneficioMaximoInicial { get; set; }
        [DataMember]
        public string CodigoTipoCobertura { get; set; }
        [DataMember]
        public string CodigoSubTipoCobertura { get; set; }
        [DataMember]
        public string CodEspecialidad { get; set; }

    }

    [DataContract]
    public class Detalle
    {
        [DataMember]
        public string Codigo { get; set; }
        [DataMember]
        public string Procedimiento { get; set; }
        [DataMember]
        public string Genero { get; set; }
        [DataMember]
        public string CodCopagoFijo { get; set; }
        [DataMember]
        public string DesCopagoFijo { get; set; }
        [DataMember]
        public string CodCopagoVariable { get; set; }
        [DataMember]
        public string DesCopagoVariable { get; set; }
        [DataMember]
        public string Frecuencia { get; set; }
        [DataMember]
        public string Tiempo { get; set; }
        [DataMember]
        public string Observaciones { get; set; }
        [DataMember]
        public string GrupoDiagnostico { get; set; }
        [DataMember]
        public string FechaFinVigencia { get; set; }

    }

    [DataContract]
    public class Procedimiento
    {
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string CodIndicadorProcedimiento { get; set; }
        [DataMember]
        public string DesIndicadorProcedimiento { get; set; }
        [DataMember]
        public List<Detalle> Detalle { get; set; }
    }

    [DataContract]
    public class TiempoEspera
    {
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public List<Detalle> Detalle { get; set; }
    }

    [DataContract]
    public class ExcepcionCarencia
    {
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public List<Detalle> Detalle { get; set; }

    }

    [DataContract]
    public class ConsultaProcedimientosEspecialesResponse
    {
        [DataMember]
        public Procedimiento Procedimiento { get; set; }
        [DataMember]
        public TiempoEspera TiempoEspera { get; set; }
        [DataMember]
        public ExcepcionCarencia ExcepcionCarencia { get; set; }
    }

    [DataContract]
    public class DataResponseTipoCoberturaEspecialidad
    {
        [DataMember]
        public string codigo { get; set; }
        [DataMember]
        public string descripcion { get; set; }

    }
    [DataContract]
    public class ResponseTipoCoberturaEspecialidad
    {
        [DataMember]
        public string codigo { get; set; }
        [DataMember]
        public string mensaje { get; set; }
        [DataMember]
        public List<DataResponseTipoCoberturaEspecialidad> data { get; set; }

    }

    [DataContract]
    public class DatosPagoIzipayBE
    {
        [DataMember]
        public string monto { get; set; }
        [DataMember]
        public string user { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public string merchant { get; set; }
        [DataMember]
        public string purchaseNumber { get; set; }
        [DataMember]
        public string especialidad { get; set; }
        [DataMember]
        public string nombreMedico { get; set; }
        [DataMember]
        public string nombrePaciente { get; set; }
        [DataMember]
        public string fechaAtencion { get; set; }
        [DataMember]
        public string horaInicio { get; set; }
        [DataMember]
        public string motivoCobro { get; set; }
        [DataMember]
        public string tokenEmail { get; set; }
        [DataMember]
        public string clinica { get; set; }
        [DataMember]
        public string aseguradora { get; set; }
        [DataMember]
        public string correo { get; set; }

        [DataMember]
        public string nombreCompletoPaciente { get; set; }

        [DataMember]
        public string apellidoCompletoPaciente { get; set; }

        [DataMember]
        public string direccionPaciente { get; set; }
        [DataMember]
        public string numeroDireccionPaciente { get; set; }
        [DataMember]
        public string telefono { get; set; }
        public string idCita { get; set; } //IDCita de la tabla Cita
        //[DataMember]
        public string urlToken { get; set; }
        //[DataMember]
        public string requestSource { get; set; }
        [DataMember]
        public string merchantCode { get; set; }
        [DataMember]
        public string publicKey { get; set; }
        [DataMember]
        public string merchantBuyerId { get; set; }
        [DataMember]
        public string action { get; set; }
        [DataMember]
        public string orderNumber { get; set; }
        [DataMember]
        public string payMethod { get; set; }
        [DataMember]
        public string street { get; set; }
        [DataMember]
        public string environment { get; set; }
        [DataMember]
        public string urlIPN { get; set; }
    }

    [DataContract]
    public class TokenIzipayBE
    {
        [DataMember]
        public string token { get; set; }
        [DataMember]
        public string purchaseNumber { get; set; }
        [DataMember]
        public string merchantCode { get; set; }
        [DataMember]
        public string merchantBuyerId { get; set; }
        [DataMember]
        public string action { get; set; }
        [DataMember]
        public string orderNumber { get; set; }
        [DataMember]
        public string payMethod { get; set; }
        [DataMember]
        public string street { get; set; }
        public string urlToken { get; set; }
        public string requestSource { get; set; }
        [DataMember]
        public string publicKey { get; set; }
        [DataMember]
        public string environment { get; set; }
        [DataMember]
        public string urlIPN { get; set; }
    }

    [DataContract]
    public class IzipaySDKBE
    {
        [DataMember]
        public string usuarioIzipaySDK { get; set; }
        [DataMember]
        public string passwordIzipaySDK { get; set; }
        [DataMember]
        public string urlBaseIzipaySDK { get; set; }


    }

    public class beOrigenResponse
    {
        public bool IndValido { get; set; }
        public string Mensaje { get; set; }
        public beOrigenResponse()
        {
            Mensaje = "";
        }
    }

    [DataContract]
    public class ServicioInfo
    {
        [DataMember]
        public int servicioInfoId { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string abreviatura { get; set; }
        [DataMember]
        public string equipamiento { get; set; }

    }

    public class ValidarAccionBE
    {
        public bool indValido { get; set; }
        public string mensaje { get; set; }

    }

    [DataContract]
    public class VideollamadaBE
    {
        public string AccountSid { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string paciente { get; set; }

        [DataMember]
        public string token { get; set; }
        [DataMember]
        public string room_name { get; set; }
        [DataMember]
        public string medico { get; set; }
        [DataMember]
        public string especialidad { get; set; }
        [DataMember]
        public string fechaAtencion { get; set; }
        [DataMember]
        public string horaInicio { get; set; }
    }

    [DataContract]
    public class VideollamadaMedicoBE
    {
        public string AccountSid { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string medico { get; set; }

        [DataMember]
        public string token { get; set; }
        [DataMember]
        public string paciente { get; set; }
        [DataMember]
        public string fechaAtencion { get; set; }
        [DataMember]
        public string horaInicio { get; set; }
        [DataMember]
        public string idCitaVirtual { get; set; }
    }


    [DataContract]
    public class NovedadesBE
    {
        //Cita
        public DateTime fechaOrdenamiento { get; set; }
        [DataMember]
        public string idCita { get; set; }
        [DataMember]
        public string nombrePaciente { get; set; }
        [DataMember]
        public string nombrePaciente2 { get; set; }
        [DataMember]
        public string cmp { get; set; }
        [DataMember]
        public string nombreMedico { get; set; }
        [DataMember]
        public string fechaAtencion { get; set; }
        [DataMember]
        public string horaInicio { get; set; }
        [DataMember]
        public string horaFin { get; set; }
        [DataMember]
        public string idEspecialidad { get; set; }
        [DataMember]
        public string especialidad { get; set; }
        [DataMember]
        public string idClinica { get; set; }
        [DataMember]
        public string clinica { get; set; }
        //[DataMember]
        public string idMedicoFavorito { get; set; }
        [DataMember]
        public bool esCitaVirtual { get; set; }
        [DataMember]
        public string fuePagado { get; set; }
        [DataMember]
        public string descripcionPago { get; set; }
        [DataMember]
        public string tipoPago { get; set; }
        //[DataMember]
        public int tiempoPrevioCita { get; set; }
        //[DataMember]
        public bool anular { get; set; }
        //[DataMember]
        public bool anularPago { get; set; }
        [DataMember]
        public bool mostrarBotonesPago { get; set; }
        //[DataMember]
        public bool mostrarBotonesPag { get; set; }
        [DataMember]
        public bool mostrarFilaEspera { get; set; }
        [DataMember]
        public string consultorio { get; set; }
        [DataMember]
        public string codigoTipoPaciente { get; set; }
        [DataMember]
        public string tipoPaciente { get; set; }
        [DataMember]
        public string codigoTipoAtencionHorario { get; set; }
        [DataMember]
        public string tipoAtencionHorario { get; set; }
        [DataMember]
        public string codigoTipoAtencionCita { get; set; }
        [DataMember]
        public string tipoAtencionCita { get; set; }
        [DataMember]
        public int tipoDocumento { get; set; }
        [DataMember]
        public string numeroDocumento { get; set; }
        [DataMember]
        public int idHorario { get; set; }
        [DataMember]
        public string cantidadPersonasDelante { get; set; }
        [DataMember]
        public string subEspecialidad { get; set; }
        [DataMember]
        public int idEstado { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string tiempoPrevioColaVirtual { get; set; }
        [DataMember]
        public string tiempoPrevioIngresoVirtual { get; set; }
        [DataMember]
        public string tiempoPosteriorIngresoVirtual { get; set; }
        [DataMember]
        public string abreviaturaMedico { get; set; }
        public string sexoMedico { get; set; }
        [DataMember]
        public string fotoMedico { get; set; }
        [DataMember]
        public string metodoPago { get; set; }
        [DataMember]
        public bool indicadorHospitalizacion { get; set; }
        [DataMember]
        public string codAtencion { get; set; } //Identificador de registro de hospitalización
        [DataMember]
        public string idAmbulatorio { get; set; } //idAmbulatorio de hospitalización
        [DataMember]
        public string ultimoPaso { get; set; }
        [DataMember]
        public bool esAdicional { get; set; }
        [DataMember]
        public string textoAdicional { get; set; }
        [DataMember]
        public string codigoAtencion { get; set; }
        [DataMember]
        public bool tieneReceta { get; set; }
        [DataMember]
        public string iconoReceta { get; set; }
        [DataMember]
        public bool tieneHojaRuta { get; set; }
        [DataMember]
        public string iconoHojaRuta { get; set; }
        [DataMember]
        public string codigoPaciente { get; set; }

        //Procedimientos
        
        [DataMember]
        public string idCitaProcedimiento { get; set; }
        [DataMember]
        public string procedimiento { get; set; }
        [DataMember]
        public string idServicioHorario { get; set; }
        [DataMember]
        public string idServicio { get; set; }
        [DataMember]
        public string servicio { get; set; }
        [DataMember]
        public string tipoTerapia { get; set; }
        [DataMember]
        public string numeroSesion { get; set; }
    }

    public class RequestHospitalizacionBE
    {
        //Hospitalización
        public string idAmbulatorio { get; set; }
        public string ApiKey { get; set; }
    }

    public class ResponseHospitalizacionBE
    {
        //Hospitalización
        public string codAtencion { get; set; }
        public string fechaHora { get; set; }
        public string idAmbulatorio { get; set; }
        public string grupoFamiliar { get; set; }
        public string codTipoDocumento { get; set; }
        public string desTipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string fechaNacimiento { get; set; }
        public string nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string sexo { get; set; }
        public string codMedico { get; set; }
        public string nomMedico { get; set; }
        public string codEspecialidad { get; set; }
        public string desEspecialidad { get; set; }
        public string nivelAtencion { get; set; }
        public string codEstado { get; set; }
        public string desEstado { get; set; }
        public string ultimoPaso { get; set; }
        public string codFamiliar { get; set; }
        public string desFamiliar { get; set; }
    }

    public class TurnoPacienteBE
    {
        public bool indGestorColasWS { get; set; }
        public string idCita { get; set; }
    }

    [DataContract]
    public class ListarOpcionesPagoBE
    {
        [DataMember]
        public DatosPagoBEV2 datosCita { get; set; }
        [DataMember]
        public List<OpcionPagoBE> opcionesPago { get; set; }
        public ListarOpcionesPagoBE()
        {
            opcionesPago = new List<OpcionPagoBE>();
        }
    }

    [DataContract]
    public class OpcionPagoBE
    {
        [DataMember]
        public string tipoTarifa { get; set; }
        [DataMember]
        public string RUCSeguro { get; set; }
        [DataMember]
        public string IAFAS { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string codigoProducto { get; set; }
        [DataMember]
        public string nombreProducto { get; set; }
        [DataMember]
        public string codigoCobertura { get; set; }
        [DataMember]
        public string nombreCobertura { get; set; }
        [DataMember]
        public string monto { get; set; }
        [DataMember]
        public string origenMonto { get; set; }
        [DataMember]
        public string fechaPago { get; set; }
        [DataMember]
        public string parentesco { get; set; }
        [DataMember]
        public string codigoParentesco { get; set; }
        [DataMember]
        public string codigoAfiliado { get; set; }
        [DataMember]
        public string tipoDocumentoContratante { get; set; }
        [DataMember]
        public string numeroDocumentoContratante { get; set; }
    }

    public class DatosPagoBEV2
    {
        //[DataMember]
        public string purchaseNumber { get; set; }
        [DataMember]
        public string especialidad { get; set; }
        [DataMember]
        public string nombreMedico { get; set; }
        [DataMember]
        public string nombrePaciente { get; set; }
        [DataMember]
        public string fechaAtencion { get; set; }
        [DataMember]
        public string horaInicio { get; set; }
        [DataMember]
        public string tokenEmail { get; set; }
        [DataMember]
        public string clinica { get; set; }
        [DataMember]
        public string consultorio { get; set; }
        [DataMember]
        public string tiempoAtencion { get; set; }
    }

    [DataContract]
    public class VideollamadaPendienteBE
    {
        [DataMember]
        public string[] room_name { get; set; }
    }

    [DataContract]
    public class VideollamadaTwilioSidBE
    {
        [DataMember]
        public string room_sid { get; set; }
        [DataMember]
        public int duracion { get; set; } //En segundos
    }

    public class RegistroPagoBE {
        public bool success { get; set; }
        public bool indEnviarCorreo { get; set; }
    }

    [DataContract]
    public class CitaChatBotBE
    {
        [DataMember]
        public string clinica { get; set; }
        [DataMember]
        public string especialidad { get; set; }
        [DataMember]
        public string medico { get; set; }
        public string generoMedico { get; set; }
        [DataMember]
        public bool esAdicional { get; set; }
        public bool esCitaVirtual { get; set; }
        public DateTime fechaCita { get; set; }
        public string horaCita { get; set; }
        public string horaInicioHorario { get; set; }
        public string horaFinHorario { get; set; }
        [DataMember]
        public string fechaHoraPresentacion { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidoPaterno { get; set; }
        [DataMember]
        public string apellidoMaterno { get; set; }
    }

    public class ObjetoQRBE
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

    [DataContract]
    public class IndicadorFlyerBE
    {
        [DataMember]
        public bool mostrarFlyer { get; set; }
        [DataMember]
        public string imagen { get; set; }
    }

    //[DataContract]
    //public class PeriodoBE
    //{
    //    [DataMember]
    //    public string periodo { get; set; }
    //}

    [DataContract]
    public class CitaImagenBE
    {
        [DataMember]
        public string codigoAtencion { get; set; }
        [DataMember]
        public string fechaHoraAtencion { get; set; }
        [DataMember]
        public string nombrePaciente { get; set; }
        [DataMember]
        public string numeroDocumentoPaciente { get; set; }
        [DataMember]
        public string nombreExamen { get; set; }
        [DataMember]
        public bool esInformeResultado { get; set; }
        [DataMember]
        public bool esImagenResultado { get; set; }
        [DataMember]
        public string idImagenResultado { get; set; }
        [DataMember]
        public string idInformeResultado { get; set; }
    }

    [DataContract]
    public class ParametroCorreoCitaPresencialBE
    {
        [DataMember]
        public string IDCita { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string NombrePaciente { get; set; }
        [DataMember]
        public string ApellidoPaternoPaciente { get; set; }
        [DataMember]
        public string CelularPaciente { get; set; }
        [DataMember]
        public string NombreMedico { get; set; }
        [DataMember]
        public string FechaAtencion { get; set; }
        [DataMember]
        public string HoraInicio { get; set; }
        [DataMember]
        public string Especialidad { get; set; }
        [DataMember]
        public string Clinica { get; set; }
        [DataMember]
        public string IDClinica { get; set; }
        [DataMember]
        public string ApellidoMaternoPaciente { get; set; }
        [DataMember]
        public string Consultorio { get; set; }
        [DataMember]
        public string TiempoAtencion { get; set; }
        [DataMember]
        public string NumeroDocumentoPaciente { get; set; }
        [DataMember]
        public string LinkPago { get; set; }
        [DataMember]
        public string LinkCalendario { get; set; }
        [DataMember]
        public string IndicadorBotonPagar { get; set; }
        [DataMember]
        public string EsPrePago { get; set; }
        [DataMember]
        public string EsAdicional { get; set; }
        [DataMember]
        public string HoraInicioHorario { get; set; }
        [DataMember]
        public string HoraFinHorario { get; set; }
        [DataMember]
        public string Ubicacion { get; set; }
        [DataMember]
        public string DireccionCorta { get; set; }
        [DataMember]
        public string Referencia { get; set; }
        [DataMember]
        public string URLBanner { get; set; }
    }

    public class ReprogramarCitaBE
    {
        [DataMember]
        public string idCita { get; set; }
        [DataMember]
        public bool procesarPago { get; set; }
        [DataMember]
        public string tarjeta { get; set; }
        [DataMember]
        public string tipoTarjeta { get; set; }
        [DataMember]
        public bool enviarCorreo { get; set; }
    }

    [DataContract]
    public class DatosCitaVirtualBE
    {
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string NombrePaciente { get; set; }
        [DataMember]
        public string ApellidoPaternoPaciente { get; set; }
        [DataMember]
        public string ApellidoMaternoPaciente { get; set; }
        [DataMember]
        public string FechaNacimiento { get; set; }
        [DataMember]
        public string EdadPaciente { get; set; }
        [DataMember]
        public string Sexo { get; set; }
        [DataMember]
        public string CelularPaciente { get; set; }
        [DataMember]
        public string NombreMedico { get; set; }
        [DataMember]
        public string FechaAtencion { get; set; }
        [DataMember]
        public string HoraInicio { get; set; }
        [DataMember]
        public string Especialidad { get; set; }
        [DataMember]
        public string Clinica { get; set; }
        [DataMember]
        public string EmailMedico { get; set; }
        [DataMember]
        public string Seguro { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public string EmailPago { get; set; }
        [DataMember]
        public string IDClinica { get; set; }
    }
    [DataContract]
    public class DatosCitaBE
    {
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string TipoDocumento { get; set; }
        [DataMember]
        public string NumeroDocumento { get; set; }
        [DataMember]
        public string NombrePaciente { get; set; }
        [DataMember]
        public string ApellidoPaternoPaciente { get; set; }
        [DataMember]
        public string ApellidoMaternoPaciente { get; set; }
        [DataMember]
        public string FechaNacimiento { get; set; }
        [DataMember]
        public string EdadPaciente { get; set; }
        [DataMember]
        public string Sexo { get; set; }
        [DataMember]
        public string CelularPaciente { get; set; }
        [DataMember]
        public string NombreMedico { get; set; }
        [DataMember]
        public string FechaAtencion { get; set; }
        [DataMember]
        public string FechaAtencionAux { get; set; }
        [DataMember]
        public string HoraInicio { get; set; }
        [DataMember]
        public string Especialidad { get; set; }
        [DataMember]
        public string Clinica { get; set; }
        [DataMember]
        public string Seguro { get; set; }
        [DataMember]
        public string CodigoSunasa { get; set; }
        [DataMember]
        public string RUCSpring { get; set; }
        [DataMember]
        public string EmailPago { get; set; }
        [DataMember]
        public string IDClinica { get; set; }
        [DataMember]
        public string EsAdicional { get; set; }
        [DataMember]
        public string HoraInicioHorario { get; set; }
        [DataMember]
        public string HoraFinHorario { get; set; }
    }

    [DataContract]
    public class IzipaySimpleBE
    {
        [DataMember]
        public string purchaseNumber { get; set; }
        [DataMember]
        public string monto { get; set; }
        public string urlToken { get; set; }
        public string requestSource { get; set; }
        [DataMember]
        public string merchantCode { get; set; }
        [DataMember]
        public string publicKey { get; set; }
        [DataMember]
        public string merchantBuyerId { get; set; }
        [DataMember]
        public string action { get; set; }
        [DataMember]
        public string orderNumber { get; set; }
        [DataMember]
        public string token { get; set; }
        [DataMember]
        public string payMethod { get; set; }
        [DataMember]
        public string street { get; set; }
        [DataMember]
        public string environment { get; set; }
        [DataMember]
        public string urlIPN { get; set; }
    }

    [DataContract]
    public class UsuarioTarjetaBE
    {
        [DataMember]
        public string idUsuarioTarjeta { get; set; }
        [DataMember]
        public string numeroTarjeta { get; set; }
        [DataMember]
        public string tokenTarjeta { get; set; }
        [DataMember]
        public string tipoTarjeta { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidos { get; set; }
        [DataMember]
        public string correo { get; set; }
        [DataMember]
        public string merchantBuyerId { get; set; }
        [DataMember]
        public string urlTarjeta { get; set; }
        [DataMember]
        public string urlTipoTarjeta { get; set; }
    }
    [DataContract]
    public class IzipayTemporalBE
    {
        [DataMember]
        public string token { get; set; }
    }
}



