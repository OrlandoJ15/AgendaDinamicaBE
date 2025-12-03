namespace Entities.Models
{
    public class Agenda
    {
        public int NumAgenda { get; set; }
        public int NumTipoAgenda { get; set; }
        public int IndEstado { get; set; }
        public string NomAgenda { get; set; } = string.Empty;
        public string? IdEdificio { get; set; }
        public string? NumPiso { get; set; }
        public string? CodProf { get; set; }
        public string? DesUbicacion { get; set; }
        public string? DesObservacion { get; set; }
        public string UsrRegistra { get; set; } = string.Empty;
        public DateTime FecRegistra { get; set; }
        public string? UsrActualiza { get; set; }
        public DateTime? FecActualiza { get; set; }
    }
}
