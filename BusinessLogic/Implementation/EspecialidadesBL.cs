using BusinessLogic.Interfaces;
using CommonMethods;
using DataAccess.Interfaces;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Implementation
{
    public class EspecialidadesBL : IEspecialidadesBL
    {
        private readonly IEspecialidadesDA _especialidadesDA;
        private readonly AsyncExceptions _exceptions;

        public EspecialidadesBL(IEspecialidadesDA especialidadesDA, AsyncExceptions exceptions)
        {
            _especialidadesDA = especialidadesDA;
            _exceptions = exceptions;
        }

        public async Task<List<Especialidad>> RecEspecialidadesxUsuarioAsync(int tipoAgenda, int usuarioId, int sucursal)
        {
            return await _exceptions.EjecutarProcConEntidadAsync(
                async () => await _especialidadesDA.RecEspecialidadesxUsuarioAsync(tipoAgenda, usuarioId, sucursal)
            );
        }
    }
}
