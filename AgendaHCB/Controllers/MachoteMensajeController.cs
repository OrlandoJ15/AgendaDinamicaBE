using BussinessLogic.Interfaces;
using DataAccess.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgenteWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MachoteMensajeController : ControllerBase
    {
        private readonly IMachoteMensajeBL _machoteLN;

        public MachoteMensajeController(IMachoteMensajeBL machoteLN)
        {
            _machoteLN = machoteLN;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var lista = _machoteLN.ObtenerMachotes();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var machote = _machoteLN.ObtenerMachotePorId(id);
            if (machote == null)
                return NotFound($"No se encontró el machote con ID {id}");
            return Ok(machote);
        }

        [HttpPost]
        public IActionResult Create([FromBody] MachoteMensaje machote)
        {
            if (_machoteLN.InsertarMachote(machote))
                return Ok("Machote creado correctamente");
            return BadRequest("Error al crear el machote");
        }

        [HttpPut]
        public IActionResult Update([FromBody] MachoteMensaje machote)
        {
            if (_machoteLN.ActualizarMachote(machote))
                return Ok("Machote actualizado correctamente");
            return BadRequest("Error al actualizar el machote");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_machoteLN.EliminarMachote(id))
                return Ok("Machote eliminado correctamente");
            return BadRequest("Error al eliminar el machote");
        }
    }
}
