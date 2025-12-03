using Entities.Models;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IMachoteMensajeDA
    {
        IEnumerable<MachoteMensaje> ObtenerMachotes();
        MachoteMensaje ObtenerMachotePorId(int numId);
        bool InsertarMachote(MachoteMensaje machote);
        bool ActualizarMachote(MachoteMensaje machote);
        bool EliminarMachote(int numId);
    }
}
