/*using AgendaHCB.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgendaHCB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OracleController : Controller
    {
        private readonly OracleService _oracleService;

        public OracleController(OracleService oracleService)
        {
            _oracleService = oracleService;
        }

        [HttpGet("empleados")]
        public async Task<IActionResult> GetEmpleados()
        {
            var query = "SELECT * FROM EMPLEADOS FETCH FIRST 10 ROWS ONLY";
            var resultado = await _oracleService.EjecutarQueryAsync(query);
            return Ok(resultado);
        }
    }

}

*/
