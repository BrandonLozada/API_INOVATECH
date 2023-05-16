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
    public class BL_TOKEN
    {
        public static List<UsuarioIdentidadDTO> ObtenerToken(string P_Cadena, Usuario Usuario)
        {
            List<UsuarioIdentidadDTO> lstUsuarioIdentificado = new List<UsuarioIdentidadDTO>();
            DataTable Dt = new DataTable();
      
            var dpParametros = new
            {
                P_Correo = Usuario.correo,
                P_Contrasenia = Usuario.contrasenia,
            };

            Dt = Contexto.Funcion_StoreDB(P_Cadena, "spIdentificarUsuario", dpParametros);

            if (Dt.Rows.Count > 0)
            {
                lstUsuarioIdentificado = (from item in Dt.AsEnumerable()
                                            select new UsuarioIdentidadDTO
                                            {
                                                id_usuario = item.Field<int>("id_usuario"),
                                                nombre = item.Field<string>("nombre"),
                                                primer_apellido = item.Field<string>("primer_apellido"),
                                                segundo_apellido = item.Field<string>("segundo_apellido"),
                                                fecha_nacimiento = item.Field<DateTime>("fecha_nacimiento").ToString("yyyy/MM/dd"),
                                                sexo = item.Field<string>("sexo"),
                                                celular = item.Field<string>("celular"),
                                                correo = item.Field<string>("correo"),
                                                es_activo = item.Field<bool>("es_activo"),
                                                id_rol = item.Field<int>("id_rol")
                                            }
                                ).ToList();
            }
               
            return lstUsuarioIdentificado;

        }
    }


}
