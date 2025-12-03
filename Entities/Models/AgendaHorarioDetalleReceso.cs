using System;

namespace Entities.Models
{
    public class AgendaHorarioDetalleReceso
    {
        public decimal ID_AGENDAHORARIO_DETALLERECESO { get; set; }
        public decimal ID_AGENDAHORARIO_DETALLE { get; set; }
        public string DESCRIPCION { get; set; }
        public DateTime HORA_INICIO { get; set; }
        public DateTime HORA_FINAL { get; set; }
    }
}
