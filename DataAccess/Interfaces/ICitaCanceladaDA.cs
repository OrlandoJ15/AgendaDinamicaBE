using Entities.Models;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface ICitaCanceladaDA
    {
        // =======================================================
        // Recupera todas las citas canceladas
        // =======================================================
        List<CitaCancelada> RecCitasCanceladas();

        // =======================================================
        // Recupera una cita cancelada por su ID
        // =======================================================
        CitaCancelada? RecCitaCanceladaXId(int numCitaCancelada);

        // =======================================================
        // Inserta una nueva cita cancelada
        // =======================================================
        bool InsCitaCancelada(CitaCancelada citaCancelada);

        // =======================================================
        // Modifica una cita cancelada existente
        // =======================================================
        bool ModCitaCancelada(CitaCancelada citaCancelada);

        // =======================================================
        // Elimina una cita cancelada por su ID
        // =======================================================
        bool DelCitaCancelada(int numCitaCancelada);
    }
}
