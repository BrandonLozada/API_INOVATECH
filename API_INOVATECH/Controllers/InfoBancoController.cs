using Microsoft.AspNetCore.Mvc;
using BLL;
using Models.DTO;

namespace API_INOVATECH.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class InfoBancoController : ControllerBase {
        private readonly string Cadena;
        public InfoBancoController(IConfiguration Config)
        {
            Cadena = Config.GetConnectionString("PROD");
        }

        [HttpPost]
        [Route("CrearInfoBancaria")]
        public IActionResult GuardarInfoBancaria([FromBody] InfoBancoDTO InfoBanco)
        {
            List<string> lstDatos = BL_INFOBANCO.InsertarInfoBanco(Cadena, InfoBanco.id_usuario, InfoBanco.banco, InfoBanco.titular, InfoBanco.num_cuenta, InfoBanco.clabe, InfoBanco.tarjeta, InfoBanco.es_activa);

            if (lstDatos[0] == "00")
            {
                return Ok(new { Value = lstDatos[1] });
            }
            else
            {
                return BadRequest(new { Value = lstDatos[1] });
            }
        }

        [HttpGet]
        [Route("ListarInfoBancaria")]
        public IActionResult ListarPerfilEmpleado()
        {
            List<InfoBancoRepDTO> lstInfoBanco = BL_INFOBANCO.ConsultaTodo(Cadena);

            return Ok(new { Value = lstInfoBanco });
        }

        [HttpGet]
        [Route("ListarNombre/{Nombre}")]
        public IActionResult ListarNombre(string Nombre)
        {
            List<InfoBancoRepDTO> lstInfoBanco = BL_INFOBANCO.ConsultaXNombre(Cadena, Nombre);

            return Ok(new { Value = lstInfoBanco });
        }
    }
}
