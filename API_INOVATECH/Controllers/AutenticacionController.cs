using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Cors;
using BLL;
using Models.DTO;
using System.Text;

namespace API_INOVATECH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacionController : ControllerBase
    {
        private readonly string secretKey;
        private readonly string Cadena;
        public AutenticacionController(IConfiguration Config)
        {
            secretKey = Config.GetSection("settings").GetSection("secretkey").ToString();
            Cadena = Config.GetConnectionString("PROD");
        }

        [HttpPost]
        [Route("Validar")]
        public IActionResult Validar([FromBody] Usuario request)
        {
            List<UsuarioIdentidadDTO> lstValidacion = BL_TOKEN.ObtenerToken(Cadena, request.correo, request.contrasenia);

            if (lstValidacion != null)
            {
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.correo));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    // TODO: Agregar más tiempo al token.
                    //Expires = DateTime.UtcNow.AddMinutes(5),
                    //Expires = DateTime.UtcNow.AddHours(5),
                    Expires = DateTime.UtcNow.AddDays(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokencreado = tokenHandler.WriteToken(tokenConfig);

                return StatusCode(StatusCodes.Status200OK, new { accessToken = tokencreado, Value = lstValidacion });

            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { accessToken = "" });
            }

        }
    }
}
