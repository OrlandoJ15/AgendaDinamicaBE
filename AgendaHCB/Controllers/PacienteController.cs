using BusinessLogic.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteBL _pacienteBL;

        public PacienteController(IPacienteBL pacienteBL)
        {
            _pacienteBL = pacienteBL;
        }

        // GET: api/Paciente?primerNom=...&segundoNom=...&primerAp=...&segundoAp=...
        [HttpGet("ByName")]
        public ActionResult<List<Expediente>> GetRecordByName(
            [FromQuery] string primerNom = null,
            [FromQuery] string segundoNom = null,
            [FromQuery] string primerAp = null,
            [FromQuery] string segundoAp = null)
        {
            try
            {
                var resultados = _pacienteBL.GetRecordByName(primerNom, segundoNom, primerAp, segundoAp);

                if (resultados == null || resultados.Count == 0)
                    return NotFound("No se encontraron expedientes con los parámetros proporcionados.");

                return Ok(resultados);
            }
            catch (Exception ex)
            {
                // Aquí puedes loguear el error con tu clase Exceptions si quieres
                return StatusCode(500, $"Ocurrió un error al obtener los expedientes: {ex.Message}");
            }
        }

        [HttpGet("ByIdentification")]
        public ActionResult<List<Expediente>> GetRecordByIdentification(
           [FromQuery] string pidentificacion = null,
           [FromQuery] string pcod_tipdoc = null)
        {
            try
            {
                var resultados = _pacienteBL.GetRecordByIdentification(pidentificacion, pcod_tipdoc);

                if (resultados == null || resultados.Count == 0)
                    return NotFound("No se encontraron expedientes con los parámetros proporcionados.");

                return Ok(resultados);
            }
            catch (Exception ex)
            {
                // Aquí puedes loguear el error con tu clase Exceptions si quieres
                return StatusCode(500, $"Ocurrió un error al obtener los expedientes: {ex.Message}");
            }
        }

        [HttpPost("InsertNewRecord")]
        public ActionResult<List<string>> InsertNewRecord([FromBody] Expediente expediente)
        {
            try
            {
                if (expediente == null)
                    return BadRequest("El objeto Expediente no puede ser nulo.");

                var resultado = _pacienteBL.InsertNewRecord(expediente);

                // Ejemplo: resultado = [ "0", "Registro insertado correctamente", "EXP1234" ]
                if (resultado == null || resultado.Count == 0)
                    return StatusCode(500, "No se obtuvo respuesta del procedimiento almacenado.");

                var codError = resultado[0];
                var desError = resultado.Count > 1 ? resultado[1] : "";
                var numExpediente = resultado.Count > 2 ? resultado[2] : "";

                if (codError != "0")
                    return BadRequest(new { codError, desError });

                return Ok(new
                {
                    codError,
                    desError,
                    numExpediente
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al insertar el expediente: {ex.Message}");
            }
        }
    }
}
