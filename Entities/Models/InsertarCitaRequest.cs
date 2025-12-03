// Entities/Models/InsertarCitaRequest.cs
namespace Entities.Models
{
    public class InsertarCitaRequest
    {
        public Cita Cita { get; set; }                     // Tu entidad Cita existente
        public List<CitaProcedimiento> Servicios { get; set; } = new();
        public string BitacoraDatosDespues { get; set; } = string.Empty;
    }
}
