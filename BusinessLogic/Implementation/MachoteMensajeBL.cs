using System.Collections.Generic;
using BussinessLogic.Interfaces;
using DataAccess.Interfaces;
using Entities.Models;

namespace BussinessLogic.Implementation
{
    public class MachoteMensajeBL : IMachoteMensajeBL
    {
        private readonly IMachoteMensajeDA _dataAccess;

        public MachoteMensajeBL(IMachoteMensajeDA dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<MachoteMensaje> ObtenerMachotes() => _dataAccess.ObtenerMachotes();

        public MachoteMensaje ObtenerMachotePorId(int numId) => _dataAccess.ObtenerMachotePorId(numId);

        public bool InsertarMachote(MachoteMensaje machote) => _dataAccess.InsertarMachote(machote);

        public bool ActualizarMachote(MachoteMensaje machote) => _dataAccess.ActualizarMachote(machote);

        public bool EliminarMachote(int numId) => _dataAccess.EliminarMachote(numId);
    }
}
