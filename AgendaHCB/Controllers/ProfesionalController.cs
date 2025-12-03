using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using BussinessLogic.Interfaces;
using CommonMethods;
using System.Collections.Generic;

namespace AgenteWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProfesionalController : ControllerBase
    {
        private readonly IProfesionalBL _profLN;
        private readonly Exceptions _exceptions = new Exceptions();

        public ProfesionalController(IProfesionalBL profLN)
        {
            _profLN = profLN;
        }

        private ActionResult ManejoError(System.Exception ex)
        {
            _exceptions.LogError(ex);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

        private IActionResult HandleResponse<T>(T response)
        {
            if (response == null)
                return NotFound("Profesional no encontrado");
            return Ok(response);
        }

        [HttpGet("RecProfesionales")]
        public ActionResult<List<Profesional>> RecProfesionales()
        {
            try { return Ok(_profLN.RecProfesionales()); }
            catch (System.Exception ex) { return ManejoError(ex); }
        }

        [HttpGet("RecProfesionalXCod/{codProf}")]
        public IActionResult RecProfesionalXCod(string codProf)
        {
            try
            {
                var prof = _profLN.RecProfesionalXCod(codProf);
                return HandleResponse(prof);
            }
            catch (System.Exception ex) { return ManejoError(ex); }
        }

        [HttpPost("InsProfesional")]
        public IActionResult InsProfesional([FromBody] Profesional prof)
        {
            if (!ModelState.IsValid) return BadRequest("Modelo inválido");
            try
            {
                _profLN.InsProfesional(prof);
                return CreatedAtAction(nameof(RecProfesionalXCod), new { codProf = prof.CodProf }, prof);
            }
            catch (System.Exception ex) { return ManejoError(ex); }
        }

        [HttpPut("ModProfesional")]
        public IActionResult ModProfesional([FromBody] Profesional prof)
        {
            if (!ModelState.IsValid) return BadRequest("Modelo inválido");
            try
            {
                _profLN.ModProfesional(prof);
                return Ok(prof);
            }
            catch (System.Exception ex) { return ManejoError(ex); }
        }

        [HttpDelete("DelProfesional/{codProf}")]
        public IActionResult DelProfesional(string codProf)
        {
            try
            {
                var prof = _profLN.RecProfesionalXCod(codProf);
                if (prof == null) return NotFound("Profesional no encontrado");
                _profLN.DelProfesional(codProf);
                return Ok(prof);
            }
            catch (System.Exception ex) { return ManejoError(ex); }
        }
    }
}
