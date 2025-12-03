namespace Entities.Models
{
    public class Profesional
    {
        public string CodProf { get; set; }                      // COD_PROF
        public string NomProf { get; set; }                      // NOM_PROF
        public string Estado { get; set; }                       // ESTADO
        public string IndSic { get; set; }                       // IND_SIC
        public string Tipo { get; set; }                         // TIPO
        public string CodSociedad { get; set; }                  // COD_SOCIEDAD
        public string CodEspec { get; set; }                     // COD_ESPEC
        public string CodGrupo { get; set; }                     // COD_GRUPO
        public string Observ { get; set; }                       // OBSERV
        public string IndMed { get; set; }                       // IND_MED
        public string NoCedula { get; set; }                     // NO_CEDULA
        public string NoTelefono { get; set; }                   // NO_TELEFONO
        public string NoCuentaMaster { get; set; }               // NO_CUENTA_MASTER
        public string IndAprob { get; set; }                     // IND_APROB
        public string NoProve { get; set; }                      // NO_PROVE
        public string ClaveMedica { get; set; }                  // CLAVE_MEDICA
        public string GrupoCxc { get; set; }                     // GRUPO_CXC
        public string TipoCxc { get; set; }                      // TIPO_CXC
        public string Horario { get; set; }                      // HORARIO
        public string CodFormaPago { get; set; }                 // COD_FORMAPAGO
        public decimal? PorTarjeta { get; set; } = 7m;           // POR_TARJETA
        public string Banco { get; set; }                        // BANCO
        public decimal PorFijo { get; set; } = 0m;               // POR_FIJO
        public string NoClienteCxc { get; set; }                 // NO_CLIENTE_CXC
        public string TipoSociedad { get; set; } = "S";          // TIPO_SOCIEDAD
        public int NumFactura { get; set; } = 0;                 // NUM_FACTURA
        public string GenerarFacturas { get; set; } = "N";       // GENERAR_FACTURAS
        public string IndCxp { get; set; } = "N";                // IND_CXP
        public string IndFacturaProf { get; set; } = "N";        // IND_FACTURA_PROF
        public string IndMontoFijo { get; set; } = "N";          // IND_MONTO_FIJO
        public string IndUrgencias { get; set; } = "N";          // IND_URGENCIAS
        public string UsrActualiza { get; set; }                 // USR_ACTUALIZA
        public DateTime? FecActualiza { get; set; }              // FEC_ACTUALIZA
        public string IndPagarVacuna { get; set; } = "N";        // IND_PAGARVACUNA
        public string IndMedicoIns { get; set; } = "N";          // IND_MEDICO_INS
        public string CorreoElectronico { get; set; }            // CORREO_ELECTRONICO
        public DateTime? FecNacimiento { get; set; }             // FEC_NACIMIENTO
        public string IndExcDescuentoClientePrefe { get; set; } = "N"; // IND_EXC_DESCUENTO_CLIENTEPREFE
        public string IndCodSalida { get; set; }                 // IND_COD_SALIDA
        public int? CanAsistentes { get; set; }                  // CAN_ASISTENTES
        public string IndRequiRecetaElectronica { get; set; } = "N";   // IND_REQUI_RECETAELECTRONICA
        public string IndAplicaRevCredenciales { get; set; }     // IND_APLICAREVCREDENCIALES
        public string DesAreaTrabajo { get; set; }               // DES_AREATRABAJO
        public bool IndAceptaSeguros { get; set; } = false;      // IND_ACEPTASEGUROS (NUMBER(1))
        public bool IndAceptaConvenios { get; set; } = false;    // IND_ACEPTACONVENIOS (NUMBER(1))
        public string DesSeguros { get; set; }                   // DES_SEGUROS
        public int? NumAgendaDirtel { get; set; }                // NUM_AGENDA_DIRTEL
        public DateTime FecActualizacionClave { get; set; } = DateTime.Now; // FEC_ACTUALIZACIONCLAVE
        public bool IndReportaPatologia { get; set; } = false;   // IND_REPORTAPATOLOGIA
        public string ClaveConsultarAgenda { get; set; }         // CLAVE_CONSULTARAGENDA
        public string IndProfesionalPlanilla { get; set; } = "N"; // IND_PROFESIONAL_PLANILLA
        public string CodigoColegioProfesional { get; set; }     // CODIDO_COLEGIO_PROFESIONAL
        public bool IndSecretario { get; set; } = false;         // IND_SECRETARIO
        public byte[] Firma { get; set; }                        // FIRMA (BLOB)
        public string EnviarEncuestaPacientes { get; set; } = "S"; // ENVIAR_ENCUESTA_PACIENTES
        public bool IndRealizoCursos { get; set; } = false;      // IND_REALIZOCURSOS
        public bool IndCorreoJustificacion { get; set; } = false; // IND_CORREOJUSTIFICACION
        public DateTime? FecCorreoJustificacion { get; set; }    // FEC_CORREOJUSTIFICACION
        public DateTime? FecNotificacion { get; set; }           // FEC_NOTIFICACION
        public string UsrNotificacion { get; set; }              // USR_NOTIFICACION
        public string IndCenso { get; set; }                     // IND_CENSO
        public DateTime? FecReplicado { get; set; }              // FEC_REPLICADO
        public decimal? RatingActual { get; set; } = 0m;         // RATING_ACTUAL
        public int? NumTotalRespuestas { get; set; }             // NUM_TOTALRESPUESTAS
        public string ObsExclusivoDmComentarios { get; set; }    // OBS_EXCLUSIVODM_COMENTARIOS
    }
}
