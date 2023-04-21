using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using Models;
using BLL;
using Models.DTO;

namespace API_INOVATECH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DomicilioController : ControllerBase
    {
        private readonly string Cadena;
        public DomicilioController(IConfiguration Config)
        {
            Cadena = Config.GetConnectionString("PROD");
        }

        [HttpPost]
        [Route("LlenarDomicilio")]
        public IActionResult GuardarDomicilio([FromBody] DomicilioDTO Domicilio)
        {
            List<string> lstDatos = BL_DOMICILIO.InsertarDomicilio(Cadena, Domicilio.id_usuario, Domicilio.calle, Domicilio.numero_interior, Domicilio.numero_exterior, Domicilio.entre_calles_1, Domicilio.entre_calles_2, Domicilio.codigo_postal, Domicilio.colonia, Domicilio.ciudad, Domicilio.estado, Domicilio.pais);

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
        [Route("ListarDomicilio/{IdUsuario}")]
        public IActionResult ListarDomicilio(int IdUsuario)
        {
            List<DomicilioRepDTO> lstDomicilioRep = BL_DOMICILIO.ConsultaDomicilio(Cadena, IdUsuario);

            return Ok(new { Value = lstDomicilioRep });
        }
    }
}
