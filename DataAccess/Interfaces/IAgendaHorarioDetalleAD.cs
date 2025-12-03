using Entities.Models;

namespace DataAccess.Interfaces
{
    public interface IAgendaHorarioDetalleAD
    {
        // =======================================================
        // Recupera todos los registros de Agenda Horario Detalle
        // =======================================================
        List<AgendaHorarioDetalle> RecAgendaHorarioDetalles();

        // =======================================================
        // Recupera un registro de Agenda Horario Detalle por su ID
        // =======================================================
        AgendaHorarioDetalle? RecAgendaHorarioDetalleXId(int idDetalle);

        // =======================================================
        // Inserta un nuevo registro de Agenda Horario Detalle
        // =======================================================
        bool InsAgendaHorarioDetalle(AgendaHorarioDetalle detalle);

        // =======================================================
        // Modifica un registro de Agenda Horario Detalle existente
        // =======================================================
        bool ModAgendaHorarioDetalle(AgendaHorarioDetalle detalle);

        // =======================================================
        // Elimina un registro de Agenda Horario Detalle por su ID
        // =======================================================
        bool DelAgendaHorarioDetalle(int idDetalle);
    }
}
