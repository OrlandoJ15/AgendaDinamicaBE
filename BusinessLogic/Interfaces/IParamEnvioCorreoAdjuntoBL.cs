using Entities.Models;
using System.Collections.Generic;

namespace BussinessLogic.Interfaces
{
    public interface IParamEnvioCorreoAdjuntoBL
    {
        IEnumerable<ParamEnvioCorreoAdjunto> ObtenerAdjuntos();
        ParamEnvioCorreoAdjunto ObtenerAdjuntoPorNombre(string nombre);
        bool InsertarAdjunto(ParamEnvioCorreoAdjunto adjunto);
        bool EliminarAdjunto(string nombre);
    }
}
