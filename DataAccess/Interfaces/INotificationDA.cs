using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface INotificationDA
    {
        Task<MachoteMensaje> ObtenerMachoteAsync(int numMachote, string codigoIdioma);
        Task<string> ObtenerDireccionEnvioAsync(int numMachote);
        Task<List<CitaProcedimiento>> ObtenerProcedimientosCitaAsync(int numCita);
        Task<ArticuloDetalle> ObtenerArticuloDetalleAsync(string codArticulo);
        //Task<List<string>> EnviarCorreoAsync(ParamEnvioCorreo correo, List<ParamEnvioCorreoAdjunto> adjuntos, bool enviarAppointment);
    }
}
