using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using Models;
using BLL;
using Models.DTO;

namespace API_INOVATECH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolController : ControllerBase
    {
        private readonly string Cadena;
        public RolController(IConfiguration Config)
        {
            Cadena = Config.GetConnectionString("PROD");
        }

        [HttpGet]
        [Route("ListarTodo")]
        public IActionResult ListarTodo()
        {
            List<RolRepDTO> lstRolRep = BL_ROL.ConsultaRoles(Cadena);

            return Ok(new { Value = lstRolRep });
        }
    }
}
