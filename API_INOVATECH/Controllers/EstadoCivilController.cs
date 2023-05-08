using BLL;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace API_INOVATECH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadoCivilController : ControllerBase
    {
        private readonly string Cadena;
        public EstadoCivilController(IConfiguration Config)
        {
            Cadena = Config.GetConnectionString("PROD");
        }

        [HttpGet]
        [Route("ListarTodo")]
        public IActionResult ListarTodo()
        {
            List<EstadoCivilDTO> lstEstadoCivil = BL_ESTADO_CIVIL.ConsultaEstadosCiviles(Cadena);

            return Ok(new { Value = lstEstadoCivil });
        }
    }
}

