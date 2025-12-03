using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using BussinessLogic.Interfaces;
using CommonMethods;
using System.Collections.Generic;

namespace AgenteWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CitaReprogramadaController : ControllerBase
    {
        private readonly ICitaReprogramadaBL _citaReprogramadaBL;
        private readonly Exceptions gObjExcepciones = new Exceptions();

        public CitaReprogramadaController(ICitaReprogramadaBL citaReprogramadaLN)
        {
            _citaReprogramadaBL = citaReprogramadaLN;
        }

        private ActionResult ManejoError(Exception ex)
        {
            gObjExcepciones.LogError(ex);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

        private IActionResult HandleResponse<T>(T response)
        {
            if (response == null)
                return NotFound("Registro no encontrado");
            return Ok(response);
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult<List<CitaReprogramada>> RecCitasReprogramadas()
        {
            try
            {
                var lista = _citaReprogramadaBL.RecCitasReprogramadas();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]/{numCitaReprogramada}")]
        [HttpGet]
        public IActionResult RecCitaReprogramadaXId(int numCitaReprogramada)
        {
            try
            {
                var cita = _citaReprogramadaBL.RecCitaReprogramadaXId(numCitaReprogramada);
                return HandleResponse(cita);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsCitaReprogramada([FromBody] CitaReprogramada cita)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            try
            {
                _citaReprogramadaBL.InsCitaReprogramada(cita);
                return CreatedAtAction(nameof(RecCitaReprogramadaXId),
                    new { numCitaReprogramada = cita.NumCitaReprogramada }, cita);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public IActionResult ModCitaReprogramada([FromBody] CitaReprogramada cita)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            try
            {
                _citaReprogramadaBL.ModCitaReprogramada(cita);
                return Ok(cita);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]/{numCitaReprogramada}")]
        [HttpDelete]
        public IActionResult DelCitaReprogramada(int numCitaReprogramada)
        {
            try
            {
                var cita = _citaReprogramadaBL.RecCitaReprogramadaXId(numCitaReprogramada);
                if (cita == null)
                    return NotFound("Cita reprogramada no encontrada");

                _citaReprogramadaBL.DelCitaReprogramada(numCitaReprogramada);
                return Ok(cita);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }
    }
}
