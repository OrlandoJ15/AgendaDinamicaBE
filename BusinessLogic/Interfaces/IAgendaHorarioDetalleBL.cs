using Entities.Models;
using System.Collections.Generic;

namespace BussinessLogic.Interfaces
{
    public interface IAgendaHorarioDetalleBL
    {
        // =======================================================
        // Recupera todos los registros de AgendaHorarioDetalle
        // =======================================================
        List<AgendaHorarioDetalle> RecAgendaHorarioDetalles();

        // =======================================================
        // Recupera un registro de AgendaHorarioDetalle por su ID
        // =======================================================
        AgendaHorarioDetalle? RecAgendaHorarioDetalleXId(int idDetalle);

        // =======================================================
        // Inserta un nuevo registro de AgendaHorarioDetalle
        // =======================================================
        bool InsAgendaHorarioDetalle(AgendaHorarioDetalle detalle);

        // =======================================================
        // Modifica un registro de AgendaHorarioDetalle existente
        // =======================================================
        bool ModAgendaHorarioDetalle(AgendaHorarioDetalle detalle);

        // =======================================================
        // Elimina un registro de AgendaHorarioDetalle por su ID
        // =======================================================
        bool DelAgendaHorarioDetalle(int idDetalle);
    }
}
