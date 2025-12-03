using BussinessLogic.Interfaces;
using DataAccess.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgenteWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ParamEnvioCorreoAdjuntoController : ControllerBase
    {
        private readonly IParamEnvioCorreoAdjuntoBL _adjuntoLN;

        public ParamEnvioCorreoAdjuntoController(IParamEnvioCorreoAdjuntoBL adjuntoLN)
        {
            _adjuntoLN = adjuntoLN;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var lista = _adjuntoLN.ObtenerAdjuntos();
            return Ok(lista);
        }

        [HttpGet("{nombre}")]
        public IActionResult GetByName(string nombre)
        {
            var adjunto = _adjuntoLN.ObtenerAdjuntoPorNombre(nombre);
            if (adjunto == null)
                return NotFound($"No se encontró un adjunto con nombre {nombre}");
            return Ok(adjunto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ParamEnvioCorreoAdjunto adjunto)
        {
            if (_adjuntoLN.InsertarAdjunto(adjunto))
                return Ok("Adjunto insertado correctamente");
            return BadRequest("Error al insertar el adjunto");
        }

        [HttpDelete("{nombre}")]
        public IActionResult Delete(string nombre)
        {
            if (_adjuntoLN.EliminarAdjunto(nombre))
                return Ok("Adjunto eliminado correctamente");
            return BadRequest("Error al eliminar el adjunto");
        }
    }
}
