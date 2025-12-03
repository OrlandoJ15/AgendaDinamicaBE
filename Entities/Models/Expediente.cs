namespace Entities.Models
{
    public class Expediente
    {
        public int NumExpediente { get; set; }                  // NUM_EXPEDIENTE
        public string NumCarnet { get; set; }                   // NUM_CARNET
        public string PrimerAp { get; set; }                    // PRIMER_AP
        public string SegundoAp { get; set; }                   // SEGUNDO_AP
        public string PrimerNom { get; set; }                   // PRIMER_NOM
        public string SegundoNom { get; set; }                  // SEGUNDO_NOM
        public string Sexo { get; set; }                        // SEXO
        public string CodTipDoc { get; set; }                   // COD_TIPDOC
        public string NumId { get; set; }                       // NUM_ID
        public string CodClase { get; set; }                    // COD_CLASE
        public DateTime? FecNacimiento { get; set; }             // FEC_NACIMIENTO
        public int? TelHab { get; set; }                        // TEL_HAB
        public int? TelOfic { get; set; }                       // TEL_OFIC
        public int? TelOpc { get; set; }                        // TEL_OPC
        public string Apartado { get; set; }                    // APARTADO
        public string DireccionHab { get; set; }                // DIRECCION_HAB
        public string CodProvincia { get; set; }                // COD_PROVINCIA
        public string CodCanton { get; set; }                   // COD_CANTON
        public string CodDistrito { get; set; }                 // COD_DISTRITO
        public string CodBarrio { get; set; }                   // COD_BARRIO
        public DateTime? FecExpediente { get; set; }             // FEC_EXPEDIENTE
        public string EstadoExp { get; set; }                   // ESTADO_EXP
        public string Medico { get; set; }                      // MEDICO
        public string CodMedico { get; set; }                   // COD_MEDICO
        public string Observ { get; set; }                      // OBSERV
        public string NomPaciente { get; set; }                 // NOM_PACIENTE
        public string Usuario { get; set; }                     // USUARIO
        public int? NumVisitas { get; set; }                    // NUM_VISITAS
        public string CorreoElectronico { get; set; }           // CORREO_ELECTRONICO
        public string IndDonador { get; set; }                  // IND_DONADOR
        public int? NumDonador { get; set; }                    // NUM_DONADOR
        public string UserActualiza { get; set; }               // USER_ACTUALIZA
        public DateTime? FecActualiza { get; set; }             // FEC_ACTUALIZA
        public string DscCampoActualizado { get; set; }         // DSC_CAMPO_ACTUALIZADO
        public string CodGrupo { get; set; }                    // COD_GRUPO
        public string CodSigno { get; set; }                    // COD_SIGNO
        public string IndFallecido { get; set; } = "N";         // IND_FALLECIDO
        public string CodTipoTricare { get; set; }              // COD_TIPO_TRICARE
        public decimal? SaldoDeducTricare { get; set; }         // SALDO_DEDUC_TRICARE
        public decimal? MonConsumidoTricare { get; set; }       // MON_CONSUMIDO_TRICARE
        public string CodIdioma { get; set; }                   // COD_IDIOMA
        public string CodEtnia { get; set; }                    // COD_ETNIA
        public string CodReligion { get; set; }                 // COD_RELIGION
        public string NumIdAnterior { get; set; }               // NUM_ID_ANTERIOR
        public string CodProfesion { get; set; }                // COD_PROFESION
        public string IndConsEnvioInfo { get; set; }            // IND_CONS_ENVIOINFO
        public string CodPaisNac { get; set; }                  // COD_PAIS_NAC
        public string CodCategoriaACSocial { get; set; }        // COD_CATEGORIA_ACSOCIAL
        public int? TelCelular { get; set; }                    // TEL_CELULAR
        public string IndOrigen { get; set; } = "SIGH";         // IND_ORIGEN
        public bool IndVivePais { get; set; }                   // IND_VIVEPAIS (NUMBER(1))
        public bool IndCronico { get; set; } = false;           // IND_CRONICO (NUMBER(1))
        public string CodPaisVive { get; set; }                 // COD_PAISVIVE
        public string CodEstadoPaisVive { get; set; }           // COD_ESTADO_PAISVIVE
        public bool IndPacienteConvenio { get; set; } = false;  // IND_PACIENTE_CONVENIO (INTEGER)
        public string CodConvenio { get; set; }                 // COD_CONVENIO
        public string CorreoElectronicoFE { get; set; }         // CORREO_ELECTRONICO_FE
        public string IndExpedienteTemp { get; set; }           // IND_EXPEDIENTE_TEMP
        public string IndExtranjero { get; set; } = "N";        // IND_EXTRANJERO
        public string CodHotel { get; set; }                    // COD_HOTEL
        public bool? IndAfiliadoMiVida { get; set; }            // IND_AFILIADO_MIVIDA (NUMBER(1))
        public string CodEscolaridad { get; set; }              // COD_ESCOLARIDAD
        public string ConocidoComo { get; set; }                // CONOCIDO_COMO
        public DateTime? FechaDefuncion { get; set; }           // FECHA_DEFUNCION
        public string EstadoCivil { get; set; }                 // ESTADO_CIVIL
    }
}
