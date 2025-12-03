using System;

namespace Entities.Models
{
    public class CitaBitacoraInsertar
    {
        public int NUM_CITA { get; set; }
        public DateTime FEC_HORAINICIAL { get; set; }
        public DateTime FEC_HORAFINAL { get; set; }
        public string IND_OPERACION { get; set; }
        public string USR_REGISTRA { get; set; }
        public string DES_DATOANTES { get; set; }
        public string DES_DATODESPUES { get; set; }
        public int NUM_AGENDA { get; set; }

        public CitaBitacoraInsertar()
        {
            NUM_CITA = 0;
            FEC_HORAINICIAL = new DateTime();
            FEC_HORAFINAL = new DateTime();
            IND_OPERACION = string.Empty;
            USR_REGISTRA = string.Empty;
            DES_DATOANTES = string.Empty;
            DES_DATODESPUES = string.Empty;
            NUM_AGENDA = 0;
        }
    }
}
