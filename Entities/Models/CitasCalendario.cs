using System;

namespace Entities.Models
{
    public class CitasCalendario
    {
        public int NUM_CITA { get; set; }
        public int NUM_AGENDA { get; set; }
        public DateTime FEC_HORAINICIAL { get; set; }
        public DateTime FEC_HORAFINAL { get; set; }
        public string NOM_PACIENTE { get; set; }
        public int IND_ASISTENCIA { get; set; }
        public int IND_APLICAPACIENTE { get; set; }
        public string COLOR_CITA { get; set; }
        public string DES_OBSERVACION { get; set; }
        public int IND_CONFIRMACION { get; set; }

        public CitasCalendario()
        {
            NUM_CITA = 0;
            NUM_AGENDA = 0;
            FEC_HORAINICIAL = DateTime.MinValue;
            FEC_HORAFINAL = DateTime.MinValue;
            NOM_PACIENTE = string.Empty;
            IND_ASISTENCIA = 0;
            IND_APLICAPACIENTE = 0;
            COLOR_CITA = string.Empty;
            DES_OBSERVACION = string.Empty;
            IND_CONFIRMACION = 0;
        }
    }
}
