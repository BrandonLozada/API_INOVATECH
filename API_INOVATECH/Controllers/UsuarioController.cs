using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using Models;
using BLL;
using Models.DTO;

namespace API_INOVATECH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly string Cadena;
        public UsuarioController(IConfiguration Config)
        {
            Cadena = Config.GetConnectionString("PROD");
        }

        // TODO: Corregir este EndPoint, verificar la finalidad para Front.
        [HttpGet]
        [Route("ListarTodo")]
        public IActionResult ListarTodo()
        {
            List<UsuarioGenDTO> lstUsuarioRep = BL_USUARIO.ConsultaTodo(Cadena);

            return Ok(new { Value = lstUsuarioRep });
        }

        [HttpGet]
        [Route("ListarUsuario")]
        public IActionResult ListarUsuario()
        {
            List<UsuarioRepDTO> lstUsuarioRep = BL_USUARIO.ConsultaGeneral(Cadena);

            return Ok(new { Value = lstUsuarioRep });
        }

        [HttpGet]
        [Route("ListarNombre/{Nombre}")]
        public IActionResult ListarNombre(string Nombre)
        {
            List<UsuarioRepDTO> lstUsuarioRep = BL_USUARIO.ConsultaXNombre(Cadena, Nombre);

            return Ok(new { Value = lstUsuarioRep });
        }

        [HttpPost]
        [Route("GuardarUsuario")]
        public IActionResult GuardarUsuario([FromBody] UsuarioDTO Usuario)
        {
            List<string> lstDatos = BL_USUARIO.InsertarUsuario(Cadena, Usuario.nombre, Usuario.primer_apellido, Usuario.segundo_apellido, Usuario.fecha_nacimiento, Usuario.sexo, Usuario.celular, Usuario.correo, Usuario.contrasenia, Usuario.es_activo, Usuario.id_rol);

            if (lstDatos[0] == "00")
            {
                return Ok(new { Value = lstDatos[1] });
            }
            else
            {
                return BadRequest(new { Value = lstDatos[1] });
            }

        }

        [HttpPost]
        [Route("ActualizarUsuario/{IdUsuario}")]
        public IActionResult ActualizarUsuario(int IdUsuario, [FromBody] UsuarioDTO Usuario)
        {
            List<string> lstDatos = BL_USUARIO.ActualizarUsuario(Cadena, IdUsuario, Usuario.nombre, Usuario.primer_apellido, Usuario.segundo_apellido, Usuario.fecha_nacimiento, Usuario.sexo, Usuario.celular, Usuario.correo, Usuario.contrasenia, Usuario.es_activo, Usuario.id_rol);

            if (lstDatos[0] == "00")
            {
                return Ok(new { Value = lstDatos[1] });
            }
            else
            {
                return BadRequest(new { Value = lstDatos[1] });
            }

        }

        [HttpPost]
        [Route("EliminarUsuario/{IdUsuario}")]
        public IActionResult EliminarUsuario(int IdUsuario)
        {
            List<string> lstDatos = BL_USUARIO.EliminarUsuario(Cadena, IdUsuario);

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
