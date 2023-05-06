using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using Models;
using BLL;
using Models.DTO;


namespace API_INOVATECH.Controllers
{
    [EnableCors("ReglasCors")]
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly string Cadena;
        public UsuarioController(IConfiguration Config)
        {
            Cadena = Config.GetConnectionString("PROD");
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

        [HttpPut]
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

        [HttpPost]
        [Route("IdentificarUsuario")]
        public IActionResult IdentificarUsuario([FromBody] UsuarioInicioSesionDTO Usuario2)
        {
            List<UsuarioIdentidadDTO> lstUsuarioIdentificado = BL_USUARIO.IdentificarUsuario(Cadena, Usuario2.correo, Usuario2.contrasenia);

            return Ok(new { Value = lstUsuarioIdentificado });

        }

        // TODO: En los EndPoints donde obtengo a un solo usuario por medio de un ID, la respuesta tiene que ser de otro tipo que no sea "List" (Pref Objeto). 
        //       Esto es para mejor manipulación en el Front y a la hora de la función Modificar enviar como "Props" a otra página.
        [HttpGet]
        [Route("ListarPerfil/{IdUsuario}")]
        public IActionResult ListarPerfil(int IdUsuario)
        {
            List<UsuarioBioDTO> lstUsuarioRep = BL_USUARIO.ConsultaPerfil(Cadena, IdUsuario);

            return Ok(new { Value = lstUsuarioRep });
        }

        [HttpGet]
        [Route("ListarUsuario")]
        public IActionResult ListarUsuario()
        {
            List<UsuarioRepDTO> lstUsuarioRep = BL_USUARIO.ConsultaGeneral(Cadena);

            return Ok(new { Value = lstUsuarioRep });
        }
    }
}
