using System;

namespace Entities.Models
{
    public class Cita
    {
        public int NUM_CITA { get; set; }
        public int NUM_AGENDA { get; set; }
        public DateTime FEC_HORAINICIAL { get; set; }
        public DateTime FEC_HORAFINAL { get; set; }
        public int NUM_TIPOCITA { get; set; }
        public int IND_PACIENTERECARGO { get; set; }
        public int NUM_AGENDACOMPARTIDA { get; set; }
        public int NUM_EXPEDIENTE { get; set; }
        public int NUM_ORDSERV { get; set; }
        public string NOM_PACIENTE { get; set; } = string.Empty;
        public DateTime FEC_REGISTRA { get; set; }
        public string USR_REGISTRA { get; set; } = string.Empty;
        public int IND_ASISTENCIA { get; set; }
        public DateTime FEC_ACTUALIZA { get; set; }
        public string USR_ACTUALIZA { get; set; } = string.Empty;
        public string DES_RECARGO { get; set; } = string.Empty;
        public string DES_OBSERVACION { get; set; } = string.Empty;
        public int IND_REFERENCIAMEDICA { get; set; }
        public string COD_PROFREFIERE { get; set; } = string.Empty;
        public string DES_REFERENCIA { get; set; } = string.Empty;
        public int NUM_CITA_MADRE { get; set; }
        public int IND_CONFIRMACION { get; set; }
        public DateTime FEC_CONFIRMACION { get; set; }
        public string DES_CONFIRMACION { get; set; } = string.Empty;
        public string USR_CONFIRMACION { get; set; } = string.Empty;
        public DateTime HORA_PRESENTARSE { get; set; }
        public string DES_TIPOPROC { get; set; } = string.Empty;
        public int NUM_TIPOPROC { get; set; }
        public int IND_APLICASEGURO { get; set; }
        public int IND_COPAGO { get; set; }
        public int IND_DEDUCIBLE { get; set; }
        public int IND_COBERTURATOTAL { get; set; }
        public string OBSERVACIONES_SEGURO { get; set; } = string.Empty;
        public string COD_INSTITUC { get; set; } = string.Empty;
        public int IND_CITASECUNDARIA { get; set; }
    }
}
