using Entities.Models;
using System.Collections.Generic;

namespace BussinessLogic.Interfaces
{
    public interface IMachoteMensajeBL
    {
        IEnumerable<MachoteMensaje> ObtenerMachotes();
        MachoteMensaje ObtenerMachotePorId(int numId);
        bool InsertarMachote(MachoteMensaje machote);
        bool ActualizarMachote(MachoteMensaje machote);
        bool EliminarMachote(int numId);
    }
}
