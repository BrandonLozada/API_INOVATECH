using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using Models.DTO;

namespace BLL
{
    public class BL_USUARIO
    {
        public static List<UsuarioDTO> ConsultaTodo(string P_Cadena)
        {
            List<UsuarioDTO> lstUsuarioRep = new List<UsuarioDTO>();
            DataTable Dt = new DataTable();

            var dpParametros = new
            {
                P_Accion = 1
            };

            Dt = Contexto.Funcion_StoreDB(P_Cadena, "spConsultarUsuario", dpParametros);


            if (Dt.Rows.Count > 0)
            {
                lstUsuarioRep = (from item in Dt.AsEnumerable()
                                 select new UsuarioDTO
                                 {
                                     id_usuario = item.Field<int>("id_usuario"),
                                     nombre = item.Field<string>("nombre"),
                                     primer_apellido = item.Field<string>("primer_apellido"),
                                     segundo_apellido = item.Field<string>("segundo_apellido"),
                                     fecha_nacimiento = Convert.ToString(item.Field<DateTime>("fecha_nacimiento")),
                                     sexo = item.Field<string>("sexo"),
                                     celular = item.Field<string>("celular"),
                                     correo = item.Field<string>("correo"),
                                     contrasenia = item.Field<string>("contrasenia"),
                                     nombre_rol = item.Field<string>("nombre_rol"),
                                     es_activo = item.Field<string>("activo"),
                                     fecha_registro = item.Field<string>("fecha_registro")
                                 }
                               ).ToList();
            }

            return lstUsuarioRep;
        }

        public static List<UsuarioRepDTO> ConsultaGeneral(string P_Cadena)
        {
            List<UsuarioRepDTO> lstUsuarioRep = new List<UsuarioRepDTO>();
            DataTable Dt = new DataTable();

            var dpParametros = new
            {
                P_Accion = 2
            };

            Dt = Contexto.Funcion_StoreDB(P_Cadena, "spConsultarUsuario", dpParametros);


            if (Dt.Rows.Count > 0)
            {
                lstUsuarioRep = (from item in Dt.AsEnumerable()
                                 select new UsuarioRepDTO
                                 {
                                     id_usuario = item.Field<int>("id_usuario"),
                                     nombre_completo = item.Field<string>("nombre_completo"),
                                     correo = item.Field<string>("correo"),
                                     rol = item.Field<string>("rol"),
                                     activo = item.Field<string>("activo"),
                                     fecha_registro = item.Field<string>("fecha_registro")
                                 }
                               ).ToList();
            }

            return lstUsuarioRep;
        }

        public static List<UsuarioRepDTO> ConsultaXNombre(string P_Cadena, string P_Letras)
        {
            List<UsuarioRepDTO> lstUsuarioRep = new List<UsuarioRepDTO>();
            DataTable Dt = new DataTable();

            var dpParametros = new
            {
                P_Accion = 3,
                P_NombreC = P_Letras
            };

            Dt = Contexto.Funcion_StoreDB(P_Cadena, "spConsultarUsuario", dpParametros);


            if (Dt.Rows.Count > 0)
            {
                lstUsuarioRep = (from item in Dt.AsEnumerable()
                                 select new UsuarioRepDTO
                                 {
                                     id_usuario = item.Field<int>("id_usuario"),
                                     nombre_completo = item.Field<string>("nombre_completo"),
                                     correo = item.Field<string>("correo"),
                                     rol = item.Field<string>("rol"),
                                     activo = item.Field<string>("activo"),
                                     fecha_registro = item.Field<string>("fecha_registro")
                                 }
                               ).ToList();
            }

            return lstUsuarioRep;
        }

        public static List<string> InsertarUsuario(string P_Cadena, string P_NombreE, string P_PrimerApellidoE, string P_SegundoApellidoE, string P_FechaNacimientoE, string P_SexoE, string P_CelularE, string P_CorreoE, string P_ContraseniaE, int P_EsActivoE, int P_IdRolE)
        {
            List<string> lstMensaje = new List<string>();
            try
            {
                var dpParametros = new
                {
                    P_Nombre = P_NombreE,
                    P_PrimerApellido = P_PrimerApellidoE,
                    P_SegundoApellido = P_SegundoApellidoE,
                    P_FechaNacimiento = P_FechaNacimientoE,
                    P_Sexo = P_SexoE,
                    P_Celular  = P_CelularE,
                    P_Correo = P_CorreoE, // TODO: Investigar una funcionalidad de autenticación de dos factores para la API de Twilio.
                    P_Contrasenia = P_ContraseniaE,
                    P_EsActivo = P_EsActivoE,
                    P_IdRol = P_IdRolE
                };

                Contexto.Procedimiento_StoreDB(P_Cadena, "spCrearUsuario", dpParametros);
                lstMensaje.Add("00"); // TODO: ¿Por qué es "00"?
                lstMensaje.Add("Información Guardada");
            }
            catch (SqlException e)
            {
                lstMensaje.Add("14");
                lstMensaje.Add(e.Message);
            }

            return lstMensaje;
        }

        public static List<string> ActualizarUsuario(string P_Cadena, int P_idUsuarioE, string P_NombreE, string P_PrimerApellidoE, string P_SegundoApellidoE, string P_FechaNacimientoE, string P_SexoE, string P_CelularE, string P_CorreoE, string P_ContraseniaE, int P_EsActivoE, int P_IdRolE)
        {
            List<string> lstMensaje = new List<string>();
            try
            {
                var dpParametros = new
                {
                    P_IdUsuario = P_idUsuarioE,
                    P_Nombre = P_NombreE,
                    P_PrimerApellido = P_PrimerApellidoE,
                    P_SegundoApellido = P_SegundoApellidoE,
                    P_FechaNacimiento = P_FechaNacimientoE,
                    P_Sexo = P_SexoE,
                    P_Correo = P_CorreoE,
                    P_Celular = P_CelularE,
                    P_Contrasenia = P_ContraseniaE,
                    P_IdRol = P_IdRolE,
                    P_EsActivo = P_EsActivoE
                };

                Contexto.Procedimiento_StoreDB(P_Cadena, "spActualizarUsuario", dpParametros);
                lstMensaje.Add("00");
                lstMensaje.Add("Información Actualizada");


            }
            catch (SqlException e)
            {
                lstMensaje.Add("14");
                lstMensaje.Add(e.Message);
            }

            return lstMensaje;
        }

        public static List<string> EliminarUsuario(string P_Cadena, int P_idUsuarioE)
        {
            List<string> lstMensaje = new List<string>();
            try
            {
                var dpParametros = new
                {
                    P_IdUsuario = P_idUsuarioE
                };

                Contexto.Procedimiento_StoreDB(P_Cadena, "spEliminarUsuario", dpParametros);
                lstMensaje.Add("00");
                lstMensaje.Add("Usuario Eliminado");


            }
            catch (SqlException e)
            {
                lstMensaje.Add("14");
                lstMensaje.Add(e.Message);
            }

            return lstMensaje;
        }

    }
}
