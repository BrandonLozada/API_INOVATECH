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
            List<string> lstDatos = BL_INFOBANCO.InsertarInfoBanco(Cadena, InfoBanco.id_usuario, InfoBanco.banco, InfoBanco.titular, InfoBanco.num_cuenta, InfoBanco.clabe, InfoBanco.tarjeta);

            if (lstDatos[0] == "00")
            {
                return Ok(new { Value = lstDatos[1] });
            }
            else
            {
                return BadRequest(new { Value = lstDatos[1] });
            }
        }
    }
}
