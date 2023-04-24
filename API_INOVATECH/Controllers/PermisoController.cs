﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        [Route("GuardarPermiso")]
        public IActionResult GuardarPermiso([FromBody] PermisoDTO Permiso)
        {
            List<string> lstDatos = BL_PERMISO.InsertarSolicitudPermiso(Cadena, Permiso.id_usuario_solicitante, Permiso.id_permiso, Permiso.motivo, Permiso.fecha_inicio, Permiso.fecha_fin, Permiso.id_usuario_autorizador);

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
