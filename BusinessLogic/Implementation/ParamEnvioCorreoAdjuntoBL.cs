using System.Collections.Generic;
using BussinessLogic.Interfaces;
using DataAccess.Interfaces;
using Entities.Models;

namespace BussinessLogic.Implementation
{
    public class ParamEnvioCorreoAdjuntoBL : IParamEnvioCorreoAdjuntoBL
    {
        private readonly IParamEnvioCorreoAdjuntoDA _dataAccess;

        public ParamEnvioCorreoAdjuntoBL(IParamEnvioCorreoAdjuntoDA dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<ParamEnvioCorreoAdjunto> ObtenerAdjuntos() => _dataAccess.ObtenerAdjuntos();

        public ParamEnvioCorreoAdjunto ObtenerAdjuntoPorNombre(string nombre) => _dataAccess.ObtenerAdjuntoPorNombre(nombre);

        public bool InsertarAdjunto(ParamEnvioCorreoAdjunto adjunto) => _dataAccess.InsertarAdjunto(adjunto);

        public bool EliminarAdjunto(string nombre) => _dataAccess.EliminarAdjunto(nombre);
    }
}
