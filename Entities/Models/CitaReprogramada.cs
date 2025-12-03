namespace Entities.Models
{
    public class CitaReprogramada
    {
        public int NumCitaReprogramada { get; set; }    // NUM_CITAREPROGRAMADA
        public int NumAgenda { get; set; }              // NUM_AGENDA
        public int? NumCitaAnterior { get; set; }      // NUM_CITAANTERIOR (nullable)
        public DateTime FecCita { get; set; }          // FEC_CITA
        public int? NumNuevaCita { get; set; }         // NUM_NUEVACITA (nullable)
        public DateTime FecNuevaCita { get; set; }     // FEC_NUEVACITA
        public string UsrRePrograma { get; set; }      // USR_REPROGRAMA
        public DateTime? FecRePrograma { get; set; }   // FEC_REPROGRAMA (nullable)
    }
}
