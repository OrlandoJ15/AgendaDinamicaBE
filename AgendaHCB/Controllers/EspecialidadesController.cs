/*using BusinessLogic.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaHCB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesController : ControllerBase
    {
        private readonly IEspecialidadesBL _especialidadesBL;

        public EspecialidadesController(IEspecialidadesBL especialidadesBL)
        {
            _especialidadesBL = especialidadesBL;
        }

        [HttpGet]
        public async Task<ActionResult<List<Especialidad>>> GetEspecialidades(
            [FromQuery] int tipoAgenda,
            [FromQuery] int usuarioId,
            [FromQuery] int sucursal)
        {
            var result = await _especialidadesBL.RecEspecialidadesxUsuarioAsync(tipoAgenda, usuarioId, sucursal);

            if (result == null || result.Count == 0)
                return NotFound("No se encontraron especialidades para el usuario indicado.");

            return Ok(result);
        }
    }
}
*/