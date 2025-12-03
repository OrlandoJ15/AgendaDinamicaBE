using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ICitaBL
    {
        Task<List<AppointmentSlot>> ConsultarKeyAsync(int numAgenda, DateTime fecInicial, DateTime fecFinal);
        Task<List<AgendaHorario>> ConsultarPorFechaAsync(int numAgenda, DateTime fechaInicial, DateTime fechaFinal);
        Task<List<AgendaHorarioDetalleReceso>> ConsultarRecesosAsync(decimal idAgendaHorarioDetalle);
        Task<Cita?> ConsultarKeyAsync(int numCita);
        Task<List<CitasCalendario>> ConsultarExisteEnRangoAsync(int numAgenda, DateTime fecInicial, DateTime fecFinal, int numTipoAgenda, int numIntervalo);
        Task<List<string>> InsertarCitaPacienteAsync(Cita cita, List<CitaProcedimiento> servicios, string bitacoraDatosDespues);
        Task<string> NotificaCitaConvenioAsync(string codInstituc, int numAgenda);
    }
}
