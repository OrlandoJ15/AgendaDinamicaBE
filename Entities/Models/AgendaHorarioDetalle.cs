namespace Entities.Models
{
    public class AgendaHorarioDetalle
    {
        public int IdAgendaHorarioDetalle { get; set; }   // ID_AGENDAHORARIO_DETALLE
        public int? IdAgendaHorario { get; set; }        // ID_AGENDAHORARIO (nullable si no es obligatorio)
        public int NumDia { get; set; }                  // NUM_DIA
        public int NumHoraInicio { get; set; }           // NUM_HORAINICIO
        public int NumHoraFinal { get; set; }            // NUM_HORAFINAL
        public int NumIntervaloCita { get; set; }        // NUM_INTERVALOCITA
        public bool IndLibre { get; set; }               // IND_LIBRE (0 = false, 1 = true)
        public int NumCitas { get; set; } = 1;          // NUM_CITAS, valor por defecto 1
        public string UsrRegistra { get; set; }          // USR_REGISTRA
        public DateTime FecRegistra { get; set; }        // FEC_REGISTRA
        public string UsrActualiza { get; set; }         // USR_ACTUALIZA
        public DateTime? FecActualiza { get; set; }      // FEC_ACTUALIZA (nullable)
    }
}
