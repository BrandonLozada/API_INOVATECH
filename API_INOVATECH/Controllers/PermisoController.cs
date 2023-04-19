using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using Models;
using BLL;
using Models.DTO;

namespace API_INOVATECH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermisoController : ControllerBase
    {
        private readonly string Cadena;
        public PermisoController(IConfiguration Config)
        {
            Cadena = Config.GetConnectionString("PROD");
        }

        [HttpGet]
        [Route("ListarTodo")]
        public IActionResult ListarTodo()
        {
            List<PermisoRepDTO> lstPermisoRep = BL_PERMISO.ConsultaPermisos(Cadena);

            return Ok(new { Value = lstPermisoRep });
        }
    }
}
