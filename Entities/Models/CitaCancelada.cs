namespace Entities.Models
{
    public class CitaCancelada
    {
        public int NumCitaCancelada { get; set; }        // NUM_CITACANCELADA
        public int NumAgenda { get; set; }               // NUM_AGENDA
        public DateTime FecCita { get; set; }            // FEC_CITA
        public DateTime FecRegistra { get; set; }        // FEC_REGISTRA
        public string UsrRegistra { get; set; }          // USR_REGISTRA
        public bool IndReprogramada { get; set; } = false; // IND_REPROGRAMADA (0 = false, 1 = true)
        public bool IndSeguimiento { get; set; } = false;  // IND_SEGUIMIENTO (0 = false, 1 = true)
        public int? NumExpediente { get; set; }          // NUM_EXPEDIENTE (nullable)
        public string NomPaciente { get; set; }          // NOM_PACIENTE
        public int NumMotivo { get; set; }               // NUM_MOTIVO
        public string DesMotivo { get; set; }            // DES_MOTIVO
        public DateTime? FecFinalCita { get; set; }      // FEC_FINALCITA (nullable)
        public int? NumCitaAnterior { get; set; }       // NUM_CITAANTERIOR (nullable)
        public int? NumNuevaAgenda { get; set; }        // NUM_NUEVAAGENDA (nullable)
        public int? NumNuevaCita { get; set; }          // NUM_NUEVACITA (nullable)
        public DateTime? FecNuevaCita { get; set; }      // FEC_NUEVACITA (nullable)
        public string UsrRePrograma { get; set; }       // USR_REPROGRAMA
        public DateTime? FecRePrograma { get; set; }     // FEC_REPROGRAMA (nullable)
        public string DesSeguimiento { get; set; }      // DES_SEGUIMIENTO
        public int? NumTipoCita { get; set; }           // NUM_TIPOCITA (nullable)
    }
}
