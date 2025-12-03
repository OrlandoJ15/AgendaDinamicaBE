using BusinessLogic.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaHCB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitaController : ControllerBase
    {
        private readonly ICitaBL _citaBL;

        public CitaController(ICitaBL citaBL)
        {
            _citaBL = citaBL;
        }


        [HttpGet("consultByKey")]
        public async Task<IActionResult> ConsultarKey([FromQuery] int numAgenda, [FromQuery] DateTime fecInicial, [FromQuery] DateTime fecFinal)
        {
            if (numAgenda <= 0)
                return BadRequest("El número de agenda es obligatorio.");

            var result = await _citaBL.ConsultarKeyAsync(numAgenda, fecInicial, fecFinal);

            if (result == null || result.Count == 0)
                return NotFound("No se encontraron citas para el rango indicado.");

            return Ok(result);
        }

        [HttpGet("consultByDate")]
        public async Task<IActionResult> ConsultarPorFecha([FromQuery] int numAgenda, [FromQuery] DateTime fecInicial, [FromQuery] DateTime fecFinal)
        {
            if (numAgenda <= 0)
                return BadRequest("El número de agenda es obligatorio.");

            var result = await _citaBL.ConsultarPorFechaAsync(numAgenda, fecInicial, fecFinal);

            if (result == null || result.Count == 0)
                return NotFound("No se encontraron horarios para el rango indicado.");

            return Ok(result);
        }

        [HttpGet("ConsultarRecesosAsync")]
        public async Task<IActionResult> ConsultarRecesosAsync([FromQuery] decimal idAgendaHorarioDetalle)
        {
            if (idAgendaHorarioDetalle <= 0)
                return BadRequest("El número de agenda es obligatorio.");

            var result = await _citaBL.ConsultarRecesosAsync(idAgendaHorarioDetalle);

            if (result == null || result.Count == 0)
                return NotFound("No se encontraron horarios para el rango indicado.");

            return Ok(result);

        }

        [HttpGet("ConsultarCitaKeyAsync")]
        public async Task<IActionResult> ConsultarKeyAsync([FromQuery] int numCita)
        {
            if (numCita <= 0)
                return BadRequest("El número de agenda es obligatorio.");

            var result = await _citaBL.ConsultarKeyAsync(numCita);

            if (result == null)
                return NotFound($"No se encontró ninguna cita con el número {numCita}");

            return Ok(result);

        }

        [HttpGet("ConsultarExisteEnRangoAsync")]
        public async Task<IActionResult> ConsultarExisteEnRangoAsync([FromQuery] int numAgenda, [FromQuery] DateTime fecInicial, [FromQuery] DateTime fecFinal, [FromQuery] int numTipoAgenda, [FromQuery] int numIntervalo)
        {
            if (numAgenda <= 0)
                return BadRequest("El número de agenda es obligatorio.");

            var result = await _citaBL.ConsultarExisteEnRangoAsync(numAgenda, fecInicial, fecFinal, numTipoAgenda, numIntervalo);

            if (result == null || result.Count == 0)
                return NotFound("No se encontraron citas en el rango indicado.");

            return Ok(result);

        }

        [HttpPost("insertarCita")]
        public async Task<IActionResult> InsertarCitaPaciente([FromBody] InsertarCitaRequest request)
        {
            if (request?.Cita == null)
                return BadRequest("La información de la cita es obligatoria.");

            // Llamar al BL (asumiendo que ICitaBL tiene InsertarCitaPacienteAsync con la misma firma)
            var result = await _citaBL.InsertarCitaPacienteAsync(
                request.Cita,
                request.Servicios ?? new List<CitaProcedimiento>(),
                request.BitacoraDatosDespues ?? string.Empty
            );

            if (result == null || result.Count < 1)
                return StatusCode(500, "No se obtuvo respuesta del proceso.");

            // result[0] = código (string), result[1] = mensaje o número de cita (según tu impl)
            if (result[0] != "0")
                return BadRequest(new { Codigo = result[0], Mensaje = (result.Count > 1 ? result[1] : "Error") });

            // éxito: devolver número de cita (estás poniendo newNumCita en result[1])
            return Ok(new { Codigo = result[0], NumCita = (result.Count > 1 ? result[1] : string.Empty) });
        }

        [HttpGet("notificarConvenio")]
        public async Task<IActionResult> NotificaCitaConvenioAsync([FromQuery] string codInstituc, [FromQuery] int numAgenda)
        {
            if (string.IsNullOrWhiteSpace(codInstituc))
                return BadRequest("El código de institución es obligatorio.");

            if (numAgenda <= 0)
                return BadRequest("El número de agenda es obligatorio.");

            var result = await _citaBL.NotificaCitaConvenioAsync(codInstituc, numAgenda);

            if (string.IsNullOrWhiteSpace(result))
                return NotFound("No se obtuvo respuesta del procedimiento almacenado.");

            return Ok(new { Resultado = result });
        }


    }
}
