using Entities.Models;

namespace DataAccess.Interfaces
{
    public interface IAgendaDA
    {
        // Recupera todas las agendas
        List<Agenda> RecAgenda();

        // Recupera una agenda por su ID
        Agenda? RecAgendaXId(int numAgenda);

        // Inserta una nueva agenda
        bool InsAgenda(Agenda agenda);

        // Modifica una agenda existente
        bool ModAgenda(Agenda agenda);

        // Elimina una agenda por su ID
        bool DelAgenda(int numAgenda);
    }
}

