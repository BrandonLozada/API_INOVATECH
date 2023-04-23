using DAL;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BL_PERMISO
    {
        public static List<PermisoRepDTO> ConsultaPermisos(string P_Cadena)
        {
            List<PermisoRepDTO> lstPermisoRep = new List<PermisoRepDTO>();
            DataTable Dt = new DataTable();

            var dpParametros = new
            {
                
            };

            Dt = Contexto.Funcion_StoreDB(P_Cadena, "spPermisos", dpParametros);


            if (Dt.Rows.Count > 0)
            {
                lstPermisoRep = (from item in Dt.AsEnumerable()
                                      select new PermisoRepDTO
                                      {
                                          id_permiso = item.Field<int>("id_permiso"),
                                          nombre = item.Field<string>("nombre")
                                      }
                               ).ToList();
            }

            return lstPermisoRep;
        }

        public static List<string> InsertarSolicitudPermiso(string P_Cadena, int P_id_usuario_solicitante, int P_id_permiso, string P_fecha_inicio, string P_fecha_fin)
        {
            List<string> lstMensaje = new List<string>();
            DateTime fechaini = Convert.ToDateTime(P_fecha_inicio);
            DateTime fechafin = Convert.ToDateTime(P_fecha_fin);

            var diffDias =  fechafin - fechaini;

            try
            {
                var dpParametros = new
                {
                    P_IdUsuarioSolicitante = P_id_usuario_solicitante,
                    P_IdPermiso = P_id_permiso,
                    P_Dias = diffDias.Days,
                    P_Fechainicio = P_fecha_inicio,
                    P_FechaFin = P_fecha_fin
                };

                Contexto.Procedimiento_StoreDB(P_Cadena, "spSolicitarPermiso", dpParametros);
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
    }
}
