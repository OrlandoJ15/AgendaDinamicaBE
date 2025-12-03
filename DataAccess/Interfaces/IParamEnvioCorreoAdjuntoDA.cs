using Entities.Models;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IParamEnvioCorreoAdjuntoDA
    {
        IEnumerable<ParamEnvioCorreoAdjunto> ObtenerAdjuntos();
        ParamEnvioCorreoAdjunto ObtenerAdjuntoPorNombre(string nombre);
        bool InsertarAdjunto(ParamEnvioCorreoAdjunto adjunto);
        bool EliminarAdjunto(string nombre);
    }
}
