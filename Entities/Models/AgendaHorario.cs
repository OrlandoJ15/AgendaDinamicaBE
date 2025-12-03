using System;

namespace Entities.Models
{
    public class AgendaHorario
    {
        public decimal ID_AGENDAHORARIO_DETALLE { get; set; }
        public int NUM_AGENDA { get; set; }
        public int NUM_DIA { get; set; }
        public int NUM_HORAINICIO { get; set; }
        public int NUM_HORAFINAL { get; set; }
        public int NUM_INTERVALOCITA { get; set; }
        public string DESCRIPCION { get; set; }
        public int IND_LIBRE { get; set; }
        public string USR_REGISTRA { get; set; }
        public DateTime FEC_REGISTRA { get; set; }
        public string USR_ACTUALIZA { get; set; }
        public DateTime FEC_ACTUALIZA { get; set; }
        public int NUM_CITAS { get; set; }
        public DateTime NUM_FORMATOHORAINICIO { get; set; }
        public DateTime NUM_FORMATOHORAFINAL { get; set; }
        public string NOM_DIA { get; set; }

        public AgendaHorario()
        {
            DESCRIPCION = string.Empty;
            USR_REGISTRA = string.Empty;
            USR_ACTUALIZA = string.Empty;
            NOM_DIA = string.Empty;
        }
    }
}
