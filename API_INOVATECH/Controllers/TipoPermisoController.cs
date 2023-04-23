using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using Models;
using BLL;
using Models.DTO;

namespace API_INOVATECH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoPermisoController : ControllerBase
    {
        private readonly string Cadena;
        public TipoPermisoController(IConfiguration Config)
        {
            Cadena = Config.GetConnectionString("PROD");
        }

        [HttpGet]
        [Route("ListarTodo")]
        public IActionResult ListarTodo()
        {
            List<TipoPermisoDTO> lstTipoPermiso = BL_TIPO_PERMISO.ConsultaTiposPermisos(Cadena);

            return Ok(new { Value = lstTipoPermiso });
        }
    }
}
