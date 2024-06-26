﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using Models;
using BLL;
using Models.DTO;

namespace API_INOVATECH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerfilEmpleadoController : ControllerBase
    {
        private readonly string Cadena;
        public PerfilEmpleadoController(IConfiguration Config)
        {
            Cadena = Config.GetConnectionString("PROD");
        }

        [HttpPost]
        [Route("CrearPerfilEmpleado")]
        public IActionResult GuardarPerfilEmpleado([FromBody] PerfilEmpleadoDTO PerfilEmpleado)
        {
            List<string> lstDatos = BL_PERFIL_EMPLEADO.InsertarPerfilEmpleado(Cadena, PerfilEmpleado.id_usuario, PerfilEmpleado.nomina, PerfilEmpleado.CURP, PerfilEmpleado.RFC, 
                                                                              PerfilEmpleado.NSS, PerfilEmpleado.infonavit, PerfilEmpleado.salario, PerfilEmpleado.estado_civil,
                                                                              PerfilEmpleado.fecha_ingreso, PerfilEmpleado.fecha_egreso, PerfilEmpleado.id_puesto, PerfilEmpleado.id_departamento);

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
        [Route("ListarPerfilEmpleado/{IdUsuario}")]
        public IActionResult ListarPerfilEmpleado(int IdUsuario)
        {
            List<PerfilEmpleadoBioDTO> lstPerfilEmpleado = BL_PERFIL_EMPLEADO.ConsultaPerfilEmpleado(Cadena, IdUsuario);

            return Ok(new { Value = lstPerfilEmpleado });
        }

        [HttpPut]
        [Route("ActualizarDiasDescanso/{IdUsuario}")]
        public IActionResult ActualizarPermiso(int IdUsuario)
        {
            List<string> lstDatos = BL_PERFIL_EMPLEADO.ActualizarDiasDescanso(Cadena, IdUsuario);

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
