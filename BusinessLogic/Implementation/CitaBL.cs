using BusinessLogic.Interfaces;
using CommonMethods;
using DataAccess.Interfaces;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Implementation
{
    public class CitaBL : ICitaBL
    {
        private readonly ICitaDA _citaDA;
        private readonly AsyncExceptions _exceptions;

        public CitaBL(ICitaDA citaDA, AsyncExceptions exceptions)
        {
            _citaDA = citaDA;
            _exceptions = exceptions;
        }

        public async Task<List<AppointmentSlot>> ConsultarKeyAsync(int numAgenda, DateTime fecInicial, DateTime fecFinal)
        {
            // Ejecutamos y dejamos que Exceptions registre errores si ocurre alguno
            return await _exceptions.EjecutarProcConEntidadAsync(async () =>
            {
                var result = await _citaDA.ConsultarKeyAsync(numAgenda, fecInicial, fecFinal);
                return result;
            });
        }

        public async Task<List<AgendaHorario>> ConsultarPorFechaAsync(int numAgenda, DateTime fechaInicial, DateTime fechaFinal)
        {
            // Ejecutamos y dejamos que Exceptions registre errores si ocurre alguno
            return await _exceptions.EjecutarProcConEntidadAsync(async () =>
            {
                var result = await _citaDA.ConsultarPorFechaAsync(numAgenda, fechaInicial, fechaFinal);
                return result;
            });
        }

        public async Task<List<AgendaHorarioDetalleReceso>> ConsultarRecesosAsync(decimal idAgendaHorarioDetalle)
        {
            // Ejecutamos y dejamos que Exceptions registre errores si ocurre alguno
            return await _exceptions.EjecutarProcConEntidadAsync(async () =>
            {
                var result = await _citaDA.ConsultarRecesosAsync(idAgendaHorarioDetalle);
                return result;
            });
        }

        public async Task<Cita?> ConsultarKeyAsync(int numCita)
        {
            // Ejecutamos y dejamos que Exceptions registre errores si ocurre alguno
            return await _exceptions.EjecutarProcConEntidadAsync(async () =>
            {
                var result = await _citaDA.ConsultarKeyAsync(numCita);
                return result;
            });
        }

        public async Task<List<CitasCalendario>> ConsultarExisteEnRangoAsync(int numAgenda, DateTime fecInicial, DateTime fecFinal, int numTipoAgenda, int numIntervalo)
        {
            return await _exceptions.EjecutarProcConEntidadAsync(async () =>
            {
                var result = await _citaDA.ConsultarExisteEnRangoAsync(numAgenda, fecInicial, fecFinal, numTipoAgenda, numIntervalo);
                return result;
            });
        }

        public async Task<List<string>> InsertarCitaPacienteAsync(Cita cita, List<CitaProcedimiento> servicios, string bitacoraDatosDespues)
        {
            return await _exceptions.EjecutarProcConEntidadAsync(async () =>
            {
                var result = await _citaDA.InsertarCitaPacienteAsync(cita, servicios, bitacoraDatosDespues);
                return result;
            });
        }

        public async Task<string> NotificaCitaConvenioAsync(string codInstituc, int numAgenda)
        {
            return await _exceptions.EjecutarProcConEntidadAsync(async () =>
            {
                var result = await _citaDA.NotificaCitaConvenioAsync(codInstituc, numAgenda);
                return result;
            });
        }
    }
}
