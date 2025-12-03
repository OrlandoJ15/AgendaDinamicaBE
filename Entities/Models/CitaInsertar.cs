using System;

namespace Entities.Models
{
    public class CitaInsertar
    {
        #region Propiedades
        public int NUM_AGENDA { get; set; }
        public DateTime FEC_HORAINICIAL { get; set; }
        public DateTime FEC_HORAFINAL { get; set; }
        #endregion

        #region Constructor
        public CitaInsertar()
        {
            NUM_AGENDA = 0;
            FEC_HORAINICIAL = new DateTime();
            FEC_HORAFINAL = new DateTime();
        }
        #endregion
    }
}
