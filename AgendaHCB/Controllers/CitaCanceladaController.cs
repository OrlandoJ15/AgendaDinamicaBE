/*using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;
using CommonMethods;
using System.Collections.Generic;

namespace AgenteWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CitaCanceladaController : ControllerBase
    {
        private readonly ICitaCanceladaBL _citaCanceladaLN;
        private readonly Exceptions gObjExcepciones = new Exceptions();

        public CitaCanceladaController(ICitaCanceladaBL citaCanceladaLN)
        {
            _citaCanceladaLN = citaCanceladaLN;
        }

        // =======================================================
        // MÉTODOS PRIVADOS DE MANEJO DE ERRORES Y RESPUESTAS
        // =======================================================

        private ActionResult ManejoError(Exception ex)
        {
            gObjExcepciones.LogError(ex);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

        private IActionResult HandleResponse<T>(T response)
        {
            if (response == null)
                return new JsonResult(null); // 404 Not Found

            return Ok(response); // 200 OK
        }

        // =======================================================
        // MÉTODOS DEL API
        // =======================================================

        [Route("[action]")]
        [HttpGet]
        public ActionResult<List<CitaCancelada>> RecCitasCanceladas()
        {
            try
            {
                var lista = _citaCanceladaLN.RecCitasCanceladas();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]/{numCitaCancelada}")]
        [HttpGet]
        public IActionResult RecCitaCanceladaXId(int numCitaCancelada)
        {
            try
            {
                var cita = _citaCanceladaLN.RecCitaCanceladaXId(numCitaCancelada);
                return HandleResponse(cita);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsCitaCancelada([FromBody] CitaCancelada cita)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            try
            {
                _citaCanceladaLN.InsCitaCancelada(cita);
                return CreatedAtAction(nameof(RecCitaCanceladaXId), new { numCitaCancelada = cita.NumCitaCancelada }, cita);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public IActionResult ModCitaCancelada([FromBody] CitaCancelada cita)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            try
            {
                _citaCanceladaLN.ModCitaCancelada(cita);
                return Ok(cita);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]/{numCitaCancelada}")]
        [HttpDelete]
        public IActionResult DelCitaCancelada(int numCitaCancelada)
        {
            try
            {
                var cita = _citaCanceladaLN.RecCitaCanceladaXId(numCitaCancelada);
                if (cita == null)
                    return NotFound("Cita cancelada no encontrada");

                _citaCanceladaLN.DelCitaCancelada(numCitaCancelada);
                return Ok(cita);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }
    }
}
*/