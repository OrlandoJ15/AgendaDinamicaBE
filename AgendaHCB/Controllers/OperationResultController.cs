using BussinessLogic.Interfaces;
using DataAccess.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgenteWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OperationResultController : ControllerBase
    {
        private readonly IOperationResultBL _operationLN;

        public OperationResultController(IOperationResultBL operationLN)
        {
            _operationLN = operationLN;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var results = _operationLN.ObtenerResultados();
            return Ok(results);
        }

        [HttpGet("{code}")]
        public IActionResult GetByCode(int code)
        {
            var result = _operationLN.ObtenerResultadoPorCodigo(code);
            if (result == null)
                return NotFound($"No se encontró un resultado con código {code}");
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] OperationResult result)
        {
            if (_operationLN.InsertarResultado(result))
                return Ok("Resultado insertado correctamente");
            return BadRequest("Error al insertar el resultado");
        }

        [HttpDelete("{code}")]
        public IActionResult Delete(int code)
        {
            if (_operationLN.EliminarResultado(code))
                return Ok("Resultado eliminado correctamente");
            return BadRequest("Error al eliminar el resultado");
        }
    }
}
