using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class AppointmentSlot
    {
        public int NumAgenda { get; set; }
        public DateTime FecHoraInicial { get; set; }
        public DateTime FecHoraFinal { get; set; }

        public AppointmentSlot()
        {
            NumAgenda = 0;
            FecHoraInicial = DateTime.MinValue;
            FecHoraFinal = DateTime.MinValue;
        }
    }
}
