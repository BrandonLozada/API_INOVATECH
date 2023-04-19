using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using Models;
using BLL;
using Models.DTO;

namespace API_INOVATECH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentoController : ControllerBase
    {
        private readonly string Cadena;
        public DepartamentoController(IConfiguration Config)
        {
            Cadena = Config.GetConnectionString("PROD");
        }

        [HttpGet]
        [Route("ListarTodo")]
        public IActionResult ListarTodo()
        {
            List<DepartamentoRepDTO> lstDepartamentoRep = BL_DEPARTAMENTO.ConsultaDepartamentos(Cadena);

            return Ok(new { Value = lstDepartamentoRep });
        }
    }
}
