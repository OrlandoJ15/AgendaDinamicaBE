using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using BussinessLogic.Interfaces;
using CommonMethods;
using System.Collections.Generic;

namespace AgenteWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CitaProcedimientoController : ControllerBase
    {
        private readonly ICitaProcedimientoBL _citaProcedimientoLN;
        private readonly Exceptions gObjExcepciones = new Exceptions();

        public CitaProcedimientoController(ICitaProcedimientoBL citaProcedimientoLN)
        {
            _citaProcedimientoLN = citaProcedimientoLN;
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
                return NotFound("Registro no encontrado");

            return Ok(response);
        }

        // =======================================================
        // MÉTODOS DEL API
        // =======================================================

        [Route("[action]")]
        [HttpGet]
        public ActionResult<List<CitaProcedimiento>> RecCitaProcedimientos()
        {
            try
            {
                var lista = _citaProcedimientoLN.RecCitaProcedimientos();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]/{numCita}/{codArticulo}")]
        [HttpGet]
        public IActionResult RecCitaProcedimientoXId(int numCita, string codArticulo)
        {
            try
            {
                var citaProc = _citaProcedimientoLN.RecCitaProcedimientoXId(numCita, codArticulo);
                return HandleResponse(citaProc);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsCitaProcedimiento([FromBody] CitaProcedimiento citaProc)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            try
            {
                _citaProcedimientoLN.InsCitaProcedimiento(citaProc);
                return CreatedAtAction(nameof(RecCitaProcedimientoXId),
                    new { numCita = citaProc.NUM_CITA, codArticulo = citaProc.COD_ARTICULO }, citaProc);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public IActionResult ModCitaProcedimiento([FromBody] CitaProcedimiento citaProc)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            try
            {
                _citaProcedimientoLN.ModCitaProcedimiento(citaProc);
                return Ok(citaProc);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]/{numCita}/{codArticulo}")]
        [HttpDelete]
        public IActionResult DelCitaProcedimiento(int numCita, string codArticulo)
        {
            try
            {
                var citaProc = _citaProcedimientoLN.RecCitaProcedimientoXId(numCita, codArticulo);
                if (citaProc == null)
                    return NotFound("Cita procedimiento no encontrada");

                _citaProcedimientoLN.DelCitaProcedimiento(numCita, codArticulo);
                return Ok(citaProc);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }
    }
}
