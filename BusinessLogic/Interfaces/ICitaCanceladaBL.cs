using Entities.Models;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    public interface ICitaCanceladaBL
    {
        // Recupera todas las citas canceladas
        List<CitaCancelada> RecCitasCanceladas();

        // Recupera una cita cancelada por su ID
        CitaCancelada? RecCitaCanceladaXId(int numCitaCancelada);

        // Inserta una nueva cita cancelada
        bool InsCitaCancelada(CitaCancelada cita);

        // Modifica una cita cancelada existente
        bool ModCitaCancelada(CitaCancelada cita);

        // Elimina una cita cancelada por su ID
        bool DelCitaCancelada(int numCitaCancelada);
    }
}
