/*using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Interfaces;
using CommonMethods;
using DataAccess.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgenteWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaBL _agendaBL;
        private readonly Exceptions gObjExcepciones = new Exceptions();

        public AgendaController(IAgendaBL agendaLN)
        {
            _agendaBL = agendaLN;
        }

        private ActionResult ManejoError(System.Exception ex)
        {
            gObjExcepciones.LogError(ex);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

        private IActionResult HandleResponse<T>(T response)
        {
            if (response == null)
                return NotFound("Registro no encontrado.");
            return Ok(response);
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult RecAgenda()
        {
            try
            {
                var lista = _agendaBL.RecAgenda();
                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]/{numAgenda}")]
        [HttpGet]
        public IActionResult RecAgendaXId(int numAgenda)
        {
            try
            {
                var agenda = _agendaBL.RecAgendaXId(numAgenda);
                return HandleResponse(agenda);
            }
            catch (System.Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsAgenda([FromBody] Agenda agenda)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            try
            {
                _agendaBL.InsAgenda(agenda);
                return CreatedAtAction(nameof(RecAgendaXId), new { numAgenda = agenda.NumAgenda }, agenda);
            }
            catch (System.Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public IActionResult ModAgenda([FromBody] Agenda agenda)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            try
            {
                _agendaBL.ModAgenda(agenda);
                return Ok(agenda);
            }
            catch (System.Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]/{numAgenda}")]
        [HttpDelete]
        public IActionResult DelAgenda(int numAgenda)
        {
            try
            {
                var agenda = _agendaBL.RecAgendaXId(numAgenda);
                if (agenda == null)
                    return NotFound("Registro no encontrado.");

                _agendaBL.DelAgenda(numAgenda);
                return Ok($"Agenda {numAgenda} eliminada correctamente.");
            }
            catch (System.Exception ex)
            {
                return ManejoError(ex);
            }
        }
    }
}
*/