using NLog;
using System;
using System.Threading.Tasks;

namespace CommonMethods
{
    public class AsyncExceptions
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void LogError(Exception ex)
        {
            var methodInfo = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = methodInfo?.ToString() ?? "Método no disponible";

            _logger.Error("SE HA PRODUCIDO UN ERROR. Detalle: {0} // InnerException: {1}. Método: {2}",
                ex.Message,
                ex.InnerException?.Message ?? "No Inner Exception",
                methodName);
        }

        public async Task<T> EjecutarProcConEntidadAsync<T>(Func<Task<T>> funcion)
        {
            try
            {
                return await funcion();
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        public async Task<bool> EjecutarProcSinEntidadAsync(Func<Task> accion)
        {
            try
            {
                await accion();
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }
    }
}
