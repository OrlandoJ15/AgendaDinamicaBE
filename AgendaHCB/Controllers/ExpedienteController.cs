using BussinessLogic.Interfaces;
using CommonMethods;
using DataAccess.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AgenteWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExpedienteController : ControllerBase
    {
        private readonly IExpedienteBL _expedienteBL;
        private readonly Exceptions gObjExcepciones = new Exceptions();

        public ExpedienteController(IExpedienteBL expedienteLN)
        {
            _expedienteBL = expedienteLN;
        }

        private ActionResult ManejoError(System.Exception ex)
        {
            gObjExcepciones.LogError(ex);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

        private IActionResult HandleResponse<T>(T response)
        {
            if (response == null)
                return NotFound("No se encontró el registro solicitado.");
            return Ok(response);
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult RecExpediente()
        {
            try
            {
                var lista = _expedienteBL.RecExpediente();
                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]/{numExpediente}")]
        [HttpGet]
        public IActionResult RecExpedienteXId(int numExpediente)
        {
            try
            {
                var expediente = _expedienteBL.RecExpedienteXId(numExpediente);
                return HandleResponse(expediente);
            }
            catch (System.Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsExpediente([FromBody] Expediente expediente)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            try
            {
                _expedienteBL.InsExpediente(expediente);
                return CreatedAtAction(nameof(RecExpedienteXId), new { numExpediente = expediente.NumExpediente }, expediente);
            }
            catch (System.Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public IActionResult ModExpediente([FromBody] Expediente expediente)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            try
            {
                _expedienteBL.ModExpediente(expediente);
                return Ok(expediente);
            }
            catch (System.Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]/{numExpediente}")]
        [HttpDelete]
        public IActionResult DelExpediente(int numExpediente)
        {
            try
            {
                var expediente = _expedienteBL.RecExpedienteXId(numExpediente);
                if (expediente == null)
                    return NotFound("Registro no encontrado");

                _expedienteBL.DelExpediente(numExpediente);
                return Ok($"Expediente {numExpediente} eliminado correctamente.");
            }
            catch (System.Exception ex)
            {
                return ManejoError(ex);
            }
        }
    }
}
