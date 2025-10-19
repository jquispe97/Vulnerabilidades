using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CSF.CITASWEB.WS.BE
{
    [DataContract]
    public class MedicoFavoritoBE
    {
        [DataMember]
        public string idMedicoFavorito { get; set; }
        [DataMember]
        public MedicoBE medico { get; set; }
    }

    [DataContract]
    public class MedicoBE
    {
        [DataMember]
        public string cmp { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string cargo { get; set; }
        [DataMember]
        public string mostrarCV { get; set; }
        [DataMember]
        public string foto { get; set; }
        [DataMember]
        public string tituloMedico { get; set; }
        [DataMember]
        public string premiosHonores { get; set; }
        [DataMember]
        public string pertenenciaSociedad { get; set; }
        [DataMember]
        public string investigacionPublicaciones { get; set; }
        [DataMember]
        public List<ClinicaBE> centrosAtencion { get; set; }
        [DataMember]
        public List<EspecialidadBE> especialidad { get; set; }
        [DataMember]
        public string RNE { get; set; }
        [DataMember]
        public string idiomas { get; set; }
        [DataMember]
        public string informacionAdicional { get; set; }
    }

    [DataContract]
    public class MedicoHorarioBE
    {
        [DataMember]
        public string cmp { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string especialidad { get; set; }
        [DataMember]
        public string idClinica { get; set; }
        [DataMember]
        public string idEspecialidad { get; set; }
        [DataMember]
        public string centroAtencion { get; set; }
        [DataMember]
        public string idMedicoFavorito { get; set; }
        [DataMember]
        public string cvVisible { get; set; }
        [DataMember]
        public string soloLlamadas { get; set; }
        public List<HorariosBrutoBE> horariosEnteros { get; set; }
        [DataMember]
        public List<MedicoHorarioDetalleBE> horarios { get; set; }
        [DataMember]
        public string foto { get; set; }
        [DataMember]
        public DateTime fechaFinal { get; set; }
        [DataMember]
        public string fechaProxima { get; set; }
        [DataMember]
        public bool prioritario { get; set; }
        [DataMember]
        public string idSubEspecialidad { get; set; }
        [DataMember]
        public string subEspecialidad { get; set; }
        [DataMember]
        public string tipoPaciente { get; set; }
        [DataMember]
        public string abreviatura { get; set; }
        [DataMember]
        public string codigoTipoPaciente { get; set; }
        [DataMember]
        public string unidadEspecializacion { get; set; }
        public string sexo { get; set; }
        [DataMember]
        public string campoClinico { get; set; }
        public int prioridad { get; set; }
    }
    public class HorarioCitaMasProxima
    {
        [DataMember]
        public List<MedicoHorarioBE> horarios { get; set; }
        [DataMember]
        public string fecha { get; set; }
        [DataMember]
        public bool disponibilidad { get; set; }
        [DataMember]
        public ClinicaObj clinica { get; set; }
        [DataMember]
        public EspecialidadObj especialidad { get; set; }
    }
    public class ClinicaObj
    {
        [DataMember]
        public string idClinica { get; set; }
        [DataMember]
        public string nombre { get; set; }

    }
    public class EspecialidadObj
    {
        [DataMember]
        public string idEspecialidad { get; set; }
        [DataMember]
        public string especialidad { get; set; }
    }
    public class HorariosBrutoBE
    {
        public int idDetalleHorarios { get; set; }
        public string idHorarioDetalle { get; set; }
        public TimeSpan horaInicio { get; set; }
        public int tiempoAtencion { get; set; }
        public int cantidadTurnos { get; set; }
        public int idClinica { get; set; }
    }

    [DataContract]
    public class MedicoHorarioDetalleBE
    {
        public int idDetalleHorarios { get; set; }
        [DataMember]
        public string idHorarioDetalle { get; set; }
        public TimeSpan horaTime { get; set; }
        [DataMember]
        public string hora { get; set; }
        [DataMember]
        public string turno { get; set; }
        [DataMember]
        public int idClinica { get; set; }
    }

    [DataContract]
    public class MedicoHorarioSimpleWebBE
    {
        [DataMember]
        public string cmp { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string idEspecialidad { get; set; }
        [DataMember]
        public string especialidad { get; set; }
        [DataMember]
        public string idCentroAtencion { get; set; }
        [DataMember]
        public string centroAtencion { get; set; }
        [DataMember]
        public string idMedicoFavorito { get; set; }
        [DataMember]
        public string cvVisible { get; set; }
        [DataMember]
        public string soloLlamadas { get; set; }
        [DataMember]
        public string foto { get; set; }
        [DataMember]
        public string idSubEspecialidad { get; set; }
        [DataMember]
        public string subEspecialidad { get; set; }
        [DataMember]
        public string tipoPaciente { get; set; }
        [DataMember]
        public string abreviatura { get; set; }
        [DataMember]
        public string codigoTipoPaciente { get; set; }
        [DataMember]
        public string unidadEspecializacion { get; set; }
        public string sexo { get; set; }
        [DataMember]
        public string campoClinico { get; set; }
        [DataMember]
        public string mensajePersonalizado { get; set; }
        [DataMember]
        public string telefonoSecretaria { get; set; }
        public int prioridad { get; set; }
    }

    [DataContract]
    public class MedicoHorarioDisponibleBE
    {
        public DateTime fechaDate { get; set; }
        [DataMember]
        public string fecha { get; set; }
        [DataMember]
        public string idHorarioDetalle { get; set; }
        [DataMember]
        public string numeroTurno { get; set; }
        [DataMember]
        public string horaInicio { get; set; }
        [DataMember]
        public string disponible { get; set; }
    }

    [DataContract]
    public class DiasHorarioDisponibleBE
    {
        public DateTime fechaDate { get; set; }
        [DataMember]
        public string fecha { get; set; }
        [DataMember]
        public string turnosDisponibles { get; set; }
    }

    public class HorarioTiempoPreCitaBE
    {
        public int tiempoMinimoEspera { get; set; }
        public int idClinica { get; set; }
    }
    //[DataContract]
    //public class CMPMedicoHorarioBE
    //{
    //    [DataMember]
    //    public string CMP { get; set; }
    //    [DataMember]
    //    public string apellidoPaterno { get; set; }
    //    [DataMember]
    //    public string apellidoMaterno { get; set; }
    //    [DataMember]
    //    public string nombres { get; set; }
    //    [DataMember]
    //    public List<MedicoHorarioProximaFechaBE> fechas { get; set; }
    //}
    [DataContract]
    public class MedicoHorarioProximaFechaBE
    {
        public DateTime fechaDate { get; set; }
        [DataMember]
        public string fecha { get; set; }
        [DataMember]
        public int dia { get; set; }
        //public string CMP { get; set; }
        [DataMember]
        public List<MedicoHorarioProximaHorarioBE> horarios { get; set; }
    }
    [DataContract]
    public class MedicoHorarioProximaHorarioBE
    {
        public DateTime fechaDate { get; set; }
        public string fecha { get; set; }
        [DataMember]
        public int idHorario { get; set; }
        //public string CMP { get; set; }
        [DataMember]
        public List<MedicoHorarioProximaTurnoBE> turnos { get; set; }
    }
    [DataContract]
    public class MedicoHorarioProximaTurnoBE
    {
        public DateTime fechaDate { get; set; }
        public string fecha { get; set; }
        public int idHorario { get; set; }
        [DataMember]
        public string horaInicio { get; set; }
        [DataMember]
        public int numeroTurno { get; set; }
    }

    public class MantMedicoBE
    {
        public string CMP { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Cargo { get; set; }
        public bool MuestraCV { get; set; }
        public string Foto { get; set; }
        public string TituloMedico { get; set; }
        public string Premios { get; set; }
        public string PertenenciaSociedad { get; set; }
        public string Investigaciones { get; set; }
        public int? RNE { get; set; }
        public string Idiomas { get; set; }
        public string InformacionAdicional { get; set; }
    }

    [DataContract]
    public class MedicoHorarioDisponibleBEV2
    {
        public DateTime fechaDate { get; set; }
        [DataMember]
        public string fecha { get; set; }
        [DataMember]
        public List<MHDHorarioBE> horarios { get; set; }
        public MedicoHorarioDisponibleBEV2()
        {
            horarios = new List<MHDHorarioBE>();
        }
    }
    [DataContract]
    public class MHDHorarioBE
    {
        public DateTime fechaDate { get; set; }
        public string fecha { get; set; }
        [DataMember]
        public string idHorarioDetalle { get; set; }
        [DataMember]
        public string numeroTurno { get; set; }
        [DataMember]
        public string horaInicio { get; set; }
        [DataMember]
        public string disponible { get; set; }
    }
    [DataContract]
    public class InfoMedicoBE
    {
        [DataMember]
        public string abreviatura { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string RNE { get; set; }
        [DataMember]
        public string foto { get; set; }
        //[DataMember]
        //public string idClinica { get; set; }
        //[DataMember]
        //public string clinica { get; set; }
        [DataMember]
        public string idEspecialidad { get; set; }
        [DataMember]
        public string especialidad { get; set; }
        [DataMember]
        public string idSubEspecialidad { get; set; }
        [DataMember]
        public string subEspecialidad { get; set; }
        [DataMember]
        public string areasInteres { get; set; }
        [DataMember]
        public string unidadEspecializacion { get; set; }
        [DataMember]
        public string codigoTipoPaciente { get; set; }
        [DataMember]
        public string tipoPaciente { get; set; }
        [DataMember]
        public string soloLlamadas { get; set; }
        [DataMember]
        public string idiomas { get; set; }
        [DataMember]
        public string informacionAdicional { get; set; }
        [DataMember]
        public string tituloMedico { get; set; }
        [DataMember]
        public string premiosHonores { get; set; }
        [DataMember]
        public string pertenenciaSociedad { get; set; }
        [DataMember]
        public string investigacionPublicaciones { get; set; }
        [DataMember]
        public string cargo { get; set; }
        [DataMember]
        public List<ClinicaSimpleBE> clinicas { get; set; }
        public string sexo { get; set; }
        public string mostrarCV { get; set; }
        [DataMember]
        public string mensajePersonalizado { get; set; }
        [DataMember]
        public string telefonoSecretaria { get; set; }
        public InfoMedicoBE ()
        {
            clinicas = new List<ClinicaSimpleBE>();
        }
    }

    [DataContract]
    public class DirectorioMedicoBE
    {
        [DataMember]
        public string cmp { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string idEspecialidad { get; set; }
        [DataMember]
        public string especialidad { get; set; }
        [DataMember]
        public string idMedicoFavorito { get; set; }
        [DataMember]
        public string cvVisible { get; set; }
        [DataMember]
        public string soloLlamadas { get; set; }
        [DataMember]
        public string foto { get; set; }
        [DataMember]
        public string idSubEspecialidad { get; set; }
        [DataMember]
        public string subEspecialidad { get; set; }
        [DataMember]
        public string tipoPaciente { get; set; }
        [DataMember]
        public string abreviatura { get; set; }
        [DataMember]
        public string codigoTipoPaciente { get; set; }
        [DataMember]
        public string unidadEspecializacion { get; set; }
        public string sexo { get; set; }
        [DataMember]
        public string campoClinico { get; set; }
        [DataMember]
        public string mensajePersonalizado { get; set; }
        [DataMember]
        public string telefonoSecretaria { get; set; }
        public int prioridad { get; set; }
    }
}
