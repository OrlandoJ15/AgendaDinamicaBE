using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using BussinessLogic.Interfaces;
using CommonMethods;
using System.Collections.Generic;

namespace AgenteWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ParamEnvioCorreoController : ControllerBase
    {
        private readonly IParamEnvioCorreoBL _paramEnvioCorreoLN;
        private readonly Exceptions gObjExcepciones = new Exceptions();

        public ParamEnvioCorreoController(IParamEnvioCorreoBL paramEnvioCorreoLN)
        {
            _paramEnvioCorreoLN = paramEnvioCorreoLN;
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
        public ActionResult<List<ParamEnvioCorreo>> RecParamsEnvioCorreo()
        {
            try
            {
                var lista = _paramEnvioCorreoLN.RecParamsEnvioCorreo();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]/{compania}")]
        [HttpGet]
        public IActionResult RecParamEnvioCorreoXId(string compania)
        {
            try
            {
                var param = _paramEnvioCorreoLN.RecParamEnvioCorreoXId(compania);
                return HandleResponse(param);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsParamEnvioCorreo([FromBody] ParamEnvioCorreo param)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            try
            {
                _paramEnvioCorreoLN.InsParamEnvioCorreo(param);
                return CreatedAtAction(nameof(RecParamEnvioCorreoXId), new { compania = param.COMPAÑIA }, param);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public IActionResult ModParamEnvioCorreo([FromBody] ParamEnvioCorreo param)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            try
            {
                _paramEnvioCorreoLN.ModParamEnvioCorreo(param);
                return Ok(param);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]/{compania}")]
        [HttpDelete]
        public IActionResult DelParamEnvioCorreo(string compania)
        {
            try
            {
                var param = _paramEnvioCorreoLN.RecParamEnvioCorreoXId(compania);
                if (param == null)
                    return NotFound("Parámetro de envío de correo no encontrado");

                _paramEnvioCorreoLN.DelParamEnvioCorreo(compania);
                return Ok(param);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }
    }
}
