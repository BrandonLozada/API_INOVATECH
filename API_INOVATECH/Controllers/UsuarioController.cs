using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using Models;
using BLL;
using Models.DTO;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Swashbuckle.AspNetCore.SwaggerGen;

using System;
using System.Collections.Generic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;


namespace API_INOVATECH.Controllers
{
    public class ServicioSMS
    {
        public void EviarSMS(string numeroDestino)
        {
            var accountSid = "AC194e2870b28b91f22648c50c158cb8b8";
            var authToken = "396b74310219c88c3df82f0e0fbccefe";
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
              new PhoneNumber(numeroDestino));
            messageOptions.From = new PhoneNumber("+12708127824");
            messageOptions.Body = "Se ha creado un nuevo en el sistema Inovatech";

            var message = MessageResource.Create(messageOptions);
            Console.WriteLine(message.Body);
        }
    }

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

        public static bool IsPositive(int number)
        {
            return number > 0;
        }

        public static bool IsNegative(int number)
        {
            return number < 0;
        }

        public static bool IsZero(int number)
        {
            return number == 0;
        }

        private static bool ValidarId(int number)
        {
            if (!IsNegative(number) && IsPositive(number) && !IsZero(number))
                return true;
           
            return false;
        }

        private static bool ValidarFechaNacimiento(string fecha_nacimiento)
        {
            DateTime fecha;

            if (DateTime.TryParseExact(fecha_nacimiento, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out fecha))
            {
                return true;
            }

            return false;
        }

        private static bool ValidarSexo(string sexo)
        {
            Dictionary<string, string> dictSexos = new Dictionary<string, string>
            {
                { "M", "Activo" },
                { "F", "Inactivo" }
            };

            if (dictSexos.ContainsKey(sexo))
            {
                return true;
            }

            return false;
        }

        private static bool ValidarCelular(string celular)
        {
            Regex r = new Regex(@"^([0-9]){10,10}$");
            Match match = r.Match(celular);

            if (match.Success)
                return true;
            
            return false;
        }

        private static bool ValidarCorreo(string correo)
        {
            //if (!MailAddress.TryCreate(correo, out var mailAddress))
            //    return false;

            try
            {
                MailAddress m = new MailAddress(correo);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private static bool ValidarContrasenia(string contrasenia)
        {
            Regex r = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9])(?=.{10,})");
            Match match = r.Match(contrasenia);

            if (match.Success)
            {
                if (contrasenia.Length > 20)
                    return false;
       
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool ValidarActivo(int es_activo)
        {
            Dictionary<int, string> dictEstados = new Dictionary<int, string>
            {
                { 1, "Activo" },
                { 0, "Inactivo" }
            };

            if (dictEstados.ContainsKey(es_activo))
            {
                return true;
            }

            return false;
        }

        private static bool ValidarRol(int id_rol)
        {
            if (id_rol == 1)
                return false;

            Dictionary<int, string> dictRoles = new Dictionary<int, string>
            {
                { 2, "Programador" },
                { 3, "Administrador de base de datos" },
                { 4, "Administrador" },
                { 5, "Soporte de aplicaciones" },
                { 6, "Encargado de sistemas" },
                { 7, "Ordinario" }
            };

            if (dictRoles.ContainsKey(id_rol))
            {
                return true;
            }

            return false;
        }

        [HttpPost]
        [Route("GuardarUsuario")]
        public IActionResult GuardarUsuario([FromBody] UsuarioDTO Usuario)
        {
            if (!ValidarFechaNacimiento(Usuario.fecha_nacimiento))
            {
                return BadRequest(new { Value = "Formato inválido para campo fecha. Formato permitido (YYYY/MM/DD)" });
            }

            if (!ValidarSexo(Usuario.sexo))
            {
                return BadRequest(new { Value = "Valor inválido para campo sexo. Valores permitidos 'M' | 'F'" });
            }

            if (!ValidarCelular(Usuario.celular))
            {
                return BadRequest(new { Value = "Valor inválido para campo celular. Valor permitido '##########' de 10 digítos" });
            }

            if (!ValidarCorreo(Usuario.correo))
            {
                return BadRequest(new { Value = "El correo no tiene formato correcto." });
            }

            if (!ValidarContrasenia(Usuario.contrasenia))
            {
                return BadRequest(new { Value = "La contraseña no cumple con los requisitos." });
            }

            if (!ValidarActivo(Usuario.es_activo))
            {
                return BadRequest(new { Value = "Valor inválido para la actividad del usuario." });
            }

            if (!ValidarRol(Usuario.id_rol))
            {
                return BadRequest(new { Value = "No se encuentra ese rol de usuario." });
            }

            List<string> lstDatos = BL_USUARIO.InsertarUsuario(Cadena, Usuario.nombre, Usuario.primer_apellido, Usuario.segundo_apellido, Usuario.fecha_nacimiento, Usuario.sexo, Usuario.celular, Usuario.correo, Usuario.contrasenia, Usuario.es_activo, Usuario.id_rol);

            if (lstDatos[0] == "00")
            {
                ServicioSMS _ServicioSms = new ServicioSMS();
                _ServicioSms.EviarSMS("+528180251208");
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
            if (!ValidarId(IdUsuario))
            {
                return BadRequest(new { Value = "Identificador del usuario inválido. No existe usuario con ese identificador" });
            }

            if (!ValidarFechaNacimiento(Usuario.fecha_nacimiento))
            {
                return BadRequest(new { Value = "Formato inválido para campo fecha. Formato permitido (YYYY/MM/DD)" });
            }

            if (!ValidarSexo(Usuario.sexo))
            {
                return BadRequest(new { Value = "Valor inválido para campo sexo. Valores permitidos 'M' | 'F'" });
            }

            if (!ValidarCelular(Usuario.celular))
            {
                return BadRequest(new { Value = "Valor inválido para campo celular. Valor permitido '##########' de 10 digítos" });
            }

            if (!ValidarCorreo(Usuario.correo))
            {
                return BadRequest(new { Value = "El correo no tiene formato correcto." });
            }

            if (!ValidarContrasenia(Usuario.contrasenia))
            {
                return BadRequest(new { Value = "La contraseña no cumple con los requisitos." });
            }

            if (!ValidarActivo(Usuario.es_activo))
            {
                return BadRequest(new { Value = "Valor inválido para la actividad del usuario." });
            }

            if (!ValidarRol(Usuario.id_rol))
            {
                return BadRequest(new { Value = "No se encuentra ese rol de usuario." });
            }

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
            if (!ValidarId(IdUsuario))
            {
                return BadRequest(new { Value = "Identificador del usuario inválido. No existe usuario con ese identificador" });
            }

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

        //[HttpDelete]
        //[Route("EliminarUsuarioFisico/{IdUsuario}")]
        //public IActionResult EliminarUsuarioFisico(int IdUsuario)
        //{
        //    if (!ValidarId(IdUsuario))
        //    {
        //        return BadRequest(new { Value = "Identificador del usuario inválido. No existe usuario con ese identificador" });
        //    }

        //    List<string> lstDatos = BL_USUARIO.EliminarUsuarioFisico(Cadena, IdUsuario);

        //    if (lstDatos[0] == "00")
        //    {
        //        return Ok(new { Value = lstDatos[1] });
        //    }
        //    else
        //    {
        //        return BadRequest(new { Value = lstDatos[1] });
        //    }

        //}

        // TODO: En los EndPoints donde obtengo a un solo usuario por medio de un ID, la respuesta tiene que ser de otro tipo que no sea "List" (Pref Objeto). 
        //       Esto es para mejor manipulación en el Front y a la hora de la función Modificar enviar como "Props" a otra página.
        [HttpGet]
        [Route("ListarPerfil/{IdUsuario}")]
        public IActionResult ListarPerfil(int IdUsuario)
        {
            if (!ValidarId(IdUsuario))
            {
                return BadRequest(new { Value = "Identificador del usuario inválido. No existe usuario con ese identificador" });
            }

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
